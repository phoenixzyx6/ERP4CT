function IndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , primaryKey: 'ID'
            , sortByField: 'ID'
            , showPageBar: true
            , setGridPageSize: 30
            , buttons: buttons0
            , dialogWidth: 700
            , dialogHeight: 350
            , storeURL: storeUrl.storeUrl
            , storeCondition: '1=1'
            , autoLoad: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 50, hidden: true }
                , { label: '采购单号', name: 'StockPactID', index: 'StockPactID' }
                , { label: '供应商ID', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: '供应商', name: 'SupplyName', index: 'SupplyName', width: 180 }
                , { label: '入库材料金额', name: 'PayBalance', index: 'PayBalance', width: 90 }
                , { label: '垫资金额', name: 'DzMoney', index: 'DzMoney', width: 90 }
                , { label: '结算时间', name: 'AdjustDate', index: 'AdjustDate', formatter: 'date' }
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
                        btn: btn,
                        afterFormLoaded: function () {
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                        }
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