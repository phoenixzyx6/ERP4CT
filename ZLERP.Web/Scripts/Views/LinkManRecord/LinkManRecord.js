function linkmanrecordIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 280
            , dialogHeight: 180
		    , showPageBar: true
            , singleSelect: true
		    , initArray: [
                { label: '记录ID', name: 'ID', index: 'ID', width: 50 }
                , { label: '内容', name: 'Content', index: 'Content' }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 50 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 50 }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
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