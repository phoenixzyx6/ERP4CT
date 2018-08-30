function StuffInfoPriceIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'year1 desc,StuffName,month1'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 300
            , dialogHeight: 250
            , singleSelect: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID',  hidden: true}
                , { label: '年份', name: 'year1', index: 'year1', width: 80 }
                , { label: '月份', name: 'month1', index: 'month1', width: 80 }
                , { label: '原料', name: 'StuffName', index: 'StuffName' }
                , { name: 'StuffID', index: 'StuffID', hidden: true }
                ,  { label: '单价', name: 'price', index: 'price', formatter: 'currency', align: 'right', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
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
                            myJqGrid.setFormFieldDisabled('year1', false);
                            myJqGrid.setFormFieldDisabled('month1', false);
                            myJqGrid.setFormFieldDisabled('StuffID', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('year1', true);
                            myJqGrid.setFormFieldDisabled('month1', true);
                            myJqGrid.setFormFieldDisabled('StuffID', true);
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