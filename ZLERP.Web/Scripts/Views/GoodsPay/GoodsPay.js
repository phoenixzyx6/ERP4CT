
function goodspayIndexInit(storeUrl) {
    //-------------------------------------------------左边grid----------------------------------------------------
    var myJqGridSupply = new MyGrid({
        renderTo: 'MyJqGridSupply'
        //, width: '100%'
            ,title:"供应商列表"
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: "/SupplyInfo.mvc/Find"
            , storeCondition: "IsUsed =1"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 300
            , dialogHeight: 250
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '供应商简称', name: 'ShortName', index: 'ShortName',width:160 }
                , { label: '供应商名称', name: 'SupplyName', index: 'SupplyName', width: 160 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGridSupply.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGridSupply.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!myJqGridSupply.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = myJqGridSupply.getSelectedRecord();
                    myJqGridSupply.handleAdd({
                        width: 400,
                        height: 300,
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGridSupply.setFormFieldValue("SupplyID", selectrecord.ID);
                            $("#SupplyName").html(selectrecord.SupplyName);
                        }
                        , postCallBack: function (response) {
                            myJqGrid.refreshGrid("SupplyID='" + selectrecord.ID + "'");
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGridSupply.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    myJqGridSupply.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleInvoice: function (btn) {//录入发票记录
                    if (!myJqGridSupply.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = myJqGridSupply.getSelectedRecord();
                    myJqGridSupply.handleAdd({
                        width: 400,
                        height: 300,
                        loadFrom: 'MyFormDivInvoice',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGridSupply.setFormFieldValue("SupplyID", selectrecord.ID);
                            $("#SupplyName2").html(selectrecord.SupplyName);
                        }
                        , postCallBack: function (response) {
                            MyJqGridInvoice.refreshGrid("SupplyID='" + selectrecord.ID + "'");
                        }
                    });
                }
            }
        });
        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        myJqGridSupply.addListeners("rowclick", function (id, record) {
            myJqGrid.refreshGrid("SupplyID='" + id + "'");
            MyJqGridInvoice.refreshGrid("SupplyID='" + id + "'");
        });


        //---------------------------------------------------右上边GRID-------------------------------------------------------
        var myJqGrid = new MyGrid({
            renderTo: 'MyJqGrid'
            //, width: '100%'
            , title: "付款记录"
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight / 2-50
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'act'
            , editSaveUrl: "/GoodsPay.mvc/Update"
		    , initArray: [
                { label: '明细编号', name: 'ID', index: 'ID',hidden:true }
                , { label: '供应商编号', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: '操作', name: 'act', index: 'act', width: 60, sortable: false, align: "center" }
                , { label: '付款时间', name: 'PayDate', index: 'PayDate', width: 60, formatter: "date", editable: true, align: "center", sorttype: "datetime" }
                , { label: "付款金额", name: "PayMoney", index: "PayMoney", width: 80, formatter: "integer", editable: true, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                , { label: "付款方式", name: "PayType", index: "PayType", width: 80, editable: true, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "ContractPayType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("ContractPayType") }, edittype: 'select', editoptions: { value: getDicPayTypeStr()} }
                , { label: "经办人", name: "Manager", index: "Manager", width: 80, align: "center", editable: true }
                , { label: '备注', name: 'Remark', index: 'Remark', editable: true, width: 200 }
		    ]
		    , autoLoad: false
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
                        btn: btn
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
        myJqGrid.addListeners("gridComplete", function () {

            var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeletePay(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                myJqGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

            }

        });
        //cellclick 事件定义，选择日期列单元格弹出日期选择控件 (lzl add 2016-07-26)
        myJqGrid.addListeners("cellclick", function (id, record) {
            var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
            $("#" + rowno + "_PayDate").datepicker();
        });
        //获取付款方式列表 lzl add 2016-07-28
        function getDicPayTypeStr() {
            //动态生成select内容
            var requestURL = "/Dic.mvc/FindDicsList";
            var postParams = { nodeid: "ContractPayType" };
            var str = "";

            $.ajax({
                type: 'post',
                async: false,
                url: requestURL,
                data: postParams,
                success: function (data) {
                    if (data != null) {
                        var jsonobj = eval(data);
                        var length = jsonobj.length;
                        for (var i = 0; i < length; i++) {
                            if (i != length - 1) {
                                str += jsonobj[i].ID + ":" + jsonobj[i].DicName + ";";
                            } else {
                                str += jsonobj[i].ID + ":" + jsonobj[i].DicName;
                            }
                        }
                    }
                    //return str;
                }
            });
            return str;
        }

        window.handleDeletePay = function (id) {
            showConfirm("确认信息", "您确定删除此项付款记录？", function () {
                $.post(
                    "/GoodsPay.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        myJqGrid.reloadGrid();
                    }

                );
                $(this).dialog("close");
            });
        };

        //---------------------------------------------------右下边GRID------------------------------------------------

        $("#InvoiceDate").datepicker();
        var MyJqGridInvoice = new MyGrid({
            renderTo: 'MyJqGridInvoice'
            //, width: '100%'
            , title: "发票记录"
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight / 2 - 50
		    , storeURL: "/GoodsInvoice.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupingView: { groupSummary: [true], groupText: ['笔数({1})'] }
            , groupField: 'act'
            , editSaveUrl: "/GoodsInvoice.mvc/Update"
		    , initArray: [
                { label: '明细编号', name: 'ID', index: 'ID',hidden:true }
                , { label: '供应商编号', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: '操作', name: 'act', index: 'act', width: 60, sortable: false, align: "center" }
                , { label: '开票时间', name: 'InvoiceDate', index: 'InvoiceDate', width: 60, formatter: "date", editable: true, align: "center", sorttype: "datetime" }
                , { label: "开票金额", name: "InvoiceMoney", index: "InvoiceMoney", width: 80, formatter: "integer", editable: true, align: "center", summaryType: 'sum', summaryTpl: '{0}' }
                , { label: "经办人", name: "Manager", index: "Manager", width: 80, align: "center", editable: true }
                , { label: "物资类别", name: "GoodsTypeId", index: "GoodsTypeId", width: 80, align: "center", editable: false }
                , { label: '备注', name: 'Meno', index: 'Meno', editable: true, width: 200 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    MyJqGridInvoice.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MyJqGridInvoice.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    MyJqGridInvoice.handleAdd({
                        loadFrom: 'MyFormDivInvoice',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    MyJqGridInvoice.handleEdit({
                        loadFrom: 'MyFormDivInvoice',
                        btn: btn,
                        width:350,
                        height:300
                    });
                }
                , handleDelete: function (btn) {
                    MyJqGridInvoice.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        //cellclick 事件定义，选择日期列单元格弹出日期选择控件 (lzl add 2016-12-19)
        MyJqGridInvoice.addListeners("cellclick", function (id, record) {
            var rowno = $('#' + id)[0].rowIndex; //根据ID获取行号
            $("#" + rowno + "_InvoiceDate").datepicker();
        });
        MyJqGridInvoice.addListeners("gridComplete", function () {

            var ids = MyJqGridInvoice.getJqGrid().jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteInvoice(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                MyJqGridInvoice.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

            }

        });
        window.handleDeleteInvoice = function (id) {
            showConfirm("确认信息", "您确定删除此项记录？", function () {
                $.post(
                    "/GoodsInvoice.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        MyJqGridInvoice.reloadGrid();
                    }

                );
                $(this).dialog("close");
            });
        };
    }
