function maintenanceIndexInit(options) {
    var BGrid = new MyGrid({
        renderTo: 'BGrid'
        //, width: '100%'
            , autoWidth: true
            , height: gGridHeight
		    , storeURL: options.bstoreUrl
            , storeCondition: "ClassID ='" + options.classTypeID + "'"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 18
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            //, toolbarSearch: false
		    , initArray: [
                { label: '大类编号', name: 'ID', index: 'ID' }
                , { label: '大类名称', name: 'ClassBName', index: 'ClassBName', editable: true }
                
                
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    BGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    BGrid.refreshGrid('1=1');
                }
            }
        });
        BGrid.addListeners("rowclick", function (id, record) {
            $("#ClassBID").val(id);
            $("#bigclassid").val(id);
            $("#bigclassname").val(record.ClassBName);
            MaintenanceGrid.refreshGrid("ClassBID='" + id + "'");
        });
        var MaintenanceGrid = new MyGrid({
            renderTo: 'MaintenanceGrid'
            //, width: '100%'
            , autoWidth: true
            , height: gGridHeight * 0.7
            , buttons: buttons1
            , buttonRenderTo: 'maintenanceButton'
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , editSaveUrl: options.maintenanceUpdateUrl
		    , initArray: [
                { label: '项目代码', name: 'ID', index: 'ID' }
                , { label: '大类编号', name: 'ClassBID', index: 'ClassBID' }
                , { label: '项目名称', name: 'ItemName', index: 'ItemName', editable: true }
                , { label: '领用类别', name: 'DrawType', index: 'DrawType', editable: true, edittype: 'select', editoptions: {value : dicToolbarSearchValues('DrawType')}, editrules: {required: true} }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true, edittype: 'textarea' }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    MaintenanceGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MaintenanceGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    maintenanceSave();
                },
                handleEdit: function (btn) {
                    MaintenanceGrid.handleEdit({
                        loadFrom: 'MaintenanceDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    MaintenanceGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        window.maintenanceSave = function () {
            var _classBID = $("#ClassBID").val();
            if (isEmpty(_classBID)) {
                showMessage('提示', '请先选择一个大类！');
                return false;
            }
            $.post(
                options.maintenanceAddUrl,
                $("#MaintenanceForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    //$("#MaintenanceForms")[0].reset();//此处会清掉所有表单项
                    $("#ID").val("");
                    $("#ItemName").val("");
                    $("#DrawType").val("");
                    $("#Remark").val("");
                    MaintenanceGrid.refreshGrid("ClassBID='" + _classBID + "'");
                }
            );
        }

}