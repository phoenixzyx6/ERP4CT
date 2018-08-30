
function AuditReportInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: opts.condition
		    , sortByField: 'TaskID'
		    , primaryKey: 'TaskID,IsAudit'
		    , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
        //, groupingView: { groupColumnShow: [true], groupSummary: [true] }
        //, groupField: 'IsAudit'
            , subGrid: true
            , subGridRowExpanded: function (subgrid_id, row_id) {
                var record = myJqGrid.getRecordByKeyValue(row_id);
                var taskid = record.TaskID;
                var viewrecords = true;
                if (taskid.length == 0) viewrecords = false;

                var isaudit = record.IsAudit;
                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                subgrid_pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + subgrid_pager_id + "'></div>");
                $("#" + subgrid_table_id).jqGrid({
                    url: opts.shipdocUrl,
                    datatype: 'json',
                    mtype: 'post',
                    colModel: [
                { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', search: false, width: 50 }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '途中', '1': '已回' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 50, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '未审核', '1': '已审核' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn'] }, hidden: true }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', search: false }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 60, align: 'right', search: false }
                , { label: '真实坍落度', name: 'RealSlump', index: 'RealSlump', width: 80 }
                , { label: '混凝土方量', name: 'BetonCount', index: 'BetonCount', width: 50, align: 'right', search: false }
                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 50, align: 'right', search: false }
                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 50, align: 'right' }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 50, align: 'right', search: false }
                , { label: '上车余料', name: 'RemainCube', index: 'RemainCube', width: 50, align: 'right', search: false }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 50, align: 'right', search: false }
                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 50, align: 'right' }
                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 50, align: 'right', search: false }
                , { label: '本车余料', name: 'TransferCube', index: 'TransferCube', width: 50, align: 'right', search: false }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 50, align: 'right', search: false }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }

		    ],
                    rowNum: 5,
                    height: '100%',
                    viewrecords: viewrecords,
                    sortable: true,
                    sortname: 'ID',
                    sortorder: 'desc',
                    pager: subgrid_pager_id,
                    postData: { condition: "IsEffective='1' and TaskID='" + taskid + "' and IsAudit='" + isaudit + "' and" + opts.condition },
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
                { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80 }
                , { label: '客户名称', name: 'CustName', index: 'CustName' }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 80 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
                , { label: '调度方量', name: 'SendCube', index: 'SendCube', summaryType: 'sum', summaryTpl: '{0}', width: 80 }
                , { label: '结算方量', name: 'SignInCube', index: 'SignInCube', summaryType: 'sum', summaryTpl: '{0}', width: 80 }
                , { label: '审核状态', name: 'IsAudit', index: 'IsAudit', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, width: 80 }
                , { label: '操作', name: 'act', index: 'act', width: 120, sortable: false, align: "center" }

		    ]
		    , autoLoad: true
    });

    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');

        for (var i = 0; i < ids.length - 1; i++) {
            var cl = ids[i];
            var record = myJqGrid.getRecordByKeyValue(ids[i]);
            //be = "<input class='identityButton'  type='button' value='打印' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handlePrint(" + ids[i] + ");\"  />";
            ce = "<input class='identityButton'  type='button' value='审核' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleAudit(" + record.TaskID + "," + ids[i] + ");\"  />";
            myJqGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });
        }
    });

    window.handlePrint = function (id) { alert(id); };

    window.handleAudit = function (taskid, id) {
        var begin = $("#begin_ProduceDate").val();
        var end = $("#end_ProduceDate").val();
        var record = myJqGrid.getRecordByKeyValue(id);
        if (record.IsAudit=="true") {
            showError("警告","该单据已审核");
            return false;
        }
        ajaxRequest(opts.handleAuditUrl, { taskid: taskid, begin: begin, end: end }, false, function (response) {
            if (response.Result) {
                showMessage(response.Message);
                myJqGrid.reloadGrid();
            }
        });
    };

}