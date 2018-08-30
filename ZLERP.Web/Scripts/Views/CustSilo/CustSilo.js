function custsiloIndexInit(opts) {
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
            , groupField: 'CustName'
            , dialogWidth: 500
            , dialogHeight: 300
		    , initArray: [
                { label: '客户库存编号', name: 'ID', index: 'ID',width:80 }
                , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: '原料名称', name: 'StuffName', index: 'StuffID', width: 100 }
                , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', hidden: true }
                 , { label: '筒仓名称', name: 'SiloName', index: 'SiloID', width: 100 }
                , { label: '客户编码', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'CustomerID', width: 140 }
                , { label: '年月', name: 'YearMonth', index: 'YearMonth', width: 80 }
                , { label: '期初数量', name: 'Inival', index: 'Inival', width: 80 }
                , { label: '进货数量', name: 'Imval', index: 'Imval', width: 80 }
                , { label: '领用数量', name: 'Useval', index: 'Useval', width: 80 }
                , { label: '调整数量', name: 'Resetval', index: 'Resetval', width: 80 }
                , { label: '结存数量', name: 'Val', index: 'Val', width: 80 }
                , { label: '单价', name: 'UniPrice', index: 'UniPrice', width: 80 }
                , { label: '金额', name: 'TotalPrice', index: 'TotalPrice', width: 80 }
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
                            myJqGrid.setFormFieldReadOnly('ID', false);
                            myJqGrid.getFormField("Val").bind('blur', function () {
                           
                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
                                var Val = myJqGrid.getFormField("Val").val();
                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
                            });

                            myJqGrid.getFormField("UniPrice").bind('blur', function () {
                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
                                var Val = myJqGrid.getFormField("Val").val();
                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', true);
                            myJqGrid.getFormField("Val").bind('blur', function () {
                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
                                var Val = myJqGrid.getFormField("Val").val();
                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
                            });

                            myJqGrid.getFormField("UniPrice").bind('blur', function () {
                                var UniPrice = myJqGrid.getFormField("UniPrice").val();
                                var Val = myJqGrid.getFormField("Val").val();
                                myJqGrid.getFormField("TotalPrice").val(UniPrice * Val);
                            });
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