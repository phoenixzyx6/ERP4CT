function formulaIndexInit(opts) {
    var filterCondition = "LifeCycle != -1";
    var FormulaGrid = new MyGrid({
        renderTo: 'FormulaGridDiv'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridWithTitleHeight
		    , storeURL: opts.storeUrl
            , storeCondition: filterCondition
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 720
            , dialogHeight: 600
		    , setGridPageSize: 30
            , groupField: 'FormulaType'
            , groupingView: { groupText: ['{0}(<b>{1}</b>)'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
            , singleSelect: true
		    , showPageBar: true
		    , initArray: [
                 { label: ' 理论配比编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 理论配比名称', name: 'FormulaName', index: 'FormulaName', width: 80 }
                , { label: '料种', name: 'FormulaType', index: 'FormulaType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'FType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('FType')} }
                , { label: ' 砼强度', name: 'ConStrength', index: 'ConStrength', width: 80, stype: 'select', searchoptions: { dataUrl: opts.conStrengthSelectUrl} }
                , { label: ' 坍落度', name: 'Slump', index: 'Slump', width: 80 }
                , { label: ' 浇筑方式', name: 'CastMode', index: 'CastMode', width: 80 }
                , { label: ' 水泥等级', name: 'CementBreed', index: 'CementBreed', width: 80, search: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ST'} }
                , { label: ' 设计容重', name: 'Weight', index: 'Weight', width: 60, align: 'right', formatter: rzFmt, unformat: rzUnFmt }
                , { label: ' 调整容重', name: 'TuneWeight', index: 'TuneWeight', width: 60, align: 'right', formatter: rzFmt, unformat: rzUnFmt }
                , { label: ' 水灰比', name: 'WCRate', index: 'WCRate', width: 60, align: 'right' }
                , { label: ' 砂率', name: 'SCRate', index: 'SCRate', width: 50, align: 'right' }
                , { label: ' 所属季节', name: 'SeasonType', index: 'SeasonType', width: 80, stype: 'select', searchoptions: { value: dicToolbarSearchValues('Season')} }
                , { label: ' 混凝土类别', name: 'CementType', index: 'CementType', width: 80 }
                , { label: ' 细度模数', name: 'Mesh', index: 'Mesh', width: 60 }
                , { label: ' 抗渗等级', name: 'ImpGrade', index: 'ImpGrade', width: 60 }
                , { label: ' 抗折强度', name: 'FlexStrength', index: 'FlexStrength', width: 60 }
                , { label: ' 骨料粒径', name: 'CarpRadii', index: 'CarpRadii', search: false }
                , { label: ' 砼标记', name: 'BetonTag', index: 'BetonTag', search: false }
                , { label: ' 搅拌时间', name: 'MixingTime', index: 'MixingTime', align: 'center', width: 60, search: false }
                , { label: ' 用途', name: 'Purpose', index: 'Purpose', search: false }
                , { label: ' 备注', name: 'Remark', index: 'Remark', search: false }
                , { label: ' 理论配比编号', name: 'ID', index: 'ID', search: false }
                , { label: ' 状态', name: 'Lifecycle', index: 'Lifecycle', search: false, hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    FormulaGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    FormulaGrid.refreshGrid(filterCondition);
                },
                handleAdd: function (btn) {
                    FormulaGrid.handleAdd({
                        btn: btn
                        , loadFrom: 'FormulaFormDiv'
                        , closeDialog: false
                        , afterFormLoaded: function () {
                            var formulaid = FormulaGrid.getFormField("Formula.ID").val();
                            mysubGrid.refreshGrid("FormulaID='" + formulaid + "'");

                            FormulaGrid.setFormFieldReadOnly("Formula.ID", false);
                            //                            FormulaGrid.setFormFieldReadOnly("Formula.FormulaName", true);
                        }
                        , postCallBack: function (response) {
//                            alert(this);
                            if (FormulaGrid.getFormField("Formula.ID").val().length > 0) {
                                $("#FormulaFormDiv").dialog('close'); //如果隐藏控件里面有值了就直接关闭，传后台也是如果有值说明是更新而不是新增。
                            }
                            else if (response.Result) {
                                var uid = response.Data; //Data是id
                                FormulaGrid.getFormField("Formula.ID").val(uid);
                                var postUrl = opts.getFormulaUrl;
                                if (uid) {
                                    var postParams = { id: uid }; //调的是Get方法 参数id
                                    setFormValuesWithPre("FormulaFormDiv", postUrl, postParams, this, "Formula"); //现在看起来好像没什么用了
                                }
                                showMessage("消息", "请根据表格填写配比用量");
                                mysubGrid.refreshGrid("FormulaID='" + uid + "'");
                            }
                        }
                    });
                },
                handleEdit: function (btn) {
                    FormulaGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'FormulaFormDiv',

                        prefix: 'Formula',
                        afterFormLoaded: function () {
                            var formulaid = FormulaGrid.getFormField("Formula.ID").val();
                            mysubGrid.refreshGrid("FormulaID='" + formulaid + "'");

                            FormulaGrid.setFormFieldReadOnly("Formula.ID", true);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    FormulaGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , postCallBack: function (response) {
                            FormulaGrid.reloadGrid();
                            ItemsGrid.reloadGrid();
                        }
                    });
                }
                , handleHide: function (btn) {
                    if (!FormulaGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = FormulaGrid.getSelectedRecord();
                    var lifeCycleValue = selectedRecord.Lifecycle;
                    if (lifeCycleValue == -1) {
                        showMessage('提示', '您选择的记录已是隐藏状态！');
                        return;
                    }
                    ajaxRequest(
                        btn.data.Url,
                        {
                            "ID": selectedRecord.ID,
                            "Lifecycle": -1

                        },
                        true,
                        function () {
                            FormulaGrid.refreshGrid();
                        }
                    );
                }
                , handleShow: function (btn) {
                    if (!FormulaGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = FormulaGrid.getSelectedRecord();
                    var lifeCycleValue = selectedRecord.Lifecycle;
                    if (lifeCycleValue == 0) {
                        showMessage('提示', '您选择的记录已是显示状态！');
                        return;
                    }
                    ajaxRequest(
                        btn.data.Url,
                        {
                            "Formula.ID": selectedRecord.ID,
                            "Formula.Lifecycle": 0

                        },
                        true,
                        function () {
                            FormulaGrid.refreshGrid();
                        }
                    );
                }
                , handleViewHidden: function (btn) {
                    FormulaGrid.refreshGrid("LifeCycle = -1");
                }
            }
    });

    //右边的
    var ItemsGrid = new MyGrid({
        renderTo: 'ItemsGridDiv'
		    , title: '材料用量列表'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridWithTitleHeight
		    , storeURL: opts.formulaItemStoreUrl
		    , sortByField: 'StuffType.OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
        //, groupField: 'UserType'
		    , setGridPageSize: 30
		    , showPageBar: true
            , storeCondition: '1<>1'
        //, singleSelect: true
            , editSaveUrl: opts.itemUpdateUrl
            , autoLoad: false
		    , initArray: [
                { label: ' 子项编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 理论配比编号', name: 'FormulaID', index: 'FormulaID', hidden: true }
                , { label: ' 材料类型编号', name: 'StuffTypeID', index: 'StuffTypeID', hidden: true }
                , { label: ' 材料类型', name: 'StuffTypeName', index: 'StuffType.StuffTypeName' }
                , { label: ' 材料用量', name: 'StuffAmount', index: 'StuffAmount', width: 80, editable: true }
                , { label: ' 标准用量', name: 'StandardAmount', index: 'StandardAmount', width: 80, hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    ItemsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    var formulaid = FormulaGrid.getSelectedKey();
                    ItemsGrid.refreshGrid("FormulaID='" + formulaid + "'");
                },
                handleAdd: function (btn) {
                    var formulaid = FormulaGrid.getSelectedKey();
                    if (formulaid.length > 0) {
                        ItemsGrid.handleAdd({
                            btn: btn
                        , loadFrom: 'ItemsDiv'

                        , afterFormLoaded: function () {
                            ItemsGrid.setFormFieldValue('ForItem.FormulaID', formulaid);
                            ItemsGrid.setFormFieldReadOnly('ForItem.FormulaID', true);
                        }
                        , postCallBack: function (response) {
                            ItemsGrid.refreshGrid("FormulaID='" + formulaid + "'");
                            FormulaGrid.reloadGrid();
                        }
                        });
                    }
                    else {
                        showError("警告", "请在左侧选择一条配比信息");
                    }
                },
                handleEdit: function (btn) {
                    ItemsGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'ItemsDiv'
                        , postCallBack: function (response) {
                            FormulaGrid.reloadGrid();
                        }
                    });
                }
                , handleDelete: function (btn) {
                    ItemsGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , postCallBack: function (response) {
                            FormulaGrid.reloadGrid();
                        }
                    });
                }
                , handleExport: function (btn) {
                    var formulaid = FormulaGrid.getSelectedKey();
                    if (formulaid.length > 0) {
                        ajaxRequest(opts.exportStuffUrl, { formulaid: formulaid }, false, function (response) {
                            if (response.Result) {
                                showMessage("消息", "请根据表格填写配比用量");
                                ItemsGrid.refreshGrid("FormulaID='" + formulaid + "'");
                                FormulaGrid.reloadGrid();
                            }
                            else {
                                showError("错误", response.Message);
                            }
                        }, null, btn);
                    }
                    else {
                        showMessage("提示", "请先保存配比信息");
                    }
                }
            }
        });




        ItemsGrid.addListeners("afterSubmitCell", function () {
            FormulaGrid.reloadGrid();
        });

        //弹出框内部
    var mysubGrid = new MyGrid({
        renderTo: 'SubGridDiv'
		    , title: '材料用量列表'
            , width: 700
        //, autoWidth: true
            , buttons: buttons2
            , height: 150
		    , storeURL: opts.formulaItemStoreUrl
		    , sortByField: 'StuffType.OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
        //, groupField: 'UserType'
		    , setGridPageSize: 30
		    , showPageBar: true 
        //, singleSelect: true
            , editSaveUrl: opts.itemUpdateUrl
            , autoLoad: false
		    , initArray: [
                { label: ' 子项编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 理论配比编号', name: 'FormulaID', index: 'FormulaID', hidden: true }
                , { label: ' 材料类型编号', name: 'StuffTypeID', index: 'StuffTypeID', hidden: true }
                , { label: ' 材料类型', name: 'StuffTypeName', index: 'StuffTypeName', sortable: false }
                , { label: ' 材料用量', name: 'StuffAmount', index: 'StuffAmount', width: 80, editable: true }
                , { label: ' 标准用量', name: 'StandardAmount', index: 'StandardAmount', width: 80, editable: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    var formulaid = FormulaGrid.getFormField("Formula.ID").val();
                    mysubGrid.refreshGrid("FormulaID='" + formulaid + "'");
                },
                handleRefresh: function (btn) {
                    var formulaid = FormulaGrid.getFormField("Formula.ID").val();
                    mysubGrid.refreshGrid("FormulaID='" + formulaid + "'");
                },
                handleAdd: function (btn) {
                    var formulaid = FormulaGrid.getFormField("Formula.ID").val();
                    if (formulaid.length > 0) {
                        mysubGrid.handleAdd({
                            btn: btn
                        , loadFrom: 'ItemsDiv'
                        , afterFormLoaded: function () {
                            mysubGrid.setFormFieldValue('ForItem.FormulaID', formulaid);
                            mysubGrid.setFormFieldReadOnly('ForItem.FormulaID', true);
                        }
                        , postCallBack: function (response) {
                            mysubGrid.refreshGrid("FormulaID='" + formulaid + "'");
                            FormulaGrid.reloadGrid();
                        }
                        });
                    }
                    else {
                        showError("警告", "请先保存配比信息");
                    }
                },
                handleEdit: function (btn) {
                    mysubGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'ItemsDiv'
                         , postCallBack: function (response) {
                             FormulaGrid.reloadGrid();
                         }
                    });
                }
                , handleDelete: function (btn) {
                    mysubGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleExport: function (btn) {
                    var formulaid = FormulaGrid.getFormField("Formula.ID").val();
                    if (formulaid) {
                        ajaxRequest(opts.exportStuffUrl, { formulaid: formulaid }, true, function (response) {
                            if (response.Result) {
                                showMessage("消息", "请根据表格填写配比用量");
                                mysubGrid.refreshGrid("FormulaID='" + formulaid + "'");
                            }
                            else {
                                showError("消息", response.Message);
                            }
                        }, null, btn);
                    }
                    else {
                        showMessage("提示", "请先保存配比信息");
                    }
                }
            }
    });
    var _selectedFormulaID = null;
    FormulaGrid.addListeners('rowclick', function (id, record, selBool) {
        ItemsGrid.getJqGrid().setCaption("材料用量列表：" + record.FormulaName + "&nbsp;<font color='#2E6E9E'>－</font>&nbsp;" + record.ConStrength);
        ItemsGrid.refreshGrid("FormulaID='" + id + "'");

        _selectedFormulaID = id;
    });
    FormulaGrid.addListeners('gridComplete', function () {
        if (_selectedFormulaID) {
            FormulaGrid.getJqGrid().jqGrid("setSelection", _selectedFormulaID);
        }
    });

    $("#CEAmount_R").bind('blur', function () {
        $("#CEAmount_E").val($("#CEAmount_R").val());
    });

    $("#WaAmount_S").bind('blur', function () {
        var wc = $("#Formula_WCRate").val();
        var mw = $("#WaAmount_S").val();
        var c0 = 0;
        $("#WaAmount_R").val(mw);
        $("#WaAmount_E").val(mw);
        if (wc > 0) {
            c0 = (Math.round(mw / wc * 100)) / 100;
            $("#CEAmount_S").val(c0);
            $("#CEAmount_R").val(c0);
            $("#CEAmount_E").val(c0);
        }
        else {
            showMessage("消息", "请根据计算结果填写水灰比");
        }
    });

    $("#Formula_SCRate").bind('blur', function () {
        var scrate = $("#Formula_SCRate").val();

        if (scrate > 0) {
            var weight = $("#Formula_Weight").val();
            var mc0 = $("#CEAmount_S").val();
            var mw0 = $("#WaAmount_S").val();
            var gs = weight - mc0 - mw0;

            var ms0 = Math.round(scrate * gs) / 100;
            var mg0 = Math.round((gs - ms0) * 100) / 100;

            $("#CaAmount_S").val(mg0);
            $("#CaAmount_R").val(mg0);
            $("#CaAmount_E").val(mg0);

            $("#FaAmount_S").val(ms0);
            $("#FaAmount_R").val(ms0);
            $("#FaAmount_E").val(ms0);
        }
        else {
            showMessage("消息", "请根据右表填写砂率信息");
        }

    });

    $("#AIR1Rate").bind('blur', function () {
        var rate1 = $("#AIR1Rate").val();
        if (rate1 >= 0) {
            var mc0 = $("#CEAmount_R").val();

            var Quo = $("#AIR1Quo").val();

            var mf0 = Math.round(mc0 * rate1 * Quo) / 100;

            $("#AIR1Amount_S").val(mf0);
            $("#AIR1Amount_R").val(mf0);
            $("#AIR1Amount_E").val(mf0);

            $("#CEAmount_S").val(Math.round((mc0 - mf0) * 100) / 100);
            //$("#CEAmount_R").val(Math.round((mc0 - mf0) * 100) / 100);
            //$("#CEAmount_E").val(Math.round((mc0 - mf0) * 100) / 100);
        }
        else {
            showMessage("消息", "请填写掺合料取代率");
        }
    });

    $("#AIR2Rate").bind('blur', function () {
        var rate1 = $("#AIR2Rate").val();
        if (rate1 >= 0) {
            var mc0 = $("#CEAmount_R").val();

            var mk0 = Math.round(mc0 * rate1) / 100;

            $("#AIR2Amount_S").val(mk0);
            $("#AIR2Amount_R").val(mk0);
            $("#AIR2Amount_E").val(mk0);

            $("#CEAmount_S").val(Math.round((mc0 - mk0) * 100) / 100);
            //$("#CEAmount_R").val(Math.round((mc0 - mk0) * 100) / 100);
            //$("#CEAmount_E").val(Math.round((mc0 - mk0) * 100) / 100);
        }
        else {
            showMessage("消息", "请填写掺合料取代率");
        }
    });

    $("#ADM1Rate").bind('blur', function () {
        var rate = $("#ADM1Rate").val();
        if (rate >= 0) {
            var mc0 = $("#CEAmount_S").val();

            var ma1 = Math.round(mc0 * rate) / 100;

            $("#ADM1Amount_R").val(ma1);
            $("#ADM1Amount_S").val(ma1);
            $("#ADM1Amount_E").val(ma1);
        }
        else {
            showMessage("消息", "请填写外加剂一掺量");
        }
    });

    $("#ADM2Rate").bind('blur', function () {
        var rate = $("#ADM2Rate").val();
        if (rate >= 0) {
            var mc0 = $("#CEAmount_S").val();

            var ma1 = Math.round(mc0 * rate) / 100;

            $("#ADM2Amount_R").val(ma1);
            $("#ADM2Amount_S").val(ma1);
            $("#ADM2Amount_E").val(ma1);
        }
        else {
            showMessage("消息", "请填写外加剂二掺量");
        }
    });



    $("#WaAmount_R").bind('blur', function () {
        $("#WaAmount_E").val($("#WaAmount_R").val());
    });



    $("#CaAmount_R").bind('blur', function () {
        $("#CaAmount_E").val($("#CaAmount_R").val());
    });

    $("#FaAmount_R").bind('blur', function () {
        $("#FaAmount_E").val($("#FaAmount_R").val());
    });

    $("#AIR1Amount_R").bind('blur', function () {
        $("#AIR1Amount_E").val($("#AIR1Amount_R").val());
    });

    $("#AIR2Amount_R").bind('blur', function () {
        $("#AIR2Amount_E").val($("#AIR2Amount_R").val());
    });


    $("#ADM1Amount_R").bind('blur', function () {
        $("#ADM1Amount_E").val($("#ADM1Amount_R").val());
    });

    $("#ADM2Amount_R").bind('blur', function () {
        $("#ADM2Amount_E").val($("#ADM2Amount_R").val());
//        $("#TAmount_R").val(parseFloat($("#CEAmount_R").val())+ parseFloat($("#WaAmount_R").val())
//        + $("#CaAmount_R").val() + $("#FaAmount_R").val()
//        + $("#AIR1Amount_R").val() + $("#AIR2Amount_R").val()
//        + $("#AIR2Amount_R").val() + $("#AIR2Amount_R").val()
//        + $("#ADM1Amount_R").val() + $("#ADM2Amount_R").val()
//        );
    });

    $("#TAmount_S").bind('blur', function () {
        var amount=$("#Formula_Weight").val();
        $("#TAmount_S").val(amount);
        $("#TAmount_R").val(amount);
        $("#TAmount_E").val(amount);
    });

}

window.SaveFormula = function () {
    var FormulaName = $("#FormulaName").val();
    var FormulaType = $("#FormulaType").val();
    var ConStrength = $("#ConStrength").val();


    var data = $("#FormulaDesignDiv form").serialize();
    //data是FormulaDesignModel序列化对象

    $.post("/Formula.mvc/SaveDeFormula", data, function (response) {
        if (response.Result) {
            showMessage("消息", response.Message);
        }
    });
}

