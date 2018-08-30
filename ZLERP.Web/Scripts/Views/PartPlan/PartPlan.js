function partplanIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 500
            , dialogHeight: 250
		    , showPageBar: true
		    , initArray: [
                { label: '计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '计划日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date', width: 100 }
                , { label: '配件ID', name: 'PartID', index: 'PartID', width: 100, hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartName', width: 100, sortable: false }
                , { label: '计划人', name: 'PlanMan', index: 'PlanMan', width: 80 }
                , { label: '计划数量', name: 'PlanNum', index: 'PlanNum', width: 80 }
                , { label: '单位', name: 'UnitID', index: 'UnitID', width: 80, sortable: false }
                , { label: '部门ID', name: 'DepartmentID', index: 'DepartmentID', width: 80, hidden: true }
                , { label: '部门名称', name: 'DepartmentName', index: 'DepartmentName', width: 80, sortable: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 100 }
                , { label: '计划执行状态', name: 'PlanStatus', index: 'PlanStatus', width: 80 }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus') }, width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', width: 120 }
                , { label: '创建人', name: 'Builder', index: 'Builder' }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
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
                , handleAudit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    showConfirm("确认审核", "确认要审核选中的记录", function () {
                        ajaxRequest(options.auditUrl, { id: keys, auditstatus: 1 }, true, function (response) {
                            if (response.Result) {
                                myJqGrid.reloadGrid();
                            }
                        });
                        $(this).dialog('close');
                    });
                    
                }
            }
    });


        $("#PartID").bind("change", function (select) {
            var partid = $(this).val();
            var params = { id: partid };
            ajaxRequest(options.getUrl, params, false, function (response) {
                if (response.Result) {
                    var data = response.Data;
                    $('#Inventory').val(data.Inventory);
                    $('#UnitID').val(data.UnitID);
                    //alert(data.UnitID); Inventory
                }
            });
        });

}