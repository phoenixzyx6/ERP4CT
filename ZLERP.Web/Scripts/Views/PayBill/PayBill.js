function paybillIndexInit(storeUrl) {
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
                { label: '付款单号', name: 'ID', index: 'ID', width: 80 }
                , { label: '厂商代号', name: 'SupplyID', index: 'SupplyID',width:80 }
                , { label: '厂商', name: 'SupplyName', index: 'Supply.SupplyName' }
                , { label: '付款日期', name: 'PayDate', index: 'PayDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '付现金额(+)', name: 'Cash', index: 'Cash', width: 55, align: 'right', formatter: 'currency' }
                , { label: '付票金额(+)', name: 'CheckAmt', index: 'CheckAmt', width: 55, align: 'right', formatter: 'currency' }
                , { label: '折让金额(+)', name: 'Discount', index: 'Discount', width: 55, align: 'right', formatter: 'currency' }
                , { label: '前期预付(+)', name: 'LastDeposit', index: 'LastDeposit', width: 55, align: 'right', formatter: 'currency' }
                , { label: '本期预付(-)', name: 'NewDeposit', index: 'NewDeposit', width: 55, align: 'right', formatter: 'currency' }
                , { label: '其他金额(+)', name: 'OtherAdd', index: 'OtherAdd', width: 55, align: 'right', formatter: 'currency' }
                , { label: '其他金额(-)', name: 'OtherSub', index: 'OtherSub', width: 55, align: 'right', formatter: 'currency' }
                , { label: '可冲账金额', name: 'Total', index: 'Total', width: 80, align: 'right', formatter: 'currency', classes: 'green' }
                , { label: '付款人', name: 'PayUser', index: 'PayUser',width:80 }
                , { label: '收款人', name: 'ReceiveUser', index: 'ReceiveUser', width: 80 }
                , { label: '采购员', name: 'Buyer', index: 'Buyer', width: 80 }
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
        function calcTotal() {
            var cash = myJqGrid.getFormFieldValue('Cash') * 1;
            var checkAmt = myJqGrid.getFormFieldValue('CheckAmt') * 1;
            var discount = myJqGrid.getFormFieldValue('Discount') * 1;
            var newDeposit = myJqGrid.getFormFieldValue('NewDeposit') * 1;
            var lastDeposit = myJqGrid.getFormFieldValue('LastDeposit') * 1; 
            var otherAdd = myJqGrid.getFormFieldValue('OtherAdd') * 1;
            var otherSub = myJqGrid.getFormFieldValue('OtherSub') * 1;
            var total = cash + checkAmt + discount + lastDeposit + otherAdd - newDeposit - otherSub;
            myJqGrid.setFormFieldValue('Total', total);
        }
        $('.num input').bind('change', calcTotal);
}