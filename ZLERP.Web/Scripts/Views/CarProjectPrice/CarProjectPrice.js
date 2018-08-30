
function CarProjectPriceIndexInit(storeUrl) {

    var MyJqGridMain = new MyGrid({
        renderTo: 'MyJqGridMain'
        //, width: '100%'
            , title: '价格方案列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: "/CarProjectPriceMain.mvc/Find"
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '主项编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '方案名称', name: 'Name', index: 'Name', formatter: 'text' }
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
                        beforeSubmit: function () {
                            var sdate = $("#Name").val();
                            if (isEmpty(sdate)) {
                                //设置为必填警示颜色
                                $("#Name").addClass('input-validation-error');
                                return false;
                            }
                            $("#Name").removeClass("input-validation-error");
                            
                            return true;
                        }
                    });
                },
                handleEdit: function (btn) {
                    MyJqGridMain.handleEdit({
                        loadFrom: 'MyFormDivMain',
                        btn: btn,
                        beforeSubmit: function () {
                            var sdate = $("#Name").val();
                            if (isEmpty(sdate)) {
                                //设置为必填警示颜色
                                $("#Name").addClass('input-validation-error');
                                return false;
                            }
                            $("#Name").removeClass("input-validation-error");

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
        myJqGrid.refreshGrid("CarProjectPriceMainId='" + id + "'");
    });

    //----------------------------------------------------右边Gird------------------------------------------------------
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , title: '车次区间对应价格'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
            //, groupingView: { groupSummary: [true], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            //, groupField: 'TypeId'
             , editSaveUrl: "/CarProjectPrice.mvc/Update"
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '主表编号', name: 'CarProjectPriceMainId', index: 'CarProjectPriceMainId', editable: true, editrules: { required: true }, hidden: true }
                , { label: '开始公里数', name: 'StartTimes', index: 'StartTimes', formatter: 'text', width: 120, hidden: false }
                , { label: '结束公里数', name: 'EndTimes', index: 'EndTimes', formatter: 'text', width: 120, hidden: false }
                , { label: '单价', name: 'Price', index: 'Price', editable: true }
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
                            var s = $("#StartTimes").val();
                            var e = $("#EndTimes").val();
                            if (s * 1 > e * 1) {
                                showError('错误', '开始车次不能大于结束车次！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {

                            $("#CarProjectPriceMainId").val(selectrecord.ID);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        beforeSubmit: function () {
                            var s = $("#StartTimes").val();
                            var e = $("#EndTimes").val();
                            if (s * 1 > e * 1) {
                                showError('错误', '开始车次不能大于结束车次！');
                                return;
                            }
                            return true;
                        },
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
