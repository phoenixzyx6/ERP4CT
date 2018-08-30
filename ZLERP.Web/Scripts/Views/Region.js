//区间数据维护
function regionIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: ' 区间代号', name: 'ID', index: 'ID', editable: true, editrules: { required: true} }
                , { label: ' 区间名称', name: 'RegionName', index: 'RegionName', editable: true }
                , { label: ' 里程', name: 'Mileage', index: 'Mileage', editable: true }
                , { label: ' 趟次单价', name: 'UnitPrice', index: 'UnitPrice', editable: true }
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
                        btn: btn,
                        afterFormLoaded: function () { myJqGrid.setFormFieldReadOnly("ID",false); }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () { myJqGrid.setFormFieldReadOnly("ID", true); }
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