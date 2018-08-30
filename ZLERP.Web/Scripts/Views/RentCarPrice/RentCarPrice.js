function rentcarpriceIndexInit(storeUrl) {
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
            , dialogWidth: 530
            , dialogHeight: 249
		    , showPageBar: true
		    , initArray: [
                { label: '单价设定编号', name: 'ID', index: 'ID' }
                , { label: '单价设定时间', name: 'PriceSetTime', index: 'PriceSetTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '元/方（<=3km）', name: 'Price1', index: 'Price1' }
                , { label: '元/方（<=20km）', name: 'Price2', index: 'Price2' }
                , { label: '元/方（<=35km）', name: 'Price3', index: 'Price3' }
                , { label: '元/方（<=50km）', name: 'Price4', index: 'Price4' }
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
                        },
                        beforeSubmit: function () {

                            return true;
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