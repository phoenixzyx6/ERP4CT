function goodsrevertIndexInit(opts) {
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
                { label: '归还编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '物资编号', name: 'GoodsID', index: 'GoodsID', hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsInfo.GoodsName', width: 100 }
                , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 100 }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsInfo.GoodsTypeName', width: 100 }
                , { label: '归还人', name: 'RevertMan', index: 'RevertMan', width: 100 }
                , { label: '归还数量', name: 'RevertNum', index: 'RevertNum', align: 'right', width: 100 }
                , { label: '归还时间', name: 'RevertTime', index: 'RevertTime', width: 130, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '归还原因', name: 'RevertReason', index: 'RevertReason', width: 200 }
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
                            myJqGrid.setFormFieldReadOnly('BorrowNum', true);
                            GoodsInfoChange();
                        },
                        beforeSubmit: function () {
                            var borrowNum = myJqGrid.getFormFieldValue('BorrowNum');
                            borrowNum = isEmpty(borrowNum) ? 0 : parseFloat(borrowNum);

                            var revertNum = myJqGrid.getFormFieldValue('RevertNum');
                            revertNum = isEmpty(revertNum) ? 0 : parseFloat(revertNum);

                            if (revertNum > borrowNum) {
                                showError('错误', '归还数量不能大于借用数量，请重新输入!');
                                myJqGrid.getFormField('RevertNum').addClass('input-validation-error');
                                return false;
                            }

                            return true;
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
                        myJqGrid.getFormField("BorrowNum").val(response.Data == null ? "" : rData.BorrowNum - rData.RevertNum);
                    }
                });
            }
        });
    }
}