function stuffsellIndexInit(opts) {
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
            , dialogWidth: 550
            , dialogHeight: 350
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '客户编码', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'Customer.CustName', width: 140 }
                , { label: '原料名称', name: 'StuffID', index: 'Silo.StuffInfo.ID', hidden: true }
                , { label: '筒仓编号', name: 'SiloID', index: 'Silo.ID', hidden: true }
                , { label: '原料名称', name: 'StuffName', index: 'Silo.StuffInfo.StuffName', width: 100 }
                , { label: '筒仓名称', name: 'SiloName', index: 'Silo.SiloName', width: 100 }
                , { label: '销售时间', name: 'SellTime', index: 'SellTime', formatter: 'date', width: 100, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '合同单号', name: 'SellContractID', index: 'SellContractID', width: 100 }
                , { label: '数量', name: 'SellName', index: 'SellName', width: 80 }
                , { label: '单价', name: 'SellPrice', index: 'SellPrice', width: 80, formatter: 'currency' }
                , { label: '销售额', name: 'SellTotalPrice', index: 'SellTotalPrice', width: 80, formatter: 'currency' }
                , { label: '业务员', name: 'Seller', index: 'Seller', width: 80 }
                , { label: '是否扣除库存', name: 'IsReduce', index: 'IsReduce', width: 100, formatter: booleanFmt, unformat: booleanUnFmt }
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
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.getFormField("SellPrice").bind('blur', function () {
                                var SellPrice = myJqGrid.getFormField("SellPrice").val();
                                var SellName = myJqGrid.getFormField("SellName").val();
                                myJqGrid.getFormField("SellTotalPrice").val(SellPrice * SellName);
                            });
                            myJqGrid.getFormField("SellName").bind('blur', function () {
                                var SellPrice = myJqGrid.getFormField("SellPrice").val();
                                var SellName = myJqGrid.getFormField("SellName").val();
                                myJqGrid.getFormField("SellTotalPrice").val(SellPrice * SellName);
                            });
                        }
                    });
                },
                handleEdit: function (btn) {

                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $('#StuffID').trigger("change");


                             
                            myJqGrid.getFormField("SellPrice").bind('blur', function () {
                                var SellPrice = myJqGrid.getFormField("SellPrice").val();
                                var SellName = myJqGrid.getFormField("SellName").val();
                                myJqGrid.getFormField("SellTotalPrice").val(SellPrice * SellName);
                            });
                            myJqGrid.getFormField("SellName").bind('blur', function () {
                                var SellPrice = myJqGrid.getFormField("SellPrice").val();
                                var SellName = myJqGrid.getFormField("SellName").val();
                                myJqGrid.getFormField("SellTotalPrice").val(SellPrice * SellName);
                            });
                            setTimeout(function () { myJqGrid.setFormFieldValue("SiloID", myJqGrid.getSelectedRecord()["SiloID"]); }, 500);
                        
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

        $('#StuffID').bind('change', function () {
            bindSelectData($('#SiloID'), opts.findSiloUrl, { condition: "stuffid='" + $(this).val() + "'" });
        });   

}