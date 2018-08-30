function handlerecordIndexInit(storeUrl) {
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
            , storeCondition: "stuffname!='水'"
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 100 }
                , { label: '材料用量', name: 'StuffAmount', index: 'StuffAmount', width: 100 }
                , { label: '生产线名称', name: 'ProductLineName', index: 'ProductLineName', width: 100 }
                , { label: '操作员', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '操作时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                //, { label: '生产线编号', name: 'ProductLineID', index: 'ProductLineID' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid("1=1 and stuffname!='水'");
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