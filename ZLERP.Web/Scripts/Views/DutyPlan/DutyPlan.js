function dutyPlanIndexInit(opts) {

    //render用户
    function userFmt(cellvalue, options, rowObject) {
        if (cellvalue == null)
            return '';
//        if (typeof (cellvalue) != 'undefined' && !$.isEmptyObject(gUsers)) {
//            var total = gUsers.length;
//            for (var i = 0; i < total; ++i) {
//                var user = gUsers[i];
//                if (user.ID == cellvalue) {
//                    return user.TrueName;
//                }
//            }
//        }
        return cellvalue;
    }

    function userUnFmt(cellvalue, options, rowObject) {

        return cellvalue;
    }

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: opts.storeUrl
		, sortByField: 'ID'
		, primaryKey: 'ID'
        , sortOrder: 'ASC'
		, setGridPageSize: 50
		, showPageBar: true
        , editSaveUrl: opts.updateUrl
        , storeCondition: opts.currentCondition
		, initArray: [
            { label: '计划编号', name: 'ID', index: 'ID' }
            , { label: '值班日期', name: 'DutyDate', index: 'DutyDate', formatter: 'date' }
            , { label: '主调', name: 'MainDispatcher', index: 'MainDispatcher', formatter: userFmt, unformat: userUnFmt, editable: true, edittype: 'select', editoptions: { value: eval("(" + dispatcherInlineStr + ")")} }
            , { label: '副调', name: 'SecondDispatcher', index: 'SecondDispatcher', formatter: userFmt, unformat: userUnFmt, editable: true, edittype: 'select', editoptions: { value: eval("(" + dispatcherInlineStr + ")")} }
            , { label: '备注', name: 'Remark', index: 'Remark' }
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
                    width: 380,
                    height: 300,
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    beforeSubmit: function () {
                        return true;
                    },
                    postData: { beginDate: document.getElementById("DutyDate").value, sd: $('#SecondDispatcher').val(), md: $('#MainDispatcher').val() }
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    width: 380,
                    height: 300,
                    loadFrom: 'MyFormDiv',
                    btn: btn
                });
            },
            handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            },
            handleMultAdd: function (btn) {
                myJqGrid.handleAdd({
                    width: 320,
                    height: 200,
                    loadFrom: 'MultAddForm',
                    btn: btn,
                    afterFormLoaded: function () {
                        $('#BeginDate', '#MultAddForm').addClass('text requiredval');
                        $('#EndDate', '#MultAddForm').addClass('text requiredval');
                    },
                    beforeSubmit: function () {
                        return true;
                    },
                    postData: { beginDate: $('#BeginDate', '#MultAddForm').val(), EndDate: $('#EndDate', '#MultAddForm').val() }
                });
            },
            handleCurrentMonthPlan: function (btn) {
                myJqGrid.refreshGrid(opts.currentCondition);
            },
            handleNextMonthPlan: function (btn) {
                myJqGrid.refreshGrid(opts.nextCondition); 
            },
            handleAllPlan: function (btn) {
                myJqGrid.refreshGrid("1=1");
            }
        }
    });


    

}