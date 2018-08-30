//泵送作业
function pumpworkIndexInit(options) {
    var PumpWorkGrid = new MyGrid({
        renderTo: 'PumpWorkGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 600
            , dialogHeight: 455
		    , storeURL: options.pumpWorkStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '泵送单号', name: 'ID', index: 'ID' }
                , { label: '合同代号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: '合同名称', name: 'ContractName', index: 'Contract.ContractName' }
                , { label: '客户代号', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'Customer.CustName' }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID' }
                , { label: '泵送日期', name: 'PumpDate', index: 'PumpDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '业务员', name: 'Saler', index: 'Saler' }
                , { label: '泵车类型', name: 'PumpType', index: 'PumpType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'PumpType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('PumpType')} }
                , { label: '泵车号', name: 'CarID', index: 'CarID' }
                , { label: '泵车司机', name: 'Driver', index: 'Driver' }
                , { label: '客户数量', name: 'CustNum', index: 'CustNum' }
                , { label: '实泵数量', name: 'PumpNum', index: 'PumpNum' }
                , { label: '泵费', name: 'PumpPrice', index: 'PumpPrice' }
                , { label: '金额', name: 'PumpSum', index: 'PumpSum' }
                , { label: '起始时间', name: 'BeginDate', index: 'BeginDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '截止时间', name: 'EndDate', index: 'EndDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '砂浆客户数量', name: 'SlurryCustNum', index: 'SlurryCustNum' }
                , { label: '砂浆实泵数量', name: 'SlurryPumpNum', index: 'SlurryPumpNum' }
                , { label: '砂浆泵费', name: 'SlurryPumpPrice', index: 'SlurryPumpPrice' }
                , { label: '砂浆金额', name: 'SlurryPumpSum', index: 'SlurryPumpSum' }
                , { label: '应收账款', name: 'Credit', index: 'Credit' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    PumpWorkGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    PumpWorkGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    PumpWorkGrid.handleAdd({
                        loadFrom: 'pumpworkform',
                        btn: btn,
                        afterFormLoaded: function () {
                            //级联关系处理:客户-合同-任务单
                            var custList = $("input[name='CustomerID']");
                            var contractList = $("#ContractID");
                            var taskList = $("#TaskID");
                            var pumpTypeList = $("#PumpType");
                            var carList = $("#CarID");
                            var pumpDateField = $("#PumpDate");
                            custList.unbind("change");
                            custList.bind("change", function () {
                                var custID = $(this).val();
                                if (custID) {
                                    bindSelectData(
                                        contractList,
                                        options.listContractUrl,
                                        {
                                            textField: "ContractName",
                                            valueField: "ID",
                                            condition: "CustomerID='" + custID + "'"
                                        },
                                        function () {
                                            taskList.empty();
                                        }
                                    );
                                } else {
                                    contractList.empty();
                                }
                            });
                            contractList.unbind("change");
                            contractList.bind("change", function () {
                                var contractID = $(this).val();
                                if (contractID) {
                                    bindSelectData(
                                        taskList,
                                        options.listTaskUrl,
                                        {
                                            textField: "ID",
                                            valueField: "ID",
                                            condition: "ContractID='" + contractID + "'"
                                        }
                                    );
                                } else {
                                    taskList.empty();
                                }
                            });
                            taskList.unbind("change");
                            taskList.bind("change", function () {
                                fillPumpTypeCost();
                            });

                            pumpDateField.unbind("change");
                            pumpDateField.bind("change", function () {
                                fillPumpTypeCost();
                            });
                            //泵车类型与泵车绑定
                            pumpTypeList.unbind("change");
                            pumpTypeList.bind("change", function () {
                                var pumpTypeID = $(this).val();
                                if (pumpTypeID) {
                                    bindSelectData(
                                        carList,
                                        options.listCarUrl,
                                        {
                                            textField: "ID",
                                            valueField: "ID",
                                            condition: "PumpTypeID='" + pumpTypeID + "'"
                                        }
                                    );
                                } else {
                                    carList.empty();
                                }
                            });

                            function fillPumpTypeCost(taskId, pumpDate) {

                                var taskId = $("#TaskID").val();
                                var pumpDate = $("#PumpDate").val();
                                if (pumpDate == "") {
                                    showError('提示', '请选择泵送日期！');
                                    return false;
                                }
                                if (taskId == ""||taskId ==null) {
                                    showError('提示', '请选择任务单！');
                                    return false;
                                }

                                ajaxRequest(
                                        options.getPumpTypeCostStoreUrl,
                                        {
                                            taskId: taskId,
                                            pumpDate: pumpDate
                                        },
                                        false,
                                        function (responseData) {
                                            var pumpType = responseData.PumpType;
                                            $("#PumpType").val(pumpType);
                                            $("#PumpType").trigger("change");

                                            PumpWorkGrid.setFormFieldValue("CustNum", responseData.ShipSum);
                                            PumpWorkGrid.setFormFieldValue("PumpNum", responseData.ShipSum);
                                            PumpWorkGrid.setFormFieldValue("PumpPrice", responseData.PumpCost);
                                        }

                                    );
                            }
                            /*
                            *计算混凝土与砂浆泵送金额
                            *计算规则如下：
                            *混凝土泵送金额（PumpSum） = 客户数量（CustNum）* 泵费（PumpPrice）
                            *砂浆泵送金额（SlurryPumpSum） = 砂浆客户数量（SlurryCustNum）* 砂浆泵费（SlurryPumpPrice）
                            *应收账款（Credit）= 混凝土泵送金额（PumpSum）+ 砂浆泵送金额（SlurryPumpSum）
                            */
                            var custnum = PumpWorkGrid.getFormField("CustNum");
                            var pumpprice = PumpWorkGrid.getFormField("PumpPrice");
                            var slurrycustnum = PumpWorkGrid.getFormField("SlurryCustNum");
                            var slurrypumpprice = PumpWorkGrid.getFormField("SlurryPumpPrice");
                            var tmpPumpSum = 0;
                            var tmpSlurryPumpSum = 0;
                            var tmpCredit = 0;
                            custnum.bind("blur", calcCredit);
                            pumpprice.bind("blur", calcCredit);
                            slurrycustnum.bind("blur", calcCredit);
                            slurrypumpprice.bind("blur", calcCredit);

                            function calcCredit() {
                                tmpPumpSum = (isEmpty(custnum.val()) ? 0 : parseFloat(custnum.val())) * (isEmpty(pumpprice.val()) ? 0 : parseFloat(pumpprice.val()));
                                tmpSlurryPumpSum = (isEmpty(slurrycustnum.val()) ? 0 : parseFloat(slurrycustnum.val())) * (isEmpty(slurrypumpprice.val()) ? 0 : parseFloat(slurrypumpprice.val()));
                                tmpCredit = tmpPumpSum + tmpSlurryPumpSum;
                                PumpWorkGrid.setFormFieldValue("PumpSum", tmpPumpSum);
                                PumpWorkGrid.setFormFieldValue("SlurryPumpSum", tmpSlurryPumpSum);
                                PumpWorkGrid.setFormFieldValue("Credit", tmpCredit);
                            }

                        },
                        beforeSubmit: function () {
                            var custnum = PumpWorkGrid.getFormField("CustNum");
                            var pumpprice = PumpWorkGrid.getFormField("PumpPrice");
                            var slurrycustnum = PumpWorkGrid.getFormField("SlurryCustNum");
                            var slurrypumpprice = PumpWorkGrid.getFormField("SlurryPumpPrice");
                            var tmpPumpSum = 0;
                            var tmpSlurryPumpSum = 0;
                            var tmpCredit = 0;
                            tmpPumpSum = (isEmpty(custnum.val()) ? 0 : parseFloat(custnum.val())) * (isEmpty(pumpprice.val()) ? 0 : parseFloat(pumpprice.val()));
                            tmpSlurryPumpSum = (isEmpty(slurrycustnum.val()) ? 0 : parseFloat(slurrycustnum.val())) * (isEmpty(slurrypumpprice.val()) ? 0 : parseFloat(slurrypumpprice.val()));
                            tmpCredit = tmpPumpSum + tmpSlurryPumpSum;
                            PumpWorkGrid.setFormFieldValue("PumpSum", tmpPumpSum);
                            PumpWorkGrid.setFormFieldValue("SlurryPumpSum", tmpSlurryPumpSum);
                            PumpWorkGrid.setFormFieldValue("Credit", tmpCredit);
                            return true;
                        }
                    });
                },
                handleEdit: function (btn) {
                    PumpWorkGrid.handleEdit({
                        loadFrom: 'pumpworkeditform',
                        btn: btn,
                        afterFormLoaded: function () {
                            PumpWorkGrid.setFormFieldValue("CustomerID", PumpWorkGrid.getSelectedRecord()["CustName"]);
                            PumpWorkGrid.setFormFieldValue("ContractID", PumpWorkGrid.getSelectedRecord()["ContractName"]);

                            //泵车类型与泵车绑定
                            var pumpTypeList = $("#PumpType_EDIT");
                            var carList = $("#CarID_EDIT");
                            var pumpDateFieldEdit = $("#PumpDate_EDIT");
                            pumpDateFieldEdit.unbind("change");
                            pumpDateFieldEdit.bind("change", function () {
                                fillPumpTypeCost();
                            });
                            //表单加载时泵车列表绑定数据
                            var pt = PumpWorkGrid.getSelectedRecord()["PumpType"];
                            bindSelectData(
                                        carList,
                                        options.listCarUrl,
                                        {
                                            textField: "ID",
                                            valueField: "ID",
                                            condition: "PumpTypeID='" + pt + "'"
                                        },
                                        function () {
                                            PumpWorkGrid.setFormFieldValue("CarID", PumpWorkGrid.getSelectedRecord()["CarID"]);
                                        }
                            );
                            pumpTypeList.unbind("change");
                            pumpTypeList.bind("change", function () {
                                var pumpTypeID = $(this).val();
                                if (pumpTypeID) {
                                    bindSelectData(
                                        carList,
                                        options.listCarUrl,
                                        {
                                            textField: "ID",
                                            valueField: "ID",
                                            condition: "PumpTypeID='" + pumpTypeID + "'"
                                        }
                                    );
                                } else {
                                    carList.empty();
                                }
                            });
                            /*
                            *计算混凝土与砂浆泵送金额
                            *计算规则如下：
                            *混凝土泵送金额（PumpSum） = 客户数量（CustNum）* 泵费（PumpPrice）
                            *砂浆泵送金额（SlurryPumpSum） = 砂浆客户数量（SlurryCustNum）* 砂浆泵费（SlurryPumpPrice）
                            *应收账款（Credit）= 混凝土泵送金额（PumpSum）+ 砂浆泵送金额（SlurryPumpSum）
                            */
                            var custnum = $("#CustNum_EDIT");
                            var pumpprice = $("#PumpPrice_EDIT");
                            var slurrycustnum = $("#SlurryCustNum_EDIT");
                            var slurrypumpprice = $("#SlurryPumpPrice_EDIT");
                            var tmpPumpSum = 0;
                            var tmpSlurryPumpSum = 0;
                            var tmpCredit = 0;
                            custnum.unbind("blur");
                            custnum.bind("blur", calcCredit);
                            pumpprice.unbind("blur");
                            pumpprice.bind("blur", calcCredit);
                            slurrycustnum.unbind("blur");
                            slurrycustnum.bind("blur", calcCredit);
                            slurrypumpprice.unbind("blur");
                            slurrypumpprice.bind("blur", calcCredit);

                            function calcCredit() {
                                tmpPumpSum = (isEmpty(custnum.val()) ? 0 : parseFloat(custnum.val())) * (isEmpty(pumpprice.val()) ? 0 : parseFloat(pumpprice.val()));
                                tmpSlurryPumpSum = (isEmpty(slurrycustnum.val()) ? 0 : parseFloat(slurrycustnum.val())) * (isEmpty(slurrypumpprice.val()) ? 0 : parseFloat(slurrypumpprice.val()));
                                tmpCredit = tmpPumpSum + tmpSlurryPumpSum;
                                $("#PumpSum_EDIT").val(tmpPumpSum);
                                $("#SlurryPumpSum_EDIT").val(tmpSlurryPumpSum);
                                $("#Credit_EDIT").val(tmpCredit);
                            }
                            function fillPumpTypeCost(taskId, pumpDate) {
                                var taskId = $("#TaskID_EDIT").val();
                                var pumpDate = $("#PumpDate_EDIT").val();
                                if (pumpDate == "") {
                                    showError('提示', '请选择泵送日期！');
                                    return false;
                                }
                                if (taskId == ""||taskId ==null) {
                                    showError('提示', '请选择任务单！');
                                    return false;
                                }
                                ajaxRequest(
                                        options.getPumpTypeCostStoreUrl,
                                        {
                                            taskId: taskId,
                                            pumpDate: pumpDate
                                        },
                                        false,
                                        function (responseData) {
                                            var pumpType = responseData.PumpType;
                                            $("#PumpType").val(pumpType);
                                            $("#PumpType").trigger("change");

                                            PumpWorkGrid.setFormFieldValue("CustNum", responseData.ShipSum);
                                            PumpWorkGrid.setFormFieldValue("PumpNum", responseData.ShipSum);
                                            PumpWorkGrid.setFormFieldValue("PumpPrice", responseData.PumpCost);
                                        }

                                    );
                            }
                        },
                        beforeSubmit: function () {
                            var custnum = $("#CustNum_EDIT");
                            var pumpprice = $("#PumpPrice_EDIT");
                            var slurrycustnum = $("#SlurryCustNum_EDIT");
                            var slurrypumpprice = $("#SlurryPumpPrice_EDIT");
                            var tmpPumpSum = 0;
                            var tmpSlurryPumpSum = 0;
                            var tmpCredit = 0;
                            tmpPumpSum = (isEmpty(custnum.val()) ? 0 : parseFloat(custnum.val())) * (isEmpty(pumpprice.val()) ? 0 : parseFloat(pumpprice.val()));
                            tmpSlurryPumpSum = (isEmpty(slurrycustnum.val()) ? 0 : parseFloat(slurrycustnum.val())) * (isEmpty(slurrypumpprice.val()) ? 0 : parseFloat(slurrypumpprice.val()));
                            tmpCredit = tmpPumpSum + tmpSlurryPumpSum;
                            $("#PumpSum_EDIT").val(tmpPumpSum);
                            $("#SlurryPumpSum_EDIT").val(tmpSlurryPumpSum);
                            $("#Credit_EDIT").val(tmpCredit);
                            return true;
                        }
                    });
                }
                , handleDelete: function (btn) {
                    PumpWorkGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
}