function carclassIndexInit(storeUrl, storeUrl1) {
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
            , dialogHeight: 240
            , dialogWidth: 400
		    , initArray: [
                  { label: '车种编号', name: 'ID', index: 'ID' }
                , { label: '车种名称', name: 'CarClassName', index: 'CarClassName' }
                , { label: '详细说明', name: 'DetailRemark', index: 'DetailRemark', sortable: false }
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

                            myJqGrid.setFormFieldReadOnly('CarClass.ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: 'CarClass',
                        afterFormLoaded: function () {

                            myJqGrid.setFormFieldReadOnly('CarClass.ID', true);
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

    var myItemGrid = new MyGrid({
        renderTo: 'MyItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: storeUrl1
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , storeCondition: '1<>1'
		    , initArray: [
                { label: '子表编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '车种编号', name: 'CarClassID', index: 'CarClassID' }
                , { label: '车种名称', name: 'CarClassName', index: 'CarClass.CarClassName' }
                , { label: '轮胎位置', name: 'TyrPlace', index: 'TyrPlace' }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myItemGrid.handleAdd({
                        loadFrom: 'MyItemFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {

                            myItemGrid.setFormFieldReadOnly('CarClassName', false);
                            myItemGrid.setFormFieldDisabled('CarClassItem.CarClassID', false);
                            $("input[name='CarClassName'] ~ button")[0].disabled = false;
                            var record = myJqGrid.getSelectedRecord();
                            if (myJqGrid.isSelectedOnlyOne()) {
                              
                                myItemGrid.setFormFieldValue("CarClassName", record["CarClassName"]);
                                myItemGrid.setFormFieldValue("CarClassItem.CarClassID", record["ID"]);
                            }

                        }
                    });
                },
                handleEdit: function (btn) {
                    myItemGrid.handleEdit({
                        loadFrom: 'MyItemFormDiv',
                        btn: btn,
                        prefix: 'CarClassItem',
                        afterFormLoaded: function () {
                            var record = myItemGrid.getSelectedRecord();
                            myItemGrid.setFormFieldReadOnly('CarClassName', true);
                            myItemGrid.setFormFieldDisabled('CarClassItem.CarClassID', true);
                            $("input[name='CarClassName'] ~ button")[0].disabled = true;
                            if (myItemGrid.isSelectedOnlyOne()) {
                                myItemGrid.setFormFieldValue("CarClassName", record["CarClassName"]);
                                myItemGrid.setFormFieldValue("CarClassItem.CarClassID", record["CarClassID"]);
                            }
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        myItemGrid.refreshGrid("CarClassID='" + id + "'");
    });

}