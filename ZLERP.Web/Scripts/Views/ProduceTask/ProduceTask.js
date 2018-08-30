//生产任务
function produceTaskIndexInit(opts) {
    function bindIdentitySettings(container, contractItemId, taskId) {
        var container = $(container);
        container.empty();
        if (isEmpty(contractItemId)) return;
        ajaxRequest(opts.findIdentitySettingsUrl,
        { contractItemId: contractItemId, taskId: taskId },
        false,
        function (response) {
            for (var i = 0; i < response.length; i++) {
                var item = response[i];
                var identDiv = $("<div>").appendTo(container)
                                    .attr("id", "Ident_" + i)
                                    .addClass('identbox ui-corner-all')
                                    .append('<div class="titlebar ui-widget-header ui-corner-all ui-helper-clearfix">' + item.IdentType.DicName + '</div>');
                identDiv.checkboxlist({ data: item.IdentItems });
            }
        });
    }

    function taskIdFormatter(cellvalue, options, rowObject) {
        return "<a href='#' data-task-id='" + cellvalue + "'>" + cellvalue + "</a>";
    };
    function taskIdUnFormatter(cellvalue, options, cell) {
        return $('a', cell).attr('data-task-id');
    }
    //禁用技术参数编辑
    function disableTechField(disable) {
        mygrid.setFormFieldDisabled('ConsPosType', disable);
        mygrid.setFormFieldDisabled('ConsPos', disable);
        mygrid.setFormFieldDisabled('CastMode', disable);
        mygrid.setFormFieldDisabled('Slump', disable);
        mygrid.setFormFieldDisabled('PumpType', disable);
        //合同名称
        mygrid.setFormFieldReadOnly('ProjectName', true);
        $("input[name='Slump']").next('button').attr('disabled', disable);
        $("input[name='CastMode']").next('button').attr('disabled', disable);
    }
    var mygrid = new MyGrid({
        renderTo: 'first-grid'
        //  , title: '生产任务列表'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , storeURL: opts.storeUrl
            , storeCondition: "IsCompleted=0"
            , sortByField: 'ID'
            , primaryKey: 'ID'
            //, groupField: 'TaskType'
            , groupingView: { groupDataSorted: true }
            , setGridPageSize: 30
            , showPageBar: true
            , dialogWidth: 750
            , dialogHeight: 510
            , rowNumbers: true
            , altRows:true
            , altclass:'ui-priority-secondary'
            , initArray: [
        { label: '任务单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, formatter: taskIdFormatter, unformat: taskIdUnFormatter }
        , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', align: 'center', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AuditStatus')} }
        , { label: '是否完成', name: 'IsCompleted', index: 'IsCompleted', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
        , { label: '配比下发', name: 'IsFormulaSend', index: 'IsFormulaSend', search: false, sortable: false, align: 'center', width: 50, formatter: booleanFmt, unformat: booleanUnFmt }
        , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', searchoptions: { sopt: ['cn']} }
        , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
        , { label: '客户编码', name: 'CustomerID', jsonmap: 'Contract.Customer.ID', index: 'Contract.Customer.ID', type: 'hidden', hidden: true }
        , { label: '客户名称', name: 'CustName', jsonmap: 'Contract.Customer.CustName', index: 'Contract.Customer.CustName', searchoptions: { sopt: ['cn']} }
        , { label: '合同编码', name: 'ContractID', index: 'Contract.ID', type: 'hidden', hidden: true }
        , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 80, searchoptions: { sopt: ['cn']} }
        , { label: '坍落度', name: 'Slump', index: 'Slump', align: 'center', width: 50 }
        , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
        , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', align: 'center', width: 50, stype: 'select', searchoptions: { dataUrl: opts.conStrengthSelectUrl} }
        , { label: '计划时间', name: 'NeedDate', index: 'NeedDate', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', align: 'right', width: 50 }
        , { label: '计划车数', name: 'NeedTimes', index: 'NeedTimes', align: 'right', width: 50 }
        , { label: '已供车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', sortable: false }
        , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 60, align: 'right', sortable: false }
        , { label: '泵车类型', name: 'PumpType', index: 'PumpType', width: 50, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PumpType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('PumpType')} }
        , { label: '销售员', name: 'Seller', index: 'Seller', width: 50 }
        , { label: '含砂浆', name: 'IsSlurry', index: 'IsSlurry', align: 'center', width: 50, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
        , { label: '开盘时间', name: 'OpenTime', index: 'OpenTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
        , { label: '合同明细编号', name: 'ContractItemsID', index: 'ContractItemsID', hidden: true }
        , { label: '特性参数', name: 'IdentityValue', index: 'IdentityValue', search: false, sortable: false, width: 60 }
        , { label: '施工部位类型', name: 'ConsPosType', index: 'ConsPosType', width: 70, stype: 'select', searchoptions: { value: dicToolbarSearchValues('ConsPos')} }
        , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit' }
         , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit', type: 'hidden', hidden: true }
        , { label: '供应单位电话', name: 'SupplyUnitTel', index: 'SupplyUnitTel' }
        , { label: '代外生产', name: 'IsCommission', index: 'IsCommission', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
        , { label: '公司负责人', name: 'CompPrincipal', index: 'CompPrincipal' }
        , { label: '前场工长', name: 'LinkMan', index: 'LinkMan', align: 'center', width: 50 }
        , { label: '工地电话', name: 'Tel', index: 'Tel' }
        , { label: '出资料否', name: 'IsDatum', index: 'IsDatum', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
        , { label: '级配', name: 'CarpGrade', index: 'CarpGrade', edittype: 'select', width: 50, editoptions: { value: "I级配:I级配;II级配:II级配"} }
        , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii' }
        , { label: '砼标记', name: 'BetonTag', index: 'BetonTag' }
        , { label: '特殊要求', name: 'SpecialDemand', index: 'SpecialDemand', sortable: false }
        , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed' }
        , { label: '其它要求', name: 'OtherDemand', index: 'OtherDemand' }
        , { label: '审核人', name: 'Auditor', index: 'Auditor', align: 'center', width: 50 }
        , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        , { label: '审核意见', name: 'AuditInfo', index: 'AuditInfo', width: 150 }
        , { label: '完成日期', name: 'CompleteDate', index: 'CompleteDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
        , { label: '备注', name: 'Remark', index: 'Remark' }
        , { label: '工程运距', name: 'Distance', index: 'Distance' }
        , { label: '区间', name: 'Region.RegionName', index: 'Region.RegionName', width: 50 }
        , { label: '区间', name: 'RegionID', index: 'RegionID', hidden: true }
        , { label: '砂浆配比', name: 'SlurryFormulaName', index: 'SlurryFormulaInfo.FormulaName' }
        , { label: '砂浆配比ID', name: 'SlurryFormula', index: 'SlurryFormula', hidden: true }
        , { label: '任务单类型', name: 'TaskType', sortable: false, index: 'TaskType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('TType')} }
        , { label: '混凝土类别', name: 'CementType', index: 'CementType' }
        , { label: ' 混凝土配比', name: 'BetonFormulaName', index: 'BetonFormulaInfo.FormulaName', width: 80 }
        , { label: ' 混凝土配比ID', name: 'BetonFormula', index: 'BetonFormula', width: 80, hidden: true }
        , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID' }
        , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', width: 60 }
        , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', width: 60 }
        , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', width: 60 }
        , { label: '计划班组', name: 'PlanClass', index: 'PlanClass' }
 		, { label: '生产负责人', name: 'ProductPrincipal', index: 'ProductPrincipal' }
        , { label: '上料员', name: 'ForkLift', index: 'ForkLift' }
        , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80 }
        , { label: '创建人', name: 'Builder', index: 'Builder' }
        , { label: '修改人', name: 'Modifier', index: 'Modifier' }
        , { label: '异常信息', name: 'Exception', index: 'Exception', width: 400 }
        , { label: '责任部门', name: 'ResponsibleParty', index: 'ResponsibleParty' }

        , { label: '要求塌落度', name: 'DemandSlump', index: 'DemandSlump', width: 80 }
        , { label: '要求检测人', name: 'DemandChecker', index: 'DemandChecker', width: 80 }
        , { label: '要求检测时间', name: 'DemandCheckTime', index: 'DemandCheckTime', formatter: 'datetime', width: 120 }
        , { label: '实测塌落度', name: 'RealSlump', index: 'RealSlump', width: 80 }
        , { label: '实测检测人', name: 'RealChecker', index: 'RealChecker', width: 80 }
        , { label: '实测检测时间', name: 'RealCheckTime', index: 'RealCheckTime', formatter: 'datetime', width: 120 }
   	]
            , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    mygrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    mygrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var records = mygrid.getSelectedRecords();
                    if (records.length == 1) {
                        mygrid.handleEdit({
                            btn: btn,
                            loadFrom: 'MyFormDiv',
                            afterFormLoaded: function () {
                                //                                alert(2);//这个handleAdd根本不执行
                                var record = mygrid.getSelectedRecord();
                                mygrid.getFormField("ID").val("");
                                var customerId = record.CustomerID; 
                                var contractIdField = mygrid.getFormField("ContractID"); //存合同id的
                                var customerIdField = mygrid.getFormField("CustomerID"); //存客户id的


                                bindSelectData(contractIdField,
                                        opts.listContractUrl,
                                        { textField: 'ContractName',
                                            valueField: 'ID',
                                            condition: "CustomerID='" + customerId + "' and ContractStatus!='3' and AuditStatus='1'"
                                        }, function () {
                                            if (!isEmpty(record.ContractID)) {
                                                var contractId = record.ContractID;
                                                contractIdField.val(contractId); //直接把主页的选中行的合同id赋给点'新增'的弹出框的合同控件
                                                mygrid.setFormFieldDisabled("CustName", true);
                                                mygrid.getFormField("CustName").next('button').attr('disabled', true);
                                                bindSelectData(mygrid.getFormField("ContractItemsID"),
                                                    opts.listContractItemUrl,
                                                    { textField: 'ConStrength',
                                                        valueField: 'ID',
                                                        orderBy: 'ConStrength',
                                                        ascending: true,
                                                        condition: "ContractID='" + contractId + "'"
                                                    }, function () {
                                                        if (!isEmpty(record.ContractItemsID)) {
                                                            mygrid.getFormField("ContractItemsID").val(record.ContractItemsID);

                                                        }
                                                    }
                                                );

                                                bindSelectData(mygrid.getFormField("ProjectID"),
                                                    opts.listProjectUrl,
                                                    { textField: 'ProjectName',
                                                        valueField: 'ID',
                                                        condition: "ContractID='" + contractId + "'"
                                                    }
                                                );
                                            }
                                        }
                                    );
                                //bindSelectData(contractIdField,
                                contractChange();
                                consPosTypeChange();
                                linkManChange();
                                mygrid.getFormField("PlanCube").val(""); //计划方量和计划时间都设空
                                mygrid.getFormField("NeedDate").val("");
                                //启用工程名称编辑
                                mygrid.setFormFieldReadOnly('ProjectName', false);
                            }
                            //afterFormLoaded
                        });
                        // mygrid.handleEdit

                        return;
                    }
                    mygrid.handleAdd({
                        btn: btn,
                        loadFrom: 'MyFormDiv',
                        afterFormLoaded: function () {
                            //                            alert(1);
                            disableTechField(false);
                            var contractIdField = mygrid.getFormField("ContractID");
                            var customerIdField = mygrid.getFormField("CustomerID");
                            mygrid.setFormFieldDisabled("CustName", false);
                            mygrid.getFormField("CustName").next('button').attr('disabled', false);
                            customerChange();
                            contractChange();
                            consPosTypeChange();
                            projectChange();
                            contractItemsChange();
                            linkManChange();
                            //启用工程名称编辑
                            mygrid.setFormFieldReadOnly('ProjectName', false);
                        }
                        , closeDialog: false
                        , postCallBack: function (response) {
                            //保存特性
                            if (response.Result) {
                                var uid = response.Data;
                                var postUrl = opts.saveTaskIdentitiesUrl;

                                var identities = [];
                                $('#rightdiv input:checked').each(function () {
                                    identities.push($(this).val()); //选择的都压人数组.ar identities = []和int[] identities一样.
                                });

                                ajaxRequest(postUrl, { taskId: uid, identities: identities }, false, function (response) {
                                    $("#MyFormDiv").dialog('close');
                                });

                            }
                        }
                    });
                },
                handleCrash: function (btn) {
                    mygrid.handleAdd({
                        btn: btn,
                        loadFrom: 'CrashForm',
                        afterFormLoaded: function () {
                            disableTechField(false);
                            //加载砼强度的列表
                            bindSelectData($('#ConStrength', '#CrashForm'),
                                opts.listAllConStrength,
                                { textField: 'ConStrengthCode',
                                    valueField: 'ConStrengthCode',
                                    orderBy: 'ConStrengthCode',
                                    ascending: true,
                                    condition: ""
                                }
                            );

                            var customerIdField = mygrid.getFormField("CustomerID");
                            customerIdField.unbind('change');
                            //更改了客户重新加载客户名
                            customerIdField.bind('change', function () {
                                $('#ContractName', '#CrashForm').empty();
                                mygrid.getFormField("ContractName").val('');
                                var customerId = $(this).val();
                                if (customerId) {
                                    bindSelectData($('#ContractName', '#CrashForm'),
                                    opts.listContractUrl,
                                    { textField: 'ContractName',
                                        valueField: 'ID',
                                        condition: "CustomerID='" + customerId + "' and ContractStatus!='3' and AuditStatus='1'"
                                    },
                                    function (data) {
                                        var count = data.length;
                                        if (count > 0) {
                                            $('#ContractName', '#CrashForm').children().first().remove(); //去掉了空白选项
                                        }
                                    }
                                );
                                }
                                mygrid.setFormFieldValue('ConstructUnit', mygrid.getFormFieldValue('CustName'));
                            });

                            var contractNameField = mygrid.getFormField("ContractName");
                            contractNameField.unbind('change');
                            //合同下拉更换
                            contractNameField.bind('change', function (event) {
                                mygrid.getFormField("ProjectID").empty(); //工程明细下拉empty调
                                var contractName = $(this).val(); //得到合同名称
                                var selectedContractName = $('#ContractName', '#CrashForm').children(':selected').text(); //得到合同名称
                                var contractId = $('#ContractName', '#CrashForm').val(); //得到合同id
                                if (contractId && contractName == selectedContractName) {
                                    //加载工程明细
                                    bindSelectData(mygrid.getFormField("ProjectID"),
                                        opts.listProjectUrl,
                                        { textField: 'ProjectName',
                                            valueField: 'ID',
                                            condition: "ContractID='" + contractId + "'"
                                        }
                                    );
                                }
                                mygrid.setFormFieldValue('ProjectName', contractName);
                            });

                            projectChange();
                            contractItemsChange();
                            consPosTypeChange();
                            linkManChange();
                            //启用工程名称编辑
                            mygrid.setFormFieldReadOnly('ProjectName', false);
                        }
                        , closeDialog: false
                        , postCallBack: function (response) {
                            //保存特性
                            if (response.Result) {
                                var uid = response.Data;
                                var postUrl = opts.saveTaskIdentitiesUrl;

                                var identities = [];
                                $('#rightdiv input:checked').each(function () {
                                    identities.push($(this).val());
                                });

                                ajaxRequest(postUrl, { taskId: uid, identities: identities }, false, function (response) {
                                    $("#CrashForm").dialog('close');
                                });

                            }
                        }
                        , beforeSubmit: beforeFormSubmit
                    });
                },
                handleEdit: function (btn) {
                    mygrid.handleEdit({
                        btn: btn,
                        loadFrom: 'MyEditForm',
                        afterFormLoaded: function () {
                            var record = mygrid.getSelectedRecord();
                            mygrid.setFormFieldValue("CustomerID", record["CustName"]);
                            var taskid = mygrid.getSelectedKey(); //任务单id
                            var cid = mygrid.getFormField("ContractItemsID").val(); //合同明细id
                            //var cid = contractItemsField.val();
                            bindIdentitySettings('#rightdiv1', cid, taskid);
                            if (record.IsFormulaSend == "true") {
                                //
                                setTimeout(function () {
                                    $('#ui-dialog-title-MyEditForm').html("修改生产任务   <font color='red'>注意：任务单配比已下发，技术参数不能修改</font>");
                                }, 500);
                                disableTechField(true);
                            }
                            else {
                                disableTechField(false);
                            }
                            consPosTypeChange();
                            linkManChange();
                        },
                        postCallBack: function (response) {
                            //保存特性
                            if (response.Result) {

                                var tid = mygrid.getFormFieldValue('ID');
                                var postUrl = opts.saveTaskIdentitiesUrl;
                                var identities = [];
                                $('#rightdiv1 input:checked').each(function () {
                                    identities.push($(this).val());
                                });
                                var postParam = { taskId: tid };
                                if (identities.length > 0) {
                                    postParam.identities = identities;
                                }
                                ajaxRequest(postUrl, postParam, false, function (response) {
                                    $("#MyFormDiv").dialog('close');
                                });

                            }
                        }
                    });
                }
                , handleDelete: function (btn) {
                    mygrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , reloadGrid: true
                    });
                }
                , handleSaveException: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    //var data = shippDocGrid.getSelectedRecord();

                    mygrid.handleEdit({
                        title: "生产异常信息",
                        btn: btn,
                        width: 400,
                        height: 300,
                        loadFrom: 'ExceptionForm',
                        afterFormLoaded: function () {
                            //mygrid.setFormFieldValue('NeedDate', new Date().format('Y-m-d H:i:s'));
                        }
                    });
                }
                , handleEditTime: function (btn) {
                    //可选择多行调另一个方法
                    var keys = mygrid.getSelectedKeys();
                    //alert(keys.length);
                    if (keys.length > 1) {
                        var requestURL = opts.HandleTodayPlanUrl;
                        var postParams = { ids: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            mygrid.reloadGrid();
                        });
                    }
                    else {
                        mygrid.handleEdit({
                            btn: btn,
                            width: 350,
                            height: 250,
                            loadFrom: 'TaskPlanFormDiv',
                            afterFormLoaded: function () {
                                mygrid.setFormFieldValue('NeedDate', new Date().format('Y-m-d H:i:s'));
                            }
                        });
                    }
                }
                , handleAuditing: function (btn) {
                    var records = mygrid.getSelectedRecords();
                    for (var i = 0; i < records.length; i++) {
                        var auditValue = records[i].AuditStatus;
                        if (auditValue == 1) {
                            showMessage('提示', '请选择未审核的任务单！');
                            return;
                        }
                    }

                    var keys = mygrid.getSelectedKeys();
                    if (keys.length > 1) {
                        var requestURL = opts.handleBatchAuditUrl;
                        var postParams = { id: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            mygrid.reloadGrid();
                        });
                    }
                    else {
                        mygrid.handleEdit({
                            loadFrom: 'AuditingFormDiv',
                            btn: btn,
                            width: 480,
                            height: 250
                        });
                    }
                },
                handleUnAuditTask: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = mygrid.getSelectedRecord();
                    var auditValue = selectedRecord.AuditStatus;
                    if (auditValue == 0) {
                        showMessage('提示', '该任务单正处于未审核状态！');
                        return;
                    } else {
                        //确认操作
                        showConfirm("确认信息", "您确定要<font color=green><b>取消审核</b></font>吗？", function () {
                            ajaxRequest(
                                btn.data.Url,
                                {
                                    taskID: selectedRecord.ID,
                                    auditStatus: 0
                                },
                                true,
                                function () {
                                    mygrid.refreshGrid();
                                }
                            );
                            $(this).dialog("close");
                        });
                    }
                },
                //已完工任务单 add by xyl on 2012-3-26
                handleCompleted: function () {
                    mygrid.refreshGrid("IsCompleted = 1");
                },
                //未完工任务单 add by xyl on 2012-3-26
                handleNotCompleted: function () {
                    mygrid.refreshGrid("IsCompleted = 0");
                },
                //将任务单置为完工 add by xyl on 2012-3-26
                handleSetComplete: function (btn) {
                    setcmp(btn, mygrid, true);
                },
                //将任务单置为未完工 add by xyl on 2012-3-26
                handleSetNotComplete: function (btn) {
                    setcmp(btn, mygrid, false);
                },
                //查看历史记录 add by xyl on 2012-3-27
                handleViewHis: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = mygrid.getSelectedRecord();
                    showHis(btn, selectedRecord.ID, "M");
                },
                //查看已删除的任务单 add by xyl on 2012-3-27
                handleViewDeleted: function (btn) {
                    showHis(btn, null, "D");
                },
                //查看近三天
                handleThree: function (btn) {
                    var d = new Date();
                    var today = d.format("Y-m-d");
                    var uom = GetDateBySpan(2);
                    condition = "NeedDate between '" + uom + " 00:00:00' and '" + today + " 23:59:59'";
                    mygrid.refreshGrid(condition);
                },
                //查看当天
                handleToday: function (btn) {
                    var d = new Date();
                    var today = d.format("Y-m-d");
                    var uom = GetDateBySpan(-1);
                    //从0点算
                    condition = "NeedDate between '" + today + " 00:00:00' and '" + today + " 23:59:59'";
                    //从交班时间开始算                    
                    //condition = "NeedDate >= '" + today + " " + gSysConfig.ChangeTime + ":00' and NeedDate < '" + uom + " " + gSysConfig.ChangeTime + ":00'";
                    mygrid.refreshGrid(condition);
                },
                FindCustRecord: function (btn) {
                    mygrid.showWindow({
                        title: '选择时间范围',
                        width: 280,
                        height: 180,
                        resizable: false,
                        loadFrom: 'SelectTimeForm',
                        afterLoaded: function () {

                        },
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var BeginTime = $("#pBeginTime").val();
                                var EndTime = $("#pEndTime").val();
                                condition = "NeedDate between '" + BeginTime + "' and '" + EndTime + "'";
                                mygrid.refreshGrid(condition);
                                var checked = $("#pIsAutoClose")[0].checked;
                                if (checked) {
                                    $(this).dialog('close');
                                }
                            }
                        }
                    });
                },
                handleOtherPrice: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }


//                    alert(btn.data.Url);
                    mygrid.handleAdd({
                        width: 550,
                        height: 400,
                        title: '其他加价',
                        //                        btn: btn,
                        
                        loadFrom: 'otherPriceWindow',
                        afterFormLoaded: function () {
                            var record = mygrid.getSelectedRecord();
                            var TaskID = record["ID"];
                            var ContractID = record["ContractID"];
                            OtherPriceGrid.refreshGrid("ProduceTaskID='" + TaskID + "'");
                            bindSelectData($("#OtherPriceID"),
                                    opts.ContractOtherPrice,
                                    {
                                        ContractID: ContractID
                                    }, null
                                );

                        }

                    });
                },
                handlePrintRpt: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        OpenReportByPara("/Reports/Produce/R410710.aspx", mygrid.getSelectedKeys().join(','));
                    }
                    else {
                        var selectedRecord = mygrid.getSelectedRecord();
                        OpenReportByPara("/Reports/Produce/R410706.aspx", selectedRecord.ID);
                    }
                },
                handleEditTodayPlanAmount: function (btn) {
                    //todayPlanSettingForm
                    mygrid.handleAdd({
                        loadFrom: 'todayPlanSettingForm',
                        btn: btn,
                        width: 240,
                        height: 150,
                        afterFormLoaded: function () {
                            setFormValues('todayPlanSettingForm', opts.getSysConfigUrl, { configName: 'TodayPlanAmount' }, null);
                        }
                    });
                },


                /*start zjy*/
                handleSaveDemandSlump: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作！");
                        return false;
                    }

                    mygrid.handleEdit({
                        title: "要求出机塌落度",
                        btn: btn,
                        width: 300,
                        height: 200,
                        loadFrom: 'DemandSlumpForm',
                        afterFormLoaded: function () {
                        }
                    });
                },
                handleSaveRealSlump: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作！");
                        return false;
                    }

                    mygrid.handleEdit({
                        title: "实测出机塌落度",
                        btn: btn,
                        width: 300,
                        height: 200,
                        loadFrom: 'RealSlumpForm',
                        afterFormLoaded: function () {
                        }
                    });
                },
                /*end zjy*/

                handlePrintExam: function (btn) {
                    if (!mygrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var selectedRecord = mygrid.getSelectedRecord();
                    if (isEmpty(selectedRecord.BetonFormula)) {
                        showMessage("提示", "该任务单还没下发配比");
                        return false;
                    }
                    mygrid.showWindow({
                        title: '选择报表模版',
                        width: 280,
                        height: "auto",
                        resizable: false,
                        loadFrom: 'SelectTpl',
                        afterLoaded: function () {
                            //加载模版
                            $("#newTpl").empty();
                            $("#onehisTpl").empty();
                            $.post(
                                "/TplManage.mvc/Find",
                                {
                                    condition: "TplType='实验室'",
                                    page: 1,
                                    rows: 30,
                                    sidx: "ID",
                                    sord: "asc"
                                },
                                function (resp) {
                                    var data = resp.rows;
                                    $.each(data, function (i, n) {
                                        $("<div style='height:20px;line-height:20px;cursor:pointer; margin:1px 0;' tplfilename='" + n.TplFileName + "' tplurl='" + n.TplUrl + "' previewurl='" + n.PreviewUrl + "' parms='" + n.Parms + "'>" + (i + 1) + "、" + n.TplName + "</div>").appendTo($("#newTpl"));
                                    });
                                }
                            );

                        },
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var selectedElFromNewTpl = $("#SelectTpl>div div").filter(function (index) {
                                    return $(this).attr("current");
                                }).last();

                                if (selectedElFromNewTpl.length == 0) {
                                    showMessage("提示", "请选择一个模版或模版的历史数据");
                                    return false;
                                } else {
                                    //打开新模版还是历史数据？
                                    var isHis = isEmpty(selectedElFromNewTpl.attr("hisdata"));
                                    var tplPath = isHis ? selectedElFromNewTpl.attr("tplurl") : selectedElFromNewTpl.attr("previewurl");
                                    //拼凑参数?与&。json参数必须是selectedRecord里面的字段
                                    var parmsStr = selectedElFromNewTpl.attr("parms");
                                    if (parmsStr != "null") {
                                        var parmsJson = $.parseJSON(parmsStr);
                                        var count = 0;
                                        $.each(parmsJson, function (i, n) {
                                            if (count > 0) {
                                                tplPath += "&" + i + "=" + (isHis ? selectedRecord[n.toString()] : n);
                                            } else {
                                                tplPath += "?" + i + "=" + (isHis ? selectedRecord[n.toString()] : n);
                                            }
                                            count++;
                                        })
                                    }
                                    window.open(tplPath);
                                }
                            }
                        }
                    });
                }
            }
    });

    //施工部位类型Change事件
    window.consPosTypeChange = function () {
        var consPosTypeField = mygrid.getFormField("ConsPosType");
        consPosTypeField.unbind('change');
        consPosTypeField.bind('change', function (event) {
            mygrid.getFormField("ConsPos").val(consPosTypeField.val());
        });
    };

    //客户控件类型Change事件
    window.customerChange = function () {
        var customerIdField = mygrid.getFormField("CustomerID");
        customerIdField.unbind('change');
        customerIdField.bind('change', function () {
            var customerId = $(this).val();
            if (customerId) {
                bindSelectData(mygrid.getFormField("ContractID"),
                                        opts.listContractUrl,
                                        { textField: 'ContractName',
                                            valueField: 'ID',
                                            condition: "CustomerID='" + customerId + "' and ContractStatus!='3' and AuditStatus='1'"
                                        }
                                    );

                mygrid.setFormFieldValue('ConstructUnit', mygrid.getFormFieldValue('CustName'));

            }
            else {
                mygrid.getFormField("ContractID").empty();
            }
        });
    };

    //合同控件Change事件
    window.contractChange = function () {
        var contractIdField = mygrid.getFormField("ContractID");
        contractIdField.unbind('change');
        contractIdField.bind('change', function (event) {
            var contractId = $(this).val();
            if (contractId) {
                ajaxRequest(
                                        opts.checkContractAuditStatusUrl,
                                        { contractId: contractId },
                                        false,
                                        function (response) {
                                            if (!response.Result) {
                                                showError("出错啦！", response.Message + "<br>合同编号为：" + contractId);
                                                contractIdField.val("");
                                                mygrid.getFormField("ContractItemsID").empty();
                                                return false;
                                            } else {
                                                bindSelectData(mygrid.getFormField("ContractItemsID"),
                                                    opts.listContractItemUrl,
                                                    { textField: 'ConStrength',
                                                        valueField: 'ID',
                                                        orderBy: 'ConStrength',
                                                        ascending: true,
                                                        condition: "ContractID='" + contractId + "'"
                                                    }
                                                );

                                                bindSelectData(mygrid.getFormField("ProjectID"),
                                                    opts.listProjectUrl,
                                                    { textField: 'ProjectName',
                                                        valueField: 'ID',
                                                        condition: "ContractID='" + contractId + "'"
                                                    }
                                                );
                                            }
                                        }
                                    );

            }
            else {
                mygrid.getFormField("ContractItemsID").empty();
            }
        });
    };

    //合同明细Change事件
    window.contractItemsChange = function () {
        var contractItemsField = mygrid.getFormField("ContractItemsID");
        contractItemsField.unbind('change');
        contractItemsField.bind('change', function (event) {
            var cid = contractItemsField.val();
            bindIdentitySettings('#rightdiv', cid);
        });
    };

    //工程控件Change事件
    window.projectChange = function () {
        var projectIdField = mygrid.getFormField("ProjectID");
        projectIdField.unbind('change');
        projectIdField.bind('change', function (event) {
            var pid = projectIdField.val();

            //mygrid.getFormField("ProjectName").val(mygrid.getFormField("ProjectID").val());
            if (isEmpty(pid)) {
                //空项
                mygrid.setFormFieldValue('ProjectName', '');
                mygrid.setFormFieldValue('ProjectAddr', '');
                mygrid.setFormFieldValue('BuildUnit', '');
                mygrid.setFormFieldValue('ConstructUnit', '');
                //启用工程名称编辑
                mygrid.setFormFieldReadOnly('ProjectName', false);
            }
            else {

                var requestURL = opts.getProjectUrl;
                var postParams = { id: pid };
                var rData;
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        rData = response.Data;
                        mygrid.getFormField("ProjectName").val(response.Data == null ? "" : rData.ProjectName);
                        mygrid.getFormField("ProjectAddr").val(response.Data == null ? "" : rData.ProjectAddr);
                        //mygrid.getFormField("ConstructUnit").val(rData.ConstructUnit);
                        mygrid.getFormField("BuildUnit").val(response.Data == null ? "" : rData.BuildUnit);
                        var sgdw = mygrid.getFormFieldValue('ConstructUnit');
                        if (isEmpty(sgdw)) {
                            mygrid.setFormFieldValue('ConstructUnit', response.Data == null ? "" : rData.ConstructUnit);
                        }
                    }
                });
                //禁用工程名称编辑
                mygrid.setFormFieldReadOnly('ProjectName', true);
            }
        });
    };

    //任务单完成状态操作
    function setcmp(b, grid, isCompleted) {
        var keys = grid.getSelectedKeys();
        if (keys.length > 0) {
            ajaxRequest(
            b.data.Url,
            {
                ids: keys,
                isCompleted: isCompleted
            },
            true,
            function () {
                grid.reloadGrid();
            },
            null,
            b
        );
        }
    }
    //任务单历史记录及已删除表格
    var taskHisGridInitArray = [
                { label: '序号', name: 'ID', index: 'ID', width: 50, align: 'center', sortable: false }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', sortable: false }
                , { label: '施工部位类型', name: 'ConsPosType', index: 'ConsPosType', sortable: false, width: 90 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', sortable: false }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', sortable: false, width: 60 }
                , { label: '计划车数', name: 'NeedTimes', index: 'NeedTimes', sortable: false, width: 60 }
                , { label: '泵车类型', name: 'PumpType', index: 'PumpType', sortable: false }
                , { label: '用砼时间', name: 'NeedDate', index: 'NeedDate', sortable: false, formatter: 'date', width: 80, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', sortable: false, width: 50, align: 'center' }
                , { label: '坍落度', name: 'Slump', index: 'Slump', sortable: false, width: 60 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', sortable: false, width: 80 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', sortable: false, align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '区间', name: 'RegionID', index: 'RegionID', sortable: false, width: 80 }
                , { label: '操作', name: 'Act', index: 'Act', sortable: false, width: 60, align: 'center' }
                , { label: '执行人', name: 'ExecMan', index: 'ExecMan', sortable: false, width: 60, align: 'center' }
                , { label: '执行时间', name: 'ExecTime', index: 'ExecTime', sortable: false, formatter: 'datetime', width: 130, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
	];

    //subGrid子表格 subGridRowExpanded根据两个参数创建新的jqGrid对象subgrid_id div的id row_id 主要表格所需要展开子表格的行id
    var taskHisGrid = new MyGrid({
                    renderTo: 'taskHisGrid'
            , width: 735
                    //, buttons: buttons0
            , height: 235
            , storeURL: opts.TaskHisStoreUrl
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , subGrid: true
            , subGridRowExpanded: function (subgrid_id, row_id) {
                var record = taskHisGrid.getRecordByKeyValue(row_id);
                var tid = record.TaskID;
                var subgrid_table_id, pager_id;
                subgrid_table_id = subgrid_id + "_t";
                subgrid_pager_id = "p_" + subgrid_table_id;
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + subgrid_pager_id + "'></div>");
                $("#" + subgrid_table_id).jqGrid({
                    url: opts.TaskHisStoreUrl,
                    datatype: 'json',
                    mtype: 'post',
                    colModel: taskHisGridInitArray,
                    rowNum: 5,
                    height: '100%',
                    viewrecords: true,
                    sortable: true,
                    sortname: 'ID',
                    sortorder: 'desc',
                    pager: subgrid_pager_id,
                    postData: { condition: "TaskID='" + tid + "' AND Act='修改'" },
                    jsonReader: {
                        root: "rows",
                        page: "page",
                        total: "total",
                        records: "records",
                        repeatitems: false,
                        id: "ID"
                    }

                });

            }
            , initArray: taskHisGridInitArray
            , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    taskHisGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    taskHisGrid.refreshGrid('1=1');
                }

            }
                });

    //查看单条任务单的历史记录
    function showHis(b, id, t) {
        var title = null;
        var refreshCon = null;
        if (t == "M") {
            title = "任务单：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的历史记录";
            refreshCon = "TaskID='" + id + "' and Act ='修改'";
            taskHisGrid.getJqGrid().jqGrid('hideCol', 'TaskID');
            taskHisGrid.getJqGrid().jqGrid('hideCol', 'subgrid');
        } else {
            title = "已删除的任务单列表如下";
            refreshCon = "Act ='删除'";
            taskHisGrid.getJqGrid().jqGrid('showCol', 'TaskID');
            taskHisGrid.getJqGrid().jqGrid('showCol', 'subgrid');



        }
        $("#taskHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                taskHisGrid.getJqGrid().jqGrid('clearGridData'); 
                //关闭窗口时清除grid中的数据
            }
        });
        $('#taskHisWindow').dialog('open');
        taskHisGrid.refreshGrid(refreshCon);
    }

    //弹出“任务单详细信息”对话框
    $('#dlgProduceTask').dialog({ autoOpen: false, bgiframe: true, resizable: true, width: 600, height: 350, title: '任务单详细信息' });
    $('a[data-task-id]').live('click', function (e) {
        if (e.preventDefault) {
            e.preventDefault();
        }
        else {
            e.returnValue = false;
        }

        var dialogPosition = $(this).offset();//对话框显示位置
        $('#dlgProduceTask').dialog('option', 'position', [dialogPosition.left, dialogPosition.top+15]);
        $('#dlgProduceTask').dialog('open');

        var data = mygrid.getRecordByKeyValue($(this).attr('data-task-id'));

        for (var i in data) {
            if (data[i] != "" && data[i] != null && data[i] == "true") {
                data[i] = "是";
            } if (data[i] != "" && data[i] != null && data[i] == "false") {
                data[i] = "否";
            } else {
                data[i] = data[i];
            }

        }
        $('#dlgProduceTask').empty();
        $('#tmplTask').tmpl(data).appendTo('#dlgProduceTask');//显示详细项数据
    });
    
    var OtherPriceGrid = new MyGrid({
        renderTo: 'otherPriceGrid'
            , width: 500
            , height: 160
            , storeURL: opts.ProduceTaskOtherPrice
            , sortByField: 'ID'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , editSaveUrl: opts.itemUpdateUrl
            , showPageBar: true
            , initArray: [
              { label: '序号', name: 'ID', index: 'ID', width: 50, align: 'center', hidden: true, sortable: false }
            , { label: '加价项目', name: 'PriceType', index: 'PriceType', width: 80, sortable: false }
            , { label: '计价类型', name: 'CalcType', index: 'CalcType', width: 80, sortable: false }
            , { label: '数量/百分比', name: 'Num', index: 'Num', width: 80, sortable: false, formatter: 'number', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
            , { label: '操作', name: 'act', index: 'act', width: 90, sortable: false, align: "center" }
            ]
            , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    OtherPriceGrid.reloadGrid();
                },
                handleRefresh: function (btn) {

                    OtherPriceGrid.refreshGrid();
                }
            }
    });


    window.handleDeleteOtherPrice = function (id) {
        var record = mygrid.getSelectedRecord();
        var TaskID = record["ID"];
        showConfirm("确认信息", "您确定删除此项加价项目？", function () {
            $.post(
                    opts.itemDeleteUrl
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        OtherPriceGrid.refreshGrid("ProduceTaskID='" + TaskID + "'");
                    }
                );
            $(this).dialog("close");
        });
    };

    OtherPriceGrid.addListeners("gridComplete", function () {
        var ids = OtherPriceGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteOtherPrice(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
            OtherPriceGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });
        }

    });

    window.ProduceTaskotherPriceSave = function () {
        var record = mygrid.getSelectedRecord();

        var TaskID = record["ID"];
        mygrid.setFormFieldValue('ProduceTaskID', TaskID);

        $.post(
                opts.OtherPriceSave,
                $("#produceTaskOtherpriceForm form").serialize(),
                function (data) {
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    $("#produceTaskOtherpriceForm form")[0].reset();
                    OtherPriceGrid.refreshGrid("ProduceTaskID='" + TaskID + "'");
                }
            );
    };

    function beforeFormSubmit() {
        var ContractItemsID = mygrid.getFormFieldValue("ConStrength");
        var CustName = mygrid.getFormFieldValue("CustName");
        if (ContractItemsID == "") {
            $("select[name='ConStrength']").addClass("input-validation-error");
            showError('验证错误', '必须填写砼强度信息');
            return false;
        }
        if (CustName == "") {
            $("select[name='CustName']").addClass("input-validation-error");
            showError('验证错误', '必须填写客户名称');
            return false;
        }
        $("select[name='ConStrength']").removeClass("input-validation-error");
        return true;
    }

    linkManChange = function () {
        var linkManField = mygrid.getFormField("LinkMan");
        linkManField.unbind('change');
        linkManField.bind('change', function (event) {
            var linkMan = linkManField.val();
            var selectedLinkMan = $('#LinkMan', '#' + mygrid.getFormId()).children(':selected').text();//在本grid内找到下拉框选择的子项文本
            var linkManId = $('#LinkMan', '#' + mygrid.getFormId()).val();
            mygrid.setFormFieldValue('Tel', '');
            //linkMan linkManId是一样的值都是value值,selectedLinkMan是text的值

            if (linkManId && linkMan == selectedLinkMan) {//填充前场工长电话
                var requestURL = "User.mvc/Get";
                var postParams = { id: linkManId };
                var rData;
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        rData = response.Data;
                        mygrid.getFormField("Tel").val(response.Data == null ? "" : rData.Tel);
                    }
                });
            }
        });
    };
    //新模版
    $("#newTpl>div").die("click");
    $("#newTpl>div").die("mouseover");
    $("#newTpl>div").die("mouseout");
    $("#newTpl>div").live({
        click: function (e) {
            //1、先改变样式
            if ($(this).attr("current")) {
                e.preventDefault();
            } else {
                $("#newTpl>div").removeAttr("current").removeClass("itemhover");
                $(this).attr("current", true).addClass("itemhover");
            }
            //2、找历史数据
            var selectedRecord = mygrid.getSelectedRecord();
            var _previewurl = $(this).attr("PreviewUrl");
            $.post(
                "/ReportDatum.mvc/Find",
                {
                    condition: "Stencil='" + $(this).attr("tplfilename") + "' and ObjectID='" + selectedRecord.ID + "'",
                    page: 1,
                    rows: 30,
                    sidx: "ID",
                    sord: "asc"
                },
                function (resp) {
                    if (!$.isEmptyObject(resp.rows)) {
                        $("#onehisTpl").empty();
                        $.each(resp.rows, function (i, n) {
                            $("<div style='height:20px;line-height:20px;cursor:pointer; margin:1px 0;' id='" + n.ID + "' previewurl='" + _previewurl + "' parms='{\"hisid\":\"" + n.ID + "\"}' hisdata = 1><span style='width:10%;float:left'>" + n.ID + "</span><span style='width:50%;float:left'>" + dataTimeFormat(n.BuildTime) + "</span><span style='width:25%;text-align:right;float:left'>" + n.Builder + "</span><span attid='delHis' id='" + n.ID + "' attv='" + n.ID + "' style='width:13%; text-align:center;float:right; border-left:1px dashed #fff; border-right:1px solid #fff;font-weight:bold; font-size:16px;color:red;' title='删除'>×</span></div>").appendTo($("#onehisTpl"));
                        });
                    } else {
                        $("#onehisTpl").html("无历史数据");
                    }
                }
            );
        },
        mouseover: function (e) {
            if ($(this).attr("current")) { return; }
            $(this).toggleClass('itemhover');
        },
        mouseout: function (e) {
            if ($(this).attr("current")) { return; }
            $(this).toggleClass('itemhover');
        }
    });

    mygrid.getJqGrid().jqGrid('setGridWidth', $("#first-grid").width());
    $("#onehisTpl>div").die("click");
    $("#onehisTpl>div").die("mouseover");
    $("#onehisTpl>div").die("mouseout");
    $("#onehisTpl>div").live({
        click: function (e) {
            //1、先改变样式
            if ($(this).attr("current")) {
                //e.preventDefault();
                $(this).removeAttr("current").removeClass("itemhover");
            } else {
                $("#onehisTpl>div").removeAttr("current").removeClass("itemhover");
                $(this).attr("current", true).addClass("itemhover");
            }
        },
        mouseover: function (e) {
            if ($(this).attr("current")) {
                return;
            } else {
                $(this).addClass('itemhover');
            }

        },
        mouseout: function (e) {
            if ($(this).attr("current")) {
                return;
            }
            else {
                $(this).removeClass("itemhover");
            }
        }
    });
    //删除历史数据
    $("#onehisTpl>div>span[attid=delHis]").die("click");
    $("#onehisTpl>div>span[attid=delHis]").die("mouseover");
    $("#onehisTpl>div>span[attid=delHis]").die("mouseout");
    $("#onehisTpl>div>span[attid=delHis]").live({
        click: function (e) {
            //1、先改变样式
            var id = $(this).attr("attv").toString();
            var _this = $(this);
            $.post(
                "/ReportDatum.mvc/Delete",
                { id: id },
                function (resp) {
                    if (resp.Result) {
                        _this.parent().first().detach();//删除元素
                    }
                }
            );

        },
        mouseover: function (e) {
            $(this).addClass("itemDelhover");

        },
        mouseout: function (e) {
            $(this).removeClass("itemDelhover");
        }
    });

}