
function IndexInit(opts) {
    var monthold;
    var MyJqGridMain = new MyGrid({
        renderTo: 'MyJqGridMain'
        //, width: '100%'
            , title: '结存月份列表'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: "/GoodsAccountMain.mvc/Find"
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 400
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '主项编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '月份', name: 'Month', index: 'Month', formatter: 'text' }
        // , { label: '结束日期', name: 'EndDate', index: 'EndDate', formatter: 'date' }
                , { label: '备注', name: 'Meno', index: 'Meno' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    MyJqGridMain.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MyJqGridMain.refreshGrid('1=1');
                },
                handleAdd: function (btn) {

                    MyJqGridMain.handleAdd({
                        loadFrom: 'MyFormDivMain',
                        btn: btn,
                        width: 300,
                        height: 200,
                        beforeSubmit: function () {
                            var month = $("#Month").val();
                            if (isEmpty(month)) {
                                //设置为必填警示颜色
                                $("#Month").addClass('input-validation-error');
                                return false;
                            }
                            $("#Month").removeClass("input-validation-error");
                            var ids = MyJqGridMain.getJqGrid().jqGrid('getDataIDs');
                            var amount = 0;
                            for (var i = 0; i < ids.length; i++) {
                                var record = MyJqGridMain.getRecordByKeyValue(ids[i]);
                                if (record.Month == month) {
                                    showError('错误', '已经存在此月份,请选择其他月份！');
                                    return false;
                                }
                            }
                            return true;
                        }
                    });
                },
                handleEdit: function (btn) {
                    MyJqGridMain.handleEdit({
                        loadFrom: 'MyFormDivMain',
                        btn: btn,
                        width: 300,
                        height: 200,
                        afterFormLoaded: function () {
                            monthold = $("#Month").val(); //记录修改前月份值
                        },
                        beforeSubmit: function () {
                            var month = $("#Month").val();
                            if (isEmpty(month)) {
                                //设置为必填警示颜色
                                $("#Month").addClass('input-validation-error');
                                return false;
                            }
                            $("#Month").removeClass("input-validation-error");

                            var ids = MyJqGridMain.getJqGrid().jqGrid('getDataIDs');
                            var amount = 0;
                            for (var i = 0; i < ids.length; i++) {
                                var record = MyJqGridMain.getRecordByKeyValue(ids[i]);
                                if (record.Month == month & record.Month != monthold) {
                                    showError('错误', '已经存在此月份,请选择其他月份！');
                                    return false;
                                }
                            }

                            //                            var edate = $("#EndDate").val();
                            //                            if (isEmpty(edate)) {
                            //                                //设置为必填警示颜色
                            //                                $("#EndDate").addClass('input-validation-error');
                            //                                return false;
                            //                            }
                            //                            $("#EndDate").removeClass("input-validation-error");

                            //                            if (compareTime(sdate + " 00:00:00", edate + " 00:00:00")) {
                            //                                showError('错误', '开始时间不能大于结束时间！');
                            //                                return;
                            //                            }
                            return true;
                        }
                    });
                }
                , handleDelete: function (btn) {
                    MyJqGridMain.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    MyJqGridMain.addListeners('rowclick', function (id, record, selBool) {
        myJqGrid.refreshGrid("GoodsAccountMainId='" + id + "'");
    });

    //----------------------------------------------------右边Gird------------------------------------------------------
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , title: '结存材料列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
        //, groupingView: { groupSummary: [true], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
        //, groupField: 'SiloId'
             , editSaveUrl: "/GoodsAccount.mvc/Update"
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '主表编号', name: 'GoodsAccountMainId', index: 'GoodsAccountMainId', editable: true, editrules: { required: true }, hidden: true }
                , { label: '物资编号', name: 'GoodsId', index: 'GoodsId', hidden: false, width: 80 }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsName', hidden: false }
                , { label: '盘存数量', name: 'CurrentCount', index: 'CurrentCount', editable: true, width: 60 }
                , { label: '盘存金额', name: 'CurrentMoney', index: 'CurrentMoney', editable: true, width: 80 }
                , { label: '备注', name: 'Meno', index: 'Meno', editable: true }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!MyJqGridMain.isSelectedOnlyOne()) {
                        showError("提示", "请选择左边表格一条记录");
                        return false;
                    }
                    var selectrecord = MyJqGridMain.getSelectedRecord();

                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        beforeSubmit: function () {
                            var startKm = $("#StartKm").val();
                            var endKm = $("#EndKm").val();

                            if (endKm * 1 < startKm * 1) {
                                showError('错误', '开始里程不能大于结束里程！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {

                            $("#GoodsAccountMainId").val(selectrecord.ID);
                        }
                    });
                },
                handleEdit: function (btn) {
                    var selectrecord = MyJqGridMain.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        beforeSubmit: function () {
                            //                            alert(selectrecord.SiloId);
                            //                            $("#SiloId").val(selectrecord.SiloId);
                            return true;
                        },
                        afterFormLoaded: function () {

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
