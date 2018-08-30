function IndexInit(storeUrl) {
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
		    , initArray: [
                { label: '车辆出租编号', name: 'ID', index: 'ID', width: 80,hidden:true }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
                , { label: '当事司机', name: 'UserID', index: 'UserID', width: 120 }
                , { label: '发生时间', name: 'OccurrenceTime', index: 'OccurrenceTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '罚款金额', name: 'FineAmount', index: 'FineAmount', width: 80 }
                , { label: '罚款描述', name: 'Describe', index: 'Describe', width: 180 }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 180 }
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
                        width:460,
                        height:300,
                        btn: btn,
                        afterFormLoaded: function () {
                            //myJqGrid.setFormFieldReadOnly('CarLend.ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        width: 460,
                        height: 300,
                        btn: btn,
                        afterFormLoaded: function () {
                            //myJqGrid.setFormFieldReadOnly('CarLend.ID', true);
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