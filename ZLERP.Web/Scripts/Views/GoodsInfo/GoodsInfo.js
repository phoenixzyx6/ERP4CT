
function goodsinfoIndexInit(opts) {

//    function IsMFmt(cellvalue, options, rowObject) {
//        if (cellvalue == '0') {
//            var style = "color:green;";
//            if (typeof (options.colModel.formatterStyle) != "undefined") {
//                var txt = options.colModel.formatterStyle[1];
//            } else {
//                var txt = "正常";
//            }
//        }
//        else if (cellvalue == '1') {
//            var style = "color:red;";
//            if (typeof (options.colModel.formatterStyle) != "undefined") {
//                var txt = options.colModel.formatterStyle[1];
//            } else {
//                var txt = "高于最大警戒值";
//            } 
//        }
//        else {
//            var txt = '';
//        }

//        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
//    }
//    function IsMUnFmt(cellvalue, options, cell) {
//        return $('span', cell).attr('rel');
//    }

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , title: '<span style="color:#FFAE4A">紫色：</span>【库存数量大于最大报警数量】  <span style="color:#00BBFF">蓝色：</span>【库存数量小于最小报警数量】 <span style="color:red"> 物资名称加粗加红：</span>【超时】'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl 
            , storeCondition:'IsUsed = 1'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 480
            , dialogHeight: 320
            , groupField: 'GoodsTypeName'
            , groupingView: { groupSummary: [true] }
		    , initArray: [
                { label: '物资编号', name: 'ID', index: 'ID', width: 100, cellattr: addCellAttr }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsName', width: 120,cellattr: addCellAttr }
                , { label: '型号', name: 'GoodsModel', index: 'GoodsModel', width: 80 }
                , { label: '类型编号', name: 'GoodsTypeID', index: 'GoodsTypeID', hidden:true }
                , { label: '物资类型', name: 'GoodsTypeName', index: 'GoodsTypeID', width: 100, stype: 'select', searchoptions: { value: getStuffinfoStr()} }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '总量', name: 'TotalNum', index: 'TotalNum', align: 'right', width: 80 }
                , { label: '库存数量', name: 'ContentNum', index: 'ContentNum', align: 'right', width: 80 }
                , { label: '设备大类', name: 'ClassBName', index: 'ClassBName', width: 100 }
                , { label: '设备大类', name: 'ClassBID', index: 'ClassBID', width: 100, hidden: true }
                , { label: '设备中类', name: 'ClassMName', index: 'ClassMName', width: 100 }
                , { label: '设备中类', name: 'ClassMID', index: 'ClassMID', width: 100, hidden: true }
                , { label: '设备细类', name: 'ClassSName', index: 'ClassSName', width: 100 }
                , { label: '设备细类', name: 'ClassSID', index: 'ClassSID', width: 100, hidden: true }
                , { label: '均价', name: 'uPrice', index: 'uPrice', align: 'right', width: 80 }
                , { label: '单位', name: 'unit', index: 'unit', align: 'right', width: 80 }
                , { label: '总价', name: 'tPrice', index: 'tPrice', align: 'right', width: 80, summaryType: 'sum', summaryTpl: '小计:{0}' }
                , { label: '入库数量', name: 'InNum', index: 'InNum', align: 'right', width: 80 }
                , { label: '领用数量', name: 'OutNum', index: 'OutNum', align: 'right', width: 80 }
                , { label: '借用数量', name: 'BorrowNum', index: 'BorrowNum', align: 'right', width: 80 }
                , { label: '归还数量', name: 'RevertNum', index: 'RevertNum', align: 'right', width: 80 }
                , { label: '报废数量', name: 'InvalidateNum', index: 'InvalidateNum', align: 'right', width: 80 }
                , { label: '进出库情况', name: 'IsM', index: 'IsM', align: 'right', width: 100 , hidden: true }
                , { label: '最小报警数量', name: 'MinWarmContent', index: 'MinWarmContent', align: 'right', width: 100 }
                , { label: '最大报警数量', name: 'MaxWarmContent', index: 'MaxWarmContent', align: 'right', width: 100 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 100 }
                , { label: '创建人', name: 'Builder', index: 'Builder' }
                , { label: '修改人', name: 'Modifier', index: 'Modifier' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleIsUsed: function () {//已启用
                    myJqGrid.refreshGrid("IsUsed = 1");
                },
                handleNotUsed: function () {//已停用
                    myJqGrid.refreshGrid("IsUsed = 0");
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({                        
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            goodsChange();
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
                //启用
                , handleSetUsed: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showError("提示", "请选择要启用的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.IsUsed == "true") {
                        showError("提示", "此记录已启用!");
                        return;
                    }
                    showConfirm("确认信息", "确认要启用选中的记录?", function (btn) {
                        ajaxRequest(
                            '/GoodsInfo.mvc/UpdateUsedStatus', //URL
                            {ids: keys, usedStatus: true }, //参数
                            true,
                            function (response) {
                                if (response.Result) {//执行成功
                                    myJqGrid.reloadGrid(); //重新加载
                                }
                            }
                        );

                        $(this).dialog('close'); //关闭对话框窗体
                    });
                }
                //停用
                , handleSetUnused: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择要停用的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.IsUsed == "false") {
                        showError("提示", "此记录已停用!");
                        return;
                    }
                    showConfirm("确认信息", "确认要停用选中的记录?", function (btn) {
                        ajaxRequest(
                            '/GoodsInfo.mvc/UpdateUsedStatus',
                            { ids: keys, usedStatus: false },
                            true,
                            function (response) {
                                myJqGrid.reloadGrid();
                            }
                        );

                        $(this).dialog('close');
                    });
                }
            }
        });

        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        var ClassBList = $("#ClassBID");
        var ClassMList = $("#ClassMID");
        var ClassSList = $("#ClassSID");
        ClassBList.bind("change", function (e) {
            var bid = $(this).val();
            if (bid) {
                //中类数据绑定
                bindSelectData(
                ClassMList,
                opts.listClassMUrl,
                {
                    textField: "ClassMName",
                    valueField: "ID",
                    condition: "ClassBID='" + bid + "' AND ClassID = 'EquipType'"
                },
                function () {

                    ClassSList.empty();
                })
            }
        });

        ClassMList.bind("change", function () {
            _bid = ClassBList.val();
            mid = $(this).val();
            if (mid) {
                bindSelectData(
                ClassSList,
                opts.listClassSUrl,
                {
                    textField: "ClassSName",
                    valueField: "ID",
                    condition: "ClassBID ='" + _bid + "' AND ClassMID='" + mid + "' AND  ClassID = 'EquipType'"
                },
                function () {
                })}
        });

        myJqGrid.addListeners("gridComplete", function () {
            var ids = myJqGrid.getJqGrid().jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var record = myJqGrid.getRecordByKeyValue(ids[i]);
                //当前值小于最小报警数量--蓝色
                if (parseInt(record.ContentNum) < parseInt(record.MinWarmContent)) {
                    var $id = $(document.getElementById(ids[i]));
                    $id.removeAttr("style");
                    $id.removeClass("ui-widget-content");
                    document.getElementById(ids[i]).style.backgroundColor = "#00BBFF";
                }
                //当前值大于最大报警数量--紫色
                if (parseInt(record.ContentNum) > parseInt(record.MaxWarmContent)) {
                    var $id = $(document.getElementById(ids[i]));
                    $id.removeAttr("style");
                    $id.removeClass("ui-widget-content");
                    document.getElementById(ids[i]).style.backgroundColor = "#FFAE4A";
                }
//                //超时--橙色
//                if (parseInt(record.IsM) == 1) { //1超时 0正常
//                    var $id = $(document.getElementById(ids[i]));
//                    $id.removeAttr("style");
//                    $id.removeClass("ui-widget-content");
//                    document.getElementById(ids[i]).style.color = "red"; //"#E93EFF";
//                    document.getElementById(ids[i]).style.fontWeight = 'bold';
//                }
            }
        });
        function addCellAttr(rowId, val, rawObject, cm, rdata) {
            if (rawObject != null) {
                if (rawObject.IsM == 1 && rawObject.ContentNum>0) {
                    return "style='font-weight:bold;color:red'";
                }
            }

        }
        //$("#MyJqGrid").append("<span style='color:#FFAE4A'>紫色：</span>【库存数量大于最大报警数量】  <span style=\"color:#00BBFF\">蓝色：</span>【库存数量小于最小报警数量】 <span style=\"color:red\"> 物资名称加粗加红：</span>【超时】"); 
        
        //获取材料列表 lzl add 2016-07-28
        function getStuffinfoStr() {
            //动态生成select内容
            var requestURL = "/GoodsType.mvc/FindGoodsType";
            var postParams = {};
            var str = "";

            $.ajax({
                type: 'post',
                async: false, //必须是false 同步方式
                url: requestURL,
                data:postParams,
                success: function (data) {
                    if (data != null) {
                        var jsonobj = eval(data);
                        var length = jsonobj.length;
                        str += "" + ":" + ";";
                        for (var i = 0; i < length; i++) {
                            if (i != length - 1) {
                                str += jsonobj[i].ID + ":" + jsonobj[i].GoodsTypeName + ";";
                            } else {
                                str += jsonobj[i].ID + ":" + jsonobj[i].GoodsTypeName ;
                            }
                        }

                    }

                }
            });
            return str;
            //return str = "CL0003:地维水泥;CL0004:水;CL0005:粉煤灰;CL0006:机制砂";           
        }

        //获取型号列表
        goodsChange = function () {
            var goodsNameField = myJqGrid.getFormField("GoodsName");

            goodsNameField.unbind('blur');
            goodsNameField.bind('blur', function () {
                $('#GoodsModel').empty();
                var goodsName = goodsNameField.val();
                if (!isEmpty(goodsName)) {
                    bindSelectData($('#GoodsModel'),
                    '/GoodsInfo.mvc/ListData',
                    { textField: 'GoodsModel',
                        valueField: 'GoodsModel',
                        condition: "GoodsName='" + goodsName + "'"
                    },
                    function (data) {
                        var count = data.length;
                        if (count > 0) {
                            $('#GoodsModel').children().first().remove(); //
                        }
                    }
                );

                }
            });
        };
    }
   