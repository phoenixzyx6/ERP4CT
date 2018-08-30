function taskassIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '辅助单号', name: 'ID', index: 'ID', width: 90 }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 90 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProduceTask.ProjectName' }
                , { label: '出货日期', name: 'AssDate', index: 'AssDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '单据类型', name: 'AssType', index: 'AssType', width: 90, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AssType'} }
                , { label: '数量', name: 'AssNum', index: 'AssNum', width: 90 }
                , { label: '单价', name: 'AssPrice', index: 'AssPrice', width: 90, formatter: 'currency', align: 'right' }
                , { label: '金额', name: 'AssTotal', index: 'AssTotal', width: 90, formatter: 'currency', align: 'right' }
                , { label: '应收账款', name: 'APMoney', index: 'APMoney', width: 90, formatter: 'currency', align: 'right' }
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
                            myJqGrid.getFormField("AssPrice").bind('blur', function () {
                                var AssPrice = myJqGrid.getFormField("AssPrice").val();
                                var AssNum = myJqGrid.getFormField("AssNum").val();
                                myJqGrid.getFormField("AssTotal").val(AssPrice * AssNum);
                                myJqGrid.getFormField("APMoney").val(AssPrice * AssNum);
                            });

                            myJqGrid.getFormField("AssNum").bind('blur', function () {
                                var AssPrice = myJqGrid.getFormField("AssPrice").val();
                                var AssNum = myJqGrid.getFormField("AssNum").val();
                                myJqGrid.getFormField("AssTotal").val(AssPrice * AssNum);
                                myJqGrid.getFormField("APMoney").val(AssPrice * AssNum);
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.getFormField("AssPrice").bind('blur', function () {
                                var AssPrice = myJqGrid.getFormField("AssPrice").val();
                                var AssNum = myJqGrid.getFormField("AssNum").val();
                                myJqGrid.getFormField("AssTotal").val(AssPrice * AssNum);
                                myJqGrid.getFormField("APMoney").val(AssPrice * AssNum);
                            });
                            myJqGrid.getFormField("AssNum").bind('blur', function () {
                                var AssPrice = myJqGrid.getFormField("AssPrice").val();
                                var AssNum = myJqGrid.getFormField("AssNum").val();
                                myJqGrid.getFormField("AssTotal").val(AssPrice * AssNum);
                                myJqGrid.getFormField("APMoney").val(AssPrice * AssNum);
                            });
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
}