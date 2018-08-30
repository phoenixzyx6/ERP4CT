function partborrowIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
            , dialogWidth: 550
            , dialogHeight: 220
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID',width:'100' }
                , { label: '借用日期', name: 'BorrowDate', index: 'BorrowDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '借用天数', name: 'Days', index: 'Days', width: '100' }
                , { label: '借用人', name: 'Borrower', jsonmap: 'BorrowerUser.TrueName', index: 'Borrower', sortable: false, width: '100' }
                 , { label: '借用人', name: 'Borrower', index: 'Borrower', hidden: true }
                , { label: '借用部门', name: 'BorrowerDepartment.DepartmentName', jsonmap: 'BorrowerDepartment.DepartmentName', index: 'DepartmentID', width: '150' }
                , { label: '借用部门', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
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
                        prefix: 'PartBorrow',
                        btn: btn,
                        afterFormLoaded: function () {
                            var DepartmentIDField = myJqGrid.getFormField("PartBorrow.DepartmentID");
                            var BorrowerField = myJqGrid.getFormField("PartBorrow.Borrower");
                            DepartmentIDField.unbind('change');
                            DepartmentIDField.bind('change', function () {
                                var DepartmentID = $(this).val();
                                if (DepartmentID) {
                                    bindSelectData(BorrowerField,
                                        options.listUserUrl,
                                        { textField: 'TrueName',
                                            valueField: 'ID',
                                            condition: "DepartmentID='" + DepartmentID + "' and IsUsed=1"
                                        }
                                    );
                                }
                                else {
                                    BorrowerField.empty();
                                }
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        prefix: 'PartBorrow',
                        btn: btn,
                        afterFormLoaded: function () {
                            var DepartmentIDField = myJqGrid.getFormField("PartBorrow.DepartmentID");
                            var BorrowerField = myJqGrid.getFormField("PartBorrow.Borrower");
                            DepartmentIDField.unbind('change');
                            DepartmentIDField.bind('change', function () {
                                var DepartmentID = $(this).val();
                                if (DepartmentID) {
                                    bindSelectData(BorrowerField,
                                        options.listUserUrl,
                                        { textField: 'TrueName',
                                            valueField: 'ID',
                                            condition: "DepartmentID='" + DepartmentID + "' and IsUsed=1"
                                        }
                                    );
                                }
                                else {
                                    BorrowerField.empty();
                                }
                            });
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

    myJqGrid.addListeners('rowclick', function () {
        var partBorrowId = myJqGrid.getSelectedKey();
        itemGrid.refreshGrid("BorrowID='" + partBorrowId + "'");
    });

    //明细itemGrid
    var itemGrid = new MyGrid({
        renderTo: 'itemGrid'
        // , title: '工具借用明细'
        //, width: '100%'
            , autoLoad: false
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: options.partBorrowItemUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 350
            , dialogHeight: 200
		    , showPageBar: true
            , editSaveUrl: options.updatPortBorrwItemItem
		    , initArray: [
                    { label: '序号', name: 'ID', index: 'ID',width:'80' }
                , { label: '借用编号', name: 'BorrowID', index: 'BorrowID', hidden: true }
                , { label: '工具名称', name: 'PartName', index: 'PartName', width: '150' }
                , { label: '工具名称', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '借用数量', name: 'BorrowNum', index: 'BorrowNum', width: '80' }
                , { label: '归还数量', name: 'ReturnNum', index: 'ReturnNum', width: '80' }
		    ]
            , functions: {
                handleReload: function (btn) {
                    itemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    itemGrid.refreshGrid();
                },

                handleEdit: function (btn) {
                    itemGrid.handleEdit({
                        loadFrom: 'itemFormDiv',
                        prefix: 'PartBorrowItem',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    itemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //增加明细
                , handleItemAdd: function (btn) {
                    if (myJqGrid.isSelectedOnlyOne('请选择左边一条记录！')) {
                        var id = myJqGrid.getSelectedKey();
                        itemGrid.handleAdd({
                            loadFrom: 'itemFormDiv',
                            afterFormLoaded: function () {
                                itemGrid.setFormFieldValue("PartBorrowItem.BorrowID", id);
                            },
                            btn: btn
                        });
                    }
                }
            }
    });



}