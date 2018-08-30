function arbillIndexInit(storeUrl) {
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
            , dialogWidth: 500
            , dialogHeight: 300
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '销售员', name: 'Sales', index: 'Sales', width: 50 }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID' }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width:200, sortable:false}
                , { label: '开账日', name: 'ARDate', index: 'ARDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '数量', name: 'Amount', index: 'Amount', width: 80, align: 'right' }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 80, align: 'right', formatter: 'currency' }
                , { label: '总金额', name: 'Total', index: 'Total', width: 80, align: 'right', formatter: 'currency'}
                , { label: '已收金额', name: 'InTotal', index: 'InTotal', width: 80, align: 'right', formatter: 'currency' }
                , { label: '未收金额', name: 'UnTotal', index: 'UnTotal', width: 80, align: 'right', formatter: 'currency' }
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
        function calcTotalPrice() { 
            var amount = myJqGrid.getFormFieldValue('Amount') * 1;
            var unitPrice = myJqGrid.getFormFieldValue('UnitPrice') * 1;
            var inTotal = myJqGrid.getFormFieldValue('InTotal') * 1;
            myJqGrid.setFormFieldValue('Total', amount * unitPrice);
            myJqGrid.setFormFieldValue('UnTotal', amount * unitPrice - inTotal);
        }
        var unitPriceField = $('#UnitPrice'); 
         unitPriceField.unbind('change',calcTotalPrice);
        unitPriceField.bind('change', calcTotalPrice);
        var amountField = $('#Amount');
         amountField.unbind('change', calcTotalPrice);
         amountField.bind('change', calcTotalPrice);

         var inTotalField = $('#InTotal');
         inTotalField.unbind('change', calcTotalPrice);
         inTotalField.bind('change', calcTotalPrice);
}