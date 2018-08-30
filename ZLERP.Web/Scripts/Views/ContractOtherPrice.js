function contractotherpriceIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, autoWidth: true
            , width:610
            , buttons: buttons0
            , height: 300
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width:50 }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width:100 }
                , { label: '加价项目', name: 'PriceType', index: 'PriceType', width: 110 }
                , { label: '计算方式', name: 'CalcType', index: 'CalcType', width: 80 }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 60 }
                , { label: '数量', name: 'Amount', index: 'Amount', width: 60 }
                , { label: '全加', name: 'IsAll', index: 'IsAll', width: 50 }
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