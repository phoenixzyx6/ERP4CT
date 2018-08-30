function customerplanIndexInit(opts) {
    var d = new Date();
    var tomorrow = opts.tomorrowDate; //  d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
    var condition = "PlanDate >= '" + tomorrow + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'";
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'NeedDate, ConstructUnit '
		    , primaryKey: 'ID'
		    , setGridPageSize: 50
            , dialogWidth: 510
            , dialogHeight: 480
		    , showPageBar: true
            , singleSelect: true
            , storeCondition: condition
            , groupField: 'PlanDate'
            , groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [true], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: '工地计划编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', search: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, width: 60 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName' }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit' }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName' }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr' }
                , { label: '供应单位', name: 'SupplyUnit', index: 'SupplyUnit' }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 60 }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 60 }
                , { label: '坍落度', name: 'Slump', index: 'Slump', width: 60 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 60 }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 100 }
                , { label: '计划方量', name: 'PlanCube', index: 'PlanCube', width: 60, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
                , { label: '计划日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date', search: false, sortable: false, width: 80 }
                , { label: '到场时间', name: 'NeedDate', index: 'NeedDate', search: false, width: 60 }
                , { label: '区间', name: 'RegionID', index: 'RegionID', width: 80 }
                , { label: '开盘安排', name: 'PiepLen', index: 'PiepLen', width: 50 }
                , { label: '工地电话', name: 'Tel', index: 'Tel' }
                , { label: '工地联系人', name: 'LinkMan', index: 'LinkMan' }
                , { label: '泵工', name: 'PumpMan', index: 'PumpMan', width: 60 }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID' }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'date', search: false }
                , { label: '审核意见', name: 'AuditInfo', index: 'AuditInfo', search: false }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', search: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        closeDialog: false,
                        afterFormLoaded: function () {

                            myJqGrid.setFormFieldReadOnly('ContractName', false);
                            myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                            //myJqGrid.setFormFieldReadOnly('ConStrength', true);
                            myJqGrid.setFormFieldReadOnly('Slump', true);
                            myJqGrid.setFormFieldReadOnly('CastMode', true);
                            myJqGrid.setFormFieldReadOnly('NeedDate', true);
                            // myJqGrid.setFormFieldReadOnly('ProjectName', true);
                            $("input[name='ContractName'] ~ button")[0].disabled = false;
                            linkManChange();
                            contractChange();

                        },
                        beforeSubmit: function () {

                            if (IsExistConStrength()) {
                                return true;
                            } else {
                                return false;
                            }

                        },
                        postCallBack: function (response) {
                            myJqGrid.getFormField("ConsPos").val("");
                            myJqGrid.getFormField("ConStrength").val("");

                            //if (btn.currentTarget != null) {
                            //    $(btn.currentTarget).button({ disabled: false });
                            //}
                        }
                    });
                },
                handleEdit: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.AuditStatus != '0') {
                        showError('该计划单已审核,不能修改');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldValue("ContractName", record.ContractName);
                            myJqGrid.setFormFieldReadOnly('ContractName', true);

                            myJqGrid.setFormFieldReadOnly('SupplyUnit', true);
                            //                            myJqGrid.setFormFieldReadOnly('ConStrength', true);
                            myJqGrid.setFormFieldReadOnly('Slump', true);
                            myJqGrid.setFormFieldReadOnly('CastMode', true);
                            myJqGrid.setFormFieldReadOnly('NeedDate', true);
                            myJqGrid.setFormFieldReadOnly('ProjectName', true);

                            $("input[name='ContractName'] ~ button")[0].disabled = true; //禁用AutoComplete控件
                            linkManChange();

                            var contractId = myJqGrid.getFormField("ContractID").val();
                            getCastMode(contractId); //加载合同对应的浇筑方式
                            getConStrength(contractId); //加载合同对应的砼强度列表

                        }
                        ,
                        beforeSubmit: function () {

                            if (IsExistConStrength()) {
                                return true;
                            } else {
                                return false;
                            }

                        }
                    });
                }
                , handleDelete: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.AuditStatus != '0') {
                        showError('该计划单已审核,不能删除');
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleTomorrowPlan: function (btn) {
                    var d = new Date();
                    var tomorrow = opts.tomorrowDate; //d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + (d.getDate() + 1);
                    var condition = "PlanDate >= '" + tomorrow + " 00:00:00' and PlanDate < '" + tomorrow + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }
                , handleTodayPlan: function (btn) {
                    var d = new Date();
                    var today = d.format("Y-m-d");
                    var condition = "PlanDate >= '" + today + " 00:00:00' and PlanDate < '" + today + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }
                , handleAllPlan: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                }
            }
    });
    function disableFormFields(disable) {
        myJqGrid.setFormFieldReadOnly('ProjectName', disable);
        myJqGrid.setFormFieldReadOnly('ProjectAddr', disable);
        myJqGrid.setFormFieldReadOnly('ConStrength', disable);
        myJqGrid.setFormFieldReadOnly('ConsPos', disable);
        myJqGrid.setFormFieldReadOnly('Slump', disable);
        myJqGrid.setFormFieldReadOnly('CastMode', disable);
        myJqGrid.setFormFieldReadOnly('Tel', disable);
        myJqGrid.setFormFieldReadOnly('LinkMan', disable);
    }

    var target = $('#TaskID');
    target.append("<option value=''>新任务单</option>");
    if (typeof (tasks) != 'undefined' && tasks.length > 0) {
        var target = $('#TaskID');
        target.append("<option value=''>新任务单</option>");
        //每天的任务单都是新单子，因此不需要下拉获取旧数据
        /*
        for(var i = 0; i < tasks.length; i++) {
        var d = tasks[i];
        target.append("<option value='" + d.ID + "'>" + d.ProjectName +"-" +d.ConStrength+"-" + d.ConsPos +"-" + d.CastMode+ "</option>");
        }
            
        target.bind('change', taskChanged);
        */
    }

    window.linkManChange = function () {
        var linkManField = myJqGrid.getFormField("LinkMan");
        linkManField.unbind('change');
        linkManField.bind('change', function (event) {
            var linkMan = linkManField.val();
            var selectedLinkMan = $('#LinkMan', '#' + myJqGrid.getFormId()).children(':selected').text();
            var linkManId = $('#LinkMan', '#' + myJqGrid.getFormId()).val();
            myJqGrid.setFormFieldValue('Tel', '');

            if (linkManId && linkMan == selectedLinkMan) {
                var requestURL = opts.getUserUrl;
                var postParams = { id: linkManId };
                var rData;
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        rData = response.Data;
                        myJqGrid.getFormField("Tel").val(response.Data == null ? "" : rData.Tel);
                    }
                });
            }
        });
    };

    //自动设置施工单位为客户名称
    contractChange = function () {
        var contractIdField = myJqGrid.getFormField("ContractID");

        contractIdField.unbind('change');
        contractIdField.bind('change', function () {
            //myJqGrid.setFormFieldValue('ProjectName', myJqGrid.getFormFieldValue("ContractName"));
            //myJqGrid.getFormField('ProjectName').focus();
            $('#ProjectName').empty();
            myJqGrid.getFormField("ProjectName").val('');
            var contractId = contractIdField.val();



            if (!isEmpty(contractId)) {
                //bindSelectData($('[name="ConStrength"]'),
                getCastMode(contractId); //加载合同对应的浇筑方式
                getConStrength(contractId); //加载合同对应的砼强度列表

                bindSelectData($('#ProjectName'),
                    '/Project.mvc/ListData1',
                    { textField: 'ProjectName',
                        valueField: 'ID',
                        condition: "ContractID='" + contractId + "'"
                    },
                    function (data) {
                        var count = data.length;
                        if (count > 0) {
                            $('#ProjectName').children().first().remove(); //施工单位清掉

                            //获取加
                            ajaxRequest('/Project.mvc/GetProjectName', { id: contractId }, false, function (response) {
                                if (response) {
                                    var name = response.Name;
                                    //                                    alert($('[name="ProjectName"]').length);
                                    //                                    $('#ProjectName').val(name);
                                    $('[name="ProjectName"]').val(name);
                                }
                            })
                        }
                    }

                  );

                //mygrid.setFormFieldValue('ConstructUnit', mygrid.getFormFieldValue('CustName'));

                var requestURL = "/Contract.mvc/Get";
                var postParams = { id: contractId };
                var rData;
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        rData = response.Data;
                        myJqGrid.getFormField("ConstructUnit").val(response.Data == null ? "" : rData.CustName);
                        myJqGrid.getFormField("ProjectAddr").val(response.Data == null ? "" : rData.ProjectAddr);
                        myJqGrid.getFormField("Tel").val(response.Data == null ? "" : rData.BLinkTel);

                        //                            //加载第一个工程名称
                        //                            $('#ProjectName').get(0).selectedIndex = 0;
                        //                            alert($('#ProjectName').get(0).selectedIndex);
                    }
                });
            }
        });
    };

    //加载合同对应的砼强度列表
    function getConStrength(contractId) {       
        bindSelectData($('#ConStrength'),
            '/CustomerPlan.mvc/ListDataStrengthInfo',
            { textField: 'ConStrength',
                valueField: 'ConStrength',
                condition: " ContractID='" + contractId + "'"
            });
    }

    //加载合同对应的浇筑方式
    function getCastMode(contractId) {        
        bindSelectData($('[name="CastMode"]'),
        '/ContractPump.mvc/ListData',
        { textField: 'PumpType',
            valueField: 'PumpType',
            condition: " ContractID='" + contractId + "'"
        });
    }

    //判断是否砼强度是否存在
    IsExistConStrength = function () {

        var ischeck = false;
        var str1 = myJqGrid.getFormField("ContractID").val();
        var str2 = $('[name="ConStrength"]').val();
        $.ajax({
            url: "/ContractItem.mvc/getContractItem",
            type: 'POST',
            json: 'json',
            async: false, //必须是同步
            data: { contractid: str1, constrength: str2 },
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
            showError('提示', "请选择合同匹配的砼强度,不能手动输入!");
            return false;
        }
    }

}