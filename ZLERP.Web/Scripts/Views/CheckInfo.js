//原材料盘点
function checkInfoIndexInit(storeUrl1, storeUrl2, storeUrl3, storeUrl4, FindSiloUrl, limitTimeUrl, FindSiloUrl2) {

    var checkitemlongtime = "false";

    var checkGrid = new MyGrid({
        renderTo: 'CheckGriddiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl1
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 760
            , dialogHeight: 560
		    , showPageBar: true
		    , initArray: [
                  { label: '编号', name: 'ID', index: 'ID', editable: true, editrules: { required: true }, width: 80 }
                , { label: '盘点人', name: 'Checker', index: 'Checker', editable: true, width: 80 }
                , { label: '盘点日期', name: 'CheckTime', index: 'CheckTime', editable: true, formatter: 'date', width: 100, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    checkGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    checkGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    checkGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        closeDialog: false,
                        postCallBack: function (response) {
                            checkGrid.getFormField("check.ID").val(response.Data);
                            var checkid = checkGrid.getFormField("check.ID").val();
                            subGrid.refreshGrid("CheckID='" + checkid + "'");
                        }
                    });
                },
                handleEdit: function (btn) {
                    checkGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: 'check',
                        afterFormLoaded: function () {
                            var checkid = checkGrid.getSelectedKey();
                            subGrid.refreshGrid("CheckID='" + checkid + "'");
                        }
                    });
                }
                , handleDelete: function (btn) {
                    checkGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //月结盘点
                , handleMonthPd: function (btn) {
                    checkGrid.handleAdd({
                        loadFrom: 'MyFormDivMonth',
                        btn: btn,
                        closeDialog: false,
                        width: 700,
                        height: 600,
                        afterFormLoaded: function () {
                            var checkid = checkGrid.getSelectedKey();
                            subGridMonth.refreshGrid("SiloID IN (SELECT SiloID from SiloProductLine)");
                        },
                        postCallBack: function (response) {
                            checkGrid.getFormField("check.ID").val(response.Data);
                            var checkid = checkGrid.getFormField("check.ID").val();
                            subGridMonth.refreshGrid("CheckID='" + checkid + "'");
                        }
                    });
                }
            }
    });

    // 右边Grid
    var itemGrid = new MyGrid({
        renderTo: 'ItemsGridDiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: storeUrl2
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 480
            , dialogHeight: 250
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '盘点子表编号', name: 'ID', index: 'ID', editable: true, editrules: { required: true }, hidden: true }
                , { label: '盘点主表编号', name: 'CheckID', index: 'CheckID', editable: true, editrules: { required: true }, hidden: true }
                , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', editable: true, editrules: { required: true }, hidden: true }
                , { label: '筒仓名称', name: 'SiloName', index: 'Silo.SiloName', width: 80 }
                , { label: '账面值', name: 'SystemValue', index: 'SystemValue', width: 60, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '盘点值', name: 'FactValue', index: 'FactValue', width: 60, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '盘盈（损）值', name: 'ProfitAndLoss', index: 'ProfitAndLoss', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '原因', name: 'Reason', index: 'Reason', width: 80 }
                , { label: '盘点人', name: 'Builder', index: 'Builder', width: 60 }
                , { label: '盘点时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 120 }
                , { label: '需要审核?', name: 'IsAuditor', index: 'IsAuditor', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 70 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, width: 80 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus')} }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    itemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    itemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {

                    if (checkitemlongtime == "true" || checkitemlongtime == true) {
                        showConfirm('确认', '此条盘点已经过期，是否确定继续操作？', function () {

                            var checkid = checkGrid.getSelectedKey();
                            if (checkid.length > 0) {
                                itemGrid.handleAdd({
                                    loadFrom: 'MyItemFormDiv',
                                    btn: btn,
                                    afterFormLoaded: function () {
                                        itemGrid.setFormFieldValue('checkitem.CheckID', checkid);
                                        itemGrid.setFormFieldReadOnly('checkitem.CheckID', true);
                                        itemGrid.setFormFieldValue('checkitem.IsAuditor', true); //需要审核
                                        bindSilo(itemGrid);
                                        bindHandler(itemGrid);
                                    },
                                    postCallBack: function (response) {
                                        itemGrid.refreshGrid("CheckID='" + checkid + "'");
                                    }
                                });
                            }
                            else {
                                showError("警告", "请先选择一条盘点基本信息");
                            }

                        });
                    }
                    else {
                        var checkid = checkGrid.getSelectedKey();
                        if (checkid.length > 0) {
                            itemGrid.handleAdd({
                                loadFrom: 'MyItemFormDiv',
                                btn: btn,
                                afterFormLoaded: function () {
                                    itemGrid.setFormFieldValue('checkitem.CheckID', checkid);
                                    itemGrid.setFormFieldReadOnly('checkitem.CheckID', true);
                                    itemGrid.setFormFieldValue('checkitem.IsAuditor', true); //需要审核
                                    bindSilo(itemGrid);
                                    bindHandler(itemGrid);
                                },
                                postCallBack: function (response) {
                                    itemGrid.refreshGrid("CheckID='" + checkid + "'");
                                }
                            });
                        }
                        else {
                            showError("警告", "请先选择一条盘点基本信息");
                        }
                    }
                },
                //直接盘点按钮
                handleAdd2: function (btn) {

                    if (checkitemlongtime == "true" || checkitemlongtime == true) {
                        showConfirm('确认', '此条盘点已经过期，是否确定继续操作？', function () {
                            var checkid = checkGrid.getSelectedKey();
                            var IsAuditor = checkGrid.getFormField(" check.IsAuditor").val();
                            if (checkid.length > 0) {
                                itemGrid.handleAdd({
                                    loadFrom: 'MyItemFormDiv',
                                    btn: btn,
                                    afterFormLoaded: function () {
                                        itemGrid.setFormFieldValue('checkitem.CheckID', checkid);
                                        itemGrid.setFormFieldReadOnly('checkitem.CheckID', true);
                                        itemGrid.setFormFieldValue('checkitem.IsAuditor', false); //不需要审核
                                        bindSilo(itemGrid);
                                        bindHandler(itemGrid);
                                    },
                                    postCallBack: function (response) {
                                        itemGrid.refreshGrid("CheckID='" + checkid + "'");
                                    }
                                });
                            }
                            else {
                                showError("警告", "请先选择一条盘点基本信息");
                            }

                        });
                    }
                    else {

                        var checkid = checkGrid.getSelectedKey();
                        var IsAuditor = checkGrid.getFormField(" check.IsAuditor").val();
                        if (checkid.length > 0) {
                            itemGrid.handleAdd({
                                loadFrom: 'MyItemFormDiv',
                                btn: btn,
                                afterFormLoaded: function () {
                                    itemGrid.setFormFieldValue('checkitem.CheckID', checkid);
                                    itemGrid.setFormFieldReadOnly('checkitem.CheckID', true);
                                    itemGrid.setFormFieldValue('checkitem.IsAuditor', false); //不需要审核
                                    bindSilo(itemGrid);
                                    bindHandler(itemGrid);
                                },
                                postCallBack: function (response) {
                                    itemGrid.refreshGrid("CheckID='" + checkid + "'");
                                }
                            });
                        }
                        else {
                            showError("警告", "请先选择一条盘点基本信息");
                        }
                    }

                },
                handleEdit: function (btn) {
                    var data = itemGrid.getSelectedRecord();
                    if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                        showError("警告", "该条盘点记录已影响库存不能进行修改！");
                        return;
                    }

                    if (checkitemlongtime == "true" || checkitemlongtime == true) {
                        showConfirm('确认', '此条盘点已经过期，是否确定继续操作？', function () {

                            itemGrid.handleEdit({
                                loadFrom: 'MyItemFormDiv',
                                btn: btn,
                                prefix: 'checkitem',
                                afterFormLoaded: function () {
                                    itemGrid.getFormField("checkitem.SystemValue").val(data.SystemValue);
                                    itemGrid.getFormField("checkitem.FactValue").val(data.FactValue);
                                    itemGrid.getFormField("checkitem.ProfitAndLoss").val(data.ProfitAndLoss);
                                    itemGrid.getFormField("SystemValue_T").val(data.SystemValue / 1000);
                                    itemGrid.getFormField("FactValue_T").val(data.FactValue / 1000);
                                    itemGrid.getFormField("ProfitAndLoss_T").val(data.ProfitAndLoss / 1000);

                                    itemGrid.setFormFieldReadOnly('SystemValue_T', true);
                                    itemGrid.setFormFieldReadOnly('ProfitAndLoss_T', true);
                                    bindSilo(itemGrid);
                                    bindHandler(itemGrid);
                                }
                            });

                        });
                    }
                    else {

                        itemGrid.handleEdit({
                            loadFrom: 'MyItemFormDiv',
                            btn: btn,
                            prefix: 'checkitem',
                            afterFormLoaded: function () {
                                itemGrid.getFormField("checkitem.SystemValue").val(data.SystemValue);
                                itemGrid.getFormField("checkitem.FactValue").val(data.FactValue);
                                itemGrid.getFormField("checkitem.ProfitAndLoss").val(data.ProfitAndLoss);
                                itemGrid.getFormField("SystemValue_T").val(data.SystemValue / 1000);
                                itemGrid.getFormField("FactValue_T").val(data.FactValue / 1000);
                                itemGrid.getFormField("ProfitAndLoss_T").val(data.ProfitAndLoss / 1000);

                                itemGrid.setFormFieldReadOnly('SystemValue_T', true);
                                itemGrid.setFormFieldReadOnly('ProfitAndLoss_T', true);
                                bindSilo(itemGrid);
                                bindHandler(itemGrid);
                            }
                        });

                    }
                }
                , handleDelete: function (btn) {

                    var data = itemGrid.getSelectedRecord();
                    if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                        showError("警告", "该条盘点记录已影响库存不能删除！");
                        return;
                    }

                    if (checkitemlongtime == "true" || checkitemlongtime == true) {
                        showConfirm('确认', '此条盘点已经过期，是否确定继续操作？', function () {

                            itemGrid.deleteRecord({
                                deleteUrl: btn.data.Url
                            });
                        });
                    }
                    else {
                        var data = itemGrid.getSelectedRecord();
                        if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                            showError("警告", "该条盘点记录已影响库存不能删除！");
                            return;
                        }
                        itemGrid.deleteRecord({
                            deleteUrl: btn.data.Url
                        });
                    }
                }
                , handleAuditing: function (btn) {
                    var data = itemGrid.getSelectedRecord();
                    if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                        showMessage("警告", "该条盘点记录不需要审核，或者已经通过审核！");
                    }
                    else {
                        itemGrid.handleEdit({
                            loadFrom: 'AuditingFormDiv',
                            btn: btn,
                            prefix: 'checkitem',
                            afterFormLoaded: function () {
                                itemGrid.getFormField("checkitem_FactValue_T").val(data.FactValue / 1000);
                            }
                        });
                    }
                }
            }
    });

    // form中的grid
    var subGrid = new MyGrid({
        renderTo: 'SubGridDiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: 300
		    , storeURL: storeUrl3
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 480
            , dialogHeight: 250
		    , showPageBar: true
		    , initArray: [
                { label: '盘点子表编号', name: 'ID', index: 'ID', editable: true, editrules: { required: true }, hidden: true }
                , { label: '盘点主表编号', name: 'CheckID', index: 'CheckID', editable: true, editrules: { required: true }, hidden: true }
                , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', editable: true, editrules: { required: true }, hidden: true }
                , { label: '筒仓名称', name: 'SiloName', index: 'Silo.SiloName', width: 80 }
                , { label: '账面值', name: 'SystemValue', index: 'SystemValue', width: 60, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '盘点值', name: 'FactValue', index: 'FactValue', width: 60, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '盘盈（损）值', name: 'ProfitAndLoss', index: 'ProfitAndLoss', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '原因', name: 'Reason', index: 'Reason', width: 80 }
                , { label: '盘点人', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '盘点时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130 }
                , { label: '需要审核?', name: 'IsAuditor', index: 'IsAuditor', width: 80, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 80 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, width: 80 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus')} }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    subGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    subGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                
                    var checkid = checkGrid.getFormField("check.ID").val();
                    var IsAuditor = checkGrid.getFormField(" check.IsAuditor").val();
                    if (checkid.length > 0) {
                        subGrid.handleAdd({
                            loadFrom: 'MyItemFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                subGrid.setFormFieldValue('checkitem.CheckID', checkid);
                                subGrid.setFormFieldReadOnly('checkitem.CheckID', true);
                                subGrid.setFormFieldValue('checkitem.IsAuditor', true);
                                bindSilo(subGrid);
                                bindHandler(subGrid);
                            },
                            postCallBack: function (response) {
                                subGrid.refreshGrid("CheckID='" + checkid + "'");
                            }
                        });
                    }
                    else {
                        showError("警告", "请先保存盘点基本信息");
                    }
                },
                handleAdd2: function (btn) {
                    var checkid = checkGrid.getFormField("check.ID").val();
                    var IsAuditor = checkGrid.getFormField(" check.IsAuditor").val();
                    if (checkid.length > 0) {
                        subGrid.handleAdd({
                            loadFrom: 'MyItemFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                subGrid.setFormFieldValue('checkitem.CheckID', checkid);
                                subGrid.setFormFieldReadOnly('checkitem.CheckID', true);
                                subGrid.setFormFieldValue('checkitem.IsAuditor', false);
                                bindSilo(subGrid);
                                bindHandler(subGrid);
                            },
                            postCallBack: function (response) {
                                subGrid.refreshGrid("CheckID='" + checkid + "'");
                            }
                        });
                    }
                    else {
                        showError("警告", "请先保存盘点基本信息");
                    }
                },
                handleEdit: function (btn) {

                    var data = subGrid.getSelectedRecord();
                    if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                        showError("警告", "该条盘点记录已影响库存不能进行修改！");
                        return;
                    }
                    subGrid.handleEdit({
                        loadFrom: 'MyItemFormDiv',
                        btn: btn,
                        prefix: 'checkitem',
                        afterFormLoaded: function () {
                            subGrid.getFormField("checkitem.SystemValue").val(data.SystemValue);
                            subGrid.getFormField("checkitem.FactValue").val(data.FactValue);
                            subGrid.getFormField("checkitem.ProfitAndLoss").val(data.ProfitAndLoss);
                            subGrid.getFormField("SystemValue_T").val(data.SystemValue / 1000);
                            subGrid.getFormField("FactValue_T").val(data.FactValue / 1000);
                            subGrid.getFormField("ProfitAndLoss_T").val(data.ProfitAndLoss / 1000);

                            subGrid.setFormFieldReadOnly('SystemValue_T', true);
                            subGrid.setFormFieldReadOnly('ProfitAndLoss_T', true);
                            bindSilo(subGrid);
                            bindHandler(subGrid);
                        }
                    });
                }
                , handleDelete: function (btn) {

                    var data = subGrid.getSelectedRecord();
                    if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                        showError("警告", "该条盘点记录已影响库存不能删除！");
                        return;
                    }
                    subGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                          , handleAuditing: function (btn) {
                              var data = subGrid.getSelectedRecord();
                              if (data.IsAuditor == false || data.IsAuditor == "false" || data.AuditStatus == 1) {
                                  showMessage("警告", "该条盘点记录不需要审核，或者已经通过审核！");
                              }
                              else {
                                  subGrid.handleEdit({
                                      loadFrom: 'AuditingFormDiv',
                                      btn: btn,
                                      prefix: 'checkitem'
                                  });
                              }
                          }
            }
    });
    checkGrid.addListeners('rowclick', function (id, record, selBool) {
        itemGrid.refreshGrid("CheckID='" + id + "'");

        ajaxRequest(limitTimeUrl, { checkinfoID: id }, false, function (response) {
            if (response.Result) {
                checkitemlongtime = response.Data;
            }
        });
    });

    //绑定生产线对应的筒仓
    function bindSilo(itemGrid) {
        itemGrid.getFormField("ProductLine").unbind('change');
        itemGrid.getFormField("ProductLine").bind('change', function () {
            bindSelectData(itemGrid.getFormField("checkitem.SiloID"),
                       FindSiloUrl,
                       { textField: 'SiloName',
                           valueField: 'ID',
                           condition: "SiloID IN (SELECT SiloID from SiloProductLine where ProductLineID = '" + itemGrid.getFormField("ProductLine").val() + "')"
                       });
        });
        
    }


    function bindHandler(itemGrid) {
        itemGrid.getFormField("checkitem.SiloID").unbind('change');;
        itemGrid.getFormField("checkitem.SiloID").bind('change', function () {
            var siloid = itemGrid.getFormField("checkitem.SiloID").val();
            var postParams = { id: siloid };
            var rData;
            itemGrid.getFormField("FactValue_T").val(0);
            ajaxRequest(storeUrl4, postParams, false, function (response) {
                if (response.Data) {
                    rData = response.Data;
                    itemGrid.getFormField("checkitem.SystemValue").val(rData.Content);
                    itemGrid.getFormField("SystemValue_T").val(rData.Content / 1000);
                    itemGrid.setFormFieldReadOnly('SystemValue_T', true);

                    calcProfitAndLoss(itemGrid);
                }
            });
        });
        itemGrid.getFormField("FactValue_T").unbind();
        itemGrid.getFormField("FactValue_T").bind('blur', function () {
            calcProfitAndLoss(itemGrid);
        });


    }

    function calcProfitAndLoss(itemGrid) {
        var fact = itemGrid.getFormField("FactValue_T").val();
        var sys = itemGrid.getFormField("SystemValue_T").val();
        itemGrid.getFormField("checkitem.FactValue").val(fact * 1000);
        itemGrid.getFormField("checkitem.SystemValue").val(sys * 1000);
        itemGrid.getFormField("checkitem.ProfitAndLoss").val((fact - sys) * 1000);
        itemGrid.getFormField("ProfitAndLoss_T").val(fact - sys);
        itemGrid.setFormFieldReadOnly('ProfitAndLoss_T', true);

    }

    

}