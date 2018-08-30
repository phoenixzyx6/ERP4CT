//合同管理
function contractIndexInit(options) {
    $("#mailinfo").slideDown();
    $("#jjxx").toggle(
            function () {
                $(this).text("详细信息");
                $("#mailinfo").slideUp();
                $("#ContractForm").height(355);
                $("#mailinfo").toggle();
                $(this).siblings("span").toggleClass('ui-icon-arrowthickstop-1-s');
            },
            function () {
                $(this).text("精简信息");
                $("#mailinfo").toggle();
                $("#ContractForm").height(520);
                $("#mailinfo").slideDown();
                
                $(this).siblings("span").toggleClass('ui-icon-arrowthickstop-1-s');
            }
        );

    //重置表单样式
    function resetClass(obj) {
        if (!$.isEmptyObject(obj)) {
            $.each(obj, function (i, n) {
                if (n == "removeClass") {
                    $("input[name='" + i + "']").removeClass("text requiredval");
                } else {
                    $("input[name='" + i + "']").addClass("text requiredval");
                }
            });
        }
    }
    
    //合同表单提交之前触发的事件
    function beforeContractFormSubmit() {
        resetClass({
            "Contract.ContractST": "removeClass",
            "Contract.ContractET": "removeClass",
            "Contract.MatCube": "removeClass",
            "Contract.PrepayCube": "removeClass"
        });
        var contractLimitTypeVal = ContractGrid.getFormFieldValue("Contract.ContractLimitType");//合同限制类型
        var contractST = ContractGrid.getFormFieldValue("Contract.ContractST");//合同开始时间
        var contractET = ContractGrid.getFormFieldValue("Contract.ContractET");//合同结束时间
        var contractMatCube = ContractGrid.getFormFieldValue("Contract.MatCube");//合同垫资方量
        var contractPrepayCube = ContractGrid.getFormFieldValue("Contract.PrepayCube"); //合同预付方量
        switch (contractLimitTypeVal) {
            case limitObj.limit0://不受限制
                break;
            case limitObj.limit1: //受合同起止时间限制
                if (!isEmpty(contractST) && !isEmpty(contractET)) {
                    if (Date.parse(contractST.replace(/\-/g, "\/")) > Date.parse(contractET.replace(/\-/g, "/"))) {
                        $("input[name='Contract.ContractET']").addClass("input-validation-error");
                        showError("验证错误", "合同结束时间必须大于合同开始时间");
                        return false;
                    }

                } else {
                    if (contractST == "" || isEmpty(contractST)) {
                        $("input[name='Contract.ContractST']").addClass("input-validation-error");
                        showError("验证错误", "请根据合同限制类型选择合同开始时间");
                        return false;
                    }
                    if (contractET == "" || isEmpty(contractET)) {
                        $("input[name='Contract.ContractET']").addClass("input-validation-error");
                        showError("验证错误", "请根据合同限制类型选择合同结束时间");
                        return false;
                    }
                }
                break;
            case limitObj.limit2: //受垫资方量限制
                if (contractMatCube == "") {
                    $("input[name='Contract.MatCube']").addClass("input-validation-error");
                    showError("验证错误", "请根据合同限制类型设定垫资方量");
                    return false;
                }
                break;
            case limitObj.limit3: //受预付方量限制
                if (contractPrepayCube == "") {
                    $("input[name='Contract.PrepayCube']").addClass("input-validation-error");
                    showError("验证错误", "请根据合同限制类型设定预付方量");
                    return false;
                }
                break;
            default:
                break;
                
        }
        return true;
    }
    var ContractGrid = new MyGrid({
        renderTo: "Contractid"
            , title: "合同列表"
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: "buttonToolbar"
            , height: gGridWithTitleHeight
            , dialogWidth: 760
            , dialogHeight: 440
            , storeURL: options.ContractStoreUrl
            , storeCondition: "ContractStatus != '3'"
            , sortByField: "ID"
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
        //, singleSelect: true
            , initArray: [
                 { label: "合同编号", name: "ID", index: "ID", align: "center", width: 80 }
                , { label: "客户名称", name: "CustName", index: "Customer.CustName", width: 200, searchoptions: { sopt: ["cn"]} }
                , { label: "合同名称", name: "ContractName", index: "ContractName" }
                , { label: "审核状态", name: "AuditStatus", index: "AuditStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "AuditStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("AuditStatus") }, width: 100, align: "center" }
                , { label: "已受限否", name: "IsLimited", index: "IsLimited", align: "center", width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: "select", searchoptions: { value: booleanSearchValues()} }
                , { label: "合同状态", name: "ContractStatus", index: "ContractStatus", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractStatus" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractStatus") }, width: 80 }
                , { label: "业务类型", name: "BusinessType", index: "BusinessType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "BusinessType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("BusinessType") }, width: 80 }
                , { label: "合同限制类型", name: "ContractLimitType", index: "ContractLimitType", formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "CubeLimit" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("CubeLimit") }, width: 80 }
                , { label: "客户编码", name: "CustomerID", index: "CustomerID", hidden: true }
                , { label: '垫资1', name: 'Dianzi1', index: 'Dianzi1', width: 180, hidden: true }
                , { label: '垫资2', name: 'Dianzi2', index: 'Dianzi2', width: 180, hidden: true }
                , { label: '垫资3', name: 'Dianzi3', index: 'Dianzi3', width: 180, hidden: true }
                , { label: '垫资4', name: 'Dianzi4', index: 'Dianzi4', width: 180, hidden: true }
                , { label: '垫资5', name: 'Dianzi5', index: 'Dianzi5', width: 180, hidden: true }
                , { label: '垫资6', name: 'Dianzi6', index: 'Dianzi6', width: 180, hidden: true }
                , { label: '垫资7', name: 'Dianzi7', index: 'Dianzi7', width: 180, hidden: true }
                , { label: '垫资7', name: 'JsCoefficient', index: 'JsCoefficient', width: 180, hidden: true }
                , { label: '垫资7', name: 'QyCoefficient', index: 'QyCoefficient', width: 180, hidden: true }
                , { label: '垫资7', name: 'HkCoefficient', index: 'HkCoefficient', width: 180, hidden: true }
                , { label: '垫资7', name: 'DeductPerPrice', index: 'DeductPerPrice', width: 180, hidden: true }
                , { label: '垫资约定', name: 'DianziString', index: 'DianziString', width: 180 }
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
                , { label: "结账日期", name: "FootDate1", index: "FootDate1" }
                , { label: "付款日期", name: "PaymentDate1", index: "PaymentDate1"}
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
                handleAdd: function (btn) {
                    ContractGrid.handleAdd({
                        btn: btn,
                        loadFrom: "ContractForm",
                        beforeSubmit: beforeContractFormSubmit,
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(response.Data); //上传附件
                            }
                        },
                        afterFormLoaded: function () {
                            $("#Attachments").empty();
                        }

                    });
                },
                handleEdit: function (btn) {
                    var data = ContractGrid.getSelectedRecord();
                    var auditValue = data.AuditStatus;
                    if (auditValue == 1) {
                        showMessage("提示", "已审核的数据不能修改");
                        return;
                    }
                    ContractGrid.handleEdit({
                        btn: btn,
                        loadFrom: "ContractForm",
                        prefix: "Contract",
                        beforeSubmit: beforeContractFormSubmit,
                        afterFormLoaded: function () {
                            var attDiv = $("#Attachments");
                            attDiv.empty();
                            attDiv.append(data["Attachments"]);
                            $("a[att-id]").show();
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(data.ID);
                            }
                        }
                    });
                }
                , handleDelete: function (btn) {
                    ContractGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                //添加合同明细
                handleAddContractItem: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作！");
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();
                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
                        showMessage("提示", "已完工的合同不能再添加合同明细");
                        return;
                    }
                    ContractGrid.handleAdd({
                        title: "添加合同明细",
                        width: 400,
                        height: 250,
                        loadFrom: "ContractItemForm",
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ContractItem.ContractID", selectrecord.ID);
                            $("#ContractName_span").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            ContractItemGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        }
                    });
                },
                //添加工程明细
                handleAddProject: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作！");
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();
                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
                        showMessage("提示", "已完工的合同不能再设置工程明细");
                        return;
                    }
                    ContractGrid.handleAdd({
                        btn: btn,
                        width: 500,
                        height: 350,
                        loadFrom: "/Project.mvc/Index #projectForm form",
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("ContractID", selectrecord.ID);
                            $("#ContractName_project").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            ProjectGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        }
                    });
                },
                //其他加价设定
                handleOtherPrice: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作！");
                        return;
                    }
                    var selectedRecord = ContractGrid.getSelectedRecord();
                    ContractItemGrid.showWindow({
                        title: "其他加价设定",
                        width: 650,
                        height: 400,
                        loadFrom: "ContractOtherpriceGridAndForm",
                        afterLoaded: function () {
                            $("input[name='ContractOtherPrice.ContractID']").val(selectedRecord.ID);
                            //$("#DisplayContractItemsID").html("<font color=red>" + id + "</font>");

                            contractOtherpriceGrid.getJqGrid().jqGrid("setGridParam", { editurl: options.otherPriceRowDeleteUrl });

                            //contractOtherpriceGrid.getJqGrid().jqGrid("setGridWidth", 450);
                            contractOtherpriceGrid.refreshGrid("ContractID='" + selectedRecord.ID + "'");

                            //计算方式改变时，对应的数量与百分比描述变化
                            var CalcTypelist = $("select[name='ContractOtherPrice.CalcType']");
                            CalcTypelist.unbind("change");
                            CalcTypelist.bind("change", function () {
                                if ($(this).val() == "百分比") {
                                    $("#amountorper label").text("百分比（%）");
                                } else {
                                    $("#amountorper label").text("数量");
                                }
                            });
                        }
                        , buttons: {
                            "关闭": function () {
                                $(this).dialog("close");
                            }
                        }
                    });
                },

                //已完工合同
                handleCompleted: function () {
                    ContractGrid.refreshGrid("ContractStatus = 3");
                },
                //未完工合同
                handleNotCompleted: function () {
                    ContractGrid.refreshGrid("ContractStatus != 3 and IsLimited = 0");
                },
                //受限合同
                handleLimitBy: function () {
                    //modify by xyl on 2012-4-9 去除ContractLimitType不为不受限制这个条件
                    //ContractGrid.refreshGrid("ContractLimitType !='" + limitObj.limit0 + "' and IsLimited = 1");
                    ContractGrid.refreshGrid("IsLimited = 1");
                },
                handleSetComplete: function (btn) {
                    setcmp(btn, ContractGrid, true);
                },
                //                handleSetUnComplete: function (btn) {
                //                    setuncmp(btn, ContractGrid, true);
                //                },
                handleImport: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作！");
                        return;
                    }
                    var selectrecord = ContractGrid.getSelectedRecord();
                    if (selectrecord.ContractStatus == 3) {//状态为3表示已完工，取自字典表
                        showMessage("提示", "已完工的合同不能再添加合同明细");
                        return;
                    }
                    ContractGrid.handleAdd({
                        title: "导入合同明细",
                        width: 400,
                        height: 300,
                        loadFrom: "ImportForm",
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            ContractGrid.setFormFieldValue("contractID", selectrecord.ID);
                            $("#ContractNameForImportForm").html(selectrecord.ContractName);
                        }
                        , postCallBack: function (response) {
                            ContractItemGrid.refreshGrid("ContractID='" + selectrecord.ID + "'");
                        }
                    });
                },
                handleAddProduceTask: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条合同进行操作！");
                        return;
                    }
                    if (!ProjectGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条工程明细进行操作！");
                        return;
                    }
                    var contractrecord = ContractGrid.getSelectedRecord();
                    var projectrecord = ProjectGrid.getSelectedRecord();
                    if (contractrecord.AuditStatus != "1") {
                        showMessage("提示", "合同未审核或审核不通过！");
                        return;
                    }
                    ProjectGrid.handleAdd({
                        btn: btn,
                        width:700,
                        loadFrom: "ProduceTaskForm",
                        afterFormLoaded: function () {
                            ProjectGrid.setFormFieldValue("ProduceTask.CustName", contractrecord.CustName);
                            ProjectGrid.setFormFieldValue("ProduceTask.ContractName", contractrecord.ContractName);
                            ProjectGrid.setFormFieldValue("ProduceTask.ContractID", contractrecord.ID);
                            ProjectGrid.setFormFieldValue("ProduceTask.ConstructUnit", contractrecord.CustName);
                            ProjectGrid.setFormFieldValue("ProduceTask.ProjectName", projectrecord.ProjectName);
                            ProjectGrid.setFormFieldValue("ProduceTask.ProjectID", projectrecord.ID);
                            ProjectGrid.setFormFieldValue("ProduceTask.ProjectAddr", projectrecord.ProjectAddr);
                            ProjectGrid.setFormFieldValue("ProduceTask.BuildUnit", projectrecord.BuildUnit);
                            if (!isEmpty(projectrecord.ConstructUnit))
                                ProjectGrid.setFormFieldValue("ProduceTask.ConstructUnit", projectrecord.ConstructUnit);
                            ProjectGrid.setFormFieldValue("ProduceTask.LinkMan", projectrecord.LinkMan);
                            ProjectGrid.setFormFieldValue("ProduceTask.Tel", projectrecord.Tel);
                            bindSelectData(ProjectGrid.getFormField("ProduceTask.ContractItemsID"),
                                options.listContractItemUrl,
                                { textField: "ConStrength",
                                    valueField: "ID",
                                    orderBy: "ConStrength",
                                    ascending: true,
                                    condition: "ContractID='" + contractrecord.ID + "'"
                                }
                            );
                            //合同明细Change事件
                            var contractItemsField = ProjectGrid.getFormField("ProduceTask.ContractItemsID");
                            contractItemsField.unbind("change");
                            contractItemsField.bind("change", function (event) {
                                var cid = contractItemsField.val(); //alert();
                                bindIdentitySettings("#ProduceTaskFormRightDiv", cid);
                            });
                            //施工部位类型Change事件
                            var consPosTypeField = ProjectGrid.getFormField("ProduceTask.ConsPosType");
                            consPosTypeField.unbind("change");
                            consPosTypeField.bind("change", function (event) {
                                ProjectGrid.getFormField("ProduceTask.ConsPos").val(consPosTypeField.val());
                            });
                            //前场工长Change事件
                            var linkManField = ProjectGrid.getFormField("ProduceTask.LinkMan");
                            linkManField.unbind("change");
                            linkManField.bind("change", function (event) {
                                var linkMan = linkManField.val();
                                var selectedLinkMan = $("#ProduceTask_LinkMan", "#" + ProjectGrid.getFormId()).children(":selected").text();
                                var linkManId = $("#ProduceTask_LinkMan", "#" + ProjectGrid.getFormId()).val();
                                ProjectGrid.setFormFieldValue("ProduceTask.Tel", "");

                                if (linkManId && linkMan == selectedLinkMan) {
                                    var requestURL = "User.mvc/Get";
                                    var postParams = { id: linkManId };
                                    var rData;
                                    ajaxRequest(requestURL, postParams, false, function (response) {
                                        if (response) {
                                            rData = response.Data;
                                            ProjectGrid.getFormField("ProduceTask.Tel").val(response.Data == null ? "" : rData.Tel);
                                        }
                                    });
                                }
                            });
                        },
                        closeDialog: false,
                        postCallBack: function (response) {
                            //保存特性
                            if (response.Result) {
                                var uid = response.Data;
                                var postUrl = options.saveTaskIdentitiesUrl;

                                var identities = [];
                                $("#ProduceTaskFormRightDiv input:checked").each(function () {
                                    identities.push($(this).val());
                                });

                                ajaxRequest(postUrl, { taskId: uid, identities: identities }, false, function (response) {
                                    $("#ProduceTaskForm").dialog("close");
                                });
                            }
                        }
                    });
                },
                handlePrintCotract: function (btn) {
                    if (!ContractGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一个合同进行操作！");
                        return;
                    }
                    if (!ProjectGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一个合同工程进行操作！");
                        return;
                    }
                    var contractrecord = ContractGrid.getSelectedRecord();
                    var projectrecord = ProjectGrid.getSelectedRecord();
                    var path = "/Reports/ContractData";
                    var tplPath = path + "/Contract.aspx?customerid=" + contractrecord.CustomerID + "&contractid=" + projectrecord.ContractID + "&projectid=" + projectrecord.ID;
                    window.open(tplPath);


                }
            }
    });
        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        ContractGrid.addListeners("rowclick", function (id, record,selBool) {
            ContractItemGrid.getJqGrid().setCaption("合同砼强度明细：" + record.CustName + "&nbsp;<font color='#2E6E9E'><strong>-></strong></font>&nbsp;" + record.ContractName);
            ContractItemGrid.refreshGrid("ContractID='"+ id +"'");
            ProjectGrid.getJqGrid().setCaption("合同工程明细："+record.CustName+"&nbsp;<font color='#2E6E9E'><strong>-></strong></font>&nbsp;"+record.ContractName);
            ProjectGrid.refreshGrid("ContractID='" + id + "'");
            ContractItemGrid.getJqGrid().jqGrid("setGridHeight", ContractGrid.getJqGrid().jqGrid("getGridParam", "height") * 0.5);
            ProjectGrid.getJqGrid().jqGrid("setGridHeight", ContractGrid.getJqGrid().jqGrid("getGridParam", "height") * 0.3);
        });

        var contractLimitType = $("select[name='Contract.ContractLimitType']");
        contractLimitType.unbind("change");
        contractLimitType.bind("change", function () {
            switch ($(this).val()) {
                case limitObj.limit0:
                    //resetClass(["Contract.ContractST", "Contract.ContractET", "Contract.MatCube", "Contract.PrepayCube"]);
                    resetClass({
                        "Contract.ContractST": "removeClass",
                        "Contract.ContractET": "removeClass",
                        "Contract.MatCube": "removeClass",
                        "Contract.PrepayCube": "removeClass"
                    });
                    break;
                case limitObj.limit1:
                    resetClass({
                        "Contract.ContractST": "addClass",
                        "Contract.ContractET": "addClass",
                        "Contract.MatCube": "removeClass",
                        "Contract.PrepayCube": "removeClass"
                    });
                    break;
                case limitObj.limit2:
                    resetClass({
                        "Contract.ContractST": "removeClass",
                        "Contract.ContractET": "removeClass",
                        "Contract.MatCube": "addClass",
                        "Contract.PrepayCube": "removeClass"
                    });
                    break;
                case limitObj.limit3:
                    resetClass({
                        "Contract.ContractST": "removeClass",
                        "Contract.ContractET": "removeClass",
                        "Contract.MatCube": "removeClass",
                        "Contract.PrepayCube": "addClass"
                    });
                    break;
                default:
                    break;

            }
        });

        ContractGrid.getJqGrid().jqGrid("setGridWidth",$("#Contractid").width()-6);

        //合同置为完工状态操作
        function setcmp(b, grid, isCompleted) {
            if (!grid.isSelectedOnlyOne()) {
                showMessage("提示", "请选择一条记录进行操作！");
                return;
            }
            var selectedRecord = grid.getSelectedRecord();
            ajaxRequest(
                b.data.Url,
                {
                    contractID: selectedRecord.ID
                },
                true,
                function () {
                    grid.refreshGrid();
                },
                null,
                b
            );
        }
        //合同置为未完工状态操作
        function setuncmp(b, grid, isCompleted) {
            if (!grid.isSelectedOnlyOne()) {
                showMessage("提示", "请选择一条记录进行操作！");
                return;
            }
            var selectedRecord = grid.getSelectedRecord();
            ajaxRequest(
                b.data.Url,
                {
                    contractID: selectedRecord.ID
                },
                true,
                function () {
                    grid.refreshGrid();
                },
                null,
                b
            );
        }

        var ContractItemGrid = new MyGrid({
            renderTo: "ContractItemGrid"
            , title: "合同砼强度明细"
            , autoWidth: true
            , buttons: buttons1
            , height: 300
            , storeURL: options.itemStoreUrl
            , sortByField: "ConStrength"
            , sortOrder:"ASC"
            , primaryKey: "ID"
            , setGridPageSize: 50
            , showPageBar: true
            , autoLoad: false
            , dialogWidth: 600
            , dialogHeight: 400
            , singleSelect: true
            , editSaveUrl: options.itemUpdateUrl
            , initArray: [
                { label: "合同明细编号", name: "ID", index: "ID", hidden:true}
                , { label: "合同编号", name: "ContractID", index: "ContractID", hidden: true }
                , { label: "合同名称", name: "ContractName", index: "ContractName", hidden: true }
                , { label: "砼强度", name: "ConStrength", index: "ConStrength", width:50, align:"center" }
                , { label: "非泵价格", name: "UnPumpPrice", index: "UnPumpPrice", width: 50, align: "right", formatter: "number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","},hidden:true }
                , { label: "泵送费", name: "PumpCost", index: "PumpCost", width: 50, align: "right", formatter: "number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: "<font color=red>泵送价格</font>", name: "PumpPrice", width: 50, align: "right", formatter: "number", index: "PumpPrice", formatoptions: { thousandsSeparator: "," }, sortable: false, hidden: true }
                , { label: "<font color=green>砂浆价格</font>", name: "SlurryPrice", width: 50, index: "SlurryPrice", formatter: "number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, hidden: true }
                , { label: "操作", name: "act", index: "act", width: 90, sortable: false, align: "center", hidden: true }
            ]
            , functions: {
                handleReload: function (btn) {
                    ContractItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractItemGrid.refreshGrid();
                }

            }
        });

        ContractItemGrid.addListeners("gridComplete", function () {
            var ids = ContractItemGrid.getJqGrid().jqGrid("getDataIDs");
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                be = "<input class='identityButton'  type='button' value='特性设定' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleIdentitySet(" + ids[i] + ");\"  />";
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteContractItem(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                ContractItemGrid.getJqGrid().jqGrid("setRowData", ids[i], { act: ce + be });

            }

        });

        ContractItemGrid.addListeners("afterSubmitCell",function(){
            ContractItemGrid.reloadGrid();
           
        });

        window.handleIdentitySet = function (id) {

            ContractItemGrid.showWindow({
                title: "特性设定",
                width: 600,
                height: 450,
                loadFrom: "IdentitySetForm"
                        , afterLoaded: function () {
                            $("input[name='IdentitySetting.ContractItemsID']").val(id);
                            $("#DisplayContractItemsID").html("<font color=red>" + id + "</font>");

                            identitySettingGrid.getJqGrid().jqGrid("setGridParam", { editurl: options.identityRowDeleteUrl });

                            identitySettingGrid.getJqGrid().jqGrid("setGridWidth", 450);
                            identitySettingGrid.refreshGrid("ContractItemsID='" + id + "'");

                            //下拉列表级联绑定
                            var identityTypelist = $("select[name='IdentitySetting.IdentityType']");
                            identityTypelist.unbind("change");
                            identityTypelist.bind("change", function () {
                                bindSelectData(
                                    $("select[name='IdentitySetting.IdentityName']"),
                                    options.listDataUrl,
                                    { textField: "IdentityName", valueField: "IdentityName", condition: "IdentityType='" + $(this).val() + "'" }
                                );
                            });


                            var identityNamelist = $("select[name='IdentitySetting.IdentityName']");
                            identityNamelist.unbind("change");
                            identityNamelist.bind("change", function () {
                                var requestURL = options.getIdentityPriceUrl;
                                var postParams = { identityName: $("select[name='IdentitySetting.IdentityName']").val(),
                                    identityType: $("select[name='IdentitySetting.IdentityType']").val()
                                };
                                ajaxRequest(requestURL, postParams, false, function (response) {
                                    $("input[name='IdentitySetting.IdentityPrice']").val(response.IdentityPrice);
                                });

                            });



                        }
                        , buttons: {
                            "关闭": function () {
                                $(this).dialog("close");
                            }
                        }
            });
        };


        
        //保存特性设定函数
        window.identitySave = function () {
            var _ContractItemsID = $("[name='IdentitySetting.ContractItemsID']").val();
            $.post(
                options.identityAddUrl,
                $("#IdentitySetForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    $("#IdentitySetForms")[0].reset();
                    identitySettingGrid.refreshGrid("ContractItemsID='" + _ContractItemsID + "'");
                }
            );
        };
        //保存合同锁定理由
        window.lockSave = function () {
            ajaxRequest(
                    options.saveLockUrl,
                    {
                        contractID: $("#ContractIDlock").val(),
                        remark: $("#lockReason").val()
                    },
                    true,
                    function (data) {
                        $("#lockReason").val("");
                        $("#lockReasonForm").dialog("close");
                        ContractGrid.refreshGrid();
                    }
            );
        };

        window.handleDeleteContractItem = function (id) {
            showConfirm("确认信息", "您确定删除此项合同明细？", function () {
                $.post(
                    options.itemDeleteUrl
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        ContractItemGrid.reloadGrid();
                    }

                );
                $(this).dialog("close");
            });
        };

        ContractItemGrid.getJqGrid().jqGrid("setGridWidth",$("#ContractItemGrid").width()-6);

        //特性设定
        var identitySettingGrid = new MyGrid({
            renderTo: "identitySettingGrid"
            , autoWidth: true
            , buttons: buttons2
            , storeURL: options.identityStoreUrl
            , sortByField: "ID"
            , primaryKey: "ID"
            , dialogWidth:500
            , dialogHeight:300
            , setGridPageSize: 30
            , showPageBar: true
            , altclass: "identityButton"
            , altRows: true
            , autoLoad: false
            , editSaveUrl: options.identityUpdateUrl
            , initArray: [
                {label:"特性设定编号", name: "IdentitySettingID", index:"IdentitySettingID",hidden:true}
                ,{label:"合同明细编号", name: "ContractItemsID", index:"ContractItemsID",hidden:true }
                ,{ label: "特性类型", name: "IdentityType", index: "IdentityType", width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "IdenType"} }
                ,{label:"详细特性", name: "IdentityName", index:"IdentityName", width:100 }
                ,{label:"特性价格", name: "IdentityPrice", index:"IdentityPrice", editable:true, width:100 }
                ,{label:"操作", name: "myac", width:80, fixed:true, sortable:false, resize:false, formatter:"actions",
            formatoptions:{keys:true,editbutton:false}}
            ]
            , functions: {
                handleReload: function (btn) {
                    identitySettingGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    identitySettingGrid.refreshGrid();
                }
            }
        });

        //其他加价设定
        var contractOtherpriceGrid = new MyGrid({
            renderTo: "contractOtherpriceGrid"
            //, autoWidth: true
            , width: 610
            //, buttons: buttons4
            , height: 150
            , storeURL: options.contractOtherPriceStoreUrl
            , sortByField: "ID"
            , primaryKey: "ID"
            , setGridPageSize: 30
            , showPageBar: true
            , editSaveUrl: options.otherPriceUpdateUrl
            , initArray: [
                { label: "编号", name: "ID", index: "ID", width: 47 }
                , { label: "合同编号", name: "ContractID", index: "ContractID", width: 88, align: "center" }
                , { label: "加价项目", name: "PriceType", index: "PriceType", width: 110 }
                , { label: "计算方式", name: "CalcType", index: "CalcType", width: 70, editable: true, edittype: "select", editoptions: {value:"百分比:百分比;公斤:公斤;固定:固定"} }
                , { label: "单价", name: "UnitPrice", index: "UnitPrice", width: 50, formatter:"number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: "数量/百分比", name: "Amount", index: "Amount", width: 80, formatter:"number", editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
                , { label: "全加", name: "IsAll", index: "IsAll", width: 50, sortable: false, formatter: "checkbox", align: "center", editable: true, edittype: "checkbox", editoptions: { value: "true:false"} }
                , { label: "操作", name: "myac", width: 50, fixed: true, sortable: false, resize: false, formatter: "actions",
                    formatoptions: { keys: true, editbutton: false}
                }
            ]
            , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    contractOtherpriceGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    contractOtherpriceGrid.refreshGrid("1=1");
                },
                handleAdd: function (btn) {
                    contractOtherpriceGrid.handleAdd({
                        loadFrom: "MyFormDiv",
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    contractOtherpriceGrid.handleEdit({
                        loadFrom: "MyFormDiv",
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    contractOtherpriceGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        //保存其他加价设定函数
        window.otherPriceSave = function () {
            var _ContractID = $("[name='ContractOtherPrice.ContractID']").val();
            $.post(
                options.otherPriceAddUrl,
                $("#otherPriceForms").serialize(),
                function (data) {
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    $("#otherPriceForms")[0].reset();
                    contractOtherpriceGrid.refreshGrid("ContractID='" + _ContractID + "'");
                }
            );
        };

        $("a[data-id]").live("click", function (e) {
                if (e.preventDefault)
                    e.preventDefault();
                else
                    e.returnValue = false;

                var data = ContractGrid.getRecordByKeyValue($(this).attr("data-id"));

                $("a[att-id]").hide();
            });
            $("a[att-id]").live("click", function (e) {
                if (e.preventDefault)
                    e.preventDefault();
                else
                    e.returnValue = false;
                var caller = $(this);


                ajaxRequest(options.deleteAttachmentUrl,
             { id: caller.attr("att-id") },
             false,
             function (response) {
                 if (response.Result) {
                     caller.parent("li").remove();
                 }
             });
            });
         //上传附件
         function attachmentUpload(objectId) {
                var fileElement = $("input[type=file]");
                if (fileElement.val() == "") return;

                $.ajaxFileUpload
                ({
                    url: options.uploadUrl + "?objectType=Contract&objectId=" + objectId,
                    secureuri: false,
                    fileElementId: "uploadFile",
                    dataType: "json",
                    beforeSend: function () {
                        $("#loading").show();
                    },
                    complete: function () {
                        $("#loading").hide();
                    },
                    success: function (data, status) {
                        if (data.Result) {
                            showMessage("附件上传成功");
                            ContractGrid.reloadGrid();
                        }
                        else {
                            showError("附件上传失败", data.Message);
                        }
                    },
                    error: function (data, status, e) {
                        showError(e);
                    }
                }
        );
        return false;

       }

     var ProjectGrid = new MyGrid({
        renderTo: "ProjectGrid"
            , title: "合同工程明细"
            , autoWidth: true
            , height: 150
            , dialogWidth: 920
            , dialogHeight: 510
            , storeURL: options.ProjectStoreUrl
            , sortByField: "ID"
            , primaryKey: "ID"
            , setGridPageSize: 100
            , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , editSaveUrl: '/Project.mvc/Update'
            , initArray: [
                { label: "工程编号", name: "ID", index: "ID", width: 80 }
                , { label: "工程名称", name: "ProjectName", index: "ProjectName",  editrules: { required: true } }
                , { label: "项目地址", name: "ProjectAddr", index: "ProjectAddr", editable: true }
                , { label: "工程运距", name: "Distance", index: "Distance", editable: true, width: 80 }
                , { label: "建设单位", name: "BuildUnit", index: "BuildUnit", editable: true }
                , { label: "施工单位", name: "ConstructUnit", index: "ConstructUnit", editable: true }
                , { label: "工地联系人", name: "LinkMan", index: "LinkMan", editable: true, width: 80 }
                , { label: "工地电话", name: "Tel", index: "Tel", editable: true, width: 80 }
                , { label: "备注", name: "Remark", index: "Remark", editable: true }
                , { label: "合同编号", name: "ContractID", index: "ContractID", width: 80 }
                , { label: "合同名称", name: "ContractName", index: "ContractName" }
            ]
            , functions: {
                handleReload: function (btn) {
                    ProjectGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ProjectGrid.refreshGrid();
                }
            }
        });
        ProjectGrid.addListeners("gridComplete", function () {
            var ids = ProjectGrid.getJqGrid().jqGrid("getDataIDs");
            if (ids.length == 1) { //只有一条时就自动选择
                ProjectGrid.getJqGrid().jqGrid("setSelection", ids[0]);
            }

        });
        function bindIdentitySettings(container, contractItemId, taskId) {
            var container = $(container);
            container.empty();
            if (isEmpty(contractItemId)) return;
            ajaxRequest(options.findIdentitySettingsUrl,
            { contractItemId: contractItemId, taskId: taskId },
            false,
            function (response) {
                for (var i = 0; i < response.length; i++) {
                    var item = response[i];
                    var identDiv = $("<div>").appendTo(container)
                                        .attr("id", "Ident_" + i)
                                        .addClass("identbox ui-corner-all")
                                        .append("<div class='titlebar ui-widget-header ui-corner-all ui-helper-clearfix'>" + item.IdentType.DicName + "</div>");
                    identDiv.checkboxlist({ data: item.IdentItems });
                }
            });
        }

    }
    function AddUnit() {
        $.ajax({
            url: "/Dic.mvc/AddUnit",
            dataType: "json",
            type: "POST",
            data: {
                text: $("#AddUnit").val()
            },
            success: function (data) {
                if (data.Result) {
                    showMessage("增加成功");
                    $("#ProduceTask_SupplyUnit").append("<option value='" + data.Data + "'>" + data.Data + "</option>");
                }
                else {
                    showError("增加失败", data.Message);
                }
            },
            error: function (data, e) {
                showError(e);
            }
        });
    }