function alkalireportIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
             , dialogWidth: 750
            , dialogHeight: 500
		    , initArray: [
                { label: '计算书编号', name: 'ID', index: 'ID' }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID' }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID' }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength' }
                , { label: '水泥原料', name: 'CEStuffID', index: 'CEStuffID' }
                , { label: '水泥试验', name: 'CEExamID', index: 'CEExamID' }
                , { label: '水泥厂商', name: 'CESupplyID', index: 'CESupplyID' }
                , { label: '水泥用量', name: 'CEAmount', index: 'CEAmount' }
                , { label: '水泥碱含量%', name: 'CEAlkPer', index: 'CEAlkPer' }
                , { label: '外加剂原料', name: 'ADM1StuffID', index: 'ADM1StuffID' }
                , { label: '外加剂试验', name: 'ADM1ExamID', index: 'ADM1ExamID' }
                , { label: '外加剂厂商', name: 'ADM1SupplyID', index: 'ADM1SupplyID' }
                , { label: '外加剂用量', name: 'ADM1Amount', index: 'ADM1Amount' }
                , { label: '外加剂碱含量%', name: 'ADM1AlkPer', index: 'ADM1AlkPer' }
                , { label: '煤灰原料', name: 'AIR1StuffID', index: 'AIR1StuffID' }
                , { label: '煤灰试验', name: 'AIR1ExamID', index: 'AIR1ExamID' }
                , { label: '煤灰厂商', name: 'AIR1SupplyID', index: 'AIR1SupplyID' }
                , { label: '煤灰用量', name: 'AIR1Amount', index: 'AIR1Amount' }
                , { label: '煤灰碱含量%', name: 'AIR1AlkPer', index: 'AIR1AlkPer' }
                , { label: '矿粉原料', name: 'AIR2StuffID', index: 'AIR2StuffID' }
                , { label: '矿粉试验', name: 'AIR2ExamID', index: 'AIR2ExamID' }
                , { label: '矿粉厂商', name: 'AIR2SupplyID', index: 'AIR2SupplyID' }
                , { label: '矿粉用量', name: 'AIR2Amount', index: 'AIR2Amount' }
                , { label: '矿粉碱含量%', name: 'AIR2AlkPer', index: 'AIR2AlkPer' }
                , { label: '膨胀剂原料', name: 'ADM2StuffID', index: 'ADM2StuffID' }
                , { label: '膨胀剂试验', name: 'ADM2ExamID', index: 'ADM2ExamID' }
                , { label: '膨胀剂厂商', name: 'ADM2SupplyID', index: 'ADM2SupplyID' }
                , { label: '膨胀剂用量', name: 'ADM2Amount', index: 'ADM2Amount' }
                , { label: '膨胀剂碱含量%', name: 'ADM2AlkPer', index: 'ADM2AlkPer' }
                , { label: '其他外加剂原料', name: 'ADM3StuffID', index: 'ADM3StuffID' }
                , { label: '其他外加剂试验', name: 'ADM3ExamID', index: 'ADM3ExamID' }
                , { label: '其他外加剂厂商', name: 'ADM3SupplyID', index: 'ADM3SupplyID' }
                , { label: '其他外加剂用量', name: 'ADM3Amount', index: 'ADM3Amount' }
                , { label: '其他外加剂碱含量%', name: 'ADM3AlkPer', index: 'ADM3AlkPer' }
                , { label: '碱总含量KG', name: 'AlkAmount', index: 'AlkAmount' }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult' }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
                , { label: '计算', name: 'Accountor', index: 'Accountor' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
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
                            myJqGrid.getFormField("CEAlkPer").bind('blur', function () {
                                var ceper = myJqGrid.getFormField("CEAlkPer").val();
                                var ceamount = myJqGrid.getFormField("CEAmount").val();
                                myJqGrid.getFormField("CEAlkAmount").val(ceper * ceamount / 100);
                            });
                            myJqGrid.getFormField("ADM1AlkPer").bind('blur', function () {
                                var adm1per = myJqGrid.getFormField("ADM1AlkPer").val();
                                var adm1amount = myJqGrid.getFormField("ADM1Amount").val();
                                myJqGrid.getFormField("ADM1AlkAmount").val(adm1per * adm1amount / 100);
                            });
                            myJqGrid.getFormField("AIR1AlkPer").bind('blur', function () {
                                var air1per = myJqGrid.getFormField("AIR1AlkPer").val();
                                var air1amount = myJqGrid.getFormField("AIR1Amount").val();
                                myJqGrid.getFormField("AIR1AlkAmount").val(air1per * air1amount / 100);
                            });
                            myJqGrid.getFormField("AIR2AlkPer").bind('blur', function () {
                                var air2per = myJqGrid.getFormField("AIR2AlkPer").val();
                                var air2amount = myJqGrid.getFormField("AIR2Amount").val();
                                myJqGrid.getFormField("AIR2AlkAmount").val(air2per * air2amount / 100);
                            });
                            myJqGrid.getFormField("ADM2AlkPer").bind('blur', function () {
                                var adm2per = myJqGrid.getFormField("ADM2AlkPer").val();
                                var adm2amount = myJqGrid.getFormField("ADM2Amount").val();
                                myJqGrid.getFormField("ADM2AlkAmount").val(adm2per * adm2amount / 100);
                            });
                            myJqGrid.getFormField("ADM3AlkPer").bind('blur', function () {
                                var adm3per = myJqGrid.getFormField("ADM3AlkPer").val();
                                var adm3amount = myJqGrid.getFormField("ADM3Amount").val();
                                myJqGrid.getFormField("ADM3AlkAmount").val(adm3per * adm3amount / 100);
                                var ce = myJqGrid.getFormField("CEAlkAmount").val();
                                var adm1 = myJqGrid.getFormField("ADM1AlkAmount").val();
                                var adm2 = myJqGrid.getFormField("ADM2AlkAmount").val();
                                var adm3 = myJqGrid.getFormField("ADM3AlkAmount").val();
                                var air1 = myJqGrid.getFormField("AIR1AlkAmount").val();
                                var air2 = myJqGrid.getFormField("AIR2AlkAmount").val();
                                myJqGrid.getFormField("AlkAmount").val(ce + adm1 + adm2 + adm3 + air1 + air2);
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}