function classbIndexInit(options) {
    var BGrid = new MyGrid({
        renderTo: 'BGrid'
        //, width: '100%'
            , autoWidth: true
            , height: gGridHeight
		    , storeURL: options.bstoreUrl
            , storeCondition: "ClassID ='"+options.classTypeID+"'"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 18
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , toolbarSearch: false
            , editSaveUrl: options.classbUpdateUrl
		    , initArray: [
                { label: '大类编号', name: 'ID', index: 'ID' }
                , { label: '大类名称', name: 'ClassBName', index: 'ClassBName', editable: true }
                , { label: '类别类型', name: 'ClassID', index: 'ClassID', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ClassType'} }
                , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false}
                } 
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
        BGrid.addListeners('beforeselect', function (id, record) {
            $(":button#mb").button("enable");
            $("#ClassM_ClassBID").val(id);
            MGrid.refreshGrid("ClassBID='" + id + "' and ClassID = '" + record.ClassID + "'");
        });

    BGrid.addListeners("gridComplete", function () {
        BGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ClassB.mvc/Delete" });
    });
    window.SaveB = function () {

        $.post(
                "/ClassB.mvc/Add",
                $("#ClassBForms").serialize(),
                function (data) {
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    BGrid.refreshGrid("1=1 AND ClassID ='"+options.classTypeID+"'");
                }
        );
    };
    var MGrid = new MyGrid({
        renderTo: 'MGrid'
            , autoWidth: true
            , height: gGridHeight * 0.5 - 3
		    , storeURL: options.mstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 9
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , toolbarSearch: false
		    , initArray: [
                  { label: '中类编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '大类编号', name: 'ClassBID', index: 'ClassBID', width: 100 }
                , { label: '中类名称', name: 'ClassMName', index: 'ClassMName', editable: true }
                , { label: '类别类型', name: 'ClassID', index: 'ClassID', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ClassType'} }
                , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false}
                } 
		    ]
		    , autoLoad: false
            , editSaveUrl: options.classmUpdateUrl
            , functions: {
                handleReload: function (btn) {
                    MGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MGrid.refreshGrid('1=1');
                }

            }
        });
        MGrid.addListeners('beforeselect', function (id, record) {
            $(":button#sb").button("enable");
            $("#Classs_ClassMID").val(record.ID);
            $("#Classs_ClassBID").val(record.ClassBID);
            SGrid.refreshGrid("ClassMID='" + record.ID + "' and ClassBID  = '" + record.ClassBID + "' and ClassID = '" + record.ClassID + "'");
        });
        MGrid.addListeners("gridComplete", function () {
            MGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ClassM.mvc/Delete" });
        });
    window.SaveM = function () {
        if (!BGrid.isSelectedOnlyOne() || isEmpty($("#ClassM_ClassBID").val())) {
            showMessage('提示', '请先选择大类！');
            return false;
        }
        $.post(
                "/ClassM.mvc/Add",
                $("#ClassMForms").serialize(),
                function (data) {
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    MGrid.reloadGrid("ClassID ='" + $("#ClassM_ClassID").val() + "' AND ClassBID = '" + $("#ClassM_ClassBID").val() + "'");
                }
        );
    };
    var SGrid = new MyGrid({
        renderTo: 'SGrid'
            , autoWidth: true
            , height: gGridHeight * 0.3 - 2
		    , storeURL: options.sstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 5
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , toolbarSearch: false
		    , initArray: [
                  { label: '细类编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '中类编号', name: 'ClassMID', index: 'ClassMID', width: 100 }
                , { label: '细类名称', name: 'ClassSName', index: 'ClassSName', editable: true }
                , { label: '类别类型', name: 'ClassID', index: 'ClassID', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ClassType'} }
                , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false}
                } 
		    ]
		    , autoLoad: false
            , editSaveUrl: options.classsUpdateUrl
            , functions: {
                handleReload: function (btn) {
                    SGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    SGrid.refreshGrid('1=1');
                }
            }
        });
        
        SGrid.addListeners("gridComplete", function () {
            SGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/Classs.mvc/Delete" });
        });
    window.SaveS = function () {
        if (!MGrid.isSelectedOnlyOne() || isEmpty($("#Classs_ClassMID").val())) {
            showMessage('提示', '请先选择中类！');
            return false;
        }
        $.post(
                "/ClassS.mvc/Add",
                $("#ClasssForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
  
                    SGrid.refreshGrid("ClassID ='" + $("#Classs_ClassID").val() + "' AND ClassBID = '" + $("#Classs_ClassBID").val() + "' AND ClassMID ='" + $("#Classs_ClassMID").val() + "'");
                }
        );
            };

            window.Search = function (t) {

                var classTypeVal = $("#Class" + t + "_ClassID").val();
                var classNID = $("#Class" + t + "_ID").val();
                var classNName = $("#Class" + t + "_Class" + t.toUpperCase() + "Name").val();
                var BSearchCondition = "";
                var MSearchCondition = "";
                var SSearchCondition = "";
                var CclassNname = isEmpty(classNName) ? "Class" + t.toUpperCase() + "Name = '" + classNName + "'" : "Class" + t.toUpperCase() + "Name like '%" + classNName + "%'";
                
                if (isEmpty(classNID) && isEmpty(classNName)) {
                    BSearchCondition = "1=1 AND ClassID ='" + options.classTypeID + "'";
                    MSearchCondition = "1=1 AND ClassID ='" + options.classTypeID + "'";
                    SSearchCondition = "1=1 AND ClassID ='" + options.classTypeID + "'";
                } else {
                    BSearchCondition = "ClassID ='" + classTypeVal + "' AND (ClassBID = '" + classNID + "' OR " + CclassNname + ")";
                    MSearchCondition = "ClassID ='" + classTypeVal + "' AND (ClassMID = '" + classNID + "' OR " + CclassNname + ")";
                    SSearchCondition = "ClassID ='" + classTypeVal + "' AND (ClassSID = '" + classNID + "' OR " + CclassNname + ")";
                }
                

                switch (t) {
                    case "B":
                        BGrid.refreshGrid(BSearchCondition);
                        break;
                    case "M":
                        MGrid.refreshGrid(MSearchCondition);
                        break;
                    case "s":
                        SGrid.refreshGrid(SSearchCondition);
                        break;
                }
            };
}