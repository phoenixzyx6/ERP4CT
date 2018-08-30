//施工配比
function consMixpropIndexInit(opts) {
    var initArray = [
            { label: '操作', name: 'act', index: 'act', width: 30, sortable: false, align: "center", search: false, frozen: true }
        , { label: '<font color=red>修改状态？</font>', name: 'SynStatus', index: 'SynStatus', width: 90, align: 'center', hidden: true }
        , { label: ' 编号', name: 'ID', index: 'ID', width: 63, align: "center", sortable: false, frozen: true }
        , { label: ' 类别', name: 'IsSlurry', index: 'IsSlurry', sortable: false, formatter: booleanFmt, unformat: booleanUnFmt, width: 45, formatterStyle: { "0": "混凝土", "1": "砂浆"} }
        , { label: ' 任务单号', name: 'TaskID', index: 'TaskID', hidden: true, sortable: false }
        , { label: ' 客户名称', name: 'CustName', index: 'CustName', width: 80, sortable: false, search: false, hidden: true }
        , { label: ' 工程名称', name: 'ProjectName', index: 'ProduceTask.ProjectName', width: 80, sortable: false }
        , { label: ' 施工部位', name: 'ConsPos', index: 'ProduceTask.ConsPos', width: 80, sortable: false }
        , { label: ' 机组代号', name: 'ProductLineID', index: 'ProductLineID', width: 50, sortable: false }
        , { label: ' 砼强度', name: 'ConStrength', index: 'ConStrength', width: 60, align: 'center', stype: 'select', searchoptions: { dataUrl: opts.conStrengthSelectUrl }, sortable: false }
        , { label: '<span id="hidesomecol" class="ui-icon ui-icon-circlesmall-plus" style="display: block;"></span>', name: 'hidesomecol', index: 'hidesomecol', width: 16, align: 'center', sortable: false }
        , { label: ' 审核人', name: 'Auditor', index: 'Auditor', sortable: false, width: 60, search: false, hidden: true }
        , { label: ' 审核时间', name: 'AuditTime', index: 'AuditTime', sortable: false, width: 120, search: false, formatter: 'datetime', hidden: true }
        , { label: ' 审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 60, sortable: false, hidden: true }
        , { label: ' 审核意见', name: 'AuditInfo', index: 'AuditInfo', width: 150, sortable: false, search: false, hidden: true }
        , { label: ' 理论配比', name: 'FormulaName', index: 'FormulaName', width: 80, sortable: false, hidden: true }
        , { label: ' 搅拌时间', name: 'MixingTime', index: 'MixingTime', width: 60, editable: true, editrules: { maxValue: 50, required: true, number: true }, sortable: false, search: false }
        , { label: ' 水灰比', name: 'WCRate', index: 'WCRate', width: 50, sortable: false, align: 'right' }
        , { label: ' 砂率', name: 'SCRate', index: 'SCRate', width: 50, sortable: false, align: 'right' }
        //, { label: ' 理论容重', name: 'TuneWeight', index: 'Formula.TuneWeight', width: 60, align: 'right', formatter: rzFmt, unformat: rzUnFmt }
        , { label: ' 初始容重', name: 'Weight', index: 'Weight', width: 60, sortable: false, align: 'right', formatter: rzFmt, unformat: rzUnFmt }
        , { label: ' 调整容重', name: 'TuneWeight', index: 'TuneWeight', jsonmap: 'Weight', width: 60, sortable: false, align: 'right', formatter: rzFmt, unformat: rzUnFmt }
        , { label: ' 容重差', name: 'RzDif', index: 'RzDif', width: 40, sortable: false, align: 'center' }
    ];
    var newDirtyJson = {};
    var beforeEditValue = 0;
    var iLastRow = 0;
    var iLastCol = 0;
    var beginDate;
    var endDate;
    var faildConsMixID = {};
    var iknown = false;
    var options = {
            //caption: '施工配比'
            autowidth: true
            , height: gGridWithTitleHeight
		    , url: opts.storeUrl
            , datatype: 'json'
            , mtype: 'POST'
            , postData: { condition: " ProduceTask.IsCompleted=0 and AuditStatus = 1 and ProductLineID = '001' " }
            , jsonReader: {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                id: 'ID'
            }
		    , sortable: false
            , sortname: 'ID'
            , sortorder: 'desc'
            , altRows: true
            , altclass: 'ui-priority-secondary'
		    , rowNum: 30
            , rowList: [10, 20, 30]
            , autoLoad: true
            , pager: '#ConsGridDivPager'
            , pagerpos: 'center'
            , recordpos: 'right'
            , multiselect: false
            , hidegrid: false
           //, groupingView: { groupField: ['ProductLineID'], groupColumnShow: [false], groupDataSorted: [true], groupOrder: ['ASC'] }
		    , cellEdit: true
            , cellurl: '/ConsMixprop.mvc/Update'
            , cellsubmit: 'clientArray'
            , viewrecords: true
            , forceFit: false
            , shrinkToFit: false
            , colModel: initArray
            , gridComplete: function () {
                var ids = jQuery("#ConsGridDiv").jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteConsMixprop('" + ids[i] + "');\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                    jQuery("#ConsGridDiv").jqGrid('setRowData', ids[i], { act: ce });
                }
                if (faildConsMixID) {
                    $.each(faildConsMixID, function (i, n) {
                        $("#" + $.trim(n)).css("background", "#F49687");
                    });
                }
            }
            , beforeEditCell: function (rowid, cellname, value, iRow, iCol) {
                //alert("第" + iRow + "行\n" + "编辑前的值：" + value + "\n第" + iCol + "列\n" + "列名：" + cellname + "\n行编号：" + rowid);
                if (!jQuery.isEmptyObject(faildConsMixID)) {
                    if (!faildConsMixID[rowid] && !iknown) {//若没有继续修改失败的记录，则提示
                        $('<div title="提示"><p>您还有未修改成功的配比！</p></div>').dialog({
                            resizable: false,
                            show: "fade",
                            hide: "fade",
                            modal: true,
                            buttons: {
                                "我知道了": function (btn) {
                                    $(btn.currentTarget).button({ disabled: true });
                                    iknown = true;
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                }
                beforeEditValue = value;
                iLastRow = iRow;
                iLastCol = iCol;
            }
            , beforeSaveCell: function (rowid, cellname, value, iRow, iCol) {
                var record = jQuery("#ConsGridDiv").jqGrid('getRowData', rowid);
                //材料修改差量
                var clValue = value - beforeEditValue;
                var preWeight = jQuery("#ConsGridDiv").jqGrid().getCell(rowid, 'Weight');
                var preTuneWeight = jQuery("#ConsGridDiv").jqGrid().getCell(rowid, 'TuneWeight');
                var finalWeight = parseFloat(preTuneWeight) + parseFloat(clValue);
                $("td:eq(" + iCol + ")", this.rows[iRow]).addClass("dirty-cell");
                $("td:eq(" + iCol + ")", this.rows[iRow]).removeClass("ui-state-highlight");
                var msg = "";
                if (record.IsSlurry == "true" || record.IsSlurry == true || record.IsSlurry == 1) { //砂浆
                    if (finalWeight > gSysConfig.SlurryRZMax * 1 || finalWeight < gSysConfig.SlurryRZMin * 1) {
                        msg = "调整后的容重将变为<font color=red>" + finalWeight + "</font>，超出了砂浆的容重范围（" + gSysConfig.SlurryRZMin * 1 + " - " + gSysConfig.SlurryRZMax * 1 + "）";
                    }
                } else { //混凝土
                    if (finalWeight > gSysConfig.FormulaRZMax * 1 || finalWeight < gSysConfig.FormulaRZMin * 1) {
                        msg = "调整后的容重将变为<font color=red>" + finalWeight + "</font>，超出了混凝土的容重范围（" + gSysConfig.FormulaRZMin * 1 + " - " + gSysConfig.FormulaRZMax * 1 + "）";
                    }
                }

                if (!isEmpty(msg)) { //如果有警告就show出来
                    $.jgrid.info_dialog(
                    "警告",
                    msg,
                    $.jgrid.edit.bClose, {
                        left: $("td:eq(" + iCol + ")", this.rows[iRow]).offset().left,
                        top: $("td:eq(" + iCol + ")", this.rows[iRow]).offset().top + $("td:eq(" + iCol + ")", this.rows[iRow]).height(),
                        modal: true
                    });
                    //jQuery("#ConsGridDiv").jqGrid("editCell", iRow, iCol, true);

                }
                if (preWeight != finalWeight) {
                    $("td[aria-describedby=ConsGridDiv_TuneWeight]", this.rows[iRow]).addClass("changed-cell");
                } else {
                    $("td[aria-describedby=ConsGridDiv_TuneWeight]", this.rows[iRow]).removeClass("changed-cell");
                }
                jQuery("#ConsGridDiv").jqGrid().setCell(rowid, 'TuneWeight', finalWeight);
                jQuery("#ConsGridDiv").jqGrid().setCell(rowid, 'RzDif', finalWeight - preWeight);
            }
            , afterSaveCell: function (rowid, cellname, value, iRow, iCol) {

                if (newDirtyJson[rowid]) {
                    newDirtyJson[rowid].push(cellname);
                } else {
                    var newDirtyArray = [];
                    newDirtyArray.push(cellname);
                    newDirtyJson[rowid] = newDirtyArray;
                }
                jQuery("#ConsGridDiv").jqGrid().setCell(rowid, 'SynStatus', 0);
                $("td[aria-describedby=ConsGridDiv_SynStatus]", this.rows[iRow]).addClass("dirty-cell");
            }

        };

   

    var functions = {
        handleReload: function (btn) {
            jQuery("#ConsGridDiv").jqGrid().trigger("reloadGrid");
        },
        handleRefresh: function (btn) {
            jQuery("#ConsGridDiv").jqGrid('setGridParam', { page: 1 }).trigger("reloadGrid");
        }
        , handleViewHis: function (btn) {
            var isSelectedOnlyOne = false;
            if (options.multiselect) {
                var selectedId = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selarrrow');
                if (selectedId.length == 1) {
                    isSelectedOnlyOne = true;
                }
            } else {
                if (!isEmpty(jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow'))) {
                    isSelectedOnlyOne = true;
                }
            }
            if (!isSelectedOnlyOne) {
                showMessage('提示', '请选择一条记录进行操作！');
                return;
            }
            var id = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow');
            var selectedRecord = jQuery("#ConsGridDiv").jqGrid('getRowData', id);
            showHis(btn, selectedRecord.ID, "M");
        }
        //施工配比审核功能函数
        , handleAudit: function (btn) {
            var isSelectedOnlyOne = false;
            if (options.multiselect) {
                var selectedId = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selarrrow');
                if (selectedId.length == 1) {
                    isSelectedOnlyOne = true;
                }
            } else {
                if (!isEmpty(jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow'))) {
                    isSelectedOnlyOne = true;
                }
            }
            if (!isSelectedOnlyOne) {
                showMessage('提示', '请选择一条记录进行操作！');
                return;
            }
            var id = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow');
            var record = jQuery("#ConsGridDiv").jqGrid('getRowData', id);
            var auditValue = record.AuditStatus;
            if (auditValue == 1) {
                showMessage('提示', '该配比已审核！');
                return;
            }
            var ConsGrid = new MyGrid();
            ConsGrid.showWindow({
                btn: btn,
                title: '配比审核',
                width: 350,
                height: 250,
                loadFrom: 'PBAuditForm',
                afterLoaded: function () {
                    $("[name='ConsMixprop.ID']", "#PBAuditForm").val(id);
                    $("[name='ConsMixprop.AuditStatus']", "#PBAuditForm").val(1);
                    $("[name='ConsMixprop.FormulaName']", "#PBAuditForm").val(record.FormulaName);
                    $("[name='ConsMixprop.MixingTime']", "#PBAuditForm").val(record.MixingTime);
                    $('#PBAuditForm form').attr("id", "PBAuditForm");
                    $('#PBAuditForm form').attr("action", btn.data.Url);
                },
                buttons: {
                    "关闭": function () {
                        $(this).dialog('close');
                    }, "保存": function () {
                        var reloadGrid = true;
                        if (this.reloadGrid != undefined && this.reloadGrid == false) {
                            reloadGrid = false;
                        }
                        //执行ajax请求
                        var postData = $("#PBAuditForm form").serialize();
                        var postUrl = btn.data.Url;
                        //附加额外的参数
                        ajaxRequest(postUrl, postData, true, function (response) {
                            if (reloadGrid && response.Result) {
                                jQuery("#ConsGridDiv").jqGrid('setGridParam', { url: opts.storeUrl, datatype: "json" }).trigger("reloadGrid");
                            }
                            var closeDialog = true;

                            if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                closeDialog = false;
                            }

                            //窗口关闭处理                        
                            if (response.Result && closeDialog) {
                                $("#PBAuditForm").dialog('close');
                                $("#PBAuditForm form")[0].reset();
                            }
                        });
                    }
                }
            });
        }
        //施工配比取消审核功能函数
        , handleUnAuditCons: function (btn) {
            var isSelectedOnlyOne = false;
            if (options.multiselect) {
                var selectedId = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selarrrow');
                if (selectedId.length == 1) {
                    isSelectedOnlyOne = true;
                }
            } else {
                if (!isEmpty(jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow'))) {
                    isSelectedOnlyOne = true;
                }
            }
            if (!isSelectedOnlyOne) {
                showMessage('提示', '请选择一条记录进行操作！');
                return;
            }
            var id = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow');
            var record = jQuery("#ConsGridDiv").jqGrid('getRowData', id);
            var auditValue = record.AuditStatus;
            if (auditValue == 0) {
                showMessage('提示', '该施工配比正处于未审核状态！');
                return;
            } else {
                //确认操作
                showConfirm("确认信息", "您确定要<font color=green><b>取消审核</b></font>吗？", function () {
                    ajaxRequest(
                        btn.data.Url,
                        {
                            consMixID: record.ID,
                            auditStatus: 0
                        },
                        true,
                        function () {
                            jQuery("#ConsGridDiv").jqGrid('setGridParam', { page: 1 }).trigger("reloadGrid");
                        }
                    );
                    $(this).dialog("close");
                });
            }
        }
        //配合比任务通知单报表
        , handleReport: function (btn) {
            var isSelectedOnlyOne = false;
            if (options.multiselect) {
                var selectedId = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selarrrow');
                if (selectedId.length == 1) {
                    isSelectedOnlyOne = true;
                }
            } else {
                if (!isEmpty(jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow'))) {
                    isSelectedOnlyOne = true;
                }
            }
            if (!isSelectedOnlyOne) {
                showMessage('提示', '请选择一条记录进行操作！');
                return;
            }
            var id = jQuery("#ConsGridDiv").jqGrid('getGridParam', 'selrow');
            var record = jQuery("#ConsGridDiv").jqGrid('getRowData', id);
            window.open("/Reports/QCC/R710844.aspx?uid=" + record.ID);
        }
        //查看已审核配比
        , handleViewAudited: function (btn) {
            var pl = $("div#SearchDiv select");
            var plValue = '';
            if (pl.val()) {
                plValue = "ProductLineID='" + pl.val() + "' AND ";
            } else {
                showMessage('提示', '请选择生产线！');
                return;
            }

            var data = { url: opts.storeUrl, datatype: "json", page: 1, postData: { condition: plValue + "AuditStatus = 1"} };
            jQuery("#ConsGridDiv").jqGrid('setGridParam', data).trigger("reloadGrid");
        }
        //查看未审核配比
        , handleViewUnAudit: function (btn) {
            var pl = $("div#SearchDiv select");
            var plValue = '';
            if (pl.val()) {
                plValue = "ProductLineID='" + pl.val() + "' AND ";
            } else {
                showMessage('提示', '请选择生产线！');
                return;
            }
            var data = { url: opts.storeUrl, datatype: "json", page: 1, postData: { condition: plValue + "AuditStatus != 1"} };
            jQuery("#ConsGridDiv").jqGrid('setGridParam', data).trigger("reloadGrid");
        }
        , handleSaveAll: function (btn) {
            //特殊处理：将正在编辑的单元格变成非编辑状态，editCell模式下有且只有一个单元格会处理编辑状态，除非人为操作
            //jQuery("#ConsGridDiv").jqGrid("saveCell", iLastRow, iLastCol);
            /*以下为jqgrid自带的方式，优点是不用自己手动获取脏数据行，但缺点是不能过滤没有修改的数据*/
            var dirtyDatas = jQuery("#ConsGridDiv").jqGrid().getChangedCells("dirty");
            //var _newDirtyJson = newDirtyJson;
            if (dirtyDatas.length > 0 && !$.isEmptyObject(newDirtyJson)) {
                /*取脏数据时，如果取的是全部，但需要过滤某些字段时请用如下编码，如只取配比编号、线号、搅拌时间及用量信息   
                var newdirtyDatas = new Array();
                $.each(_newDirtyJson, function (x, y) {
                //得到哪一条记录被修改过 x.toString()
                $.each(dirtyDatas, function (i, n) {
                var l = {};
                if (n.ID == x.toString()) {
                var includeFields = $.merge(["ID", "ProductLineID","MixingTime"], y);
                $.each(n, function (k, m) {
                if ((jQuery.inArray(k.toString(), includeFields) != -1)) {
                l[k] = m;
                }
                });
                newdirtyDatas.push(l);
                return false;
                }
                });
                });
                var postDirtyJson = JSON.stringify(newdirtyDatas);
                */
                var postDirtyJson = JSON.stringify(dirtyDatas);
                ajaxRequest(btn.data.Url, { DirtyDatas: postDirtyJson }, false, function (resp) {
                    if (resp.Result) {
                        showMessage('提示', '修改成功！');
                        faildConsMixID = {};
                        jQuery("#ConsGridDiv").jqGrid().trigger("reloadGrid");
                    } else {
                        showError('错误', resp.Message);
                        jQuery("#ConsGridDiv").jqGrid().trigger("reloadGrid");
                        faildConsMixID = resp.Data;
                        iknown = false;
                    }
                });
            } else {
                showMessage('提示', '您没有修改配比！');
                return;
            }

        }
        , QuickModifyRZ: function (btn) {
            var ConsGrid = new MyGrid();
            ConsGrid.showWindow({
                btn: btn,
                title: '快速修改配比容重范围',
                width: 350,
                height: 250,
                resizable: false,
                loadFrom: 'QuickModifyRZForm',
                afterLoaded: function () {
                    $('#QuickModifyRZForm form').attr("id", "QuickModifyRZ");
                    $('#QuickModifyRZForm form').attr("action", btn.data.Url);
                    lockAndUnlock();
                    $('#QuickModifyRZForm input').css("background-color", "#CCC").attr("readonly", true);
                    $('.ui-icon-unlocked').removeClass('ui-icon-unlocked');
                    $("#CurrentFormulaRZMin").text(gSysConfig.FormulaRZMin + "（当前值）");
                    $("#CurrentFormulaRZMax").text(gSysConfig.FormulaRZMax + "（当前值）");
                    $("#CurrentSlurryRZMin").text(gSysConfig.SlurryRZMin + "（当前值）");
                    $("#CurrentSlurryRZMax").text(gSysConfig.SlurryRZMax + "（当前值）");
                },
                buttons: {
                    "关闭": function () {
                        $(this).dialog('close');
                    }, "保存": function () {
                        //执行ajax请求
                        var postData = $("#QuickModifyRZForm form").serializeArray();
                        var newPostData = new Array();
                        $.each(postData, function (i, n) {
                            if (!isEmpty(n.value)) {
                                var temp = {};
                                temp[n.name] = n.value;
                                newPostData.push(temp);
                            }

                        });
                        var postRZJson = JSON.stringify(newPostData);
                        var postUrl = btn.data.Url;
                        //附加额外的参数
                        ajaxRequest(postUrl, { rzRange: postRZJson }, true, function (response) {
                            var closeDialog = true;

                            if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                closeDialog = false;
                            }

                            //窗口关闭处理                        
                            if (response.Result && closeDialog) {
                                $("#QuickModifyRZForm").dialog('close');
                                $("#QuickModifyRZForm form")[0].reset();
                                if ($.type(postData) == "array") {
                                    $.each(postData, function (i, n) {
                                        if (!isEmpty(n.value)) {
                                            gSysConfig[n.name] = n.value;
                                        }
                                    })
                                }
                                jQuery("#ConsGridDiv").jqGrid().trigger("reloadGrid");
                            }
                        });
                    }
                }
            });
        }
        , handleChangeSilo: function (btn) {
            var ConsGrid = new MyGrid();
            ConsGrid.handleAdd({
                btn: btn,
                width: 400,
                height: 200,
                loadFrom: 'ChangeSiloDiv',
                formId: 'ChangeSiloDivForm',
                afterFormLoaded: function () {
                    $("span#fromto").hide();
                    $("#From").datetimepicker({ showSecond: false, timeFormat: 'hh:mm' });
                    $("#To").datetimepicker({ showSecond: false, timeFormat: 'hh:mm' });
                    $("#ChangeSiloDivForm")[0].reset();
                    $("#S_SiloID").empty();
                    $("#D_SiloID").empty();
                },
                postCallBack: function (response) {
                    if (response.Result) {
                    }
                    else {
                        return;
                    }
                },
                buttons: {
                    "关闭": function () {
                        $(this).dialog('close');
                    }, "更换": function (b) {
                        $(b.currentTarget).button({ disabled: true });
                        //beforeSubmit事件
                        if (!beforeSubmit()) {
                            $(b.currentTarget).button({ disabled: false });
                            return;
                        }
                        var posturl = btn.data.Url;
                        var postdata = {
                            ProductLineID: pl2.val(),
                            S_SiloID: $("#S_SiloID").val(),
                            D_SiloID: $("#D_SiloID").val(),
                            BeginDate: beginDate,
                            EndDate: endDate
                        };
                        ajaxRequest(posturl, postdata, true, function (response) {
                            //窗口关闭处理                        
                            if (response.Result) {
                                $("#ChangeSiloDiv").dialog('close');
                                $("#ChangeSiloDiv form")[0].reset();
                                jQuery("#ConsGridDiv").jqGrid().trigger("reloadGrid");
                            } else {
                                $(b.currentTarget).button({ disabled: false });
                                return;
                            }
                        });
                    }
                }
            });
        }

    };
    var buttonRenderDiv = "rendertoButton";
    var toobarClsName = "rendertoButton-toolbar";
    getDynamicColAndCreateGrid("001");
    function CreateJqGrid(options) {
        jQuery("#ConsGridDiv").jqGrid("GridUnload");
        jQuery("#ConsGridDiv").jqGrid(options);
        jQuery("#ConsGridDiv").jqGrid('navGrid', '#ConsGridDivPager', { add: false, del: false, edit: false,search:false });
        jQuery("#ConsGridDiv").jqGrid('navButtonAdd', '#ConsGridDivPager', { caption: "高级查询", buttonicon: 'ui-icon ui-icon-search', onClickButton: function () {
            jQuery("#ConsGridDiv").jqGrid('searchGrid', { multipleSearch: true, showQuery: true });
        }
        });


        jQuery("#ConsGridDiv").jqGrid('setGridWidth', $("#container").width(), false);
        createButton();
        $("table[aria-labelledby=gbox_ConsGridDiv]:not('#ConsGridDiv')").children().children("tr[role=rowheader]").children().children("div").css({ "overflow": "visible", "vertical-align": "middle", "height": "auto", "top": "0px" });
        jQuery("#ConsGridDiv").jqGrid('setGroupHeaders', { useColSpanStyle: true, groupHeaders: [{ startColumnName: 'act', numberOfColumns: 9, titleText: '任务单基本信息'}] });
    }

    var pl = $("div#SearchDiv select");
    pl.bind('change', function (event) {
        if (pl.val()) {
            getDynamicColAndCreateGrid(pl.val());
        }
    });
    var pl2 = $("#ProductLineID2");
    pl2.bind('change', function (event) {
        if (pl2.val()) {
            bindSelectData($("#S_SiloID"),
                            opts.listSiloUrl,
                            { textField: 'SiloName',
                                valueField: 'SiloID',
                                condition: "ProductLineID='" + pl2.val() + "'"
                            }, null
                        );
            bindSelectData($("#D_SiloID"),
                            opts.listSiloUrl,
                            { textField: 'SiloName',
                                valueField: 'SiloID',
                                condition: "ProductLineID='" + pl2.val() + "'"
                            }, null
                        );
        } else {
            $("#S_SiloID").empty();
            $("#D_SiloID").empty();
        }
    });
    var selectDR = $(":radio[name='SelectDuringDate']");
    selectDR.unbind("click");
    selectDR.bind("click", function () {
        $(this).blur();
        if ($(this).val() == "0") {
            $("span#fromto").hide();
            $("#ChangeSiloDiv").dialog("option", "width", 400);
        } else {
            $("span#fromto").show();
            $("#ChangeSiloDiv").dialog("option", "width", 550);
        }
    });
    $("#emsg").dialog({
        title: '提示',
        autoOpen: false,
        minHeight: 30,
        height: 'auto',
        width: 'auto',
        resizable: false, //不允许改变窗口大小
        //draggable: false, //不允许移动
        modal: true, //模态化窗口
        open: function () {
            //$(this)[0].previousElementSibli/ng.remove();//去掉标题
        }
    });
    //根据生产线获取动态列并组合生成GRID
    function getDynamicColAndCreateGrid(pid, plValue) {
        ajaxRequest(
                "/ConsMixprop.mvc/GetDynamicCol",
                { ProductLineID: pid },
                false,
                function (response) {
                    var dynamicCol = new Array();
                    $.each(response.ColumnData, function (i, n) {
                        var cols = {
                            label: n.label,
                            name: n.name,
                            index: n.index,
                            width: isEmpty(n.width) ? 80 : n.width,
                            align: n.align,
                            editrules: { number: true },
                            editable: n.editable,
                            sortable: n.sortable
                        };
                        dynamicCol.push(cols);
                    });
                    var initArrayClone = initArray.slice(0);
                    initArrayCombin = $.merge(initArray, dynamicCol);
                    initArray = initArrayClone;
                    var plValue = '';
                    if (pid) {
                        plValue = " ProduceTask.IsCompleted=0 and ProductLineID='" + pid + "' AND ";
                    }
                    options.colModel = initArrayCombin;
                    options.postData = { condition: plValue + "AuditStatus = 1" };
                    CreateJqGrid(options);
                    //jQuery("#ConsGridDiv").jqGrid('navGrid', '#ConsGridDivPager', { edit: false, add: false, del: false });
                    hideOrShowCol();
                }
            );
    }

    function hideOrShowCol() {
        var hideOrShowCol = ['FormulaName', 'Auditor', 'AuditTime'];
        $("#hidesomecol").parent("div").unbind("click");
        $("#hidesomecol").parent("div").toggle(
            function () {
                jQuery("#ConsGridDiv").jqGrid().showCol(hideOrShowCol);
                $(this).children("span").toggleClass('ui-icon-circlesmall-minus');
            },
            function () {
                jQuery("#ConsGridDiv").jqGrid().hideCol(hideOrShowCol);
                $(this).children("span").toggleClass('ui-icon-circlesmall-minus');
            }
        );
    }
    hideOrShowCol();
    //系统按钮处理
    function createButton() {
        if (!isEmpty(buttons0)) {
            $("#" + buttonRenderDiv).empty();
            var length = buttons0.length;
            for (var i = 0; i < length; i++) {
                var funcId = buttons0[i].ID;
                var funcName = buttons0[i].FuncName;
                var funcDesc = buttons0[i].FuncDesc;
                var iconCls = buttons0[i].Icon;
                var handlerName = buttons0[i].HandlerName;
                //底部工具栏与顶部工具栏处理
                if (buttons0[i].ButtonPos == 1 && !isEmpty("#ConsGridDivPager")) {

                    jQuery("#ConsGridDiv").jqGrid('navButtonAdd', "#ConsGridDivPager", { caption: funcName, buttonicon: iconCls, onClickButton: functions[handlerName] });
                } else {
                    var button = $('<button type="button" title="' + funcDesc + '" value="' + funcId + '">' + funcName + '</button>');
                    $(button).button({
                        icons: {
                            primary: iconCls
                        },
                        text: funcName

                    });
                    if (!isEmpty(buttonRenderDiv)) {
                        $("#" + buttonRenderDiv).append(button);
                    } else {
                        $("#ConsGridDiv" + " ." + toobarClsName).append(button);
                    }
                    button.bind('click', buttons0[i], functions[handlerName]);
                }
            }
        }
    }

    window.handleDeleteConsMixprop = function (id) {
        showConfirm("确认信息", "您确定删除此施工配比？", function () {
            $.post(
                        "/ConsMixprop.mvc/Delete"
                        , { ID: id }
                        , function (data) {
                            if (!data.Result) {
                                showError("出错啦！", data.Message);
                                return false;
                            }
                            jQuery("#ConsGridDiv").jqGrid().trigger("reloadGrid");
                        }

                    );
            $(this).dialog("close");
        });
    };
    function beforeSubmit() {
        //检查各项表单
        var radioVal = $("input[name='SelectDuringDate'][type='radio']:checked").val();
        if (radioVal == 0) { //选择当天交班时间
            beginDate = new Date().format("Y-m-d") + " " + gSysConfig["ChangeTime"];
            endDate = GetDateBySpan(-1) + " " + gSysConfig["ChangeTime"];
        } else if (radioVal == 1) { //自定义时间
            beginDate = $("#From").val();
            endDate = $("#To").val();

        } else { //非法操作

        }
        if (isEmpty(beginDate) || isEmpty(endDate)) {
            $("#emsg").html("请设置好时间段").css("color", "red");
            $("#emsg").dialog("open");
            return false;
        } else {
            if (compareTime(beginDate, endDate)) {
                $("#emsg").html("开始时间不能大于结束时间").css("color", "red");
                $("#emsg").dialog("open");
                return false;
            }
        }
        if (isEmpty($("#ProductLineID2").val())) {
            $("#emsg").html("请选择生产线").css("color", "red");
            $("#emsg").dialog("open");
            return false;
        }
        if (isEmpty($("#S_SiloID").val()) || isEmpty($("#D_SiloID").val())) {
            $("#emsg").html("请设置好需要切换的桶仓").css("color", "red");
            $("#emsg").dialog("open");
            return false;
        }
        return true;
    }
    //配比子表修改历史
    var ItemsHisGrid = new MyGrid({
        renderTo: 'itemHisGrid'
            , width: 600
        //, autoWidth: true
        //, buttons: buttons0
            , height: 300
		    , storeURL: opts.historyUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , singleSelect: true
		    , showPageBar: true
            , toolbarSearch: false
            , emptyrecords: '当前无任何修改'
            , subGrid: true
            , subGridRowExpanded: function (subgrid_id, row_id) {
                var record = ItemsHisGrid.getRecordByKeyValue(row_id);
                var consid = record.ConsMixpropID;
                var siloid = record.SiloID;
                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                subgrid_pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + subgrid_pager_id + "'></div>");
                $("#" + subgrid_table_id).jqGrid({
                    url: opts.historyUrl,
                    datatype: 'json',
                    mtype: 'post',
                    colModel: [
                { label: '序号', name: 'ID', index: 'ID', width: 49, sortable: false, hidden: true }
                , { label: '操作', name: 'Act', index: 'Act', align: 'center', width: 59, sortable: false, classes: 'red' }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', width: 80, sortable: false, hidden: true }
                , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', width: 60, sortable: false, hidden: true }
                , { label: '筒仓名称', name: 'SiloName', index: 'SiloName', width: 80 }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 100 }
                , { label: '用量', name: 'Amount', index: 'Amount', width: 60, sortable: false }
                , { label: '执行时间', name: 'ExecTime', index: 'ExecTime', align: 'center', formatter: 'datetime', width: 130, sortable: false, classes: 'red' }
                , { label: '执行人', name: 'ExecMan', index: 'ExecMan', width: 60, sortable: false, classes: 'red' }
		    ],
                    rowNum: 5,
                    height: '100%',
                    viewrecords: true,
                    sortable: true,
                    sortname: 'ID',
                    sortorder: 'desc',
                    pager: subgrid_pager_id,
                    postData: { condition: "ConsMixpropID='" + consid + "' AND SiloID='" + siloid + "'  AND Act='修改'" },
                    jsonReader: {
                        root: "rows",
                        page: "page",
                        total: "total",
                        records: "records",
                        repeatitems: false,
                        id: "ID"
                    }

                });

            }
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false, hidden: true }
                , { label: '操作', name: 'Act', index: 'Act', align: 'center', width: 60, sortable: false, classes: 'green' }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', width: 80, sortable: false, hidden: true }
                , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', width: 60, sortable: false, hidden: true }
                , { label: '筒仓名称', name: 'SiloName', index: 'SiloName', width: 80 }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 100 }
                , { label: '用量', name: 'Amount', index: 'Amount', width: 60, sortable: false }
                , { label: '执行时间', name: 'ExecTime', index: 'ExecTime', align: 'center', formatter: 'datetime', width: 130, sortable: false, classes: 'green' }
                , { label: '执行人', name: 'ExecMan', index: 'ExecMan', width: 60, sortable: false, classes: 'green' }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    ItemsHisGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ItemsHisGrid.refreshGrid('1=1');
                }
            }
    });

    //查看单条配比的历史记录
    function showHis(b, id, t) {
        var title = null;
        var refreshCon = null;
        if (t == "M") {
            title = "配比号为：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的历史记录";
            refreshCon = "ConsMixpropID='" + id + "' and Act ='原始'";
            ItemsHisGrid.getJqGrid().jqGrid('hideCol', 'ConsMixpropID');
            //ItemsHisGrid.getJqGrid().jqGrid('hideCol', 'subgrid');
        } else {
            title = "已删除的施工配比列表如下";
            refreshCon = "Act = '删除'";
            ItemsHisGrid.getJqGrid().jqGrid('showCol', 'ConsMixpropID');
            ItemsHisGrid.getJqGrid().jqGrid('showCol', 'subgrid');



        }
        ItemsHisGrid.refreshGrid(refreshCon);
        $("#itemHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 640, height: 400, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                //ItemsHisGrid.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
            }
        });
        ItemsHisGrid.addListeners("gridComplete", function () {
            if (ItemsHisGrid.getAllRecords().length < 1) {
                showMessage('提示', '该配比没有任何历史修改数据！');
                return false;
            } else {
                $('#itemHisWindow').dialog('open');
            }
        });

    }
    function lockAndUnlock() {
        $(".ui-icon-locked").unbind("click");
        $.each($(".ui-icon-locked"), function (i, n) {
            $(n).toggle(
            function () {
                $(this).toggleClass('ui-icon-unlocked');
                $(this).attr("title", "点击锁定");
                $(this).siblings("input").css("background-color", "#FFF").attr("readonly", false);

            },
            function () {
                $(this).toggleClass('ui-icon-unlocked');
                $(this).attr("title", "点击解锁");
                $(this).siblings("input").css("background-color", "#CCC").attr("readonly", true);
            }
        );
        });
    }
    lockAndUnlock();

}