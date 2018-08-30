//生产调度

function dispatchListIndexInit(options) {
 
    ///=========================地图相关操作 begin ========================///
    var MapAuthorization = false;           //判断是否有查看地图的权限
    $.each(subMenus, function (i, n) {
        if (n.ID == "4112") {
            MapAuthorization = true;
            return false;
        }
    });

    function mapformatter(cellvalue, options, rowObject) {

        if (MapAuthorization && gSysConfig.Gps == 'true') {
            return '<a href="javascript:void(0)"  style="color:green" data-taskmap-id ="' + rowObject.ID + '">' + cellvalue + '</a> ';
        }
        else {
            return cellvalue;
        }
    }


    $('a[data-taskmap-id]').die('click');

    $('a[data-taskmap-id]').live('click', function (e) {

        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;

        var data = scrwGrid.getRecordByKeyValue($(this).attr('data-taskmap-id'));
        var RefreshCarLocation = null;
        $('#div_map_dialog').empty();
        $('#div_map_dialog').dialog({ modal: true, autoOpen: true, bgiframe: false, resizable: false, width: 950, height: 540, title: data.ProjectName, open: function () {
            var mapDialog = new abcMap();        //绘制地图
            var opts = {
                render: "div_map_dialog",
                cLng: companyinfo.lon,
                cLat: companyinfo.lat,
                level: 11
            };
            mapDialog.init(opts);

            refreshTaskCarLocation(data, mapDialog);        //先执行一次，定义了时钟不会马上执行

            DrawTaskInMap(data, mapDialog);
            RefreshCarLocation = window.setInterval(function () {
                if ($('#div_map_dialog').dialog("isOpen")) {
                    refreshTaskCarLocation(data, mapDialog);
                }
            }, 1000 * gSysConfig.DataRefTime);
        },
            close: function () {
                $('#div_map_dialog').empty();
                clearInterval(RefreshCarLocation);
            }
        });

    });

    function refreshTaskCarLocation(data, map) {
        ajaxRequest(options.carInMapUrl, { taskid: data.ID }, false, function (response) {
            if (response) {
                rData = response;
                var car_arr = [];

                $.each(rData, function (i, n) {

                    var content = "<table class='tb_cr_img'><tr><td><img src='" + getCarIcon(n) + "'/></td></tr><tr><td><span> " + n.Id + " </span></td></tr></table>";
                    car_arr.push({ lng: n.Longtidue, lat: n.Latitude, content: content, id: 'car' + n.Id });
                });

                if (car_arr.length > 0 && !isEmpty(map))
                    map.addPoints(car_arr, false);
            }
        });
    }

    //读取工地的位置信息
    function DrawTaskInMap(data, map) {

        ajaxRequest('Project.mvc/Get', { id: data.ProjectID }, false, function (response) {
            if (response) {

                rData = response.Data;
                if (!isEmpty(rData) && !isEmpty(map)) {

                    var content = "<table class='tb_pj_img'><tr><td><img src='" + iconArr.yardIcon + "' /></td></tr><tr><td><span>" + rData.ProjectName + "</span></td></table>";

                    map.addPoints([{ lng: rData.Longitude, lat: rData.Latitude, offsetx: 55, content: content, id: 'prj' + rData.ID}], true);
                    map.addCircle("pjrange" + rData.ID, rData.Longitude, rData.Latitude, rData.PlaceRange);
                }

            }
        });
    }


    function mapUnformatter(cellvalue, options, rowObject) {
        return cellvalue;
    }

    ///=========================地图相关操作end ========================///

    //设置字段必填颜色提示
    $('#DispatchList_ProductLineID', '#scdjFormDiv').addClass('text requiredval valid'); 
    $('#ShippingDocument_ProjectName', '#scdjFormDiv').addClass('text requiredval valid');
    $('#ShippingDocument_CarID', '#scdjFormDiv').addClass('text requiredval valid');
    $('#DeliveryTime', '#scdjFormDiv').addClass('text requiredval valid');
    var scrwGrid = new MyGrid({
        renderTo: 'task-grid'
        //, title: '生产任务列表'
            , height: 320
            , autoWidth: true
            , buttons: buttons0
		    , storeURL: options.getRangeTimeTaskUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , toolbarSearch: true
            , buttonRenderTo: 'operate-buttons'
		    , setGridPageSize: 200
		    , showPageBar: true
            , singleSelect: true
            , groupField: 'IsFormulaSend'
            , groupingView: { groupSummary: [true] } 
           // , groupingView: { groupColumnShow: [false], groupSummary: [true], groupText: ['<b>任务数 - {1}</b>'] }
           //  , groupField: 'IsFormulaSend'
		    , initArray: [
            { label: '操作', name: 'act', index: 'act', width: 90, sortable: false, align: "center", width: 40, search: false }
        , { label: '配比?', name: 'IsFormulaSend', index: 'IsFormulaSend', sortable: false, align: 'center', width: 30, formatter: booleanFmt, unformat: booleanUnFmt, search: false, hidden: true }
   		, { label: '任务单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn']} }
        , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', width: 150, searchoptions: { sopt: ['cn']} }
        , { name: 'ProjectID', index: 'ProjectID', hidden: true }
   		, { label: '工程名称', name: 'ProjectName', index: 'ProjectName', formatter: mapformatter, unformat: mapUnformatter, width: 200, searchoptions: { sopt: ['cn']} }
   		, { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 50, align: 'center' }
   		, { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
   		, { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 100 }
   		, { label: '计划', name: 'PlanCube', index: 'PlanCube', width: 40, align: 'center', search: false, formatter: FixedTwoNumber, summaryType: 'sum', summaryTpl: '{0}' }
   		, { label: '已供', name: 'ProvidedCube', index: 'ProvidedCube', width: 40, align: 'right', sortable: false, search: false, summaryType: 'sum', formatter: FixedTwoNumber, summaryTpl: '{0}' }
        , { label: '代生产', name: 'InsteadCube', index: 'InsteadCube', width: 40, align: 'right', sortable: false, search: false, summaryType: 'sum', formatter: FixedTwoNumber, summaryTpl: '{0}' }
        , { label: '用砼时间', name: 'NeedDate', index: 'NeedDate', width: 120, formatter: 'datetime', search: false }
		, { label: '开盘时间', name: 'OpenTime', index: 'OpenTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
   		, { label: '已供车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', sortable: false, search: false }
        , { label: '含砂浆', name: 'IsSlurry', index: 'IsSlurry', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, search: false }
   		, { label: '泵车类型', name: 'PumpType', index: 'PumpType', width: 50, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PumpType' }, search: false, hidden: true }
   		, { label: '坍落度', name: 'Slump', index: 'Slump', width: 70, search: false }
   		, { label: '其它要求', name: 'OtherDemand', index: 'OtherDemand', width: 100, search: false }
   		, { label: '任务单类型', name: 'TaskType', index: 'TaskType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TType' }, search: false }
   		, { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80, search: false }
   		, { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 150, search: false }
   		, { label: '客户编码', name: 'CustID', index: 'CustID', search: false, sortable: false, hidden: true }
   		, { label: '级配', name: 'CarpGrade', index: 'CarpGrade', edittype: 'select', width: 60, editoptions: { value: "I级配:I级配;II级配:II级配" }, search: false }
   		, { label: '异常信息', name: 'Exception', index: 'Exception', search: false }
   		, { label: '备注', name: 'Remark', index: 'Remark', search: false }
   		, { label: '砂浆配比', name: 'SlurryFormulaName', index: 'SlurryFormulaName', search: false }
   		, { label: '混凝土类别', name: 'CementType', index: 'CementType', width: 80, search: false }
   		, { label: '混凝土配比', name: 'BetonFormulaName', index: 'BetonFormulaName', search: false }
   		, { label: '配比是否下发', name: 'FormulaStatus', index: 'FormulaStatus', hidden: true }
        , { label: '是否含润泵砂浆', name: 'IsLubricatingSlurry', index: 'IsLubricatingSlurry', hidden: true }
        , { label: '计划车数', name: 'NeedTimes', index: 'NeedTimes', width: 55, align: 'right', search: false, hidden: true }
   	        ]
		    , autoLoad: true
            , functions: {
                handleAdd: function (btn) {
                    SaveDispatchPrint(btn);
                    //scrwGrid.refreshGrid();
                }
                , handleAddPreView: function (btn) {
                    SaveDispatchPrint(btn, 'preview');
                    //scrwGrid.refreshGrid();
                }
                , handleAddPrint: function (btn) {
                    SaveDispatchPrint(btn, 'print');
                    //scrwGrid.refreshGrid();
                }
                , printShippingDocument: function (btn) {

                    scrwGrid.handleAdd({
                        loadFrom: options.newDocUrl,
                        width: 700,
                        height: 420,
                        btn: btn,
                        postUrl: options.createDocUrl,
                        postCallBack: function (response) {
                            if (response.Result) {
                                var docid = response.Data;
                                //设置发货单模板
                                var shipTemp = $('#TemplateID').val();
                                if (!isEmpty(shipTemp)) {
                                    setShipDocTemplate(shipTemp);
                                }
                                printShippingDoc('print', docid);
                            }
                        }
                    });

                    scrwGrid.setFormId('scdj-form');

                }
                , printShippingDocument1: function (btn) {

                    scrwGrid.handleAdd({
                        loadFrom: options.newDocUrl1,
                        width: 700,
                        height: 420,
                        btn: btn,
                        postUrl: options.createDocUrl,
                        postCallBack: function (response) {
                            if (response.Result) {
                                var docid = response.Data;
                                //设置发货单模板
                                var shipTemp = $('#TemplateID').val();
                                if (!isEmpty(shipTemp)) {
                                    setShipDocTemplate(shipTemp);
                                }
                                printShippingDoc('print', docid);
                            }
                        }
                    });

                    scrwGrid.setFormId('scdj-form');

                }
                , handleToday: function (btn) {
                    //  var d = new Date();
                    // var today = d.format("Y-m-d");
                    // condition = "NeedDate between '" + today + " 00:00:00' and '" + today + " 23:59:59'";
                    showUpdateTime = false;
                    scrwGrid.setGridParam({ url: options.getRangeTimeTaskUrl });
                }
                , handleLastDay: function (btn) {
                    var d = new Date();
                    var today = d.format("Y-m-d");
                    var uom = GetDateBySpan(1);
                    condition2 = "NeedDate between '" + uom + " 00:00:00' and '" + uom + " 23:59:59'";
                    scrwGrid.setGridParam({ url: 'ProduceTask.mvc/Find', postData: { condition: condition2} });

                    //scrwGrid.refreshGrid(condition2);
                }
                , handleAllUnCompleted: function (btn) {
                    showUpdateTime = true;
                    scrwGrid.setGridParam({ url: options.getNotRangeTimeTaskUrl });
                }
		, handleIsOpen: function (btn) {
		    var d = new Date();
		    var today = d.format("Y-m-d");
		    var startdate = today + " 08:00:00";
		    var enddate = GetDateBySpan(-1) + " 08:00:00";
		    if (!compareTime(d.format("Y-m-d H:i:s").toString(), startdate.toString())) {
		        startdate = GetDateBySpan(1) + " 08:00:00";
		        enddate = today + " 08:00:00";
		    }
		    condition = "(NeedDate between '" + startdate + "' and '" + enddate + "') and OpenTime <> '' and AuditStatus = 1 AND IsCompleted = 0 AND FormulaStatus > 0";
		    scrwGrid.setGridParam({ url: 'ProduceTask.mvc/Find', postData: { condition: condition} });
		}
		, handleUnOpen: function (btn) {
		    var d = new Date();
		    var today = d.format("Y-m-d");
		    var startdate = today + " 08:00:00";
		    var enddate = GetDateBySpan(-1) + " 08:00:00";
		    if (!compareTime(d.format("Y-m-d H:i:s").toString(), startdate.toString())) {
		        startdate = GetDateBySpan(1) + " 08:00:00";
		        enddate = today + " 08:00:00";
		    }
		    condition = "(NeedDate between '" + startdate + "' and '" + enddate + "') and (OpenTime = '' or OpenTime is null) and AuditStatus = 1 AND IsCompleted = 0 AND FormulaStatus > 0";
		    scrwGrid.setGridParam({ url: 'ProduceTask.mvc/Find', postData: { condition: condition} });
		}
		//代我司生产
        , handleInsteadProduct: function (btn) {
            if (!scrwGrid.isSelectedOnlyOne()) {
                showMessage("提示", "请选择一条记录进行操作！");
                return;
            }
            var selectrecord = scrwGrid.getSelectedRecord();
            scrwGrid.handleAdd({
                btn: btn,
                loadFrom: "InsteadProductForm",
                width: 420,
                height: 300,
                afterFormLoaded: function () {
                    scrwGrid.setFormFieldValue("InsteadProduct.TaskID", selectrecord.ID);
                }

            });
            scrwGrid.setFormId('scdj-form');
        }
            }
    });


    var scrwGrid1 = new MyGrid({
        renderTo: 'task-grid1'
            , height: $('#tabs').height() - 100
            , width: $('#tabs').width() - 15
		    , storeURL: options.getNotRangeTimeTaskUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , toolbarSearch: true
		    , setGridPageSize: 200
		    , showPageBar: true
            , singleSelect: true
            , groupingView: { groupColumnShow: [false], groupSummary: [true], groupText: ['<b>任务数 - {1}</b>'] }
            , groupField: 'IsFormulaSend'
		    , initArray: [
            { label: '更新', name: 'act', index: 'act', width: 90, sortable: false, align: "left", width: 40, search: false }
        , { label: '完成', name: 'act1', index: 'act1', width: 90, sortable: false, align: "left", width: 40, search: false }
        , { label: '配比?', name: 'IsFormulaSend', index: 'IsFormulaSend', sortable: false, align: 'center', width: 30, formatter: booleanFmt, unformat: booleanUnFmt, search: false, hidden: true }
   		, { label: '任务单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn']} }
        , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', width: 150, searchoptions: { sopt: ['cn']} }
   		, { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 200, searchoptions: { sopt: ['cn']} }
   		, { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 50, align: 'center' }
   		, { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
   		, { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 100 }
   		, { label: '计划', name: 'PlanCube', index: 'PlanCube', width: 40, align: 'center', search: false, formatter: FixedTwoNumber, summaryType: 'sum', summaryTpl: '{0}' }
   		, { label: '已供', name: 'ProvidedCube', index: 'ProvidedCube', width: 40, align: 'right', sortable: false, search: false, summaryType: 'sum', formatter: FixedTwoNumber, summaryTpl: '{0}' }
        , { label: '计划开始时间', name: 'NeedDate', index: 'NeedDate', width: 120, formatter: 'datetime', search: false }
		, { label: '计划结束时间', name: 'NeedDateEnd', index: 'NeedDateEnd', formatter: 'datetime', width: 130, search: false }
		, { label: '开盘时间', name: 'OpenTime', index: 'OpenTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
   		, { label: '已供车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', sortable: false, search: false }
        , { label: '含砂浆', name: 'IsSlurry', index: 'IsSlurry', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, search: false }
   		, { label: '泵车类型', name: 'PumpType', index: 'PumpType', width: 50, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PumpType' }, search: false, hidden: true }
   		, { label: '坍落度', name: 'Slump', index: 'Slump', width: 70, search: false }
   		, { label: '其它要求', name: 'OtherDemand', index: 'OtherDemand', width: 100, search: false }
   		, { label: '任务单类型', name: 'TaskType', index: 'TaskType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TType' }, search: false }
   		, { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80, search: false }
   		, { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 150, search: false }
   		, { label: '客户编码', name: 'CustID', index: 'CustID', search: false, sortable: false, hidden: true }
   		, { label: '级配', name: 'CarpGrade', index: 'CarpGrade', edittype: 'select', width: 60, editoptions: { value: "I级配:I级配;II级配:II级配" }, search: false }
   		, { label: '备注', name: 'Remark', index: 'Remark', search: false }
   		, { label: '砂浆配比', name: 'SlurryFormulaName', index: 'SlurryFormulaName', search: false }
   		, { label: '混凝土类别', name: 'CementType', index: 'CementType', width: 80, search: false }
   		, { label: '混凝土配比', name: 'BetonFormulaName', index: 'BetonFormulaName', search: false }
   		, { label: '配比是否下发', name: 'FormulaStatus', index: 'FormulaStatus', hidden: true }
        , { label: '是否含润泵砂浆', name: 'IsLubricatingSlurry', index: 'IsLubricatingSlurry', hidden: true }
        , { label: '计划车数', name: 'NeedTimes', index: 'NeedTimes', width: 55, align: 'right', search: false, hidden: true }
   	        ]
		    , autoLoad: true
            , functions: {
            }
    });

    var showUpdateTime = false;
    scrwGrid.addListeners("gridComplete", function () {
        var ids = scrwGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            if (showUpdateTime) {
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleUpdateTime('" + ids[i] + "');\" class='ui-pg-div ui-inline-del' style='width:16px;margin:auto;' title='更新时间'><span class='ui-icon ui-icon-clock'></span></div>";
            }
            else {
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleCompleted('" + ids[i] + "');\" class='ui-pg-div ui-inline-del' style='width:16px;margin:auto;' title='置为完成'><span class='ui-icon ui-icon ui-icon-stop'></span></div>";

            }
            scrwGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });
        }
        if (rwid) {
            scrwGrid.getJqGrid().jqGrid("setSelection", rwid);
        }


    });

    window.handleCompleted = function (id) {
        showConfirm("确认信息", "您确定要将该任务单置为完成？", function () {
            ajaxRequest(
                options.setCompleteUrl,
                {
                    ids: id,
                    isCompleted: true
                },
                true,
                function () {
                    scrwGrid.refreshGrid();
                }
            );
            $(this).dialog('close');
        });
    };

    window.handleOtherCompleted = function (id) {
        showConfirm("确认信息", "您确定要将该任务单置为完成？", function () {
            ajaxRequest(
                options.setCompleteUrl,
                {
                    ids: id,
                    isCompleted: true
                },
                true,
                function () {
                    scrwGrid.refreshGrid();
                }
            );
            $(this).dialog('close');
        });
    };

    window.handleUpdateTime = function (id) {
        showConfirm("确认信息", "您确定要将该任务单更新为今日任务？", function () {
            ajaxRequest(
                options.HandleTodayPlanUrl,
                {
                    ids: id
                },
                true,
                function () {
                    showUpdateTime = false;
                    scrwGrid.refreshGrid();

                }
            );
            $(this).dialog('close');
        });
    };

    var d = new Date();
    var today = d.format("Y-m-d");
    //var getLatestDocscondition = "ProduceDate between '" + today + " 00:00:00' and '" + today + " 23:59:59' ";
    var getLatestDocscondition = "1=1";
    //var getLatestDocscondition = "";
    var shippDocGrid = new MyGrid({
        renderTo: 'latestShip'
        //,title:'最近发车记录'
            , autoWidth: true
        //, buttons: buttons0
            , height: 180
		    , storeURL: options.getLatestDocs
            , storeCondition: '1<>1'
		    , sortByField: 'ProduceDate'
            , sortOrder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 10
		    , showPageBar: true
            , dialogWidth: 700
            , dialogHeight: 400
            , singleSelect: true
            , rowNumbers: false
            , toolbarSearch: false
            , excelExport: false
            , advancedSearch: false
        //, groupField: 'ProductLineName'
		    , initArray: [
                { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, hidden: true }
                , { label: '有效否', name: 'IsEffective', index: 'IsEffective', width: 40, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 40, align: 'center', searchoptions: { sopt: ['eq']} }
                , { label: '时间', name: 'DeliveryTime', index: 'DeliveryTime', width: 40, align: 'center', formatter: 'date', formatoptions: { newformat: "H:i" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 40, align: 'center', search: false }
                , { label: '已供量', name: 'ProvidedCube', index: 'ProvidedCube', width: 40, align: 'center', search: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
                , { label: '调度量', name: 'SendCube', index: 'SendCube', width: 40, align: 'right' }
                , { label: '出票量', name: 'ParCube', index: 'ParCube', width: 40, align: 'right', search: false }
                , { label: '剩余量', name: 'RemainCube', index: 'RemainCube', width: 40, align: 'right', search: false }
                , { label: '报废量', name: 'ScrapCube', index: 'ScrapCube', width: 40, align: 'right', search: false }
                , { label: '转料量', name: 'TransferCube', index: 'TransferCube', width: 40, align: 'right', search: false }
                , { label: '其他方量2', name: 'XuCube', index: 'XuCube', width: 40, align: 'right', search: false }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 80, align: 'center' }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    shippDocGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    shippDocGrid.refreshGrid('1=1');
                }
                //作废
                , handleGarbage: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能作废');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 0 || isEffective == 'false') {
                        showMessage('提示', '已作废的发货单不能作废');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "发货单作废原因",
                        width: 350,
                        height: 200,
                        loadFrom: 'GarbageForm',
                        btn: btn,
                        postUrl: opts.garbageUrl,
                        postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            $("#remark").val("");
                        }

                    });
                }
                //取消作废
                , handleCancelGarbage: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能取消作废');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 1 || isEffective == 'true') {
                        showMessage('提示', '未作废的发货单不能取消作废');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "发货单取消作废原因",
                        width: 350,
                        height: 200,
                        loadFrom: 'GarbageForm',
                        btn: btn,
                        postUrl: opts.garbageUrl,
                        postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            $("#remark").val("");
                        }
                    });
                }

            }
    });


    var insteadProductGrid = new MyGrid({
        renderTo: 'insteadProductGrid'
            , title: '他司代生产记录'
            , autoWidth: true
        //, buttons: buttons0
            , height: 80
            , storeURL: options.getInsteadProduct
            , storeCondition: '1<>1'
        //, sortByField: 'ProduceDate'
        //, sortOrder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 10
		    , showPageBar: true
            , dialogWidth: 700
            , dialogHeight: 400
            , singleSelect: true
            , rowNumbers: false
            , toolbarSearch: false
            , excelExport: false
            , advancedSearch: false
        //, groupField: 'ProductLineName'
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 80, hidden: true }
                , { label: '带生产时间', name: 'ProductTime', index: 'ProductTime', width: 80, align: 'center', formatter: 'date' }
                , { label: '代生产站名', name: 'ProductName', index: 'ProductName', width: 100, align: 'center', search: false }
                , { label: '代生产方量', name: 'ProductNum', index: 'ProductNum', width: 80, align: 'center', search: false }
                , { label: '代生产单价', name: 'ProductSinglePrice', index: 'ProductSinglePrice', width: 80, align: 'center' }
                , { label: '代生产总价', name: 'ProductTotalPrice', index: 'ProductTotalPrice', width: 80, align: 'right' }
                , { label: '操作', name: 'myac', width: 50, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    insteadProductGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    insteadProductGrid.refreshGrid('1=1');
                }
            }


    });

    insteadProductGrid.addListeners("gridComplete", function () {
        insteadProductGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/InsteadProduct.mvc/Delete" });
    });

    //        shippDocGrid.addListeners("gridComplete", function () {
    //            //            var ids = shippDocGrid.getJqGrid().jqGrid('getDataIDs');
    //            //            for (var i = 0; i < ids.length; i++) {
    //            //                var cl = ids[i];
    //            //                var record = shippDocGrid.getRecordByKeyValue(ids[i]);
    //            //                shippDocGrid.getJqGrid().setCell(cl, "CarID", '', { background: 'red', color: 'white' }, ''); //CarID是name值
    //            //            }
    //            alert(shippDocGrid.getJqGrid().jqGrid());
    //        });


    //保存打印
    var errorMsg = "";
    function SaveDispatchPrint(btn, printType) {
        $.validator.unobtrusive.parse(document);
        var TaskID = scrwGrid.getFormFieldValue('ShippingDocument.TaskID');
        if (TaskID.length == 0) {
            showError('提示', '请选择今日任务单！');
            return;
        }
        //处理添加生产登记跨天时，出站时间异常问题
        DeliveryTimeFieldeProvided();
        var m = scrwGrid.getFormFieldValue('DeliveryTime');
        if (!isTime(m)) {           //空isTime返回true
            $("#DeliveryTime").focus();
            $('#DeliveryTime').addClass('input-validation-error');
            errorMsg += "<br>出站时间 字段是必需的。";
            showError('提示', '请输入正确的出站时间！');
            return;
        }
        var record = scrwGrid.getSelectedRecord();
        scrwGrid.handleFormSubmit({
            formId: 'scdj-form'
                , btn: btn
                , postUrl: options.saveDispatchUrl
                , postData: ''
                , beforeSubmit: function () {
                    var pvalue = scrwGrid.getFormFieldValue('DispatchList.ProductLineID');
                    if (isEmpty(pvalue)) {
                        $('#DispatchList_ProductLineID', '#scdjFormDiv').addClass('input-validation-error'); //设置字段框红色提示                     
                        showError('提示', '请选择搅拌机组编号！');
                        return false;
                    }
                    var carId = scrwGrid.getFormFieldValue('ShippingDocument.CarID');

                    if (isEmpty(carId)) {
                        $('#ShippingDocument_CarID', '#scdjFormDiv').addClass('input-validation-error'); //设置字段框红色提示
                        showError('提示', '请选择运输车号！');
                        return false;
                    }

                    //开盘提前时间
                    var tqmin = gSysConfig.MinutesBeforeNeedDate;
                    var now = new Date();
                    var needtime = Date.parse(record.NeedDate);
                    //计划时间内未开盘
                    if (record.Exception.replace(/(^\s+)|(\s+$)/g, "") == "" && record.OpenTime.replace(/(^\s+)|(\s+$)/g, "") == "" && needtime.setMinutes(needtime.getMinutes() + parseInt(tqmin), needtime.getSeconds(), 0) < now) {
                        scrwGrid.handleEdit({
                            loadFrom: 'ExForm',
                            width: 300,
                            title: '开盘异常',
                            height: 220,
                            afterFormLoaded: function () {
                                $("#ProduceTask_ID").val(record.ID);
                            },
                            postUrl: '/ProduceTask.mvc/Update',
                            postCallBack: function (response) {
                            }
                        });
                        scrwGrid.setFormId('scdj-form');
                        return false;
                    }


                    var maxCube = scrwGrid.getFormFieldValue('MaxCube'); //本车容量
                    var hntfs = scrwGrid.getFormFieldValue('DispatchList.BetonCount'); // 混凝土方量
                    var sjfs = scrwGrid.getFormFieldValue('DispatchList.SlurryCount'); // 砂浆方量
                    var qtfs = scrwGrid.getFormFieldValue('ShippingDocument.OtherCube'); //其他方量
                    var syfs = scrwGrid.getFormFieldValue('ShippingDocument.RemainCube'); //剩余方量
                    maxCube = isEmpty(maxCube) ? 0 : parseFloat(maxCube);
                    hntfs = isEmpty(hntfs) ? 0 : parseFloat(hntfs);
                    sjfs = isEmpty(sjfs) ? 0 : parseFloat(sjfs);
                    qtfs = isEmpty(qtfs) ? 0 : parseFloat(qtfs);
                    syfs = isEmpty(syfs) ? 0 : parseFloat(syfs);
                    var totalfs = hntfs + sjfs + qtfs + syfs;
                    if (gSysConfig.IsAllowSendTogether == "false" || gSysConfig.IsAllowSendTogether == 0) {
                        if (hntfs > 0 && sjfs > 0) {
                            showError('错误', '不能同时发送混凝土与砂浆!');
                            return false;
                        }
                    }

                    if (parseFloat(totalfs) > maxCube) {
                        showError('错误', '总方量值超过了本车的最大装载量!');
                        return false;
                    }

                    if (hntfs == 0 && sjfs == 0 && qtfs == 0 && syfs == 0) {
                        showError('错误', '请设置票据方量!');
                        return false;
                    }

                    if (hntfs < 0 || sjfs < 0 || qtfs < 0 || syfs < 0) {
                        showError('错误', '请设置非负方量!');
                        return false;
                    }
                    return true;
                }
                , postCallBack: function (response) {
                    if (response.Result) {

                        scrwGrid.resetForm();
                        $("#border-title").html("<font color='green'><b> 生产调度 </b></font>");
                        scdjGrid.reloadGrid();
                        if (!isEmpty(printType)) {
                            printShippingDoc(printType, response.Data);
                        }
                        refreshCarStatus();
                        rwid = TaskID;
                        scrwGrid.reloadGrid();
                    }
                    else {

                        if (response.Data == 0) {               //存在未处理的自动转退料记录，但是没有当前用户没有权限处理
                            //Grid框架内会弹出提示，所以不再重新提示
                            return;
                        }
                        else if (response.Data != null) {       //存在未处理的自动转退料记录，并且当前用户有权限处理
                            var recordTz = response.Data;
                            scrwGrid.handleAdd({
                                loadFrom: 'MyFormDiv_dispatch',
                                width: 700,
                                title: '检测到车号：【' + recordTz.CarID + '】 存在未处理的自动转退料记录',
                                height: 220,
                                afterFormLoaded: function () {
                                    $('#TZRalation_ReturnType').children('option[value=RT0]').remove();
                                    $("#TZRalation_CarID").val(recordTz.CarID);
                                    $("#TZRalation_Cube").val(recordTz.Cube);
                                    $("#TZRalation_ID").val(recordTz.ID);
                                    $("#TZRalation_Driver").val(recordTz.DriverUser);
                                    GetFrontTZInfo(recordTz.CarID);
                                },

                                postUrl: options.getTzMarkActionUrl,
                                postCallBack: function (response) {
                                    if (response.Result) {
                                        carSelectAction(recordTz.CarID);
                                    }
                                }
                            });
                            scrwGrid.setFormId('scdj-form');
                            return;
                        }

                    }
                    scrwGrid.resetForm();
                    scdjGrid.reloadGrid();
                    shippDocGrid.refreshGrid("TaskID='" + TaskID + "'", 'ID', 'DESC');
                    $("#parcube_red").html(0);
                }
        });
    }

    function renderIsRunning(cellvalue, options, rowObject) {
        if (rowObject['IsCompleted']) {
            return '<span style="margin:center;color:green;font-weight:bolder;">否</span>'
        }
        if (cellvalue) {
            return '<span rel="' + cellvalue + '" style="margin:center;color:red;font-weight:bolder;">是</span>'
        } else {
            return '<span rel="' + cellvalue + '" style="margin:center;color:green;font-weight:bolder;">否</span>'
        }
    }

    function renderProjectName(cellvalue, options, rowObject) {
        var projectInfo = getFormTitle(rowObject);
        return isEmpty(projectInfo) ? cellvalue : projectInfo;
    }
    
    scrwGrid.setFormId('scdj-form');
    //记录上次的已供方量、累计车数等。
    var lastDocRecord = null;
    //生产调度列表
    var containerWidth = $("#container").width();
    $("#left-panel").css("width", containerWidth * 0.49);
    var scdjGrid = new MyGrid({
        renderTo: 'scdj-grid'
		    , title: '生产调度列表'
            , width: containerWidth * 0.5
        //, autoWidth: true
            , buttons: buttons1
            , height: 420
		    , storeURL: options.findDispatchUrl
            , storeCondition: "IsCompleted = 0"
		    , sortByField: 'DispatchOrder'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , toolbarSearch: false
            , excelExport: false
            , dialogWidth: 500
            , dialogHeight: 300
		    , groupField: 'ProductLineName'
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , groupingView: { groupColumnShow: [true], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['asc'], groupSummary: [false], groupColumnShow: true, showSummaryOnHide: true, groupCollapse: false, minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                  { label: '发送状态', name: 'SendStatus', index: 'SendStatus', formatter: sendStatusFmt, unformat: booleanUnFmt, width: 100, align: 'center', sortable: false }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'center', sortable: false }
                , { label: '正生产', name: 'IsRunning', index: 'IsRunning', width: 45, align: 'center', sortable: false, formatter: renderIsRunning, unformat: booleanUnFmt, sortable: false }
                , { label: '剩余盘数', name: 'Field4', index: 'Field4', width: 50, align: 'center', sortable: false }
                , { label: '生产方量', name: 'ProduceCube', index: 'ProduceCube', width: 50, align: 'center', sortable: false }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 50, align: 'center', sortable: false }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 50, align: 'center', sortable: false }
                , { label: '工程信息', name: 'ProjectName', index: 'ProjectName', width: 200, editable: true, formatter: renderProjectName, sortable: false }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 50, sortable: false, align: 'center', sortable: false }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 50, sortable: false, align: 'center', sortable: false }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', editable: true, width: 80, sortable: false }
                , { label: '已完成', name: 'IsCompleted', index: 'IsCompleted', width: 45, align: 'center', sortable: false, formatter: booleanFmt, unformat: booleanUnFmt, sortable: false }
                , { label: '运输单号', name: 'ShipDocID', index: 'ShipDocID', editable: true, editrules: { required: true }, sortable: false }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', editable: true, sortable: false }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', editable: true, sortable: false }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', editable: true, hidden: true }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', editable: true, hidden: true }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', editable: true, hidden: true }
                , { label: '混凝土方量', name: 'BetonCount', index: 'BetonCount', editable: true, editrules: { required: true }, sortable: false, width: 50 }
                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', editable: true, editrules: { required: true }, sortable: false, width: 50 }
                , { label: '剩余方量', name: 'RemainCube', index: 'RemainCube', editable: true, editrules: { required: true }, sortable: false, width: 50 }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', editable: true, editrules: { required: true }, sortable: false, width: 50 }
                , { label: '其他方量2', name: 'XuCube', index: 'XuCube', editable: true, editrules: { required: true }, sortable: false, width: 70 }
                , { label: '当班司机', name: 'Driver', index: 'Driver', editable: true, sortable: false }
                , { label: '登记号', name: 'ID', index: 'ID', sortable: false }
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', editable: true, sortable: false }
                , { label: '发货员', name: 'Signer', index: 'Signer', editable: true, sortable: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', sortable: false }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', editable: true, sortable: false }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true, sortable: false }
                , { label: '创建人', name: 'Builder', index: 'Builder', editable: true, sortable: false }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', sortable: false }

		    ]
            , functions: {
                handleRefresh: function (btn) {
                    scdjGrid.refreshGrid();
                },
                handleDelete: function (btn) {
                    if (scdjGrid.isSelectedOnlyOne('请选择一条记录!')) {
                        var record = scdjGrid.getSelectedRecord();
                        scdjGrid.deleteRecord({
                            deleteUrl: btn.data.Url
                        , postCallBack: function (response) {
                            if (response.Result) {
                                shippDocGrid.refreshGrid("TaskID='" + record.TaskID + "' and " + getLatestDocscondition, 'ID', 'DESC');
                                scrwGrid.refreshGrid();
                                refreshCarStatus();
                            }
                        }
                        });
                    }
                }
                , printShippingDocument: function (btn) {
                    if (scdjGrid.isSelectedOnlyOne('请选择一条记录!')) {
                        var record = scdjGrid.getSelectedRecord();
                        var docId = record['ShipDocID'];
                        printShippingDoc('print', docId);
                    }
                }
                , handleCompleted: function (btn) {
                    if (scdjGrid.isSelectedOnlyOne('请选择一条记录!')) {
                        showConfirm("确认操作", "确认要置为完成?", function () {
                            var dispatchid = scdjGrid.getSelectedKey();
                            if (!isEmpty(dispatchid)) {
                                ajaxRequest(options.completedUrl, { id: dispatchid }, true, function (response) {
                                    if (response.Result) {
                                        scdjGrid.reloadGrid();
                                    }
                                });
                            }
                            $(this).dialog('close');
                        });
                    }
                }
                , handleMoveUp: function (btn) {
                    if (scdjGrid.isSelectedOnlyOne('请选择一条记录!')) {
                        var dispatchid = scdjGrid.getSelectedKey();
                        ajaxRequest(options.moveUpUrl, { id: dispatchid }, true, function (response) {
                            if (response.Result) {
                                scdjGrid.reloadGrid();
                            }
                        }, null, btn);
                    }
                }
                , handleSampling: function (btn) {
                    if (scdjGrid.isSelectedOnlyOne('请选择一个车号!')) {
                        var dispatchid = scdjGrid.getSelectedKey();
                        ajaxRequest(btn.data.Url, { dispatchid: dispatchid, carid: "" }, true, function (response) {
                            if (response.Result) {
                                scdjGrid.reloadGrid();
                            }
                        });
                    }
                }
                //异常处理操作
                , handleException: function (btn) {
                    if (scdjGrid.isSelectedOnlyOne('请选择一条记录!')) {
                        var record = scdjGrid.getSelectedRecord();
                        var docId = record['ShipDocID'];
                        var djh = record['ID'];
                        var carid = record['CarID'];
                        var driver = record['Driver'];
                        var productLineName = record['ProductLineName'];
                        var projectInfo = getFormTitle(record);
                        var parCube = record['ParCube'];
                        $("#exception-cube").val(0);

                        scdjGrid.showWindow({
                            loadFrom: 'exception-dialog'
                            , height: 200
                            , title: projectInfo + '    异常处理'
                            , afterLoaded: function () {
                                $("#exception-djh").val(djh);
                                $("#exception-curcarid").html(carid);
                                $("#exception-curproductline").html(productLineName);
                                $("#exception-productline").empty();
                                $("#exception-subtract").unbind("click");
                                $("#exception-cpbtn").unbind("click");
                                $("#exception-cubebtn").unbind("click");
                                $("#exception-carbtn").unbind("click");
                                $("#exception-providedbtn").unbind("click");
                                $("#exception-fill").unbind('click');
                                $("#exception-carid").unbind('change');
                                $("#exception-carid").bind('change', parCube, function (select) {
                                    var carid = select.target.value;
                                    var driverList = $("#exception-driver");
                                    driverList.empty();
                                    ajaxRequest(options.getCarInfoUrl, { id: carid }, false, function (response) {
                                        var users = response.Users;
                                        var maxCube = response.MaxCube;
                                        if (parseFloat(maxCube) < parseFloat(select.data)) {
                                            showError('错误', "抱歉，装不下！");
                                        }
                                        if (isEmpty(users)) return;
                                        //设定司机
                                        for (var i = 0; i < users.length; i++) {
                                            var user = users[i];
                                            driverList.append("<option value=\"" + user.UserID + "\">" + user.TrueName + "</option>");
                                        }
                                        $("#exception-driver").val(driver);
                                    });

                                });
                                $("#exception-carid").val(carid);
                                $("#exception-carid").trigger('change');
                                $("#exception-cpbtn").bind('click', djh, function (btn) {
                                    var djh = btn.data;
                                    var productlineid = $('#exception-productline').val();
                                    showConfirm("确认操作", "<span style='color:red'><b>警告：</b>换线前请与搅拌楼操作员确认原生产线未开始生产并告知不要启动生产，未确认的情况下进行换线操作可能导致错料，生产异常等严重的后果，由于未经确认进行换线而产生的损失，由操作人承担！</span><br/><br/>确认要换线生产?", function () {
                                        ajaxRequest(options.changeProductLineUrl, { id: djh, pid: productlineid }, true, function (response) {
                                            if (response.Result) {
                                                $("#exception-dialog").dialog('close');
                                                scdjGrid.reloadGrid();
                                            } scdjGrid.reloadGrid();
                                        });
                                        $(this).dialog('close');
                                    });
                                });

                                $("#exception-cubebtn").bind('click', djh, function (btn) {
                                    var djh = btn.data;
                                    var cube = $('#exception-cube').val();
                                    var fill = $('#exception-fill').is(":checked") ? true : false;
                                    var subtract = $('#exception-subtract').is(":checked") ? true : false;
                                    var confirmMsg = "补" + cube;
                                    if (subtract) {
                                        confirmMsg = "扣" + cube;
                                        cube = -cube;
                                    }
                                    if (Math.abs(cube) == 0) {
                                        showError('提示', '请输入大于0的方量值!');
                                        return;
                                    }
                                    showConfirm("确认操作", "确认要" + confirmMsg + " 方 ?", function () {
                                        ajaxRequest(options.changeCubeUrl, { id: djh, cube: cube }, true, function (response) {
                                            if (response.Result) {
                                                $("#exception-dialog").dialog('close');
                                                scdjGrid.reloadGrid();
                                            }
                                        });
                                        $(this).dialog('close');
                                    });
                                });

                                $("#exception-carbtn").bind('click', record, function (btn) {
                                    var oldcar = btn.data['CarID'];
                                    var driver = btn.data['Driver'];
                                    var djh = btn.data['ID'];
                                    var newcarid = $('#exception-carid').val();
                                    var newdriver = $('#exception-driver').val();
                                    if (oldcar == newcarid && driver == newdriver) {
                                        showMessage('提示', '没有修改值！');
                                        return;
                                    }
                                    var info = "确认要换车?";
                                    if (oldcar == newcarid && driver != newdriver) {
                                        info = "确认要换司机?";
                                    }
                                    showConfirm("确认操作", info, function () {
                                        ajaxRequest(options.changeCarUrl, { id: djh, carid: newcarid, driver: newdriver }, true, function (response) {
                                            if (response.Result) {
                                                $("#exception-dialog").dialog('close');
                                                scdjGrid.reloadGrid();
                                                refreshCarStatus();
                                            }
                                        });
                                        $(this).dialog('close');
                                    });
                                });

                                $("#exception-ProvidedTimes").val(scdjGrid.getSelectedRecord().ProvidedTimes);
                                $("#exception-ProvidedCube").val(scdjGrid.getSelectedRecord().ProvidedCube);
                                //修改已供车次，方量
                                $("#exception-providedbtn").bind('click', record, function (btn) {

                                    showConfirm("确认操作", "是否执行操作？", function () {
                                        ajaxRequest(options.changeProvidedUrl, { ID: scdjGrid.getSelectedKey(),
                                            ProvidedTimes: $("#exception-ProvidedTimes").val(),
                                            ProvidedCube: $("#exception-ProvidedCube").val()
                                        }, true, function (response) {
                                            if (response.Result) {
                                                $("#exception-dialog").dialog('close');
                                                scdjGrid.reloadGrid();
                                            }
                                        });
                                        $(this).dialog('close');
                                    });
                                });


                                //获取可选机组，车号等。
                                ajaxRequest(options.getDispatchUrl, { id: djh }, false, function (response) {
                                    var productLines = response.ProductLines;
                                    if (!isEmpty(productLines)) {
                                        for (var i = 0; i < productLines.length; i++) {
                                            var id = productLines[i].ID;
                                            var name = productLines[i].ProductLineName;
                                            $("#exception-productline").append("<option value=\"" + id + "\">" + name + "</option>");
                                        }
                                    }
                                    var defaultCheck = response.DefaultCheck;
                                    $("#exception-" + defaultCheck).attr('checked', true);
                                    $("#exception-subtract").bind('click', response.SubTract, function (btn) {
                                        $("#exception-cube").val(btn.data);
                                    });
                                    $("#exception-fill").bind('click', response.FillCube, function (btn) {
                                        $("#exception-cube").val(btn.data);
                                    });
                                });

                            }
                        });
                        $("#exception-tabs").tabs();
                    }

                }
            }
    });

    //重发生产调度
    window.handleRepeatSend = function (id, shipDocID) {
        ajaxRequest(
                    "DispatchList.mvc/RepeatSend",
                    {
                        "dispatchid": id,
                        "sendstatus": 1
                    },
                    true,
                    function (response) {
                        if (response.Result) {
                            scrwGrid.resetForm();
                            $("#border-title").html("<font color='green'><b> 生产调度 </b></font>");
                            scdjGrid.reloadGrid();
                            showConfirm('提示', '需要打印吗？', function () {
                                printShippingDoc('print', shipDocID);
                            });
                            refreshCarStatus();
                            dispatchrecord = scdjGrid.getRecordByKeyValue(id);
                            rwid = dispatchrecord.TaskID;
                            scrwGrid.reloadGrid();
                            shippDocGrid.refreshGrid("TaskID='" + rwid + "' and " + getLatestDocscondition, 'ID', 'DESC');
                        }
                        scrwGrid.resetForm();
                        scdjGrid.refreshGrid();

                    }
                );
    };

    //获取 标题栏信息
    function getFormTitle(record) {
        var projectName = record['ProjectName'];
        var tqd = record['ConStrength'];
        var sgbw = record['ConsPos'];
        return projectName + " - " + tqd + " - " + sgbw;
    }
   
    //刷新车辆状态
    function refreshCarStatus() {
        if ($('.carlist').length == 0) {
            clearInterval(gTimer_Car);
            return;
        }
        ajaxRequest(options.getCarStatusUrl, { "IsUsed": "1" }, false, function (response) {
            var allowShipCar = response.AllowShipCar;
            var shippingCar = response.ShippingCar;
            var restCar = response.RestCar;

            if (allowShipCar == null || allowShipCar == 'undefined') { 
            }
            else {
                //可调度车辆
                $('#AllowShipCar').empty();
                var allowShipHtml = '';
                for (var i = 0; i < allowShipCar.length; i++) {
                    var car = allowShipCar[i];
                    allowShipHtml += '<li data-itemid="' + car.ID + '"><div>' + car.ID + '</div></li>';
                }
                $('#AllowShipCar').append(allowShipHtml);
            }



            if (shippingCar == null || shippingCar == 'undefined') { 
            }
            else {
                //出货车辆
                $('#ShippingCar').empty();
                var shippingHtml = '';
                for (var i = 0; i < shippingCar.length; i++) {
                    var car = shippingCar[i];
                    shippingHtml += '<li data-itemid="' + car.ID + '"><div>' + car.ID + '</div></li>';
                }
                $('#ShippingCar').append(shippingHtml);
            }


            if (restCar == null || restCar == 'undefined') { 
            }
            else {
                //休息车辆
                $('#RestCar').empty();
                var restHtml = '';
                for (var i = 0; i < restCar.length; i++) {
                    var car = restCar[i];
                    restHtml += '<li data-itemid="' + car.ID + '"><div>' + car.ID + '</div></li>';
                }
                $('#RestCar').append(restHtml);
                //scrwGrid.refreshGrid();
            }


        });
    }

    //获取最后一张发货单的相关数据
    function setScdjForm(record) {
        var taskid = record['ID'];
        ajaxRequest(options.getLastDocUrl, { id: taskid }, false, function (response) {
            lastDocRecord = response;
            //设置remark/pumpname;
            var remark = response.Remark;
            var pumpName = response.PumpName;
            var pumpMan = response.PumpMan;

            if (response.ProvidedCube == 0) {
                remark = record['Remark'];
                //                pumpName = record['PumpName'];
            }
            scrwGrid.setFormFieldValue('ShippingDocument.PumpName', pumpName);
            scrwGrid.setFormFieldValue('ShippingDocument.PumpMan', pumpMan);

            if (scrwGrid.getFormFieldValue('ShippingDocument.Remark').toString().lenth > 0) {
                scrwGrid.setFormFieldValue('ShippingDocument.Remark', remark);
            }
            //设置以供方量、累计车数.
            scrwGrid.setFormFieldValue('ShippingDocument.ProvidedCube', response.ProvidedCube);
            scrwGrid.setFormFieldValue('ShippingDocument.ProvidedTimes', response.ProvidedTimes);
            var plist = response.ProductLines;
            //移除生产线下拉，重新填充.
            var pDropDownList = scrwGrid.getFormField('DispatchList.ProductLineID');
            pDropDownList.empty();
            var rateDropDownList = scrwGrid.getFormField('DispatchList.PCRate');
            rateDropDownList.empty();
            if (isEmpty(plist)) return;
            pDropDownList.append("<option value=\"\"></option>");
            var productLineArr = new Array();
            for (i = 0; i < plist.length; i++) {
                var id = plist[i].ID;
                var name = plist[i].ProductLineName;
                var dishNum = plist[i].DishNum;
                //通过一次查询，将发车时间保存到了ModifyTime中，避免两次查询。
                var deliveryTime = plist[i].ModifyTime;
                pDropDownList.append("<option value=\"" + id + "\">" + name + "</option>");
                productLineArr[id] = plist[i];
            }
            pDropDownList.unbind("change");
            //机组下拉列表事件绑定
            pDropDownList.bind('change', productLineArr, productLineChange);

            //解除car list disable,绑定事件
            var carField = scrwGrid.getFormField('ShippingDocument.CarID');
            carField[0].disabled = false;
            carField.unbind("change");
            carField.bind('change', carListChange);
            //自动选择车辆

            var autoCarId = response.AutoCarID;
            if (!isEmpty(autoCarId)) {
                scrwGrid.setFormFieldValue('ShippingDocument.CarID', autoCarId);
                carField.trigger('change');
            }

        },
            function () {
                isScrwGridClicked = false;
            });
    }

    //车辆选中事件处理
    function carListChange(select) {
        //ZLMessager(200, 200, 'show', 1000, "提示信息", "剩余方量3.8方", 5000);

        var carid = select.target.value;
        //        alert(2);
        carSelectAction(carid)
    }


    function carSelectAction(carid) {
        if (isEmpty(carid)) {
            isScrwGridClicked = false;
            return;
        }
        scrwGrid.setFormFieldReadOnly('DispatchList.BetonCount', false);
        var driverList = scrwGrid.getFormField('ShippingDocument.Driver');
        driverList.empty();
        ajaxRequest(options.getCarInfoUrl, { id: carid }, false, function (response) {
            var users = response.Users;
            //设定容重，
            var maxCube = response.MaxCube;
            scrwGrid.setFormFieldValue('MaxCube', maxCube); //本车容量
            if (!isEmpty(users)) {
                //设定司机
                for (var i = 0; i < users.length; i++) {
                    var user = users[i];
                    driverList.append("<option value=\"" + user.UserID + "\">" + user.TrueName + "</option>");
                }
            }
            //填充方量判断，获取配置中的数据。
            var hntfs = maxCube;
            //调度方量设定，0为车辆最大容量，否则默认为指定的方量
            var defaultCube = isEmpty(gSysConfig.DefaultManufactureCube) ? 0 : parseFloat(gSysConfig.DefaultManufactureCube);
            if (defaultCube > 0 && maxCube > defaultCube) {
                hntfs = defaultCube;
            }
            ajaxRequest(options.getRemaincubeUrl, { carId: carid }, false, function (response) {
                var remainCube = response.Data;
                if (!response.Result)          //存在没有处理的自动转退料记录
                {
                    if (response.Data == null)              //当前没有处理转退料的权限
                    {
                        showMessage('提示', '该车存在未处理的自动转退料记录');
                        return;
                    }
                    else if (response.Data != 0) {
                        recordTz = response.Data;

                        scrwGrid.handleAdd({
                            loadFrom: 'MyFormDiv_dispatch',
                            width: 700,
                            title: '检测到车号：【' + carid + '】 存在未处理的自动转退料记录',
                            height: 220,
                            afterFormLoaded: function () {
                                $('#TZRalation_ReturnType').children('option[value=RT0]').remove();
                                $("#TZRalation_CarID").val(recordTz.CarID);
                                $("#TZRalation_Cube").val(recordTz.Cube);
                                $("#TZRalation_ID").val(recordTz.ID);
                                $("#TZRalation_Driver").val(recordTz.DriverUser);
                                GetFrontTZInfo(recordTz.CarID);
                            },

                            postUrl: options.getTzMarkActionUrl,
                            postCallBack: function (response) {
                                if (response.Result) {
                                    carSelectAction(carid);
                                }
                            }
                        });
                        scrwGrid.setFormId('scdj-form');
                        scrwGrid.setFormFieldValue('DispatchList.BetonCount', hntfs);
                        return;
                    }

                }

                scrwGrid.setFormFieldValue('DispatchList.BetonCount', formatFloat((hntfs - remainCube), 2));
                scrwGrid.setFormFieldValue('ShippingDocument.RemainCube', remainCube);
                if (remainCube > 0) showMessage('提示', carid + '号车有剩料：' + remainCube + '方');
                //                alert(3);
                reCalculateProvided();
                isScrwGridClicked = false;
                //是否禁用运输单号下拉列表
                if (gSysConfig.IsDisableShipCar == "true") {
                    $('#shipCardDiv').css('display', 'none');
                    $('#shipCardDiv2').css('display', 'block');
                    $('#shipCarNo').val(carid);
                    //document.getElementById("ShippingDocument_CarID").disabled = true;
                    //scrwGrid.setFormFieldDisabled('ShippingDocument_CarID', true);
                }
                else {
                    $('#shipCardDiv').css('display', 'block');
                    $('#shipCardDiv2').css('display', 'none');
                    //document.getElementById("ShippingDocument_CarID").disabled = false;
                    //scrwGrid.setFormFieldDisabled('ShippingDocument_CarID', false);
                }
            });
        });
    }

    function formatFloat(src, pos) {
        return Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos);
    }


    //混凝土方量、砂浆方量、剩余方量等blur事件触发。
    function calculatePrivided() {
        //以供数量 
        var ygsl = isEmpty(lastDocRecord.ProvidedCube) ? 0 : parseFloat(lastDocRecord.ProvidedCube);
        ygsl = formatFloat(ygsl, 2);
        //累计车次加1
        //        alert(lastDocRecord.ProvidedTimes);
        var cc = isEmpty(lastDocRecord.ProvidedTimes) ? 0 : parseInt(lastDocRecord.ProvidedTimes);
        var current_cc = cc;
        var checksjfs = scrwGrid.getFormFieldValue('DispatchList.SlurryCount');
        var checkhntfs = scrwGrid.getFormFieldValue('DispatchList.BetonCount');
        var checkTLFL = scrwGrid.getFormFieldValue('ShippingDocument.RemainCube');
        var checkQTFL = scrwGrid.getFormFieldValue('ShippingDocument.OtherCube');
        var IsLubricatingSlurry = scrwGrid.getFormFieldValue('IsLubricatingSlurry');
        // if (!isEmpty(lastDocRecord.PumpName))
        //   scrwGrid.setFormFieldValue('ShippingDocument.PumpName', lastDocRecord.PumpName);
        checksjfs = isEmpty(checksjfs) ? 0 : parseFloat(checksjfs);
        checkhntfs = isEmpty(checkhntfs) ? 0 : parseFloat(checkhntfs);
        checkTLFL = isEmpty(checkTLFL) ? 0 : parseFloat(checkTLFL);
        checkQTFL = isEmpty(checkQTFL) ? 0 : parseFloat(checkQTFL);

        var current_ygsl = 0;

        if ((IsLubricatingSlurry == 'true') && (gSysConfig.IsSlurryAddUp == 'false')) {

            current_ygsl = ygsl + checkhntfs + checkTLFL + checkQTFL;

            //            alert(checkhntfs);
            if (checkhntfs > 0) {
                current_cc = current_cc + 1;
            }
        }
        else {

            current_ygsl = ygsl + checksjfs + checkhntfs + checkTLFL + checkQTFL;
            current_cc = current_cc + 1;
            //            alert(current_cc);
        }
        current_ygsl = formatFloat(current_ygsl, 2);
        //alert(ygsl + "\n" + checksjfs + "\n" + checkhntfs + "\n" + checkTLFL + "\n" + current_ygsl);

        var parcube = checksjfs + checkhntfs + checkTLFL + checkQTFL;
        parcube = formatFloat(parcube, 2);
        $("#parcube_red").html(parcube);

        scrwGrid.setFormFieldValue('ShippingDocument.ProvidedCube', current_ygsl);
        //        alert(current_cc);
        scrwGrid.setFormFieldValue('ShippingDocument.ProvidedTimes', current_cc);
        //增加超过计划方量提示
        var checkJHFL = scrwGrid.getFormFieldValue('ShippingDocument.PlanCube');
        checkJHFL = isEmpty(checkJHFL) ? 0 : parseFloat(checkJHFL);
        var checkDSCFL = scrwGrid.getSelectedRecord().InsteadCube;
        checkDSCFL = isEmpty(checkDSCFL) ? 0 : parseFloat(checkDSCFL);
        var temp = current_ygsl + checkDSCFL - checkJHFL;
        if (temp > 0) {
            showMessage('提示', '超过计划方量' + temp.toFixed(2) + '方');
        }
    }


    //机组下拉列表事件
    function productLineChange(select) {
        var pid = select.target.value;

        var rateDropDownList = scrwGrid.getFormField('DispatchList.PCRate');
        rateDropDownList.empty();
        if (isEmpty(pid)) return;
        //重置罐容比列表
        var productLineJson = select.data[pid];
        var dishNum = productLineJson.DishNum;
        rateDropDownList.append("<option value=\"" + dishNum + "\">" + dishNum + "</option>");
        var options = gDics.PCRate;
        for (i = 0; i < options.length; i++) {
            if (options[i].TreeCode < dishNum)
                rateDropDownList.append("<option value=\"" + options[i].TreeCode + "\">" + options[i].DicName + "</option>");
        }

        //重置发车时间
        var time = productLineJson.ModifyTime;
        var MTime = dataTimeFormat(time, 'H:i');
        //ms = dataTimeFormat(time, 'Y-m-d');
        scrwGrid.setFormFieldValue('DeliveryTime', MTime);
        scrwGrid.setFormFieldValue('ShippingDocument.DeliveryTime', dataTimeFormat(productLineJson.ModifyTime));
    }
    function DeliveryTimeFieldeProvided() {
        if (!isEmpty(scrwGrid.getFormFieldValue('DeliveryTime')) && !isEmpty(scrwGrid.getFormFieldValue('ShippingDocument.DeliveryTime'))) {
            var y = "";
            var m = "";
            var ss = "";
            y = scrwGrid.getFormFieldValue('ShippingDocument.DeliveryTime').substring(0, 10);
            m = scrwGrid.getFormFieldValue('DeliveryTime');
            ss = y + " " + m;
            scrwGrid.setFormFieldValue('ShippingDocument.DeliveryTime', ss);

        }
    }

    function isTime(str) {
        var regexp = /^([0-1][0-9]|[2][0-3])(:)([0-5][0-9])$/g;
        return regexp.test(str);
    }

    var isScrwGridClicked = false;

    //生产任务点击事件
    //已选择的生产登记
    var rwid = null;
    scrwGrid.addListeners('cellclick', function (id, record, colIndex, cellcontent) {
        if (isScrwGridClicked == true)
            return;
        isScrwGridClicked = true;
        lastDocRecord = null;
        scrwGrid.resetForm();
        var projectInfo = getFormTitle(record);
        $("#border-title").html("<font color='green'><b> 新增调度 :  " + projectInfo + " </b></font>");
        var gcmc = record['ProjectName'];
        var OtherDemand = record['Remark'];
        var IsLubricatingSlurry = record["IsLubricatingSlurry"];
        scrwGrid.setFormFieldValue('ShippingDocument.ProjectName', gcmc);
        scrwGrid.setFormFieldValue('ShippingDocument.Remark', OtherDemand);
        scrwGrid.setFormFieldValue('IsLubricatingSlurry', IsLubricatingSlurry);

        var taskId = record['ID'];
        scrwGrid.setFormFieldValue('ShippingDocument.TaskID', taskId);
        //车辆禁用，待请求结束后再enable。防止操作速度过快，累计方量、车数 不对。
        scrwGrid.setFormFieldDisabled('ShippingDocument.CarID', true);
        //scrwGrid.setFormFieldDisabled('ShippingDocument.Remark', true);
        //设置机组、绑定事件等。

        setScdjForm(record);
        //是否禁用砂浆输入框
        var sjbool = record['IsSlurry'];
        if (sjbool) {
            scrwGrid.setFormFieldReadOnly('DispatchList.SlurryCount', false);
        } else {
            scrwGrid.setFormFieldReadOnly('DispatchList.SlurryCount', true);
        }
        var jhfl = record['PlanCube'];
        scrwGrid.setFormFieldValue('DispatchList.SlurryCount', 0);
        scrwGrid.setFormFieldValue('DispatchList.BetonCount', 0);

        scrwGrid.setFormFieldReadOnly('DispatchList.BetonCount', true);

        scrwGrid.setFormFieldValue('ShippingDocument.RemainCube', 0);
        scrwGrid.setFormFieldValue('ShippingDocument.OtherCube', 0);
        scrwGrid.setFormFieldValue('ShippingDocument.PlanCube', jhfl);
        /*
        *不使用这种方式查看最近发车记录 xyl 2013-06-05
        if (colIndex > 7) {
        scdjGrid.refreshGrid("TaskID='" + taskId + "' AND IsCompleted = 1", 'ID', 'DESC');
        //scdjGrid.getJqGrid().jqGrid('setGridParam', { sortname: 'ID', sortorder: 'DESC', page: 1, postData: { condition: "TaskID='" + taskId + "' AND IsCompleted = 1"} });
        }
        */
        //shippDocGrid.refreshGrid("TaskID='" + taskId + "' and " + getLatestDocscondition, 'ID', 'DESC');
        shippDocGrid.refreshGrid("TaskID='" + taskId + "'", 'ID', 'DESC');
        insteadProductGrid.refreshGrid("TaskID='" + taskId + "'", 'ID', 'DESC');

    });



    //几个方量字段增加blur事件绑定。
    var hntfsField = scrwGrid.getFormField('DispatchList.BetonCount');
    var sjfsField = scrwGrid.getFormField('DispatchList.SlurryCount');
    var remainField = scrwGrid.getFormField('ShippingDocument.RemainCube');
    var otherField = scrwGrid.getFormField('ShippingDocument.OtherCube');
    function reCalculateProvided() {
        if (!isEmpty(lastDocRecord)) {
            //            alert(4);
            calculatePrivided();
        }
    }
    hntfsField.bind('blur', reCalculateProvided);
    sjfsField.bind('blur', reCalculateProvided);
    remainField.bind('blur', reCalculateProvided);
    otherField.bind('blur', reCalculateProvided);
    //几个方量字段增加blur事件绑定。   

    refreshCarStatus();

    $('#allow-ship-car-btn button').click(shiftButtonForCar);
    $('#shipping-car-btn button').click(HandleCarReturn);
    $('#rest-car-btn button').click(shiftButtonForCar);

    function shiftButtonForCar(btn) {
        if (gSysConfig.EnableCarDrag == "false") {
            showMessage('提示', '系统不允许在该页面调整车辆状态，请联系车辆管理人员!');
            return;
        }
        var operType = $(this).attr('opertype');
        var btnGroup = $(this).attr('btngroup');
        var carstatus = $(this).attr('carstatus'); //此处的carstatus表示要做的操作状态，并非车的当前状态
        var carId = null;
        if ($('.carlist div').hasClass('clickCarCls') && $('.clickCarCls', "#" + btnGroup).length > 0) {
            carId = $('.clickCarCls', "#" + btnGroup)[0].innerHTML;
        }
        //alert(operType + "\n" + btnGroup + "\n" + carstatus + "\n" + carId);
        if (isEmpty(carId)) {
            showMessage('提示', '请选中要操作的车辆!');
            return;
        }
        //判断操作类型
        //        var tt;
        //        if (operType == "Down" && carstatus == "1") { //置为休息
        //            tt = "置为休息";
        //        } else if (operType == "Handle" && carstatus == "0") { //置为出厂
        //            tt = "置为出厂";
        //        } else if (operType == "Up" && carstatus == "2") { //开工
        //            tt = "开工";
        //        }
        //        ajaxRequest("/SysLog.mvc/Add", { LogType: "手动改变车辆状态", Memo: carId + "号车" + tt + "，经办人：" + options.currentUser }, false, function (response) {
        //            if (response && response.Result) {
        //                //refreshCarStatus();
        //            }
        //        });
        var params = { operType: operType, carId: carId, status: carstatus };
        ajaxRequest(options.shiftCarStatusUrl, params, false, function (response) {
            if (response.Result) {
                refreshCarStatus();
            }
        }, function () { $('.carlist div').removeClass('clickCarCls') }, btn);

    }

    function HandleCarReturn(btn) {
        var operType = $(this).attr('opertype');
        var btnGroup = $(this).attr('btngroup');
        var carstatus = $(this).attr('carstatus');
        var carId = null;
        if ($('.carlist div').hasClass('clickCarCls') && $('.clickCarCls', "#" + btnGroup).length > 0) {
            carId = $('.clickCarCls', "#" + btnGroup)[0].innerHTML;
        }
        //alert(operType + "\n" + btnGroup + "\n" + carstatus + "\n" + carId);
        if (isEmpty(carId)) {
            showMessage('提示', '请选中要操作的车辆!');
            return;
        }
        //        ajaxRequest("/SysLog.mvc/Add", { LogType: "手动改变车辆状态", Memo: carId + "号车回厂，经办人：" + options.currentUser }, false, function (response) {
        //            if (response && response.Result) {
        //                //refreshCarStatus();
        //            }
        //        });
        ajaxRequest(options.carReturnUrl, { carId: carId }, false, function (response) {
            if (response && response.Result) {
                refreshCarStatus();
            }
        }, function () { $('.carlist div').removeClass('clickCarCls') }, btn);
    }

    //已选择的生产登记
    var sid = null;
    scdjGrid.addListeners("rowclick", function (ids) {
        sid = ids;
    });
    scdjGrid.addListeners("gridComplete", function () {
        if (sid) {
            scdjGrid.getJqGrid().jqGrid("setSelection", sid);
        }
    });

    function scdjAutoRefresh() {
        scdjGrid.refreshGrid();

        //        //这里新加一个弹出框 作为提示作用
        //        ajaxRequest(
        //                options.remindinfoUrl,
        //                null,
        //                false,
        //                function (response) {
        //                    if (response.Data == null||response.Data=='undefinded')
        //                        return;
        //                    remind(response.Data);
        //                }
        //            );
    }

    function showConfirm1(title, msg, okFn, cancleFn) {

        gTimer_Remind = window.clearInterval(gTimer_Remind);
        $('<div title="' + title + '"><p>' + msg + '</p></div>').dialog({
            resizable: false,
            closeOnEscape: false,
            open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); },
            show: "fade",
            hide: "fade",
            modal: true,
            buttons: {
                "取消": function (btn) {
                    gTimer_Remind = window.setInterval(remindRefresh, gSysConfig.SCDJRefreshInterval * 1000);
                    $(this).dialog("close");
                    if (cancleFn != null) {
                        executeFunction(cancleFn);
                    }
                },
                "确认": function (btn) {
                    gTimer_Remind = window.setInterval(remindRefresh, gSysConfig.SCDJRefreshInterval * 1000);
                    $(btn.currentTarget).button({ disabled: true });
                    if (okFn != null) {
                        executeFunction(okFn, btn);
                    }
                    $(this).dialog("close");
                }
            }
        });
    }


    function remindRefresh() {

        //这里新加一个弹出框 作为提示作用
        ajaxRequest(
                options.remindinfoUrl,
                null,
                false,
                function (response) {
                    if (response.Data == null || response.Data == 'undefinded')
                        return;
                    remind(response.Data);
                }
            );
    }

    function remind(obj) {
        showConfirm1("异常信息", obj[1], function () {
            ajaxRequest(
                options.remindinfoUpdateUrl,
                { DispatchID: obj[0] },
                true,
                null);
        });
    }


    if (gSysConfig.SCDJRefreshInterval) {
        var refreshInterval = gSysConfig.SCDJRefreshInterval * 1;
        if (refreshInterval > 0) {
            if (gTimer_SCDJ)
                clearInterval(gTimer_SCDJ);
            gTimer_SCDJ = window.setInterval(scdjAutoRefresh, gSysConfig.SCDJRefreshInterval * 1000);
            //是否自动刷新车辆状态
            if (gSysConfig.IsRefreshCarStatus=="true") {
                gTimer_Car = window.setInterval(refreshCarStatus, gSysConfig.SCDJRefreshInterval * 1000);
            }
            if (gTimer_Remind != null)
                gTimer_Remind = window.clearInterval(gTimer_Remind);
            gTimer_Remind = window.setInterval(remindRefresh, gSysConfig.SCDJRefreshInterval * 1000);
        }
    }
    scrwGrid.getJqGrid().jqGrid('filterToolbar', { stringResult: true, defaultSearch: 'cn' });

    //车辆拖放事件后台操作。
    if (gSysConfig.EnableCarDrag == "true") {
        $("#AllowShipCar").dragsort({ dragSelector: "div", dragBetween: true, placeHolderTemplate: "<li class='placeHolder'><div></div></li>", callBack: saveCarOrder });
    }

    function saveCarOrder(carId) {

        var data = $("#AllowShipCar li").map(function () { return $(this).attr("data-itemid"); }).get();
        var params = { "carid": carId, "orders": data };
        ajaxRequest(options.dragCarUrl, params, false, function (response) {
            if (!response.Result) {
                showError('提示', response.Message);
                refreshCarStatus();
            }
        });
    }


    function GetFrontTZInfo(carId) {
        ajaxRequest(options.getLastDocAutoByCarId,
                { carId: carId },
                false,
                function (response) {
                    $('table.info', '#MyFormDiv_dispatch').remove();
                    if (response.Result) {
                        $('#tmplShipDoc').tmpl(response.Data).insertAfter('#MyFormDiv_dispatch');
                        scdjGrid.setFormFieldValue('Driver', response.Data.Driver);
                        scdjGrid.setFormFieldValue('SourceShipDocID', response.Data.ID)
                    }

                }
            );
    }
    //车号点击事件
    var last_click_car = null;
    $('li div').live('click', function (t) {
        if (last_click_car != null) {
            last_click_car.removeClass('clickCarCls');
        }
        $(this).addClass('clickCarCls');
        last_click_car = $(this);
    });

    
};

//增加搅拌站
function addUnit() {
    var name = $("#AddUnit").val();
    if (isEmpty(name))
    {
        showError("提示！", "站名不能为空!");
        return;
    }
    $.ajax({
        url: "/Dic.mvc/AddUnit",
        dataType: "json",
        type: "POST",
        data: {
            text: $("#AddUnit").val()
        },
        success: function (data) {
            if (data.Result) {
                showMessage("增加成功");
                $("#InsteadProduct_ProductName").append("<option value='" + data.Data + "'>" + data.Data + "</option>");
            }
            else {
                showError("增加失败", data.Message);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        }
    });
};
var gTimer_SCDJ;
var gTimer_Car;
var gTimer_Remind;

