function carlendIndexInit(storeUrl, ItemstoreUrl, addCarUrl) {
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
                { label: '车辆出租编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '客户编码', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '租入单位', name: 'CustName', index: 'Customer.CustName', width: 120 }
                , { label: '出租时间', name: 'LendTime', index: 'LendTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '租入办理人', name: 'LendIner', index: 'LendIner', width: 80 }
                , { label: '租出办理人', name: 'LendOuter', index: 'LendOuter', width: 80 }
                , { label: '施工日期', name: 'ProductDate', index: 'ProductDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '租车单价', name: 'LendPrice', index: 'LendPrice', width: 60 }
                , { label: '单价类型', name: 'PriceType', index: 'PriceType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Ptype' },stype:'select', searchoptions: {value: dicToolbarSearchValues('Ptype')}, width: 60 }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 80 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 80 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 80 }
                , { label: '当班签字', name: 'CurSign', index: 'CurSign', width: 60 }
                , { label: '车队签字', name: 'MotorSign', index: 'MotorSign', width: 60 }
                , { label: '经理签字', name: 'MangSign', index: 'MangSign', width: 60 }
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
                            myJqGrid.setFormFieldReadOnly('CarLend.ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        prefix: 'CarLend',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('CarLend.ID', true);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                 , handleAddCar: function (btn) {
                    var ids = myJqGrid.getSelectedKeys();

                    if (ids.length == 1) {
                        var id = myJqGrid.getSelectedKey();
                        ajaxRequest(addCarUrl, { id: id }, true, function (response) {
                                    if (response.Result) {
                                        myItemGrid.refreshGrid("CarLendID='" + id + "'");
                                    }
                                }, null, btn);
                    }
                    else {
                        showMessage('提示', "请选择一条记录信息");
                    }
                }
            }
    });

    var myItemGrid = new MyGrid({
        renderTo: 'MyItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: ItemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 300
            , dialogHeight: 250
            , storeCondition: '1<>1'
		    , initArray: [
                { label: '出租车辆明细编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '车辆出租编号', name: 'CarLendID', index: 'CarLendID', hidden: true }
                , { label: '车辆代号', name: 'CarID', index: 'CarID', width: 80 }
                , { label: '车牌号码', name: 'CarNo', jsonmap: 'Car.CarNo', index: 'Car.CarNo', width: 60 }
                , { label: '车辆类型', name: 'CarTypeID', index: 'Car.CarTypeID', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CarType'} }
                , { label: '回厂时间', name: 'BackTime', index: 'BackTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    var ids = myJqGrid.getSelectedKeys();
                    if (ids.length == 1) {
                        var id = myJqGrid.getSelectedKey();
                        myItemGrid.reloadGrid("CarLendID='" + id + "'");
                    } else {
                        showError("警告", "请选择一条记录信息");
                    }
                },
                handleRefresh: function (btn) {
                    myItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var ids = myJqGrid.getSelectedKeys();
                    if (ids.length == 1) {
                        var CarLendID = myJqGrid.getSelectedKey();
                        myItemGrid.handleAdd({
                            loadFrom: 'MyItemFormDiv',
                            btn: btn
                            , afterFormLoaded: function () {
                                myItemGrid.setFormFieldValue('CarLendItem.CarLendID', CarLendID);
                                myItemGrid.setFormFieldReadOnly('CarLendItem.CarLendID', true);
                            }
                        , postCallBack: function (response) {
                            myItemGrid.refreshGrid("CarLendID='" + CarLendID + "'");
                            myJqGrid.reloadGrid();
                        }
                        });
                    }
                    else {
                        showError("警告", "请在左侧选择一条记录信息");
                    }
                },
                handleEdit: function (btn) {
                    if (!myItemGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var record = myItemGrid.getSelectedRecord();
                    if (!isEmpty(record.BackTime)) {
                        showMessage('提示', '已经回厂的记录不能再修改！');
                        return;
                    }
                    myItemGrid.handleEdit({
                        loadFrom: 'MyItemFormDiv',
                        btn: btn,
                        prefix: 'CarLendItem'
                    });
                }
                , handleDelete: function (btn) {
                    myItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

        myJqGrid.addListeners('rowclick', function (id, record, selBool) {
            myItemGrid.refreshGrid("CarLendID='" + id + "'");
        });

}