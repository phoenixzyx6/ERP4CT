function partreturnIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 480
            , dialogHeight: 250
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID' }
                , { label: '退回日期', name: 'ReturnDate', index: 'ReturnDate', formatter: 'date' }
                , { label: '接收人', name: 'Recipientor', index: 'Recipientor' }
        //, { label: 'FactoryID', name: 'FactoryID', index: 'FactoryID' }
                , { label: '发票张数', name: 'InvoiceNum', index: 'InvoiceNum' }
                , { label: '收票方式', name: 'OperateStatus', index: 'OperateStatus' }
                , { label: '物资采购', name: 'Purchaser', index: 'Purchaser' }
                , { label: '送料人', name: 'Sender', index: 'Sender' }
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