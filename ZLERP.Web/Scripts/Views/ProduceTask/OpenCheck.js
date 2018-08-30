function TaskIndexInit(storeUrl) {
    var mygrid = new MyGrid({
        renderTo: 'TaskGrid'
		    , title: '生产任务列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridWithTitleHeight
		    , storeURL: storeUrl
            , storeCondition: "IsOut=0"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupField: 'IsOut'
            , dialogWidth: 920
            , dialogHeight: 510
		    , initArray: [
   		{ label: ' 任务单号', name: 'ID', index: 'ID', width: 70 }
        , { label: ' 客户配比', name: 'CustMixpropID', index: 'CustMixpropID', width: 70, hidden: true }
        , { label: ' 客户配比代号', name: 'MixpropCode', index: 'MixpropCode', width: 80, sortable: false }
        , { label: ' 混凝土配比', name: 'BetonFormulaName', index: 'BetonFormulaInfo.FormulaName', width: 80 }
        , { label: ' 混凝土配比ID', name: 'BetonFormula', index: 'BetonFormula', width: 80, hidden: true }
        , { label: ' 砂浆配比', name: 'SlurryFormulaName', index: 'SlurryFormulaInfo.FormulaName', width: 80 }
        , { label: '砂浆配比ID', name: 'SlurryFormula', index: 'SlurryFormula', hidden: true }
        , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', align: 'center', width: 70 }
        , { label: ' 出资料否', name: 'IsDatum', index: 'IsDatum', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
   		, { label: ' 已出开盘', name: 'IsOut', index: 'IsOut', align: 'center', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
   		, { label: '计划时间', name: 'NeedDate', index: 'NeedDate', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        , { label: '开盘时间', name: 'OpenTime', index: 'OpenTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        //, { label: ' 是否完成', name: 'IsCompleted', index: 'IsCompleted', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt }
        //, { label: ' 配比下发', name: 'IsFormulaSend', index: 'IsFormulaSend', align: 'center', width: 50, formatter: booleanFmt, unformat: booleanUnFmt }
        , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
   		, { label: ' 项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
   		, { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 80 }
        , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 80 }
   	]
		    , autoLoad: true
            , functions: {
                handleAuto: function (btn) {
                    alert();
                    var keys = mygrid.getSelectedKeys();
                    if (keys.length < 1) {
                        showMessage("请至少选择一条生产任务单");
                    }
                    else {
                        ajaxRequest(btn.data.Url, { ids: keys }, true, function (response) {
                            //showMessage(response.Message);
                            mygrid.reloadGrid();
                        }, null, btn);
                    }
                },
                handlePrintExam: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var selectedRecord = mygrid.getSelectedRecord();
                    if (isEmpty(selectedRecord.BetonFormula)) {
                        showMessage("提示", "该任务单还没下发配比");
                        return false;
                    }
                    mygrid.showWindow({
                        title: '选择报表模版',
                        width: 280,
                        height: "auto",
                        resizable: false,
                        loadFrom: 'SelectTpl',
                        afterLoaded: function () {
                            //加载模版
                            $("#newTpl").empty();
                            $("#onehisTpl").empty();
                            $("#hisTpl>fieldset>legend").contents(":not('b')").remove();
                            $("#onehisTpl").css({ "height": "auto" });
                            //$.post(
                            //    "/TplManage.mvc/Find",
                            //    {
                            //        condition: "TplType='实验室'",
                            //        page: 1,
                            //        rows: 30,
                            //        sidx: "ID",
                            //        sord: "asc"
                            //    },
                            //    function (resp) {
                            //        var data = resp.rows;
                            //        $.each(data, function (i, n) {
                            //            $("<div style='height:20px;line-height:20px;cursor:pointer; margin:1px 0;' tplfilename='" + n.TplFileName + "' tplurl='" + n.TplUrl + "' previewurl='" + n.PreviewUrl + "' parms='" + n.Parms + "'>" + (i + 1) + "、" + n.TplName + "</div>").appendTo($("#newTpl"));
                            //        });
                            //    }
                            //);
                            var path = "/Reports/LabData";
                            $.post(
                                "/ProduceTask.mvc/FindFilesInfoByPath",
                                {
                                    path: path
                                },
                                function (resp) {
                                    var data = $.parseJSON(resp.Data); //取得所有文件的头部信息
                                    $.each(data, function (i, n) {
                                        var fileinfo = $.parseJSON(n.toString());
                                        $("<div style='height:20px;line-height:20px;cursor:pointer; margin:1px 0;' reporttype='" + fileinfo.ReportType + "' stencil='" + fileinfo.Stencil + "' tplfilename='" + fileinfo.FileName + "' tplurl='" + path + "/" + fileinfo.FileName + "' previewurl='" + path + "/" + fileinfo.PreviewFile + "' parms='" + JSON.stringify(fileinfo.Parms) + "'>" + (i + 1) + "、" + fileinfo.TplName + "</div>").appendTo($("#newTpl"));
                                    });
                                }
                            );

                        },
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var selectedElFromNewTpl = $("#SelectTpl>div div").filter(function (index) {
                                    return $(this).attr("current");
                                }).last();

                                if (selectedElFromNewTpl.length == 0) {
                                    showMessage("提示", "请选择一种资料模版或该模版最近保存的数据");
                                    return false;
                                } else {
                                    //打开新模版还是上一次保存的数据？
                                    var isHis = isEmpty(selectedElFromNewTpl.attr("hisdata")); //选择选项是否包含hisdata属性
                                    var tplPath = isHis ? selectedElFromNewTpl.attr("tplurl") : selectedElFromNewTpl.attr("previewurl");
                                    //拼凑参数?与&。json参数必须是selectedRecord里面的字段
                                    var parmsStr = selectedElFromNewTpl.attr("parms");
                                    if (parmsStr != "null") {
                                        var parmsJson = $.parseJSON(parmsStr);
                                        var count = 0;
                                        $.each(parmsJson, function (i, n) {
                                            if (count > 0) {
                                                tplPath += "&" + i + "=" + (isHis ? selectedRecord[n.toString()] : n);
                                            } else {
                                                tplPath += "?" + i + "=" + (isHis ? selectedRecord[n.toString()] : n);
                                            }
                                            count++;
                                        })
                                    }
                                    tplPath += "&reporttype=" + selectedElFromNewTpl.attr("reporttype") + "&stencil=" + selectedElFromNewTpl.attr("stencil");
                                    window.open(tplPath);
                                }
                            }
                        }
                    });
                }
            }
    });
    //新模版
    $("#newTpl>div").die("click");
    $("#newTpl>div").die("mouseover");
    $("#newTpl>div").die("mouseout");
    $("#newTpl>div").live({
        click: function (e) {
            //1、先改变样式
            if ($(this).attr("current")) {
                e.preventDefault();
            } else {
                $("#newTpl>div").removeAttr("current").removeClass("itemhover");
                $(this).attr("current", true).addClass("itemhover");
            }
            //2、找历史数据
            var selectedRecord = mygrid.getSelectedRecord();
            var _previewurl = $(this).attr("previewurl"); //从父选项中取得预览路径
            var _stencil = $(this).attr("stencil");
            var _reporttype = $(this).attr("reporttype");
            $.post(
                "/ReportDatum.mvc/Find",
                {
                    condition: "Stencil='" + _stencil + "' and ObjectID='" + selectedRecord.ID + "'",
                    page: 1,
                    rows: 1,
                    sidx: "ID",
                    sord: "desc"
                },
                function (resp) {
                    if (!$.isEmptyObject(resp.rows)) {
                        $("#onehisTpl").empty();

                        if ($("#hisTpl>fieldset>legend").find("b").siblings("span#hiscount").length > 0) {
                            //若存在则修改数量 
                            $("#hisTpl>fieldset>legend").find("b").siblings("span#hiscount").html(resp.records);
                        } else {
                            $("#hisTpl>fieldset>legend").find("b").after(" －<span id='hiscount' style='color:red;'>" + resp.records + "</span> 条");
                        }
                        $.each(resp.rows, function (i, n) {
                            if (i >= 8) { //如果记录大于等于8条则出现内部滚动条
                                $("#onehisTpl").css({ "height": "169px", "overflow": "auto" });
                            } else {
                                $("#onehisTpl").css({ "height": "auto" });
                            }
                            var lastModifyTime = isEmpty(n.ModifyTime) ? dataTimeFormat(n.BuildTime) : dataTimeFormat(n.ModifyTime);
                            $("<div style='height:20px;line-height:20px;cursor:pointer; margin:1px 0;' id='" + n.ID + "' stencil='" + _stencil + "' reporttype='" + _reporttype + "' previewurl='" + _previewurl + "' parms='{\"hisid\":\"" + n.ID + "\"}' hisdata = 1><span class='ui-icon-red ui-icon-seek-next' style='width:16px;height:16px;vertical-align:middle;margin-top:2px;float:left'></span><span style='width:50%;float:left'>" + lastModifyTime + "</span><span style='width:25%;text-align:right;float:left'>" + n.Builder + "</span><span attid='delHis' id='" + n.ID + "' attv='" + n.ID + "' style='width:13%; text-align:center;float:right; border-left:1px dashed #fff; border-right:1px solid #fff;font-weight:bold; font-size:16px;color:red;' title='删除'>×</span></div>").appendTo($("#onehisTpl"));
                        });
                        $("#onehisTpl").children().first().trigger("click"); //默认选中保存的最后一条
                    } else {
                        $("#hisTpl>fieldset>legend").contents(":not('b')").remove();
                        $("#onehisTpl").css({ "height": "auto" });
                        $("#onehisTpl").html("无历史打印记录");
                    }
                }
            );
        },
        mouseover: function (e) {
            if ($(this).attr("current")) { return; }
            $(this).toggleClass('itemhover');
        },
        mouseout: function (e) {
            if ($(this).attr("current")) { return; }
            $(this).toggleClass('itemhover');
        }
    });


    $("#onehisTpl>div").die("click");
    $("#onehisTpl>div").die("mouseover");
    $("#onehisTpl>div").die("mouseout");
    $("#onehisTpl>div").live({
        click: function (e) {
            //1、先改变样式
            if ($(this).attr("current")) {
                //e.preventDefault();
                $(this).removeAttr("current").removeClass("itemhover");
            } else {
                $("#onehisTpl>div").removeAttr("current").removeClass("itemhover");
                $(this).attr("current", true).addClass("itemhover");
            }
        },
        mouseover: function (e) {
            if ($(this).attr("current")) {
                return;
            } else {
                $(this).addClass('itemhover');
            }

        },
        mouseout: function (e) {
            if ($(this).attr("current")) {
                return;
            }
            else {
                $(this).removeClass("itemhover");
            }
        }
    });
    //删除历史数据
    $("#onehisTpl>div>span[attid='delHis']").die("click");
    $("#onehisTpl>div>span[attid='delHis']").die("mouseover");
    $("#onehisTpl>div>span[attid='delHis']").die("mouseout");
    $("#onehisTpl>div>span[attid='delHis']").live({
        click: function (e) {
            //1、先改变样式
            var id = $(this).attr("attv").toString();
            var _this = $(this);
            $.post(
                "/ReportDatum.mvc/Delete",
                { id: id },
                function (resp) {
                    if (resp.Result) {
                        _this.parent().first().detach(); //删除元素
                        var selectedItemParent = $("#SelectTpl>div div").filter(function (index) {
                            return $(this).attr("current");
                        }).first();
                        selectedItemParent.trigger("click"); //触发父元素点击事件刷新列表
                        //$("#hisTpl>fieldset>legend").find("b").siblings("span#hiscount").html($("#hisTpl>fieldset>legend").find("b").siblings("span#hiscount").html() - 1);
                    }
                }
            );

        },
        mouseover: function (e) {
            $(this).addClass("itemDelhover");

        },
        mouseout: function (e) {
            $(this).removeClass("itemDelhover");
        }
    });
}