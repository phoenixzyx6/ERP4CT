//原材料采购合同
function stockPactInit(opts) {
    var sid;
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight / 2
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 800
            , dialogHeight: 580
		    , initArray: [
                { label: '流水号', name: 'ID', index: 'ID', width: 80 }
                , { label: ' 合同号', name: 'StockPactNo', index: 'StockPactNo', width: 80 }
                , { label: ' 供货厂家编号', name: 'SupplyID', index: 'SupplyID', editable: true, hidden: true }
                , { label: ' 供货厂家', name: 'SupplyName', index: 'SupplyInfo.SupplyName', width: 110, sortable: false }
                , { label: ' 合同名称', name: 'PactName', index: 'PactName', width: 110 }
                , { label: ' 原料编号1', name: 'StuffID', index: 'StuffID', editable: true, hidden: true }
                , { label: ' 原料编号2', name: 'StuffID1', index: 'StuffID1', editable: true, hidden: true }
                , { label: ' 原料编号3', name: 'StuffID2', index: 'StuffID2', editable: true, hidden: true }
                , { label: ' 原料编号4', name: 'StuffID3', index: 'StuffID3', editable: true, hidden: true }
                , { label: ' 原料编号5', name: 'StuffID4', index: 'StuffID5', editable: true, hidden: true }
                , { label: ' 磅差一', name: 'lowbangcha', index: 'lowbangcha', editable: true, hidden: true }
                , { label: ' 磅差二', name: 'highbangcha', index: 'highbangcha', editable: true, hidden: true }

                , { label: ' 单位', name: 'GageUnit', index: 'GageUnit', editable: true, hidden: true }
                , { label: ' 原料名称1', name: 'StuffName', index: 'StuffInfo.StuffName', width: 110 }
                , { label: ' 单价1', name: 'StockPrice', index: 'StockPrice', width: 60 }
                , { label: ' 原料名称2', name: 'StuffName1', index: 'StuffInfo1.StuffName', width: 110 }
                , { label: ' 单价2', name: 'StockPrice1', index: 'StockPrice1', width: 60 }
                , { label: ' 原料名称3', name: 'StuffName2', index: 'StuffInfo2.StuffName', width: 110 }
                , { label: ' 单价3', name: 'StockPrice2', index: 'StockPrice2', width: 60 }
                , { label: ' 原料名称4', name: 'StuffName3', index: 'StuffInfo3.StuffName', width: 110 }
                , { label: ' 单价4', name: 'StockPrice3', index: 'StockPrice3', width: 60 }
                , { label: ' 原料名称5', name: 'StuffName4', index: 'StuffInfo4.StuffName', width: 110 }
                , { label: ' 单价5', name: 'StockPrice4', index: 'StockPrice4', width: 60 }
        //                , { label: '垫资类型', name: 'DianziType', index: 'DianziType', width: 80 }
        //                , { label: '垫资量(天|吨|元)', name: 'DianziNum', index: 'DianziNum', width: 100 }
        //                , { label: '完成垫资', name: 'IsDianzi', index: 'IsDianzi', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
        //                , { label: '垫资金额', name: 'DianziMoney', index: 'DianziMoney', width: 80 }

                , { label: '垫资1', name: 'Dianzi1', index: 'Dianzi1', width: 180, hidden: true }
                , { label: '垫资2', name: 'Dianzi2', index: 'Dianzi2', width: 180, hidden: true }
                , { label: '垫资3', name: 'Dianzi3', index: 'Dianzi3', width: 180, hidden: true }
                , { label: '垫资4', name: 'Dianzi4', index: 'Dianzi4', width: 180, hidden: true }
                , { label: '垫资4', name: 'Dianzi5', index: 'Dianzi5', width: 180, hidden: true }
                , { label: '垫资4', name: 'Dianzi6', index: 'Dianzi6', width: 180, hidden: true }
                , { label: '垫资4', name: 'Dianzi7', index: 'Dianzi7', width: 180, hidden: true }
                , { label: '垫资约定', name: 'DianziString', index: 'DianziString', width: 180 }
                , { label: '数量(吨)', name: 'Amount', index: 'Amount', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100, hidden: true }
                , { label: ' 采购单价(元/吨)', name: 'StockPrice', index: 'StockPrice', width: 120, hidden: true, formatter: T2KgFmt, unformat: Kg2TFmt }
                , { label: ' 认磅差比例', name: 'BangchaRate', index: 'BangchaRate', editable: true }
                , { label: ' 质量要求', name: 'QualityNeed', index: 'QualityNeed', editable: true }
                , { label: '录入时间', name: 'EstablishTime', index: 'EstablishTime', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '录入人', name: 'EstablishMan', index: 'EstablishMan', editable: true }
                , { label: '开始有效日期', name: 'ValidFrom', index: 'ValidFrom', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '结束有效日期', name: 'ValidTo', index: 'ValidTo', editable: true, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '税率', name: 'TaxRate', index: 'TaxRate', hidden: true, editable: true }
                , { label: '报警百分比', name: 'WarmPercent', index: 'WarmPercent', hidden: true, editable: true }
                , { label: '称重依据', name: 'WeighBy', index: 'WeighBy', hidden: true, editable: true }
                , { label: '来源地', name: 'SourceAddr', index: 'SourceAddr', hidden: true, editable: true }
                , { label: '结算方式', name: 'FootMode', index: 'FootMode', hidden: true, editable: true }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', editable: true }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: "合同样本", name: "Attachments", formatter: attachmentFmt2, sortable: false, search: false }
                , { label: ' 备注', name: 'Remark', index: 'Remark', editable: true }
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
                        beforeSubmit: function () {
                            var supplyn=$("input[name='SupplyName']").val();
                            if (isEmpty(supplyn)) {
                                //设置为必填警示颜色
                                $("input[name='SupplyName']").addClass('input-validation-error');
                                return false;
                            }
                            $("input[name='SupplyName']").removeClass("input-validation-error");

                            var stuffn = $("#MyFormDiv :text[name='StuffName']").val();
                            if (isEmpty(stuffn)) {
                                //设置为必填警示颜色
                                $("#MyFormDiv :text[name='StuffName']").addClass('input-validation-error');
                                return false;
                            }
                            $("#MyFormDiv :text[name='StuffName']").removeClass("input-validation-error");

                            return true;
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(response.Data); //上传附件
                            }
                        },
                        afterFormLoaded: function () {
                            //设置为必填颜色
                            $("input[name='SupplyName']").addClass('text requiredval valid');
                            $("input[name='StuffName']").addClass('text requiredval valid');

                            myJqGrid.getFormField("EstablishMan").val(opts.currentUser); //录入人
                            myJqGrid.setFormFieldReadOnly('EstablishMan', true); //设置录入人栏只读
                            myJqGrid.getFormField("Amount_T").bind('blur', function () {
                                var Amount_T = myJqGrid.getFormField("Amount_T").val();
                                myJqGrid.getFormField("Amount").val(Amount_T * 1000);
                            });
                            myJqGrid.getFormField("StockPrice_T").bind('blur', function () {
                                var StockPrice_T = myJqGrid.getFormField("StockPrice_T").val();
                                myJqGrid.getFormField("StockPrice").val(StockPrice_T / 1000);
                            });
                            selectChange();

                            $("#Attachments").empty();
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.AuditStatus == '1') {
                        showMessage('该合同已审核，不允许再修改');
                        return;
                    };
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        
                        beforeSubmit: function () {
                            var supplyn = $("input[name='SupplyName']").val();
                            if (isEmpty(supplyn)) {
                                //设置为必填警示颜色
                                $("input[name='SupplyName']").addClass('input-validation-error');
                                return false;
                            }
                            $("input[name='SupplyName']").removeClass("input-validation-error");

                            var stuffn = $("#MyFormDiv :text[name='StuffName']").val();
                            if (isEmpty(stuffn)) {
                                //设置为必填警示颜色
                                $("#MyFormDiv :text[name='StuffName']").addClass('input-validation-error');
                                return false;
                            }
                            $("#MyFormDiv :text[name='StuffName']").removeClass("input-validation-error");
                            return true;
                        },
                        afterFormLoaded: function () {
                            //设置为必填颜色
                            $("input[name='SupplyName']").addClass('text requiredval valid');
                            $("input[name='StuffName']").addClass('text requiredval valid');

                            //附件
                            var attDiv = $("#Attachments");
                            attDiv.empty();
                            attDiv.append(data["Attachments"]);
                            $("a[att-id]").show();

                            myJqGrid.getFormField("Amount_T").val(data.Amount / 1000);
                            myJqGrid.getFormField("StockPrice_T").val(data.StockPrice * 1000);

                            myJqGrid.getFormField("Amount_T").bind('blur', function () {
                                var Amount_T = myJqGrid.getFormField("Amount_T").val();
                                myJqGrid.getFormField("Amount").val(Amount_T * 1000);
                            });
                            myJqGrid.getFormField("StockPrice_T").bind('blur', function () {
                                var StockPrice_T = myJqGrid.getFormField("StockPrice_T").val();
                                myJqGrid.getFormField("StockPrice").val(StockPrice_T / 1000);
                            });
                            selectChange();
                            getlabel();
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(data.ID);
                            }
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleAuditing: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.AuditStatus == '1') {
                        showMessage('该合同已审核，不允许再审核');
                        return;
                    };
                    myJqGrid.handleEdit({
                        loadFrom: 'AuditingFormDiv',
                        btn: btn,
                        width: 300,
                        height: 250,
                        afterFormLoaded: function () {
                            myJqGrid.getFormField("Auditor").val(opts.currentUser);
                        }
                    });
                }
                , handlePay: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'PayFormDiv',
                        btn: btn,
                        width: 480,
                        height: 250,
                        afterFormLoaded: function () {

                        }
                        , postCallBack: function (response) {//提交后回调函数
                            SPCGrid.refreshGrid("StockPactID='" + pid + "'");
                        }
                    });
                }

                , handleFapiao: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'SPCCFormDiv',
                        btn: btn,
                        width: 480,
                        height: 250
                        , postCallBack: function (response) {//提交后回调函数
                            SPCCGrid.refreshGrid("StockPactID='" + pid + "'");
                        }
                    });
                }
                , handleDianzi: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'DianziDiv',
                        btn: btn,
                        width: 480,
                        height: 250
                    });
                }
                //合同调价
                ,handleAdjustPrice: function (btn) {
                     myJqGrid.handleEdit({
                         loadFrom: 'AdjustPriceForm',
                         btn: btn,
                         width: 480,
                         height: 280,
                         afterFormLoaded: function () {

                         }
                        , postCallBack: function (response) {//提交后回调函数
                            myJqGrid.reloadGrid();
                        }
                     });
                }
                
            }
    });
    //上传附件
    function attachmentUpload(objectId) {
        var fileElement = $("input[type=file]");
        if (fileElement.val() == "") return;

        $.ajaxFileUpload
            ({
                url: opts.uploadUrl + "?objectType=StockPact&objectId=" + objectId,
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
                        myJqGrid.reloadGrid();
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

    //加载完成
    myJqGrid.addListeners("gridComplete", function () {
        var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        var currentDate = new Date(); //当前日期
        var m = currentDate.getMonth() + 2; //获取当前月份(0-11,0代表1月) 
        var syDate = currentDate.getFullYear() + "-" + m + "-" + currentDate.getDate(); //有效结束时间后一月日期
        for (var i = 0; i < ids.length; i++) {
            var record = myJqGrid.getRecordByKeyValue(ids[i]);
            var ValidTo = record.ValidTo; //有效结束时间

            if (ValidTo != null || ValidTo != "") {

                if (compareTime(syDate + " 00:00:00", ValidTo + " 00:00:00")) {
                    var $id = $(document.getElementById(ids[i]));
                    $id.removeAttr("style");
                    $id.removeClass("ui-widget-content");
                    document.getElementById(ids[i]).style.backgroundColor = "#FFAE4A";
                }
            }
        }
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    var pid;
    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        SPCGrid.refreshGrid("StockPactID='" + id + "'");
        SPCCGrid.refreshGrid("StockPactID='" + id + "'");
        pid = id;
    });


    var SPCGrid = new MyGrid({
        renderTo: 'SPCGrid'
            , title: '付款记录'
            , autoWidth: true
            , height: gGridHeight / 2 - 60
		    , storeURL: opts.SPCStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 700
            , dialogHeight: 420
            , storeCondition: '1<>1'
		    , showPageBar: true
        //,buttons: buttons1
            , editSaveUrl: opts.SPCUpdateUrl
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'act1'
		    , initArray: [
                { label: '操作', name: 'act1', index: 'act1', width: 45, sortable: false, align: "center", summaryType: 'sum', summaryTpl: '合计：' }
                , { label: '付款编号', name: 'ID', index: 'ID', width: 70, editable: false }
                , { label: '付款时间', name: 'PayTime', index: 'PayTime', formatter: 'date', editable: true, width: 80, editrules: { date: true} }
                , { label: '合同ID', name: 'StockPactID', index: 'StockPactID', width: 70 }
                , { label: '付款金额', name: 'PayMoney', index: 'PayMoney', width: 70, editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                , { label: '材料ID', name: 'StuffID', index: 'StuffID', width: 100, editable: true, edittype: 'select', editoptions: { value: getStuffinfoStr()} }
                //, { label: '材料名称', name: 'StuffName', index: 'StuffName'}
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 130 }
                //, { label: '材料名称2', name: 'StuffName2', index: 'StuffName', width: 130, editable: true }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    SPCGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    SPCGrid.refreshGrid('1=1');
                }
                , handlePayUpdate: function (btn) {
                    if (SPCGrid.isSelectedOnlyOne('请选择一条付款记录！')) {
                        var objectArray = new Array();
                        SPCGrid.handleEdit({
                            loadFrom: 'PayFormDiv',
                            btn: btn,
                            width: 480,
                            height: 250
                            , beforeSubmit: function () {

                                objectArray[0] = pid;
                                objectArray[1] = $('#PayFormDiv #StockPactChild_PayTime').val();
                                objectArray[2] = $('#PayFormDiv #StockPactChild_PayMoney').val();
                                objectArray[3] = $('#PayFormDiv input[name="StuffID"]').val();
                                objectArray[4] = sid;

                                return true;
                            }
                            , postData: { sp: objectArray }
                            , postCallBack: function () {
                                myJqGrid.reloadGrid();
                                SPCGrid.reloadGrid();
                            }
                        });
                    }
                }
            }

    });

        //获取材料列表 lzl add 2016-07-28
        function getStuffinfoStr() {
            //动态生成select内容
            var requestURL = "/StuffInfo.mvc/FindStuffinfo";
            var postParams = { };
            var str = "";

            $.ajax({
                type: 'post',
                async: false,//必须是false 同步方式
                url: requestURL,
                success: function (data) {
                    if (data != null) {
                        var jsonobj = eval(data);
                        var length = jsonobj.length;
                        for (var i = 0; i < length; i++) {
                            if (i != length - 1) {
                                str += jsonobj[i].ID + ":" + jsonobj[i].StuffName +"|("+jsonobj[i].ID + ");";
                            } else {
                                str += jsonobj[i].ID + ":" + jsonobj[i].StuffName+"|("+jsonobj[i].ID + ")";
                            }
                        }

                    }

                }
            });
            return str; 
            //return str = "CL0003:地维水泥;CL0004:水;CL0005:粉煤灰;CL0006:机制砂";           
        }

        //grid行操作栏按钮
        SPCGrid.addListeners("gridComplete", function () {
            var ids = SPCGrid.getJqGrid().jqGrid('getDataIDs'); //获取表格ID组
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteSPC(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                SPCGrid.getJqGrid().jqGrid('setRowData', ids[i], { act1: ce });

            }

        });

        SPCGrid.addListeners('bindKeys', { "onEnter": function (rowid) { alert("你enter了一行， id为:" + rowid) } });

        //cellclick 事件定义
        SPCGrid.addListeners("cellclick", function (id, record) {

            //选择日期列单元格弹出日期选择控件
            var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
            $("#" + rowno + "_PayTime").datepicker({
                showButtonPanel: true,
                changeMonth: false,
                changeYear: true
            });

            //
            //s$("#" + rowno + "_ID").dialog();

            var str = new Array();
            $("#" + rowno + "_StuffName2").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/StuffInfo.mvc/FindStuffinfo",
                        async: false,
                        dataType: "json",
                        data: { stuffname: request.term },
                        success: function (data) {
                            if (data != null) {
                                var jsonobj = eval(data);
                                var length = jsonobj.length;
                                for (var i = 0; i < length; i++) {
                                    if (i != length - 1) {
                                        str[i]=jsonobj[i].StuffName;
                                    } else {
                                        str[i]=jsonobj[i].StuffName;
                                        
                                    }
                                }

                            }

                            response(data.length === 1 && data[0].length === 0 ? [] : str);
                        }
                    });
                },
                minLength: 0,
                select: function (event, ui) {
                    //log("Selected: " + ui.item.label);
                }
            });

        });
        

        //删除付款记录
        window.handleDeleteSPC = function (id) {
            //jQuery("#SPCGrid").datepicker({ dateFormat: "yy-mm-dd" });
            showConfirm("询问", "您确定删除此项记录吗？", function () {
                $.post(
                    opts.SPCDeleteUrl
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        SPCGrid.reloadGrid();
                    }

                );
                $(this).dialog("close");
            });
        };

        var SPCCGrid = new MyGrid({
            renderTo: 'SPCCGrid'
            , title: '发票与资源单'
            //, buttons: buttons2
            , autoWidth: true
            , height: gGridHeight / 2 - 60
		    , storeURL: opts.SPCCStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 700
            , dialogHeight: 420
            , storeCondition: '1<>1'
		    , showPageBar: true
            , editSaveUrl: opts.SPCCUpdateUrl
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'act'
		    , initArray: [
                { label: '操作', name: 'act', index: 'act', width: 50, sortable: false, align: "center", summaryType: 'sum', summaryTpl: '合计：' }
                , {label: '发票与资源单编号', name: 'ID', index: 'ID', hidden: true, width: 80 }
                , { label: '合同ID', name: 'StockPactID', index: 'StockPactID', hidden: true }
                , { label: '收到时间', name: 'PayTime', index: 'PayTime', formatter: 'date', editable: true, editrules: { date: true }, width: 70 }
                , { label: '发票金额', name: 'FapiaoMoney', index: 'FapiaoMoney', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, width: 60, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                , { label: '发票号码', name: 'FapiaoNumber', index: 'FapiaoNumber', editable: true, width: 70 }
                , { label: '发票对应材料量[吨]', name: 'FapiaoCailiao', index: 'FapiaoCailiao', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: "," }, width: 110 }
                , { label: '资源税证明单数量', name: 'Zyszmd', index: 'Zyszmd', editable: true, width: 100 }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', editable: true, width: 100 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    SPCCGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    SPCCGrid.refreshGrid('1=1');
                }

                , handleUpdateFapiao: function (btn) {
                    if (SPCCGrid.isSelectedOnlyOne('请选择一条记录！')) {
                        var objectArray = new Array();
                        SPCCGrid.handleEdit({
                            loadFrom: 'SPCCFormDiv',
                            btn: btn,
                            width: 480,
                            height: 250
                            , afterFormLoaded: function () {

                                var record = SPCCGrid.getSelectedRecord();
                                $('#SPCCFormDiv #StockPactChildChild_PayTime').val(record.PayTime);
                                $('#SPCCFormDiv #StockPactChildChild_Zyszmd').val(record.Zyszmd);
                                $('#SPCCFormDiv #StockPactChildChild_FapiaoMoney').val(record.FapiaoMoney);
                                $('#SPCCFormDiv #StockPactChildChild_FapiaoNumber').val(record.FapiaoNumber);
                                $('#SPCCFormDiv #StockPactChildChild_FapiaoCailiao').val(record.FapiaoCailiao);
                                $('#SPCCFormDiv input[name="StuffID"]').val(record.StuffID);
                            }
                            , beforeSubmit: function () {

                                objectArray[0] = pid;
                                objectArray[1] = _sid;
                                objectArray[2] = $('#SPCCFormDiv #StockPactChildChild_PayTime').val();
                                objectArray[3] = $('#SPCCFormDiv #StockPactChildChild_Zyszmd').val();
                                objectArray[4] = $('#SPCCFormDiv #StockPactChildChild_FapiaoMoney').val();
                                objectArray[5] = $('#SPCCFormDiv #StockPactChildChild_FapiaoNumber').val();
                                objectArray[6] = $('#SPCCFormDiv #StockPactChildChild_FapiaoCailiao').val();
                                objectArray[7] = $('#SPCCFormDiv input[name="StuffID"]').val();
                                return true;
                            }
                            , postData: { sp: objectArray }
                            , postCallBack: function () {
                                myJqGrid.reloadGrid();
                                SPCGrid.reloadGrid();
                                SPCCGrid.reloadGrid();
                            }
                        });
                    }
                }

            }

        });
        //grid行操作栏按钮
        SPCCGrid.addListeners("gridComplete", function () {
            var ids = SPCCGrid.getJqGrid().jqGrid('getDataIDs'); //获取表格ID组
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteSPCC(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                SPCCGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

            }
        });
        //cellclick 事件定义，选择日期列单元格弹出日期选择控件
        SPCCGrid.addListeners("cellclick", function (id, record) {
            var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
            $("#" + rowno + "_PayTime").datepicker();
        });

        //删除发票和资源单记录单记录
        window.handleDeleteSPCC = function (id) {
            showConfirm("询问", "您确定删除此项记录吗？", function () {
                $.post(
                    opts.SPCCDeleteUrl
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        SPCCGrid.reloadGrid();
                    }

                );
                $(this).dialog("close");
            });
        };

        selectChange = function () {
            $("#dianzitd").hide(); 
            var areaField = myJqGrid.getFormField("DianziType");
            areaField.unbind('change');
            areaField.bind('change', function (event) {
                $("#DianziNum").val("");
                getlabel();
            });
        };
        getlabel = function () {
            var areaField = myJqGrid.getFormField("DianziType");
            var area = areaField.val();
            switch (area) {
                case "垫时长":
                    $("#dianzitd").show();                    
                    $("#dianzilabel").html("垫资天数（天)");
                    break;
                case "垫数量":
                    $("#dianzitd").show(); 
                    $("#dianzilabel").html("垫资数量（吨）");
                    break;
                case "垫金额":
                    $("#dianzitd").show(); 
                    $("#dianzilabel").html("垫资金额（元）");
                    break;
                default:
                    $("#dianzitd").hide();
                    break;
            }
        }

        var _sid;

        SPCGrid.addListeners('rowclick', function (id, record, selBool) {
            sid = id;
        });

        SPCCGrid.addListeners('rowclick', function (id, record, selBool) {
            _sid = id;
        });

        //跳转到价格变动界面
        var stockpackid, stuffid;
        $("#tj1").click(function () {
            stockpackid = $("#AdjustPriceForm :text[name='ID']").val();
            stuffid = $("#AdjustPriceForm :hidden[name='StuffID']").val();
            if (stuffid =="") {
                return;
            }
            handlePriceSet(stockpackid, stuffid);
        });
        $("#tj2").click(function () {
            stockpackid = $("#AdjustPriceForm :text[name='ID']").val();
            stuffid = $("#AdjustPriceForm :hidden[name='StuffID1']").val();
            if (stuffid == "") {
                return;
            }
            handlePriceSet(stockpackid, stuffid);
        });
        $("#tj3").click(function () {
            stockpackid = $("#AdjustPriceForm :text[name='ID']").val();
            stuffid = $("#AdjustPriceForm :hidden[name='StuffID2']").val();
            if (stuffid == "") {
                return;
            }
            handlePriceSet(stockpackid, stuffid);
        });
        $("#tj4").click(function () {
            stockpackid = $("#AdjustPriceForm :text[name='ID']").val();
            stuffid = $("#AdjustPriceForm :hidden[name='StuffID3']").val();
            if (stuffid == "") {
                return;
            }
            handlePriceSet(stockpackid, stuffid);
        });
        $("#tj5").click(function () {
            stockpackid = $("#AdjustPriceForm :text[name='ID']").val();
            stuffid = $("#AdjustPriceForm :hidden[name='StuffID4']").val();
            if (stuffid == "") {
                return;
            }
            handlePriceSet(stockpackid, stuffid);
        });

        var priceSettingGrid;
        //弹出【价格变动】窗体
        window.handlePriceSet = function (stockpackid, stuffid) {
            myJqGrid.showWindow({
                title: '价格变动',
                width: 510,
                height: 440,
                loadFrom: '/StockPact.mvc/PriceSet #PriceSetForm div'
                        , afterLoaded: function () {

                            //BuildTime
                            $("input[name='StockPactID']").val(stockpackid);
                            //$("input[name='StuffID']").val(stuffid);

                            $("form[name='SPriceSetForm'] input[name='StuffID']").val(stuffid);
                            $("input[name='BuildTime']").val("2010-01-01");

                            //$("#StockPactID").html("<font color=red>" + stockpackid + "</font>");
                            //$("#StuffID").html("<font color=red>" + stuffid + "</font>");

                            //价格设定
                            priceSettingGrid = new MyGrid({
                                renderTo: 'priceSettingGrid'
                                , autoWidth: true
                                //, buttons: buttons1
                                , storeURL: opts.priceStoreUrl
                                , sortByField: 'ID'
                                , primaryKey: 'ID'
                                , dialogWidth: 800
                                , dialogHeight: 700
                                , setGridPageSize: 30
                                , showPageBar: true
                                , altclass: 'identityButton'
                                , altRows: true
                                , autoLoad: false
                                , editSaveUrl: opts.priceUpdateUrl
                                , initArray: [
                                    { label: '编号', name: 'ID', index: 'ID', hidden: true }
                                    , { label: '合同编号', name: 'StockPactID', index: 'StockPactID', hidden: true }
                                    , { label: '材料ID', name: 'StuffID', index: 'StuffID', hidden: true }
                                    , { label: '订价时间', name: 'ChangeTime', index: 'ChangeTime', formatter: "date", editable: true, width: 90 }
                                    , { label: '单价', name: 'Price', index: 'Price', editable: true, width: 80 }
                                    , { label: '操作', name: 'myac', width: 50, fixed: true, sortable: false, resize: false, formatter: 'actions',
                                        formatoptions: { keys: true, editbutton: false }
                                    }
                                    , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
                                    , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 80 }
                                ]
                                , functions: {
                                    handleReload: function (btn) {
                                        priceSettingGrid.reloadGrid();
                                    },
                                    handleRefresh: function (btn) {
                                        priceSettingGrid.refreshGrid();
                                    }
                                }
                            });
                            priceSettingGrid.getJqGrid().jqGrid('setGridParam', { editurl: opts.priceRowDeleteUrl });
                            priceSettingGrid.refreshGrid("StockPactID='" + stockpackid + "' and StuffID='" + stuffid + "'");
                        }
                        , buttons: {
                            "关闭": function () {
                                $(this).dialog("close");
                            }
                        }
            });
        };

        
        //保存价格变动函数
        window.priceSave = function () {
            var _StockPactID = $("[name='StockPactID']").val();
            var _StuffId = $("form[name='SPriceSetForm'] input[name='StuffID']").val();
            $.post(
                opts.priceAddUrl, $("form[name='SPriceSetForm']").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    $("form[name='SPriceSetForm'] input[name='Price']").val("");
                    $("form[name='SPriceSetForm'] input[name='ChangeTime']").val("");
                    priceSettingGrid.refreshGrid("StockPactID='" + _StockPactID + "' and StuffID='" + _StuffId + "'");
                }
            );
        };
}
