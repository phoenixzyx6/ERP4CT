//报表模版
function tplmanageIndexInit(storeUrl) {
    var tplJqGrid = new MyGrid({
        renderTo: 'tplJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 450
            , dialogHeight:300
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '模版编号', name: 'ID', index: 'ID', width:80 }
                , { label: '模版类型', name: 'TplType', index: 'TplType', width: 100 }
                , { label: '模版名称', name: 'TplName', index: 'TplName', width: 200 }
                , { label: '模版文件', name: 'TplFileName', index: 'TplFileName', width: 150 }
                , { label: '模版路径', name: 'TplUrl', index: 'TplUrl', width: 300 }
                , { label: '预览路径', name: 'PreviewUrl', index: 'PreviewUrl', width: 300 }
                , { label: '传递参数', name: 'Parms', index: 'Parms', width: 350 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    tplJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    tplJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    tplJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    tplJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    tplJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}