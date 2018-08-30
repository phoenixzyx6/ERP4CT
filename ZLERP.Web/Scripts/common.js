
function isEmpty(value) {
    if (typeof (value) == 'boolean') {
        return false;
    }
    if (typeof (value) == 'undefined' || value == '' || value == '0' || value == null || value == 0) {
        return true;
    } else {
        return false;
    }
}

function isEmpty1(value) {
    if (typeof (value) == 'boolean') {
        return false;
    }
    if (typeof (value) == 'undefined' || value == ''  || value == null ) {
        return true;
    } else {
        return false;
    }
}

// 对Date的扩展，将 Date 转化为指定格式的String 
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
} 




function IsLicit(str) {
    return RegExp(/^[0-9a-zA-Z]{0,20}$/).test(str);
}

//消息制造函数
//width, height定义弹出窗口的宽度和高度;
//type, speed定义窗口以什么样的方式(show\fade)和速度呈现
//title, text, time主要定义窗口显示的内容，以及窗口显示多长时间后进行隐藏
function ZLMessager(width, height, type, speed, title, text, time) {
    $.messager.lays(width, height);
    $.messager.anim(type, speed);
    $.messager.show(title, text, time);
}

function showMessage(title, msg) {
    if (typeof (msg) == 'undefined') {
        msg = title;
        title = '提示';
    }
    $.pnotify({
        pnotify_title: title
		    , pnotify_text: msg
		    , pnotify_type: 'notice'
            , pnotify_width: '250px'
		    , pnotify_delay: 3000
            , pnotify_animate_speed: 100
			, pnotify_opacity: .9
            , pnotify_history: false
		    , pnotify_animation: { effect_in: 'fade', effect_out: 'fade' }
    });
}
function showError(title, errMsg) {
    if (typeof (errMsg) == 'undefined') {
        errMsg = title;
        title = '错误';
    }
    $.pnotify({
        pnotify_title: title
		    , pnotify_text: errMsg
		    , pnotify_type: 'error'
            , pnotify_width: '250px'
		    , pnotify_delay: 5000
            , pnotify_animate_speed: 100
			, pnotify_opacity: .9
            , pnotify_history: false
		    , pnotify_animation: { effect_in: 'fade', effect_out: 'fade' }
    });
}

function showAlert(title, msg, okFn) {
    $('<div title="' + title + '"><p>' + msg + '</p></div>').dialog({
        resizable: false,
        show: "fade",
        hide: "fade",
        modal: true,
        buttons: {
            "确认": function (btn) {
                $(btn.currentTarget).button({ disabled: true });
                if (okFn != null) {
                    executeFunction(okFn, btn);
                }
                $(this).dialog("close");
            }
        }
    });
}

function showConfirm(title, msg, okFn, cancleFn) {
    $('<div title="' + title + '"><p>' + msg + '</p></div>').dialog({
        resizable: false,
        show: "fade",
        hide: "fade",
        modal: true,
        buttons: {
            "取消": function (btn) {
                $(this).dialog("close");
                if (cancleFn != null) {
                    executeFunction(cancleFn);
                }
            },
            "确认": function (btn) {
                $(btn.currentTarget).button({ disabled: true });
                if (okFn != null) {
                    executeFunction(okFn, btn);
                }
                $(this).dialog("close");
            }
        }
    });
}
function showPrompt(title, msg, yesFn, noFn, cancleFn) {
    $('<div title="' + title + '"><p>' + msg + '</p></div>').dialog({
        resizable: false,
        show: "fade",
        hide: "fade",
        modal: true,
        buttons: {
            "取消": function () {
                $(this).dialog("close");
            },
            "否": noFn,
            "是": yesFn
        }

    });
}
function executeFunction(func, paramObj) {
    if (func != undefined && jQuery.isFunction(func)) {
        jQuery(func(paramObj));
    }
}
/*
处理url请求错误（返回值不为200的网络错误）
*/
function handleServerError(xhr, status, error) {
    var errCap = "错误";
    if (typeof (error) != 'undefined') {
        errCap = error;
    }
    if (xhr.status == 403) {
        showError(errCap + xhr.status, "您没有权限使用该功能");
    }
    else if (!isEmpty(xhr.responseText))
    //防止返回错误过长显示到页面
        if (xhr.responseText.length > 200) {
            showError(errCap + xhr.status, "服务器错误");
        }
        else {
            showError(errCap + xhr.status, xhr.responseText);
        }
}
/*
ajax请求
@requestURL 
@postParams： 提交参数的json数据
@msgBool: 是否显示消息 true/false，不显示消息留空或false
@callBack 回调函数
@completeCallback: 请求完成后回调函数
@btn: 源按钮
*/
function ajaxRequest(requestURL, postParams, msgBool, callBack, completeCallback, btn) {
    if (btn != null) {
        if (btn.currentTarget != null)
            $(btn.currentTarget).button({ disabled: true });
        else
            $(btn).button({ disabled: true });
    }
    jQuery.ajax({
        url: requestURL, type: 'POST', json: 'json', data: postParams, traditional: true,
        success: function (response) {
            try {
                if (msgBool) {
                    if (response.Result) {
//                        alert(response + "|" + requestURL + "|" + postParams);
                        showMessage('提示', response.Message);
                    } else {
//                        alert(response + "|" + requestURL + "|" + postParams);
                        showError('错误提示', response.Message);
                        // return false;
                    }
                }
                executeFunction(callBack, response);
            } catch (e) {
                alert(e);
                showError('错误提示', '此次请求发生异常，请重试!' + e);
            }
        },
        complete: function () {
            if (completeCallback != null) {
                executeFunction(completeCallback);
            }
            if (btn != null) {
                if (btn.currentTarget != null)
                    $(btn.currentTarget).button({ disabled: false });
                else
                    $(btn).button({ disabled: false });
            }
        }
    });
}


function setFormValues(formId, postURL, postData, callBack) {
    ajaxRequest(postURL, postData, false, function (response) {
        if (response.Result) {
            var rowdata = response.Data;
            if (rowdata) {
                for (var i in rowdata) {
                    if ($("[name='" + $.jgrid.jqID(i) + "']", "#" + formId).is("input:radio") || $("[name='" + $.jgrid.jqID(i) + "']", "#" + formId).is("input:checkbox")) {
                        $("[name='" + $.jgrid.jqID(i) + "']", "#" + formId).each(function () {
                            if ($(this).val() == rowdata[i] | $(this).val() == rowdata[i] + '') {
                                $(this).attr("checked", true);
                            } else {
                                $(this).attr("checked", false);
                            }
                        });
                    } else {
                        if (typeof (rowdata[i]) == "string" && rowdata[i].indexOf('Date(') > 0) {
                            $("[name='" + $.jgrid.jqID(i) + "']", "#" + formId).val(dataTimeFormat(rowdata[i]));
                        } else {
                            $("[name='" + $.jgrid.jqID(i) + "']", "#" + formId).val(rowdata[i]);
                        }
                    }
                }
            }
        } else {
            //alert(3);
            showError('错误提示', response.Message);
        }
        if (callBack != null) {
            executeFunction(callBack, response);
        }
    });
};

 
//带前缀的setFormValues方法
function setFormValuesWithPre(formId, postURL, postData, callBack, pre) {
    ajaxRequest(postURL, postData, false, function (response) {
        if (response.Result) {
            var rowdata = response.Data;
            if (rowdata) {
                for (var i in rowdata) {
                    //                    //alert(i);
                    if ($("[name='" + pre + "." + $.jgrid.jqID(i) + "']", "#" + formId).is("input:radio") || $("[name='" + pre + "." + $.jgrid.jqID(i) + "']", "#" + formId).is("input:checkbox")) {
                        $("[name='" + pre + "." + $.jgrid.jqID(i) + "']", "#" + formId).each(function () {
                            if ($(this).val() == rowdata[i]) {
                                $(this).attr("checked", true);
                            } else {
                                $(this).attr("checked", false);
                            }
                        });
                    } else {
                        if (typeof (rowdata[i]) == "string" && rowdata[i].indexOf('Date(') > 0) {
                            $("[name=" + pre + "." + $.jgrid.jqID(i) + "]", "#" + formId).val(dataTimeFormat(rowdata[i]));
                        } else {
                            $("[name=" + pre + "." + $.jgrid.jqID(i) + "]", "#" + formId).val(rowdata[i]);
                        }
                    }
                }
            }
        } else {
            //alert(4);
            showError('错误提示', response.Message);
        }
        if (callBack != null) {
            executeFunction(callBack, response);
        }
    });
};


function setFormValueWithPre(formId, postURL, postData, callBack, pre) {
    ajaxRequest(postURL, postData, false, function (response) {
        if (response.Result) {
            var rowdata = response.Data;
            if (rowdata) {
                for (var i in rowdata) {
                    if ($("[name='" + pre + "." + i + "']", formId).is("input:radio") || $("[id='" + pre + "_" + i + "']", formId).is("input:checkbox")) {
                        $("[name='" + pre + "." + i + "']", formId).each(function () {
                            if (rowdata[i]) {
                                $("[id='" + pre + "_" + i + "']", formId).attr("checked", true);
                            } else {
                                $("[id='" + pre + "_" + i + "']", formId).attr("checked", false);
                            }
                        });
                    } else {
                        $("[name='" + pre + "." + i + "']", formId).val(rowdata[i]);
                    }
                }
            }
        } else {
            //alert(1);
            showError('错误提示', response.Message);
        }
        if (callBack != null) {
            executeFunction(callBack, response);
        }
    });
};

/*formats*/
//bool类型的值格式化，只针对0,1
function booleanFmt(cellvalue, options, rowObject) {
    if (cellvalue == true || cellvalue == "true") {
        var style = "color:green;";
        if (typeof (options.colModel.formatterStyle) != "undefined") {
            var txt = options.colModel.formatterStyle[1];
        } else {
            var txt = "是";
        }

    }
    else if (cellvalue == false || cellvalue == "false") {
        var style = "color:red;";
        if (typeof (options.colModel.formatterStyle) != "undefined") {
            var txt = options.colModel.formatterStyle[0];
        } else {
            var txt = "否";
        }
    }
    else {
        var txt = '';
    }

    return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
}

function booleanUnFmt(cellvalue, options, cell) {
    return $('span', cell).attr('rel');
}

function saveStatusFmt(cellvalue, options, rowObject) {
    var style = "color:red;";
    var txt = '修改中……<div title="保存所选记录" style="float:left;margin-left:5px;" class="" onclick=handleModifySave("' + rowObject.ID + '"); onmouseover=jQuery(this).addClass("ui-state-hover"); onmouseout=jQuery(this).removeClass("ui-state-hover");><span class="ui-button-icon-primary ui-icon ui-icon-disk"></span></div>';
    if (cellvalue == true || cellvalue == "true") {
        style = "color:green;";
        var txt = '已发送';
    }


    return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
}
function sendStatusFmt(cellvalue, options, rowObject) {
    var style = "color:red;";
    var txt = '发送失败，<a title="重发所选记录" style="margin-left:5px;cursor:pointer;"  onclick=handleRepeatSend("' + rowObject.ID + '","' + rowObject.ShipDocID + '"); onmouseover=jQuery(this).addClass("ui-state-hover"); onmouseout=jQuery(this).removeClass("ui-state-hover");>重发</a>';
    if (cellvalue == true || cellvalue == "true") {
        style = "color:green;";
        var txt = '发送成功';
    }
    if (cellvalue == 2) {
        style = "color:blue;";
        var txt = '发送中……';
    }


    return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
}

function duringTimeFmt(cellvalue, options, rowObject) {
    var style = "color:red;";
    var txt = "";
    if (cellvalue == 0) {
        txt = "今天";
    } else if (cellvalue == 1) {
        style = "color:Green;";
        txt = "昨天";
    } else if (cellvalue == 2) {
        style = "color:Black;";
        txt = "更早";
    } else {
        style = "color:Blue;";
        txt = "您的系统时间有问题，请修复！";
    }

    return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>';
}

function recieverFmt(cellvalue, options, rowObject) {

    var style = "color:black;";
    var txt = cellvalue;
    var aTag = "";
    if (isEmpty(cellvalue)) {
        txt = "暂未指定收件人";
    } else if (cellvalue.length > 25) {
        txt = txt.substr(0, 25).concat("...");
        aTag = "[<a id='expandUserList_" + rowObject.ID + "' href='javascript:void(0);'>展开</a>]";
    }

    return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>' + aTag;
}

function recieverUnFmt(cellvalue, options, cell) {
    return $('span', cell).attr('rel');
}
function LiftclyFmt(cellvalue, options, rowObject) {
    var style = "color:green;";
    var txt = '新单';
    if (cellvalue == -1) {
        style = "color:red;";
        var txt = '删除';
    }

    if (cellvalue == 1) {
        style = "color:blue;";
        var txt = '已审核';
    }

    return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
}

function LiftclyUnFmt(cellvalue, options, cell) {
    return $('span', cell).attr('rel');
}

function FixedOneNumber(cellvalue, options, cell) {
    var q = /[\$,%]/g;
    cellvalue = parseFloat(String(cellvalue.toFixed(1)).replace(q, ""));
    return isNaN(cellvalue) ? 0 : cellvalue;
}

function FixedTwoNumber(cellvalue, options, cell) {
    var q = /[\$,%]/g;
    cellvalue = parseFloat(String(cellvalue.toFixed(2)).replace(q, ""));
    return isNaN(cellvalue) ? 0 : cellvalue;
}


function FixedThreeNumber(cellvalue, options, cell) {
    var q = /[\$,%]/g;
    cellvalue = parseFloat(String(cellvalue.toFixed(3)).replace(q, ""));
    return isNaN(cellvalue) ? 0 : cellvalue;
}

//格式化浮点数，自定义保留小数位数
function formatFloat(src, pos) {
    return Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos);
}
//Kg与T的格式化操作,也共用与Yuan/Kg与Yuan/T
function Kg2TFmt(cellvalue, options, rowObject) {
    if (cellvalue && cellvalue != 0)
        return formatFloat(cellvalue / 1000, 2);
    else
        return 0;
}
function T2KgFmt(cellvalue, options, rowObject) {
    if (cellvalue && cellvalue != 0)
        return cellvalue * 1000;
    else
        return 0;
}

//render 字典数据
function dicCodeFmt(cellvalue, options, rowObject) {
    if (cellvalue == null) return '';
    if (typeof (cellvalue) != 'undefined' && !$.isEmptyObject(gDics)) {
        var pId = options.colModel.formatoptions.parentId;
        if (gDics.hasOwnProperty(pId)) {
            var total = gDics[pId].length;
            for (var i = 0; i < total; i++) {
                var dic = gDics[pId][i];
                if (dic.TreeCode == cellvalue || dic.ID == cellvalue) {//Dic表TreeCode和ID字段关联都可以
                    return '<span rel="' + cellvalue + '" class="' + pId + '_' + cellvalue + '">' + dic.DicName + '</span>';
                }

            }
        }
    }

    return cellvalue;
}

function dicCodeUnFmt(cellvalue, options, cell) {
    /*if (typeof (cellvalue) != 'undefined' && !$.isEmptyObject(gDics)) {
    var pId = options.colModel.formatoptions.parentId;
    if (gDics.hasOwnProperty(pId)) {
    var total = gDics[pId].length;
    for (var i = 0; i < total; i++) {
    var dic = gDics[pId][i];
    if (dic.DicName == cellvalue) {
    return dic.TreeCode;
    }
    }
    }
    }
    */
    var val = $('span', cell).attr('rel');
    if (typeof (val) != 'undefined' && val != "")
        return val;
    else
        return cellvalue;
}

function dicNormalRenderer(val, parentId) {
    var options = { colModel: { formatoptions: { parentId: parentId}} };
    return dicCodeFmt(val, options);

}
//容重
function rzFmt(cellvalue, options, rowObject) {
    var rz = cellvalue * 1;
    var ftype = "";
    if (typeof (rowObject.FormulaType) != 'undefined' && rowObject.FormulaType != "") {
        ftype = rowObject.FormulaType;
    }
    if (typeof (rowObject.IsSlurry) != 'undefined' && rowObject.IsSlurry != "") {
        ftype = rowObject.IsSlurry;
    }
    var cls = "";
    if (ftype == "FType_S" || ftype == 1 || ftype == true) {
        if (rz > gSysConfig.SlurryRZMax * 1 || rz < gSysConfig.SlurryRZMin * 1) {
            cls = "class='hlcell' title='砂浆容重超出范围'";
        }
    }
    else {
        if (rz > gSysConfig.FormulaRZMax * 1 || rz < gSysConfig.FormulaRZMin * 1) {
            cls = "class='hlcell' title='混凝土容重超出范围'";
        }
    }
    return '<span rel="' + rz + '" ' + cls + '>' + rz + '</span>';
}
function rzUnFmt(cellvalue, options, cell) {
    var val = $('span', cell).attr('rel');
    if (typeof (val) != 'undefined' && val != "")
        return val;
    else
        return cellvalue;
}
function identityFormatter(cellvalue, options, rowObject) {
    var idStr = '';

    for (var i = 0; i < cellvalue.length; i++) {
        if (idStr.length > 0) {
            idStr += ',';
        }
        idStr += cellvalue[i].IdentityName;

    }
    return idStr;
}

//返回jqgrid toolbar search需要的字典对象，使用searchoptions:{value:dicToolbarSearchValues('UserType')}
function dicToolbarSearchValues(pId) {
    var total = gDics[pId].length;
    var obj = { '': '' };
    for (var i = 0; i < total; i++) {
        var dic = gDics[pId][i];
        obj[dic.TreeCode] = dic.DicName;
    }
    return obj;
}
function booleanSearchValues() {
    return { '': '', 1: '是', 0: '否' };
}
function booleanSelectValues() {
    return { 'true': '是', 'false': '否' };
}

//创建日期时间控件
function createDateTimeWidget(options, format) {

}
//重新绑定下拉列表数据
function bindSelectData(target, url, data, callback) {
    $.post(url,
        data,
        function (data) {
            target.empty();
            target.append("<option value=''></option>");
            if (!$.isEmptyObject(data)) {
                target = $(target);
                for (var i = 0; i < data.length; i++) {
                    var d = data[i];
                    target.append("<option value='" + d.Value + "'>" + d.Text + "</option>");
                }
            }
            if (callback != null) {
                executeFunction(callback, data);
            }
        });
    }
    //重新绑定下拉列表数据
    function bindSelectData1(target, url, data, callback) {
        $.post(url,
        data,
        function (data) {
            target.empty();
            target.append("<option value=''></option>");
            if (!$.isEmptyObject(data)) {
                target = $(target);
                for (var i = 0; i < data.length; i++) {
                    var d = data[i];
                    if (i == 0)
                        target.append("<option selected='selected' value='" + d.Value + "'>" + d.Text + "</option>");
                    else
                        target.append("<option value='" + d.Value + "'>" + d.Text + "</option>");
                }
            }
            if (callback != null) {
                executeFunction(callback, data);
            }
        });
    }


//时间日期格式化
Date.prototype.format = function (format) {
    if (typeof (jQuery) != 'undefined' && typeof (jQuery.jgrid) != 'undefined' && typeof (jQuery.fmatter) != 'undefined')
        return $.fmatter.util.DateFormat('', this, format, $.jgrid.formatter.date);
    else
        return this;
};
//格式化时间日期，可以格式化“\/Date(1329786028000)\/”类型的字符串
function dataTimeFormat(value, format) {
    if (typeof (format) == 'undefined') {
        format = 'Y-m-d H:i:s';
    }
    return $.fmatter.util.DateFormat('', value, format, $.jgrid.formatter.date)
}

function attachmentFmt(cellvalue, options, rowObject) {
    if (cellvalue.length == 0) return '';
    var html = '<ul>';
    for (var i = 0; i < cellvalue.length; i++) {
        var data = cellvalue[i];
        html += '<li><a href="' + data.FileUrl + '" target="_blank">' + data.Title + '</a>  <a att-id="' + data.ID + '" style="display:none;float:left" title="删除附件" href="javascript:void 0;"><span class="ui-icon ui-icon-trash"></span></a></li>';
    }

    return html + '</ul>';
}

function attachmentFmt2(cellvalue, options, rowObject) {
    if (cellvalue.length == 0) return '';
    var html = '';
    for (var i = 0; i < cellvalue.length; i++) {
        var data = cellvalue[i];
        html += '<span style="display: block; height: 19px; padding-left:17px; background: url(/Content/themes/default/images/fj.gif) repeat-y scroll 0px 2px transparent; "><span><a att-id="' + data.ID + '" href="' + data.FileUrl + '" target="_blank" style="text-decoration:none; ">' + data.Title + '</a></span><span att-id="' + data.ID + '"  style="color:red;cursor:pointer;" title="删除此附件" onclick="physicalDel(this,\'' + data.ID + '\', \'' + data.FileUrl + '\' )"> ✘<\/span>&nbsp;<\/span>';
    }

    return html;
}

//物理删除文件
function physicalDel(o, id, path) {
    caller = $(o);
    ajaxRequest(
        "/Attachment.mvc/Delete",
        {
            id: id
        },
        false,
        function (rep) {
            if (rep.Result) {
                var filelisttr = caller.parents("#filelisttr");
                caller.parent('span').remove();
                File.count--;
                if (File.count == 0) {
                    filelisttr.hide();
                }
            }
        }
    );
}

/*初始化自定义控件方法*/
function initErpControls(container) {
    if (!container) {
        container = '#container';
    }
    container = $(container);
    $('input[zlerp],select[zlerp]', container).each(function () {
        var type = $(this).attr("zlerp");
        if (type) {
            if (typeof (ErpInitFunctions) != 'undefined' && ErpInitFunctions.hasOwnProperty(type)) {
                eval('(ErpInitFunctions.' + type + '(this))');
                //移除属性
                $(this).removeAttr('zlerp');
                var attrs = [];
                for (var i = 0; i < this.attributes.length; i++) {
                    var attrName = this.attributes.item(i).nodeName;
                    if (attrName.indexOf('zl-') == 0)
                        attrs.push(attrName);
                }
                for (var i = 0; i < attrs.length; i++) {
                    $(this).removeAttr(attrs[i]);
                }
            }
        }
    });
    var iconMaps = {
        "refresh": "ui-icon-refresh",
        "add": "ui-icon-plus",
        "save": "ui-icon-disk",
        "edit": "ui-icon-pencil",
        "delete": "ui-icon-close",
        "remove": "ui-icon-close",
        "audit": "ui-icon-check",
        "zhuanfa": "ui-icon-arrowreturnthick-1-e",
        "return": "ui-icon-arrowrefresh-1-w",
        "print": "ui-icon-print",
        "import":"ui-icon-transferthick-e-w"
    };
    //初始化button
    $('button.tbtn', container).each(function () {
        $(this).removeClass('tbtn');
        var cls = $(this).attr('class');
        var icon = cls;
        if (iconMaps.hasOwnProperty(icon)) {
            icon = iconMaps[cls];
        }
        $(this).button({
            icons: {
                primary: icon
            }
        });
    });
    //必填字段样式
    $("[data-val-required]", container).addClass("requiredval");

}
var ErpInitFunctions = {
    //autocomplete
    autocomplete: function (obj) {
        var caller = $(obj);
        var url = caller.attr('zl-ac-url');
        var value = caller.attr('zl-ac-value');
        var text = caller.attr('zl-ac-text');
        var allowCache = (typeof (caller.attr('allowCache')) != 'undefined' && caller.attr('allowCache').toLowerCase() == 'false') || (!isEmpty(caller.attr('zl_callback'))) ? false : true;

        var extraCallback = caller.attr('zl_callback');

        caller.autocp({ url: url, valueInputName: value, displayInputName: text, allowCache: allowCache, extraCallback: extraCallback });
    },
    combobox: function (obj) {
        $(obj).combobox();
    },
    datepicker: function (obj) {
        var caller = $(obj);
        caller.datepicker({ changeMonth: true, changeYear: true });
    },
    datetimepicker: function (obj) {
        var caller = $(obj);
        caller.datetimepicker({ showSecond: true, timeFormat: 'hh:mm:ss', changeMonth: true, changeYear: true });
    },
    timepicker: function (obj) {
        var caller = $(obj);
        caller.timepicker({
            timeFormat: 'hh:mm:ss'
            //,separator: ' @ '
        });
    },
    //dropdowntree
    ddtree: function (obj) {
        var caller = $(obj);
        var url = caller.attr('zl-dt-url');
        var value = caller.attr('zl-dt-value');
        var text = caller.attr('zl-dt-text');
        var defaultVal = caller.attr('zl-dt-default-val');
        caller.dropdownTree({ url: url, showField: text, valueField: value, defaultVal: defaultVal });
    }
};
/*扩展jqgrid GridToForm方法*/

$(function () {
    $.jgrid.extend({
        GridToForm: function (rowid, formid, prefix, jsonprefix) {
            return this.each(function () {
                var $t = this;
                if (!$t.grid) { return; }
                if ($('td', $t).hasClass('edit-cell')) {
                    $($t).jqGrid("restoreCell", $t.p.iRow, $t.p.iCol);
                }
                if ($t.grid.hDiv.loading) {
                    setTimeout(function () {
                        $($t).jqGrid('GridToForm', rowid, formid, prefix, jsonprefix);
                    }, 10);
                    return;
                }
                var rowdata = $($t).jqGrid("getRowData", rowid);
                if (jsonprefix) {
                    var key, pos;
                    var newrecord = {};
                    $.each(rowdata, function (i, n) {
                        key = i.toString();
                        pos = key.indexOf(jsonprefix + ".");
                        if (pos != -1) {
                            key = key.substr(pos + jsonprefix.length + 1);
                        }
                        newrecord[key] = n;
                    });
                    rowdata = newrecord;
                }
                prefix = prefix ? prefix + "." : "";
                if (rowdata) {
                    for (var i in rowdata) {

                        if ($("[name='" + prefix + $.jgrid.jqID(i) + "']", formid).is("input:radio") || $("[name='" + prefix + $.jgrid.jqID(i) + "']", formid).is("input:checkbox")) {
                            $("[name='" + prefix + $.jgrid.jqID(i) + "']", formid).each(function () {
                                if ($(this).val() == rowdata[i]) {
                                    $(this)[$t.p.useProp ? 'prop' : 'attr']("checked", true);
                                } else {
                                    $(this)[$t.p.useProp ? 'prop' : 'attr']("checked", false);
                                }
                            });
                        } else {
                            // this is very slow on big table and form.
                            $("[name='" + prefix + $.jgrid.jqID(i) + "']", formid).val(rowdata[i]);
                        }
                    }
                }
            });
        },
        //扩展getRowData对特殊字符的处理 xyl
        getRowData: function (rowid) {
            var res = {}, resall, getall = false, len, j = 0;
            this.each(function () {
                var $t = this, nm, ind;
                if (typeof (rowid) == 'undefined') {
                    getall = true;
                    resall = [];
                    len = $t.rows.length;
                } else {
                    ind = $t.rows.namedItem(rowid);
                    if (!ind) { return res; }
                    len = 2;
                }
                while (j < len) {
                    if (getall) { ind = $t.rows[j]; }
                    if ($(ind).hasClass('jqgrow')) {
                        //选择记录时，rowid包含特殊字符的处理(以特殊字符开头的除外)。该特殊字符主要影响jquery的选择器 xyl 2013-06-20
                        var repid = rowid;
                        var range = $('td[aria-describedby]', ind);
                        var specialCharacters = ['.', ':', '[', ']'];
                        $.each(specialCharacters, function (i, n) {
                            
                            if (rowid.toString().indexOf(n) != -1) {
                                repid = repid.replace(n, "\\" + n);
                            }
                        });

                        if (repid !== rowid) range = $("#" + repid + " td[aria-describedby]");
                        range.each(function (i,domEl) {
                            if ($t.p.colModel[i] != undefined) {
                                nm = $t.p.colModel[i].name;
                            }
                            if (nm !== 'cb' && nm !== 'subgrid' && nm !== 'rn') {
                                if ($t.p.treeGrid === true && nm == $t.p.ExpandColumn) {
                                    res[nm] = $.jgrid.htmlDecode($("span:first", this).html());
                                } else {
                                    try {
                                        res[nm] = $.unformat(this, { rowId: ind.id, colModel: $t.p.colModel[i] }, i);
                                    } catch (e) {
                                        res[nm] = $.jgrid.htmlDecode($(this).html());
                                    }
                                }
                            }
                        });
                        if (getall) { resall.push(res); res = {}; }
                    }
                    j++;
                }
            });
            return resall ? resall : res;
        },
        getRootNodes: function () {
            var result = [];
            this.each(function () {
                var $t = this;
                if (!$t.grid || !$t.p.treeGrid) { return; }
                switch ($t.p.treeGridModel) {
                    case 'nested':
                        var level = $t.p.treeReader.level_field;
                        $($t.p.data).each(function (i) {
                            if (parseInt(this[level], 10) === parseInt($t.p.tree_root_level, 10)) {
                                result.push(this);
                            }
                        });
                        break;
                    case 'adjacency':
                        var parent_id = $t.p.treeReader.parent_id_field;
                        $($t.p.data).each(function (i) {
                            if (this[parent_id] === null || String(this[parent_id]).toLowerCase() == "null") {
                                result.push(this);
                            }
                        });
                        break;
                }
                if (result.length == 0 && $t.p.data.length > 0) {
                    var item = $t.p.data[0];
                    switch ($t.p.treeGridModel) {
                        case 'nested':
                            var level = $t.p.treeReader.level_field;
                            $($t.p.data).each(function (i) {
                                if (parseInt(this[level], 10) === parseInt(item[level], 10)) {
                                    result.push(this);
                                }
                            });
                            break;
                        case 'adjacency':
                            var parent_id = $t.p.treeReader.parent_id_field;
                            $($t.p.data).each(function (i) {
                                if (this[parent_id] === item[parent_id]) {
                                    result.push(this);
                                }
                            });
                            break;
                    }
                }
            });
            return result;
        }
    });
});

//打开关闭快捷菜单
function toggleFMenu() {
    var menu = $('#favoriteMenu>ul');
    menu.toggle('blind', 'fast', function () {
        $('.ftitle span').toggleClass('ui-icon-triangle-1-e');
    });

}
//handle ajax server error
$.ajaxSetup({ error: handleServerError });

//打开带参数的报表信息
window.OpenReportByPara = function (url, para) {
    if (para == null) {
        var ids = $("#ID").val();
        if (ids == null || ids.length < 1) {
            showMessage("未找到对应的技术资料");
            return;
        }
        else {
            para = $("#ID").val();
        }
    }
    window.open(url + "?uid=" + para);
};

function GetDateBySpan(span) {
    //获取系统时间 
    var LSTR_ndate = new Date();
    var LSTR_Year = LSTR_ndate.getFullYear();
    var LSTR_Month = LSTR_ndate.getMonth();
    var LSTR_Date = LSTR_ndate.getDate();

    //处理 
    var uom = new Date(LSTR_Year, LSTR_Month, LSTR_Date);
    uom.setDate(uom.getDate() - span); //根据系统时间以及跨度取得新的时间,重点在这里,负数是前几天,正数是后几天 
    var LINT_MM = uom.getMonth();
    LINT_MM++;
    var LSTR_MM = LINT_MM > 10 ? LINT_MM : ("0" + LINT_MM);
    var LINT_DD = uom.getDate();
    var LSTR_DD = LINT_DD > 10 ? LINT_DD : ("0" + LINT_DD);
    //得到最终结果 
    uom = uom.getFullYear() + "-" + LSTR_MM + "-" + LSTR_DD;
    return uom;
}

//判断日期，时间大小  
function compareTime(startDate, endDate) {
    if (startDate.length > 0 && endDate.length > 0) {
        var startDateTemp = startDate.split(" ");
        var endDateTemp = endDate.split(" ");

        var arrStartDate = startDateTemp[0].split("-");
        var arrEndDate = endDateTemp[0].split("-");

        var arrStartTime = startDateTemp[1].split(":");
        var arrEndTime = endDateTemp[1].split(":");

    var allStartDate = new Date(arrStartDate[0], arrStartDate[1], arrStartDate[2], arrStartTime[0], arrStartTime[1], isEmpty(arrStartTime[2]) ? "00" : arrStartTime[2]);
    var allEndDate = new Date(arrEndDate[0], arrEndDate[1], arrEndDate[2], arrEndTime[0], arrEndTime[1], isEmpty(arrEndTime[2]) ? "00" : arrEndTime[2]);   
                   
if (allStartDate.getTime() >= allEndDate.getTime()) { //开始时间大于结束时间  
        return true;   
} else {   
    return false;   
       }
}
}

(function ($) {
    $.cookie = function (key, value, options) {

        // key and at least value given, set cookie...
        if (arguments.length > 1 && (!/Object/.test(Object.prototype.toString.call(value)) || value === null || value === undefined)) {
            options = $.extend({}, options);

            if (value === null || value === undefined) {
                options.expires = -1;
            }

            if (typeof options.expires === 'number') {
                var days = options.expires, t = options.expires = new Date();
                t.setDate(t.getDate() + days);
            }

            value = String(value);

            return (document.cookie = [
                encodeURIComponent(key), '=', options.raw ? value : encodeURIComponent(value),
                options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
                options.path ? '; path=' + options.path : '',
                options.domain ? '; domain=' + options.domain : '',
                options.secure ? '; secure' : ''
            ].join(''));
        }

        // key and possibly options given, get cookie...
        options = value || {};
        var decode = options.raw ? function (s) { return s; } : decodeURIComponent;

        var pairs = document.cookie.split('; ');
        for (var i = 0, pair; pair = pairs[i] && pairs[i].split('='); i++) {
            if (decode(pair[0]) === key) return decode(pair[1] || ''); // IE saves cookies with empty string as "c; ", e.g. without "=" as opposed to EOMB, thus pair[1] may be undefined
        }
        return null;
    };
})(jQuery);

function setCookie(key, value) {
    var opt = { expires: 365 };
    $.cookie(key, value, opt);
}
/*************************************
** AutoComplete中的回调函数
** callback:回调函数的名称，对应的函数定义必须与该名称相同
** xyl 2013-08-07
*************************************/
extraCallback = function (callback) {
    return eval(callback + "();");
};

/*************************************
** 在指定的URL中获取指定的参数值
** url:要获取参数的URL
** paramname:要取值的参数名称
** xyl 2013-08-07
*************************************/
function getParamValue(url, paramname) {
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {};
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paramname.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

/*************************************
** 闪烁效果
** el:要闪烁的元素
** cls:样式名称
** times:闪烁次数
** xyl 2013-09-22
*************************************/
function shake(el, cls, times) {
    var i = 0, t = false, o = el.attr("class") + " ", c = "", times = times || 2;
    if (t) return;
    t = setInterval(function () {
        i++;
        c = i % 2 ? o + cls : o;
        el.attr("class", c);
        if (i == 2 * times) {
            clearInterval(t);
            el.removeClass(cls);
        }
    }, 500);
}

function downloads(rowObject) {
    //console.dir(rowObject);
}

/*************************************
** 隐藏一组功能按钮
** toolbarId:按钮组所在的工具栏ID
** funcIdArray:要隐藏的按钮功能ID数组
** 说明：如果funcIdArray为空数组或为null，则表示不隐藏按钮，toolbarId下的所有按钮均显示
** xyl 2013-10-22
*************************************/
function hideGroupButton(toolbarId, funcIdArray, callBack) {

    $.each($("#" + toolbarId).children(), function (i, n) {
        $.inArray($(n).val().toString(), funcIdArray) != -1 ? $(n).hide() : $(n).show();
    });
    if (callBack != null) {
        executeFunction(callBack);
    }

}

function EDateFormat(datestr, style) {
    datestr = datestr.replace(/-/g, "/");
    datestr = Date.parse(datestr);
    if (typeof (style) == "undefined") style = 0;
    var nowdate, y, m, d, h, i, s, t, APM, hAPM;
    nowdate = new Date(datestr);
    var weekDay = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
    y = nowdate.getFullYear();
    m = nowdate.getMonth() + 1;
    d = nowdate.getDate();
    w = weekDay[nowdate.getDay()];
    h = nowdate.getHours();
    i = nowdate.getMinutes();
    s = nowdate.getSeconds();

    if (h > 12) {
        APM = "下午";
        hAPM = parseInt(h) % 12;
    } else {
        APM = "上午";
        hAPM = h;
    }

    if (style == 9) {
        var currentdate = new Date();
        cy = currentdate.getFullYear();
        cm = currentdate.getMonth() + 1;
        cd = currentdate.getDate();
        ch = currentdate.getHours();
        ci = currentdate.getMinutes();
        cs = currentdate.getSeconds();

        if (cy == y && cm == m && cd == d) style = 8;
        if (cy == y && cm != m) style = 4;
        if (cy != y) style = 5;
    }

    switch (style) {
        case 0:
            t = y + "年" + m + "月" + d + "日" + "（" + w + "）" + " " + APM + hAPM + ":" + i + ":" + s;
            break;
        case 1:
            t = y + "-" + m + "-" + d + " " + h + ":" + i + ":" + s;
            break;
        case 2:
            t = y + "-" + m + "-" + d + " " + h + ":" + i;
            break;
        case 3:
            t = (y.toString()).substring(2, 4) + "-" + m + "-" + d + " " + h + ":" + i;
            break;
        case 4:
            t = m + "-" + d + " " + h + ":" + i;
            break;
        case 5:
            t = y + "-" + m + "-" + d;
            break;
        case 6:
            t = (y.toString()).substring(2, 4) + "-" + m + "-" + d;
            break;
        case 7:
            t = m + "-" + d;
            break;
        case 8:
            t = h + ":" + i + ":" + s;
            break;
    }
    return t;

}

function reJsonByPrefix(jsonrecord, prefixstr) {
    var key, pos;
    var newrecord = {};
    $.each(jsonrecord, function (i, n) {
        key = i.toString();
        pos = key.indexOf(prefixstr + ".");
        if (pos != -1) {
            key = key.substr(pos + prefixstr.length + 1);
        }
        newrecord[key] = n;
    });
    return newrecord;
}

var Base64 = (function () {
    // private property
    var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    // private method for UTF-8 encoding
    function utf8Encode(string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }
        return utftext;
    }

    // public method for encoding
    return {
        encode: (typeof btoa == 'function') ? function (input) { return btoa(utf8Encode(input)); } : function (input) {
            var output = "";
            var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;
            input = utf8Encode(input);
            while (i < input.length) {
                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;
                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }
                output = output +
                keyStr.charAt(enc1) + keyStr.charAt(enc2) +
                keyStr.charAt(enc3) + keyStr.charAt(enc4);
            }
            return output;
        }
    };
})();

//getExcelXml = function (grid, includeHidden) {
//    var cm = $(grid).getGridParam('colModel');
//    var mydata = "[";
//    for (var i = 0; i < cm.length; i++) {
//        var lable = cm[i].label ? cm[i].label : '';
//        lable = lable.replace(/<.*?>/g, "").replace(/[^A-Za-z0-9_\u4E00-\u9FA5]/g, "");
//        var name = cm[i].name ? cm[i].name : '';

//        if (lable.length == 0) continue;
//        var tmp = "{ id:'" + i + "',lable:'" + lable + "',name:'" + name + "'},";
//        mydata += tmp;
//    }
//    mydata += "]";

//    var myobj = eval(mydata);

//    $("#list482").jqGrid({
//        data: myobj,
//        datatype: "local",
//        height: 300,
//        rowNum: 10,
//        width: 400,
//        rowList: [10, 20, 30, 50, 100],
//        colNames: ['编号', '标签', '编码'],
//        multiselect: true,
//        rowNumbers: true,
//        colModel: [
//       		{ name: 'id', index: 'id', width: 30, sorttype: "int" },
//            { name: 'lable', index: 'lable', width: 50 },
//            { name: 'name', index: 'name', width: 50 }
//            ],
//        viewrecords: true,
//        pager: "#plist484",
//        sortname: 'id'
//    });
//    $("#div_excel_erp").dialog({
//        title: "请选择要显示的列"
//        , width: 420
//        , buttons: {
//            "关闭": function () {
//                $(this).dialog("close");
//            }, "保存": function (btn) {
//                var ids = jQuery("#table_excel_erp").jqGrid('getGridParam', "selarrrow");
//                var xml = '<xml>'
//        + '<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">'
//            + '<xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">'
//                + '<xs:complexType>'
//                    + '<xs:choice minOccurs="0" maxOccurs="unbounded">'
//                        + '<xs:element name="Table">'
//                            + '<xs:complexType>'
//                                + '<xs:sequence>';
//                var header;
//                for (var i = 0; i < ids.length; i++) {
//                    var record = jQuery("#table_excel_erp").jqGrid('getRowData', ids[i]);
//                    var header = record.lable ? record.lable : '';
//                    header = header.replace(/<.*?>/g, "").replace(/[^A-Za-z0-9_\u4E00-\u9FA5]/g, "");
//                    if (header.length == 0) continue;
//                    xml += '<xs:element name="' + header + '" type="xs:string" minOccurs="0" />';
//                }

//                xml = xml
//                                + '</xs:sequence>'
//                            + '</xs:complexType>'
//                        + '</xs:element>'
//                    + '</xs:choice>'
//                + '</xs:complexType>'
//            + '</xs:element>'
//        + '</xs:schema>'
//        + '<NewDataSet>';
//                var selectedKeys = $(grid).getGridParam('selarrrow');
//                var selectedRowCount = selectedKeys.length;
//                var allKeys = $(grid).getDataIDs();
//                var allRowCount = allKeys.length;
//                var expandedRowCount = selectedRowCount > 0 ? selectedRowCount : allRowCount;
//                var expandedKeys = selectedRowCount > 0 ? selectedKeys : allKeys;

//                for (i = 0; i < expandedKeys.length; i++) {
//                    xml += '<Table>';
//                    var tr = grid.rows.namedItem(expandedKeys[i]);
//                    var tds = $(tr).children('td');
//                    for (var j = 0; j < ids.length; j++) {
//                        var record = jQuery("#table_excel_erp").jqGrid('getRowData', ids[j]);
//                        var header = record.lable ? record.lable : '';
//                        header = header.replace(/<.*?>/g, "").replace(/[^A-Za-z0-9_\u4E00-\u9FA5]/g, "");
//                        if (header.length == 0) continue;
//                        var index = ids[j];
//                        var v = $(tds[index]).text();
//                        xml += '<' + header + '>';
//                        xml += (v == undefined || $.trim(v).length == 0) ? '' : v.replace(/[<&]/g, "");
//                        xml += '</' + header + '>';
//                    }
//                    xml += '</Table>';
//                }
//                xml += '</NewDataSet>' + '</xml>';
//                return xml;
//            }
//        }
//    });
//}

function CheckDate(BeginDate, EndDate, Days) {
    var beginDate = $("#" + BeginDate).val(),
          endDate = $("#" + EndDate).val();
    if (beginDate == '') {
        showError("请输入开始时间!");
        $("#" + BeginDate).focus();
        return false;
    }
    if (endDate == '') {
        showError("请输入结束时间!");
        $("#" + EndDate).focus();
        return false;
    }
    if (beginDate > endDate) {
        showError("开始时间不能大于结束时间，请检查！");
        return false;
    }
    if (typeof (Days) != "undefined") {
        var beginArr = beginDate.split("-"),
              endArr = endDate.split("-"),
              bY = parseInt(beginArr[0]),
              bM = parseInt(beginArr[1]),
              bD = parseInt(beginArr[2]),
              eY = parseInt(endArr[0]),
              eM = parseInt(endArr[1]),
              eD = parseInt(endArr[2]);

        if (eY - bY == 0) {
            if (eM - bM == 0) {
                if (eD - bD < Days) {
                    return true;
                } else {
                    showError("查询时间不能大于" + Days + "天!");
                    return false;
                }
            } else {
                showError("查询时间不能大于" + Days + "天!");
                return false;
            }
        }
        else {
            showError("查询时间不能大于" + Days + "天!");
            return false;
        }

    }
    else {
        return true;
    }

}

////////////////////////弹出页面//////////////////////
/*
* ShowPage(Url, Title, Width, Height)
*   Url:提交页面的URL，Title页面的标题，Width:页面的宽度（可以不填），Height:页面的高度（可以不填）
*/
////////////////////////////////////////////////////
function showPage(Url, Title, Width, Height) {
    
    if (typeof (Url) == 'undefined') {
        showMessage('提示', 'ShowPage函数的Url不能为空');
        return;
    }
    if (typeof (Title) == 'undefined') {
        Title = '信息';
    }
    if (typeof (Width) == 'undefined') {
        Width = 'auto';
    }
    if (typeof (Height) == 'undefined') {
        Height = 'auto';
    }
    $("#jqDialog").empty();
    
    $("#jqDialog").load(Url).dialog({
        title: Title,
        width: Width,
        height: Height,
        modal: true,
        autoOpen: true
    });
   
}


///
/////////////////////////Ajax提交方法////////////////////
///
var url111 = '';
var ojson111 ;
function DoAjax(Url, oJson, Handler) {
    url111 = Url;
    ojson111 = oJson;
    $.ajax({
        url: Url,
        data: oJson,
        dataType: "json",
        cache: false,
        type: "POST",
        success: function (data) {
            Handler(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

            if (textStatus == "timeout") {
                showError("错误","此次连接超时！");
            } else if (textStatus == "error") {
                showError("错误","连接服务器失败！");
            } else {
                showError("错误", "操作失败！Ajax请求异常（" + textStatus + "）");
            }
        }
    });
}

/* 
* 获得时间差,时间格式为 年-月-日 小时:分钟:秒 或者 年/月/日 小时：分钟：秒 
* 其中，年月日为全格式，例如 ： 2010-10-12 01:00:00 
* 返回精度为：秒，分，小时，天 
*/
function GetDateDiff(startTime, endTime, diffType) {
    //将xxxx-xx-xx的时间格式，转换为 xxxx/xx/xx的格式 
    startTime = startTime.replace(/-/g, "/");
    endTime = endTime.replace(/-/g, "/");
    //将计算间隔类性字符转换为小写 
    diffType = diffType.toLowerCase();
    var sTime = new Date(startTime); //开始时间 
    var eTime = new Date(endTime); //结束时间 
    //作为除数的数字 
    var divNum = 1;
    switch (diffType) {
        case "second":
            divNum = 1000;
            break;
        case "minute":
            divNum = 1000 * 60;
            break;
        case "hour":
            divNum = 1000 * 3600;
            break;
        case "day":
            divNum = 1000 * 3600 * 24;
            break;
        default:
            break;
    }
    return parseInt((eTime.getTime() - sTime.getTime()) / parseInt(divNum));
}

 
$(function () {
    $.jgrid.extend({
        excelExport: function (o) {
            o = $.extend({
                exptype: "remote",
                url: null,
                oper: "oper",
                tag: "excel",
                exportOptions: {}
            }, o || {});
            return this.each(function () {
                if (!this.grid) { return; }
                var url;
                if (o.exptype == "remote") {
                    var pdata = $.extend({}, this.p.postData);
                    pdata[o.oper] = o.tag;
                    var params = jQuery.param(pdata);
                    if (o.url.indexOf("?") != -1) { url = o.url + "&" + params; }
                    else { url = o.url + "?" + params; }
                    var includeHidden = false;
                    if (o.exportOptions.includeHidden)
                        includeHidden = o.exportOptions.includeHidden;
                    $('#excelExportForm').remove();
                    var grid = $(this); //jQuery对象
                    var vgrid = this; //DOM对象
                    var cm = grid.getGridParam('colModel');
                    var mydata = "[";
                    for (var i = 0; i < cm.length; i++) {
                        var lable = cm[i].label ? cm[i].label : '';
                        var hidden=cm[i].hidden;
                        lable = lable.replace(/<.*?>/g, "").replace(/[^A-Za-z0-9_\u4E00-\u9FA5]/g, "");
                        var name = cm[i].name ? cm[i].name : '';

                        if (lable.length == 0 || hidden) continue;
                        var tmp = "{ id:'" + i + "',lable:'" + lable + "',name:'" + name + "'},";
                        mydata += tmp;
                    }
                    mydata += "]";

                    var myobj = eval(mydata);

                    $("#table_excel_erp").jqGrid("GridUnload");
                    $("#table_excel_erp").jqGrid({
                        data: myobj,
                        datatype: "local",
                        height: 300,
                        rowNum: 100,
                        width: 400,
                        rowList: [10, 20, 30, 50, 100],
                        colNames: ['编号', '标签', '编码'],
                        multiselect: true,
                        colModel: [
       		{ name: 'id', index: 'id', width: 30, sorttype: "int" },
            { name: 'lable', index: 'lable', width: 50 },
            { name: 'name', index: 'name', width: 50,hidden:true }
            ],
                        viewrecords: true,
                        pager: "#div_page_erp",
                        sortname: 'id'
                    });
                    $("#div_excel_erp").dialog({
                        title: "<font color='red'>请勾选需要显示的列，不选默认导出全部列</font>"
        , width: 420
        , modal: true
        , autoOpen: true
        , closeOnEscape: true

        , buttons: {
            "关闭": function () {
                $(this).dialog("close");
            }, "确认": function (btn) {
                var ids = jQuery("#table_excel_erp").jqGrid('getGridParam', "selarrrow");
                if (ids.length == 0) {
                    ids = jQuery("#table_excel_erp").getDataIDs();
                }
                var xml = '<xml>'
        + '<xs:schema id="NewDataSet" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">'
            + '<xs:element name="NewDataSet" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">'
                + '<xs:complexType>'
                    + '<xs:choice minOccurs="0" maxOccurs="unbounded">'
                        + '<xs:element name="Table">'
                            + '<xs:complexType>'
                                + '<xs:sequence>';
                var header;
                for (var i = 0; i < ids.length; i++) {
                    var record = jQuery("#table_excel_erp").jqGrid('getRowData', ids[i]);
                    var header = record.lable ? record.lable : '';
                    header = header.replace(/<.*?>/g, "").replace(/[^A-Za-z0-9_\u4E00-\u9FA5]/g, "");
                    if (header.length == 0) continue;
                    xml += '<xs:element name="' + header + '" type="xs:string" minOccurs="0" />';
                }

                xml = xml
                                + '</xs:sequence>'
                            + '</xs:complexType>'
                        + '</xs:element>'
                    + '</xs:choice>'
                + '</xs:complexType>'
            + '</xs:element>'
        + '</xs:schema>'
        + '<NewDataSet>';

                var selectedKeys = $(grid).getGridParam('selarrrow');

                var selectedRowCount = selectedKeys.length;
                var allKeys = $(grid).getDataIDs();
                var allRowCount = allKeys.length;
                var expandedRowCount = selectedRowCount > 0 ? selectedRowCount : allRowCount;
                var expandedKeys = selectedRowCount > 0 ? selectedKeys : allKeys;

                for (i = 0; i < expandedKeys.length; i++) {
                    xml += '<Table>';
                    var tr = vgrid.rows.namedItem(expandedKeys[i]);
                    var tds = $(tr).children('td');
                    for (var j = 0; j < ids.length; j++) {
                        var record = jQuery("#table_excel_erp").jqGrid('getRowData', ids[j]);
                        var header = record.lable ? record.lable : '';
                        header = header.replace(/<.*?>/g, "").replace(/[^A-Za-z0-9_\u4E00-\u9FA5]/g, "");
                        if (header.length == 0) continue;
                        var index = ids[j];
                        var v = $(tds[index]).text();
                        xml += '<' + header + '>';
                        xml += (v == undefined || $.trim(v).length == 0) ? '' : v.replace(/[<&]/g, "");
                        xml += '</' + header + '>';
                    }
                    xml += '</Table>';
                }
                xml += '</NewDataSet>' + '</xml>';
                ////alert(xml);
                var $inputContent = $('<input>').attr({ name: 'xml', value: Base64.encode(xml) });
                var $form = $('<form>');
                $form.attr({ id: 'excelExportForm', target: '_blank', method: 'post', style: 'display: none', action: o.url }).append($inputContent).appendTo('body');
                $form.submit();

                $("#table_excel_erp").jqGrid("clearGridData");
                $(this).dialog("destroy");
            }
        }
                    });

                }
            });
        }
    });
});

function ArrayUnique(arrayObj) {
    var data = [];
    var a = {};
    for (var i = 0; i < arrayObj.length; i++) {
        if (!a[arrayObj[i]]) {
            a[arrayObj[i]] = true;
            data[data.length] = arrayObj[i];
        }
    }
    return data;
}
//Array.prototype.unique = function () {
//    var data = [];
//    var a = {};
//    for (var i = 0; i < this.length; i++) {
//        if (!a[this[i]]) {
//            a[this[i]] = true;
//            data[data.length] = this[i];
//        }
//    }
//    return data;
//};
