function partstockplanIndexInit(options) {
    var stockPlanGrid = new MyGrid({
        renderTo: 'stockPlanGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: 200
            , dialogWidth: 500
            , dialogHeight: 200
		    , storeURL: options.stockPlanUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '计划编号', name: 'ID', index: 'ID' }
                , { label: '配件ID', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartName', sortable: false }
                , { label: '计划采购日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date' }
                , { label: '计划采购数量', name: 'PlanNum', index: 'PlanNum', width: 80 }
                , { label: '当前库存', name: 'Inventory', index: 'Inventory', width: 80, sortable: false }
                , { label: '单位', name: 'UnitID', index: 'UnitID', width: 50, sortable: false }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '审核人', name: 'Auditor', index: 'Auditor' }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'date' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    stockPlanGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    stockPlanGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    stockPlanGrid.handleAdd({
                        loadFrom: 'stockPlanForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    stockPlanGrid.handleEdit({
                        loadFrom: 'stockPlanForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    stockPlanGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , addDetail: function (btn) {
                    if (stockPlanGrid.isSelectedOnlyOne('请选择一条记录！')) {
                        var id = stockPlanGrid.getSelectedKey();
                        stockPlanDetailGrid.handleAdd({
                            loadFrom: 'stockPlanDetailForm',
                            afterFormLoaded: function () {
                                stockPlanDetailGrid.setFormFieldValue("PartStockPlanDetail.StockPlanID", id);
                            },
                            btn: btn
                        });
                    }

                }
                , handleAudit: function (btn) {
                    var keys = stockPlanGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    showConfirm("确认审核", "确认要审核选中的记录", function () {
                        ajaxRequest(options.auditUrl, { id: keys, auditstatus: 1 }, true, function (response) {
                            if (response.Result) {
                                stockPlanGrid.reloadGrid();
                            }
                        });
                        $(this).dialog('close');
                    });

                }
            }
    });

         

    stockPlanGrid.addListeners('rowclick', function (id, record, selBool) {
        stockPlanDetailGrid.refreshGrid("StockPlanID = '" + id + "'");
    });


    var stockPlanDetailGrid = new MyGrid({
        renderTo: 'stockPlanDetailGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: 200
		    , storeURL: options.stockPlanDetailUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight:200
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
		    , initArray: [
                 { label: '部门编号', name: 'DepartmentID', index: 'DepartmentID',hidden:true }
                , { label: '部门名称', name: 'DepartmentName', index: 'DepartmentName' }
                , { label: '采购ID', name: 'StockPlanID', index: 'StockPlanID' ,hidden:true}
                , { label: '计划数量', name: 'PlanNum', index: 'PlanNum' }
		    ]
            , functions: {
                handleReload: function (btn) {
                    stockPlanDetailGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    stockPlanDetailGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    stockPlanDetailGrid.handleAdd({
                        loadFrom: 'stockPlanDetailForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    stockPlanDetailGrid.handleEdit({
                        loadFrom: 'stockPlanDetailForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    stockPlanDetailGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });


        $("#PartID").bind('change', function () {
            var partid = $(this).val();
            ajaxRequest(options.getPartInfoUrl, { ID: partid }, false, function (response) {
                if (response.Result) {
                    var data = response.Data;
                    $('#Inventory').val(data.Inventory);
                    $('#UnitID').val(data.UnitID);
                    //alert(data.UnitID); Inventory
                }
            });
        });

}