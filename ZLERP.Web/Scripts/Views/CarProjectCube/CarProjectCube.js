
function IndexInit(storeUrl) {

    var MyJqGridMain = new MyGrid({
        renderTo: 'MyJqGridMain'
          //, width: '100%'
            , title: '价格方案列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: "/CarProjectCubeMain.mvc/Find"
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '主项编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '方案名称', name: 'Name', index: 'Name', formatter: 'text',width:100 }
                , { label: '执行时间', name: 'RunTime', index: 'RunTime', formatter: 'date', width: 70 }
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
        myJqGrid.refreshGrid("CarProjectCubeMainId='" + id + "'");
        myJqGridCar.refreshGrid("CarProjectCubeMainId='" + id + "'");
    });

    //----------------------------------------------------右边Gird------------------------------------------------------
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , title: '方量区间对应价格'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight/3
		    , storeURL: storeUrl
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
            //, groupingView: { groupSummary: [true], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            //, groupField: 'DriverClassType'
             , editSaveUrl: "/CarProjectCube.mvc/Update"
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '主表编号', name: 'CarProjectCubeMainId', index: 'CarProjectCubeMainId', editable: true, editrules: { required: true }, hidden: true }
                , { label: '开始方量', name: 'StartCube', index: 'StartCube', formatter: 'text', width: 120, hidden: false }
                , { label: '结束方量', name: 'EndCube', index: 'EndCube', formatter: 'text', width: 120, hidden: false }
                , { label: '计算方量', name: 'Cube', index: 'Cube', editable: true }
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
                            var s = $("#StartCube").val();
                            var e = $("#EndCube").val();
                            if (s * 1 >= e * 1) {
                                showError('错误', '开始方量不能大于结束方量！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {

                            $("#CarProjectCubeMainId").val(selectrecord.ID);
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

        var myJqGridCar = new MyGrid({
            renderTo: 'MyJqGridCar'
            , title: '车列表'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight/2-30
		    , storeURL: '/CarProjectCubeCar.mvc/Find'
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
            //, groupingView: { groupSummary: [true], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            //, groupField: 'DriverClassType'
             , editSaveUrl: "/CarProjectCubeCar.mvc/Update"
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '主表编号', name: 'CarProjectCubeMainId', index: 'CarProjectCubeMainId', editable: true, editrules: { required: true }, hidden: true }
                , { label: '车号', name: 'CarID', index: 'CarID', formatter: 'text', width: 120, hidden: false }
                , { label: '备注', name: 'Meno', index: 'Meno', formatter: 'text', width: 120, hidden: false, editable: true }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myJqGridCar.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGridCar.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!MyJqGridMain.isSelectedOnlyOne()) {
                        showError("提示", "请选择左边表格一条记录");
                        return false;
                    }
                    var selectrecord = MyJqGridMain.getSelectedRecord();
                    myJqGridCar.handleAdd({
                        loadFrom: 'MyFormDivCar',
                        btn: btn,
                        beforeSubmit: function () {
                            var a = $("#CarProjectCubeMainId").val();

                            if (a="") {
                                showError('错误', '请选择左边表格一条记录！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {
                            myJqGridCar.setFormFieldValue('CarProjectCubeMainId', selectrecord.ID)
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGridCar.handleEdit({
                        loadFrom: 'MyFormDivCar',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    myJqGridCar.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
}
