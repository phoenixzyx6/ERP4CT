function carcardIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 320
            , dialogHeight: 270
            , singleSelect: false
            , rowNumbers: true
		    , initArray: [
                { label: '卡号', name: 'ID', index: 'ID' }
                , { label: '供货厂家编号', name: 'SupplyID', index: 'SupplyID', search: false, hidden: true }
                , { label: '供货厂家', name: 'SupplyName', index: 'SupplyInfo.SupplyName', search: false }
                , { label: '车号', name: 'CarID', index: 'CarID' }
                , { label: '车牌号', name: 'CarNo', index: 'Car.CarNo' }
                , { label: '卡片类型', name: 'CardType', index: 'CardType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CardType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('CardType')} }
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
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', true);
                        }
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