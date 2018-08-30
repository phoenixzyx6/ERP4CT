function stuffpriceIndexInit(storeUrl) {
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
            , dialogWidth: 300
            , dialogHeight: 300
            , singleSelect: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width:50}
                , { label: '供应商', name: 'SupplyName',jsonmap:'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName' }
                , { label: '原料', name: 'StuffName', jsonmap:'StuffInfo.StuffName', index: 'StuffInfo.StuffName' }
                , { name: 'SupplyID', index: 'SupplyID', hidden:true }
                , { name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: '价格日期', name: 'PriceDate', index: 'PriceDate', width:80 }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', formatter:'currency', align:'right', width:80 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
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