function tyrerepairIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , storeCondition: "RepairResult is  null"
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 300
            , dialogHeight: 300
		    , showPageBar: true
		    , initArray: [
                { label: '维修编号', name: 'ID', index: 'ID' }
                , { label: '维修时间', name: 'RepairDate', index: 'RepairDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '送修时间', name: 'ReceiveDate', index: 'ReceiveDate', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '车辆代号', name: 'CarID', index: 'CarID' }
                , { label: '轮胎编号', name: 'TyreID', index: 'TyreID' }
                , { label: '维修类型', name: 'RepairType', index: 'RepairType' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '是否修好', width: 60, name: 'RepairResult', index: 'RepairResult', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '修理备注', name: 'RepairRemark', index: 'RepairRemark' }
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

                            myJqGrid.setFormFieldDisabled('CarID', false);
                            $("input[name='TyreName'] ~ button")[0].disabled = false;
                            myJqGrid.setFormFieldDisabled('TyreName', true);

                            myJqGrid.setFormFieldDisabled('CarID', false);

                            myJqGrid.getFormField('CarID').bind('change', function () {

                                $("input[name='TyreName'] ~ button")[0].value = '';
                                myJqGrid.setFormFieldValue("TyreName", '');
                                myJqGrid.setFormFieldValue("TyreID", '');
                            });
                        }
                    });
                },
                handleEdit: function (btn) {

                    var record = myJqGrid.getSelectedRecord();

                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {

                            myJqGrid.setFormFieldDisabled('CarID', true);
                            $("input[name='TyreName'] ~ button")[0].disabled = true;
                            myJqGrid.setFormFieldDisabled('TyreName', true);

                            myJqGrid.setFormFieldValue("TyreName", record["TyreID"]);
                            myJqGrid.setFormFieldValue("TyreID", record["TyreID"]);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , reloadGrid: true
                    });
                }
                , handleRepairResult: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record["RepairResult"] != null && record["RepairResult"] != 'null') {
                        showMessage("消息", "不能重复处理该记录！");
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'RepairResultFormDiv',
                        btn: btn,
                        width: 320,
                        height: 200,
                        postUrl: options.repairResultUrl,
                        afterFormLoaded: function () {

                            myJqGrid.getFormField('Repair').bind('change', function () {

                                var RepairValue = $('input:radio[name = "Repair"]:checked').val();

                                if (RepairValue == 'TyreStatus4') {
                                    $("#TyreRepair_Return2").click();
                                    $("#TyreRepair_Return1").attr("disabled", "disabled");
                                }
                                else {
                                    $("#TyreRepair_Return1").removeAttr("disabled");
                                }

                            });

                        }
                    });
                }
                , handleRepairing: function (btn) {
                    myJqGrid.refreshGrid("RepairResult is  null");
                }
                , handleRepaired: function (btn) {
                    myJqGrid.refreshGrid("RepairResult is not  null");
                }
            }
    });
    CallBack = function () {

        var CarID = myJqGrid.getFormField("CarID").val();
        if (CarID == '' || CarID == undefined) {
            return { changedCondition: " CurrentStatus = 'TyreStatus1'" };
    } 
        else {
            return { changedCondition: "CarID='" + myJqGrid.getFormField('CarID').val() + "'" };
        }
    }
        
 

}