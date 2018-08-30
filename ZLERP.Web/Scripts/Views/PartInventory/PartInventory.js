function partinventoryIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
            , dialogWidth: 550
            , dialogHeight: 200
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                  { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus')} }
                , { label: '编号', name: 'ID', index: 'ID' }
                , { label: '盘点日期', name: 'InventoryDate', index: 'InventoryDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '盘点人', name: 'InventoryMan', index: 'InventoryMan' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
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

                    var data = myJqGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能修改');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        prefix: 'PartInventory',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {

                    var data = myJqGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能删除');
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleAudit: function (btn) {
                    var id = myJqGrid.getSelectedKeys();
                    if (id.length != 1) {
                        showError("警告", "请选择一条记录信息进行审核");
                        return;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    if ( data.AuditStatus == 1) {
                        showError("警告", "该条盘点记录已经通过审核！");
                        return;
                    }
                    showConfirm("确认审核", "确认要审核选中的记录,审核后将更新库存记录", function () {
                        ajaxRequest(btn.data.Url, { id: id }, true, function (response) {
                            if (response.Result) {
                                myJqGrid.reloadGrid();
                            }
                        });
                        $(this).dialog('close');
                    });

                }
            }
    });

        myJqGrid.addListeners('rowclick', function () {
            var InventoryID = myJqGrid.getSelectedKey();
            itemGrid.refreshGrid("InventoryID='" + InventoryID + "'");
        });

        //明细itemGrid
        var itemGrid = new MyGrid({
            renderTo: 'itemGrid'
            //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: options.PartInventoryDetailUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 500
            , dialogHeight: 250
		    , showPageBar: true
            , autoLoad: false
            , editSaveUrl: options.updatPortBorrwItemItem
		    , initArray: [
                  { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '配件名称', name: 'PartID', index: 'PartInfo.ID', hidden: true }
                , { label: '配件名称', name: 'PartName', index: 'PartInfo.PartName' }
                , { label: '帐面值', name: 'FaceValue', index: 'FaceValue' }
                , { label: '盘点值', name: 'ActualValue', index: 'ActualValue' }
                 , { label: 'InventoryID', name: 'InventoryID', index: 'InventoryID', hidden: true }
                , { label: '盘盈（损）值', name: 'DiffenceAmount', index: 'DiffenceAmount' }
		    ]
            , functions: {
                handleAdd: function (btn) {
                 
                    var id = myJqGrid.getSelectedKeys();
                    if (id.length != 1) {
                        showError("警告", "请在左侧选择一条记录信息");
                        return;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能增加');
                        return;
                    }
                    itemGrid.handleAdd({
                        loadFrom: 'MyDetailFormDiv',
                        afterFormLoaded: function () {
                            itemGrid.setFormFieldValue('PartInventoryDetail.InventoryID', id);
                            itemGrid.getFormField("PartInventoryDetail.ActualValue").bind('blur', function () {
                                var face = itemGrid.getFormField("PartInventoryDetail.FaceValue").val();
                                var actual = itemGrid.getFormField("PartInventoryDetail.ActualValue").val();
                                itemGrid.getFormField("PartInventoryDetail.DiffenceAmount").val(actual - face);
                                var test = itemGrid.getFormField("PartInventoryDetail.DiffenceAmount").val();
                                itemGrid.setFormFieldReadOnly('PartInventoryDetail.DiffenceAmount', true);
                                itemGrid.setFormFieldReadOnly('PartInventoryDetail.FaceValue', true);
                            });
                        },
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                  
                    var data = myJqGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能修改');
                        return;
                    }
                    itemGrid.handleEdit({
                        loadFrom: 'MyDetailFormDiv',
                        prefix: 'PartInventoryDetail',
                        afterFormLoaded: function () {

                            itemGrid.getFormField("PartInventoryDetail.ActualValue").bind('blur', function () {

                                var face = itemGrid.getFormField("PartInventoryDetail.FaceValue").val();
                                var actual = itemGrid.getFormField("PartInventoryDetail.ActualValue").val();
                                itemGrid.getFormField("PartInventoryDetail.DiffenceAmount").val(actual - face);
                                var test = itemGrid.getFormField("PartInventoryDetail.DiffenceAmount").val();
                                itemGrid.setFormFieldReadOnly('PartInventoryDetail.DiffenceAmount', true);
                                itemGrid.setFormFieldReadOnly('PartInventoryDetail.FaceValue', true);
                            });
                        },
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    var key = myJqGrid.getSelectedKey();
                    if (isEmpty(key)) {
                        showMessage('提示', "请选择左边一条记录!");
                        return;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能删除');
                        return;
                    }
                    itemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });

        $('#PartInventoryDetail_PartID').bind('change', function () {

            var partID = $(this).val();

            if (isEmpty(partID))
                return;
            ajaxRequest("/PartInfo.mvc/GetPartInfo",
                            { id: partID },
                            false,
                            function (response) {
                                if (!isEmpty(response)) {
                                    var data = response;
                                    $('#PartInventoryDetail_FaceValue').val(data.Inventory);
                                } else {
                                    showError("错误：", "该配件不存在！");
                                    return false;
                                }

                            }
                     );
        });

        
}