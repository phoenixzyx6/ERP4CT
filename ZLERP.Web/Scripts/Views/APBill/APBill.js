function apbillIndexInit(storeUrl) {
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
		    , initArray: [
                { label: '应付单ID', name: 'ID', index: 'ID', width: 80 }
                , { label: '厂商代号', name: 'SupplyID', index: 'SupplyID', width: 80 }
                , { label: '厂商', name: 'SupplyName', index: 'Supply.SupplyName' }
                , { label: '采购员', name: 'Buyer', index: 'Buyer', width: 50 }
                , { label: '数量', name: 'Amount', index: 'Amount',width:80, align:'right' }
                , { label: '总价', name: 'Total', index: 'Total', width: 80, align: 'right', formatter: 'currency' } 
                , { label: '已付金额', name: 'Paied', index: 'Paied', width: 80, align: 'right', formatter: 'currency' }
                , { label: '未付金额', name: 'UnPay', index: 'UnPay', width: 80, align: 'right', formatter: 'currency', classes: 'red' }
                , { label: '付款日期', name: 'PayDate', index: 'PayDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '备注', name: 'Memo', index: 'Memo' }
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
        //计算总价
        function calcUnPay() {
            var total = myJqGrid.getFormFieldValue('Total') * 1;
            var paied = myJqGrid.getFormFieldValue('Paied') * 1; 
            myJqGrid.setFormFieldValue('UnPay', total - paied);
        }

        $('#Total').bind('change', calcUnPay);
        $('#Paied').bind('change', calcUnPay);
}