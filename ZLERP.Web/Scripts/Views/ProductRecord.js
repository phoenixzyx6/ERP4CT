function productrecordIndexInit(storeUrl1, storeUrl2, storeUrl3, storeUrl4, storeUrl5) {
    var d = new Date();
    var today = d.format("Y-m-d");
    var condition = "ProduceDate between '" + today + " 00:00:00' and '" + today + " 23:59:59' and isProduce=1";
    var shippDocGrid = new MyGrid({
        renderTo: 'shippDocGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl1
            , storeCondition: condition
		    , sortByField: 'ProduceDate'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 700
            , dialogHeight: 400
            , advancedSearch: true
		    , initArray: [
                { label: '运输单号', name: 'ID', index: 'ID', editrules: { required: true }, width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '客户名称', name: 'CustName', index: 'CustName', width: 150 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', searchoptions: { sopt: ['cn']} }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 50 }
                , { label: '切换状态', name: 'Qstatus', index: 'Qstatus', width: 50 ,hidden:true}
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 50 }
                , { label: '生产线', name: 'ProductLineID', index: 'ProductLineID', width: 50, hidden: true }
                , { label: '生产日期', name: 'ProduceDate', index: 'ProduceDate', width: 120, formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
               // , { label: '启动生产日期', name: 'StartupTime', index: 'StartupTime', width: 120, formatter: 'datetime' }
                , { label: '调度方量', name: 'SendCube', index: 'SendCube', width: 50, search: false }
                , { label: '混凝土方量', name: 'BetonCount', index: 'BetonCount', width: 50, search: false }
                , { label: '砂浆方量', name: 'SlurryCount', index: 'SlurryCount', width: 50, search: false }
                , { label: '已生产方量', name: 'TotalProduceCube', index: 'TotalProduceCube', width: 70, align: 'right', search: false, editable: true }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 50 }
                , { label: '浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
                , { label: '施工部位', name: 'ConsPos', index: 'ConsPos', width: 150 }
                , { label: '泵名称', name: 'PumpName', index: 'PumpName', width: 60 }
                , { label: '已供方量', name: 'ProvidedCube', index: 'ProvidedCube', width: 50, search: false }
                , { label: '累计车数', name: 'ProvidedTimes', index: 'ProvidedTimes', width: 50, search: false }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, searchoptions: { sopt: ['cn']} }
                , { label: '配比编号', name: 'ConsMixpropID', index: 'ConsMixpropID', width: 60 }
                , { label: '坍落度', name: 'RealSlump', index: 'RealSlump', width: 60, search: false }             
                , { label: '司机', name: 'Driver', index: 'Driver', width: 60, search: false }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    shippDocGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    shippDocGrid.refreshGrid();
                },
                handleReport: function (btn) {
                    if (!shippDocGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }

                    var selectedRecord = shippDocGrid.getSelectedRecord();
                    window.open("/Reports/QCC/R710843.aspx?uid=" + selectedRecord.ID);
                }
                //删除
                , handleDelete: function (btn) {
                    shippDocGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , FindTodayRecord: function (btn) {
                    var d = new Date();
                    var today = d.format("Y-m-d");
                    condition = "ProduceDate between '" + today + " 00:00:00' and '" + today + " 23:59:59' and isProduce=1";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindTomRecord: function (btn) {
                    var uom = GetDateBySpan(1);
                    condition = "ProduceDate between '" + uom + " 00:00:00' and '" + uom + " 23:59:59' and isProduce=1";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindAllRecord: function (btn) {
                    condition = " BetonCount>-1 and isProduce=1";
                    shippDocGrid.refreshGrid(condition);
                }
                , FindCustRecord: function (btn) {
                    shippDocGrid.showWindow({
                        title: '选择时间范围',
                        width: 280,
                        height: 180,
                        resizable: false,
                        loadFrom: 'SelectTimeForm',
                        afterLoaded: function () {

                        },
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var BeginTime = $("#rBeginTime").val();
                                var EndTime = $("#rEndTime").val();
                                condition = "ProduceDate between '" + BeginTime + "' and '" + EndTime + "'  and isProduce=1";
                                shippDocGrid.refreshGrid(condition);
                                var checked = $("#rIsAutoClose")[0].checked;
                                if (checked) {
                                    $(this).dialog('close');
                                }
                            }
                        }
                    });
                }
                 , PRDetail: function (btn) {
                     if (!shippDocGrid.isSelectedOnlyOne()) {
                         showMessage('提示', '请选择一条记录进行操作！');
                         return;
                     }
                     var selectedRecord = shippDocGrid.getSelectedRecord();
                     window.open("/Reports/Produce/R410791.aspx?uid=" + selectedRecord.ID);
                 }

            }
    });

    shippDocGrid.addListeners("gridComplete", function () {
        var ids = shippDocGrid.getJqGrid().jqGrid('getDataIDs');
        var amount = 0;
        for (var i = 0; i < ids.length; i++) {
            var record = shippDocGrid.getRecordByKeyValue(ids[i]);
            if (record.Qstatus == 1) {
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "#00BBFF";
            }
            //if (Math.round(record.SendCube * 1)!= Math.round(record.ProductCube * 1)) {//调度方量和已生产方量不相等
            if ((record.SendCube * 1 - record.TotalProduceCube * 1 >= 0.05 || record.SendCube * 1 - record.TotalProduceCube * 1 <= -0.05) & record.TotalProduceCube!=0) {//调度方量和已生产方量不相等
                var $id = $(document.getElementById(ids[i]));
                $id.removeAttr("style");
                $id.removeClass("ui-widget-content");
                document.getElementById(ids[i]).style.backgroundColor = "red";
            }

            //amount = 0;
//            var ids2 = ProductRecordGrid.getJqGrid().jqGrid('getDataIDs');
//            for (var j = 0; j < ids2.length; j++) {
//                var record2 = ProductRecordGrid.getRecordByKeyValue(ids2[j]);
//                amount = amount*1 + record2.ProduceCube * 1;
//            }
//            if (record.SendCube*1!=amount*1)
//            {
//               document.getElementById(ids[i]).style.backgroundColor = "red";
//            }
        }

    });

    //设置日期列为日期范围搜索
    shippDocGrid.setRangeSearch("ProduceDate");
    shippDocGrid.addListeners('rowclick', function (id, record, selBool) {
        ProductRecordGrid.refreshGrid("ShipDocID='" + id + "' and ProductLineID='" + record.ProductLineID + "'");
    });
    //生产记录罐次
    var ProductRecordGrid = new MyGrid({
        renderTo: 'ProductRecordGrid'
        //, title:'生产记录罐次表'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight * 0.35
		    , storeURL: storeUrl2
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 550
            , dialogHeight: 420
            , singleSelect: true
            , rowNumbers: true
            , storeCondition: '1<>1'
		    , initArray: [
                { label: '生产记录罐数编号', name: 'ID', index: 'ID', editrules: { required: true }, hidden: true }
                , { label: '运输单号', name: 'ShipDocID', index: 'ShipDocID', hidden: true }
                , { label: '生产登记编号', name: 'DispatchID', index: 'DispatchID', width: 80, align: 'center', hidden: true }
                , { label: '当盘方量', name: 'ProduceCube', index: 'ProduceCube', width: 60, align: 'center' }
                , { label: '罐次', name: 'PotTimes', index: 'PotTimes', width: 40, align: 'center' }
                , { label: '盘容量', name: 'PCRate', index: 'PCRate', width: 50, align: 'center' }
                , { label: '电流值', name: 'ElectValue', index: 'ElectValue', width: 50, align: 'center' }
                , { label: '时间', name: 'BuildTime', index: 'BuildTime', width: 120, align: 'center', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 是否手动', name: 'IsManual', index: 'IsManual', width: 65, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
        //, { label: ' 搅拌机组编号', name: 'ProductLineID', index: 'ProductLineID',width:80 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    ProductRecordGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ProductRecordGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    ProductRecordGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    if (ProductRecordGrid.isSelectedOnlyOne('请选择一条要复制的盘次记录进行操作！')) {
                        var record = ProductRecordGrid.getSelectedRecord();
                        if (record.IsManual == "true" || record.IsManual == 1) {//只有手动复制的记录才可删除
                            ProductRecordGrid.deleteRecord({
                                deleteUrl: btn.data.Url
                                , postCallBack: function (response) {
                                    shippDocGrid.refreshGrid(condition);
                                }
                            });
                        } else {
                            showError("警告", "原始生产记录不能删除！");
                        }
                    }

                }
                , handleEdit: function (btn) {
                    var record = ProductRecordGrid.getSelectedRecord();
                    if (!isEmpty(record.DispatchID)) {
                        showError("警告", "原始生产记录不能修改！");
                        return false;
                    }
                    ProductRecordGrid.handleEdit({
                        loadFrom: 'ItemsDiv',
                        btn: btn,
                        prefix: 'ProductRecord',
                        afterFormLoaded: function () {

                            var prid = ProductRecordGrid.getFormField("ProductRecord.ID").val();
                            ProductRecordSubGrid.refreshGrid("ProductRecordID='" + prid + "'");

                            $("select[name='ProductRecord.ShipDocID']").attr("disabled", true);
                        }
                        , postCallBack: function (response) {
                            shippDocGrid.refreshGrid(condition);
                        }
                    });

                }
                , HandleCopyRecord: function (btn) {
                    if (ProductRecordGrid.isSelectedOnlyOne('请选择一条要复制的盘次记录进行操作！')) {
                        var record = ProductRecordGrid.getSelectedRecord();
                        ProductRecordGrid.showWindow({
                            title: "盘数",
                            width: 180,
                            height: 110,
                            loadFrom: 'PotTimess'
                            , buttons: {
                                "关闭": function () {
                                    ProductRecordGrid.reloadGrid();
                                    $(this).dialog("close");
                                },
                                "确定": function () {
                                    var prid = ProductRecordGrid.getSelectedKey();
                                    var ShipDocID = shippDocGrid.getSelectedKey();
                                    if (isEmpty($("#Pots").val())) {
                                        showError("错误", "盘数是必须的");
                                        return false;
                                    }
                                    ajaxRequest(storeUrl5, { id: prid, pots: $("#Pots").val() }, true, function () {
                                        ProductRecordGrid.refreshGrid("ShipDocID='" + ShipDocID + "'");
                                        $("#Pots").val("");
                                        $("#PotTimess").dialog("destroy");
                                    });

                                }
                            }
                        });

                    }
                }
                //补录生产记录
                , handleRepair: function (btn) {
                    var ShipDocID = shippDocGrid.getSelectedKey();
                    //      if (ShipDocID) {
                    ProductRecordGrid.handleAdd({
                        loadFrom: 'ItemsDiv',
                        closeDialog: false,
                        btn: btn,
                        afterFormLoaded: function () {
                            ProductRecordGrid.setFormFieldValue('ProductRecord.ShipDocID', ShipDocID);

                            $("select[name='ProductRecord.ShipDocID']").attr("disabled", false);
                        }
                        , postCallBack: function (response) {
                            if (ProductRecordGrid.getFormField("ProductRecord.ID").val().length > 0) {
                                $("#ItemsDiv").dialog('close');
                            }
                            else if (response.Result) {
                                ProductRecordGrid.setFormFieldValue('ProductRecord.ID', response.Data);
                            }

                            shippDocGrid.refreshGrid(condition);
                            var ProductRecordID = ProductRecordGrid.getFormField("ProductRecord.ID").val();
                            ProductRecordSubGrid.refreshGrid("ProductRecordID='" + ProductRecordID + "'");
                            ProductRecordItemsGrid.refreshGrid("ProductRecordID='" + ProductRecordID + "'");


                        }
                    });


                    //   }
                    //  else {
                    //     showMessage("请选择一条运输单信息");
                    //  }
                }
            }
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    ProductRecordGrid.addListeners('rowclick', function (id, record, selBool) {
        ProductRecordItemsGrid.refreshGrid("ProductRecordID='" + id + "' and (ActualAmount > 0 or TheoreticalAmount > 0)");
    });

    $("#ProductRecordItem_StuffID").bind('change', function () {
        bindSelectData($('#ProductRecordItem_SiloID'), storeUrl4, { condition: "stuffid='" + $(this).val() + "'" });
    });

    //生产记录罐次明细
    var ProductRecordItemsGrid = new MyGrid({
        renderTo: 'ProductRecordItemsGrid'
        //, title: '生产记录罐次明细表'
        , autoWidth: true
        //, buttons: buttons2
        , height: gGridHeight * 0.5
		, storeURL: storeUrl3
		, sortByField: 'ID'
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , singleSelect: true
        , rowNumbers: true
        , storeCondition: '1<>1'
		, initArray: [
            { label: '生产记录罐数明细编号', name: 'ID', index: 'ID', editrules: { required: true }, hidden: true }
            , { label: '生产记录罐数编号', name: 'ProductRecordID', index: 'ProductRecordID', editrules: { required: true }, hidden: true }
            , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
            , { label: '筒仓名称', name: 'SiloName', index: 'Silo.SiloName', width: 80 }
            , { label: '原料名称', name: 'StuffName', index: 'StuffInfo.StuffName', width: 70 }
            , { label: '原料用量', name: 'ActualAmount', index: 'ActualAmount', width: 70 }
            , { label: '配比用量', name: 'TheoreticalAmount', index: 'TheoreticalAmount', width: 70 }
            , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', width: 80, hidden: true }
            , { label: '误差率', name: 'ErrorValue', index: 'ErrorValue', width: 70 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                ProductRecordItemsGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ProductRecordItemsGrid.refreshGrid('1=1');
            }
            , handleDelete: function (btn) {
                ProductRecordItemsGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    var ProductRecordSubGrid = new MyGrid({
        renderTo: 'SubGridDiv'
        , autoWidth: true
        , buttons: buttons2
        , height: 180
		, storeURL: storeUrl3
		, sortByField: 'ID'
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , dialogWidth: 400
        , dialogHeight: 200
        , singleSelect: true
        , rowNumbers: true
        , storeCondition: '1<>1'
		, initArray: [
            { label: '生产记录罐数明细编号', name: 'ID', index: 'ID', editrules: { required: true }, hidden: true }
            , { label: '生产记录罐数编号', name: 'ProductRecordID', index: 'ProductRecordID', editrules: { required: true }, hidden: true }
            , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
            , { label: '筒仓名称', name: 'SiloName', index: 'Silo.SiloName', width: 90 }
            , { label: '原料名称', name: 'StuffName', index: 'StuffInfo.StuffName', width: 80 }
            , { label: '原料用量', name: 'ActualAmount', index: 'ActualAmount', width: 70 }
            , { label: '配比用量', name: 'TheoreticalAmount', index: 'TheoreticalAmount', width: 70 }
            , { label: '筒仓编号', name: 'SiloID', index: 'SiloID', width: 80, hidden: true }
            , { label: '误差值', name: 'ErrorValue', index: 'ErrorValue', width: 70 }
		]
		, autoLoad: false
        , functions: {
            handleReload: function (btn) {
                ProductRecordSubGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                ProductRecordSubGrid.refreshGrid('1=1');
            }
            , handleDelete: function (btn) {
                ProductRecordSubGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
            , handleImport: function (btn) {
                var prid = ProductRecordGrid.getFormField("ProductRecord.ID").val();
                var ShipDocID = ProductRecordGrid.getFormField("ProductRecord.ShipDocID").val();
                if (prid.length > 0) {
                    ajaxRequest(btn.data.Url, { id: prid }, true, function () {
                        ProductRecordGrid.refreshGrid("ShipDocID='" + ShipDocID + "'");
                        ProductRecordSubGrid.refreshGrid("ProductRecordID='" + prid + "'");                     
                    });
                }
                else {
                    showMessage("请先保存");
                }
            }
            , handleAdd: function (btn) {
                var ProductRecordID = ProductRecordGrid.getFormField("ProductRecord.ID").val();
                if (ProductRecordID.length > 0) {
                    ProductRecordSubGrid.handleAdd({
                        btn: btn,
                        loadFrom: 'SubDiv',
                        afterFormLoaded: function () {
                            ProductRecordSubGrid.setFormFieldValue('ProductRecordItem.ProductRecordID', ProductRecordID);
                        }
                    });
                }
                else {
                    showMessage("请先保存");
                }
            }
        }
    });

}