function producePlanIndexInit(storeUrl, HandleTodayPlanUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 300
            , dialogHeight: 300
            , groupField: 'PlanDay'
            , singleSelect: true
            , groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: ' 流水号', name: 'ID', index: 'ID', width: 70 }
                , { label: ' 任务单号', name: 'TaskID', index: 'TaskID', width: 70 }
                , { label: ' 工程名称', name: 'ProjectName', index: 'ProjectName', width: 150, sortable: false }
                , { label: ' 强度等级', name: 'ConStrength', index: 'ConStrength', width: 100, sortable: false }
                , { label: ' 施工部位', name: 'ConsPos', index: 'ConsPos', width: 100, sortable: false }
                , { label: ' 计划日期', name: 'PlanDay', index: 'PlanDay', formatter: 'date', hidden: true }
                , { label: ' 计划日期', name: 'PlanDate', index: 'PlanDate', formatter: 'datetime', width: 150, sortable: false, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 开盘时间', name: 'OpenTime', index: 'OpenTime', formatter: 'datetime', width: 150, sortable: false, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 计划方量', name: 'PlanCube', index: 'PlanCube', width: 70 }
                , { label: ' 计划班组', name: 'PlanClass', index: 'PlanClass', width: 70 }
                , { label: ' 上料员', name: 'ForkLift', index: 'ForkLift', width: 70 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });

                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleTodayPlan: function (btn) {

                    var requestURL = HandleTodayPlanUrl;
                    var postParams = { id: myJqGrid.getSelectedRecord().ID };
                    ajaxRequest(requestURL, postParams, true, function (response) {
                        myJqGrid.reloadGrid();
                    });
                    myJqGrid.reloadGrid();
                }
            }
    });

}