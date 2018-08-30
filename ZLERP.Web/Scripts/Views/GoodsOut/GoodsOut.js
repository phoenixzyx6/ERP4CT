//物资领用
function goodsoutIndexInit(opts) {

        function IsRFmt(cellvalue, options, rowObject) {
            if (cellvalue == '0') {
                var style = "color:black;";
                if (typeof (options.colModel.formatterStyle) != "undefined") {
                    var txt = options.colModel.formatterStyle[1];
                } else {
                    var txt = "未同步";
                }
            }
            else if (cellvalue == '1') {
                var style = "color:blue;";
                if (typeof (options.colModel.formatterStyle) != "undefined") {
                    var txt = options.colModel.formatterStyle[1];
                } else {
                    var txt = "车辆";
                }
            }
            else if (cellvalue == '2') {
                var style = "color:red;";
                if (typeof (options.colModel.formatterStyle) != "undefined") {
                    var txt = options.colModel.formatterStyle[1];
                } else {
                    var txt = "设备";
                }
            }
            else if (cellvalue == '3') {
                var style = "color:green;";
                if (typeof (options.colModel.formatterStyle) != "undefined") {
                    var txt = options.colModel.formatterStyle[1];
                } else {
                    var txt = "车辆、设备";
                }
            }
            else {
                var txt = '';
            }

            return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
        }
        function IsRUnFmt(cellvalue, options, cell) {
            return $('span', cell).attr('rel');
        }

        var myJqGrid = new MyGrid({
            renderTo: 'MyJqGrid'
            //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , dialogWidth: 480
            , dialogHeight: 390
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , advancedSearch: true
		    , initArray: [
                { label: '领用编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '物资编号', name: 'GoodsID', index: 'GoodsID', hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsInfo.GoodsName', width: 100 }
                , { label: '物资型号', name: 'GoodsModel', index: 'GoodsInfo.GoodsModel', width: 100 }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsInfo.GoodsTypeName', width: 100 }
                , { label: '同步', name: 'IsR', index: 'IsR', width: 100, formatter: IsRFmt, unformat: IsRUnFmt }
                , { label: '领用部门编号', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
                , { label: '领用部门', name: 'DepartmentName', index: 'DepartmentID', width: 100 }
                , { label: '领用人', name: 'OutMan', index: 'OutMan', width: 100 }
                , { label: '领用单价', name: 'price', index: 'price', align: 'center', width: 100 }
                , { label: '领用数量', name: 'OutNum', index: 'OutNum', align: 'center', width: 100 }
                , { label: '领用时间', name: 'OutTime', index: 'OutTime', width: 130, formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '领用原因', name: 'OutReason', index: 'OutReason', width: 200 }
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
                            $('#id1').css('display', 'block');
                            $('#id2').css('display', 'block');
                            myJqGrid.setFormFieldReadOnly('ContentNum', true);
                            GoodsInfoChange();
                            GoodsPriceChange();

                        },
                        beforeSubmit: function () {

                            if (IsExistGoods()) {

                                var contentNum = myJqGrid.getFormFieldValue('ContentNum');
                                contentNum = isEmpty(contentNum) ? 0 : parseFloat(contentNum);

                                var outNum = myJqGrid.getFormFieldValue('OutNum');
                                inNum = isEmpty(outNum) ? 0 : parseFloat(outNum);

                                if (outNum > contentNum) {
                                    showError('错误', '领用数量不能大于库存数量，请重新输入!');
                                    myJqGrid.getFormField('OutNum').addClass('input-validation-error');
                                    return false;
                                }

                                return true;
                            } else {
                                return false;
                            }

                        },
                        postCallBack: function (response) {
                            if (response && response.Result) {
                                //同步到对应的里面
                                var me = $("input[name='ME']").attr('checked');
                                var mc = $("input[name='MC']").attr('checked');
                                var carID = $("#carID").val();
                                var name = $("#lingyongren").val();
                                if ((me == true || me == "true") || (mc == true || mc == "true")) {
                                    showConfirm('提示', '是否同步？', function () {
                                        ajaxRequest("/GoodsOut.mvc/AddM", { outID: response.Data, MC: mc, ME: me, carid: carID, name: name }, true, function (response) {
                                            myJqGrid.reloadGrid();
                                            if (response.Result) {

                                            }
                                        });
                                        $(this).dialog("close");

                                    });
                                }

                                showConfirm('提示', '是否打印物资领用单？', function () {
                                    printGoodsOutDoc('preview', response.Data);
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
                        btn: btn,
                        afterFormLoaded: function () {

                            //document.getElementById("price").value = "10";
                            //var a = document.getElementById("price").value;
                            //                            document.getElementById("DepartmentID").value = "hi";
                            $('#id1').css('display', 'none');
                            $('#id2').css('display', 'none');
                        }
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

                    printGoodsOutDoc('preview', myJqGrid.getSelectedKey());
                }
                , handleDesign: function (btn) {
                    var goodsOutId = myJqGrid.getSelectedKey();

                    //如有选中一条入库记录，则使用选中的入库记录设计，否则使用测试数据设计
                    if (!isEmpty(goodsOutId)) {
                        printGoodsOutDoc('design', goodsOutId);
                    }
                    else {
                        GoodsOutRepDesign();
                    }
                }
            }
        });

        //        $('#uprice')[0].disabled = true;
        //        $(function () {
        //            //文本更改
        //            $("input[name='GoodsID']").blur(function () {
        //                var str = $(this).val();
        //                if (!isEmpty(str)) {
        //                    str = str.substring(str.indexOf('[') + 1, str.indexOf(']'));
        //                    ajaxRequest("/GoodsIn.mvc/GetPrice", { id: str }, false, function (response) {
        //                        if (response.Result) {
        //                            $('#uprice').val(response.Data);
        //                        }
        //                    });
        //                }
        //            });
        //        }) 

        //    GoodsInfoChange = function () {
        //        var GoodsField = myJqGrid.getFormField("GoodsID");
        //        GoodsField.unbind('change');
        //        GoodsField.bind('change', function (event) {
        //            var goods = GoodsField.val();

        //            if (!isEmpty(goods)) {
        //                var requestURL = opts.getGoodsUrl;
        //                var postParams = { id: goods };
        //                var rData;
        //                ajaxRequest(requestURL, postParams, false, function (response) {
        //                    if (response) {
        //                        rData = response.Data;
        //                        myJqGrid.getFormField("ContentNum").val(response.Data == null ? "" : rData.ContentNum);
        //                    }
        //                });
        //            }
        //        });
        //    }

        GoodsInfoChange = function () {

            var GoodsField = myJqGrid.getFormField("GoodsID");
            GoodsField.unbind('change');
            GoodsField.bind('change', function (event) {
                if (!IsExistGoods()) {
                    return;
                }
                var goods = GoodsField.val();
                bindSelectData(myJqGrid.getFormField("price"),
                       opts.getprice,
                       { textField: 'InPrice',
                           valueField: 'InPrice',
                           condition: " GoodsID='" + goods + "' and InPrice is not null"
                       });


            });
        }
               GoodsInfoBlur = function () {
                   
                   var GoodsField = myJqGrid.getFormField("GoodsID");
                   GoodsField.unbind('change');
                   GoodsField.bind('change', function (event) {
                       if (!IsExistGoods()) {
                           return;
                       }

                   });
               }

    //单价
    GoodsPriceChange = function () {
        
        var GoodsPriceField = myJqGrid.getFormField("price");
        GoodsPriceField.unbind('change');
        GoodsPriceField.bind('change', function (event) {
            var price = GoodsPriceField.val();
            //查库存
            if (!isEmpty(price)) {
                var goodsid = myJqGrid.getFormField("GoodsID").val();
                var requestURL = opts.getNumPrice;
                var postParams = { id: goodsid, price: price };
                var rData;
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        rData = response.Data;
                        myJqGrid.getFormField("ContentNum").val(response.Data == null ? "" : rData);
                    }
                });
            }
        });
    }

    //判断是否物资信息存在此物品
    IsExistGoods = function () {

        var ischeck = false;
        var str = $("input[name='GoodsID']").val();
        $.ajax({
            url: "/GoodsInfo.mvc/Get",
            type: 'POST',
            json: 'json',
            async: false, //必须是同步
            data: { id: str },
            traditional: true,
            success: function (response) {
                if (response.Result) {
                    ischeck = true;
                }
                else {
                    ischeck = false;
                }
            },
            error: function (response) {
                ischeck = false;
            }
        });

        if (ischeck) {
            return true;
        }
        else {
            showError('提示', "请选择物资信息里面存在的物品,不能手动输入!");
            return false;
        }
    }
}
    
