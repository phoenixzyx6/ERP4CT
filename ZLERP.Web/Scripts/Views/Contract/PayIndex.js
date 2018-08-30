//合同结算付款
function payIndexInit(options) {
    var ContractGrid = new MyGrid({
        renderTo: "Contractid"
            , title: "合同列表"
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: "buttonToolbar"
            , height: gGridWithTitleHeight * 2 / 3
            , dialogWidth: 800
            , dialogHeight: 550
            , storeURL: options.ContractStoreUrl
            , storeCondition: "ContractStatus != '3'"
            , sortByField: "ID"
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
        //, singleSelect: true
            , initArray: [
                 { label: "合同编号", name: "ID", index: "ID", align: "center", width: 70 }
                , { label: "合同名称", name: "ContractName", index: "ContractName" }
                , { label: "客户名称", name: "CustName", index: "Customer.CustName", searchoptions: { sopt: ["cn"]} }
                , { label: "审核状态", name: "AuditStatus", index: "AuditStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "AuditStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("AuditStatus") }, width: 80, align: "center" }
                , { label: "合同状态", name: "ContractStatus", index: "ContractStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractStatus") }, width: 80 }
                 , { label: "累计结算金额", name: "TotalComeMoney", index: "TotalComeMoney", width: 80, align: "right", hidden: true }
                 , { label: "累计结算金额", name: "TotalJSMoney", index: "TotalJSMoney", width: 80, align: "right" }
                , { label: "累计付款金额", name: "PayMoney", index: "PayMoney", width: 80, align: "right" }
                , { label: "欠款金额", name: "Balance", index: "Balance", width: 60, align: "right", hidden: true }
                , { label: "授信额度", name: "CreditMoney", index: "CreditMoney", width: 60, align: "right" }
                , { label: "上次结算日期", name: "FootDate", index: "FootDate", width: 70, formatter: "date", searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"]} }
                , { label: "已受限否", name: "IsLimited", index: "IsLimited", align: "center", width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
                , { label: "合同限制类型", name: "ContractLimitType", index: "ContractLimitType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "CubeLimit" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("CubeLimit") }, width: 80 }
                , { label: "客户编码", name: "CustomerID", index: "CustomerID", hidden: true }
                , { label: '垫资1', name: 'Dianzi1', index: 'Dianzi1', width: 180, hidden: true }
                , { label: '垫资2', name: 'Dianzi2', index: 'Dianzi2', width: 180, hidden: true }
                , { label: '垫资3', name: 'Dianzi3', index: 'Dianzi3', width: 180, hidden: true }
                , { label: '垫资4', name: 'Dianzi4', index: 'Dianzi4', width: 180, hidden: true }
                , { label: '垫资5', name: 'Dianzi5', index: 'Dianzi5', width: 180, hidden: true }
                , { label: '垫资6', name: 'Dianzi6', index: 'Dianzi6', width: 180, hidden: true }
                , { label: '垫资7', name: 'Dianzi7', index: 'Dianzi7', width: 180, hidden: true }
                , { label: '垫资约定', name: 'DianziString', index: 'DianziString', width: 180 }
        //                , { label: "付款日期", name: "PaymentDate", index: "PaymentDate", formatter: "date", searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"]} }
                , { label: "合同号", name: "ContractNo", index: "ContractNo" }
                , { label: "合同样本", name: "Attachments", formatter: attachmentFmt, sortable: false, search: false }
                , { label: "建设单位", name: "BuildUnit", index: "BuildUnit" }
                , { label: "项目地址", name: "ProjectAddr", index: "ProjectAddr" }
                , { label: "签订总方量", name: "SignTotalCube", index: "SignTotalCube", width: 80, align: "right" }
                , { label: "签订日期", name: "SignDate", index: "SignDate", formatter: "date", width: 80, align: "center", searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ["ge"]} }
                , { label: "签订总金额", name: "SignTotalMoney", index: "SignTotalMoney", width: 80, align: "right" }
                , { label: "合同类型", name: "ContractType", index: "ContractType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractType")} }
                , { label: "计价方式", name: "ValuationType", index: "ValuationType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ValuationType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ValuationType")} }
                , { label: "付款方式", name: "PaymentType", index: "PaymentType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "PaymentType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("PaymentType")} }
                , { label: "月结比例（%）", name: "yuejie", index: "yuejie", hidden: true }
                , { label: "审核人", name: "Auditor", index: "Auditor", hidden: true }
                , { label: "审核时间", name: "AuditTime", index: "AuditTime", formatter: "datetime", hidden: true }
                , { label: "审核意见", name: "AuditInfo", index: "AuditInfo", hidden: true }
                , { label: "业务员", name: "Salesman", index: "Salesman" }
                , { label: "甲方联系人", name: "ALinkMan", index: "ALinkMan" }
                , { label: "甲方联系电话", name: "ALinkTel", index: "ALinkTel" }
                , { label: "乙方联系人", name: "BLinkMan", index: "BLinkMan" }
                , { label: "乙方联系电话", name: "BLinkTel", index: "BLinkTel" }
                , { label: "合同开始时间", name: "ContractST", index: "ContractST", formatter: "datetime", searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ["ge"]} }
                , { label: "合同结束时间", name: "ContractET", index: "ContractET", formatter: "datetime", searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ["ge"]} }

                , { label: "回款联系人", name: "PayBackMan", index: "PayBackMan" }
                , { label: "回款联系电话", name: "PayBackTel", index: "PayBackTel" }
                , { label: "垫资方量", name: "MatCube", index: "MatCube" }
                , { label: "预付方量", name: "PrepayCube", index: "PrepayCube" }

                , { label: "备注", name: "Remark", index: "Remark" }
            ]
            , functions: {
                handleReload: function (btn) {
                    ContractGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractGrid.refreshGrid("ContractStatus != '3'");
                },
                handleReport: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }

                    var selectedRecord = ContractGrid.getSelectedRecord();
                    window.open("/Reports/Sales/R311401.aspx?uid=" + selectedRecord.ID + "&jstime=" + selectedRecord.FootDate);
                },
                handleJSTZ: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 400,
                        height: 280,
                        loadFrom: "JSTZForm",
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ContractJSTZ.ContractID", selectrecord.ID);
                            $("#ContractName_project").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            jstzGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        }
                    });
                },
                //垫资
                handleDZ: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 400,
                        height: 280,
                        loadFrom: "DZForm",
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ContractDZ.ContractID", selectrecord.ID);
                            $("#ContractName_project3").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            dzGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        }
                    });
                },
                //付款
                handlePay: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 400,
                        height: 280,
                        loadFrom: "PayForm",
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ContractPay.ContractID", selectrecord.ID);
                            $("#ContractName_project2").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {//刷新
                            payGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                            ContractGrid.refreshGrid("ContractStatus != '3'");
                        }
                    });
                },
                handleConfirm: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 350,
                        height: 150,
                        loadFrom: "ConfirmForm",
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("Contract.ID", selectrecord.ID);
                            $("#ContractName_project1").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            ContractGrid.refreshGrid();
                        }
                    });
                },
                //授信额度
                handleCredit: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();

                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 300,
                        height: 150,
                        loadFrom: "CreditForm",
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("Contract.ID", selectrecord.ID);
                            ContractGrid.setFormFieldValue("Contract.CreditMoney", selectrecord.CreditMoney);
                            $("#C_ContractName").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            ContractGrid.refreshGrid("ContractStatus != '3'");
                        }
                    });
                }

            }
    });
    var jstzGrid = new MyGrid({
        renderTo: "jstzGrid"
            , title: "调整记录"
            , autoWidth: true
            , buttons: buttons1
            , height: gGridWithTitleHeight / 3
            , storeURL: "/ContractJSTZ.mvc/Find"
            , sortByField: "ChangeDate"
            , sortOrder: "ASC"
            , primaryKey: "ID"
            , setGridPageSize: 50
            , showPageBar: false
            , autoLoad: false
            , dialogWidth: 200
            , dialogHeight: 300
            , singleSelect: true
            , editSaveUrl: "/ContractJSTZ.mvc/Update"
            , initArray: [
                { label: "调整编号", name: "ID", index: "ID", hidden: true }
                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
                , { label: "合同名称", name: "ContractName", index: "ContractName", hidden: true }
                , { label: '操作', name: 'act', index: 'act', width: 60, sortable: false, align: "center" }
                , { label: "调整时间", name: "ChangeDate", index: "ChangeDate", width: 80, formatter: "date", editable: true, align: "center" }
                , { label: '调整金额日期范围', name: 'JSDate', index: 'JSDate', width: 100, sortable: false, align: "center" }
                , { label: '调整类型', name: 'AdjustType', index: 'AdjustType', width: 60, sortable: false, align: "center" }
                , { label: "调整金额", name: "ChangeMoney", index: "ChangeMoney", width: 80, editable: true, align: "center" }
                , { label: "备注", name: "Remark", index: "Remark", width: 160, editable: true, align: "center" }
                , { label: "创建人", name: "Builder", index: "Builder", width: 80, align: "center" }

            ]
            , functions: {
                handleReload: function (btn) {
                    jstzGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    jstzGrid.refreshGrid();
                }

            }
    });
    var payGrid = new MyGrid({
        renderTo: "payGrid"
            , title: "付款记录"
            , autoWidth: true
            , buttons: buttons1
            , height: gGridWithTitleHeight / 3
            , storeURL: "/ContractPay.mvc/Find"
            , sortByField: "PayDate"
            , sortOrder: "ASC"
            , primaryKey: "ID"
            , setGridPageSize: 50
            , showPageBar: false
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 400
        //, singleSelect: true
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'act'
            , editSaveUrl: "/ContractPay.mvc/Update"
            , initArray: [
                { label: "编号", name: "ID", index: "ID", hidden: true }
                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
                , { label: "合同名称", name: "ContractName", index: "ContractName", hidden: true }
                , { label: '操作', name: 'act', index: 'act', width: 60, sortable: false, align: "center" }
                , { label: "付款日期", name: "PayDate", index: "PayDate", width: 80, formatter: "date", editable: true, align: "center", sorttype: "datetime" }
                , { label: "付款方式", name: "PayType", index: "PayType", width: 80, editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractPayType") }, edittype: 'select', editoptions: { value: getDicPayTypeStr()} }
                , { label: "付款金额", name: "PayMoney", index: "PayMoney", width: 80, editable: true, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                , { label: "备注", name: "Remark", index: "Remark", width: 160, editable: true, align: "center" }
                , { label: "经办人", name: "Manager", index: "Manager", width: 80, align: "center", editable: true }
                , { label: "创建人", name: "Builder", index: "Builder", width: 80, align: "center" }

            ]
            , functions: {
                handleReload: function (btn) {
                    payGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    payGrid.refreshGrid();

                }

            }
    });
    var dzGrid = new MyGrid({
        renderTo: "dzGrid"
            , title: "合同应收款记录"
            , autoWidth: true
            , buttons: buttons1
            , height: gGridWithTitleHeight / 3
            , storeURL: "/ContractDZ.mvc/Find"
            , sortByField: "DZDate"
            , sortOrder: "ASC"
            , primaryKey: "ID"
            , setGridPageSize: 50
            , showPageBar: false
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 400
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'act'
            , singleSelect: true
            , editSaveUrl: "/ContractDZ.mvc/Update"
            , initArray: [
                { label: "编号", name: "ID", index: "ID", hidden: true }
                 , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                     formatoptions: { keys: true, editbutton: false }
                 }
                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
                , { label: "合同应收款时间", name: "DZDate", index: "DZDate", editable: true, formatter: "date", width: 80 }
                , { label: "合同应收款金额", name: "DZMoney", index: "DZMoney", editable: true, width: 80, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                , { label: "备注", name: "Remark", index: "Remark", width: 160, editable: true, align: "center" }
                , { label: "创建人", name: "Builder", index: "Builder", width: 80, align: "center" }
            ]
            , functions: {
                handleReload: function (btn) {
                    dzGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    dzGrid.refreshGrid();
                }

            }
    });
    var productGrid = new MyGrid({
        renderTo: "productGrid"
            , title: "结算记录"
            , autoWidth: true
            , buttons: buttons1
            , height: gGridWithTitleHeight / 3
            , storeURL: "/ContractJS.mvc/Find"
            , sortByField: "Type,JSDate"
            , sortOrder: "ASC"
            , primaryKey: "ID"
            , setGridPageSize: 50
            , showPageBar: false
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 400
            , singleSelect: true
            , editSaveUrl: "/ContractJS.mvc/Update"
            , initArray: [
                { label: "编号", name: "ID", index: "ID", hidden: true }
                , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
                , { label: "合同名称", name: "ContractName", index: "ContractName", hidden: true, align: "center" }
                , { label: "类型", name: "Type", index: "Type", editable: true, width: 80 }
                , { label: "开始时间", name: "StartJSDate", index: "StartJSDate", editable: true, formatter: "date", width: 80 }
                , { label: "结算时间", name: "JSDate", index: "JSDate", editable: true, formatter: "date", width: 80 }
                , { label: "结算金额", name: "JSMoney", index: "JSMoney", editable: true, width: 80, align: "center" }
                , { label: "推迟结算(月)", name: "BSMoney", index: "BSMoney", editable: true, width: 80, align: "center" }
                , { label: "备注", name: "Remark", index: "Remark", width: 160, editable: true, align: "center" }
                , { label: "创建人", name: "Builder", index: "Builder", width: 80, align: "center" }

            ]
            , functions: {
                handleReload: function (btn) {
                    productGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    productGrid.refreshGrid();
                }

            }
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    ContractGrid.addListeners("rowclick", function (id, record) {
        jstzGrid.refreshGrid("ContractID='" + id + "'");
        payGrid.refreshGrid("ContractID='" + id + "'");
        //shipGrid.refreshGrid("ContractID='" + id + "' and IsAudit=1 and IsEffective=1 and SendCube>0");
        dzGrid.refreshGrid("ContractID='" + id + "'");
        productGrid.refreshGrid("ContractID='" + id + "'");
    });
    //加载完成后事件
    ContractGrid.addListeners("gridComplete", function () {
        var ids = ContractGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var record = ContractGrid.getRecordByKeyValue(ids[i]);
            var cmoney = record.CreditMoney;
            var bmoney = record.Balance;
            var c = bmoney * 1 - cmoney * 1;

            if (c > 0) {
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "#F1BDBD"; //设置行背景颜色
            }

        }
        
    });
    dzGrid.addListeners("gridComplete", function () {
        dzGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ContractDZ.mvc/Delete" });
    });
    
    productGrid.addListeners("gridComplete", function () {
       productGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ContractJS.mvc/Delete" });
    });

    jstzGrid.addListeners("cellclick", function (id, record) {
        var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
        $("#" + rowno + "_ChangeDate").datepicker();
    });
    //获取调整类型列表 lzl add 2016-10-28
    function getDicAdjustTypeStr() {
        //动态生成select内容
        var requestURL = "/Dic.mvc/FindDicsList";
        var postParams = { nodeid: "ContractAccAdjust" };
        var str = "";

        $.ajax({
            type: 'post',
            async: false,
            url: requestURL,
            data: postParams,
            success: function (data) {
                if (data != null) {
                    var jsonobj = eval(data);
                    var length = jsonobj.length;
                    for (var i = 0; i < length; i++) {
                        if (i != length - 1) {
                            str += jsonobj[i].ID + ":" + jsonobj[i].DicName + ";";
                        } else {
                            str += jsonobj[i].ID + ":" + jsonobj[i].DicName;
                        }
                    }
                }
                //return str;
            }
        });

        return str;
    }
    //单元格编辑提交后发生事件
    jstzGrid.addListeners("afterSubmitCell", function () {
        ContractGrid.refreshGrid();

    });


    //cellclick 事件定义，选择日期列单元格弹出日期选择控件 (lzl add 2016-07-26)
    payGrid.addListeners("cellclick", function (id, record) {
        var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
        $("#" + rowno + "_PayDate").datepicker();
    });
    //获取付款方式列表 lzl add 2016-07-28
    function getDicPayTypeStr() {
        //动态生成select内容
        var requestURL = "/Dic.mvc/FindDicsList";
        var postParams = { nodeid: "ContractPayType" };
        var str = "";

        $.ajax({
            type: 'post',
            async: false,
            url: requestURL,
            data: postParams,
            success: function (data) {
                if (data != null) {
                    var jsonobj = eval(data);
                    var length = jsonobj.length;
                    for (var i = 0; i < length; i++) {
                        if (i != length - 1) {
                            str += jsonobj[i].ID + ":" + jsonobj[i].DicName + ";";
                        } else {
                            str += jsonobj[i].ID + ":" + jsonobj[i].DicName;
                        }
                    }
                }
                //return str;
            }
        });
        return str;
    }
    //单元格编辑提交后发生事件
    payGrid.addListeners("afterSubmitCell", function () {
        ContractGrid.refreshGrid();

    });

    //    var shipGrid = new MyGrid({
    //        renderTo: "shipGrid"
    //            , title: "生产记录"
    //            , autoWidth: true
    //            , buttons: buttons1
    //            , height: gGridWithTitleHeight / 3
    //            , storeURL: "/Shippingdocument.mvc/Find"
    //            , storeCondition: "IsAudit= 1 and IsEffective=1"
    //            , sortByField: "ID"
    //            , sortOrder: "ASC"
    //            , primaryKey: "ID"
    //            , setGridPageSize: 50
    //            , groupingView: { groupSummary: [true], groupText: ['{0}({1})'] }
    //            , groupField: 'ConStrength'
    //            , autoLoad: false
    //            , dialogWidth: 600
    //            , dialogHeight: 400
    //            , editSaveUrl: "/Shippingdocument.mvc/Update"
    //            , initArray: [
    //                { label: "运输单号", name: "ID", index: "ID", width: 80 }
    //                , { label: "是否结算", name: "IsJS", index: "IsJS", width: 50, formatter: booleanFmt, formatterStyle: { '0': '否', '1': '是' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
    //                , { label: '生产费用', name: 'JSPirce', width: 60, align: 'right', editable: true, summaryType: 'sum', summaryTpl: '{0}' }
    //                , { label: '泵送费', name: 'PumpPrice', width: 60, align: 'right', editable: true, summaryType: 'sum', summaryTpl: '{0}' }
    //                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
    //                , { label: "是否审核", name: "IsAudit", index: "IsAudit", hidden: true }
    //                , { label: "是否有效", name: "IsEffective", index: "IsEffective", hidden: true }
    //                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 80, formatter: 'date' }
    //                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
    //                , { label: '客户名称', name: 'CustName', index: 'CustName' }
    //                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
    //                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 50, align: 'right' }
    //                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
    //                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
    //                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 60 }
    //            ]
    //            , functions: {
    //                handleReload: function (btn) {
    //                    shipGrid.reloadGrid("IsAudit=1 and IsEffective=1 and SendCube>0");
    //                },
    //                handleRefresh: function (btn) {
    //                    shipGrid.refreshGrid("IsAudit=1 and IsEffective=1 and SendCube>0");
    //                }

    //            }
    //    });

    //加载完成后事件
    //        productGrid.addListeners("gridComplete", function (id, records) {
    //     
    //           
    //            //            var summoney = 0;
    //            //            var records = productGrid.getAllRecords();
    //            //            for (var i = 0; i < records.length; i++) {
    //            //                summoney = summoney*1 + records[i].ProductMoney*1;
    //            //            }
    //            
    //            var sumAcount = parseFloat($("#productGrid").getCol("ProductMoney", false, 'sum')).toFixed(4);
    //            sumAcount = 100;
    //            $(".ui-jqgrid-sdiv").show();
    //            $("#gview_productGrid-datagrid").footerData("set", { ProductMoney: sumAcount });

    //        });

    jstzGrid.addListeners("gridComplete", function () {

        var ids = jstzGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteJSTZ(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
            jstzGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

        }

    });

    payGrid.addListeners("gridComplete", function () {

        var ids = payGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeletePay(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
            payGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

        }

    });


    window.handleDeleteJSTZ = function (id) {
        showConfirm("确认信息", "您确定删除此项结算记录？", function () {
            $.post(
                    "/ContractJSTZ.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        jstzGrid.reloadGrid();
                    }

                );
            $(this).dialog("close");
        });
    };

    window.handleDeletePay = function (id) {
        showConfirm("确认信息", "您确定删除此项付款记录？", function () {
            $.post(
                    "/ContractPay.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        payGrid.reloadGrid();
                        ContractGrid.refreshGrid("ContractStatus != '3'");
                    }

                );
            $(this).dialog("close");
        });
    };
} 