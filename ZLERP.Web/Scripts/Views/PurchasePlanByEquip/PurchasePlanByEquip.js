function PurchasePlanByEquipIndexInit(options) {

    function typeFmt(cellvalue, options, rowObject) {
        if (cellvalue == "0") {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "设备类";
            }

        }
        else if (cellvalue == "1") {
            var style = "color:red;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[0];
            } else {
                var txt = "车辆类";
            }
        }
        else {
            var txt = '';
        }

        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function typeUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }

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
            , dialogHeight: 300
		    , showPageBar: true
		    , initArray: [
                { label: '计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '维修类型', name: '_type', index: '_type', width: 100, formatter: typeFmt, unformat: typeUnFmt }
                , { label: '审核状态', name: 'PurchasePlan_state', index: 'PurchasePlan_state', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PurchasePlanByEquip_state' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('PurchasePlanByEquip_state') }, width: 120 }
                , { label: '计划执行状态', name: 'PurchasePlan_planstate', index: 'PurchasePlan_planstate', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PurchasePlanByEquip_planstate' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('PurchasePlanByEquip_planstate') }, width: 120 }
                , { label: '申请时间', name: 'PurchasePlan_NeedDate', index: 'PurchasePlan_NeedDate', formatter: 'date', width: 100, sortable: false }
                , { label: '设备', name: 'GoodsID', index: 'GoodsID', width: 100 }
//                , { label: '设备', name: 'EquipmentName', index: 'EquipmentName', width: 100 }
//                , { label: '物资名称', name: 'ClassBName', index: 'ClassBName', width: 100 }
//                , { label: '物资类型', name: 'ClassMName', index: 'ClassMName', width: 100 }
//                , { label: '物资类型名称', name: 'ClassSName', index: 'ClassSName', width: 200 }
                , { label: '预估价格', name: 'planmoney', index: 'planmoney', width: 80 }
                , { label: '维修原因', name: 'PurchasePlan_reason', index: 'PurchasePlan_reason', width: 200, sortable: false }
                , { label: '申请人', name: 'PurchasePlan_claimer', index: 'PurchasePlan_claimer', width: 80 }
                , { label: '申请时间', name: 'BuildTime', index: 'BuildTime', formatter: 'date', width: 100, sortable: false }
                , { label: '审核人', name: 'PurchasePlan_auditor', index: 'PurchasePlan_auditor', width: 80 }
                , { label: '审核时间', name: 'PurchasePlan_audit_date', index: 'PurchasePlan_audit_date', formatter: 'date', width: 100, sortable: false }
                , { label: '审核意见', name: 'PurchasePlan_audit_opinion', index: 'PurchasePlan_audit_opinion', width: 300, sortable: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 300, sortable: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                }
                , handleAudit2: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.PurchasePlan_state == "1" && record.PurchasePlan_planstate=='1') {  //1申请通过的 1维修过程中       审核完毕就是完成2
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn
                             , afterFormLoaded: function () {
                                 $("input[name='__type']").val(record._type == '0' ? '设备类' : '车辆类');
                                 $("input[name='__type']").disabled = true;
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_NeedDate', true);
                                 myJqGrid.setFormFieldDisabled('planmoney', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_claimer', true);
                                 myJqGrid.setFormFieldDisabled('BuildTime', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_reason', true);

                                 //审核的选项删选
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='0']").css('display', 'none');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='1']").css('display', 'none');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='2']").css('display', 'block');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='3']").css('display', 'block');
                                 //选中
                                 $("#PurchasePlan_state", "#MyFormDiv").val('3');
                             }
                        });
                     }
                     else {
                         showMessage('提示', "只能审核状态为'领导申请通过'的数据");
                         return;
                     }     
                }
                , handleAudit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.PurchasePlan_state == "0") {  //0 申请的
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn
                             , afterFormLoaded: function () {
                                 $("input[name='__type']").val(record._type == '0' ? '设备类' : '车辆类');
                                 $("input[name='__type']").disabled = true;
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_NeedDate', true);
                                 myJqGrid.setFormFieldDisabled('planmoney', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_claimer', true);
                                 myJqGrid.setFormFieldDisabled('BuildTime', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_reason', true);

                                 //审核的选项删选
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='0']").css('display', 'none');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='1']").css('display', 'block');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='2']").css('display', 'block');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='3']").css('display', 'none');
                                 //选中
                                 $("#PurchasePlan_state", "#MyFormDiv").val('1');
                             }
                        });
                     }
                     else {
                         showMessage('提示', "只能审核状态为'申请'的数据");
                         return;
                     }
                }

            }
    });
}