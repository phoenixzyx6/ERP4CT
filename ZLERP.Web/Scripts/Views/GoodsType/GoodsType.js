
function goodstypeIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
        , dialogWidth: 300
        , dialogHeight: 200
		, storeURL: storeUrl
		, sortByField: 'ID'
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
		, initArray: [
            { label: '类型编号', name: 'ID', index: 'ID' }
            , { label: '类型名称', name: 'GoodsTypeName', index: 'GoodsTypeName' }
            , { label: '备注', name: 'Remark', index: 'Remark' }
            , { label: '创建人', name: 'Builder', index: 'Builder' }
            , { label: '修改人', name: 'Modifier', index: 'Modifier' }
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
                    btn: btn
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