function goodscheckIndexInit(opts) {
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
                { label: '盘点编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '物资编号', name: 'GoodsID', index: 'GoodsID', hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsInfo.GoodsName', width: 100 }
                 , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 100 }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsInfo.GoodsTypeName', width: 100 }
                , { label: '盘点人', name: 'CheckMan', index: 'CheckMan', width: 100 }
                , { label: '账面数量', name: 'SystemNum', index: 'SystemNum', align: 'right', width: 70 }
                , { label: '盘点数量', name: 'CheckNum', index: 'CheckNum', align: 'right', width: 70 }
                , { label: '盈亏数量', name: 'ProfitAndLoss', index: 'ProfitAndLoss', align: 'right', width: 70 }
                , { label: '盘点时间', name: 'CheckTime', index: 'CheckTime', width: 130, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
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
                            GoodsInfoChange();
                            CalcCheck();
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            GoodsInfoChange();
                            CalcCheck();
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
    //选择物资获取账面数量
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
                        myJqGrid.getFormField("SystemNum").val(response.Data == null ? "" : rData.ContentNum);
                    }
                });
            }
        });

    }

    //根据盘点数量计算盘盈亏
    CalcCheck = function () {
        var GoodsField = myJqGrid.getFormField("CheckNum");
        GoodsField.unbind('blur');
        GoodsField.bind('blur', function (event) {
            var checknum = GoodsField.val();

            if (!isEmpty(checknum)) {
                var systemnum = myJqGrid.getFormField("SystemNum").val();//账面数量
                var ploss = (checknum * 1) - (systemnum * 1);//盘盈亏
                myJqGrid.getFormField("ProfitAndLoss").val(ploss == null ? "" : ploss);
            }
        });

        var GoodsField2 = myJqGrid.getFormField("SystemNum");
        GoodsField2.unbind('change');
        GoodsField2.bind('change', function (event) {
            var systemnum = GoodsField2.val();

            if (!isEmpty(systemnum)) {
                var checknum = myJqGrid.getFormField("CheckNum").val(); //账面数量
                var ploss = (checknum * 1) - (systemnum * 1); //盘盈亏
                myJqGrid.getFormField("ProfitAndLoss").val(ploss == null ? "" : ploss);
            }
        });
    }
}