//原料入库
function stuffInInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: "InNum!=0"
		    , sortByField: 'InDate'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 740
            , dialogHeight: 350
		    , showPageBar: true
		    , initArray: [
                  { label: '流水号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, formatter: stuffinIdFormatter, unformat: stuffinIdUnFormatter }
                , { label: '进厂时间', name: 'InDate', index: 'InDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: ' 入库原料', name: 'StuffName', index: 'StuffInfo.StuffName', width: 100 }
                , { label: ' 筒仓编号', name: 'SiloID', index: 'SiloID', hidden: true }
                , { label: ' 入库筒仓', name: 'SiloName', index: 'Silo.SiloName', width: 100 }
                , { label: '原料厂商', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: ' 供货厂商', name: 'SupplyName', index: 'SupplyInfo.SupplyName' }
                , { label: '采购单号', name: 'StockPactID', index: 'StockPactID', width: 60 }
                , { label: '采购单号', name: 'StockPactNo', index: 'StockPactNo', sortable: false, width: 60, hidden: true }
                , { label: '单价(吨/元)', name: 'UnitPrice', index: 'UnitPrice', width: 70, hidden: true }

                , { label: ' 运输车号', name: 'CarNo', index: 'CarNo', width: 80 }
                , { label: '入库数量(吨)', name: 'InNum', index: 'InNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '换算系数', name: 'Proportion', index: 'Proportion', width: 60 }
                , { label: '结算数量(吨)', name: 'FootNum', index: 'FootNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '厂商数量(吨)', name: 'SupplyNum', index: 'SupplyNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '磅差(吨)', name: 'Bangcha', index: 'Bangcha', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '最终结算数量(吨)', name: 'FinalFootNum', index: 'FinalFootNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '总价', name: 'TotalPrice', index: 'TotalPrice', width: 80 }
                , { label: ' 存储状态', name: 'Lifecycle', index: 'Lifecycle', formatter: LiftclyFmt, unformat: LiftclyUnFmt, width: 60 }

                , { label: '总重(吨)', name: 'TotalNum', index: 'TotalNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '空车重(吨)', name: 'CarWeight', index: 'CarWeight', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '运送数量(吨)', name: 'TransportNum', index: 'TransportNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '进货数量(吨)', name: 'StockNum', index: 'StockNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '含水率%(明扣)', name: 'WRate', index: 'WRate', align: 'right', width: 100 }
                , { label: '暗扣（KG）', name: 'DarkWeight', index: 'DarkWeight', align: 'right', width: 100, hidden: true }
                , { label: '运输公司', name: 'TransportID', index: 'TransportID', hidden: true }
                , { label: ' 运输公司', name: 'TransportName', index: 'TransportInfo.SupplyName' }
                , { label: '司机', name: 'Driver', index: 'Driver', width: 80 }
                , { name: 'pic1', index: 'pic1', hidden: true }
                , { name: 'pic2', index: 'pic2', hidden: true }
                , { name: 'pic3', index: 'pic3', hidden: true }
                , { name: 'pic4', index: 'pic4', hidden: true }
                , { name: 'IsBack', index: 'IsBack', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '司磅员', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '过磅时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 130 }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '客户', name: 'CustName', index: 'CustName' }
                , { label: '原料来源地', name: 'SourceAddr', index: 'SourceAddr' }
                , { label: '出厂时间', name: 'OutDate', index: 'OutDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: 'A/H', name: 'AH', index: 'AH', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AH'} }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: ' 经办人', name: 'Operator', index: 'Operator' }
                , { label: ' 结算状态', name: 'FootStatus', index: 'FootStatus', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '磅差2(吨)', name: 'Bangcha2', index: 'Bangcha2', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '是否月结', name: 'IsMonth', index: 'IsMonth', formatter: booleanFmt, unformat: booleanUnFmt, width: 60, stype: 'select', searchoptions: { value: booleanSearchValues()} }

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
                        btn: btn,
                        afterFormLoaded: function () {
                            StuffInfoChange0();
                            StuffInfoPriceChange();

                            myJqGrid.getFormField("TransportNum_T").bind('blur', function () {
                                var TransportNum_T = myJqGrid.getFormField("TransportNum_T").val();
                                myJqGrid.getFormField("TransportNum").val(TransportNum_T * 1000);
                            });
                            myJqGrid.getFormField("SupplyNum_T").bind('blur', function () {
                                var SupplyNum_T = myJqGrid.getFormField("SupplyNum_T").val();
                                myJqGrid.getFormField("SupplyNum").val(SupplyNum_T * 1000);
                            });
                            myJqGrid.getFormField('Proportion').bind('blur', function () {

                                CalcInNum();
                            });
                            myJqGrid.getFormField("TotalNum_T").bind('blur', function () {
                                var TotalNum_T = myJqGrid.getFormField("TotalNum_T").val();
                                myJqGrid.getFormField("TotalNum").val(TotalNum_T * 1000);
                                CalcInNum();
                            });
                            myJqGrid.getFormField("CarWeight_T").bind('blur', function () {
                                var CarWeight_T = myJqGrid.getFormField("CarWeight_T").val();
                                myJqGrid.getFormField("CarWeight").val(CarWeight_T * 1000);

                                var TotalNum_T = myJqGrid.getFormField("TotalNum_T").val();
                                CalcInNum();

                            });
                            myJqGrid.getFormField("StockNum_T").bind('blur', function () {
                                var StockNum_T = myJqGrid.getFormField("StockNum_T").val();
                                myJqGrid.getFormField("StockNum").val(StockNum_T * 1000);
                            });
                            myJqGrid.getFormField("InNum_T").bind('blur', function () {
                                var InNum_T = myJqGrid.getFormField("InNum_T").val();
                                myJqGrid.getFormField("InNum").val(InNum_T * 1000);
                            });

                            myJqGrid.getFormField("FootNum_T").bind('blur', function () {
                                var FootNum_T = myJqGrid.getFormField("FootNum_T").val();
                                myJqGrid.getFormField("FootNum").val(FootNum_T * 1000);
                            });

                            myJqGrid.getFormField("FinalFootNum_T").bind('blur', function () {
                                var FinalFootNum_T = myJqGrid.getFormField("FinalFootNum_T").val();
                                myJqGrid.getFormField("FinalFootNum").val(FinalFootNum_T * 1000);
                            });
                            myJqGrid.getFormField("Bangcha_T").bind('blur', function () {
                                var Bangcha_T = myJqGrid.getFormField("Bangcha_T").val();
                                myJqGrid.getFormField("Bangcha").val(Bangcha_T * 1000);
                                CalcInNum();
                            });
                            myJqGrid.getFormField("WRate").bind('blur', function () {
                                CalcInNum();
                            });
                            myJqGrid.getFormField("Bangcha2_T").bind('blur', function () {
                                var Bangcha2_T = myJqGrid.getFormField("Bangcha2_T").val();
                                myJqGrid.getFormField("Bangcha2").val(Bangcha2_T * 1000);

                            });
                        }
                    });
                },
                handleEdit: function (btn) {

                    var data = myJqGrid.getSelectedRecord();

                    if (data && data.Lifecycle == '1') {
                        showMessage('该入库单已审核，不允许再修改');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            StuffInfoChange0();
                            //绑定库位值
                            //myJqGrid.getFormField("StuffID").change();
                            $('#SiloID').disableSelection(); //ajax没有返回正确的数据前不能选择筒仓，以免选错 2013-03-28 徐毅力
                            ajaxRequest(
                                opts.IsGuLiaoUrl,
                                { id: data.StuffID },
                                false,
                                function (response) {
                                    $('#SiloID').enableSelection();
                                    if (response.Result) {
                                        bindSelectData(
                                            $('#SiloID'),
                                            opts.findSiloUrl,
                                            { condition: "StuffID IN(SELECT StuffID FROM stuffinfo WHERE StuffTypeID IN (SELECT StuffTypeID FROM dbo.StuffType WHERE FinalStuffType IN(SELECT FinalStuffType FROM dbo.StuffType WHERE StuffTypeID IN(SELECT StuffTypeID FROM dbo.StuffInfo WHERE StuffID = '" + data.StuffID + "'))))" }
                                            , function () {
                                                $("#SiloID").val(data.SiloID);
                                            }
                                        );
                                    } else {
                                        bindSelectData(
                                            $('#SiloID'),
                                            opts.findSiloUrl,
                                            { condition: "StuffID = '" + data.StuffID + "'" }
                                            , function () {
                                                $("#SiloID").val(data.SiloID);
                                            }
                                        );
                                    }
                                }
                            );
                            StuffInfoPriceChange();
                            //setTimeout(function () { myJqGrid.setFormFieldValue("SiloID", data.SiloID); }, 500);
                            myJqGrid.getFormField("TransportNum_T").val(data.TransportNum / 1000);
                            myJqGrid.getFormField("SupplyNum_T").val(data.SupplyNum / 1000);
                            myJqGrid.getFormField("TotalNum_T").val(data.TotalNum / 1000);
                            myJqGrid.getFormField("CarWeight_T").val(data.CarWeight / 1000);
                            myJqGrid.getFormField("StockNum_T").val(data.StockNum / 1000);
                            myJqGrid.getFormField("FootNum_T").val(data.FootNum / 1000);
                            myJqGrid.getFormField("FinalFootNum_T").val(data.FinalFootNum / 1000);
                            myJqGrid.getFormField("Bangcha_T").val(data.Bangcha / 1000);
                            myJqGrid.getFormField("InNum_T").val(data.InNum / 1000);
                            myJqGrid.getFormField("Bangcha2_T").val(data.Bangcha2 / 1000);

                            myJqGrid.getFormField("TransportNum_T").bind('blur', function () {
                                var TransportNum_T = myJqGrid.getFormField("TransportNum_T").val();
                                myJqGrid.getFormField("TransportNum").val(TransportNum_T * 1000);
                            });
                            myJqGrid.getFormField("SupplyNum_T").bind('blur', function () {
                                var SupplyNum_T = myJqGrid.getFormField("SupplyNum_T").val();
                                myJqGrid.getFormField("SupplyNum").val(SupplyNum_T * 1000);
                            });
                            myJqGrid.getFormField('Proportion').bind('blur', function () {

                                CalcInNum();
                            });
                            myJqGrid.getFormField("TotalNum_T").bind('blur', function () {
                                var TotalNum_T = myJqGrid.getFormField("TotalNum_T").val();
                                myJqGrid.getFormField("TotalNum").val(TotalNum_T * 1000);
                                CalcInNum();
                            });
                            myJqGrid.getFormField("CarWeight_T").bind('blur', function () {
                                var CarWeight_T = myJqGrid.getFormField("CarWeight_T").val();
                                myJqGrid.getFormField("CarWeight").val(CarWeight_T * 1000);

                                var TotalNum_T = myJqGrid.getFormField("TotalNum_T").val();

                                CalcInNum();

                            });
                            myJqGrid.getFormField("StockNum_T").bind('blur', function () {
                                var StockNum_T = myJqGrid.getFormField("StockNum_T").val();
                                myJqGrid.getFormField("StockNum").val(StockNum_T * 1000);
                            });
                            myJqGrid.getFormField("InNum_T").bind('blur', function () {
                                var InNum_T = myJqGrid.getFormField("InNum_T").val();
                                myJqGrid.getFormField("InNum").val(InNum_T * 1000);
                            });

                            myJqGrid.getFormField("FootNum_T").bind('blur', function () {
                                var FootNum_T = myJqGrid.getFormField("FootNum_T").val();
                                myJqGrid.getFormField("FootNum").val(FootNum_T * 1000);
                            });

                            myJqGrid.getFormField("FinalFootNum_T").bind('blur', function () {
                                var FinalFootNum_T = myJqGrid.getFormField("FinalFootNum_T").val();
                                myJqGrid.getFormField("FinalFootNum").val(FinalFootNum_T * 1000);
                            });
                            myJqGrid.getFormField("Bangcha_T").bind('blur', function () {
                                var Bangcha_T = myJqGrid.getFormField("Bangcha_T").val();
                                myJqGrid.getFormField("Bangcha").val(Bangcha_T * 1000);
                                CalcInNum();
                            });

                            myJqGrid.getFormField("WRate").bind('blur', function () {
                                CalcInNum();
                            });
                            myJqGrid.getFormField("Bangcha2_T").bind('blur', function () {
                                var Bangcha2_T = myJqGrid.getFormField("Bangcha2_T").val();
                                myJqGrid.getFormField("Bangcha2").val(Bangcha2_T * 1000);

                            });
                        }
                    });
                },
                handleDelete: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.Lifecycle == '-1') {
                        showMessage('该入库单已被删除，不允许再进行删除操作');
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handlePrint: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录打印");
                        return false;
                    }
                    printStuffinDoc('preview', myJqGrid.getSelectedKey());
                },
                handleDesign: function (btn) {
                    //使用选中的出入库单据作设计数据

                    var docId = myJqGrid.getSelectedKey();
                    if (!isEmpty(docId)) {
                        printStuffinDoc('design', docId);
                    }
                    else {
                        StuffInRepDesign();
                    }
                },
                //取消审核
                handleUnAuditContract: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showError('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.IsMonth == "true") {
                        showError('此记录已月结，不能取消审核');
                        return;
                    }
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    var auditValue = selectedRecord.Lifecycle;
                    if (auditValue == 0) {
                        showError('提示', '该合同正处于未审核状态！');
                        return;
                    } else {
                        //确认操作
                        showConfirm("确认信息", "您确定要<font color=green><b>取消审核</b></font>吗？", function () {
                            ajaxRequest(
                                "/StuffIn.mvc/UnAudit",
                                {
                                    contractID: selectedRecord.ID,
                                    auditStatus: 0
                                },
                                true,
                                function () {
                                    myJqGrid.refreshGrid();
                                }
                            );
                            $(this).dialog("close");
                        });
                    }
                },
                handleNeedAudit: function (btn) {
                    myJqGrid.refreshGrid("Lifecycle=0 and (StuffInfo.StuffType like 'CE%' or StuffInfo.StuffType like 'AIR%' or StuffInfo.StuffType like '%ADM%')");
                },
                handleMultiAudit: function (btn) {

                    StuffInfoChange();
                    var keys = myJqGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    };
                    myJqGrid.showWindow({
                        btn: btn,
                        title: '入库审核',
                        loadFrom: 'ConfirmDiv',
                        width: 420,
                        height: 300,
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "保存": function () {

                                var si = $($("[name='StuffID_S']")[0]);
                                var stuffinfoid = si.val();
                                if (si.val() == '' || si.val() == null) {
                                    showError("提示", "材料不能为空");
                                    return;
                                }
                                var sprice = $($("[name='UnitPrice']")[0]);
                                if (sprice.val() == '' || sprice.val() == null) {
                                    showError("提示", "单价不能为空");
                                    return;
                                }

                                if (sprice.val() * 1 <= 0) {
                                    showError("提示", "单价必须是大于0的数");
                                    return;
                                }
                                var spnum = $($("[name='SupplyNum']")[0]);

                                if (spnum.val() == '' || spnum.val() == null) {
                                    showError("提示", "厂商数量不能为空");
                                    return;
                                }
                                //执行ajax请求
                                var postData = $("#ConfirmDiv form").serialize();
                                //附加额外的参数
                                ajaxRequest(opts.MultiAuditUrl, { datas: postData, ids: keys, stuffinfoid: stuffinfoid }, true,
                                    function (response) {
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
                handleAudit: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行审核操作");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.Lifecycle == '1') {
                        showMessage('该入库单已审核，不允许再次审核');
                        return;
                    } if (record && record.Lifecycle == -1) {
                        showMessage('该入库单已删除，不允许审核');
                        return;
                    }
                    showConfirm("确认信息", "是否审核，审核后将不允许删除，修改", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            opts.AuditUrl,
                            {
                                id: record.ID
                            },
                            true,
                            function () {
                                $(btn.currentTarget).button({ disabled: false });
                                myJqGrid.refreshGrid('1=1');
                            }
                        );
                        $(this).dialog("close");
                    });
                },
                handleToLabRecord: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行抽样");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.Lifecycle == -1) {
                        showMessage('该入库单已删除，不允许抽样');
                        return;
                    }
                    showConfirm("确认信息", "是否对该单进行抽样", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            "/StuffIn.mvc/ToLabRecord",
                            {
                                id: record.ID
                            },
                            true,
                            function () {
                                $(btn.currentTarget).button({ disabled: false });
                                myJqGrid.refreshGrid('1=1');
                            }
                        );
                        $(this).dialog("close");
                    });
                },
                handleMultiAuditZ: function (btn) {

                    var keys = myJqGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录进行审核");
                        return;
                    }
                    showConfirm("确认信息", "是否审核，审核后将不允许删除，修改", function (btn) {
                        $(btn.currentTarget).button({ disabled: true });
                        ajaxRequest(
                            opts.MultiAuditUrlZ,
                            {
                                ids: keys
                            },
                            true,
                            function () {
                                $(btn.currentTarget).button({ disabled: false });
                                myJqGrid.reloadGrid();
                            }
                        );
                        $(this).dialog("close");

                    });

                }
                //显示-单价 列
                , hanldeShowHideUnitPrice: function (btn) {
                    myJqGrid.getJqGrid().jqGrid('showCol', 'UnitPrice'); 
                }
                //显示-暗扣 列
                , hanldeShowHideDarkWeight: function (btn) {
                    myJqGrid.getJqGrid().jqGrid('showCol', 'DarkWeight');
                },
                //历史记录
                handleStuffInHis: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = myJqGrid.getSelectedRecord();
                    showStuffInHis(btn, selectedRecord.ID);
                },
                //调整价格
                handleAdjustPrice: function (btn) {
                    myJqGrid.showWindow({
                        btn: btn,
                        title: '入库单价调整',
                        loadFrom: 'AdjustPriceForm',
                        width: 480,
                        height: 260,
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "执行": function () {
                                
                                var sdate = $("[name='beginDate']").val(); //开始日期
                                var edate = $("[name='endDate']").val();   //结束日期
                                if (sdate == "") {
                                    showError('错误', '开始日期不能为空！');
                                    return;
                                }
                                if (edate == "") {
                                    showError('错误', '结束日期不能为空！');
                                    return;
                                }
                                if (compareTime(sdate + " 00:00:00", edate + " 23:59:59")) {
                                    showError('错误', '开始日期不能大于结束日期！');
                                    return;
                                }

                                showConfirm("询问？", "<span style='color:red'>谨慎操作：请确认【原材料采购合同】处材料单价已经设置好，是否执行？</span>", function (btn) {
                               //----start-----
                               
                                document.getElementById("showProgress").style.display = ""; //显示执行提示
                                $(".ui-dialog-buttonset").button({ disabled: true }); //下面按钮禁用
                                $(".ui-dialog-titlebar-close ui-corner-all").hide();

                                //执行ajax请求
                                var postData = $("#AdjustPriceForm form").serialize();
                                //附加额外的参数
                                ajaxRequest(opts.StuffPriceAdjust, postData, true,
                                    function (response) {
                                        $(btn.currentTarget).button({ disabled: false });
                                        var closeDialog = true;
                                        if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                            closeDialog = false;
                                        }
                                        //窗口关闭处理                        
                                        if (response.Result && closeDialog) {
                                            $("#AdjustPriceForm").dialog('close');
                                            $("#AdjustPriceForm form")[0].reset();
                                        }
                                        $(".ui-dialog-buttonset").button({ disabled: false }); //下面按钮启用
                                        document.getElementById("showProgress").style.display = "none"; //隐藏执行提示
                                        myJqGrid.refreshGrid();
                               });
                           });//----end-----

                            }
                        }
                    });
                }
            }
        });

        
       //进出库修改历史
       var stuffinHisGrid = new MyGrid({
           renderTo: 'stuffinHisGrid'
            , width: 720
            , height: 240
            , storeURL:"/StuffInHistory.mvc/Find"
            , sortByField: 'ID'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: true
            , emptyrecords: '当前无任何修改'
            , initArray: [
                { label: '流水号', name: 'ID', index: 'ID', width: 80, searchoptions: { sopt: ['cn'] }, formatter: stuffinIdFormatter, unformat: stuffinIdUnFormatter }
                , { label: '进厂时间', name: 'InDate', index: 'InDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: ' 入库原料', name: 'StuffName', index: 'StuffInfo.StuffName', width: 100 }
                , { label: ' 筒仓编号', name: 'SiloID', index: 'SiloID', hidden: true }
                , { label: ' 入库筒仓', name: 'SiloName', index: 'Silo.SiloName', width: 100 }
                , { label: '原料厂商', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: ' 供货厂商', name: 'SupplyName', index: 'SupplyInfo.SupplyName' }
                , { label: '采购单号', name: 'StockPactID', index: 'StockPactID', width: 60 }
                , { label: '采购单号', name: 'StockPactNo', index: 'StockPactNo', sortable: false, width: 60, hidden: true }
                , { label: '单价(吨/元)', name: 'UnitPrice', index: 'UnitPrice', width: 70, hidden: true }

                , { label: ' 运输车号', name: 'CarNo', index: 'CarNo', width: 80 }
                , { label: '入库数量(吨)', name: 'InNum', index: 'InNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '换算系数', name: 'Proportion', index: 'Proportion', width: 60 }
                , { label: '结算数量(吨)', name: 'FootNum', index: 'FootNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '厂商数量(吨)', name: 'SupplyNum', index: 'SupplyNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '磅差(吨)', name: 'Bangcha', index: 'Bangcha', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '最终结算数量(吨)', name: 'FinalFootNum', index: 'FinalFootNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '总价', name: 'TotalPrice', index: 'TotalPrice', width: 80 }
                , { label: ' 存储状态', name: 'Lifecycle', index: 'Lifecycle', formatter: LiftclyFmt, unformat: LiftclyUnFmt, width: 60 }

                , { label: '总重(吨)', name: 'TotalNum', index: 'TotalNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '空车重(吨)', name: 'CarWeight', index: 'CarWeight', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '运送数量(吨)', name: 'TransportNum', index: 'TransportNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '进货数量(吨)', name: 'StockNum', index: 'StockNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '含水率%(明扣)', name: 'WRate', index: 'WRate', align: 'right', width: 100 }
                , { label: '暗扣（KG）', name: 'DarkWeight', index: 'DarkWeight', align: 'right', width: 100, hidden: true }
                , { label: '运输公司', name: 'TransportID', index: 'TransportID', hidden: true }
                , { label: ' 运输公司', name: 'TransportName', index: 'TransportInfo.SupplyName' }
                , { label: '司机', name: 'Driver', index: 'Driver', width: 80 }
                , { name: 'pic1', index: 'pic1', hidden: true }
                , { name: 'pic2', index: 'pic2', hidden: true }
                , { name: 'pic3', index: 'pic3', hidden: true }
                , { name: 'pic4', index: 'pic4', hidden: true }
                , { name: 'IsBack', index: 'IsBack', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '司磅员', name: 'Builder', index: 'Builder', width: 130 }
                , { label: '过磅时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '最后修改人', name: 'Modifier', index: 'Modifier', width: 130 }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '客户', name: 'CustName', index: 'CustName' }
                , { label: '原料来源地', name: 'SourceAddr', index: 'SourceAddr' }
                , { label: '出厂时间', name: 'OutDate', index: 'OutDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: 'A/H', name: 'AH', index: 'AH', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AH'} }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: ' 经办人', name: 'Operator', index: 'Operator' }
                , { label: ' 结算状态', name: 'FootStatus', index: 'FootStatus', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '磅差2(吨)', name: 'Bangcha2', index: 'Bangcha2', width: 80, align: 'right' }
            ]
            , autoLoad: false
       });

       //查看进出库对应的历史记录
       function showStuffInHis(b, id) {
           var title = "进出库单据：&nbsp;<font color='#ff0000'>" + id + "</font>&nbsp;的历史记录";
           var refreshCon = "StuffInID='" + id + "'";
           $("#stuffinHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
               close: function (event, ui) {
                   $(this).dialog("destroy");
                   stuffinHisGrid.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
               }
           });
           $('#stuffinHisWindow').dialog('open');
           stuffinHisGrid.refreshGrid(refreshCon);
       }

    //选择材料关联相对应筒仓
    $('#StuffID').bind('change', function () {
        //入库时，骨料仓不需要按照分配的筒仓过滤。因此先要查询所选的材料是不是骨料 2013-03-28 徐毅力
        $('#SiloID').disableSelection(); //ajax没有返回正确的数据前不能选择筒仓，以免选错 2013-03-28 徐毅力
        var _stuffid = $(this).val();
        ajaxRequest(
            opts.IsGuLiaoUrl,
            { id: $(this).val() },
            false,
            function (response) {
                $('#SiloID').enableSelection();//可选择
                if (response.Result) {//是骨料
                    bindSelectData($('#SiloID'), opts.findSiloUrl, { condition: "StuffID IN(SELECT StuffID FROM stuffinfo WHERE StuffTypeID IN (SELECT StuffTypeID FROM dbo.StuffType WHERE FinalStuffType IN(SELECT FinalStuffType FROM dbo.StuffType WHERE StuffTypeID IN(SELECT StuffTypeID FROM dbo.StuffInfo WHERE StuffID = '" + _stuffid + "'))))" });
                } else {
                    bindSelectData($('#SiloID'), opts.findSiloUrl, { condition: "StuffID = '" + _stuffid + "'" });
                }
            }
        );
        ajaxRequest(
               opts.GetStuffUrl,
               { id: $(this).val() },
               false,
               function (response) {
                   if (response.Data) {
                       var SopRate = response.Data.SopRate == null ? "1" : response.Data.SopRate;
                       $('#Proportion').val(SopRate);
                   } else {
                       $('#Proportion').val("1");
                   }
               });
    });


    function CalcInNum() {
        var TotalNum = myJqGrid.getFormField("TotalNum").val();
        var CarWeight = myJqGrid.getFormField("CarWeight").val();
        var WRate = myJqGrid.getFormField("WRate").val();
        var Innum = TotalNum - CarWeight - Math.round((TotalNum - CarWeight) * WRate / 100);
        Innum = GetTenNum(Innum);
        var Innum_T = Innum / 1000;
        $('#InNum').val(Innum);
        myJqGrid.getFormField("InNum_T").val(Innum_T);

        var StockNum = TotalNum - CarWeight;
        StockNum = GetTenNum(StockNum);
        var StockNum_T = StockNum / 1000;
        myJqGrid.getFormField("StockNum_T").val(StockNum_T);
        myJqGrid.getFormField("StockNum").val(StockNum);

        var Proportion = myJqGrid.getFormField("Proportion").val();
        var Bangcha = myJqGrid.getFormField("Bangcha").val();
        if (!(Proportion == 0 || isNaN(Proportion))) {
            var FootNum = Innum / Proportion;
            var FinalFootNum = parseInt(FootNum) + parseInt(Bangcha);
            FootNum = GetTenNum(FootNum);
            FinalFootNum = GetTenNum(FinalFootNum);
            var FootNum_T = FootNum / 1000;
            var FinalFootNum_T = FinalFootNum / 1000;
            myJqGrid.getFormField("FootNum_T").val(FootNum_T);
            myJqGrid.getFormField("FootNum").val(FootNum);
            myJqGrid.getFormField("FinalFootNum_T").val(FinalFootNum_T);
            myJqGrid.getFormField("FinalFootNum").val(FinalFootNum);
        }

    }

    function GetTenNum(Num) {

        return parseInt((Num + 5) / 10) * 10;
    }

    $('#dlgMetagePicDialog').dialog({ autoOpen: false, modal: true, bgiframe: true, resizable: false, width: 405, height: 335, title: "过磅照片" });
    $('#dlgMetagePicView').dialog({ autoOpen: false, modal: true, bgiframe: true, resizable: false, width: 380, height: 300 });
    $('a[data-stuffin-id]').live('click', function (e) {

        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;

        var dialogPosition = $(this).offset();

        var data = myJqGrid.getRecordByKeyValue($(this).attr('data-stuffin-id'));
        data["pic1"] = getMetagePicPath(data["pic1"]);
        data["pic2"] = getMetagePicPath(data["pic2"]);
        data["pic3"] = getMetagePicPath(data["pic3"]);
        data["pic4"] = getMetagePicPath(data["pic4"]);

        $('#dlgMetagePicDialog').empty();
        $('#tmplMetagePic').tmpl(data).appendTo('#dlgMetagePicDialog');

        $("#picView").find('img').bind("error", function () { $(this).remove(); });
        $("#picView").find('img').attr("style", "cursor:pointer");
        $("#picView").find('img').unbind("click");
        $("#picView").find('img').bind("click", function () {
            $('#dlgMetagePicView').empty();
            $('#dlgMetagePicView').append("<img src = '" + this.src.replace('/Thumbnail/', '/Photo/') + "' />");
            $('#ui-dialog-title-dlgMetagePicView').html(data["CarNo"] + '--' + data["StuffName"] + "【" + data["SupplyName"] + "】");
            $('#dlgMetagePicView').dialog('option', 'position', [dialogPosition.center, dialogPosition.center]);
            $('#dlgMetagePicView').dialog('open');
        });

        $('#ui-dialog-title-dlgMetagePicDialog').html(data["CarNo"] + '--' + data["StuffName"] + "【" + data["SupplyName"] + "】");
        $('#dlgMetagePicDialog').dialog('option', 'position', [dialogPosition.center, dialogPosition.center]);
        $('#dlgMetagePicDialog').dialog('open');
    });

    function stuffinIdFormatter(cellvalue, options, rowObject) {
        if (rowObject.AH == "Handle") {
            return cellvalue;
        }
        else {
            return "<a href='#' data-stuffin-id='" + cellvalue + "'>" + cellvalue + "</a>";
        }
    };
    function stuffinIdUnFormatter(cellvalue, options, cell) {
        return $('a', cell).attr('data-stuffin-id');
    }

    function getMetagePicPath(recordfilename) {

        if (isEmpty(recordfilename))
            return '';
        var fileArr = recordfilename.split('_');

        if (fileArr.length > 0) {
            var DirectoryName = fileArr[1].substr(0, 6);
            return "/Content/Files/MetagePic/Thumbnail/" + DirectoryName + "/" + recordfilename;
        }
        else {
            return "";
        }
    }

    //重新绑定下拉列表数据
    function bindSelectData0(target, url, data, callback) {
        $.post(url,
        data,
        function (data) {
            target.empty();
            target.append("<option value=''></option>");
            if (!$.isEmptyObject(data)) {
                target = $(target);
                for (var i = 0; i < data.length; i++) {
                    var d = data[i]; 
                    target.append("<option value='" + d.Value + "'>" + d.Text + "</option>");
                }
            }
            if (callback != null) {
                executeFunction(callback, data);
            }
        });
    }



    StuffInfoChange = function () {
        var StockPactField = $($("[name='StockPactID_S']")[0]);
        StockPactField.unbind('change');
        StockPactField.bind('change', function (event) {
            var id = StockPactField.val();
            var stuffinfo = $($("[name='StuffID_S']")[0]);
            bindSelectData0(stuffinfo,
                                   opts.getprice,
                                   {
                                       id: id,
                                       textField: 'StuffName',
                                       valueField: 'ID'
                                   });


        });
    }

        StuffInfoChange0 = function () {
            var StockPactField = $($("[name='StockPactID']")[0]);
            StockPactField.unbind('change');
            StockPactField.bind('change', function (event) {
                var id = StockPactField.val();
                var stuffinfo = $($("[name='StuffID']")[0]);
                bindSelectData0(stuffinfo,
                opts.getprice,
                {
                    id: id,
                    textField: 'StuffName',
                    valueField: 'ID'
                });


            });
        }
        //根据合同材料取得对应的单价
        StuffInfoPriceChange = function () {
            var StuffInfoField = myJqGrid.getFormField("StuffID");
            StuffInfoField.unbind('blur');
            StuffInfoField.bind('blur', function (event) {
                var StockPactField = $($("[name='StockPactID']")[0]); //
                var stockpactid = StockPactField.val();

                var StuffInfoField = myJqGrid.getFormField("StuffID");
                var stuffInfoid = StuffInfoField.val();

                //
                if (!isEmpty(stockpactid) && !isEmpty(stuffInfoid)) {

                    var requestURL = opts.getStuffPrice;
                    var postParams = { StockPactId: stockpactid, StuffInfoId: stuffInfoid };
                    var rData;
                    ajaxRequest(requestURL, postParams, false, function (response) {
                        if (response) {
                            rData = response;
                            myJqGrid.getFormField("UnitPrice").val(rData == null ? "" : rData);
                        }
                        else {
                            myJqGrid.getFormField("UnitPrice").val("0");
                        }
                    });
                }
                else {
                    myJqGrid.getFormField("UnitPrice").val("0");
                }
            });
        }

}
 