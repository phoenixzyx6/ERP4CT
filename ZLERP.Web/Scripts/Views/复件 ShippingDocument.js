
function shippingDocInit(opts) {
    var shippDocGrid = new MyGrid({
        renderTo: 'shippDocGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ProduceDate'
            , sortOrder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 700
            , dialogHeight: 400
        //, singleSelect: true
            , columnReOrder: true
            , rowNumbers: true
            , advancedSearch: true
            , viewrecords : true
		    , initArray: [
                { label: '运输单号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 50, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }

                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '途中', '1': '已回' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '无效', '1': '有效' }, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 50, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '未审核', '1': '已审核' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '客户编号', name: 'CustomerID', index: 'CustomerID', hidden: true }

                , { label: '客户名称', name: 'CustName', index: 'CustName' }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', hidden: true }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }

                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', search: false }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 60 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
                , { label: '级配', name: 'ProduceTask.CarpGrade', index: 'ProduceTask.CarpGrade', width: 80 }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 80 }
                , { label: '清洗泵名称1', name: 'PumpName1', index: 'PumpName1', width: 80 }
                , { label: '清洗泵名称2', name: 'PumpName2', index: 'PumpName2', width: 80 }
                , { label: '清洗泵名称3', name: 'PumpName3', index: 'PumpName3', width: 80 }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 55, align: 'right', search: false }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 55, align: 'right', search: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 55, align: 'right', search: false }
                , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', hidden: true }
                , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', hidden: true }
                , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', hidden: true }
                , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii', hidden: true }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed', hidden: true }
                , { label: '真实坍落度', name: 'RealSlump', index: 'RealSlump', width: 80, search: false }
                , { label: '混凝土方量', name: 'BetonCount', index: 'BetonCount', width: 60, align: 'right', search: false }
                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 55, align: 'right', search: false }
                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 55, align: 'right' }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 55, align: 'right', search: false }
                , { label: '上车余料', name: 'RemainCube', index: 'RemainCube', width: 55, align: 'right', search: false }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 55, align: 'right', search: false }
                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 55, align: 'right' }
                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 55, align: 'right', search: false }
                , { label: '本车余料', name: 'TransferCube', index: 'TransferCube', width: 55, align: 'right', search: false }
                , { label: '其他方量2', name: 'XuCube', index: 'XuCube', width: 60, align: 'right', search: false }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 55, align: 'right', search: false, hidden: true }
                , { label: '过磅方量', name: 'Cube', index: 'Cube', width: 55, align: 'right', search: false }
                , { label: '毛重(T)', name: 'TotalWeight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '皮重(T)', name: 'CarWeight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '净重(T)', name: 'Weight', width: 55, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 80, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
        // , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, width: 55, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
        // , { label: '运费', name: 'SumPrice', index: 'SumPrice' }
                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', search: false, width: 55 }
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', search: false, width: 55 }
                , { label: '调度员', name: 'Signer', index: 'Signer', search: false, width: 50 }
                , { label: '班组', name: 'ForkLift', index: 'ForkLift', search: false, width: 50 }
                , { label: '操作员', name: 'Operator', index: 'Operator', search: false, width: 50 }
                , { label: '计划班组', name: 'PlanClass', index: 'PlanClass', search: false, width: 55 }
                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID', width: 55 }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', search: false }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', search: false, width: 55 }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', search: false, width: 55 }
                , { label: '现场验收人', name: 'Accepter', index: 'Accepter', search: false, width: 65 }
                , { label: '工程运距', name: 'Distance', index: 'Distance', width: 55 }
                , { label: '区间编号', name: 'RegionID', index: 'RegionID', search: false, width: 55 }
                , { label: '前场工长', name: 'LinkMan', index: 'LinkMan', width: 55 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 75 }
                , { label: '工程编号', name: 'ProjectID', index: 'ProjectID', search: false, width: 65 }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', search: false, width: 55 }
                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', search: false, width: 55 }
                , { label: '异常信息', name: 'ExceptionInfo', index: 'ExceptionInfo', width: 200, search: false }
                , { label: '审核人', name: 'AuditMan', index: 'AuditMan', search: false, width: 50 }
                , { label: '公里数', name: 'CarKm', index: 'CarKm', width: 55 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '超时原因', name: 'OverTimeReason', index: 'OverTimeReason', width: 200, search: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    shippDocGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    shippDocGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    shippDocGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            shippDocGrid.getFormField("BetonCount").bind('blur', function () {
                                var BetonCount = shippDocGrid.getFormField("BetonCount").val();
                                var SlurryCount = shippDocGrid.getFormField("SlurryCount").val();
                                shippDocGrid.getFormField("SendCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("ParCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("SignInCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                            });
                            shippDocGrid.getFormField("SlurryCount").bind('blur', function () {
                                var BetonCount = shippDocGrid.getFormField("BetonCount").val();
                                var SlurryCount = shippDocGrid.getFormField("SlurryCount").val();
                                shippDocGrid.getFormField("SendCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("ParCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                                shippDocGrid.getFormField("SignInCube").val((BetonCount - 1 + 1) + (SlurryCount - 1 + 1));
                            });

                        }
                    });
                },
                //打印运输单
                handlePrintDoc: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录打印");
                        return false;
                    }
                    shippDocGrid.showWindow({
                        title: "打印模板选择",
                        width: 300,
                        height: 150,
                        loadFrom: 'TemplateForm',
                        //afterLoaded: function () {
                        //    var shipdocTemplateField = $("input[name='DicID']").val();
                        //}
                        buttons: {
                            "确定": function () {
                                var templateID = $("#TemplateID").val();
                                if (isEmpty(templateID)) {
                                    showMessage("提示", "请选择发货单模板");
                                    return false;
                                }
                                setShipDocTemplate(templateID);
                                printShippingDoc('preview', shippDocGrid.getSelectedKey());
                                $(this).dialog("close");
                            }
                        }
                    })
                },
                //回单处理
                handleTakeBack: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能回单');
                        return;
                    }
                    shippDocGrid.handleEdit({
                        loadFrom: 'TakeBack',
                        btn: btn,
                        afterFormLoaded: function () {
                            //$("#editDeliverTime").text(
                            //$.datepicker.formatTime('hh:mm:ss', "2007-09-12 22:34:00", { ampm: false })
                            //);

                            shippDocGrid.setFormFieldReadOnly("ID", true);
                            var carField = shippDocGrid.getFormField('CarID');
                            carField.unbind("change");
                            carField.bind('change', function (s) {
                                var carid = $(this).val();
                                var driverList = shippDocGrid.getFormField('Driver');
                                driverList.empty();
                                ajaxRequest(opts.getCarInfoUrl, { id: carid }, false, function (response) {
                                    var users = response.Users;
                                    if (!isEmpty(users)) {
                                        //设定司机
                                        for (var i = 0; i < users.length; i++) {
                                            var user = users[i];
                                            driverList.append("<option value=\"" + user.UserID + "\">" + user.TrueName + "</option>");
                                        }
                                        driverList.val(data['Driver']);
                                    }
                                });
                            });
                            carField.trigger('change');

                            //shippDocGrid.setFormFieldDisabled("ConstructUnit", true);
                            //shippDocGrid.setFormFieldDisabled("ProjectName", true);
                            //shippDocGrid.setFormFieldDisabled("CustName", true);
                            //shippDocGrid.setFormFieldDisabled("ContractName", true);

                            //                            window.setTimeout(function () {
                            //                                shippDocGrid.setFormFieldChecked("IsBack", true);
                            //                                $("input[name='ArriveTime']").val(dataTimeFormat(new Date()));
                            //                            }, 500);

                        }
                    });
                }
                //作废
                , handleGarbage: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能作废');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 0 || isEffective == 'false') {
                        showMessage('提示', '已作废的发货单不能作废');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "发货单作废原因",
                        width: 350,
                        height: 200,
                        loadFrom: 'GarbageForm',
                        btn: btn,
                        postUrl: opts.garbageUrl,
                        postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            $("#remark").val("");
                        }

                    });
                }
                //取消作废
                , handleCancelGarbage: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能取消作废');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 1 || isEffective == 'true') {
                        showMessage('提示', '未作废的发货单不能取消作废');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "发货单取消作废原因",
                        width: 350,
                        height: 200,
                        loadFrom: 'GarbageForm',
                        btn: btn,
                        postUrl: opts.garbageUrl,
                        postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            $("#remark").val("");
                        }
                    });
                }
                //删除
                , handleDelete: function (btn) {
                    var records = shippDocGrid.getSelectedRecords();
                    for (var i = 0; i < records.length; i++) {
                        var auditValue = records[i].IsAudit;
                        if (auditValue == 1 || auditValue == 'true') {
                            showMessage('提示', '已审核的发货单不能删除');
                            return;
                        }
                    }

                    shippDocGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //报表设计
                , handleDesign: function (btn) {
                    //使用选中的发货单作设计数据
                    var docId = shippDocGrid.getSelectedKey();
                    if (!isEmpty(docId)) {
                        printShippingDoc('design', docId);
                    }
                    else {//未选中任务发货单则使用测试数据设计
                        shippingDocDesign();
                    }
                },
                handleCheck: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }

                    var data = shippDocGrid.getSelectedRecord();
                    var auditValue = data.IsAudit;
                    if (auditValue == 1 || auditValue == 'true') {
                        showMessage('提示', '已审核的发货单不能修改');
                        return;
                    }

                    var isEffective = data.IsEffective;
                    if (isEffective == 0 || isEffective == 'false') {
                        showMessage('提示', '已作废的发货单不能修改');
                        return;
                    }

                    shippDocGrid.handleEdit({
                        title: "出厂检测",
                        width: 350,
                        height: 200,
                        loadFrom: 'OutCheckForm',
                        btn: btn
                    });
                }
                //审核票据
                , handleAudit: function () {
                    var keys = shippDocGrid.getSelectedKeys();
                    if (keys.length > 1) {
                        var records = shippDocGrid.getSelectedRecords();
                        for (var i = 0; i < records.length; i++) {
                            var auditValue = records[i].IsAudit;
                            if (auditValue == 1 || auditValue == 'true') {
                                showMessage('提示', '请选择未审核的运输单！');
                                return;
                            }
                        }
                        var requestURL = opts.handleBatchAuditUrl;
                        var postParams = { id: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            shippDocGrid.reloadGrid();
                        });
                    }
                    else {
                        var selectedRecord = shippDocGrid.getSelectedRecord();
                        var confirmMessage = "您确定将此运输单设置为&nbsp;<font color=red><b>审核通过</b></font>&nbsp;状态吗？";

                        if (selectedRecord.IsAudit == "true" || selectedRecord.IsAudit == 1) {
                            confirmMessage = "您确定需要将此运输单设置为&nbsp;<font color=green><b>审核未通过</b></font>&nbsp;吗？";
                        }
                        //确认操作
                        showConfirm("确认信息", confirmMessage, function () {
                            ajaxRequest(
                            opts.AuditUrl,
                            {
                                id: selectedRecord.ID,
                                status: selectedRecord.IsAudit
                            },
                            true,
                            function () {
                                shippDocGrid.reloadGrid();
                            }
                        );
                            $(this).dialog("close");
                        });
                    }
                },
                handleMultiAudit: function (btn) {

                    var keys = shippDocGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    }

                    var records = shippDocGrid.getSelectedRecords();
                    var signInCube = 0;
                    for (var i = 0; i < records.length; ++i) {
                        signInCube += isEmpty(records[i].SignInCube) ? 0 : parseFloat(records[i].SignInCube);
                    }
                    var xuCube = 0;
                    for (var i = 0; i < records.length; ++i) {
                        xuCube += isEmpty(records[i].XuCube) ? 0 : parseFloat(records[i].XuCube);
                    }
                    showConfirm("确认信息", "签收方量为：" + signInCube + "。其他方量2为：" + xuCube + "。<br />审核后将不允许删除、修改。是否继续审核？", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            opts.MultiAuditUrl,
                            {
                                ids: keys
                            },
                            true,
                            function () {
                                $(btn.currentTarget).button({ disabled: false });

                                shippDocGrid.reloadGrid();
                            }
                        );

                        $(this).dialog("close");
                    });

                }
                //查看生产登记修改历史记录
                , handleViewHis: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    showHis(btn, selectedRecord.ID);
                }
                //查看运输单修改历史记录
                , handleShipDocHis: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    showShipDocHis(btn, selectedRecord.ID);
                }
                , FindyunshuRecord: function (btn) {
                    condition = "ShipDocType='0'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindqitaRecord: function (btn) {
                    condition = "ShipDocType!='0'";
                    shippDocGrid.refreshGrid(condition);
                }


                , FindTodayRecord: function (btn) {

                    var d = new Date();
                    var today = d.format("Y-m-d");
                    condition = "ProduceDate between '" + today + " 00:00:00' and '" + today + " 23:59:59'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindTomRecord: function (btn) {
                    var uom = GetDateBySpan(1);
                    condition = "ProduceDate between '" + uom + " 00:00:00' and '" + uom + " 23:59:59'";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindAllRecord: function (btn) {
                    condition = "1=1";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindCustRecord: function (btn) {
                    shippDocGrid.showWindow({
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
                                var BeginTime = $("#sBeginTime").val();
                                var EndTime = $("#sEndTime").val();
                                condition = "ProduceDate between '" + BeginTime + "' and '" + EndTime + "'";
                                shippDocGrid.refreshGrid(condition);
                                var checked = $("#sIsAutoClose")[0].checked;
                                if (checked) {
                                    $(this).dialog('close');
                                }
                            }
                        }
                    });
                }
                , handleUpdateMetage: function (btn) {
                    shippDocGrid.handleEdit({
                        title: "出厂检测",
                        width: 550,
                        height: 200,
                        postUrl: opts.MetageUpdateUrl,
                        loadFrom: 'MetageUpdate',
                        btn: btn,
                        afterFormLoaded: function () {
                            shippDocGrid.getFormField("TotalWeight").bind('blur', function () {
                                CalcCube();
                            });
                            shippDocGrid.getFormField("CarWeight").bind('blur', function () {
                                CalcCube();
                            });

                            shippDocGrid.getFormField("Exchange").bind('blur', function () {
                                CalcCube();
                            });
                        }
                    });

                }
                //显示-其他方量 列
                , hanldeShowHideCube: function (btn) {

                    shippDocGrid.getJqGrid().jqGrid('showCol', 'OtherCube'); //显示列
                    //shippDocGrid.getJqGrid().jqGrid('hideCol', 'XuCube'); ; //隐藏列
                }
                //超时原因设置
                , hanldeOverTimeReason: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行操作");
                        return false;
                    }
                    shippDocGrid.handleEdit({
                        title: "超时原因设置",
                        width: 350,
                        height: 180,
                        loadFrom: 'OverTimeReasonForm',
                        btn: btn,
                        //postUrl: opts.OverTimeReasonUrl,
                        //postData: { id: data.ID, status: data.IsEffective, remark: $("#remark").val() },
                        afterFormLoaded: function () {
                            //$("#a01_userGrid").jqGrid("setGridParam", { postData: queryData }).trigger("reloadGrid")
                        }
                        , postCallBack: function (response) {
                            //shippDocGrid.reloadGrid();
                        }
                    });
                }
            }
    });

    shippDocGrid.getJqGrid().jqGrid('setGridWidth', $("#shippDocGrid").width());
    shippDocGrid.addListeners("gridComplete", function () {
        var ids = shippDocGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var record = shippDocGrid.getRecordByKeyValue(ids[i]);
            if (record.XuCube != null && record.XuCube > 0 && record.IsAudit == 'true') {
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "#00BBFF";//设置行背景颜色
            }

        }
    });

    //运输单修改历史
    var shipDocHisGrid = new MyGrid({
        renderTo: 'shipDocHisGrid'
            , width: 720
            , height: 240
            , storeURL: opts.findShipDocHisUrl
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , emptyrecords: '当前无任何修改'
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false }
                , { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '客户编号', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'CustName', searchoptions: { sopt: ['cn']} }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', hidden: true }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
                , { label: '配合比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', hidden: true }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', searchoptions: { sopt: ['cn']} }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 100 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 80 }
                , { label: '泵工', name: 'PumpMan', index: 'PumpMan', width: 80 }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', search: false }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 60, align: 'right', search: false }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 50, align: 'right', search: false }
                , { label: '抗渗等级', name: 'ImpGrade', index: 'ImpGrade', hidden: true }
                , { label: '抗压等级', name: 'ImyGrade', index: 'ImyGrade', hidden: true }
                , { label: '抗冻等级', name: 'ImdGrade', index: 'ImdGrade', hidden: true }
                , { label: '骨料粒径', name: 'CarpRadii', index: 'CarpRadii', hidden: true }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed', hidden: true }
                , { label: '真实坍落度', name: 'RealSlump', index: 'RealSlump', width: 80, search: false }
                , { label: '混凝土方量', name: 'BetonCount', index: 'BetonCount', width: 60, align: 'right', search: false }
                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 60, align: 'right', search: false }
                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 60, align: 'right' }
                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 60, align: 'right', search: false }
                , { label: '剩余方量', name: 'RemainCube', index: 'RemainCube', width: 60, align: 'right', search: false }
                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 60, align: 'right', search: false }
                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 60, align: 'right' }
                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 60, align: 'right', search: false }
                , { label: '转料方量', name: 'TransferCube', index: 'TransferCube', width: 60, align: 'right', search: false }
                , { label: '其他方量2', name: 'XuCube', index: 'XuCube', width: 80 }
                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 60, align: 'right', search: false }
                , { label: '过磅方量', name: 'Cube', index: 'Cube', width: 60, align: 'right', search: false }
                , { label: '毛重(T)', name: 'TotalWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '皮重(T)', name: 'CarWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '净重(T)', name: 'Weight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
                , { label: '运输单类型', name: 'ShipDocType', index: 'ShipDocType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipDocType' }, align: 'center', stype: 'select', searchoptions: { value: dicToolbarSearchValues('ShipDocType')} }
                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '当班司机', name: 'Driver', index: 'Driver', width: 60 }
                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', width: 60 }
                , { label: '发货员', name: 'Signer', index: 'Signer', width: 60 }
                , { label: '上料员', name: 'ForkLift', index: 'ForkLift', width: 60 }
                , { label: '操作员', name: 'Operator', index: 'Operator', width: 60 }
                , { label: '计划班组', name: 'PlanClass', index: 'PlanClass', width: 60 }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge', 'le', 'eq', 'gt', 'lt']} }
                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID', width: 80 }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit', width: 100 }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
                , { label: '委托单位', name: 'EntrustUnit', index: 'EntrustUnit', width: 100 }
                , { label: '发货单公司', name: 'Accepter', index: 'Accepter' }
                , { label: '工程运距', name: 'Distance', index: 'Distance', width: 60 }
                , { label: '区间编号', name: 'RegionID', index: 'RegionID', width: 80, search: false }
                , { label: '前厂工长', name: 'LinkMan', index: 'LinkMan', width: 60 }
                , { label: '工地电话', name: 'Tel', index: 'Tel', width: 80 }
                , { label: '工程编号', name: 'ProjectID', index: 'ProjectID', width: 80 }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', width: 60, search: false }
                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', width: 80 }
                , { label: '入库上传状态', name: 'InStatus', index: 'InStatus', width: 80, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '出库上传状态', name: 'OutStatus', index: 'OutStatus', width: 80, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '异常信息', name: 'ExceptionInfo', index: 'ExceptionInfo', width: 100 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 60, search: false }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 120, search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 60, search: false }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', width: 120, search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
            ]
            , autoLoad: false
    });

    //生产登记修改历史
    var dispatchHisGrid = new MyGrid({
        renderTo: 'dispatchHisGrid'
            , width: 720
            , height: 240
            , storeURL: opts.dispatchHisStoreUrl
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , emptyrecords: '当前无任何修改'
            , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 50, sortable: false }
                , { label: '操作', name: 'Act', index: 'Act', align: 'center', width: 60, sortable: false }
                , { label: '生产登记编号', name: 'DispatchID', index: 'DispatchID', width: 80, sortable: false }
                , { label: '任务单编号', name: 'TaskID', index: 'TaskID', width: 80, sortable: false }
                , { label: '生产线', name: 'ProductLineID', index: 'ProductLineID', align: 'center', width: 60 }
                , { label: '登记顺序', name: 'DispatchOrder', index: 'DispatchOrder', align: 'center', width: 60 }
                , { label: '启动时间', name: 'StartupTime', index: 'StartupTime', align: 'center', formatter: 'datetime', width: 130, sortable: false }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 60, align: 'center', sortable: false }
                , { label: '司机', name: 'Driver', index: 'Driver', width: 60, align: 'center', sortable: false }
                , { label: '执行时间', name: 'ExecTime', index: 'ExecTime', align: 'center', formatter: 'datetime', width: 130, sortable: false }
            ]
            , autoLoad: false
    });
    //生产登记修改历史
    //    var dispatchHisGrid = new MyGrid({
    //        renderTo: 'dispatchHisGrid'
    //            , width: 720
    //            , height: 240
    //            , storeURL: opts.ShipHisStoreUrl
    //            , sortByField: 'ID'
    //            , sortOrder: 'ASC'
    //            , primaryKey: 'ID'
    //            , setGridPageSize: 30
    //            , singleSelect: true
    //            , showPageBar: true
    //            , toolbarSearch: false
    //            , excelExport: false
    //            , emptyrecords: '当前无任何修改'
    //            , initArray: [
    //                { label: '流水号', name: 'ID', index: 'ID', width: 50, searchoptions: { sopt: ['cn']} }
    //                , { label: '修改人', name: 'Modifier', index: 'Modifier', align: 'center', width: 50 }
    //                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'datetime' }
    //                , { label: '车号', name: 'CarID', index: 'CarID', width: 50, align: 'right', searchoptions: { sopt: ['eq']} }
    //                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
    //                , { label: '是否带回', name: 'IsBack', index: 'IsBack', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
    //                , { label: '是否有效', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
    //                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
    //                , { label: '客户名称', name: 'CustName', index: 'CustName' }
    //                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
    //                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr', search: false }
    //                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
    //                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 60 }
    //                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos' }
    //                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 80 }
    //                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 60, align: 'right', search: false }
    //                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 60, align: 'right', search: false }
    //                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50, align: 'center' }
    //                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 50, align: 'right', search: false }
    //                
    //                , { label: '混凝土方量', name: 'BetonCount', index: 'BetonCount', width: 50, align: 'right', search: false }
    //                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 50, align: 'right', search: false }
    //                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 50, align: 'right' }
    //                , { label: '出票方量', name: 'ParCube', index: 'ParCube', width: 50, align: 'right', search: false }
    //                , { label: '上车余料', name: 'RemainCube', index: 'RemainCube', width: 50, align: 'right', search: false }
    //                , { label: '运输方量', name: 'ShippingCube', index: 'ShippingCube', width: 50, align: 'right', search: false }
    //                , { label: '签收方量', name: 'SignInCube', index: 'SignInCube', width: 50, align: 'right' }
    //                , { label: '报废方量', name: 'ScrapCube', index: 'ScrapCube', width: 50, align: 'right', search: false }
    //                , { label: '本车余料', name: 'TransferCube', index: 'TransferCube', width: 50, align: 'right', search: false }
    //                , { label: '其他方量', name: 'OtherCube', index: 'OtherCube', width: 50, align: 'right', search: false }
    //                , { label: '过磅方量', name: 'Cube', index: 'Cube', width: 50, align: 'right', search: false }
    //                , { label: '毛重(T)', name: 'TotalWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
    //                , { label: '皮重(T)', name: 'CarWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
    //                , { label: '净重(T)', name: 'Weight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
    //                , { label: '换算率(T/m³)', name: 'Exchange', width: 80, formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right' }
    //                
    //                , { label: '出厂时间', name: 'DeliveryTime', index: 'DeliveryTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
    //                , { label: '回厂时间', name: 'ArriveTime', index: 'ArriveTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
    //                , { label: '当班司机', name: 'Driver', index: 'Driver', search: false }
    //                , { label: '质检员', name: 'Surveyor', index: 'Surveyor', search: false }
    //                , { label: '发货员', name: 'Signer', index: 'Signer', search: false }
    //                , { label: '上料员', name: 'ForkLift', index: 'ForkLift', search: false }
    //                , { label: '经办人', name: 'Operator', index: 'Operator', search: false }
    //                , { label: '机组编号', name: 'ProductLineID', index: 'ProductLineID' }
    //                , { label: '现场验收人', name: 'Accepter', index: 'Accepter', search: false }
    //                , { label: '前厂工长', name: 'LinkMan', index: 'LinkMan' }
    //                , { label: '工地电话', name: 'Tel', index: 'Tel' }
    //                , { label: '打印次数', name: 'PrintCount', index: 'PrintCount', search: false }
    //                , { label: '小票票号', name: 'TicketNO', index: 'TicketNO', search: false }
    //                , { label: '异常信息', name: 'ExceptionInfo', index: 'ExceptionInfo', width: 200, search: false }
    //                , { label: '审核人', name: 'AuditMan', index: 'AuditMan', search: false }
    //                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', search: false, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
    //                , { label: '备注', name: 'Remark', index: 'Remark', width: 200, search: false }
    //            ]
    //            , autoLoad: false
    //    });

    //查看运输单对应的生产登记历史记录
    function showHis(b, id) {
        var title = "运输单：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的历史记录";
        var refreshCon = "ShipDocID='" + id + "'";

        $("#dispatchHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                dispatchHisGrid.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
            }
        });
        $('#dispatchHisWindow').dialog('open');
        dispatchHisGrid.refreshGrid(refreshCon);
    }

    //查看运输单对应的历史记录
    function showShipDocHis(b, id) {
        var title = "运输单：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的运输单历史记录";
        var refreshCon = "ShipDocID='" + id + "'";
        $("#shipDocHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                shipDocHisGrid.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
            }
        });
        $('#shipDocHisWindow').dialog('open');
        shipDocHisGrid.refreshGrid(refreshCon);
    }

    function CalcCube() {
        var TotalWeight = shippDocGrid.getFormField("TotalWeight").val();
        var CarWeight = shippDocGrid.getFormField("CarWeight").val();
        var Weight = TotalWeight - CarWeight;

        var Exchange = shippDocGrid.getFormField("Exchange").val();
        var Cube = 0;
        if (Exchange > 0) {
            Cube = parseFloat(Weight / Exchange).toFixed(2);
        }

        shippDocGrid.getFormField("Weight").val(Weight);
        shippDocGrid.getFormField("Cube").val(Cube);

    }
}
 