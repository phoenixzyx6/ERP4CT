function stuffexamIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 560
            , dialogHeight: 200
            , groupField: 'ExamTypeName'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>总编号：{0} - {1}项</b>'], groupCollapse: true, groupOrder: ['desc'] }
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID' }
                , { label: '总编号', name: 'ExamTypeName', index: 'ExamTypeName', width: 130, sortable: false }
                , { label: '原料编号', name: 'StuffID', index: 'StuffID' }
                , { label: ' 原料名称', name: 'StuffName', index: 'StuffInfo.StuffName', width: 100 }
                , { label: ' 技术参数', name: 'TechnicalParam', index: 'TechnicalParam', width: 100 }
                , { label: '原料厂家', name: 'SupplyID', index: 'SupplyID', width: 130 }
                , { label: '进厂时间', name: 'InDate', index: 'InDate', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '录入人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '录入时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
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
            }
    });

}