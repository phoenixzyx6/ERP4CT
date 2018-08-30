function sysFuncInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , treeGrid: true
        //set TreeGrid model
            , treeGridModel: 'adjacency'
        //set expand column
            , expandColumn: 'FuncName'
            , expandColClick: true
        //, groupField: 'UserType'
		    , setGridPageSize: -1
		    , showPageBar: false
            , singleSelect: true
            , dialogHeight: 400
            , dialogWidth: 500

		    , initArray: [
            { label: ' 功能名称', name: 'FuncName', index: 'FuncName', width: 200 }
                , { label: ' 功能编号', name: 'ID', index: 'ID', width: 80 }
                , { label: ' 功能描述', name: 'FuncDesc', index: 'FuncDesc', width: 80 }
                , { label: ' 父节点', name: 'ParentID', index: 'ParentID', width: 80 }
                , { label: ' 是否叶子节点', name: 'IsLeaf', index: 'IsLeaf', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: ' 按钮位置', name: 'ButtonPos', index: 'ButtonPos', width: 50, align: 'center' }
                , { label: ' 是否按钮', name: 'IsButton', index: 'IsButton', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: ' 功能图标', name: 'Icon', index: 'Icon', width: 80 }
                , { label: ' URL', name: 'URL', index: 'URL' }
                , { label: ' 是否禁用', name: 'IsDisabled', index: 'IsDisabled', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: ' 功能代码', name: 'HandlerName', index: 'HandlerName', width: 80 }
                , { label: ' 功能文件', name: 'HandlerFile', index: 'HandlerFile', width: 80 }
                , { label: ' 标志', name: 'Flag', width: 50, index: 'Flag', align: 'center' }
                , { label: ' 排序', name: 'OrderNum', width: 50, index: 'OrderNum', align: 'center' }
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
                        btn: btn,
                        loadFrom: 'MyFormDiv2'
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'MyFormDiv2'
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