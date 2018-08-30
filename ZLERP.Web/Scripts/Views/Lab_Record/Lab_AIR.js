function lab_recordIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
            , storeCondition: "FinalStuffTypeID='AIR' and StuffTypeID='AIR1'"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [

                { label: '日期', name: 'Date', index: 'Date', formatter: 'datetime', width: 120, frozen: true }
                , { label: '记录id', name: 'ID', index: 'ID', hidden: true }
//                , { label: '时间', name: 'Time', index: 'Time', width: 80 }
                , { label: '运输车号', name: 'Carno', index: 'Carno', width: 80 }
                , { label: '材料id', name: 'Stuffid', index: 'Stuffid',hidden:true }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 80 }
                , { label: '生产厂家id', name: 'Supplyid', index: 'Supplyid', hidden: true }
                , { label: '生产厂家', name: 'SupplyName', index: 'SupplyName' }
                , { label: '发车时间', name: 'StartDate', index: 'StartDate', formatter: 'date', width: 120 }
                , { label: '到站时间', name: 'EndDate', index: 'EndDate', formatter: 'date', width: 120 }
                , { label: '铅封是否完好', name: 'IsWhole', index: 'IsUsed', align: 'center', width: 100, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo' }
                , { label: '单车车重(T)', name: 'Weight', index: 'Weight', width: 80 }
                , { label: '累计重量(T)', name: 'SumWeight', index: 'SumWeight', width: 80 }
                , { label: '罐编号', name: 'Siloid', index: 'Siloid', hidden: true }
                , { label: '筒仓', name: 'SiloName', index: 'SiloName', width: 80 }
                , { label: '收料人员', name: 'InMan', index: 'InMan', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                //, { label: '操作', name: 'act', index: 'act', width: 60, sortable: false, align: "center" }
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
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleJc(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='检测'><input type='button' class='ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only ui-state-hover' value='检测' /></div>";
            myJqGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

        }

    });
    jQuery("#myJqGrid-datagrid").jqGrid('setFrozenColumns');

    window.handleJc = function (id) {
        alert(id);
        showConfirm("确认信息", "您确定删除此项结算记录？", function () {
            $.post(
                    "/ContractJSTZ.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        jstzGrid.reloadGrid();
                    }

                );
            $(this).dialog("close");
        });
    };

    var AirJqGrid = new MyGrid({
        renderTo: 'AirJqGrid'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight/2
            , dialogWidth: 700
            , dialogHeight: 530
		    , storeURL: "/Lab_AirReport.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
               , { label: '父id', name: 'Lab_RecordID', index: 'Lab_RecordID', hidden: true }
               , { label: '品种', name: 'Type', index: 'Type' }
               , { label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo', width: 80 }
               , { label: '样品说明', name: 'Description', index: 'Description' }
               , { label: '生产厂家', name: 'SupplyName', index: 'SupplyName' }
               , { label: '检测依据', name: 'DependInfoID', index: 'DependInfoID', hidden: true }
               , { label: '日期', name: 'Date', index: 'Date', formatter: 'datetime', hidden: true, formatter: 'date' }
               , { label: '报告日期', name: 'ReportDate', index: 'ReportDate', hidden: true, formatter: 'date' }
               , { label: '单位', name: 'Unit', index: 'Unit', hidden: true }
               , { label: 'Grade', name: 'Grade', index: 'Grade', hidden: true }
               , { label: 'GoDate', name: 'GoDate', index: 'GoDate', hidden: true, formatter: 'date' }
               , { label: 'Radix', name: 'Radix', index: 'Radix', hidden: true }
               , { label: 'ThinResult', name: 'ThinResult', index: 'ThinResult', hidden: true }
               , { label: 'ThinConclusion', name: 'ThinConclusion', index: 'ThinConclusion', hidden: true }
               , { label: 'NeedWaterResult', name: 'NeedWaterResult', index: 'NeedWaterResult', hidden: true }
               , { label: 'NeedWaterConclusion', name: 'NeedWaterConclusion', index: 'NeedWaterConclusion', hidden: true }
               , { label: 'BurntResult', name: 'BurntResult', index: 'BurntResult', hidden: true }
               , { label: 'BurntConclusion', name: 'BurntConclusion', index: 'BurntConclusion', hidden: true }
               , { label: 'WaterResult', name: 'WaterResult', index: 'WaterResult', hidden: true }
               , { label: 'WaterConclusion', name: 'WaterConclusion', index: 'WaterConclusion', hidden: true }
               , { label: 'SafeResult', name: 'SafeResult', index: 'SafeResult', hidden: true }
               , { label: 'SafeConclusion', name: 'SafeConclusion', index: 'SafeConclusion', hidden: true }
               , { label: 'ActiveResult', name: 'ActiveResult', index: 'ActiveResult', hidden: true }

               , { label: 'ActiveConclusion', name: 'ActiveConclusion', index: 'ActiveConclusion', hidden: true }
               , { label: 'Conclusion', name: 'Conclusion', index: 'Conclusion', hidden: true }
               , { label: 'Remark', name: 'Remark', index: 'Remark', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    AirJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    AirJqGrid.refreshGrid();
                }
                ,
                handleAirReport: function (btn) {
                    AirJqGrid.handleEdit({
                        loadFrom: 'AirFormDiv',
                        prefix: 'Lab_AirReport',
                        btn: btn
                    });
                }
            }
    });
    
    var AirOriginJqGrid = new MyGrid({
        renderTo: 'AirOriginJqGrid'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight / 2 - 80
            , dialogWidth: 860
            , dialogHeight: 700
		    , storeURL: "/Lab_AirOrigin.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
		    , initArray: [
                 { label: '检测编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '主表记录ID', name: 'Lab_RecordID', index: 'Lab_RecordID',hidden:true }
                , { label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo', width: 80 }
                , { label: '检测依据ID', name: 'DependInfoID', index: 'DependInfoID', hidden: true }
                , { label: '煤灰品种', name: 'StuffName', index: 'StuffName' }
                , { label: '等   级', name: 'Grade', index: 'Grade', hidden: true }
                , { label: '样品说明', name: 'Description', index: 'Description', hidden: true }
                , { label: '成型日期', name: 'SuccessDate', index: 'SuccessDate', formatter: 'date', hidden: true }
                , { label: '成型室温度', name: 'SuccessTemperature', index: 'SuccessTemperature', hidden: true }
                , { label: '成型室湿度', name: 'SuccessWet', index: 'SuccessWet', hidden: true }
                , { label: '试样质量(g)', name: 'Weight', index: 'Weight', hidden: true }
                , { label: '筛后样品质量(g)', name: 'AfterWeight', index: 'AfterWeight', hidden: true }
                , { label: '校正系数', name: 'Alignment', index: 'Alignment', hidden: true }
                , { label: '筛余百分数(%)', name: 'SPercent', index: 'SPercent', hidden: true }
                , { label: '对比胶砂的加水量(ml)', name: 'AddWaterThan', index: 'AddWaterThan', hidden: true }
                , { label: '试验胶砂加水量(ml)', name: 'AddWater', index: 'AddWater', hidden: true }
                , { label: '需水量比(%)', name: 'NeedWater', index: 'NeedWater', hidden: true }
                , { label: '烘干前试样质量(g)', name: 'DryBefore', index: 'DryBefore', hidden: true }
                , { label: '烘干后试样质量(g)', name: 'DryAfter', index: 'DryAfter', hidden: true }
                , { label: '含水量（%）', name: 'ContentWater', index: 'ContentWater', hidden: true }
                , { label: 'A1', name: 'A1', index: 'A1', hidden: true }
                , { label: 'A2', name: 'A2', index: 'A2', hidden: true }
                , { label: 'C1', name: 'C1', index: 'C1', hidden: true }
                , { label: 'C2', name: 'C2', index: 'C2', hidden: true }
                , { label: 'C1subA1', name: 'C1subA1', index: 'C1subA1', hidden: true }
                , { label: 'C2subA2', name: 'C2subA2', index: 'C2subA2', hidden: true }
                , { label: 'CsubAAve', name: 'CsubAAve', index: 'CsubAAve', hidden: true }
                , { label: '结果判断', name: 'Result', index: 'Result', hidden: true }
                , { label: '仪器设备运行状况', name: 'MachineRun', index: 'MachineRun', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    AirOriginJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    AirOriginJqGrid.refreshGrid();
                }
                ,
                handleAdd: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var records = AirOriginJqGrid.getAllRecords();
                    if (records.length>0) {
                        showMessage('提示', '已经存在记录，不能再新增了！');
                        return;
                    }

                    var selectrecord = myJqGrid.getSelectedRecord();
                    AirOriginJqGrid.handleAdd({
                        loadFrom: 'AirOriginFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#Lab_AirOrigin_Lab_RecordID").val(selectrecord.ID);
                            $("#Lab_AirOrigin_ShowpeieNo").val(selectrecord.ShowpeieNo);
                            $("#Lab_AirOrigin_StuffName").val(selectrecord.StuffName);

                            burntJqGrid.refreshGrid("1<>1");
                            activeJqGrid.refreshGrid("1<>1");
                        }
                    });
                },
                handleEdit: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = myJqGrid.getSelectedRecord();
                    var selectrecord2 = AirOriginJqGrid.getSelectedRecord();
                    AirOriginJqGrid.handleEdit({
                        loadFrom: 'AirOriginFormDiv',
                        prefix: 'Lab_AirOrigin',
                        btn: btn,
                        closeDialog: false,
                        afterFormLoaded: function () {
                            //myJqGrid.setFormFieldValue("Lab_AirOrigin_Lab_RecordID", selectrecord.ID);
                            $("#Lab_AirOrigin_Lab_RecordID").val(selectrecord.ID);

                            //加载烧失量列表
                            burntJqGrid.getJqGrid().jqGrid('setGridParam', { editurl: '' });
                            burntJqGrid.refreshGrid("Lab_AirOriginId='" + selectrecord2.ID + "'");
                            //加载活性指数列表
                            activeJqGrid.getJqGrid().jqGrid('setGridParam', { editurl: '' });
                            activeJqGrid.refreshGrid("Lab_AirOriginId='" + selectrecord2.ID + "'");

                        }
                        });
                }
                , handleDelete: function (btn) {
                    AirOriginJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        myJqGrid.addListeners("rowclick", function (id, record) {
            AirJqGrid.refreshGrid("Lab_RecordID='" + id + "'");
            AirOriginJqGrid.refreshGrid("Lab_RecordID='" + id + "'");
        });

        //安定性 字段计算

        //计算Mround
        function Mround(number,multiple) { 
           return Math.round(number/multiple)*multiple*1
        }
        //计算C-A
        function CsubACalc(T1, T2) {
            var a = Mround((T1+T2)/2,0.5)
            return a;
        }

        $("#Lab_AirOrigin_A1").blur(function () {
            var A1 = $('#Lab_AirOrigin_A1').val();
            var C1 = $('#Lab_AirOrigin_C1').val();
            if (isEmpty(A1) && isEmpty(C1))
                return;
            $('#Lab_AirOrigin_C1subA1').val(Mround((C1 * 1 - A1 * 1), 0.5));

            var A2 = $('#Lab_AirOrigin_A2').val();
            var C2 = $('#Lab_AirOrigin_C2').val();
            $('#Lab_AirOrigin_CsubAAve').val(CsubACalc(C1 - A1, C2 - A2));
        });

        $("#Lab_AirOrigin_A2").blur(function () {
            var A2 = $('#Lab_AirOrigin_A2').val();
            var C2 = $('#Lab_AirOrigin_C2').val();
            if (isEmpty(A2) && isEmpty(C2))
                return;
            $('#Lab_AirOrigin_C2subA2').val(Mround((C2 * 1 - A2 * 1), 0.5));

            var A1 = $('#Lab_AirOrigin_A1').val();
            var C1 = $('#Lab_AirOrigin_C1').val();
            $('#Lab_AirOrigin_CsubAAve').val(CsubACalc(C1 - A1, C2 - A2));
        });
        $("#Lab_AirOrigin_C1").blur(function () {
            var A1 = $('#Lab_AirOrigin_A1').val();
            var C1 = $('#Lab_AirOrigin_C1').val();
            if (isEmpty(A1) && isEmpty(C1))
                return;
            $('#Lab_AirOrigin_C1subA1').val(Mround((C1 * 1 - A1 * 1), 0.5));

            var A2 = $('#Lab_AirOrigin_A2').val();
            var C2 = $('#Lab_AirOrigin_C2').val();
            $('#Lab_AirOrigin_CsubAAve').val(CsubACalc(C1 - A1, C2 - A2));
        });

        $("#Lab_AirOrigin_C2").blur(function () {
            var A2 = $('#Lab_AirOrigin_A2').val();
            var C2 = $('#Lab_AirOrigin_C2').val();
            if (isEmpty(A2) && isEmpty(C2))
                return;
            $('#Lab_AirOrigin_C2subA2').val(Mround((C2 * 1 - A2 * 1), 0.5));

            var A1 = $('#Lab_AirOrigin_A1').val();
            var C1 = $('#Lab_AirOrigin_C1').val();
            $('#Lab_AirOrigin_CsubAAve').val(CsubACalc(C1 - A1, C2 - A2));
        });

        //--------------------------------------------烧失量------------------------------------------------------
        var burntJqGrid;
        burntJqGrid = new MyGrid({
            renderTo: 'BurntJqGrid'
        , width: '100%'
        , buttons: buttons3
        , autoWidth: true
        , height: gGridHeight / 11-1
        , storeURL: '/Lab_BurntInfo.mvc/Find'
        , storeCondition: "1=1"
        , sortByField: 'Sort'
        , primaryKey: 'ID'
        , sortable: true
        , sortOrder: 'Asc'
        , setGridPageSize: 30
        , showPageBar: false
        , autoLoad: false
        , singleSelect: true
        , footerrow: true
        , userDataOnFooter: true
        , editSaveUrl: "/Lab_BurntInfo.mvc/Update"
        //, groupingView: { groupSummary: [true], groupColumnShow: [false], groupText: ['({1})'] }
        //, groupField: 'act1'
        , initArray: [
            { label: 'id', name: 'ID', index: 'ID', hidden: true }
            , { label: '检测ID', name: 'Lab_AirOriginId', index: 'Lab_AirOriginId', width: 60, hidden: true }
            , { label: '样品号', name: 'Sort', index: 'Sort', width: 60, editable: true }
            , { label: '瓷坩埚的质量(g)', name: 'Weight', index: 'Weight', width: 80, editable: true }
            , { label: '瓷坩埚加试样灼烧前质量(g)', name: 'WeightBefore', index: 'WeightBefore', width: 160, editable: true }
            , { label: '瓷坩埚加试样灼烧后质量(g)', name: 'WeightAfter', index: 'WeightAfter', width: 180, editable: true, summaryType: 'avg', summaryTpl: '烧失量' }
            , { label: '烧失量', name: 'Percents', index: 'Percents', width: 88, hidden: false, summaryType: 'avg', summaryTpl: '{0}' }
            //, { label: '烧失量(%)', name: 'Percents', index: 'Percents', width: 88, formatter: PercentFormat, unformat: PercentUnFormat }
            , { label: '操作', name: 'act', index: 'act', width: 100, sortable: false, align: "center" }
            //, { label: '假行', name: 'act1', index: 'act1', width: 70, sortable: false, align: "center"}
        ]
        , autoLoad: true
        , functions: {
            handleReload: function (btn) {
                burntJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                burntJqGrid.refreshGrid('1=1');
            },
            handleAdd: function (btn) {
                var airoriginid = $("#Lab_AirOrigin_ID").val();
                if (airoriginid == "") {
                    showMessage('提示', '请先保存添加检测记录！');
                    return;
                }
                
                var airOriginId = $("#Lab_AirOrigin_ID").val();//检测编号
                burntJqGrid.handleAdd({
                    loadFrom: 'BurntInfoDiv',
                    width: 320,
                    height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_BurntInfo_Lab_AirOriginId").val(airOriginId);
                    }
                });
            },
            handleEdit: function (btn) {

                var airOriginId = $("#Lab_AirOrigin_ID").val(); //检测编号
                burntJqGrid.handleEdit({
                    loadFrom: 'BurntInfoDiv',
                    prefix: 'Lab_BurntInfo',
                    width: 320,
                    height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_BurntInfo_Lab_AirOriginId").val(airOriginId);
                    }
                });
            }
        }
        });

        burntJqGrid.addListeners("gridComplete", function () {
            var ids = burntJqGrid.getJqGrid().jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteBurnt(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                burntJqGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

            }

            //烧失量
            jQuery("#BurntJqGrid-datagrid").footerData("set", {
                "WeightAfter": "<span style='color:red;float:right;font-weight:normal;'>烧失量</span>"
            });
            //底部合计行
            var rowNum = burntJqGrid.getAllRecords().length;
            if (rowNum > 0) {
                $(".ui-jqgrid-sdiv").show();
                var Percents = parseFloat($("#BurntJqGrid-datagrid").getCol("Percents", false, "sum") * 100 / rowNum).toFixed(1)+"%";
                jQuery("#BurntJqGrid-datagrid").footerData("set", { "Percents": "<span style='color:red;float:left'>" + Percents + "</span>" }); //将合计值显示出来
            } else {
                $(".ui-jqgrid-sdiv").hide();
            }
            //隐藏水平滚动条
            jQuery("#BurntJqGrid-datagrid").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });


            //获得所有行的ID数组
//            var total = 0;
//            var rows = 1;
//            var records = burntJqGrid.getAllRecords();
//            for (var i = 0; i < records.length; i++) {
//                var w = records[i].Weight;
//                var wb = records[i].WeightBefore;
//                var wa = records[i].WeightAfter;
//                var wp = (wb * 1 - wa * 1) / (wb * 1 - w * 1); //计算烧失量
//                total = total + wp * 1;
//                $("#BurntJqGrid tr:eq(" + (i + 3) + ") td:eq(6)").text(toDecimal(wp, 10000)); //给烧失量赋值
//            } 
            //$("#BurntJqGrid tr:eq(" + (i + 4) + ") td:eq(6)").text(total / rows);

        });
        //单元格-保存后
        burntJqGrid.addListeners("afterSaveCell", function () {
            burntJqGrid.refreshGrid('1=1'); //刷新
        });
        window.handleDeleteBurnt = function (id) {
            showConfirm("确认信息", "您确定删除此项记录？", function () {
                $.post(
                    "/Lab_BurntInfo.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        burntJqGrid.reloadGrid();
                    }

                );
                $(this).dialog("close");
            });
        };


        //-------------------计算筛余百分数------------------------
        $("#Lab_AirOrigin_Weight").blur(function () {
            SPercentCalc();
        });
        $("#Lab_AirOrigin_AfterWeight").blur(function () {
            SPercentCalc();
        });
        $("#Lab_AirOrigin_Alignment").blur(function () {
            SPercentCalc();
        });
        //
        function SPercentCalc() {
            var a = $('#Lab_AirOrigin_Weight').val();
            var b = $('#Lab_AirOrigin_AfterWeight').val();
            var c = $('#Lab_AirOrigin_Alignment').val();
            //alert(a+"|"+b+"|"+c);
            if (isEmpty(a) && isEmpty(b) && isEmpty(c))
                return;
            if (a * 1 ==0)
                return;
            var d=(b *100)/(a*1) *(c * 1);
            $('#Lab_AirOrigin_SPercent').val(toDecimal(d, 10));
        }

        //-------------------计算需水量比------------------------
        $("#Lab_AirOrigin_AddWaterThan").blur(function () {
            NeedWaterCalc();
        });
        $("#Lab_AirOrigin_AddWater").blur(function () {
            NeedWaterCalc();
        });
        //
        function NeedWaterCalc() {
            var a = $('#Lab_AirOrigin_AddWaterThan').val();
            var b = $('#Lab_AirOrigin_AddWater').val();
            if (isEmpty(a) && isEmpty(b))
                return;
            if (a * 1 == 0)
                return;
            var d = (b * 100) / (a * 1);
            $('#Lab_AirOrigin_NeedWater').val(toDecimal(d, 1));
        }

        //-------------------计算含水量------------------------
        $("#Lab_AirOrigin_DryBefore").blur(function () {
            ContentWaterCalc();
        });
        $("#Lab_AirOrigin_DryAfter").blur(function () {
            ContentWaterCalc();
        });
        //
        function ContentWaterCalc() {
            var a = $('#Lab_AirOrigin_DryBefore').val();
            var b = $('#Lab_AirOrigin_DryAfter').val();
            if (isEmpty(a) && isEmpty(b))
                return;
            if (a * 1 == 0)
                return;
            var d = (a * 1-b*1)*100 / (a * 1);
            $('#Lab_AirOrigin_ContentWater').val(toDecimal(d, 10));
        }
        //------------------- 计算安定性值-------------------------------
        function PercentsCalc() {
            var w = $('#Lab_BurntInfo_Weight').val();
            var wb = $('#Lab_BurntInfo_WeightBefore').val();
            var wa = $('#Lab_BurntInfo_WeightAfter').val();
            if (isEmpty(w) && isEmpty(wb) && isEmpty(wa))
                return;
            if (wb * 1 == w * 1)
                return;
            $('#Lab_BurntInfo_Percents').val(toDecimal((wb * 1 - wa * 1) / (wb * 1 - w * 1),10000));
        }
        //保留小数位数
        function toDecimal(x,y) {
            var f = parseFloat(x);
            if (isNaN(f)) {
                return;
            }
            f = Math.round(x * y) / y;
            return f;
        } 

        $("#Lab_BurntInfo_Weight").blur(function () {
            PercentsCalc();
        });
        $("#Lab_BurntInfo_WeightBefore").blur(function () {
            PercentsCalc();
        });
        $("#Lab_BurntInfo_WeightAfter").blur(function () {
            PercentsCalc();
        });

        //自定义单元格百分比格式
        function PercentFormat(cellvalue, options, rowObject) {
            var newvalue = cellvalue * 100;
            return '' + newvalue + '%';
        }
        function PercentUnFormat(cellvalue, options, cell) {
            return ;
        }  

        //--------------------------------------------活性指数------------------------------------------------------
        var activeJqGrid;
        activeJqGrid = new MyGrid({
        renderTo: 'ActiveJqGrid'
        , width: '100%'
        , buttons: buttons4
        , autoWidth: true
        , height: gGridHeight / 3-45
        , storeURL: '/Lab_ActiveInfo.mvc/Find'
        , storeCondition: "1=1"
        , sortByField: 'ID'
        , primaryKey: 'ID'
        , sortable: true
        , sortOrder: 'Asc'
        , setGridPageSize: 30
        , showPageBar: false
        , autoLoad: false
        , singleSelect: true
        , dialogWidth: 660
        , dialogHeight: 360
        , editSaveUrl: "/Lab_ActiveInfo.mvc/Update"
        //, groupingView: { groupSummary: [true],groupColumnShow : [false], groupText: ['({1})'] }
        //, groupField: 'act1'
        ,footerrow : true
        , userDataOnFooter: true
        , altRows: true
        //, colNames: ['7d1', '7d2', '7d3', '28d1', '28d2', '28d3']
        , initArray: [
            { label: 'id', name: 'ID', index: 'ID', hidden: true }
            , { label: '检测ID', name: 'Lab_AirOriginId', index: 'Lab_AirOriginId', width: 60, hidden: true }
            , { label: '荷载', name: 'D7_1', index: 'D7_1', width: 70, editable: true, summaryType: 'avg', summaryTpl: '代表值(MPa):' }
            , { label: '单块值', name: 'D7_2', index: 'D7_2', width: 70, editable: true, summaryType: 'avg', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: "", defaulValue: "", decimalPlaces: 1 }, showSummaryOnHide: false }
            , { label: 'D7_3', name: 'D7_3', index: 'D7_3', width: 50, editable: true, hidden: true }
            , { label: '荷载', name: 'D28_1', index: 'D28_1', width: 50, editable: true }
            , { label: '单块值', name: 'D28_2', index: 'D28_2', width: 70, editable: true, summaryType: 'avg', summaryTpl: '{0}', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: ",", defaulValue: "", decimalPlaces: 1} }
            , { label: 'D28_3', name: 'D28_3', index: 'D28_3', width: 50, editable: true, hidden: true }
            , { label: '荷载', name: 'S7_1', index: 'S7_1', width: 50, editable: true }
            , { label: '单块值', name: 'S7_2', index: 'S7_2', width: 70, editable: true, summaryType: 'avg', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: "", defaulValue: "", decimalPlaces: 1} }
            , { label: 'S7_3', name: 'S7_3', index: 'S7_3', width: 50, editable: true, hidden: true }
            , { label: '荷载', name: 'S28_1', index: 'S28_1', width: 50, editable: true }
            , { label: '单块值', name: 'S28_2', index: 'S28_2', width: 70, editable: true, summaryType: 'avg', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: ",", defaulValue: "", decimalPlaces: 1} }
            , { label: 'S28_3', name: 'S28_3', index: 'S28_3', width: 50, editable: true, hidden: true }
            , { label: '操作', name: 'act', index: 'act', width: 140, sortable: false, align: "center" }
            //, { label: '假行', name: 'act1', index: 'act1', width: 70, sortable: false, align: "center" }
        ]
        , autoLoad: true
        , functions: {
            handleReload: function (btn) {
                activeJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                activeJqGrid.refreshGrid('1=1');
            },
            handleAdd: function (btn) {
                var airoriginid = $("#Lab_AirOrigin_ID").val();
                if (airoriginid == "") {
                    showMessage('提示', '请先保存添加检测记录！');
                    return;
                }
                var airOriginId = $("#Lab_AirOrigin_ID").val(); //检测编号
                activeJqGrid.handleAdd({
                    loadFrom: 'ActiveInfoDiv',
                    //width: 320,
                    //height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_ActiveInfo_Lab_AirOriginId").val(airOriginId);
                    }
                });
            },
            handleEdit: function (btn) {
                var airOriginId = $("#Lab_AirOrigin_ID").val(); //检测编号
                activeJqGrid.handleEdit({
                    loadFrom: 'ActiveInfoDiv',
                    prefix: 'Lab_ActiveInfo',
                    //width: 320,
                    //height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_ActiveInfo_Lab_AirOriginId").val(airOriginId); 
                    }
                });
            }, 
            handleDelete: function (btn) {
                activeJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    activeJqGrid.addListeners("gridComplete", function () {
        var ids = activeJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteActive(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
            activeJqGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

        }

        //合计
        jQuery("#ActiveJqGrid-datagrid").footerData("set", {
            "D7_1": "<span style='color:black;'>代表值(MPa)</span>"
        });
        //底部合计行
        var rowNum = activeJqGrid.getAllRecords().length;
        if (rowNum > 0) {
            $(".ui-jqgrid-sdiv").show();
            var d72 = parseFloat($("#ActiveJqGrid-datagrid").getCol("D7_2", false, "sum") * 1 / rowNum).toFixed(2);
            var d282 = parseFloat($("#ActiveJqGrid-datagrid").getCol("D28_2", false, "sum") * 1 / rowNum).toFixed(2);
            var S72 = parseFloat($("#ActiveJqGrid-datagrid").getCol("S7_2", false, "sum") * 1 / rowNum).toFixed(2);
            var S282 = parseFloat($("#ActiveJqGrid-datagrid").getCol("S28_2", false, "sum") * 1 / rowNum).toFixed(2);

            var d28 = "" + parseFloat(S282 * 100 / d282).toFixed(0) + "%";
            jQuery("#ActiveJqGrid-datagrid").footerData("set", { "D7_2": d72, "D28_2": d282, "S7_2": S72, "S28_2": S282, "act": "<span style='color:red;float:left;font-weight:normal;'>活性指数:<b>" + d28 + "</b></span>" }); //将合计值显示出来
        } else {
            $(".ui-jqgrid-sdiv").hide();
        }
        //隐藏水平滚动条
        jQuery("#ActiveJqGrid-datagrid").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });
    });
    //单元格-保存后
    activeJqGrid.addListeners("afterSaveCell", function () {
        activeJqGrid.refreshGrid('1=1');  //刷新
    });

    //二级表头合并
    jQuery("#ActiveJqGrid-datagrid").jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
                        { startColumnName: 'D7_1', numberOfColumns: 2, titleText: '7d' },
                        { startColumnName: 'D28_1', numberOfColumns: 2, titleText: '28d' },
                        { startColumnName: 'S7_1', numberOfColumns: 2, titleText: '7d' },
                        { startColumnName: 'S28_1', numberOfColumns: 2, titleText: '28d' }
            ]
    });
    //三级表头合并
    jQuery("#ActiveJqGrid-datagrid").jqGrid("setComplexGroupHeaders", {
        complexGroupHeaders: [
                              { startColumnName: 'D7_1', numberOfColumns: 4, titleText: '<em>对比胶砂</em>' },
                              { startColumnName: 'S7_1', numberOfColumns: 4, titleText: '试样胶砂' }
			 ]
    });

    window.handleDeleteActive = function (id) {
        showConfirm("确认信息", "您确定删除此项记录？", function () {
            $.post(
                    "/Lab_ActiveInfo.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        activeJqGrid.reloadGrid();
                    }

                );
            $(this).dialog("close");
        });
    };

    $(function () {
       
    });  
}