function PurchasePlanIndexInit(options) {

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
            , dialogHeight: 320
		    , showPageBar: true
		    , initArray: [
                { label: '计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '采购需求日期', name: 'PurchasePlan_NeedDate', index: 'PurchasePlan_NeedDate', formatter: 'date', width: 100, sortable: false }
                , { label: '物资ID', name: 'GoodsID', index: 'GoodsID', width: 100, hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsName', width: 100 }
                 , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 80 }
                , { label: '物资类型', name: 'GoodsTypeID', index: 'GoodsTypeID', width: 80, hidden: true }
                , { label: '物资类型名称', name: 'GoodsType', index: 'GoodsInfo.GoodsType', width: 80, hidden: false }
                , { label: '单位', name: 'Unit', index: 'Unit', width: 80 }
                , { label: '单价', name: 'planprice', index: 'planprice', width: 80 }
                , { label: '数量', name: 'PurchasePlan_num', index: 'PurchasePlan_num', width: 80 }
                , { label: '预估价格', name: 'planmoney', index: 'planmoney', width: 80 }
                , { label: '当前库存', name: 'GoodsNum', index: 'GoodsNum', width: 80 }
                , { label: '事由', name: 'PurchasePlan_reason', index: 'PurchasePlan_reason', width: 200, sortable: false }
                , { label: '计划执行状态', name: 'PurchasePlan_planstate', index: 'PurchasePlan_planstate', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PurchasePlan_planstate' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('PurchasePlan_planstate') }, width: 80 }
                , { label: '申请人', name: 'PurchasePlan_claimer', index: 'PurchasePlan_claimer', width: 80 }
                , { label: '申请时间', name: 'BuildTime', index: 'BuildTime', formatter: 'date', width: 100, sortable: false }
                , { label: '物资审核人', name: 'goodsername', index: 'goodsername', width: 80 }
                , { label: '物资审核时间', name: 'goodsertime', index: 'goodsertime', formatter: 'date', width: 100, sortable: false }
                , { label: '审核状态', name: 'PurchasePlan_state', index: 'PurchasePlan_state', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PurchasePlan_state' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('PurchasePlan_state') }, width: 80 }
                , { label: '审核时间', name: 'PurchasePlan_audit_date', index: 'PurchasePlan_audit_date', formatter: 'date', width: 100, sortable: false }
                , { label: '审核人', name: 'PurchasePlan_auditor', index: 'PurchasePlan_auditor', width: 80 }
               //, { label: '审核意见', name: 'PurchasePlan_audit_opinion', index: 'PurchasePlan_audit_opinion', width: 300, sortable: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 300, sortable: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleAuditingALL: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    };

                    //修整ID
                    var ids = new Array();

                    //累计预估价格
                    var datas = myJqGrid.getSelectedRecords();
                    var money = 0.00;
                    var j = 0;
                    for (var i = 0; i < datas.length; i++) {
                        if (datas[i].PurchasePlan_state == "4") {
                            money += datas[i].planmoney * 1;
                            ids[j++] = datas[i].ID;
                        }
                    }
                    $('#leiji').val(money);
                    $('#leiji')[0].disabled = true;

                    myJqGrid.showWindow({
                        btn: btn,
                        height: 200,
                        title: '物资审核',
                        loadFrom: 'ConfirmDiv',
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "保存": function () {

                                //附加额外的参数
                                ajaxRequest(btn.data.Url, { ids: ids }, true, function (response) {
                                    $(btn.currentTarget).button({ disabled: false });
                                    var closeDialog = true;
                                    if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                        closeDialog = false;
                                    }
                                    //窗口关闭处理                        
                                    if (response.Result && closeDialog) {
                                        $("#ConfirmDiv").dialog('close');
                                        $("#ConfirmDiv form")[0].reset();
                                    }
                                    myJqGrid.refreshGrid();
                                });

                            }
                        }
                    });
                },
                handleSubmitInfo: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要提交的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.PurchasePlan_planstate == "0") {
                        ajaxRequest(btn.data.Url, { id: record.ID }, true, function (response) {
                            if (response.Result) {
                                myJqGrid.reloadGrid();
                            }
                        });
                    }
                    else {
                        showMessage('提示', "不能重复提交!");
                        return;
                    }
                }
                , handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                //新增
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                        , beforeSubmit: function () {
                            return IsExistGoods();
                        }
                        , afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('GoodsModel', true);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_state', true);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_NeedDate', false);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_num', false);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_reason', false);

                            //预估价格 单价
                            //                            $("#planmoney", "#MyFormDiv")[0].disabled = true;
                            //                            $("#planprice", "#MyFormDiv")[0].disabled = true;

                            //审核的选项删选
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='0']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='1']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='2']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='3']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='4']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='5']")).css('display', 'block');
                            //选中
                            $("#PurchasePlan_state", "#MyFormDiv").val('0');

                            myJqGrid.setFormFieldDisabled('ContentNum', true);
                            //                            GoodsInfoChange();
                            $("#w1", "#MyFormDiv").css('display', 'block');
                            $("#w2", "#MyFormDiv").css('display', 'none');
                        }
                    });
                },
                //修改
                handleEdit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要修改的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.PurchasePlan_planstate == "0") {
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn
                            , beforeSubmit: function () {
                                return IsExistGoods();
                            }
                        , afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('GoodsModel', true);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_state', true);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_NeedDate', false);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_num', false);
                            myJqGrid.setFormFieldDisabled('PurchasePlan_reason', false);

                            //预估价格 单价
                            //                            $("#planmoney", "#MyFormDiv")[0].disabled = true;
                            //                            $("#planprice", "#MyFormDiv")[0].disabled = true;

                            //审核的选项删选
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='0']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='1']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='2']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='3']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='4']")).css('display', 'block');
                            $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='5']")).css('display', 'block');
                            //选中
                            $("#PurchasePlan_state", "#MyFormDiv").val('0');

                            myJqGrid.setFormFieldDisabled('ContentNum', true);

                            $("#w1", "#MyFormDiv").css('display', 'block');
                            $("#w2", "#MyFormDiv").css('display', 'none');

                            //数量
                            var str = $("input[name='GoodsID']").val();
                            ajaxRequest("/GoodsInfo.mvc/Get", { id: str }, false, function (response) {
                                if (response.Result) {

                                    $('#ContentNum').val(response.Data == null ? "" : response.Data.ContentNum);
                                }
                            });
                        }
                        });
                    }
                    else {
                        showMessage('提示', "正在流程中的数据不能修改！");
                        return;
                    }
                }
                //删除
                , handleDelete: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要提交的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.PurchasePlan_planstate == "0") {//计划执行状态为0 未提交的
                        myJqGrid.deleteRecord({
                            deleteUrl: btn.data.Url
                        });
                    }
                    else {
                        showMessage('提示', "不能删除已提交数据!");
                        return;
                    }

                }
                , handleAudit1: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要审核的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.PurchasePlan_state == "4") {//审核状态为 1 提交的
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn
                             , afterFormLoaded: function () {
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_state', false);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_NeedDate', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_num', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_reason', true);

                                 //预估价格 单价
                                 //                                 $("#planmoney", "#MyFormDiv")[0].disabled = true;
                                 //                                 $("#planprice", "#MyFormDiv")[0].disabled = true;

                                 //审核的选项删选
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='0']").css('display', 'none');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='1']").css('display', 'none');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='2']").css('display', 'block');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='3']").css('display', 'block');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='4']").css('display', 'none');
                                 $("#PurchasePlan_state", "#MyFormDiv").children("option[value='5']").css('display', 'none');
                                 //选中
                                 $("#PurchasePlan_state", "#MyFormDiv").val('2');

                                 $("#w1", "#MyFormDiv").css('display', 'none');
                                 $("#w2", "#MyFormDiv").css('display', 'block');
                                 myJqGrid.setFormFieldDisabled('GoodsName1', true);
                                 myJqGrid.setFormFieldDisabled('ContentNum', true);
                                 $('#GoodsName1').val($("input[name='GoodsName']").val().substring(0, $("input[name='GoodsName']").val().indexOf('[')));

                                 //数量
                                 var str = $("input[name='GoodsID']").val();
                                 ajaxRequest("/GoodsInfo.mvc/Get", { id: str }, false, function (response) {
                                     if (response.Result) {
                                         $('#ContentNum').val(response.Data == null ? "" : response.Data.ContentNum);
                                     }
                                 });
                             }
                        });
                    }
                    else {
                        showMessage('提示', "只能审核状态为'物资员审核通过'的数据");
                        return;
                    }

                }
                //审核
                 , handleAuditByGoods: function (btn) {
                     var keys = myJqGrid.getSelectedKeys();
                     if (keys.length == 0) {
                         showMessage('提示', "请选择要审核的记录!");
                         return;
                     }
                     var record = myJqGrid.getSelectedRecord();
                     if (record.PurchasePlan_state == "1") {//审核状态为 1 提交的
                         myJqGrid.handleEdit({
                             loadFrom: 'MyFormDiv',
                             btn: btn
                             , afterFormLoaded: function () {
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_state', false);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_NeedDate', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_num', true);
                                 myJqGrid.setFormFieldDisabled('PurchasePlan_reason', true);

                                 //预估价格 单价
                                 //                                 $("#planmoney", "#MyFormDiv")[0].disabled = true;
                                 //                                 $("#planprice", "#MyFormDiv")[0].disabled = true;

                                 //审核的选项删选
                                 $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='0']")).css('display', 'none');
                                 $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='1']")).css('display', 'none');
                                 $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='2']")).css('display', 'none');
                                 $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='3']")).css('display', 'none');
                                 $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='4']")).css('display', 'block');
                                 $($("#PurchasePlan_state", "#MyFormDiv").children("option[value='5']")).css('display', 'block');
                                 //选中
                                 $("#PurchasePlan_state", "#MyFormDiv").val('4');

                                 $("#w1", "#MyFormDiv").css('display', 'none');
                                 $("#w2", "#MyFormDiv").css('display', 'block');
                                 myJqGrid.setFormFieldDisabled('GoodsName1', true);
                                 myJqGrid.setFormFieldDisabled('ContentNum', true);
                                 $('#GoodsName1').val($("input[name='GoodsName']").val().substring(0, $("input[name='GoodsName']").val().indexOf('[')));

                                 //数量
                                 var str = $("input[name='GoodsID']").val();
                                 ajaxRequest("/GoodsInfo.mvc/Get", { id: str }, false, function (response) {
                                     if (response.Result) {
                                         $('#ContentNum').val(response.Data == null ? "" : response.Data.ContentNum);
                                         $('#Unit').val(response.Data == null ? "" : response.Data.unit); //单位
                                     }
                                 });
                             }
                         });
                     }
                     else {
                         showMessage('提示', "只能审核状态为'提交'的数据");
                         return;
                     }
                 }
            }
    });
         //判断是否物资信息存在此物品
         IsExistGoods = function () {
             
             var ischeck = false;
             var str = $("input[name='GoodsID']").val();
             $.ajax({
                 url: "/GoodsInfo.mvc/Get",
                 type: 'POST',
                 json: 'json',
                 async: false, //必须是同步
                 data: { id: str },
                 traditional: true,
                 success: function (response) {
                     if (response.Result) {
                         ischeck = true;
                     }
                     else {
                         ischeck = false;
                     }
                 },
                 error: function (response) {
                     ischeck = false;
                 }
             });

             if (ischeck) {
                 return true;
             }
             else {
                 showError('提示', "请选择物资信息里面存在的物品,不能手动输入!");
                 return false;
             }
         }
    //         GoodsInfoChange = function () {
    //             var GoodsField = myJqGrid.getFormField("GoodsID");
    //                          GoodsField.unbind('change');
    //                          GoodsField.bind('change', function (event) {
    //                              var goods = GoodsField.val();

    //                              if (!isEmpty(goods)) {
    //                                  var requestURL = opts.getGoodsUrl;
    //                                  var postParams = { id: goods };
    //                                  var rData;
    //                                  ajaxRequest(requestURL, postParams, false, function (response) {
    //                                      if (response) {
    //                                          rData = response.Data;
    //                                          myJqGrid.getFormField("ContentNum").val(response.Data == null ? "" : rData.ContentNum);
    //                                      }
    //                                  });
    //                              }
    //                          });
    //                      }

    $(function () {
        //文本更改
        $("input[name='GoodsID']").blur(function () {
            var str = $(this).val();
            if (!isEmpty(str)) {
                str = str.substring(str.indexOf('[') + 1, str.indexOf(']'));
                ajaxRequest("/GoodsInfo.mvc/Get", { id: str }, false, function (response) {
                    if (response.Result) {
                        $('#ContentNum').val(response.Data == null ? "" : response.Data.ContentNum);
                        $('#Unit').val(response.Data == null ? "" : response.Data.unit); //单位
                        $('#GoodsModel').val(response.Data == null ? "" : response.Data.GoodsModel); //型号
                        
                    }
                });

                //加载单价
                ajaxRequest("/Purchase.mvc/GetLastPrice", { id: str }, false, function (response) {
                    if (response.Result) {
                        $('#planprice').val(response.Data == null ? "" : response.Data);
                        var fiel = $('#PurchasePlan_num');
                        if (isEmpty(fiel.val()))
                        { }
                        else {
                            //总价planmoney=fiel购买数量*单价planprice
                            $('#planmoney').val(fiel.val() * $('#planprice').val());
                        }
                    }
                });
            }
        });

        //采购数量
        $("#PurchasePlan_num").blur(function () {
            var fiel = $('#planprice');
            if (isEmpty(fiel.val()))
            { }
            else {
                $('#planmoney').val(fiel.val() * $(this).val());
            }
        });
        //采购数量
        $("#planprice").blur(function () {
            var fiel = $('#PurchasePlan_num');
            if (isEmpty(fiel.val()))
            { }
            else {
                $('#planmoney').val(fiel.val() * $(this).val());
            }
        });
    })


    //        $("#PartID").bind("change", function (select) {
    //            var partid = $(this).val();
    //            var params = { id: partid };
    //            ajaxRequest(options.getUrl, params, false, function (response) {
    //                if (response.Result) {
    //                    var data = response.Data;
    //                    $('#Inventory').val(data.Inventory);
    //                    $('#UnitID').val(data.UnitID);
    //                    //alert(data.UnitID); Inventory
    //                }
    //            });
    //        });

}