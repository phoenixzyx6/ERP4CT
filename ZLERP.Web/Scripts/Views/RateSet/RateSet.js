function ratesetIndexInit(storeUrl) {
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
            , dialogWidth: 350
            , dialogHeight: 300
		    , initArray: [
                { label: '设置编号', name: 'ID', index: 'ID' }
                , { label: '砂含水%', name: 'SIWRate', index: 'SIWRate' }
                , { label: ' 砂含石%', name: 'SIRRate', index: 'SIRRate', width: 100 }
                , { label: '小石含水%', name: 'RIWRate', index: 'RIWRate', width: 130 }
                , { label: '大石含水%', name: 'RIWRate1', index: 'RIWRate1'}
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