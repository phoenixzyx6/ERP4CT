function goodsinIndexInit(storeUrl) {

    var myJqGrid1 = new MyGrid({
        renderTo: 'DocHisGrid'
            , width: 720
            , height: 240
            , storeURL: storeUrl.DocHisUrl
            , sortByField: 'BuildTime'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , advancedSearch:true
            , emptyrecords: '当前无任何修改'
		    , initArray: [
                { label: '入库编号', name: 'GoodsInIDHistory', index: 'GoodsInIDHistory', width: 100 }
                , { label: '标识', name: 'action_u', index: 'action_u', width: 80 }
                , { label: '物资编号', name: 'GoodsID', index: 'GoodsID', hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsInfo.GoodsName', width: 150 }
                , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 100 }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsInfo.GoodsTypeName', width: 100 }
                , { label: '入库数量', name: 'InNum', index: 'InNum', align: 'right', width: 80 }
                , { label: '实际单价', name: 'InPrice', index: 'InPrice', width: 80 }                
                , { label: '经办人', name: 'Operator', index: 'Operator', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 120 }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
		    ]
		    , autoLoad: false
    });

    //查看运输单对应的历史记录
    function showDocHis(b) {
        var title = "物资入库历史记录";
        $("#DocHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
            close: function (event, ui) {
                $(this).dialog("destroy");
                myJqGrid1.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
            }
        });
        $('#DocHisWindow').dialog('open');
        myJqGrid1.refreshGrid();
    }

//    $('#uprice')[0].disabled = true;
//    $(function () {
//        //文本更改
//        $("input[name='GoodsID']").blur(function () {
//            var str = $(this).val();
//            if (!isEmpty(str)) {
//                str = str.substring(str.indexOf('[') + 1, str.indexOf(']'));
//                ajaxRequest("/GoodsIn.mvc/GetPrice", { id: str }, false, function (response) {
//                    if (response.Result) {
//                        $('#uprice').val(response.Data);
//                    }
//                });
//            }
//        });
//    })

//    function GoodsChange() {
//        //$("input[name='GoodsID']")
//        var GoodsIDField = myJqGrid.getFormField("GoodsID");
//        GoodsIDField.unbind('change');
//        GoodsIDField.bind('change', function () {
//            $('#purchaseContract').empty();
//            $('#InNum').val();
//            $('#InPrice').val();
//            $('#SupplyName').val();
//            myJqGrid.getFormField("purchaseContract").val('');
//            var Goodsid = GoodsIDField.val();
//            if (!isEmpty(Goodsid)) {
//                bindSelectData($('#purchaseContract'),
//                    '/PurchaseContract.mvc/ListData',
//                    { textField: 'PurchaseContract_Name',
//                        valueField: 'ID',
//                        condition: "GoodsID='" + Goodsid + "' and PurchaseContract_state = 0 "
//                    }
//                    , function (data) {
//                        var purchaseContractFiel = myJqGrid.getFormField("purchaseContract");
//                        purchaseContractFiel.unbind('change');
//                        purchaseContractFiel.bind('change', function () {
//                            var conID = $('#purchaseContract').val();
//                            ajaxRequest('/PurchaseContract.mvc/Get', { id: conID }, false, function (response) {
//                                if (response) {
//                                    $('#InNum').val(response.Data.PurchaseContract_Num);
//                                    $('#InPrice').val(response.Data.PurchaseContract_Price);
//                                    $('#SupplyName').val(response.Data.PurchaseContract_Supply);
//                                }
//                            })
//                        });
//                    }
//                    )
//            }
//        });
//    }

//                    ,function (data) {
//                        var count = data.length;
//                        if (count > 0) {
//                            $('#ProjectName').children().first().remove(); //施工单位清掉

//                            //获取加
//                            ajaxRequest('/Project.mvc/GetProjectName', { id: contractId }, false, function (response) {
//                                if (response) {
//                                    var name = response.Name;
//                                    $('[name="ProjectName"]').val(name);
//                                }
//                            })
//            }

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 480
            , dialogHeight: 320
		    , storeURL: storeUrl.storeUrl
		    , sortByField: 'InTime'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , advancedSearch: true
           // , groupField: 'GoodsName'
           // , groupingView: { groupSummary: [true] }
		    , initArray: [
                { label: '入库编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '物资编号', name: 'GoodsID', index: 'GoodsID', hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsInfo.GoodsName', width: 100 }
                , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 100 }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsInfo.GoodsTypeName', width: 100 }
                , { label: '供应商', name: 'SupplyName', index: 'SupplyName', width: 100 }
                , { label: '运输商', name: 'TransportName', index: 'TransportName', width: 100 }
                , { label: '入库数量', name: 'InNum', index: 'InNum', align: 'right', width: 80 }
                , { label: '均价', name: 'uPrice', index: 'GoodsInfo.uPrice', width: 100 }
                , { label: '入库总价', name: 'tPrice', width: 100, summaryType: 'sum', summaryTpl: '小计:{0}' }
                , { label: '实际单价', name: 'InPrice', index: 'InPrice', width: 80 }
                , { label: '入库时间', name: 'InTime', index: 'InTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '经办人', name: 'Operator', index: 'Operator', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 120 }
                 , { label: '创建人', name: 'Builder', index: 'Builder', width: 80 }
                 , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', width: 80 }
		    ]
		    , autoLoad: true
            , functions: {
                //历史物资入库
                HandleGoodsInHistory: function (btn) {
                    showDocHis(btn);
                },
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
                        postCallBack: function (response) {
                            if (response && response.Result) {
                                showConfirm('提示', '是否打印物资入库单？', function () {
                                    printGoodsInDoc('preview', response.Data);
                                    $(this).dialog("close");
                                });
                            }
                            else {
                                showError(response.Message);
                            }
                        }

                        , afterFormLoaded: function () {
//                            myJqGrid.setFormFieldDisabled('InTime', true);
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
                //打印
                , handlePrint: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录进行打印！");
                        return false;
                    }

                    printGoodsInDoc('preview', myJqGrid.getSelectedKey());
                }
                //设计
                , handleDesign: function (btn) {
                    var goodsInId = myJqGrid.getSelectedKey();

                    //如有选中一条入库记录，则使用选中的入库记录设计，否则使用测试数据设计
                    if (!isEmpty(goodsInId)) {
                        printGoodsInDoc('design', goodsInId);
                    }
                    else {
                        GoodsInRepDesign();
                    }
                }
            }
    });

}