function taskAndFormulaInit(opts) {
    var TaskGrid = new MyGrid({
        renderTo: 'TaskGridDiv'
		    , title: opts.taskGridTitle
            , buttons: buttons0
            , autoWidth: true
            , height: gGridHeight / 2
		    , storeURL: opts.storeUrl
            , storeCondition: "IsCompleted=0"
            , sortByField: 'IsFormulaSend,ID'
		    , primaryKey: 'ID'
		    , groupField: 'IsFormulaSend'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>是否下发配比：{0} - {1}</b>'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: false
		    , initArray: [
   		{ label: ' 任务单号', name: 'ID', index: 'ID', width: 70, searchoptions: { sopt: ['cn']} }
   		, { label: '配比生成', name: 'IsFormulaSend', index: 'IsFormulaSend', width: 60, search: false, formatter: booleanFmt, unformat: booleanUnFmt }
   		, { label: '客户编码', name: 'Contract.Customer.ID', index: 'Contract.Customer.ID', type: 'hidden', hidden: true }
   		, { label: '客户名称', name: 'Contract.Customer.CustName', index: 'Contract.Customer.CustName', searchoptions: { sopt: ['cn']} }
   		, { label: '工程名称', name: 'ProjectName', index: 'ProjectName', searchoptions: { sopt: ['cn']} }
   		, { label: '施工部位', name: 'ConsPos', index: 'ConsPos', searchoptions: { sopt: ['cn']} }
        , { label: ' 砼强度', name: 'ConStrength', index: 'ConStrength', width: 60, align: 'center', searchoptions: { sopt: ['cn']} }
        , { label: ' 抗渗等级', name: 'ImpGrade', index: 'ImpGrade', width: 60, align: 'center' }
        , { label: ' 坍落度', name: 'Slump', index: 'Slump', width: 60 }
        , { label: ' 用砼时间', name: 'NeedDate', index: 'NeedDate', width: 120, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        , { label: ' 浇筑方式', name: 'CastMode', index: 'CastMode', width: 70 }
        , { label: ' 计划方量', name: 'PlanCube', index: 'PlanCube', width: 70 }
        , { label: '特性参数', name: 'IdentityValue', index: 'IdentityValue', search: false, sortable: false, width: 60 }
        , { label: ' 其它要求', name: 'OtherDemand', index: 'OtherDemand' }
        , { label: '含砂浆', name: 'IsSlurry', index: 'IsSlurry', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
        , { label: ' 计划车数', name: 'NeedTimes', index: 'NeedTimes', width: 80 }
        , { label: '施工部位类型', name: 'ConsPosType', index: 'ConsPosType', width: 80 }
        , { label: ' 级配', name: 'CarpGrade', index: 'CarpGrade', width: 60 }
        , { label: ' 混凝土配比', name: 'BetonFormula', index: 'BetonFormula', hidden: true }
        , { label: ' 混凝土配比', name: 'BetonFormulaName', index: 'BetonFormula', width: 80 }
        , { label: ' 砂浆配比', name: 'SlurryFormula', index: 'SlurryFormula', hidden: true }
        , { label: ' 砂浆配比', name: 'SlurryFormulaName', index: 'SlurryFormula', width: 80 }
        , { label: ' 客户配比', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
        , { label: ' 客户配比', name: 'MixpropCode', index: 'MixpropCode', width: 80, sortable: false }
   		, { label: ' 项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
        , { label: ' 施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
   		, { label: ' 备注', name: 'Remark', index: 'Remark' }
   		, { label: ' 任务单类型', name: 'TaskType', index: 'TaskType', sortable: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('TType')} }
   		, { label: ' 混凝土类别', name: 'CementType', index: 'CementType' }
        , { label: ' 审核状态', name: 'AuditStatus', index: 'AuditStatus', hidden: true }
   	]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    TaskGrid.reloadGrid();
                },
                handleAuto: function (btn) {
                    //                    alert();
                    var keys = TaskGrid.getSelectedKeys();
                    if (keys.length < 1) {
                        showMessage("请至少选择一条生产任务单");
                    }
                    else {
                        ajaxRequest(btn.data.Url, { ids: keys }, true, function (response) {
                            //showMessage(response.Message);
                            TaskGrid.reloadGrid();
                        }, null, btn);
                    }
                },
                handleRefresh: function (btn) {
                    TaskGrid.refreshGrid('1=1');
                },
                handleSendTo: function () {
                    var key = TaskGrid.getSelectedKey();

                    if (key.length == 0) {
                        showMessage("提示", "请至少选择一条记录");
                    }
                    else {

                        var postUrl = opts.updateProduceTaskUrl;
                        TaskGrid.handleEdit({
                            title: '任务单',
                            width: 1000,
                            height: 600,
                            loadFrom: 'BasicForm',
                            getUrl: opts.getProduceTaskUrl,
                            postUrl: postUrl,
                            //postData:{id:key},
                            afterFormLoaded: function () {
                                TaskGrid.setFormFieldsUnEdit(true);
                                TaskGrid.setFormFieldDisabled('SlurryFormula', false);
                                TaskGrid.setFormFieldDisabled('BetonFormula', false);
                                TaskGrid.setFormFieldDisabled('CustMixpropID', false);
                                TaskGrid.setFormFieldDisabled('ImpGrade', false);
                                TaskGrid.setFormFieldDisabled('ImyGrade', false);
                                TaskGrid.setFormFieldDisabled('ImdGrade', false);
                            },
                            postCallBack: function (response) {
                                if (response.Data) {
                                    showMessage("提示", "下发成功");
                                }
                            }
                        });
                    }
                },
            //今日任务
            handleTodayTask: function () {
                var d = new Date();
                var today = d.format("Y-m-d");
                var condition = "NeedDate between '" + today + " 00:00:00' and '" + today + " 23:59:59'";
                TaskGrid.refreshGrid(condition);
            }
            //明日任务
            , handleTomorrowTask: function () {
                var d = new Date();
                var n = d.getTime() + 1 * 24 * 60 * 60 * 1000;
                d = new Date(n);
                var today = d.format("Y-m-d");
                var condition = "NeedDate between '" + today + " 00:00:00' and '" + today + " 23:59:59'";
                TaskGrid.refreshGrid(condition);
            }
            //所有任务
            , handleAllTask: function () {
                TaskGrid.refreshGrid('1=1');
            },
                handleBatchGen: function () {
                    var taskids = TaskGrid.getSelectedKeys();
                    var records = TaskGrid.getSelectedRecords();
                    var betonselect = TaskGrid.getFormField("BetonFormula");
                    var slurryselect = TaskGrid.getFormField("SlurryFormula");
                    var index = records[0].ConStrength.toUpperCase().indexOf("C");
                    var csValue = records[0].ConStrength.substring(index, index + 3);
                    if (taskids.length < 1) {
                        showMessage("请至少选择一条生产任务单");
                    }
                    else {
                        for (var i = 0; i < records.length; i++) {
                            var index1 = records[i].ConStrength.toUpperCase().indexOf("C");
                            var csValue1 = records[i].ConStrength.substring(index, index + 3);
                            if (csValue1 != csValue) {
                                showMessage('提示', '批量下发砼强度不一致！');
                                return;
                            }
                        }
                        if (confirm('批量下发配比存在风险，确定继续?')) {
                            TaskGrid.showWindow({
                                title: "批量下发配比",
                                width: 400,
                                height: 300,
                                loadFrom: 'yjxfForm',
                                afterLoaded: function () {
                                    if (csValue) {
                                        bindSelectData($("#BetonFormula1"),
                                                opts.listFormulaUrl,
                                                { textField: 'FormulaName',
                                                    valueField: 'ID',
                                                    condition: "ConStrength like '%" + csValue + "%' and FormulaType='FType_C' and Lifecycle > -1"
                                                }, function () { }
                                                );
                                        bindSelectData($("#SlurryFormula1"),
                                                opts.listFormulaUrl,
                                                { textField: 'FormulaName',
                                                    valueField: 'ID',
                                                    condition: "((ConStrength like '%" + csValue + "%' and FormulaType='FType_CS') or FormulaType='FType_S') and Lifecycle > -1"
                                                }, function () { }
                                                );
                                    }
                                },
                                buttons: {
                                    "关闭": function () {
                                        $(this).dialog("close");
                                    },
                                    "确定": function () {
                                        var requestURL = "/ProduceTask.mvc/MutiGenConsMix";
                                        var formulaid = $("#BetonFormula1").val();
                                        var Slurryformulaid = $("#SlurryFormula1").val();
                                        var plist = [];
                                        var plcheckbox = $("#pldiv1 [type='checkbox']");
                                        for (var i = 0; i < plcheckbox.length; i++) {
                                            if (plcheckbox[i].checked == true) {
                                                plist.push(plcheckbox[i].name);
                                            }
                                        }
                                        if (plist.length == 0) {
                                            showMessage("提示", "请先选择机组");
                                            return;
                                        }
                                        if (formulaid.length > 0 || Slurryformulaid.length > 0) {
                                            if (formulaid.length > 0) {
                                                var postParams = { taskids: taskids, formulaid: formulaid, isSlurry: false, plist: plist };
                                                ajaxRequest(requestURL, postParams, true, function (response) {
                                                    TaskGrid.refreshGrid();
                                                    if (response.Result) {
                                                        showMessage("提示", "下发成功");
                                                    }
                                                });
                                            }
                                            if (Slurryformulaid.length > 0) {
                                                var postParams = { taskids: taskids, formulaid: Slurryformulaid, isSlurry: true, plist: plist };
                                                ajaxRequest(requestURL, postParams, true, function (response) {
                                                    TaskGrid.refreshGrid();
                                                    if (response.Result) {
                                                        showMessage("提示", "下发成功");
                                                    }
                                                });
                                            }
                                            $(this).dialog("close"); 
                                        }
                                    }
                                }
                            });
                        };
                    }
                }
            }
    });

    var tmpGrid = new MyGrid({
        renderTo: 'Beton-Grid'
        //, title: '混凝土配比'
            , width: 400
        //, buttons: buttons4
            , height: 300
		    , storeURL: opts.mixpropItemsStoreUrl
		 	, sortByField: 'Silo.OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 550
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: opts.mixpropItemsSaveUrl
            , cellsubmit: 'clientArray'
		    , initArray: [
                { label: ' 施工配比子表编号', name: 'ConsMixpropItemsID', index: 'ConsMixpropItemsID', editrules: { required: true }, hidden: true }
                , { label: ' 配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }
                , { label: ' 筒仓编号', name: 'SiloID', index: 'SiloID', hidden: true }
                , { label: ' 筒仓名称', name: 'SiloName', index: 'SiloName', width: 80, sortable: false }
                , { label: ' 材料名称', name: 'StuffName', index: 'StuffName', width: 100, sortable: false }
                , { label: ' 用量', name: 'Amount', index: 'Amount', formatter: 'number', editrules: { required: true, number: true }, formatoptions: { thousandsSeparator: "," }, editable: true, width: 60 }
                 , { label: ' 排序', name: 'OrderNum', index: 'Silo.OrderNum', width: 60, sortable: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    tmpGrid.reloadGrid();
                }
            }
    });

    $("#BetonFormula").bind('change', function () {
        var requestURL = opts.getConsMixpropUrl;
        var taskid = TaskGrid.getFormField("ID").val();
        var formulaid = this.value;
        var plist = [];
        var plcheckbox = $("#pldiv [type='checkbox']");
        for (var i = 0; i < plcheckbox.length; i++) {
            if (plcheckbox[i].checked == true) {
                plist.push(plcheckbox[i].name);
            }
        }
        if (plist.length == 0) {
            showMessage("提示", "请先选择机组");
            return;
        }
        if (formulaid.length > 0) {
            var postParams = { taskid: taskid, formulaid: formulaid, isSlurry: false, plist: plist };
            ajaxRequest(requestURL, postParams, true, function (response) {
                ConsGrid.refreshGrid("TaskID='" + taskid + "'");
                TaskGrid.reloadGrid();
                if (response.Result) {
                    tmpGrid.refreshGrid("ConsMixpropID='" + response.Data[0].ID + "'");
                    $("#lbhntrz").html(response.Data[0].Weight); //修改容量
                }
            });
        }
    });

    $("#SlurryFormula").bind('change', function () {
        var requestURL = opts.getConsMixpropUrl;
        var taskid = TaskGrid.getFormField("ID").val();
        var formulaid = this.value;
        var plist = [];
        var plcheckbox = $("#pldiv [type='checkbox']");
        for (var i = 0; i < plcheckbox.length; i++) {
            if (plcheckbox[i].checked == true) {
                plist.push(plcheckbox[i].name)
            }
        }
        if (plist.length == 0) {
            showMessage("提示", "请先选择机组");
            return;
        }
        if (formulaid.length > 0) {
            var postParams = { taskid: taskid, formulaid: formulaid, isSlurry: true, plist: plist };
            ajaxRequest(requestURL, postParams, true, function (response) {
                ConsGrid.refreshGrid("TaskID='" + taskid + "'");
                TaskGrid.reloadGrid();
                if (response.Result) {
                    tmpSlurryGrid.refreshGrid("ConsMixpropID='" + response.Data[0].ID + "'");
                    $("#lbsjrz").html(response.Data[0].Weight); //修改容量
                }
            });

        }
    });

    $("#pldiv2 [type='radio']").bind('click', function () {
        var dirtyData0 = tmpGrid.getJqGrid().getChangedCells();
        var dirtyData1 = tmpSlurryGrid.getJqGrid().getChangedCells();
        var dirtydatalen0 = dirtyData0.length;
        var dirtydatalen1 = dirtyData1.length;
        if (dirtydatalen0 > 0 || dirtydatalen1 > 0) {
            //有修改，提示需要保存
            showMessage("提示", "原有生产线还有未保存的记录，请保存后再切换");
            return false;
        }
        var records = ConsGrid.getAllRecords();

        var value = this.value;
        var betonID = "";
        var slurryID = "";
        $("#legend01")[0].innerHTML = value;
        for (var i = 0; i < records.length; i++) {

            if (records[i].ProductLineID == value && records[i].IsSlurry == "false") {
                betonID = records[i].ID;
            }
            if (records[i].ProductLineID == value && records[i].IsSlurry == "true") {
                slurryID = records[i].ID;
            }
        }
        tmpGrid.refreshGrid("ConsMixpropID='" + betonID + "'");
        tmpSlurryGrid.refreshGrid("ConsMixpropID='" + slurryID + "'");
    });


    var tmpSlurryGrid = new MyGrid({
        renderTo: 'Slurry-Grid'
        //, title: '砂浆配比'
            , width: 400
        //, buttons: buttons4
            , height: 300
		    , storeURL: opts.mixpropItemsStoreUrl
		 	, sortByField: 'Silo.OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 550
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: opts.mixpropItemsSaveUrl
            , cellsubmit: 'clientArray'
		    , initArray: [
                { label: ' 施工配比子表编号', name: 'ConsMixpropItemsID', index: 'ConsMixpropItemsID', editrules: { required: true }, hidden: true }
                , { label: ' 配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }
                , { label: ' 筒仓编号', name: 'SiloID', index: 'SiloID', hidden: true }
                , { label: ' 筒仓名称', name: 'SiloName', index: 'SiloName', width: 80, sortable: false }
                , { label: ' 材料名称', name: 'StuffName', index: 'StuffName', width: 100, sortable: false }
                , { label: ' 用量', name: 'Amount', index: 'Amount', formatter: 'number', editrules: { required: true, number: true }, formatoptions: { thousandsSeparator: "," }, editable: true, width: 60 }
                 , { label: ' 排序', name: 'OrderNum', index: 'Silo.OrderNum', width: 60, sortable: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    tmpGrid.reloadGrid();
                }
            }
    });


    var ConsGrid = new MyGrid({
        renderTo: 'ConsGridDiv'
		    , title: opts.consGridTitle
            , autoWidth: true
            , height: 300
            , buttons: buttons3
		    , storeURL: opts.consMixpropStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
            , singleSelect: true
            , groupField: 'ProductLineName'
            , groupingView: { groupColumnShow: [false] }
		    , initArray: [
                  { label: '操作', name: 'act', index: 'act', width: 50, sortable: false, align: "center" }
                , { label: '<font color=red>修改状态？</font>', name: 'SynStatus', index: 'SynStatus', formatter: saveStatusFmt, unformat: booleanUnFmt, width: 90, align: 'center' }
                , { label: ' 审核状态', name: 'AuditStatus', index: 'AuditStatus', width: 60, align: "center", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus'} }
                , { label: ' 砂浆', name: 'IsSlurry', index: 'IsSlurry', formatter: booleanFmt, unformat: booleanUnFmt, width: 40 }
                , { label: ' 任务单号', name: 'TaskID', index: 'TaskID', hidden: true }
                , { label: ' 机组代号', name: 'ProductLineID', index: 'ProductLineID', width: 50 }
                , { label: ' 机组代号', name: 'ProductLineName', index: 'ProductLineName', width: 50 }
                , { label: ' 理论配比', name: 'FormulaName', index: 'FormulaName', width: 80, sortable: false, search: false }
                , { label: ' 砼强度', name: 'ConStrength', index: 'ConStrength', width: 50, align: 'center' }
                , { label: ' 搅拌时间', name: 'MixingTime', index: 'MixingTime', align: 'center', width: 60 }
                , { label: ' 砂率', name: 'SCRate', index: 'SCRate', width: 50, align: 'right' }
                , { label: ' 水灰比', name: 'WCRate', index: 'WCRate', width: 50, align: 'right' }
                , { label: ' 理论容重', name: 'TuneWeight', index: 'Formula.TuneWeight', width: 60, align: 'right', formatter: rzFmt, unformat: rzUnFmt }
                , { label: ' 当前容重', name: 'Weight', index: 'Weight', width: 60, align: 'right' }
                , { label: ' 抗渗等级', name: 'ImpGrade', index: 'ImpGrade', width: 50 }
                , { label: ' 抗折强度', name: 'FlexStrength', index: 'FlexStrength', width: 50 }
                , { label: ' 编号', name: 'ID', index: 'ID', width: 80 }
                , { label: ' 同步更新', name: 'CompletedSyn', index: 'CompletedSyn', sortable: false, formatter: booleanFmt, unformat: booleanUnFmt, width: 50 }
                , { label: ' 所属季节', name: 'SeasonType', index: 'SeasonType', width: 60 }
                , { label: ' 创建人', name: 'Builder', index: 'Builder', width: 60 }
                , { label: ' 创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 审核人', name: 'Auditor', index: 'Auditor', width: 60 }
                , { label: ' 审核时间', name: 'AuditTime', index: 'AuditTime', width: 120, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 备注', name: 'Remark', index: 'Remark' }
		    ]
            , functions: {
                handleReload: function (btn) {
                    ConsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ConsGrid.refreshGrid();
                },
                handleViewAudited: function (btn) {
                    var taskrecord = TaskGrid.getSelectedRecord();
                    if (isEmpty(taskrecord.ID)) {
                        showMessage("提示：", "请选择一条任务单记录");
                        return false;
                    }
                    ConsGrid.refreshGrid("TaskID='" + taskrecord.ID + "' AND AuditStatus = 1");
                },
                handleViewUnAudit: function (btn) {
                    var taskrecord = TaskGrid.getSelectedRecord();
                    if (isEmpty(taskrecord.ID)) {
                        showMessage("提示：", "请选择一条任务单记录");
                        return false;
                    }
                    ConsGrid.refreshGrid("TaskID='" + taskrecord.ID + "' AND AuditStatus != 1");
                }
            }
    });

    var currentTaskid;
    TaskGrid.addListeners("gridComplete", function () {
        if (!isEmpty(currentTaskid)) {
            TaskGrid.setSelection(currentTaskid);
        }
    });


    var currentConsmixid;
    ConsGrid.addListeners("gridComplete", function () {
        var ids = ConsGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteCon('" + ids[i] + "');\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
            de = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleAudit('" + ids[i] + "');\" class='' style='float:left;margin-left:5px;' title='审核所选记录'><span class='ui-icon ui-icon-check'></span>";
            ConsGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce + de });

            var record = ConsGrid.getRecordByKeyValue(ids[i]);
            var VAL_ERROR = opts.VAL_ERROR; //理论容重与实际容重误差值
            if (Math.abs(parseFloat(record.TuneWeight) - parseFloat(record.Weight)) > VAL_ERROR) {
                ConsGrid.getJqGrid().setCell(cl, "Weight", '', { background: 'red', color: 'white' }, '');
            }

        }
        if (currentConsmixid) {
            //ConsGrid.getJqGrid().setSelection(currentConsmixid, false);
            ConsGrid.setSelection(currentConsmixid);
        }

    });

    window.handleDeleteCon = function (id) {
        showConfirm("确认信息", "您确定删除此施工配比？", function () {
            $.post(
                        "/ConsMixprop.mvc/Delete"
                        , { ID: id }
                        , function (data) {
                            if (!data.Result) {
                                showError("出错啦！", data.Message);
                                return false;
                            }
                            ConsGrid.reloadGrid();
                            ItemsGrid.reloadGrid();
                            TaskGrid.reloadGrid();
                        }

                    );
            $(this).dialog("close");
        });
    };

    window.handleAudit = function (id) {
        var selectedRecord = ConsGrid.getRecordByKeyValue(id);
        var _auditstatus = selectedRecord.AuditStatus;
        var oldMixTime = selectedRecord.MixingTime;
        var WinTitle = _auditstatus == 1 ? "取消审核" : "配比审核"; //1审核通过 0未审核 -1审核不通过
        ConsGrid.showWindow({
            title: WinTitle,
            width: 400,
            height: 300,
            loadFrom: '/ConsMixprop.mvc/Index #PBAuditForm form',
            afterLoaded: function () {
                $("#ConsMixprop_ID").val(selectedRecord.ID);
                $("#ConsMixprop_FormulaName").val(selectedRecord.FormulaName);
                $("#ConsMixprop_AuditStatus").val(_auditstatus == 1 ? 0 : 1);
                $("#ConsMixprop_MixingTime").val(oldMixTime);
            },
            buttons:
                { "关闭": function () {
                    $(this).dialog("close");
                }, "确定": function () {
                    $.post(
                        "/ConsMixprop.mvc/Audit"
                        , { ID: id, AuditStatus: $("#ConsMixprop_AuditStatus").val(), AuditInfo: $("#ConsMixprop_AuditInfo").val(), MixingTime: $("#ConsMixprop_MixingTime").val() }
                        , function (data) {
                            if (!data.Result) {
                                showError("出错啦！", data.Message);
                                return false;
                            } else {
                                if (oldMixTime != $("#ConsMixprop_MixingTime").val()) {
                                    showAlert("重要提示", "审核配比时修改了搅拌时间（由 <font color=red>" + oldMixTime + "</font> 改成了 <font color=green>" + $("#ConsMixprop_MixingTime").val() + "</font> ），请重新发送配比至搅拌楼！");
                                }
                                showMessage("提示！", data.Message);
                            }
                            ConsGrid.reloadGrid();
                            ItemsGrid.getJqGrid().jqGrid('clearGridData');
                            //ItemsGrid.reloadGrid();
                        }
                    );
                    $(this).dialog("close");
                }
                }
        });
    };

    var ItemsGrid = new MyGrid({
        renderTo: 'ConItemsGridDiv'
            , autoWidth: true
            , buttons: buttons4
            , height: 300
		    , storeURL: opts.mixpropItemsStoreUrl
		 	  , sortByField: 'Silo.OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: opts.mixpropItemsSaveUrl
            , cellsubmit: 'clientArray'
		    , initArray: [
                { label: ' 施工配比子表编号', name: 'ConsMixpropItemsID', index: 'ConsMixpropItemsID', editrules: { required: true }, hidden: true }
                , { label: ' 配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }
                , { label: ' 筒仓编号', name: 'SiloID', index: 'SiloID', hidden: true }
                , { label: ' 筒仓名称', name: 'SiloName', index: 'SiloName', width: 80, sortable: false }
                , { label: ' 材料名称', name: 'StuffName', index: 'StuffName', width: 100, sortable: false }
                , { label: ' 用量', name: 'Amount', index: 'Amount', formatter: 'number', editrules: { required: true, number: true }, formatoptions: { thousandsSeparator: "," }, editable: true, width: 60 }
                 , { label: ' 排序', name: 'OrderNum', index: 'Silo.OrderNum', width: 60, sortable: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    ItemsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ItemsGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    ItemsGrid.handleAdd({
                        loadFrom: 'ConsItemFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    ItemsGrid.handleEdit({
                        loadFrom: 'ConsItemFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    ItemsGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleModifySave: function (btn) {
                    if (!ConsGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条要保存的施工配比记录！');
                        return;
                    }
                    var selectrecord = ConsGrid.getSelectedRecord();
                    var synstatus = selectrecord.SynStatus;
                    var ID;
                    var dirtyData = ItemsGrid.getJqGrid().getChangedCells();
                    //var text = JSON.stringify(dirtyData);
                    //1、先判断修改状态
                    //2、再看用量是否修改
                    if (synstatus == 1) {
                        showMessage('提示', '当前配比没有做修改！');
                        return;
                    } else {
                        if (dirtyData == null || dirtyData.length < 0 || dirtyData == "") {
                            ID = selectrecord.ID;
                            showAlert('提示', '此次修改只改变了搅拌时间或其他，该配比的材料用量没有做修改！');
                        } else {
                            var dirtyDataArr = new Array();
                            ID = dirtyData[0].ConsMixpropID;
                            var dirtydatalen = dirtyData.length;

                            for (var k = 0; k < dirtydatalen; k++) {
                                dirtyDataArr[k] = new Array();
                                for (var j = 0; j < 1; j++) {
                                    dirtyDataArr[k][0] = dirtyData[k].id;
                                    dirtyDataArr[k][1] = dirtyData[k].Amount;
                                }

                            }

                        }
                        ajaxRequest(
                            "/ConsMixprop.mvc/SendModifiedPBToKZ",
                            {
                                "ID": ID,
                                "SynStatus": 1,
                                "DirtyDataArr": dirtyDataArr
                            },
                            true,
                            function (response) {
                                if (response.Result) {
                                    currentConsmixid = ID;
                                    ConsGrid.refreshGrid();
                                    ItemsGrid.reloadGrid();
                                }
                            }
                        );
                    }

                }

            }
    });
    window.handleModifySave = function (id) {
        if (!ConsGrid.isSelectedOnlyOne()) {
            showMessage('提示', '请选择一条要保存的施工配比记录！');
            return;
        }
        var selectrecord = ConsGrid.getSelectedRecord();
        var synstatus = selectrecord.SynStatus;
        var ID;
        var dirtyData = ItemsGrid.getJqGrid().getChangedCells();
        //var text = JSON.stringify(dirtyData);
        //1、先判断修改状态
        //2、再看用量是否修改
        if (synstatus == 1) {
            showMessage('提示', '当前配比没有做修改！');
            return;
        }
        else {
            if (dirtyData == null || dirtyData.length < 0 || dirtyData == "") {
                ID = selectrecord.ID;
                showAlert('提示', '当前配比的材料用量没有做修改！');
            }
            else {
                var dirtyDataArr = new Array();
                ID = dirtyData[0].ConsMixpropID;
                var dirtydatalen = dirtyData.length;

                for (var k = 0; k < dirtydatalen; k++) {
                    dirtyDataArr[k] = new Array();
                    for (var j = 0; j < 1; j++) {
                        dirtyDataArr[k][0] = dirtyData[k].id;
                        dirtyDataArr[k][1] = dirtyData[k].Amount;
                    }

                }

            }
            ajaxRequest(
                "/ConsMixprop.mvc/SendModifiedPBToKZ",
                {
                    "ID": ID,
                    "SynStatus": 1,
                    "DirtyDataArr": dirtyDataArr
                },
                true,
                function (response) {
                    if (response.Result) {
                        currentConsmixid = ID;
                        ConsGrid.refreshGrid();
                        ItemsGrid.reloadGrid();
                    }
                }
            );
        }
    };
    ItemsGrid.addListeners("afterSubmitCell", function () {
        ConsGrid.reloadGrid();
    });
    var beforeEditValue = 0;
    ItemsGrid.addListeners("beforeEditCell", function (rid, record, cellname, value) {
        beforeEditValue = value;
    });
    ItemsGrid.addListeners("afterSaveCell", function (rid, record, cellname, value) {
        //材料修改差量
        var clValue = value - beforeEditValue;
        ItemsGrid.getJqGrid().setCell(rid, cellname, '', { background: 'red', color: 'white' }, '');
        //容重原始值
        var preWeight = ConsGrid.getJqGrid().getCell(record.ConsMixpropID, 'Weight');
        ConsGrid.getJqGrid().setCell(record.ConsMixpropID, 'Weight', parseFloat(preWeight) + parseFloat(clValue));
        ConsGrid.getJqGrid().setCell(record.ConsMixpropID, 'SynStatus', 0);
    });

    TaskGrid.addListeners('rowdblclick', function (id, record, selBool) {
        currentTaskid = id;
        if (record.AuditStatus != 1) {
            showError("出错啦！", "该任务单未审核或审核未通过，不允许下发配比！");
            return false;
        }
        var postUrl = opts.updateProduceTaskUrl;
        TaskGrid.handleEdit({
            title: opts.taskFormTitle,
            width: 1000,
            height: 680,
            loadFrom: 'BasicForm',
            getUrl: opts.getProduceTaskUrl, //加载
            postUrl: postUrl, //提交
            //postData:{id:key},
            closeDialog: true,
            afterFormLoaded: function () {
                $("#formula-tabs").tabs();
                //var taskrecord = TaskGrid.getSelectedRecord();
                //过滤混凝土理论配比
                var _csValue = TaskGrid.getFormField("ConStrength").val();
                var index = _csValue.toUpperCase().indexOf("C");
                var csValue = _csValue.substring(index, index + 3);
                var taskRecord = TaskGrid.getSelectedRecord();
                var taskid = TaskGrid.getSelectedKey();
                var betonselect = TaskGrid.getFormField("BetonFormula");
                var slurryselect = TaskGrid.getFormField("SlurryFormula");
                var cusselect = TaskGrid.getFormField("CustMixpropID");
                var rateselect = TaskGrid.getFormField("Field1");
                var radiovalue = $("#legend01")[0].innerHTML;
                if (csValue) {
                    bindSelectData(TaskGrid.getFormField("BetonFormula"),
                                        opts.listFormulaUrl,
                                        { textField: 'FormulaName',
                                            valueField: 'ID',
                                            condition: "ConStrength like '%" + csValue + "%' and FormulaType='FType_C' and Lifecycle > -1"
                                        }, function () {
                                            if (!isEmpty(taskRecord.BetonFormula)) {
                                                betonselect.val(taskRecord.BetonFormula);
                                                var records = ConsGrid.getAllRecords();

                                                for (var i = 0; i < records.length; i++) {
                                                    if (records[i].ProductLineID == radiovalue && records[i].IsSlurry == "false") {

                                                        tmpGrid.refreshGrid("ConsMixpropID='" + records[i].ID + "'");
                                                    }
                                                }
                                            }
                                            else {
                                                tmpGrid.getJqGrid().jqGrid("clearGridData");
                                            }
                                        }
                                    );
                    bindSelectData(TaskGrid.getFormField("SlurryFormula"),
                                        opts.listFormulaUrl,
                                        { textField: 'FormulaName',
                                            valueField: 'ID',
                                            condition: "((ConStrength like '%" + csValue + "%' and FormulaType='FType_CS') or FormulaType='FType_S') and Lifecycle > -1"
                                        }, function () {

                                            if (!isEmpty(taskRecord.SlurryFormula)) {
                                                slurryselect.val(taskRecord.SlurryFormula);
                                                var records = ConsGrid.getAllRecords();
                                                for (var i = 0; i < records.length; i++) {
                                                    if (records[i].ProductLineID == radiovalue && records[i].IsSlurry == "true") {

                                                        tmpSlurryGrid.refreshGrid("ConsMixpropID='" + records[i].ID + "'");
                                                    }
                                                }
                                            }
                                            else {
                                                tmpSlurryGrid.getJqGrid().jqGrid("clearGridData");
                                            }
                                        }
                                    );
                    bindSelectData(TaskGrid.getFormField("CustMixpropID"),
                                        opts.listMixpropUrl,
                                        { textField: 'MixpropCode',
                                            valueField: 'ID',
                                            condition: "ConStrength='" + csValue + "'"
                                        }, function () {
                                            if (!isEmpty(taskRecord.CustMixpropID)) {
                                                cusselect.val(taskRecord.CustMixpropID);
                                            }
                                        }
                                    );

                }
                else {
                    TaskGrid.getFormField("BetonFormula").empty();
                    TaskGrid.getFormField("CustMixpropID").empty();
                }




                //生产线默认选中
                //                var plcheckbox = $("#pldiv [type='checkbox']");
                //                for (var i = 0; i < plcheckbox.length; i++) {
                //                    $("#pldiv [type='checkbox']")[i].checked = true;
                //                }

                betonselect.bind('change', function (event) {
                    var formulaid = betonselect.val();
                    //BetonGrid.refreshGrid("FormulaID='" + formulaid + "'  and StuffAmount>0");
                });
                slurryselect.bind('change', function (event) {
                    var sformulaid = slurryselect.val();
                    //SlurryGrid.refreshGrid("FormulaID='" + sformulaid + "' and StuffAmount>0");
                });
                rateselect.bind('change', function (event) {
                    var ratesetid = $(this).val();
                    if (ratesetid.length == 0) {
                        TaskGrid.getFormField("SIWRate").val(0);
                        TaskGrid.getFormField("SIRRate").val(0);
                        TaskGrid.getFormField("RIWRate").val(0);
                        TaskGrid.getFormField("RIWRate1").val(0);
                        return;
                    }
                    ajaxRequest(opts.getRateSet, { id: ratesetid }, false, function (response) {
                        if (response.Result && response.Data) {
                            TaskGrid.getFormField("SIWRate").val(response.Data.SIWRate);
                            TaskGrid.getFormField("SIRRate").val(response.Data.SIRRate);
                            TaskGrid.getFormField("RIWRate").val(response.Data.RIWRate);
                            TaskGrid.getFormField("RIWRate1").val(response.Data.RIWRate1);
                        }
                    });

                });

            },
            beforeSubmit: function () {
                var plist = $("#pldiv [type='checkbox']:checked").map(function () { return $(this).attr("name"); }).get();
                if (plist.length == 0) {
                    showMessage("提示", "请先选择机组");
                    return false;
                }
                $("#pldiv #plist").val(plist);
                var records = ConsGrid.getAllRecords();
                var currentpl = $("#legend01")[0].innerHTML;
                var currentbetonrecord = null;
                var currentsluryrecord = null;
                var betonid = "";
                var slurryid = "";
                for (var i = 0; i < records.length; i++) {
                    if (records[i].ProductLineID == currentpl && records[i].IsSlurry == "true") {
                        slurryid = records[i].ID;
                        currentsluryrecord = records[i];
                    }
                    if (records[i].ProductLineID == currentpl && records[i].IsSlurry == "false") {
                        betonid = records[i].ID;
                        currentbetonrecord = records[i];
                    }
                }
                if (currentbetonrecord != null) {
                    var synstatus = currentbetonrecord.SynStatus;
                    var ID;
                    var dirtyData = tmpGrid.getJqGrid().getChangedCells();
                    //var text = JSON.stringify(dirtyData);
                    //1、先判断修改状态
                    //2、再看用量是否修改
                    if (dirtyData.length > 0) {
                        var dirtyDataArr = new Array();
                        ID = dirtyData[0].ConsMixpropID;
                        var dirtydatalen = dirtyData.length;

                        for (var k = 0; k < dirtydatalen; k++) {
                            dirtyDataArr[k] = new Array();
                            for (var j = 0; j < 1; j++) {
                                dirtyDataArr[k][0] = dirtyData[k].id;
                                dirtyDataArr[k][1] = dirtyData[k].Amount;
                            }

                        }
                        if (ID != null) {
                            ajaxRequest(
                            "/ConsMixprop.mvc/SendModifiedPBToKZ",
                            {
                                "ID": ID,
                                "SynStatus": 1,
                                "DirtyDataArr": dirtyDataArr
                            },
                            true,
                            function (response) {
                                if (response.Result) {
                                    currentConsmixid = ID;
                                    ConsGrid.refreshGrid();
                                    tmpGrid.refreshGrid("ConsMixpropID='" + betonid + "'");
                                    ItemsGrid.refreshGrid("ConsMixpropID='" + betonid + "'");
                                }
                            }
                        );
                        }
                        return false;
                    }

                }

                if (currentsluryrecord != null) {
                    var synstatus = currentsluryrecord.SynStatus;
                    var ID = currentsluryrecord.ID;
                    var dirtyData = tmpSlurryGrid.getJqGrid().getChangedCells();
                    //var text = JSON.stringify(dirtyData);
                    //1、先判断修改状态
                    //2、再看用量是否修改
                    if (dirtyData.length > 0) {
                        var dirtyDataArr = new Array();
                        ID = dirtyData[0].ConsMixpropID;
                        var dirtydatalen = dirtyData.length;

                        for (var k = 0; k < dirtydatalen; k++) {
                            dirtyDataArr[k] = new Array();
                            for (var j = 0; j < 1; j++) {
                                dirtyDataArr[k][0] = dirtyData[k].id;
                                dirtyDataArr[k][1] = dirtyData[k].Amount;
                            }
                        }
                        if (ID != null) {
                            ajaxRequest(
                            "/ConsMixprop.mvc/SendModifiedPBToKZ",
                            {
                                "ID": ID,
                                "SynStatus": 1,
                                "DirtyDataArr": dirtyDataArr
                            },
                            true,
                            function (response) {
                                if (response.Result) {
                                    currentConsmixid = ID;
                                    ConsGrid.refreshGrid();
                                    tmpSlurryGrid.refreshGrid("ConsMixpropID='" + slurryid + "'");
                                    ItemsGrid.refreshGrid("ConsMixpropID='" + slurryid + "'");
                                }
                            }
                        );
                        }
                        return false;
                    }
                }
                return true;
            },
            postCallBack: function (response) {
                if (response.Result) {
                    ConsGrid.refreshGrid("TaskID='" + id + "'");
                }
            }
        }); //TaskGrid.handleEdit
    });


    TaskGrid.addListeners('rowclick', function (id, record, selBool) {
        ConsGrid.refreshGrid("TaskID='" + id + "'");
        ItemsGrid.getJqGrid().jqGrid('clearGridData');
        currentTaskid = id;
    });

    ConsGrid.addListeners('rowclick', function (id, record, selBool) {
        ItemsGrid.getJqGrid().setCaption("子表信息列表：" + record.ID + "&nbsp;<font color='#2E6E9E'>－</font>&nbsp;" + record.FormulaName + "&nbsp;<font color='#2E6E9E'>－</font>&nbsp;" + record.ConStrength);
        var dirtyData = ItemsGrid.getJqGrid().getChangedCells();
        var dirtydatalen = dirtyData.length;
        if (dirtydatalen > 0) {
            //有修改，提示是否保存
            var _ConsMixpropID = dirtyData[0].ConsMixpropID;
            showConfirm("确认信息",
                        "配比" + _ConsMixpropID + "有修改，是否提交修改至搅拌楼？",
                        function () {//是按钮事件
                            var dirtyDataArr = new Array();
                            for (var k = 0; k < dirtydatalen; k++) {
                                dirtyDataArr[k] = new Array();
                                for (var j = 0; j < 1; j++) {
                                    dirtyDataArr[k][0] = dirtyData[k].id;
                                    dirtyDataArr[k][1] = dirtyData[k].Amount;
                                }

                            }
                            ajaxRequest(
                                "/ConsMixprop.mvc/SendModifiedPBToKZ",
                                {
                                    "ID": _ConsMixpropID,
                                    "SynStatus": 1,
                                    "DirtyDataArr": dirtyDataArr
                                },
                                true,
                                function (response) {
                                    if (response.Result) {
                                        currentConsmixid = _ConsMixpropID;
                                        ItemsGrid.reloadGrid();
                                        ConsGrid.refreshGrid();

                                    }
                                }
                            );
                            $(this).dialog("close");
                        },
                        function () {//否按钮事件
                            currentConsmixid = _ConsMixpropID;
                            ConsGrid.refreshGrid();
                            ItemsGrid.refreshGrid("ConsMixpropID='" + _ConsMixpropID + "'");
                            var cellEditable = true;
                            if (gSysConfig.IsAllowModifyForConsMixprop == "true") {
                                cellEditable = true;
                            } else {
                                if (ConsGrid.getSelectedRecord().AuditStatus == 1) {
                                    cellEditable = false;
                                }
                            }
                            ItemsGrid.setGridParam({ cellEdit: cellEditable }, true);

                        }
            );
        } else {
            //没有修改
            ItemsGrid.refreshGrid("ConsMixpropID='" + id + "'");
            var cellEditable = true;
            if (gSysConfig.IsAllowModifyForConsMixprop == "true") {
                cellEditable = true;
            } else {
                if (ConsGrid.getSelectedRecord().AuditStatus == 1) {
                    cellEditable = false;
                }
            }
            ItemsGrid.setGridParam({ cellEdit: cellEditable }, false);
        }


    });

    var beforeEditValueOfslurry = 0;
    tmpSlurryGrid.addListeners("beforeEditCell", function (rid, record, cellname, value) {
        beforeEditValueOfslurry = value;
    });
    tmpSlurryGrid.addListeners("afterSaveCell", function (rid, record, cellname, value) {
        //材料修改差量
        var clValue = value - beforeEditValueOfslurry;
        tmpSlurryGrid.getJqGrid().setCell(rid, cellname, '', { background: 'red', color: 'white' }, '');
        //容重原始值
        var preWeight = parseFloat($("#lbsjrz").html());
        $("#lbsjrz").html(parseFloat(preWeight) + parseFloat(clValue));
    });

    tmpSlurryGrid.addListeners('afterSubmitCell', function (response) {
        if (!response.Result) {
            showMessage("警告", response.Message);
        }
        else
        { return; }
    });

    tmpSlurryGrid.addListeners("gridComplete", function () {
        var tmp = 0;
        var ids = tmpSlurryGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var record = tmpSlurryGrid.getRecordByKeyValue(ids[i]);
            tmp += parseFloat(record.Amount);
        }
        $("#lbsjrz").html(tmp);
    });



    var beforeEditValueOftmp = 0;
    tmpGrid.addListeners("beforeEditCell", function (rid, record, cellname, value) {
        beforeEditValueOftmp = value;
    });
    tmpGrid.addListeners("afterSaveCell", function (rid, record, cellname, value) {
        //材料修改差量
        var clValue = value - beforeEditValueOftmp;
        tmpGrid.getJqGrid().setCell(rid, cellname, '', { background: 'red', color: 'white' }, '');
        //容重原始值
        var preWeight = parseFloat($("#lbhntrz").html());
        $("#lbhntrz").html(parseFloat(preWeight) + parseFloat(clValue));
    });

    tmpGrid.addListeners('afterSubmitCell', function (response) {
        if (!response.Result) {
            showMessage("警告", response.Message);
        }
        else
        { return; }
    });

    tmpGrid.addListeners("gridComplete", function () {
        var tmp = 0;
        var ids = tmpGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var record = tmpGrid.getRecordByKeyValue(ids[i]);
            tmp += parseFloat(record.Amount);
        }
        $("#lbhntrz").html(tmp);
    });

}