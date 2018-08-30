function partstockcontractIndexInit(options) {
    var partPlanGrid = new MyGrid({
            renderTo: 'partPlanGrid'
            //, width: 610
            , height: 330
		    , storeURL: options.partStockPlanUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: false
            //, singleSelect: true
            , autoLoad: false
		    , initArray: [
                { label: '计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '计划日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date', width: 100 }
                , { label: '配件ID', name: 'PartID', index: 'PartID', width: 100, hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartName', width: 100 }
                , { label: '计划人', name: 'PlanMan', index: 'PlanMan', width: 80 }
                , { label: '计划数量', name: 'PlanNum', index: 'PlanNum', width: 80 }
                , { label: '单位', name: 'UnitID', index: 'UnitID', width: 80 }
                , { label: '部门ID', name: 'DepartmentID', index: 'DepartmentID', width: 80, hidden: true }
                , { label: '部门名称', name: 'DepartmentName', index: 'DepartmentName', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 100 }
                , { label: '计划执行状态', name: 'PlanStatus', index: 'PlanStatus', width: 80 }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 120 }
                , { label: '创建人', name: 'Builder', index: 'Builder' }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime' }
		    ]
            , functions: {
                getAvailableSilo: function () {
                    refreshSiloList(0);
                }, notAssignSilo: function () {
                    refreshSiloList(1);
                }, getAllSilo: function () {
                    refreshSiloList(2);
                }
            }
    });

    var contractGrid = new MyGrid({
        renderTo: 'contractGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: 200
            , singleSelect: true
		    , storeURL: options.contractUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogHeight: 200
		    , showPageBar: true
		    , initArray: [
                { label: '合同编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '配件编号', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartName', width: 100, sortable: false }
                , { label: '单位', name: 'UnitID', index: 'UnitID', width: 80, sortable: false }
                , { label: '当前库存', name: 'Inventory', index: 'Inventory', width: 60, sortable: false }
                , { label: '日期', name: 'ContractDate', index: 'ContractDate', formatter: 'date', width: 80 }
                , { label: '采购人', name: 'Buyer', index: 'Buyer', width: 60 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 60 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 120 }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 60 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 120 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    contractGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    contractGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    contractGrid.handleAdd({
                        loadFrom: 'contractForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    contractGrid.handleEdit({
                        loadFrom: 'contractForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    contractGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , addContractItems: function (btn) {
                    var contractid = contractGrid.getSelectedKey();
                    if (isEmpty(contractid)) {
                        showMessage('提示', '请选择一条合同信息');
                        return;
                    }
                    contractItemsGrid.handleAdd({
                        loadFrom: 'contractItemsForm',
                        afterFormLoaded: function () {
                            contractItemsGrid.setFormFieldValue('PartStockContractItem.ContractID', contractid);
                        },
                        btn: btn
                    });
                }
                , importStockPlan: function (btn) {
                    if (contractGrid.isSelectedOnlyOne('请选择一条记录！')) {
                        var record = contractGrid.getSelectedRecord();
                        contractGrid.showWindow({
                            title: btn.data.FuncDesc,
                            width: 650,
                            height: 480,
                            loadFrom: 'partPlanWindow'
                            , buttons: {
                                "关闭": function () {
                                    contractItemsGrid.reloadGrid();
                                    $(this).dialog("close");
                                },
                                "确定": function () {
                                    var keys = partPlanGrid.getSelectedKeys();
                                    if (keys.length == 0) {
                                        showMessage('提示', '请选择要导入的记录!');
                                        return;
                                    }
                                    for (var i = 0; i < keys.length; i++) {
                                        var planid = keys[i];
                                        var contractid = contractGrid.getSelectedKey();
                                        var params = { ContractID: contractid, StockPlanID: planid, UnitPrice: 0 };
                                        ajaxRequest(options.itemsAddUrl, params, true);
                                    }
                                    setTimeout(function () { partPlanGrid.reloadGrid(); }, keys.length * 500);
                                    
                                }
                            }
                        });
                        var partid = record['PartID'];
                        var contractId = record['ID'];
                        //partPlanGrid.refreshGrid("PartID='" + partid + "' AND AuditStatus = 1");
                        partPlanGrid.getJqGrid().jqGrid('setGridParam', { postData: { ContractID: contractId, PartID: partid} }).trigger("reloadGrid");
                    }
                }
                , handleAudit: function (btn) {
                    var key = contractGrid.getSelectedKey();
                    if (isEmpty(key)) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    showConfirm("确认审核", "确认要审核选中的记录", function () {
                        ajaxRequest(options.auditUrl, { id: key, auditstatus: 1 }, true, function (response) {
                            if (response.Result) {
                                contractGrid.reloadGrid();
                            }
                        });
                        $(this).dialog('close');
                    });
                }
            }
    });


    contractGrid.addListeners('rowclick', function () {
        var contractid = contractGrid.getSelectedKey();
        contractItemsGrid.refreshGrid("ContractID='" + contractid + "'");
    });

    var contractItemsGrid = new MyGrid({
        renderTo: 'contractItemsGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: 200
		    , storeURL: options.contractItemUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 350
            , dialogHeight: 200
		    , showPageBar: true
            , editSaveUrl: options.updateItemsUrl
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID',hidden:true }
                , { label: '合同编号', name: 'ContractID', index: 'PartID' }
                , { label: '采购计划编号', name: 'StockPlanID', index: 'StockPlanID' }
                , { label: '配件名称', name: 'PartName', index: 'PartName' }
                , { label: '计划采购数量', name: 'PlanNum', index: 'PlanNum', width: 80 }
                , { label: '单价(元)', name: 'UnitPrice', index: 'StockPlanID', editable: true, width: 80 }
                , { label: '单位', name: 'UnitID', index: 'UnitID', width: 50 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    contractItemsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    contractItemsGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    contractItemsGrid.handleAdd({
                        loadFrom: 'contractItemsForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    contractItemsGrid.handleEdit({
                        loadFrom: 'contractItemsForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    contractItemsGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });







}