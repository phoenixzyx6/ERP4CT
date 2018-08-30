function goodsborrowIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , dialogWidth: 480
            , dialogHeight: 320
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '借用编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '物资编号', name: 'GoodsID', index: 'GoodsID', hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsInfo.GoodsName', width: 100 }
                , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 100 }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsInfo.GoodsTypeName', width: 100 }
                , { label: '借用部门编号', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
                , { label: '借用部门', name: 'DepartmentName', index: 'DepartmentID', width: 100 }
                , { label: '借用人', name: 'BorrowMan', index: 'BorrowMan', width: 100 }
                , { label: '借用数量', name: 'BorrowNum', index: 'BorrowNum', align: 'right', width: 100 }
                , { label: '借用时间', name: 'BorrowTime', index: 'BorrowTime', width: 130, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '借用原因', name: 'BorrowReason', index: 'BorrowReason', width: 200 }
                , { label: '经办人', name: 'Operator', index: 'Operator', width: 100 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
                 , { label: '创建人', name: 'Builder', index: 'Builder', width: 100 }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 100 }
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
                            myJqGrid.setFormFieldReadOnly('ContentNum', true);
                            GoodsInfoChange();
                        },
                        beforeSubmit: function () {
                            var contentNum = myJqGrid.getFormFieldValue('ContentNum');
                            contentNum = isEmpty(contentNum) ? 0 : parseFloat(contentNum);

                            var borrowNum = myJqGrid.getFormFieldValue('BorrowNum');
                            borrowNum = isEmpty(borrowNum) ? 0 : parseFloat(borrowNum);

                            if (borrowNum > contentNum) {
                                showError('错误', '借用数量不能大于库存数量，请重新输入!');
                                myJqGrid.getFormField('BorrowNum').addClass('input-validation-error');
                                return false;
                            }

                            return true;
                        },
                        postCallBack: function (response) {
                            if (response && response.Result) {
                                showConfirm('提示', '是否打印物资领用单？', function () {
                                    printGoodsBorrowDoc('preview', response.Data);
                                    $(this).dialog("close");
                                });
                            }
                            else {
                                showError(response.Message);
                            }
                        }
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
                , handlePrint: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行打印！");
                        return false;
                    }

                    printGoodsBorrowDoc('preview', myJqGrid.getSelectedKey());
                }
                , handleDesign: function (btn) {
                    var goodsBorrowId = myJqGrid.getSelectedKey();

                    //如有选中一条入库记录，则使用选中的入库记录设计，否则使用测试数据设计
                    if (!isEmpty(goodsBorrowId)) {
                        printGoodsOutDoc('design', goodsBorrowId);
                    }
                    else {
                        GoodsBorrowRepDesign();
                    }
                }
            }
    });

    GoodsInfoChange = function () {
        var GoodsField = myJqGrid.getFormField("GoodsID");
        GoodsField.unbind('change');
        GoodsField.bind('change', function (event) {
            var goods = GoodsField.val();

            if (!isEmpty(goods)) {
                var requestURL = opts.getGoodsUrl;
                var postParams = { id: goods };
                var rData;
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        rData = response.Data;
                        myJqGrid.getFormField("ContentNum").val(response.Data == null ? "" : rData.ContentNum);
                    }
                });
            }
        });
    }
}