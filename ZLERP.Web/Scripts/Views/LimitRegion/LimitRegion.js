
function limitRegionIndexInit(opts) {

    var LimitRegionGrid = new MyGrid({
        renderTo: 'LimitRegionGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.LimitRegionStoreUrl
            , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 380
            , dialogHeight: 170
		    , showPageBar: true
		    , initArray: [
                  { name: 'ID', index: 'ID', hidden: true }
                , { label: '名称', name: 'Name', index: 'Name', width: 300 }
                , { label: '是否为越界报警', name: 'IsOutAlarm', align: 'center', index: 'IsOutAlarm', width: 100, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '驶入报警', '1': '驶出报警' }, stype: 'select', searchoptions: { value: AlarmSearchValues() }, width: 60 }
                , { label: '是否显示', name: 'IsShow', index: 'IsShow', align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '隐藏', '1': '显示' }, stype: 'select', searchoptions: { value: ViewSearchValues() }, width: 60 }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    LimitRegionGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    LimitRegionGrid.refreshGrid('1=1');
                },
                handleEdit: function (btn) {
                    LimitRegionGrid.handleEdit({
                        loadFrom: 'LimitRegionForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    LimitRegionGrid.deleteRecord({
                        deleteUrl: opts.LimitRegionDeleteUrl
                    });
                }
            }
    });

        function AlarmSearchValues() {
            return { '': '', 1: '驶出报警', 0: '驶入报警' };
        }

        function ViewSearchValues() {
            return { '': '', 1: '显示', 0: '隐藏' };
        }
}
 