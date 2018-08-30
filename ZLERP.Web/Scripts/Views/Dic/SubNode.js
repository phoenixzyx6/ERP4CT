function dicIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
            , storeCondition: "ParentID='" + options.ParentID + "'"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 300
            , dialogHeight: 250
		    , showPageBar: true
		    , initArray: [
                { label: '字典编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '名称', name: 'DicName', index: 'DicName' }
                , { label: '编号', name: 'TreeCode', index: 'TreeCode' }
                , { label: '描述', name: 'Des', index: 'Des' }
                , { label: '父节点', name: 'ParentID', index: 'ParentID', hidden: true }
                , { label: '是否叶子节点', name: 'IsLeaf', index: 'IsLeaf', hidden: true }
                , { label: '排序', name: 'OrderNum', index: 'OrderNum', hidden: true }
                , { label: '深度', name: 'Deep', index: 'Deep', hidden: true }
                , { label: '扩展字段1', name: 'Field1', index: 'Field1', hidden: true }
                , { label: '扩展字段2', name: 'Field2', index: 'Field2', hidden: true }
                , { label: '扩展字段3', name: 'Field3', index: 'Field3', hidden: true }
                , { label: '扩展字段4', name: 'Field4', index: 'Field4', hidden: true }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid("ParentID='" + options.ParentID + "'");
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            var idValue = options.ParentID + "" + new Date().valueOf();
                            myJqGrid.setFormFieldValue('ID', idValue);
                            myJqGrid.setFormFieldValue('IsLeaf', true);
                            myJqGrid.setFormFieldValue("ParentID", options.ParentID);
                        },
                        reloadGrid: false,
                        postCallBack: function () {
                            myJqGrid.refreshGrid("ParentID='" + options.ParentID + "'");
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