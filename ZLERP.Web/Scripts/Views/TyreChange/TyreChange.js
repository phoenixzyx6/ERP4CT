function tyrechangeIndexInit(options) {
    var lastMileage = 0;
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , dialogWidth: 300
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID' }
                , { label: '更换日期', name: 'ChangeDate', index: 'ChangeDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '车辆代号', name: 'CarID', index: 'CarID' }
                , { label: '轮胎类型', name: 'TyreType', index: 'TyreType' }
                , { label: '新轮胎编号', name: 'NewTyreID', index: 'NewTyreID' }
                , { label: '旧轮胎编号', name: 'OldTyreID', index: 'OldTyreID' }
                , { label: '更换原因', name: 'ChangeReason', index: 'ChangeReason' }
                , { label: '更换部位', name: 'InstallPlace', index: 'InstallPlace' }
                , { label: '里程', name: 'Mileage', index: 'Mileage' }
                , { label: '更换人', name: 'ChangeMan', jsonmap: 'ChangeManUser.TrueName', index: 'ChangeMan', sortable: false }
                , { label: '更换人', name: 'ChangeMan', index: 'ChangeMan', hidden: true }
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
                        width: 550,
                        postUrl: options.changeTyreUrl,
                        height: 250,
                        afterFormLoaded: function () {
                            $("select[name='CarID']").attr('disabled', false).next('button').attr('disabled', false);
                            $("select[name='NewTyreID']").attr('disabled', false).next('button').attr('disabled', false);
                            myJqGrid.setFormFieldDisabled('Old', true);
                            myJqGrid.setFormFieldDisabled('New', false);

                            $("input[name='Old'] ~ button")[0].disabled = false;
                            $("input[name='New'] ~ button")[0].disabled = false;

                            $("#TyreChange_TyreStatus1").attr("disabled", "");
                            $("#TyreChange_TyreStatus3").attr("disabled", "");
                            $("#TyreChange_TyreStatus4").attr("disabled", "");

                            myJqGrid.getFormField('CarID').bind('change', function () {
                                myJqGrid.setFormFieldValue("OldTyreID", '');
                                myJqGrid.setFormFieldValue("Old", '');
                            });
                        },
                        beforeSubmit: function () {
                            var mileage = myJqGrid.getFormFieldValue('Mileage');
                            if (parseFloat(mileage) <= lastMileage) {
                                showMessage('提示', '公里数必须大于上次换胎的公里数' + lastMileage);
                                return false;
                            }
                            var Old = myJqGrid.getFormFieldValue('Old');
                            if (isEmpty(Old)  ) {
                                showMessage('提示', '旧轮胎编号不能为空' );
                                return false;
                            }
                            var New = myJqGrid.getFormFieldValue('New');
                            if (isEmpty(New)) {
                                showMessage('提示', '新轮胎编号不能为空');
                                return false;
                            }
                            return true;
                        }

                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        width: 550,
                        height: 250,
                        afterFormLoaded: function () {
                            var record = myJqGrid.getSelectedRecord();

                            $("select[name='CarID']").attr('disabled', true).next('button').attr('disabled', true);
                            $("select[name='NewTyreID']").attr('disabled', true).next('button').attr('disabled', true);
                            myJqGrid.setFormFieldDisabled('Old', true);
                            myJqGrid.setFormFieldDisabled('New', true);

                            $("input[name='Old'] ~ button")[0].disabled = true;
                            $("input[name='New'] ~ button")[0].disabled = true;

                            $("#TyreChange_TyreStatus1").attr("disabled", "disabled");
                            $("#TyreChange_TyreStatus3").attr("disabled", "disabled");
                            $("#TyreChange_TyreStatus4").attr("disabled", "disabled");

                            myJqGrid.getFormField('New').val(record["NewTyreID"]);
                            myJqGrid.getFormField('Old').val(record["OldTyreID"]);

                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });



    F_InstallPlace = function () {
        var CarID = myJqGrid.getFormField("CarID").val();
        return { changedCondition: "CarID='" + myJqGrid.getFormField('CarID').val() + "'" };
    }

}