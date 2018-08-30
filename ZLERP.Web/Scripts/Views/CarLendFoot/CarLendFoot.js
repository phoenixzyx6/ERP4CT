function carlendfootIndexInit(storeUrl, ItemstoreUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'RightDiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 480
            , dialogHeight: 300
            , storeCondition: '1<>1'
		    , initArray: [
                { label: '车辆出租结算单编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '出租车辆明细编号', name: 'CarLendItemID', index: 'CarLendItemID', hidden: true }
                , { label: '司机', name: 'TrueName',jsonmap:'Driver.TrueName', index: 'Driver.TrueName', width: 60 }
                , { label: '运输趟次', name: 'TranTimes', index: 'TranTimes', width: 60 }
                , { label: '运输方量', name: 'TranCube', index: 'TranCube', width: 60 }
                , { label: '总金额', name: 'TotalPrice', index: 'TotalPrice', width: 60 }
                , { label: '送货地点', name: 'TranPlace', index: 'TranPlace', width: 80 }
                , { label: '加油量(L)', name: 'OilNum', index: 'OilNum', width: 60 }
                , { label: '油料单价', name: 'OilPrice', index: 'OilPrice', width: 60 }
                , { label: '油料金额', name: 'OilTotalPrice', index: 'OilTotalPrice', width: 60 }
                , { label: '结算时间', name: 'FootTime', index: 'FootTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var record = myItemGrid.getSelectedRecord();
                    var ids = myItemGrid.getSelectedKeys();
                    if (ids.length == 1) {
                        myJqGrid.handleAdd({
                            loadFrom: 'MyFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                myJqGrid.setFormFieldValue('CarLendFoot.CarLendItemID', record.ID);
                                myJqGrid.setFormFieldValue('CarLendFoot.LendPrice', record.LendPrice);
                                var Ptype=record.PriceType;
                                    var LendPrice = record.LendPrice;
                                myJqGrid.getFormField("CarLendFoot.TranTimes").bind('blur', function () {
                                   if(Ptype=="Ptype1")
                                    {
                                        var TranTimes=myJqGrid.getFormField("CarLendFoot.TranTimes").val();
                                        myJqGrid.getFormField("CarLendFoot.TotalPrice").val(LendPrice * TranTimes);
                                        myJqGrid.setFormFieldReadOnly('CarLendFoot.TotalPrice', true);
                                    }
                                });
                                myJqGrid.getFormField("CarLendFoot.TranCube").bind('blur', function () {                                
                                    if(Ptype=="Ptype2")
                                    {
                                        var TranCube=myJqGrid.getFormField("CarLendFoot.TranCube").val();
                                        myJqGrid.getFormField("CarLendFoot.TotalPrice").val(LendPrice * TranCube);
                                        myJqGrid.setFormFieldReadOnly('CarLendFoot.TotalPrice', true);
                                    }
                                });
                                myJqGrid.getFormField("CarLendFoot.OilNum").bind('blur', OilPriceBlur);
                                myJqGrid.getFormField("CarLendFoot.OilPrice").bind('blur', OilPriceBlur);
                            },
                            postCallBack: function (response) {
                                if (response.Result) {
                                    myJqGrid.reloadGrid();
                                }
                            }
                        });
                    }
                    else {
                        showError("警告", "请在左侧选择一条记录信息");
                    }
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: 'CarLendFoot'
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    var mySubGrid = new MyGrid({
        renderTo: 'SubDiv'
            , autoWidth: true
            , buttons: buttons1
            , height: 250
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 480
            , dialogHeight: 300
            , storeCondition: '1<>1'
		    , initArray: [
                { label: '车辆出租结算单编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '出租车辆明细编号', name: 'CarLendItemID', index: 'CarLendItemID', hidden: true }
                , { label: '司机', name: 'Driver.TrueName', index: 'Driver.TrueName', width: 60 }
                , { label: '运输趟次', name: 'TranTimes', index: 'TranTimes', width: 60 }
                , { label: '运输方量', name: 'TranCube', index: 'TranCube', width: 60 }
                , { label: '总金额', name: 'TotalPrice', index: 'TotalPrice', width: 60 }
                , { label: '送货地点', name: 'TranPlace', index: 'TranPlace', width: 80 }
                , { label: '加油量(L)', name: 'OilNum', index: 'OilNum', width: 60 }
                , { label: '油料单价', name: 'OilPrice', index: 'OilPrice', width: 60 }
                , { label: '油料金额', name: 'OilTotalPrice', index: 'OilTotalPrice', width: 60 }
                , { label: '结算时间', name: 'FootTime', index: 'FootTime', formatter: 'datetime', width: 80 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    mySubGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    mySubGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var record = myItemGrid.getSelectedRecord();
                    mySubGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            mySubGrid.setFormFieldValue('CarLendFoot.CarLendItemID', record.ID);
                            mySubGrid.setFormFieldValue('CarLendFoot.LendPrice', record.LendPrice);
                            var Ptype=record.PriceType;
                                var LendPrice = record.LendPrice;
                            mySubGrid.getFormField("CarLendFoot.TranTimes").bind('blur', function () {
                               if(Ptype=="Ptype1")
                                {
                                    var TranTimes=mySubGrid.getFormField("CarLendFoot.TranTimes").val();
                                    mySubGrid.getFormField("CarLendFoot.TotalPrice").val(LendPrice * TranTimes);
                                    mySubGrid.setFormFieldReadOnly('CarLendFoot.TotalPrice', true);
                                }
                            });
                            mySubGrid.getFormField("CarLendFoot.TranCube").bind('blur', function () {                                
                                if(Ptype=="Ptype2")
                                {
                                    var TranCube=mySubGrid.getFormField("CarLendFoot.TranCube").val();
                                    mySubGrid.getFormField("CarLendFoot.TotalPrice").val(LendPrice * TranCube);
                                    mySubGrid.setFormFieldReadOnly('CarLendFoot.TotalPrice', true);
                                }
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    mySubGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: 'CarLendFoot'
                    });
                }
                , handleDelete: function (btn) {
                    mySubGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    var myItemGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: ItemstoreUrl
            , storeCondition: "BackTime is null"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 680
            , dialogHeight: 450
		    , initArray: [
                { label: '出租车辆明细编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '出租编号', name: 'CarLendID', index: 'CarLendID', width: 70 }
                , { label: '车牌号码', name: 'CarNo', index: 'Car.CarNo', width: 60 }
                , { label: '车辆代号', name: 'CarID', index: 'CarID', width: 60 }
                , { label: '单价类型', name: 'PriceType', index: 'CarLend.PriceType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Ptype'} ,stype:'select', searchoptions: {value: dicToolbarSearchValues('Ptype')}}
                , { label: '租车单价', name: 'LendPrice', index: 'CarLend.LendPrice', width: 60   }
                , { label: '车辆类型', name: 'CarTypeID', index: 'Car.CarTypeID', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CarType'} ,stype:'select', searchoptions: {value: dicToolbarSearchValues('CarType')}}
                , { label: '回厂时间', name: 'BackTime', index: 'BackTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myItemGrid.refreshGrid('1=1');
                },
                handleEdit: function (btn) {
                   var CarLendItemID = myItemGrid.getSelectedKey();
                    myItemGrid.handleEdit({
                        loadFrom: 'MyItemFormDiv',
                        btn: btn,
                        prefix: 'CarLendItem',
                        afterFormLoaded: function () {
                            mySubGrid.refreshGrid("CarLendItemID='" + CarLendItemID + "'");
                        }
                    });
                }
            }
        });

        myItemGrid.addListeners('rowclick', function (id, record, selBool) {
            myJqGrid.refreshGrid("CarLendItemID='" + id + "'");
        });

        function OilPriceBlur() {
            var OilNum = myJqGrid.getFormField("CarLendFoot.OilNum").val();
            var OilPrice = myJqGrid.getFormField("CarLendFoot.OilPrice").val();
            if (!isEmpty(OilNum) && !isEmpty(OilPrice)) {
                myJqGrid.getFormField("CarLendFoot.OilTotalPrice").val(OilNum * OilPrice);
                myJqGrid.setFormFieldReadOnly('CarLendFoot.OilTotalPrice', true);
            }
        }
}