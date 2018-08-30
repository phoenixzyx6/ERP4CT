function partinIndexInit(options) {
    var contractGrid = new MyGrid({
        renderTo: 'contractGrid'
            , width: 600
            , height: 350
            //, singleSelect: true
		    , storeURL: options.importContractUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogHeight: 200
		    , showPageBar: true
		    , initArray: [
                { label: '合同编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '配件编号', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartName', width: 100 }
                , { label: '单位', name: 'UnitID', index: 'UnitID', width: 80 }
                , { label: '当前库存', name: 'Inventory', index: 'Inventory', width: 60 }
                , { label: '日期', name: 'ContractDate', index: 'ContractDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '采购人', name: 'Buyer', index: 'Buyer', width: 60 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 60 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
             
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    contractGrid.reloadGrid();
                }
            }
    });
    
    
    
    var partInGrid = new MyGrid({
        renderTo: 'partInGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
            , dialogWidth: 650
            , dialogHeight: 300
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , singleSelect: true
            //, editSaveUrl: options.partInSaveUrl
		    , showPageBar: true
		    , initArray: [
                  { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '进货编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '进货日期', name: 'StockDate', index: 'StockDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '接收人', name: 'Recipientor', index: 'Recipientor', width: 50 }
                , { label: '金额(元)', name: 'TotalMoney', index: 'TotalMoney', width: 50 }
                , { label: '应付金额(元)', name: 'TotalPayment', index: 'TotalPayment', width: 80, editable: true }
                , { label: '已付', name: 'IsPayment', index: 'IsPayment', width: 50, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80, width: 60 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
               
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    partInGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    partInGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    partInGrid.handleAdd({
                        loadFrom: 'partInForm',
                        btn: btn
                    });

                    partInGrid.reloadGrid();
                },
                handleEdit: function (btn) {


                    var data = partInGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能修改');
                        return;
                    }
                    partInGrid.handleEdit({
                        loadFrom: 'partInForm',
                        prefix: 'PartIn',
                        btn: btn
                    });

                    partInGrid.reloadGrid();
                }
                , handleDelete: function (btn) {

                    var data = partInGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能删除');
                        return;
                    }
                    partInGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , importContractItem: function (btn) {
                    if (partInGrid.isSelectedOnlyOne('请选择一条记录！')) {
                        var record = partInGrid.getSelectedRecord();
                        partInGrid.showWindow({
                            title: btn.data.FuncDesc,
                            width: 650,
                            height: 480,
                            loadFrom: 'contractWindow'
                            , buttons: {
                                "关闭": function () {
                                    itemsGrid.reloadGrid();
                                    $(this).dialog("close");
                                },
                                "确定": function () {
                                    var keys = contractGrid.getSelectedKeys();
                                    if (keys.length == 0) {
                                        showMessage('提示', '请选择要导入的记录!');
                                        return;
                                    }
                                    /*
                                    for (var i = 0; i < keys.length; i++) {
                                        var planid = keys[i];
                                        var contractid = itemsGrid.getSelectedKey();
                                        var params = { ContractID: contractid, StockPlanID: planid, UnitPrice: 0 };
                                        ajaxRequest(options.itemsAddUrl, params, true);
                                    }*/
                                    setTimeout(function () { itemsGrid.reloadGrid(); }, keys.length * 500);
                                }
                            }
                        });
                        contractGrid.refreshGrid("AuditStatus = 1");
                    }
                }
                , handleAudit: function (btn) {
                    var key = partInGrid.getSelectedKey();
                    if (isEmpty(key)) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    var data = partInGrid.getSelectedRecord();
                    if (data.AuditStatus == 1) {
                        showError("警告", "该条盘点记录已经通过审核！");
                        return;
                    }
                    showConfirm("确认审核", "确认要审核选中的记录", function () {
                        ajaxRequest(options.auditUrl, { id: key, auditstatus: 1 }, true, function (response) {
                            if (response.Result) {
                                partInGrid.reloadGrid();
                            }
                        });
                        $(this).dialog('close');
                    });

                }
                
            }
    });


        var itemsGrid = new MyGrid({
            renderTo: 'itemsGrid'
            , buttons: buttons1
            , height: gGridHeight
            , autoWidth: true
		    , storeURL: options.itemsUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
            //, groupField: 'UserType'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad:false
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartName', width: 120 }
              //  , { label: '入库编号', name: 'PartInID', index: 'PartInID' }
              //  , { label: '采购合同编号', name: 'ContractID', index: 'ContractID' }
                , { label: '入库数量', name: 'InNum', index: 'InNum', width: 80 }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 80 }
                , { label: '条码', name: 'BarCode', index: 'BarCode', width: 160 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
               
		    ]
            , functions: {
                handleReload: function (btn) {
                    itemsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    itemsGrid.refreshGrid();
                },
                handleEdit: function (btn) {
                    itemsGrid.handleEdit({
                        loadFrom: 'partInItemForm',
                        prefix: 'PartInItem',
                        btn: btn
                    });
                  
                },
                handleDelete: function (btn) {
                    var key = partInGrid.getSelectedKey();
                    if (isEmpty(key)) {
                        showMessage('提示', "请选择左边一条记录!");
                        return;
                    }
                    var data = partInGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能删除');
                        return;
                    }
                    itemsGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });

                }, 
                handleAddItem: function (btn) {
                    var key = partInGrid.getSelectedKey();
                    if (isEmpty(key)) {
                        showMessage('提示', "请选择左边一条记录!");
                        return;
                    }
                    var data = partInGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能增加');
                        return;
                    }
                    itemsGrid.handleAdd({
                        loadFrom: 'partInItemForm',
                        afterFormLoaded: function () {
                            itemsGrid.setFormFieldValue("PartInItem.PartInID", key);
                        },
                        btn: btn
                    });
                   
                 

                }
            }
        });


        partInGrid.addListeners('rowclick', function (id) {
            itemsGrid.refreshGrid("PartInID='" + id + "'");
        });


}