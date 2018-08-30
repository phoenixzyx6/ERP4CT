function partborrowreturnIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
            , dialogWidth: 350
            , dialogHeight: 280
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '借用编号', name: 'BorrowID', index: 'BorrowID' }
                , { label: '配件名称', name: 'PartName', index: 'PartName' }
                 , { label: '配件ID', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '归还日期', name: 'ReturnDate', index: 'ReturnDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '归还数量', name: 'ReturnNum', index: 'ReturnNum' }
                , { label: '归还人', name: 'Returner', index: 'Returner' }
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

                            var BorrowIDField = myJqGrid.getFormField("BorrowID");
                            var PartIDField = myJqGrid.getFormField("PartID");
                            //mygrid.getFormField("AuditStatus").val(0);
                            PartIDField.unbind('change');
                            PartIDField.bind('change', function () {
                                var PartIDField = $(this).val();
                                if (PartIDField) {

                                    bindSelectData(BorrowIDField,
                                        options.borrowListUrl,
                                        { textField: 'ID',
                                            valueField: 'BorrowID',
                                            condition: "PartID='" + PartIDField + "'"
                                        }
                                    );

                                }
                                else {
                                    contractIdField.empty();
                                }
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyEditFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            var BorrowIDField = myJqGrid.getFormField("BorrowID");
                            var PartIDField = myJqGrid.getFormField("PartID");
                            myJqGrid.setFormFieldDisabled('PartID', true);
                            myJqGrid.setFormFieldReadOnly('BorrowID', true);
                            //myJqGrid.setFormFieldReadOnly('ReturnNum', true);
                            var record = myJqGrid.getSelectedRecord();
                            myJqGrid.setFormFieldValue("BorrowID", record["BorrowID"]);
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