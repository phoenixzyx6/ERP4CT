function lab_recordIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
            , storeCondition: "FinalStuffTypeID='AIR' and StuffTypeID='AIR2'"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [

                { label: '日期', name: 'Date', index: 'Date', formatter: 'datetime', width: 120, frozen: true }
                , { label: '记录id', name: 'ID', index: 'ID', hidden: true }
//                , { label: '时间', name: 'Time', index: 'Time', width: 80 }
                , { label: '运输车号', name: 'Carno', index: 'Carno', width: 80 }
                , { label: '品种规格', name: 'Spec', index: 'Spec', width: 80 }
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
        showConfirm("确认信息", "您确定删除此项记录？", function () {
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
		    , storeURL: "/Lab_Air2Report.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
		    , initArray: [
                 { label: '报告编号', name: 'ID', index: 'ID', hidden: true }
                ,{ label: 'Lab_RecordID', name: 'Lab_RecordID', index: 'Lab_RecordID', hidden: true }
                ,{ label: '检测依据', name: 'DependInfoID', index: 'DependInfoID', hidden: true }
                ,{ label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo', width: 50 }
                ,{ label: '样品说明', name: 'Description', index: 'Description' }
                ,{ label: '生产厂家', name: 'SupplyName', index: 'SupplyName' }
                ,{ label: '检测日期', name: 'Date', index: 'Date', formatter: 'date', hidden: true }
                , { label: '报告日期', name: 'ReportDate', index: 'ReportDate', formatter: 'date', hidden: true }
                , { label: '单位', name: 'Unit', index: 'Unit', hidden: true }
                , { label: '样品等级', name: 'Grade', index: 'Grade', hidden: true }
                , { label: '样品类别', name: 'Type', index: 'Type', hidden: true }
                , { label: '出厂日期', name: 'GoDate', index: 'GoDate', formatter: 'date', hidden: true }
                , { label: '取样基数(T)', name: 'Radix', index: 'Radix', hidden: true }
                , { label: '密度(g/m3)结果', name: 'DensityResult', index: 'DensityResult', hidden: true }
                , { label: '密度(g/m3)结论', name: 'DensityConclusion', index: 'DensityConclusion', hidden: true }
                , { label: '比表面积(㎡/kg)结果', name: 'SpecificResult', index: 'SpecificResult', hidden: true }
                , { label: '比表面积(㎡/kg)结论', name: 'SpecificConclusion', index: 'SpecificConclusion', hidden: true }
                , { label: '活性指数7d结果', name: 'Active7dResult', index: 'Active7dResult', hidden: true }
                , { label: '活性指数7d结论', name: 'Active7dConclusion', index: 'Active7dConclusion', hidden: true }
                , { label: '活性指数28d结果', name: 'Active28dResult', index: 'Active28dResult', hidden: true }
                , { label: '活性指数28d结论', name: 'Active28dConclusion', index: 'Active28dConclusion', hidden: true }
                , { label: '流动度比结果', name: 'FluidityResult', index: 'FluidityResult', hidden: true }
                , { label: '流动度比结论', name: 'FluidityConclusion', index: 'FluidityConclusion', hidden: true }
                , { label: '含水量结果', name: 'WaterResult', index: 'WaterResult', hidden: true }
                , { label: '含水量结论', name: 'WaterConclusion', index: 'WaterConclusion', hidden: true }
                , { label: '检验结论', name: 'Conclusion', index: 'Conclusion', hidden: true }
                , { label: '备  注', name: 'Remark', index: 'Remark', hidden: true } 
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
                    var selectrecord = myJqGrid.getSelectedRecord();
                    AirJqGrid.handleEdit({
                        loadFrom: 'AirFormDiv',
                        prefix: 'Lab_Air2Report',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#Lab_Air2Report_Grade").val(selectrecord.Spec);
                        }
                    });
                }
            }
    });
    
    var Air2OriginJqGrid = new MyGrid({
        renderTo: 'Air2OriginJqGrid'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight / 2 - 80
            , dialogWidth: 860
            , dialogHeight: 700
		    , storeURL: "/Lab_Air2Origin.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
		    , initArray: [
                 { label: '检测编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '主表记录ID', name: 'Lab_RecordID', index: 'Lab_RecordID',hidden:true }
                , { label: 'DependInfoID', name: 'DependInfoID', index: 'DependInfoID', hidden: true }
                , { label: '矿粉品种', name: 'Type', index: 'Type', hidden: true }
                , { label: '生产厂家', name: 'SupplyName', index: 'SupplyName', hidden: false }
                , { label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo', width: 80 }
                , { label: '样品级别', name: 'Spec', index: 'Spec', width: 80 }
                , { label: '等   级', name: 'Grade', index: 'Grade', hidden: true }
                , { label: '样品说明', name: 'Description', index: 'Description', hidden: false }
                , { label: '成型日期', name: 'SuccessDate', index: 'SuccessDate', formatter: 'date', hidden: true }
                , { label: '成型室温度', name: 'SuccessTemperature', index: 'SuccessTemperature', hidden: true }
                , { label: '成型室湿度', name: 'SuccessWet', index: 'SuccessWet', hidden: true }
                , { label: '温度(℃)', name: 'SpecificTemperature', index: 'SpecificTemperature', hidden: true }
                , { label: '湿度', name: 'SpecificWet', index: 'SpecificWet', hidden: true }
                , { label: 'K值', name: 'SpecificK', index: 'SpecificK', hidden: true }
                , { label: '空隙率', name: 'SpecificVoidage', index: 'SpecificVoidage', hidden: true }
                , { label: '容桶体积(cm3)', name: 'SpecificBarrelVolume', index: 'SpecificBarrelVolume', hidden: true }
                , { label: '矿粉密度(g/cm3)', name: 'SpecificPowderDensity', index: 'SpecificPowderDensity', hidden: true }
                , { label: '矿粉质量1(g)', name: 'SpecificQuality1', index: 'SpecificQuality1', hidden: true }
                , { label: '矿粉质量2(g)', name: 'SpecificQuality2', index: 'SpecificQuality2', hidden: true }
                , { label: '比表面积1', name: 'SpecificArea1', index: 'SpecificArea1', hidden: true }
                , { label: '比表面积2', name: 'SpecificArea2', index: 'SpecificArea2', hidden: true }
                , { label: '比表面积', name: 'SpecificArea', index: 'SpecificArea', hidden: true }
                , { label: '对比砂浆(mm)', name: 'ContrastMortar', index: 'ContrastMortar', hidden: true }
                , { label: '试验砂浆(mm)', name: 'TestMortar', index: 'TestMortar', hidden: true }
                , { label: '流动度比', name: 'FluidityRatio', index: 'FluidityRatio', hidden: true }
                , { label: '烘干前试样质量(g)', name: 'DryBeforeQuality', index: 'DryBeforeQuality', hidden: true }
                , { label: '烘干后试样质量(g)', name: 'DryAfterQuality', index: 'DryAfterQuality', hidden: true }
                , { label: '含水量', name: 'ContentWater', index: 'ContentWater', hidden: true }
                , { label: '仪器设备运行状况', name: 'MachineRun', index: 'MachineRun', hidden: true }
                , { label: '仪器设备运行状况', name: 'MachineRun', index: 'MachineRun', hidden: true }
                
		    ]
            , functions: {
                handleReload: function (btn) {
                    Air2OriginJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    Air2OriginJqGrid.refreshGrid();
                }
                ,
                handleAdd: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var records = Air2OriginJqGrid.getAllRecords();
                    if (records.length>0) {
                        showMessage('提示', '已经存在记录，不能再新增了！');
                        return;
                    }

                    var selectrecord = myJqGrid.getSelectedRecord();
                    Air2OriginJqGrid.handleAdd({
                        loadFrom: 'Air2OriginFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#Lab_Air2Origin_Lab_RecordID").val(selectrecord.ID);
                            $("#Lab_Air2Origin.ShowpeieNo").val(selectrecord.ShowpeieNo);
                            $("#Lab_Air2Origin_SupplyName").val(selectrecord.SupplyName);
                            $("#Lab_Air2Origin_Grade").val(selectrecord.Spec);

                            air2DensityJqGrid.refreshGrid("1<>1");
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
                    var selectrecord2 = Air2OriginJqGrid.getSelectedRecord();
                    Air2OriginJqGrid.handleEdit({
                        loadFrom: 'Air2OriginFormDiv',
                        prefix: 'Lab_Air2Origin',
                        btn: btn,
                        closeDialog: false,
                        afterFormLoaded: function () {
                            $("#Lab_Air2Origin_Lab_RecordID").val(selectrecord.ID);
                            $("#Lab_Air2Origin_Grade").val(selectrecord.Spec);

                            //加载烧失量列表
                            air2DensityJqGrid.getJqGrid().jqGrid('setGridParam', { editurl: '' });
                            air2DensityJqGrid.refreshGrid("Lab_Air2OriginId='" + selectrecord2.ID + "'");
                            //加载活性指数列表
                            activeJqGrid.getJqGrid().jqGrid('setGridParam', { editurl: '' });
                            activeJqGrid.refreshGrid("Lab_Air2OriginId='" + selectrecord2.ID + "'");

                        }
                        });
                }
                , handleDelete: function (btn) {
                    Air2OriginJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        myJqGrid.addListeners("rowclick", function (id, record) {
            AirJqGrid.refreshGrid("Lab_RecordID='" + id + "'");
            Air2OriginJqGrid.refreshGrid("Lab_RecordID='" + id + "'");
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

        //--------------------------------------------密度------------------------------------------------------
        var air2DensityJqGrid;
        air2DensityJqGrid = new MyGrid({
        renderTo: 'Air2DensityJqGrid'
        , width: '100%'
        , buttons: buttons3
        , autoWidth: true
        , height: gGridHeight / 11-1
        , storeURL: '/Lab_Air2Density.mvc/Find'
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
        , editSaveUrl: "/Lab_Air2Density.mvc/Update"
        //, groupingView: { groupSummary: [true], groupColumnShow: [false], groupText: ['({1})'] }
        //, groupField: 'act1'
        , initArray: [
            { label: 'id', name: 'ID', index: 'ID', hidden: true }
            , { label: '检测ID', name: 'Lab_Air2OriginId', index: 'Lab_Air2OriginId', width: 60, hidden: true }
            , { label: '样品号', name: 'Sort', index: 'Sort', width: 60, editable: true }
            , { label: '矿粉质量(g)', name: 'OreQuality', index: 'OreQuality', width: 80, editable: true }
            , { label: '初始体积(ml)', name: 'InitialVolume', index: 'InitialVolume', width: 100, editable: true }
            , { label: '加矿粉后体积(ml)', name: 'ASlagVolume', index: 'ASlagVolume', width: 120, editable: true, summaryType: 'avg', summaryTpl: '烧失量' }
            , { label: '排开煤油体积(mL)', name: 'BKeroseneVolume', index: 'BKeroseneVolume', width: 88, editable: true, hidden: false, summaryType: 'avg', summaryTpl: '{0}' }
            , { label: '操作', name: 'act', index: 'act', width: 100, sortable: false, align: "center" }
        ]
        , autoLoad: true
        , functions: {
            handleReload: function (btn) {
                air2DensityJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                air2DensityJqGrid.refreshGrid('1=1');
            },
            handleAdd: function (btn) {
                var air2originid = $("#Lab_Air2Origin_ID").val();
                if (air2originid == "") {
                    showMessage('提示', '请先保存添加检测记录！');
                    return;
                }
                
                var air2OriginId = $("#Lab_Air2Origin_ID").val();//检测编号
                air2DensityJqGrid.handleAdd({
                    loadFrom: 'Air2DensityDiv',
                    width: 320,
                    height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_Air2Density_Lab_Air2OriginId").val(air2OriginId);
                    }
                });
            },
            handleEdit: function (btn) {

                var air2OriginId = $("#Lab_Air2Origin_ID").val(); //检测编号
                air2DensityJqGrid.handleEdit({
                    loadFrom: 'Air2DensityDiv',
                    prefix: 'Lab_Air2Density',
                    width: 320,
                    height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_Air2Origin_Lab_Air2OriginId").val(air2OriginId);
                    }
                });
            }
        }
        });

    air2DensityJqGrid.addListeners("gridComplete", function () {
        var ids = air2DensityJqGrid.getJqGrid().jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteBurnt(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:5px;' title='删除所选记录'><span class='ui-icon ui-icon-trash'></span></div>";
                air2DensityJqGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });

            }

            //烧失量
            jQuery("#Air2DensityJqGrid-datagrid").footerData("set", {
                "WeightAfter": "<span style='color:red;float:right;font-weight:normal;'>烧失量</span>"
            });
            //底部合计行
            var rowNum = air2DensityJqGrid.getAllRecords().length;
            if (rowNum > 0) {
                $(".ui-jqgrid-sdiv").show();
                var Percents = parseFloat($("#Air2DensityJqGrid-datagrid").getCol("Percents", false, "sum") * 100 / rowNum).toFixed(1) + "%";
                jQuery("#Air2DensityJqGrid-datagrid").footerData("set", { "Percents": "<span style='color:red;float:left'>" + Percents + "</span>" }); //将合计值显示出来
            } else {
                $(".ui-jqgrid-sdiv").hide();
            }
            //隐藏水平滚动条
            jQuery("#Air2DensityJqGrid-datagrid").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });

        });
        //单元格-保存后
        air2DensityJqGrid.addListeners("afterSaveCell", function () {
            air2DensityJqGrid.refreshGrid('1=1'); //刷新
        });
        window.handleDeleteBurnt = function (id) {
            showConfirm("确认信息", "您确定删除此项记录？", function () {
                $.post(
                    "/Lab_Air2Density.mvc/Delete"
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        air2DensityJqGrid.reloadGrid();
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

        //--------------------------------------------活性指数------------------------------------------------------
        var activeJqGrid;
        activeJqGrid = new MyGrid({
        renderTo: 'ActiveJqGrid'
        , width: '100%'
        , buttons: buttons4
        , autoWidth: true
        , height: gGridHeight / 3-45
        , storeURL: '/Lab_Air2ActiveInfo.mvc/Find'
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
        , editSaveUrl: "/Lab_Air2ActiveInfo.mvc/Update"
        //, groupingView: { groupSummary: [true],groupColumnShow : [false], groupText: ['({1})'] }
        //, groupField: 'act1'
        ,footerrow : true
        , userDataOnFooter: true
        , altRows: true
        //, colNames: ['7d1', '7d2', '7d3', '28d1', '28d2', '28d3']
        , initArray: [
            { label: 'id', name: 'ID', index: 'ID', hidden: true }
            , { label: '检测ID', name: 'Lab_Air2OriginId', index: 'Lab_Air2OriginId', width: 60, hidden: true }
            , { label: '荷载', name: 'D7_1', index: 'D7_1', width: 40, editable: true, summaryType: 'avg', summaryTpl: '代表值(MPa):' }
            , { label: '单块值', name: 'D7_2', index: 'D7_2', width: 40, editable: true, summaryType: 'avg', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: "", defaulValue: "", decimalPlaces: 1 }, showSummaryOnHide: false }
            , { label: '代表值', name: 'D7_30', index: 'D7_30', width: 40, editable: false, hidden: false,cellattr: function(rowId, tv, rawObject, cm, rdata) {
                //合并单元格
                return 'id=\'D7_30' + rowId + "\'";
                //if (Number(rowId) < 5) { return ' colspan=2' }
                } }
            , { label: '荷载', name: 'D28_1', index: 'D28_1', width: 40, editable: true }
            , { label: '单块值', name: 'D28_2', index: 'D28_2', width: 40, editable: true, summaryType: 'avg', summaryTpl: '{0}', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: ",", defaulValue: "", decimalPlaces: 1} }
            , { label: '代表值', name: 'D28_30', index: 'D28_30', width: 40, editable: false, hidden: false }
            , { label: '荷载', name: 'S7_1', index: 'S7_1', width: 40, editable: true }
            , { label: '单块值', name: 'S7_2', index: 'S7_2', width: 40, editable: true, summaryType: 'avg', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: "", defaulValue: "", decimalPlaces: 1} }
            , { label: '代表值', name: 'S7_30', index: 'S7_30', width: 40, editable: false, hidden: false }
            , { label: '荷载', name: 'S28_1', index: 'S28_1', width: 40, editable: true }
            , { label: '单块值', name: 'S28_2', index: 'S28_2', width: 40, editable: true, summaryType: 'avg', summaryTpl: '{0}', formatter: "number", formatoptions: { thousandsSeparator: ",", defaulValue: "", decimalPlaces: 1} }
            , { label: '代表值', name: 'S28_30', index: 'S28_30', width: 40, editable: false, hidden: false }
            , { label: '7d', name: '7d', index: '7d', width: 40, editable: false }
            , { label: '28d', name: '28d', index: '28d', width: 40, editable: false }
            , { label: '操作', name: 'act', index: 'act', width: 140, sortable: false, align: "center" }
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
                var airoriginid = $("#Lab_Air2Origin_ID").val();
                if (airoriginid == "") {
                    showMessage('提示', '请先保存添加检测记录！');
                    return;
                }
                var airOriginId = $("#Lab_Air2Origin_ID").val(); //检测编号
                activeJqGrid.handleAdd({
                    loadFrom: 'ActiveInfoDiv',
                    //width: 320,
                    //height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_Air2ActiveInfo_Lab_Air2OriginId").val(airOriginId);
                    }
                });
            },
            handleEdit: function (btn) {
                var airOriginId = $("#Lab_Air2Origin_ID").val(); //检测编号
                activeJqGrid.handleEdit({
                    loadFrom: 'ActiveInfoDiv',
                    prefix: 'Lab_Air2ActiveInfo',
                    //width: 320,
                    //height: 330,
                    btn: btn,
                    afterFormLoaded: function () {
                        $("#Lab_Air2ActiveInfo_Lab_Air2OriginId").val(airOriginId); 
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

            var d7 = "" + parseFloat(S72 * 100 / d72).toFixed(0) + "%";
            var d28 = "" + parseFloat(S282 * 100 / d282).toFixed(0) + "%";

            jQuery("#ActiveJqGrid-datagrid").footerData("set", { "D7_30": d72, "D28_30": d282, "S7_30": S72, "S28_30": S282, "7d": "<span style='color:red;float:left;font-weight:normal;'><b>" + d7 + "</b></span>", "28d": "<span style='color:red;float:left;font-weight:normal;'><b>" + d28 + "</b></span>" }); //将合计值显示出来
        } else {
            $(".ui-jqgrid-sdiv").hide();
        }
        //隐藏水平滚动条
        jQuery("#ActiveJqGrid-datagrid").closest(".ui-jqgrid-bdiv").css({ 'overflow-x': 'hidden' });

        //var gridName = "ActiveJqGrid-datagrid";
        //Merger(gridName, 'D7_30');
    });
    //单元格-保存后
    activeJqGrid.addListeners("afterSaveCell", function () {
        activeJqGrid.refreshGrid('1=1');  //刷新
    });

    //二级表头合并
    jQuery("#ActiveJqGrid-datagrid").jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
                        { startColumnName: 'D7_1', numberOfColumns: 3, titleText: '7d' },
                        { startColumnName: 'D28_1', numberOfColumns: 3, titleText: '28d' },
                        { startColumnName: 'S7_1', numberOfColumns: 3, titleText: '7d' },
                        { startColumnName: 'S28_1', numberOfColumns: 3, titleText: '28d' }
            ]
    });
    //三级表头合并
    jQuery("#ActiveJqGrid-datagrid").jqGrid("setComplexGroupHeaders", {
        complexGroupHeaders: [
                              { startColumnName: 'D7_1', numberOfColumns: 6, titleText: '<em>对比胶砂</em>' },
                              { startColumnName: 'S7_1', numberOfColumns: 6, titleText: '试样胶砂' },
                              { startColumnName: '7d', numberOfColumns: 2, titleText: '活性指数（%）' }
			 ]
    });

    window.handleDeleteActive = function (id) {
        showConfirm("确认信息", "您确定删除此项记录？", function () {
            $.post(
                    "/Lab_Air2ActiveInfo.mvc/Delete"
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