function receivebillIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth:500
		    , initArray: [
                { label: '收款单号', name: 'ID', index: 'ID', width: 80 }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 200,sortable:false }
                , { label: '开始结账日', name: 'BeginDate', index: 'BeginDate', width: 80 }
                , { label: '截止结账日', name: 'EndDate', index: 'EndDate', width: 80 }
                , { label: '收款日期', name: 'ReceiveDate', index: 'ReceiveDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '本期发货数量', name: 'NewAmount', index: 'NewAmount', width: 80 }
                , { label: '收现金额(+)', name: 'Cash', index: 'Cash', width: 50, align:'right', formatter:'currency' }
                , { label: '收票金额(+)', name: 'CheckAmt', index: 'CheckAmt', width: 50, align: 'right', formatter: 'currency' }
                , { label: '折让金额(+)', name: 'Discount', index: 'Discount', width: 50, align: 'right', formatter: 'currency' }
                , { label: '本期保留(+)', name: 'NewKeep', index: 'NewKeep', width: 50, align: 'right', formatter: 'currency' }
                , { label: '前期保留(-)', name: 'LastKeep', index: 'LastKeep', width: 50, align: 'right', formatter: 'currency' }
                , { label: '呆账金额(+)', name: 'NewBad', index: 'NewBad', width: 50, align: 'right', formatter: 'currency' }
                , { label: '呆账金额(-)', name: 'LastBad', index: 'LastBad', width: 50, align: 'right', formatter: 'currency' }
                , { label: '其他金额(+)', name: 'OtherAdd', index: 'OtherAdd', width: 50, align: 'right', formatter: 'currency' }
                , { label: '其他金额(-)', name: 'OtherSub', index: 'OtherSub', width: 50, align: 'right', formatter: 'currency' }
                , { label: '可冲账金额', name: 'Total', index: 'Total', width: 50, align: 'right', formatter: 'currency' }
                , { label: '业务员', name: 'Sales', index: 'Sales' }
                , { label: '付款人', name: 'PayUser', index: 'PayUser' }
                , { label: '收款人', name: 'ReceiveUser', index: 'ReceiveUser' }
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
            var newKeep = myJqGrid.getFormFieldValue('NewKeep') * 1;
            var lastKeep = myJqGrid.getFormFieldValue('LastKeep') * 1;
            var newBad = myJqGrid.getFormFieldValue('NewBad') * 1;
            var lastBad = myJqGrid.getFormFieldValue('LastBad') * 1;
            var otherAdd = myJqGrid.getFormFieldValue('OtherAdd') * 1;
            var otherSub = myJqGrid.getFormFieldValue('OtherSub') * 1;
            var total = cash + checkAmt + discount + newKeep + newBad + otherAdd - lastKeep - lastBad - otherSub; 
            myJqGrid.setFormFieldValue('Total',total);
        }
        //查询指定日期范围的发货方量
        function calcSendCube() {
            var contractId = myJqGrid.getFormFieldValue('ContractID');
            if (contractId == "") { 
                return;
            }
            var beginDate = myJqGrid.getFormFieldValue('BeginDate');
            var endDate = myJqGrid.getFormFieldValue('EndDate');
            if (beginDate == "" || endDate == "") {
                return;
            }
            ajaxRequest(opts.getSendCubeUrl, { contractId: contractId, beginDate: beginDate, endDate: endDate },
            false,
            function (response) {
                myJqGrid.setFormFieldValue('NewAmount', response);
            });
        }
        $('.num input').bind('change', calcTotal);

        $('#EndDate').bind('change', calcSendCube);
        $('#BeginDate').bind('change', calcSendCube);
        $("input[name='ContractID']").bind("change", calcSendCube);
}