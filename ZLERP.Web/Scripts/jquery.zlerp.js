;(function($){ 
        $.fn.dropdownTree = function(options){
            var caller = $(this);
            var ddt = new DropDownTree(caller,options);
            caller.bind('focus',function(){
                ddt.showMenu();
            });
                 
        };
    })(jQuery);

       /*
       @ 下拉树形菜单
       */
    var DropDownTree = function (caller, options) {
        this.containerId = "ddt_container";
        this.treeId = "ddt_container_tree";
        this.menuContainer = $('#' + this.containerId);
        this.tree = $('#' + this.treeId);
        var ddt = this;
        var target = $(caller);
        var valueTarget = target;
        if (options.showField != options.valueField) {
            //创建隐藏的valueField
            var fieldName = target.attr('name');
            if (fieldName.indexOf('_text') <= 0) {
                var textFieldName = fieldName + '_text';
                target.attr('name', textFieldName);
                valueTarget = $('<input type="hidden" name="' + fieldName + '"/>');
                var button = $('<button></button>');

                target.after(valueTarget);
            }
            else {
                valueTarget = $('input[name="' + fieldName.replace('_text', '') + '"]');
            }

        }
        if (options.hasOwnProperty('defaultVal') && options.defaultVal != "") {
            valueTarget.val(options.defaultVal);
        }

        this.onClick = function (e, treeId, treeNode) {
            var zTree = $.fn.zTree.getZTreeObj(treeId),
			nodes = zTree.getSelectedNodes(),
			v = "";
            if (nodes.length > 0) {
                target.attr("value", eval('nodes[0].' + options.showField));
                if (options.showField != options.valueField) {
                    valueTarget.attr("value", eval('nodes[0].' + options.valueField));
                }
                ddt.hideMenu();
            }
        };
        this.treeSettings = {
            view: { selectedMulti: false },
            data: {
                simpleData: {
                    enable: true
                }
            },
            async: {
                enable: true,
                url: options.url,
                autoParam: ["id", "name", "level"]
                // otherParam: { "otherParam": "zTreeAsyncTest" }　
            },
            callback: {
                onClick: this.onClick
            }
        };

        this.showMenu = function () {
            var targetOffset = target.offset();

            if (this.menuContainer.length == 0) {
                //创建menuContainer
                $('body').append('<div id="' + this.containerId + '"><ul id="' + this.treeId + '" class="ztree"></ul></div>');
                this.menuContainer = $('#' + this.containerId);
                this.tree = $('#' + this.treeId);
            }
            this.menuContainer.css({ left: targetOffset.left + "px",
                top: targetOffset.top + target.outerHeight() + "px"
            });

            $.fn.zTree.init(this.tree, this.treeSettings);
            this.menuContainer.slideDown("fast");

            $("body").bind("click", ddt.onBodyDown);
        };
        this.hideMenu = function () {
            this.menuContainer.fadeOut("fast");
            $("body").unbind("click", this.onBodyDown);
        };
        this.onBodyDown = function (event) {
            if (!(event.target.id == $(caller).attr('id') || event.target.id == ddt.containerId || $(event.target).parents("#" + ddt.containerId).length > 0)) {
                ddt.hideMenu();
            };

        };


        // this.showMenu();
    };
/*autocomplete 封装*/
(function ($) {
    $.fn.autocp = function (options) {
        var caller = $(this);
        function __highlight(s, t) {
            var matcher = new RegExp("(" + $.ui.autocomplete.escapeRegex(t) + ")", "ig");
            return s.replace(matcher, "<strong>$1</strong>");
        }
        var valueTarget;

        var _data = {condition:'',q:''};
        //如果未在AutoCp中设置callback，则condition取原始的condition xyl 2013-08-07
        var initCondition = getParamValue(options.url,"condition");
        if(!isEmpty(initCondition)){
            _data.condition = decodeURIComponent(initCondition);//此处需要解码
        }
        //创建隐藏的valueField
        var fieldName = caller.attr('name');
        var textFieldName = options.displayInputName;
        if (fieldName != textFieldName) {             
            caller.attr('name', textFieldName);
            caller.addClass("ui-widget-content ui-corner-left ui-autocomplete-input");
            this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "点击选择")
					.insertAfter(caller)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all ui-widget")
					.addClass("ui-corner-right ui-button-icon")
					.click(function () {
					    // close if already visible
					    if (caller.autocomplete("widget").is(":visible")) {
					        caller.autocomplete("close");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();
                        if(options.extraCallback){
                            var returnVal = extraCallback(options.extraCallback);
                            //此处改变条件
                            if(returnVal){
                                _data.condition = returnVal.changedCondition;
                            }
                        }
					    // pass empty string as value to search for, displaying all results
					    caller.autocomplete("search", "");
					    caller.focus();
					});

            valueTarget = $('<input type="hidden" name="' + fieldName + '"/>');
            valueTarget.insertAfter(this.button);
        }
        else {             
            fieldName = options.valueInputName;
            valueTarget = $('input[name="' + fieldName + '"]');
        }
        var cache = {},lastXhr;
        var allowCache = options.allowCache;
        $(this).autocomplete({
            delay: 300,
            minLength: 0,
            source: function (request, response) {
//                if(isEmpty(caller.val()) && isEmpty(request.term)){//如果选择的值与输入的值均为空，则删除隐藏字段的值
//                    valueTarget.val("");
//                }
                var term = request.term;
				if ( allowCache && term in cache ) {
					response( cache[ term ] );
					return;
				}
                _data.q = request.term;                 
                $.ajax({
                    url: options.url,
                    dataType: 'json',
                    type: 'POST',
                    beforeSend:function(XMLHttpRequest){
                        if(options.extraCallback){
                            var returnVal = extraCallback(options.extraCallback);
                            //此处改变条件
                            if(returnVal){
                                _data.condition = returnVal.changedCondition;
                            }
                        }
                    },
                    data: _data,
                    success: function (data) { 
                       var mappedData = $.map(data, function (item) {
                            var text = item.Text;
                            if(text.indexOf('<strong>') >=0){
                                text = item.Text.substring(0, item.Text.indexOf('</strong>')).replace('<strong>','')
                            }
                            return { 
                                label: item.Text, //__highlight(item.Text, request.term) + " (" + item.Value + ")",
                                value: item.Value,
                                //用以赋值给textbox，显示给用户
                                text: text
                            };

                        });
                        cache[term] = mappedData;
                        response(mappedData);
                    }
                });
            },            
            select: function (event, ui) {                 
                caller.val(ui.item.text);
                valueTarget.val(ui.item.value);
                caller.data(ui.item.value+"_cache",ui.item);
                valueTarget.trigger('change');
                $(this).trigger('blur');                    
                return false;
            },
            change: function (event, ui) {
                if (!ui.item) {
                    //$(this).val('');
                    valueTarget.val('');
                }
                valueTarget.trigger('change'); 
             }
        }).data("autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>")
                                    .data("item.autocomplete", item)
                                    .append($("<a></a>").html(item.label))
                                    .appendTo(ul);
        };
        
    }
})(jQuery);

(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "";

//            var valueInput = this.valueInput = $("<input type='hidden'/>")
//                .insertAfter(select)
//                .val(value)
//                .attr('name',select.attr('name'));
//            select.attr('name','');
            var input = this.input = $("<input>")
					.insertAfter(select)                    
					.val(selected.text())
                    .attr('name',select.attr('name'))
                    .css('width',select.width()<103 ? "103px" : select.css('width'))
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function (event, ui) {
					        if (!ui.item) {
					            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function () {
					                if ($(this).text().match(matcher)) {
					                    this.selected = valid = true;
					                    return false;
					                }
					            });
					            if (!valid) {
					                // remove invalid value, as it didn't match anything
					              //  $(this).val("");
                                   var val = $(this).val();
                                    select.append('<option value="'+val+'" selected="selected">'+val+'</option>');
					                 
					                input.data("autocomplete").term = "";
					                return false;
					            }
					        }
                            else
                            {
                                $(this).trigger('change');
                            }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");
            select.attr('name','');
            //copy the validate attrs
            //by:Sky
            var copied = [];
            var attrs = select[0].attributes;
            for(var i=0;i<attrs.length;i++){
                if(attrs[i].name.indexOf('data') == 0){
                    input.attr(attrs[i].name, attrs[i].value);
                    copied.push(attrs[i].name);
                 }
            }
            //remove origin select attr
            for(var i=0; i<copied.length;i++){            
                 select.removeAttr(copied[i]);
            }
            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
            };
            
            this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "点击选择")
					.insertAfter(input)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-button-icon")
					.click(function () {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("close");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
             
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);

//扩展jqgrid,增加datetime formatter, colMod使用方式formatter:'datetime'
(function ($) {
    $.fn.fmatter.datetime = function (cellval, opts, rwd, act) {
        var op = $.extend({}, opts.date);
        if (!$.fmatter.isUndefined(opts.colModel.formatoptions)) {
            op = $.extend({}, op, opts.colModel.formatoptions);
        }
        if (!op.reformatAfterEdit && act == 'edit') {
            return $.fn.fmatter.defaultFormat(cellval, opts);
        } else if (!$.fmatter.isEmpty(cellval)) {
            return $.fmatter.util.DateFormat(op.srcformat, cellval, 'Y-m-d H:i:s', op);
        } else {
            return $.fn.fmatter.defaultFormat(cellval, opts);
        }
    };
    $.fn.fmatter.ym = function(cellval,opts,rwd,act){
        var op = $.extend({}, opts.date);
        if (!$.fmatter.isUndefined(opts.colModel.formatoptions)) {
            op = $.extend({}, op, opts.colModel.formatoptions);
        }
        if (!op.reformatAfterEdit && act == 'edit') {
            return $.fn.fmatter.defaultFormat(cellval, opts);
        } else if (!$.fmatter.isEmpty(cellval)) {
            return $.fmatter.util.DateFormat(op.srcformat, cellval, 'Y-m', op);
        } else {
            return $.fn.fmatter.defaultFormat(cellval, opts);
        }
    };
})(jQuery);
//formatter defaults
$.jgrid.formatter.currency = { thousandsSeparator: ",", prefix: "￥", defaultValue: '￥0.00' };
$.jgrid.formatter.number = { thousandsSeparator: "," ,defaultValue:'0'};
(function ($) {
    $.fn.checkboxlist = function (options) {
        var caller = $(this);
        var ul = $('<ul>').appendTo(caller);
        for (var i = 0; i < options.data.length; i++) {
            var d = options.data[i];
            var checked = '';
            if (d.checked) {
                checked = 'checked="checked"';
            }
            var id = d.value + "_" + i;
            $('<li></li>')
            .append('<label for="' + id + '"><input id="' + id + '" ' + checked + ' type="checkbox" value="' + d.value + '"/>' + d.text + '</label>')
            .appendTo(ul);
        }
    };
})(jQuery);

$.jgrid.extend({
    editCell: function (iRow, iCol, ed) {
        return this.each(function () {
            var $t = this, nm, tmp, cc, cm;
            if (!$t.grid || $t.p.cellEdit !== true) { return; }
            iCol = parseInt(iCol, 10);
            // select the row that can be used for other methods
            $t.p.selrow = $t.rows[iRow].id;
            $($t.p.selarrrow).each(function (i, n) {
                $($t).jqGrid("setSelection", n, false);
            });
            $($t).jqGrid("setSelection", $t.rows[iRow].id, false);
            if (!$t.p.knv) { $($t).jqGrid("GridNav"); }
            // check to see if we have already edited cell
            if ($t.p.savedRow.length > 0) {
                // prevent second click on that field and enable selects
                if (ed === true) {
                    if (iRow == $t.p.iRow && iCol == $t.p.iCol) {
                        return;
                    }
                }
                // save the cell
                $($t).jqGrid("saveCell", $t.p.savedRow[0].id, $t.p.savedRow[0].ic);
            } else {
                window.setTimeout(function () { $("#" + $t.p.knv).attr("tabindex", "-1").focus(); }, 0);
            }
            cm = $t.p.colModel[iCol];
            nm = (cm.alias != undefined ? cm.alias : cm.name);
            if (nm == 'subgrid' || nm == 'cb' || nm == 'rn') { return; }
            cc = $("td:eq(" + iCol + ")", $t.rows[iRow]);
            if (cm.editable === true && ed === true && !cc.hasClass("not-editable-cell")) {
                if (parseInt($t.p.iCol, 10) >= 0 && parseInt($t.p.iRow, 10) >= 0) {
                    $("td:eq(" + $t.p.iCol + ")", $t.rows[$t.p.iRow]).removeClass("edit-cell ui-state-highlight");
                    $($t.rows[$t.p.iRow]).removeClass("selected-row ui-state-hover");
                }
                $(cc).addClass("edit-cell ui-state-highlight");
                $($t.rows[iRow]).addClass("selected-row ui-state-hover");
                try {
                    tmp = $.unformat(cc, { rowId: $t.rows[iRow].id, colModel: cm }, iCol);
                } catch (_) {
                    tmp = (cm.edittype && cm.edittype == 'textarea') ? $(cc).text() : $(cc).html();
                }
                if ($t.p.autoencode) { tmp = $.jgrid.htmlDecode(tmp); }
                if (!cm.edittype) { cm.edittype = "text"; }
                $t.p.savedRow.push({ id: iRow, ic: iCol, name: nm, v: tmp });
                if (tmp == "&nbsp;" || tmp == "&#160;" || (tmp.length == 1 && tmp.charCodeAt(0) == 160)) { tmp = ''; }
                if ($.isFunction($t.p.formatCell)) {
                    var tmp2 = $t.p.formatCell.call($t, $t.rows[iRow].id, nm, tmp, iRow, iCol);
                    if (tmp2 !== undefined) { tmp = tmp2; }
                }
                var opt = $.extend({}, cm.editoptions || {}, { id: iRow + "_" + nm, name: nm });
                var elc = $.jgrid.createEl(cm.edittype, opt, tmp, true, $.extend({}, $.jgrid.ajaxOptions, $t.p.ajaxSelectOptions || {}));
                if ($.isFunction($t.p.beforeEditCell)) {
                    $t.p.beforeEditCell.call($t, $t.rows[iRow].id, nm, tmp, iRow, iCol);
                }
                $(cc).html("").append(elc).attr("tabindex", "0");
                window.setTimeout(function () { $(elc).focus(); }, 0);
                $("input, select, textarea", cc).bind("change", function (e) {
                    $($t).jqGrid("saveCell", iRow, iCol);
                });
                $("td", $t).not(cc).bind("mousedown", function () {
                    $("input, select, textarea", cc).unbind("change");
                });
                $("input, select, textarea", cc).bind("keydown", function (e) {
                    if (e.keyCode === 27) {
                        if ($("input.hasDatepicker", cc).length > 0) {
                            if ($(".ui-datepicker").is(":hidden")) { $($t).jqGrid("restoreCell", iRow, iCol); }
                            else { $("input.hasDatepicker", cc).datepicker('hide'); }
                        } else {
                            $($t).jqGrid("restoreCell", iRow, iCol);
                        }
                    } //ESC
                    if (e.keyCode === 13) {
                        $("input, select, textarea", cc).unbind("change");
                        $($t).jqGrid("saveCell", iRow, iCol);
                        if ($t.p.savedRow.length === 0) {
                            if ($t.rows[iRow + 1]) {
                                $($t).jqGrid("editCell", iRow + 1, iCol, true);
                                var editCell = $("td:eq(" + iCol + ")", $t.rows[iRow + 1]);
                                $(":text", editCell).focus().select();
                            }
                        }
                        else {
                            var complete = $t.p.gridComplete;
                            $t.p.gridComplete = function () {
                                if ($t.rows[iRow + 1]) {
                                    $($t).jqGrid("editCell", iRow + 1, iCol, true);
                                    var editCell = $("td:eq(" + iCol + ")", $t.rows[iRow + 1]);
                                    $(":text", editCell).focus().select();
                                }
                                $t.p.gridComplete = complete;
                                if ($t.p.gridComplete) {
                                    complete();
                                }
                            };
                        }
                    } //Enter
                    if (e.keyCode == 9) {
                        $("input, select, textarea", cc).unbind("change");
                        if (!$t.grid.hDiv.loading) {
                            if (e.shiftKey) {
                                $($t).jqGrid("prevCell", iRow, iCol);
                                var nCol = false;
                                for (var i = iCol - 1; i >= 0; i--) {
                                    if ($t.p.colModel[i].editable === true) {
                                        nCol = i; break;
                                    }
                                }
                                if (nCol !== false) {
                                    var editCell = $("td:eq(" + nCol + ")", $t.rows[iRow]);
                                    $(":text", editCell).focus().select();
                                }
                                if ($t.p.savedRow.length > 1) {
                                    var complete = $t.p.gridComplete;
                                    $t.p.gridComplete = function () {
                                        if (nCol !== false) {
                                            $($t).jqGrid("editCell", iRow, nCol, true);
                                            var editCell = $("td:eq(" + nCol + ")", $t.rows[iRow]);
                                            $(":text", editCell).focus().select();
                                        }
                                        $t.p.gridComplete = complete;
                                        if ($t.p.gridComplete) {
                                            complete();
                                        }
                                    };
                                }
                            } //Shift TAb
                            else {
                                $($t).jqGrid("nextCell", iRow, iCol);
                                var nCol = false;
                                for (var i = iCol + 1; i < $t.p.colModel.length; i++) {
                                    if ($t.p.colModel[i].editable === true) {
                                        nCol = i; break;
                                    }
                                }
                                if (nCol !== false) {
                                    var editCell = $("td:eq(" + nCol + ")", $t.rows[iRow]);
                                    $(":text", editCell).focus().select();
                                }
                                if ($t.p.savedRow.length > 1) {
                                    var complete = $t.p.gridComplete;
                                    $t.p.gridComplete = function () {
                                        if (nCol !== false) {
                                            $($t).jqGrid("editCell", iRow, nCol, true);
                                            var editCell = $("td:eq(" + nCol + ")", $t.rows[iRow]);
                                            $(":text", editCell).focus().select();
                                        }
                                        $t.p.gridComplete = complete;
                                        if ($t.p.gridComplete) {
                                            complete();
                                        }
                                    };
                                }
                            } //Tab
                        } else {
                            return false;
                        }
                    }
                    e.stopPropagation();
                });
                if ($.isFunction($t.p.afterEditCell)) {
                    $t.p.afterEditCell.call($t, $t.rows[iRow].id, nm, tmp, iRow, iCol);
                }
            } else {
                if (parseInt($t.p.iCol, 10) >= 0 && parseInt($t.p.iRow, 10) >= 0) {
                    $("td:eq(" + $t.p.iCol + ")", $t.rows[$t.p.iRow]).removeClass("edit-cell ui-state-highlight");
                    $($t.rows[$t.p.iRow]).removeClass("selected-row ui-state-hover");
                }
                cc.addClass("edit-cell ui-state-highlight");
                $($t.rows[iRow]).addClass("selected-row ui-state-hover");
                if ($.isFunction($t.p.onSelectCell)) {
                    tmp = cc.html().replace(/\&#160\;/ig, '');
                    $t.p.onSelectCell.call($t, $t.rows[iRow].id, nm, tmp, iRow, iCol);
                }
            }
            $t.p.iCol = iCol; $t.p.iRow = iRow;
        });
    },
    saveCell: function (iRow, iCol) {
        return this.each(function () {
            var $t = this, fr;
            if (!$t.grid || $t.p.cellEdit !== true) { return; }
            if ($t.p.savedRow.length >= 1) { fr = 0; } else { fr = null; }
            if (fr !== null) {
                var cc = $("td:eq(" + iCol + ")", $t.rows[iRow]), v, v2,
				cm = $t.p.colModel[iCol], nm = (cm.alias != undefined ? cm.alias : cm.name), nmjq = $.jgrid.jqID(nm);
                switch (cm.edittype) {
                    case "select":
                        if (!cm.editoptions.multiple) {
                            v = $("#" + iRow + "_" + nmjq + ">option:selected", $t.rows[iRow]).val();
                            v2 = $("#" + iRow + "_" + nmjq + ">option:selected", $t.rows[iRow]).text();
                        } else {
                            var sel = $("#" + iRow + "_" + nmjq, $t.rows[iRow]), selectedText = [];
                            v = $(sel).val();
                            if (v) { v.join(","); } else { v = ""; }
                            $("option:selected", sel).each(
								function (i, selected) {
								    selectedText[i] = $(selected).text();
								}
							);
                            v2 = selectedText.join(",");
                        }
                        if (cm.formatter) { v2 = v; }
                        break;
                    case "checkbox":
                        var cbv = ["Yes", "No"];
                        if (cm.editoptions) {
                            cbv = cm.editoptions.value.split(":");
                        }
                        v = $("#" + iRow + "_" + nmjq, $t.rows[iRow]).is(":checked") ? cbv[0] : cbv[1];
                        v2 = v;
                        break;
                    case "password":
                    case "text":
                    case "textarea":
                    case "button":
                        v = $("#" + iRow + "_" + nmjq, $t.rows[iRow]).val();
                        v2 = v;
                        break;
                    case 'custom':
                        try {
                            if (cm.editoptions && $.isFunction(cm.editoptions.custom_value)) {
                                v = cm.editoptions.custom_value.call($t, $(".customelement", cc), 'get');
                                if (v === undefined) { throw "e2"; } else { v2 = v; }
                            } else { throw "e1"; }
                        } catch (e) {
                            if (e == "e1") { $.jgrid.info_dialog(jQuery.jgrid.errors.errcap, "function 'custom_value' " + $.jgrid.edit.msg.nodefined, jQuery.jgrid.edit.bClose); }
                            if (e == "e2") { $.jgrid.info_dialog(jQuery.jgrid.errors.errcap, "function 'custom_value' " + $.jgrid.edit.msg.novalue, jQuery.jgrid.edit.bClose); }
                            else { $.jgrid.info_dialog(jQuery.jgrid.errors.errcap, e.message, jQuery.jgrid.edit.bClose); }
                        }
                        break;
                }
                // The common approach is if nothing changed do not do anything
                if (v2 !== $t.p.savedRow[fr].v) {
                    if ($.isFunction($t.p.beforeSaveCell)) {
                        var vv = $t.p.beforeSaveCell.call($t, $t.rows[iRow].id, nm, v, iRow, iCol);
                        if (vv) { v = vv; v2 = vv; }
                    }
                    var cv = $.jgrid.checkValues(v, iCol, $t);
                    if (cv[0] === true) {
                        var addpost = {};
                        if ($.isFunction($t.p.beforeSubmitCell)) {
                            addpost = $t.p.beforeSubmitCell.call($t, $t.rows[iRow].id, nm, v, iRow, iCol);
                            if (!addpost) { addpost = {}; }
                        }
                        if ($("input.hasDatepicker", cc).length > 0) { $("input.hasDatepicker", cc).datepicker('hide'); }
                        if ($t.p.cellsubmit == 'remote') {
                            if ($t.p.cellurl) {
                                var postdata = {};
                                if ($t.p.autoencode) { v = $.jgrid.htmlEncode(v); }
                                postdata[nm] = v;
                                var idname, oper, opers;
                                opers = $t.p.prmNames;
                                idname = opers.id;
                                oper = opers.oper;
                                postdata[idname] = $.jgrid.stripPref($t.p.idPrefix, $t.rows[iRow].id);
                                postdata[oper] = opers.editoper;
                                postdata = $.extend(addpost, postdata);
                                $("#lui_" + $t.p.id).show();
                                $t.grid.hDiv.loading = true;
                                $.ajax($.extend({
                                    url: $t.p.cellurl,
                                    data: $.isFunction($t.p.serializeCellData) ? $t.p.serializeCellData.call($t, postdata) : postdata,
                                    type: "POST",
                                    complete: function (result, stat) {
                                        $("#lui_" + $t.p.id).hide();
                                        $t.grid.hDiv.loading = false;
                                        if (stat == 'success') {
                                            if ($.isFunction($t.p.afterSubmitCell)) {
                                                var ret = $t.p.afterSubmitCell.call($t, result, postdata.id, nm, v, iRow, iCol);
                                                if (ret[0] === true) {
                                                    $(cc).empty();
                                                    $($t).jqGrid("setCell", $t.rows[iRow].id, iCol, v2, false, false, true);
                                                    $(cc).addClass("dirty-cell-remote");
                                                    $($t.rows[iRow]).addClass("edited-remote");
                                                    if ($.isFunction($t.p.afterSaveCell)) {
                                                        $t.p.afterSaveCell.call($t, $t.rows[iRow].id, nm, v, iRow, iCol);
                                                    }
                                                    $t.p.savedRow.splice(0, 1);
                                                } else {
                                                    $.jgrid.info_dialog($.jgrid.errors.errcap, ret[1], $.jgrid.edit.bClose);
                                                    $($t).jqGrid("restoreCell", iRow, iCol);
                                                }
                                            } else {
                                                $(cc).empty();
                                                $($t).jqGrid("setCell", $t.rows[iRow].id, iCol, v2, false, false, true);
                                                $(cc).addClass("dirty-cell");
                                                $($t.rows[iRow]).addClass("edited");
                                                if ($.isFunction($t.p.afterSaveCell)) {
                                                    $t.p.afterSaveCell.call($t, $t.rows[iRow].id, nm, v, iRow, iCol);
                                                }
                                                $t.p.savedRow.splice(0, 1);
                                            }
                                        }
                                    },
                                    error: function (res, stat) {
                                        $("#lui_" + $t.p.id).hide();
                                        $t.grid.hDiv.loading = false;
                                        if ($.isFunction($t.p.errorCell)) {
                                            $t.p.errorCell.call($t, res, stat);
                                            $($t).jqGrid("restoreCell", iRow, iCol);
                                        } else {
                                            $.jgrid.info_dialog($.jgrid.errors.errcap, res.status + " : " + res.statusText + "<br/>" + stat, $.jgrid.edit.bClose);
                                            $($t).jqGrid("restoreCell", iRow, iCol);
                                        }
                                    }
                                }, $.jgrid.ajaxOptions, $t.p.ajaxCellOptions || {}));
                            } else {
                                try {
                                    $.jgrid.info_dialog($.jgrid.errors.errcap, $.jgrid.errors.nourl, $.jgrid.edit.bClose);
                                    $($t).jqGrid("restoreCell", iRow, iCol);
                                } catch (e) { }
                            }
                        }
                        if ($t.p.cellsubmit == 'clientArray') {
                            $(cc).empty();
                            $($t).jqGrid("setCell", $t.rows[iRow].id, iCol, v2, false, false, true);
                            $(cc).addClass("dirty-cell");
                            $($t.rows[iRow]).addClass("edited");
                            if ($.isFunction($t.p.afterSaveCell)) {
                                $t.p.afterSaveCell.call($t, $t.rows[iRow].id, nm, v, iRow, iCol);
                            }
                            $t.p.savedRow.splice(0, 1);
                        }
                    } else {
                        try {
                            window.setTimeout(function () { $.jgrid.info_dialog($.jgrid.errors.errcap, v + " " + cv[1], $.jgrid.edit.bClose); }, 100);
                            $($t).jqGrid("restoreCell", iRow, iCol);
                        } catch (e) { }
                    }
                } else {
                    $($t).jqGrid("restoreCell", iRow, iCol);
                }
            }
            if ($.browser.opera) {
                $("#" + $t.p.knv).attr("tabindex", "-1").focus();
            } else {
                window.setTimeout(function () { $("#" + $t.p.knv).attr("tabindex", "-1").focus(); }, 0);
            }
        });
    },
    getChangedCells: function (mthd) {
        var ret = [];
        if (!mthd) { mthd = 'all'; }
        this.each(function () {
            var $t = this, nm;
            if (!$t.grid || $t.p.cellEdit !== true) { return; }
            $($t.rows).each(function (j) {
                var res = {};
                if ($(this).hasClass("edited")) {
                    $('td', this).each(function (i) {
                        nm = $t.p.colModel[i].name;
                        if (nm !== 'cb' && nm !== 'subgrid') {
                            if ($(this).hasClass('edit-cell')) {
                                $($t).jqGrid("restoreCell", j, i);
                            }
                            if (mthd == 'dirty') {
                                if ($(this).hasClass('dirty-cell')) {
                                    try {
                                        res[nm] = $.unformat(this, { rowId: $t.rows[j].id, colModel: $t.p.colModel[i] }, i);
                                    } catch (e) {
                                        res[nm] = $.jgrid.htmlDecode($(this).html());
                                    }
                                }
                            } else {
                                try {
                                    res[nm] = $.unformat(this, { rowId: $t.rows[j].id, colModel: $t.p.colModel[i] }, i);
                                } catch (e) {
                                    res[nm] = $.jgrid.htmlDecode($(this).html());
                                }
                            }
                        }
                    });
                    res.id = this.id;
                    ret.push(res);
                }
            });
        });
        return ret;
    }
});

(function( $, undefined ) {

$.widget( "ui.autocomplete", {
	options: {
		appendTo: "body",
		delay: 300,
		minLength: 1,
		position: {
			my: "left top",
			at: "left bottom",
			collision: "none"
		},
		source: null
	},
	_create: function() {
		var self = this,
			doc = this.element[ 0 ].ownerDocument,
			suppressKeyPress;

		this.element
			.addClass( "ui-autocomplete-input" )
			.attr( "autocomplete", "off" )
			// TODO verify these actually work as intended
			.attr({
				role: "textbox",
				"aria-autocomplete": "list",
				"aria-haspopup": "true"
			})
			.bind( "keydown.autocomplete", function( event ) {
				if ( self.options.disabled || self.element.attr( "readonly" ) ) {
					return;
				}

				suppressKeyPress = false;
				var keyCode = $.ui.keyCode;
				switch( event.keyCode ) {
				case keyCode.PAGE_UP:
					self._move( "previousPage", event );
					break;
				case keyCode.PAGE_DOWN:
					self._move( "nextPage", event );
					break;
				case keyCode.UP:
					self._move( "previous", event );
					// prevent moving cursor to beginning of text field in some browsers
					event.preventDefault();
					break;
				case keyCode.DOWN:
					self._move( "next", event );
					// prevent moving cursor to end of text field in some browsers
					event.preventDefault();
					break;
				case keyCode.ENTER:
				case keyCode.NUMPAD_ENTER:
					// when menu is open and has focus
					if ( self.menu.active ) {
						// #6055 - Opera still allows the keypress to occur
						// which causes forms to submit
						suppressKeyPress = true;
						event.preventDefault();
					}
					//passthrough - ENTER and TAB both select the current element
				case keyCode.TAB:
					if ( !self.menu.active ) {
						return;
					}
					self.menu.select( event );
					break;
				case keyCode.ESCAPE:
					self.element.val( self.term );
					self.close( event );
					break;
				default:
					// keypress is triggered before the input value is changed
					clearTimeout( self.searching );
					self.searching = setTimeout(function() {
						// only search if the value has changed
						if ( self.term != self.element.val() ) {
							self.selectedItem = null;
							self.search( null, event );
						}
					}, self.options.delay );
					break;
				}
			})
			.bind( "keypress.autocomplete", function( event ) {
				if ( suppressKeyPress ) {
					suppressKeyPress = false;
					event.preventDefault();
				}
			})
			.bind( "focus.autocomplete", function() {
				if ( self.options.disabled ) {
					return;
				}

				self.selectedItem = null;
				self.previous = self.element.val();
			})
			.bind( "blur.autocomplete", function( event ) {
				if ( self.options.disabled ) {
					return;
				}

				clearTimeout( self.searching );
				// clicks on the menu (or a button to trigger a search) will cause a blur event
				self.closing = setTimeout(function() {
					self.close( event );
					self._change( event );
				}, 150 );
			}).bind('input',function(c){//jQuery UI Autocomplete 1.8.*中文输入修正
				self.search(self.item);
			});
		this._initSource();
		this.response = function() {
			return self._response.apply( self, arguments );
		};
		this.menu = $( "<ul></ul>" )
			.addClass( "ui-autocomplete" )
			.appendTo( $( this.options.appendTo || "body", doc )[0] )
			// prevent the close-on-blur in case of a "slow" click on the menu (long mousedown)
			.mousedown(function( event ) {
				// clicking on the scrollbar causes focus to shift to the body
				// but we can't detect a mouseup or a click immediately afterward
				// so we have to track the next mousedown and close the menu if
				// the user clicks somewhere outside of the autocomplete
				var menuElement = self.menu.element[ 0 ];
				if ( !$( event.target ).closest( ".ui-menu-item" ).length ) {
					setTimeout(function() {
						$( document ).one( 'mousedown', function( event ) {
							if ( event.target !== self.element[ 0 ] &&
								event.target !== menuElement &&
								!$.ui.contains( menuElement, event.target ) ) {
								self.close();
							}
						});
					}, 1 );
				}

				// use another timeout to make sure the blur-event-handler on the input was already triggered
				setTimeout(function() {
					clearTimeout( self.closing );
				}, 13);
			})
			.menu({
				focus: function( event, ui ) {
					var item = ui.item.data( "item.autocomplete" );
					if ( false !== self._trigger( "focus", event, { item: item } ) ) {
						// use value to match what will end up in the input, if it was a key event
						if ( /^key/.test(event.originalEvent.type) ) {
							self.element.val( item.value );
						}
					}
				},
				selected: function( event, ui ) {
					var item = ui.item.data( "item.autocomplete" ),
						previous = self.previous;

					// only trigger when focus was lost (click on menu)
					if ( self.element[0] !== doc.activeElement ) {
						self.element.focus();
						self.previous = previous;
						// #6109 - IE triggers two focus events and the second
						// is asynchronous, so we need to reset the previous
						// term synchronously and asynchronously :-(
						setTimeout(function() {
							self.previous = previous;
							self.selectedItem = item;
						}, 1);
					}

					if ( false !== self._trigger( "select", event, { item: item } ) ) {
						self.element.val( item.value );
					}
					// reset the term after the select event
					// this allows custom select handling to work properly
					self.term = self.element.val();

					self.close( event );
					self.selectedItem = item;
				},
				blur: function( event, ui ) {
					// don't set the value of the text field if it's already correct
					// this prevents moving the cursor unnecessarily
					if ( self.menu.element.is(":visible") &&
						( self.element.val() !== self.term ) ) {
						self.element.val( self.term );
					}
				}
			})
			.zIndex( this.element.zIndex() + 1 )
			// workaround for jQuery bug #5781 http://dev.jquery.com/ticket/5781
			.css({ top: 0, left: 0 })
			.hide()
			.data( "menu" );
		if ( $.fn.bgiframe ) {
			 this.menu.element.bgiframe();
		}
	},

	destroy: function() {
		this.element
			.removeClass( "ui-autocomplete-input" )
			.removeAttr( "autocomplete" )
			.removeAttr( "role" )
			.removeAttr( "aria-autocomplete" )
			.removeAttr( "aria-haspopup" );
		this.menu.element.remove();
		$.Widget.prototype.destroy.call( this );
	},

	_setOption: function( key, value ) {
		$.Widget.prototype._setOption.apply( this, arguments );
		if ( key === "source" ) {
			this._initSource();
		}
		if ( key === "appendTo" ) {
			this.menu.element.appendTo( $( value || "body", this.element[0].ownerDocument )[0] )
		}
	},

	_initSource: function() {
		var self = this,
			array,
			url;
		if ( $.isArray(this.options.source) ) {
			array = this.options.source;
			this.source = function( request, response ) {
				response( $.ui.autocomplete.filter(array, request.term) );
			};
		} else if ( typeof this.options.source === "string" ) {
			url = this.options.source;
			this.source = function( request, response ) {
				if (self.xhr) {
					self.xhr.abort();
				}
				self.xhr = $.ajax({
					url: url,
					data: request,
					dataType: "json",
					success: function( data, status, xhr ) {
						if ( xhr === self.xhr ) {
							response( data );
						}
						self.xhr = null;
					},
					error: function( xhr ) {
						if ( xhr === self.xhr ) {
							response( [] );
						}
						self.xhr = null;
					}
				});
			};
		} else {
			this.source = this.options.source;
		}
	},

	search: function( value, event ) {
		value = value != null ? value : this.element.val();

		// always save the actual value, not the one passed as an argument
		this.term = this.element.val();

		if ( value.length < this.options.minLength ) {
			return this.close( event );
		}

		clearTimeout( this.closing );
		if ( this._trigger( "search", event ) === false ) {
			return;
		}

		return this._search( value );
	},

	_search: function( value ) {
		this.element.addClass( "ui-autocomplete-loading" );

		this.source( { term: value }, this.response );
	},

	_response: function( content ) {
		if ( content && content.length ) {
			content = this._normalize( content );
			this._suggest( content );
			this._trigger( "open" );
		} else {
			this.close();
		}
		this.element.removeClass( "ui-autocomplete-loading" );
	},

	close: function( event ) {
		clearTimeout( this.closing );
		if ( this.menu.element.is(":visible") ) {
			this.menu.element.hide();
			this.menu.deactivate();
			this._trigger( "close", event );
		}
	},
	
	_change: function( event ) {
		if ( this.previous !== this.element.val() ) {
			this._trigger( "change", event, { item: this.selectedItem } );
		}
	},

	_normalize: function( items ) {
		// assume all items have the right format when the first item is complete
		if ( items.length && items[0].label && items[0].value ) {
			return items;
		}
		return $.map( items, function(item) {
			if ( typeof item === "string" ) {
				return {
					label: item,
					value: item
				};
			}
			return $.extend({
				label: item.label || item.value,
				value: item.value || item.label
			}, item );
		});
	},

	_suggest: function( items ) {
		var ul = this.menu.element
			.empty()
			.zIndex( this.element.zIndex() + 1 );
		this._renderMenu( ul, items );
		// TODO refresh should check if the active item is still in the dom, removing the need for a manual deactivate
		this.menu.deactivate();
		this.menu.refresh();

		// size and position menu
		ul.show();
		this._resizeMenu();
		ul.position( $.extend({
			of: this.element
		}, this.options.position ));
	},

	_resizeMenu: function() {
		var ul = this.menu.element;
		ul.outerWidth( Math.max(
			ul.width( "" ).outerWidth(),
			this.element.outerWidth()
		) );
	},

	_renderMenu: function( ul, items ) {
		var self = this;
		$.each( items, function( index, item ) {
			self._renderItem( ul, item );
		});
	},

	_renderItem: function( ul, item) {
		return $( "<li></li>" )
			.data( "item.autocomplete", item )
			.append( $( "<a></a>" ).text( item.label ) )
			.appendTo( ul );
	},

	_move: function( direction, event ) {
		if ( !this.menu.element.is(":visible") ) {
			this.search( null, event );
			return;
		}
		if ( this.menu.first() && /^previous/.test(direction) ||
				this.menu.last() && /^next/.test(direction) ) {
			this.element.val( this.term );
			this.menu.deactivate();
			return;
		}
		this.menu[ direction ]( event );
	},

	widget: function() {
		return this.menu.element;
	}
});

$.extend( $.ui.autocomplete, {
	escapeRegex: function( value ) {
		return value.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&");
	},
	filter: function(array, term) {
		var matcher = new RegExp( $.ui.autocomplete.escapeRegex(term), "i" );
		return $.grep( array, function(value) {
			return matcher.test( value.label || value.value || value );
		});
	}
});

}( jQuery ));

(function ($) {

$.fn.jqFilter = function( arg ) {
	if (typeof arg === 'string') {
		
		var fn = $.fn.jqFilter[arg];
		if (!fn) {
			throw ("jqFilter - No such method: " + arg);
		}
		var args = $.makeArray(arguments).slice(1);
		return fn.apply(this,args);
	}

	var p = $.extend(true,{
		filter: null,
		columns: [],
		onChange : null,
		afterRedraw : null,
		checkValues : null,
		error: false,
		errmsg : "",
		errorcheck : true,
		showQuery : true,
		sopt : null,
		ops : [
			{"name": "eq", "description": "equal", "operator":"="},
			{"name": "ne", "description": "not equal", "operator":"<>"},
			{"name": "lt", "description": "less", "operator":"<"},
			{"name": "le", "description": "less or equal","operator":"<="},
			{"name": "gt", "description": "greater", "operator":">"},
			{"name": "ge", "description": "greater or equal", "operator":">="},
			{"name": "bw", "description": "begins with", "operator":"LIKE"},
			{"name": "bn", "description": "does not begin with", "operator":"NOT LIKE"},
			{"name": "in", "description": "in", "operator":"IN"},
			{"name": "ni", "description": "not in", "operator":"NOT IN"},
			{"name": "ew", "description": "ends with", "operator":"LIKE"},
			{"name": "en", "description": "does not end with", "operator":"NOT LIKE"},
			{"name": "cn", "description": "contains", "operator":"LIKE"},
			{"name": "nc", "description": "does not contain", "operator":"NOT LIKE"},
			{"name": "nu", "description": "is null", "operator":"IS NULL"},
			{"name": "nn", "description": "is not null", "operator":"IS NOT NULL"},
            {"name": "bd","description":"从","operator":">="},
            {"name": "ed","description":"到","operator":"<="}
		],
		numopts : ['eq','ne', 'lt', 'le', 'gt', 'ge', 'nu', 'nn', 'in', 'ni'],
		stropts : ['eq', 'ne', 'bw', 'bn', 'ew', 'en', 'cn', 'nc', 'nu', 'nn', 'in', 'ni','bd','ed'],
		_gridsopt : [], // grid translated strings, do not tuch
		groupOps : [{ op: "AND", text: "并且" },	{ op: "OR",  text: "或者" }],
		groupButton : true,
		ruleButtons : true,
		direction : "ltr"
	}, $.jgrid.filter, arg || {});
	return this.each( function() {
		if (this.filter) {return;}
		this.p = p;
		// setup filter in case if they is not defined
		if (this.p.filter === null || this.p.filter === undefined) {
			this.p.filter = {
				groupOp: this.p.groupOps[0].op,
				rules: [],
				groups: []
			};
		}
		var i, len = this.p.columns.length, cl,
		isIE = /msie/i.test(navigator.userAgent) && !window.opera;

		// translating the options
		if(this.p._gridsopt.length) {
			// ['eq','ne','lt','le','gt','ge','bw','bn','in','ni','ew','en','cn','nc']
			for(i=0;i<this.p._gridsopt.length;i++) {
				this.p.ops[i].description = this.p._gridsopt[i];
			}
		}
		this.p.initFilter = $.extend(true,{},this.p.filter);

		// set default values for the columns if they are not set
		if( !len ) {return;}
		for(i=0; i < len; i++) {
			cl = this.p.columns[i];
			if( cl.stype ) {
				// grid compatibility
				cl.inputtype = cl.stype;
			} else if(!cl.inputtype) {
				cl.inputtype = 'text';
			}
			if( cl.sorttype ) {
				// grid compatibility
				cl.searchtype = cl.sorttype;
			} else if (!cl.searchtype) {
				cl.searchtype = 'string';
			}
			if(cl.hidden === undefined) {
				// jqGrid compatibility
				cl.hidden = false;
			}
			if(!cl.label) {
				cl.label = cl.name;
			}
			if(cl.index) {
				cl.name = cl.index;
			}
			if(!cl.hasOwnProperty('searchoptions')) {
				cl.searchoptions = {};
			}
			if(!cl.hasOwnProperty('searchrules')) {
				cl.searchrules = {};
			}

		}
		if(this.p.showQuery) {
			$(this).append("<table class='queryresult ui-widget ui-widget-content' style='display:block;max-width:440px;border:0px none;' dir='"+this.p.direction+"'><tbody><tr><td class='query'></td></tr></tbody></table>");
		}
		/*
		 *Perform checking.
		 *
		*/
		var checkData = function(val, colModelItem) {
			var ret = [true,""];
			if($.isFunction(colModelItem.searchrules)) {
				ret = colModelItem.searchrules(val, colModelItem);
			} else if($.jgrid && $.jgrid.checkValues) {
				try {
					ret = $.jgrid.checkValues(val, -1, null, colModelItem.searchrules, colModelItem.label);
				} catch (e) {}
			}
			if(ret && ret.length && ret[0] === false) {
				p.error = !ret[0];
				p.errmsg = ret[1];
			}
		};
		/* moving to common
		randId = function() {
			return Math.floor(Math.random()*10000).toString();
		};
		*/

		this.onchange = function (  ){
			// clear any error 
			this.p.error = false;
			this.p.errmsg="";
			return $.isFunction(this.p.onChange) ? this.p.onChange.call( this, this.p ) : false;
		};
		/*
		 * Redraw the filter every time when new field is added/deleted
		 * and field is  changed
		 */
		this.reDraw = function() {
			$("table.group:first",this).remove();
			var t = this.createTableForGroup(p.filter, null);
			$(this).append(t);
			if($.isFunction(this.p.afterRedraw) ) {
				this.p.afterRedraw.call(this, this.p);
			}
		};
		/*
		 * Creates a grouping data for the filter
		 * @param group - object
		 * @param parentgroup - object
		 */
		this.createTableForGroup = function(group, parentgroup) {
			var that = this,  i;
			// this table will hold all the group (tables) and rules (rows)
			var table = $("<table class='group ui-widget ui-widget-content' style='border:0px none;'><tbody></tbody></table>"),
			// create error message row
			align = "left";
			if(this.p.direction == "rtl") {
				align = "right";
				table.attr("dir","rtl");
			}
			if(parentgroup === null) {
				table.append("<tr class='error' style='display:none;'><th colspan='5' class='ui-state-error' align='"+align+"'></th></tr>");
			}

			var tr = $("<tr></tr>");
			table.append(tr);
			// this header will hold the group operator type and group action buttons for
			// creating subgroup "+ {}", creating rule "+" or deleting the group "-"
			var th = $("<th colspan='5' align='"+align+"'></th>");
			tr.append(th);

			if(this.p.ruleButtons === true) {
			// dropdown for: choosing group operator type
			var groupOpSelect = $("<select class='opsel'></select>");
			th.append(groupOpSelect);
			// populate dropdown with all posible group operators: or, and
			var str= "", selected;
			for (i = 0; i < p.groupOps.length; i++) {
				selected =  group.groupOp === that.p.groupOps[i].op ? " selected='selected'" :"";
				str += "<option value='"+that.p.groupOps[i].op+"'" + selected+">"+that.p.groupOps[i].text+"</option>";
			}

			groupOpSelect
			.append(str)
			.bind('change',function() {
				group.groupOp = $(groupOpSelect).val();
				that.onchange(); // signals that the filter has changed
			});
			}
			// button for adding a new subgroup
			var inputAddSubgroup ="<span></span>";
			if(this.p.groupButton) {
				inputAddSubgroup = $("<input type='button' value='+ {}' title='Add subgroup' class='add-group'/>");
				inputAddSubgroup.bind('click',function() {
					if (group.groups === undefined ) {
						group.groups = [];
					}

					group.groups.push({
						groupOp: p.groupOps[0].op,
						rules: [],
						groups: []
					}); // adding a new group

					that.reDraw(); // the html has changed, force reDraw

					that.onchange(); // signals that the filter has changed
					return false;
				});
			}
			th.append(inputAddSubgroup);
			if(this.p.ruleButtons === true) {
			// button for adding a new rule
			var inputAddRule = $("<input type='button' value='再加一个条件' title='增加条件' class='add-rule ui-add'/>"), cm;
			inputAddRule.bind('click',function() {
				//if(!group) { group = {};}
				if (group.rules === undefined) {
					group.rules = [];
				}
				for (i = 0; i < that.p.columns.length; i++) {
				// but show only serchable and serchhidden = true fields
					var searchable = (typeof that.p.columns[i].search === 'undefined') ?  true: that.p.columns[i].search ,
					hidden = (that.p.columns[i].hidden === true),
					ignoreHiding = (that.p.columns[i].searchoptions.searchhidden === true);
					if ((ignoreHiding && searchable) || (searchable && !hidden)) {
						cm = that.p.columns[i];
						break;
					}
				}
				
				var opr;
				if( cm.searchoptions.sopt ) {opr = cm.searchoptions.sopt;}
				else if(that.p.sopt) { opr= that.p.sopt; }
				else if  (cm.searchtype === 'string') {opr = that.p.stropts;}
				else {opr = that.p.numopts;}
				group.rules.push({
					field: cm.name,
					op: opr[0],
					data: ""
				}); // adding a new rule
				that.reDraw(); // the html has changed, force reDraw
				// for the moment no change have been made to the rule, so
				// this will not trigger onchange event
				return false;
			});
			th.append(inputAddRule);
			}

			// button for delete the group
			if (parentgroup !== null) { // ignore the first group
				var inputDeleteGroup = $("<input type='button' value='-' title='Delete group' class='delete-group'/>");
				th.append(inputDeleteGroup);
				inputDeleteGroup.bind('click',function() {
				// remove group from parent
					for (i = 0; i < parentgroup.groups.length; i++) {
						if (parentgroup.groups[i] === group) {
							parentgroup.groups.splice(i, 1);
							break;
						}
					}

					that.reDraw(); // the html has changed, force reDraw

					that.onchange(); // signals that the filter has changed
					return false;
				});
			}

			// append subgroup rows
			if (group.groups !== undefined) {
				for (i = 0; i < group.groups.length; i++) {
					var trHolderForSubgroup = $("<tr></tr>");
					table.append(trHolderForSubgroup);

					var tdFirstHolderForSubgroup = $("<td class='first'></td>");
					trHolderForSubgroup.append(tdFirstHolderForSubgroup);

					var tdMainHolderForSubgroup = $("<td colspan='4'></td>");
					tdMainHolderForSubgroup.append(this.createTableForGroup(group.groups[i], group));
					trHolderForSubgroup.append(tdMainHolderForSubgroup);
				}
			}
			if(group.groupOp === undefined) {
				group.groupOp = that.p.groupOps[0].op;
			}

			// append rules rows
			if (group.rules !== undefined) {
				for (i = 0; i < group.rules.length; i++) {
					table.append(
                       this.createTableRowForRule(group.rules[i], group)
					);
				}
			}

			return table;
		};
		/*
		 * Create the rule data for the filter
		 */
		this.createTableRowForRule = function(rule, group ) {
			// save current entity in a variable so that it could
			// be referenced in anonimous method calls

			var that=this, tr = $("<tr></tr>"),
			//document.createElement("tr"),

			// first column used for padding
			//tdFirstHolderForRule = document.createElement("td"),
			i, op, trpar, cm, str="", selected;
			//tdFirstHolderForRule.setAttribute("class", "first");
			tr.append("<td class='first'></td>");


			// create field container
			var ruleFieldTd = $("<td class='columns'></td>");
			tr.append(ruleFieldTd);


			// dropdown for: choosing field
			var ruleFieldSelect = $("<select></select>"), ina, aoprs = [];
			ruleFieldTd.append(ruleFieldSelect);
			ruleFieldSelect.bind('change',function() {
				rule.field = $(ruleFieldSelect).val();

				trpar = $(this).parents("tr:first");
				for (i=0;i<that.p.columns.length;i++) {
					if(that.p.columns[i].name ===  rule.field) {
						cm = that.p.columns[i];
						break;
					}
				}
				if(!cm) {return;}
				cm.searchoptions.id = $.jgrid.randId();
				if(isIE && cm.inputtype === "text") {
					if(!cm.searchoptions.size) {
						cm.searchoptions.size = 10;
					}
				}
				var elm = $.jgrid.createEl(cm.inputtype,cm.searchoptions, "", true, that.p.ajaxSelectOptions, true);
				$(elm).addClass("input-elm");
				//that.createElement(rule, "");

				if( cm.searchoptions.sopt ) {op = cm.searchoptions.sopt;}
				else if(that.p.sopt) { op= that.p.sopt; }
				else if  (cm.searchtype === 'string') {op = that.p.stropts;}
				else {op = that.p.numopts;}
				// operators
				var s ="", so = 0;
				aoprs = [];
				$.each(that.p.ops, function() { aoprs.push(this.name) });
				for ( i = 0 ; i < op.length; i++) {
					ina = $.inArray(op[i],aoprs);
					if(ina !== -1) {
						if(so===0) {
							rule.op = that.p.ops[ina].name;
						}
						s += "<option value='"+that.p.ops[ina].name+"'>"+that.p.ops[ina].description+"</option>";
						so++;
					}
				}
				$(".selectopts",trpar).empty().append( s );
				$(".selectopts",trpar)[0].selectedIndex = 0;
				if( $.browser.msie && $.browser.version < 9) {
					var sw = parseInt($("select.selectopts",trpar)[0].offsetWidth) + 1;
					$(".selectopts",trpar).width( sw );
					$(".selectopts",trpar).css("width","auto");
				}
				// data
				$(".data",trpar).empty().append( elm );
				$(".input-elm",trpar).bind('change',function( e ) {
					var tmo = $(this).hasClass("ui-autocomplete-input") ? 200 :0;
					setTimeout(function(){
						var elem = e.target;
						rule.data = elem.nodeName.toUpperCase() === "SPAN" && cm.searchoptions && $.isFunction(cm.searchoptions.custom_value) ?
							cm.searchoptions.custom_value($(elem).children(".customelement:first"), 'get') : elem.value;
						that.onchange(); // signals that the filter has changed
					}, tmo);
				});
				setTimeout(function(){ //IE, Opera, Chrome
				rule.data = $(elm).val();
				that.onchange();  // signals that the filter has changed
				}, 0);
			});

			// populate drop down with user provided column definitions
			var j=0;
			for (i = 0; i < that.p.columns.length; i++) {
				// but show only serchable and serchhidden = true fields
		        var searchable = (typeof that.p.columns[i].search === 'undefined') ?  true: that.p.columns[i].search ,
		        hidden = (that.p.columns[i].hidden === true),
				ignoreHiding = (that.p.columns[i].searchoptions.searchhidden === true);
				if ((ignoreHiding && searchable) || (searchable && !hidden)) {
					selected = "";
					if(rule.field === that.p.columns[i].name) {
						selected = " selected='selected'";
						j=i;
					}
					str += "<option value='"+that.p.columns[i].name+"'" +selected+">"+that.p.columns[i].label+"</option>";
				}
			}
			ruleFieldSelect.append( str );


			// create operator container
			var ruleOperatorTd = $("<td class='operators'></td>");
			tr.append(ruleOperatorTd);
			cm = p.columns[j];
			// create it here so it can be referentiated in the onchange event
			//var RD = that.createElement(rule, rule.data);
			cm.searchoptions.id = $.jgrid.randId();
			if(isIE && cm.inputtype === "text") {
				if(!cm.searchoptions.size) {
					cm.searchoptions.size = 10;
				}
			}
			var ruleDataInput = $.jgrid.createEl(cm.inputtype,cm.searchoptions, rule.data, true, that.p.ajaxSelectOptions, true);

			// dropdown for: choosing operator
			var ruleOperatorSelect = $("<select class='selectopts'></select>");
			ruleOperatorTd.append(ruleOperatorSelect);
			ruleOperatorSelect.bind('change',function() {
				rule.op = $(ruleOperatorSelect).val();
				trpar = $(this).parents("tr:first");
				var rd = $(".input-elm",trpar)[0];
				if (rule.op === "nu" || rule.op === "nn") { // disable for operator "is null" and "is not null"
					rule.data = "";
					rd.value = "";
					rd.setAttribute("readonly", "true");
					rd.setAttribute("disabled", "true");
				} else {
					rd.removeAttribute("readonly");
					rd.removeAttribute("disabled");
				}

				that.onchange();  // signals that the filter has changed
			});

			// populate drop down with all available operators
			if( cm.searchoptions.sopt ) {op = cm.searchoptions.sopt;}
			else if(that.p.sopt) { op= that.p.sopt; }
			else if  (cm.searchtype === 'string') {op = p.stropts;}
			else {op = that.p.numopts;}
			str="";
			$.each(that.p.ops, function() { aoprs.push(this.name) });
			for ( i = 0; i < op.length; i++) {
				ina = $.inArray(op[i],aoprs);
				if(ina !== -1) {
					selected = rule.op === that.p.ops[ina].name ? " selected='selected'" : "";
					str += "<option value='"+that.p.ops[ina].name+"'"+selected+">"+that.p.ops[ina].description+"</option>";
				}
			}
			ruleOperatorSelect.append( str );
			// create data container
			var ruleDataTd = $("<td class='data'></td>");
			tr.append(ruleDataTd);

			// textbox for: data
			// is created previously
			//ruleDataInput.setAttribute("type", "text");
			ruleDataTd.append(ruleDataInput);

			$(ruleDataInput)
			.addClass("input-elm")
			.bind('change', function() {
				rule.data = cm.inputtype === 'custom' ? cm.searchoptions.custom_value($(this).children(".customelement:first"),'get') : $(this).val();
				that.onchange(); // signals that the filter has changed
			});

			// create action container
			var ruleDeleteTd = $("<td></td>");
			tr.append(ruleDeleteTd);

			// create button for: delete rule
			if(this.p.ruleButtons === true) {
			var ruleDeleteInput = $("<input type='button' value='删除此条件' title='删除条件' class='delete-rule ui-del'/>");
			ruleDeleteTd.append(ruleDeleteInput);
			//$(ruleDeleteInput).html("").height(20).width(30).button({icons: {  primary: "ui-icon-minus", text:false}});
			ruleDeleteInput.bind('click',function() {
				// remove rule from group
				for (i = 0; i < group.rules.length; i++) {
					if (group.rules[i] === rule) {
						group.rules.splice(i, 1);
						break;
					}
				}

				that.reDraw(); // the html has changed, force reDraw

				that.onchange(); // signals that the filter has changed
				return false;
			});
			}
			return tr;
		};

		this.getStringForGroup = function(group) {
			var s = "(", index;
			if (group.groups !== undefined) {
				for (index = 0; index < group.groups.length; index++) {
					if (s.length > 1) {
						s += " " + group.groupOp + " ";
					}
					try {
						s += this.getStringForGroup(group.groups[index]);
					} catch (eg) {alert(eg);}
				}
			}

			if (group.rules !== undefined) {
				try{
					for (index = 0; index < group.rules.length; index++) {
						if (s.length > 1) {
							s += " " + group.groupOp + " ";
						}
						s += this.getStringForRule(group.rules[index]);
					}
				} catch (e) {alert(e);}
			}

			s += ")";

			if (s === "()") {
				return ""; // ignore groups that don't have rules
			} else {
				return s;
			}
		};
		this.getStringForRule = function(rule) {
			var opUF = "",opC="", i, cm, ret, val,
			numtypes = ['int', 'integer', 'float', 'number', 'currency']; // jqGrid
			for (i = 0; i < this.p.ops.length; i++) {
				if (this.p.ops[i].name === rule.op) {
					opUF = this.p.ops[i].operator;
					opC = this.p.ops[i].name;
					break;
				}
			}
			for (i=0; i<this.p.columns.length; i++) {
				if(this.p.columns[i].name === rule.field) {
					cm = this.p.columns[i];
					break;
				}
			}
			val = rule.data;
			if(opC === 'bw' || opC === 'bn') { val = val+"%"; }
			if(opC === 'ew' || opC === 'en') { val = "%"+val; }
			if(opC === 'cn' || opC === 'nc') { val = "%"+val+"%"; }
			if(opC === 'in' || opC === 'ni') { val = " ("+val+")"; }
			if(p.errorcheck) { checkData(rule.data, cm); }
			if($.inArray(cm.searchtype, numtypes) !== -1 || opC === 'nn' || opC === 'nu') { ret = rule.field + " " + opUF + " " + val; }
			else { ret = rule.field + " " + opUF + " \"" + val + "\""; }
			return ret;
		};
		this.resetFilter = function () {
			this.p.filter = $.extend(true,{},this.p.initFilter);
			this.reDraw();
			this.onchange();
		};
		this.hideError = function() {
			$("th.ui-state-error", this).html("");
			$("tr.error", this).hide();
		};
		this.showError = function() {
			$("th.ui-state-error", this).html(this.p.errmsg);
			$("tr.error", this).show();
		};
		this.toUserFriendlyString = function() {
			return this.getStringForGroup(p.filter);
		};
		this.toString = function() {
			// this will obtain a string that can be used to match an item.
			var that = this;
			function getStringRule(rule) {
				if(that.p.errorcheck) {
					var i, cm;
					for (i=0; i<that.p.columns.length; i++) {
						if(that.p.columns[i].name === rule.field) {
							cm = that.p.columns[i];
							break;
						}
					}
					if(cm) {checkData(rule.data, cm);}
				}
				return rule.op + "(item." + rule.field + ",'" + rule.data + "')";
			}

			function getStringForGroup(group) {
				var s = "(", index;

				if (group.groups !== undefined) {
					for (index = 0; index < group.groups.length; index++) {
						if (s.length > 1) {
							if (group.groupOp === "OR") {
								s += " || ";
							}
							else {
								s += " && ";
							}
						}
						s += getStringForGroup(group.groups[index]);
					}
				}

				if (group.rules !== undefined) {
					for (index = 0; index < group.rules.length; index++) {
						if (s.length > 1) {
							if (group.groupOp === "OR") {
								s += " || ";
							}
							else  {
								s += " && ";
							}
						}
						s += getStringRule(group.rules[index]);
					}
				}

				s += ")";

				if (s === "()") {
					return ""; // ignore groups that don't have rules
				} else {
					return s;
				}
			}

			return getStringForGroup(this.p.filter);
		};

		// Here we init the filter
		this.reDraw();

		if(this.p.showQuery) {
			this.onchange();
		}
		// mark is as created so that it will not be created twice on this element
		this.filter = true;
	});
};
$.extend($.fn.jqFilter,{
	/*
	 * Return SQL like string. Can be used directly
	 */
	toSQLString : function()
	{
		var s ="";
		this.each(function(){
			s = this.toUserFriendlyString();
		});
		return s;
	},
	/*
	 * Return filter data as object.
	 */
	filterData : function()
	{
		var s;
		this.each(function(){
			s = this.p.filter;
            //从和到的特殊处理 xyl 2013-04-26
            var rules = this.p.filter.rules;
            for(k=0;k<rules.length;k++){
                if(rules[k].op == "bd" ){
                    rules[k].op = "ge";
                }else if(rules[k].op == "ed"){
                    rules[k].op = "le";
                }
            }
		});
		return s;

	},
	getParameter : function (param) {
		if(param !== undefined) {
			if (this.p.hasOwnProperty(param) ) {
				return this.p[param];
			}
		}
		return this.p;
	},
	resetFilter: function() {
		return this.each(function(){
			this.resetFilter();
		});
	},
	addFilter: function (pfilter) {
		if (typeof pfilter === "string") {
			pfilter = jQuery.jgrid.parse( pfilter );
	}
		this.each(function(){
			this.p.filter = pfilter;
			this.reDraw();
			this.onchange();
		});
	}

});
})(jQuery);

$.jgrid.extend({
filterToolbar : function(p){
		p = $.extend({
			autosearch: true,
			searchOnEnter : true,
			beforeSearch: null,
			afterSearch: null,
			beforeClear: null,
			afterClear: null,
			searchurl : '',
			stringResult: false,
			groupOp: 'AND',
			defaultSearch : "cn"//默认搜索改为模糊匹配，原值为bw（以..开头）xyl 2013-05-31
		},p  || {});
		return this.each(function(){
			var $t = this;
			if(this.ftoolbar) { return; }
			var triggerToolbar = function() {
				var sdata={}, j=0, v, nm, sopt={},so;
				$.each($t.p.colModel,function(i,n){
					nm = this.index || this.name;
					so  = (this.searchoptions && this.searchoptions.sopt) ? this.searchoptions.sopt[0] : this.stype=='select'?  'eq' : p.defaultSearch;
					v = $("#gs_"+$.jgrid.jqID(this.name), (this.frozen===true && $t.p.frozenColumns === true) ?  $t.grid.fhDiv : $t.grid.hDiv).val();
					if(v) {
						sdata[nm] = v;
						sopt[nm] = so;
						j++;
					} else {
						try {
							delete $t.p.postData[nm];
						} catch (z) {}
					}
				});
				var sd =  j>0 ? true : false;
				if(p.stringResult === true || $t.p.datatype == "local") {
					var ruleGroup = "{\"groupOp\":\"" + p.groupOp + "\",\"rules\":[";
					var gi=0;
					$.each(sdata,function(i,n){
						if (gi > 0) {ruleGroup += ",";}
						ruleGroup += "{\"field\":\"" + i + "\",";
						ruleGroup += "\"op\":\"" + sopt[i] + "\",";
						n+="";
						ruleGroup += "\"data\":\"" + n.replace(/\\/g,'\\\\').replace(/\"/g,'\\"') + "\"}";
						gi++;
					});
					ruleGroup += "]}";
					$.extend($t.p.postData,{filters:ruleGroup});
					$.each(['searchField', 'searchString', 'searchOper'], function(i, n){
						if($t.p.postData.hasOwnProperty(n)) { delete $t.p.postData[n];}
					});
				} else {
					$.extend($t.p.postData,sdata);
				}
				var saveurl;
				if($t.p.searchurl) {
					saveurl = $t.p.url;
					$($t).jqGrid("setGridParam",{url:$t.p.searchurl});
				}
				var bsr = false;
				if($.isFunction(p.beforeSearch)){bsr = p.beforeSearch.call($t);}
				if(!bsr) { $($t).jqGrid("setGridParam",{search:sd}).trigger("reloadGrid",[{page:1}]); }
				if(saveurl) {$($t).jqGrid("setGridParam",{url:saveurl});}
				if($.isFunction(p.afterSearch)){p.afterSearch();}
			};
			var clearToolbar = function(trigger){
				var sdata={}, v, j=0, nm;
				trigger = (typeof trigger != 'boolean') ? true : trigger;
				$.each($t.p.colModel,function(i,n){
					v = (this.searchoptions && this.searchoptions.defaultValue) ? this.searchoptions.defaultValue : "";
					nm = this.index || this.name;
					switch (this.stype) {
						case 'select' :
							var v1;
							$("#gs_"+$.jgrid.jqID(this.name)+" option",(this.frozen===true && $t.p.frozenColumns === true) ?  $t.grid.fhDiv : $t.grid.hDiv).each(function (i){
								if(i===0) { this.selected = true; }
								if ($(this).text() == v) {
									this.selected = true;
									v1 = $(this).val();
									return false;
								}
							});
							if (v1) {
								// post the key and not the text
								sdata[nm] = v1;
								j++;
							} else {
								try {
									delete $t.p.postData[nm];
								} catch(e) {}
							}
							break;
						case 'text':
							$("#gs_"+$.jgrid.jqID(this.name),(this.frozen===true && $t.p.frozenColumns === true) ?  $t.grid.fhDiv : $t.grid.hDiv).val(v);
							if(v) {
								sdata[nm] = v;
								j++;
							} else {
								try {
									delete $t.p.postData[nm];
								} catch (y){}
							}
							break;
					}
				});
				var sd =  j>0 ? true : false;
				if(p.stringResult === true || $t.p.datatype == "local") {
					var ruleGroup = "{\"groupOp\":\"" + p.groupOp + "\",\"rules\":[";
					var gi=0;
					$.each(sdata,function(i,n){
						if (gi > 0) {ruleGroup += ",";}
						ruleGroup += "{\"field\":\"" + i + "\",";
						ruleGroup += "\"op\":\"" + "eq" + "\",";
						n+="";
						ruleGroup += "\"data\":\"" + n.replace(/\\/g,'\\\\').replace(/\"/g,'\\"') + "\"}";
						gi++;
					});
					ruleGroup += "]}";
					$.extend($t.p.postData,{filters:ruleGroup});
					$.each(['searchField', 'searchString', 'searchOper'], function(i, n){
						if($t.p.postData.hasOwnProperty(n)) { delete $t.p.postData[n];}
					});
				} else {
					$.extend($t.p.postData,sdata);
				}
				var saveurl;
				if($t.p.searchurl) {
					saveurl = $t.p.url;
					$($t).jqGrid("setGridParam",{url:$t.p.searchurl});
				}
				var bcv = false;
				if($.isFunction(p.beforeClear)){bcv = p.beforeClear.call($t);}
				if(!bcv) {
					if(trigger) {
						$($t).jqGrid("setGridParam",{search:sd}).trigger("reloadGrid",[{page:1}]);
					}
				}
				if(saveurl) {$($t).jqGrid("setGridParam",{url:saveurl});}
				if($.isFunction(p.afterClear)){p.afterClear();}
			};
			var toggleToolbar = function(){
				var trow = $("tr.ui-search-toolbar",$t.grid.hDiv),
				trow2 = $t.p.frozenColumns === true ?  $("tr.ui-search-toolbar",$t.grid.hDiv) : false;
				if(trow.css("display")=='none') { 
					trow.show(); 
					if(trow2) {
						trow2.show();
					}
				} else { 
					trow.hide(); 
					if(trow2) {
						trow2.hide();
					}
				}
			};
			// create the row
			function bindEvents(selector, events) {
				var jElem = $(selector);
				if (jElem[0]) {
				    jQuery.each(events, function() {
				        if (this.data !== undefined) {
				            jElem.bind(this.type, this.data, this.fn);
				        } else {
				            jElem.bind(this.type, this.fn);
				        }
				    });
				}
			}
			var tr = $("<tr class='ui-search-toolbar' role='rowheader'></tr>");
			var timeoutHnd;
			$.each($t.p.colModel,function(i,n){
				var cm=this, thd , th, soptions,surl,self;
				th = $("<th role='columnheader' class='ui-state-default ui-th-column ui-th-"+$t.p.direction+"'></th>");
				thd = $("<div style='width:100%;position:relative;height:100%;padding-right:0.3em;'></div>");
				if(this.hidden===true) { $(th).css("display","none");}
				this.search = this.search === false ? false : true;
				if(typeof this.stype == 'undefined' ) {this.stype='text';}
				soptions = $.extend({},this.searchoptions || {});
				if(this.search){
					switch (this.stype)
					{
					case "select":
						surl = this.surl || soptions.dataUrl;
						if(surl) {
							// data returned should have already constructed html select
							// primitive jQuery load
							self = thd;
							$.ajax($.extend({
								url: surl,
								dataType: "html",
								success: function(res,status) {
									if(soptions.buildSelect !== undefined) {
										var d = soptions.buildSelect(res);
										if (d) { $(self).append(d); }
									} else {
										$(self).append(res);
									}
									if(soptions.defaultValue) { $("select",self).val(soptions.defaultValue); }
									$("select",self).attr({name:cm.index || cm.name, id: "gs_"+cm.name});
									if(soptions.attr) {$("select",self).attr(soptions.attr);}
									$("select",self).css({width: "100%"});
									// preserve autoserch
									if(soptions.dataInit !== undefined) { soptions.dataInit($("select",self)[0]); }
									if(soptions.dataEvents !== undefined) { bindEvents($("select",self)[0],soptions.dataEvents); }
									if(p.autosearch===true){
										$("select",self).change(function(e){
											triggerToolbar();
											return false;
										});
									}
									res=null;
								}
							}, $.jgrid.ajaxOptions, $t.p.ajaxSelectOptions || {} ));
						} else {
							var oSv;
							if(cm.searchoptions && cm.searchoptions.value) {
								oSv = cm.searchoptions.value;
							} else if(cm.editoptions && cm.editoptions.value) {
								oSv = cm.editoptions.value;
							}
							if (oSv) {	
								var elem = document.createElement("select");
								elem.style.width = "100%";
								$(elem).attr({name:cm.index || cm.name, id: "gs_"+cm.name});
								var so, sv, ov;
								if(typeof oSv === "string") {
									so = oSv.split(";");
									for(var k=0; k<so.length;k++){
										sv = so[k].split(":");
										ov = document.createElement("option");
										ov.value = sv[0]; ov.innerHTML = sv[1];
										elem.appendChild(ov);
									}
								} else if(typeof oSv === "object" ) {
									for ( var key in oSv) {
										if(oSv.hasOwnProperty(key)) {
											ov = document.createElement("option");
											ov.value = key; ov.innerHTML = oSv[key];
											elem.appendChild(ov);
										}
									}
								}
								if(soptions.defaultValue) { $(elem).val(soptions.defaultValue); }
								if(soptions.attr) {$(elem).attr(soptions.attr);}
								if(soptions.dataInit !== undefined) { soptions.dataInit(elem); }
								if(soptions.dataEvents !== undefined) { bindEvents(elem, soptions.dataEvents); }
								$(thd).append(elem);
								if(p.autosearch===true){
									$(elem).change(function(e){
										triggerToolbar();
										return false;
									});
								}
							}
						}
						break;
					case 'text':
						var df = soptions.defaultValue ? soptions.defaultValue: "";
						$(thd).append("<input type='text' style='width:95%;padding:0px;' name='"+(cm.index || cm.name)+"' id='gs_"+cm.name+"' value='"+df+"'/>");
						if(soptions.attr) {$("input",thd).attr(soptions.attr);}
						if(soptions.dataInit !== undefined) { soptions.dataInit($("input",thd)[0]); }
						if(soptions.dataEvents !== undefined) { bindEvents($("input",thd)[0], soptions.dataEvents); }
						if(p.autosearch===true){
							if(p.searchOnEnter) {
								$("input",thd).keypress(function(e){
									var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
									if(key == 13){
										triggerToolbar();
										return false;
									}
									return this;
								});
							} else {
								$("input",thd).keydown(function(e){
									var key = e.which;
									switch (key) {
										case 13:
											return false;
										case 9 :
										case 16:
										case 37:
										case 38:
										case 39:
										case 40:
										case 27:
											break;
										default :
											if(timeoutHnd) { clearTimeout(timeoutHnd); }
											timeoutHnd = setTimeout(function(){triggerToolbar();},500);
									}
								});
							}
						}
						break;
					}
				}
				$(th).append(thd);
				$(tr).append(th);
			});
			$("table thead",$t.grid.hDiv).append(tr);
			this.ftoolbar = true;
			this.triggerToolbar = triggerToolbar;
			this.clearToolbar = clearToolbar;
			this.toggleToolbar = toggleToolbar;
		});
	}
});