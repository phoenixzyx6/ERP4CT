function stuffInfoIndexInit(opts) {
    var myJqGrid = new MyGrid({
            renderTo: 'MyJqGrid'
            //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: 'IsUsed= 1'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , groupField: 'StuffTypeName'
            , dialogWidth: 530
            , dialogHeight: 450
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , groupingView: { groupSummary: [true], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: ' 原料编号', name: 'ID', index: 'ID', width: 100 }
                , { label: ' 原料名称', name: 'StuffName', index: 'StuffName', width: 100 }
                , { label: ' 原料简码', name: 'StuffShortName', index: 'StuffShortName', width: 100 }
                , { label: ' 材料类型', name: 'StuffTypeName', index: 'StuffTypeID', width: 100 }
                , { label: '材料品种', name: 'Spec', index: 'Spec', search: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ST'} }
                , { label: ' 材料类型编号', name: 'StuffTypeID', index: 'StuffTypeID', hidden: true, sortable: false }
                , { label: '厂商代号', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: '厂商', name: 'SupplyName', index: 'SupplyID' }
                , { label: ' 库存量(吨)', name: 'Inventory', index: 'Inventory', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100, summaryType: 'sum', summaryTpl: '小计:{0}' }
                , { label: ' 当前价格(元/吨)', name: 'Price', index: 'Price', align: 'right', width: 120, formatter: T2KgFmt, unformat: Kg2TFmt, summaryType: 'avg', summaryTpl: '均价:{0}' }
                , { label: '换算系数', name: 'SopRate', index: 'SopRate', width: 80 }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '是否过磅', name: 'IsMetage', index: 'IsMetage', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '来源地', name: 'RootIn', index: 'RootIn' }
                , { label: '尺寸', name: 'Sizex', index: 'Sizex' }
                , { label: '砂水常数X', name: 'SWConstantX', index: 'SWConstantX', width: 80 }
                , { label: '砂水常数Y', name: 'SWConstantY', index: 'SWConstantY', width: 80 }
                , { label: '氯含量', name: 'Chlorin', index: 'Chlorin', width: 80 }
                , { label: '碱含量', name: 'Alkali', index: 'Alkali', width: 80 }
                , { label: '比重', name: 'Proportion', index: 'Proportion', width: 80 }
                , { label: ' 最小报警库存', name: 'MinWarmContent', index: 'MinWarmContent', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: ' 最大报警库存', name: 'MaxWarmContent', index: 'MaxWarmContent', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '明扣', name: 'BrightRate', index: 'BrightRate', width: 80 }
                , { label: '暗扣', name: 'DarkRate', index: 'DarkRate', width: 80 }
                , { label: '试验类别', name: 'TestType', index: 'TestType' }
                , { label: ' 排序', name: 'OrderNum', index: 'OrderNum', width: 80 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('IsUsed= 1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {

                            myJqGrid.setFormFieldsUnEdit(false);
                            myJqGrid.getFormField("Inventory_T").bind('blur', function () {
                                var Inventory_T = myJqGrid.getFormField("Inventory_T").val();
                                myJqGrid.getFormField("Inventory").val(Inventory_T * 1000);
                            });
                            myJqGrid.getFormField("MinWarmContent_T").bind('blur', function () {
                                var MinWarmContent_T = myJqGrid.getFormField("MinWarmContent_T").val();
                                myJqGrid.getFormField("MinWarmContent").val(MinWarmContent_T * 1000);
                            });
                            myJqGrid.getFormField("MaxWarmContent_T").bind('blur', function () {
                                var MaxWarmContent_T = myJqGrid.getFormField("MaxWarmContent_T").val();
                                myJqGrid.getFormField("MaxWarmContent").val(MaxWarmContent_T * 1000);
                            });

                            myJqGrid.getFormField("Price_T").bind('blur', function () {
                                var Price_T = myJqGrid.getFormField("Price_T").val();
                                myJqGrid.getFormField("Price").val(Price_T / 1000);
                            });
                        },
                        beforeSubmit: beforeFormSubmit
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(false);
                            //myJqGrid.setFormFieldDisabled('IsUsed', true);

                            //myJqGrid.setFormFieldDisabled('Price', true);
                            //myJqGrid.setFormFieldDisabled('Inventory', true);
                            //myJqGrid.setFormFieldDisabled('BrightRate', true);
                            //myJqGrid.setFormFieldDisabled('DarkRate', true);
                            myJqGrid.setFormFieldDisabled('Inventory_T', true);
                            myJqGrid.getFormField("Inventory_T").val(data.Inventory / 1000);
                            myJqGrid.getFormField("MinWarmContent_T").val(data.MinWarmContent / 1000);
                            myJqGrid.getFormField("MaxWarmContent_T").val(data.MaxWarmContent / 1000);
                            myJqGrid.getFormField("Price_T").val(data.Price * 1000);

                            myJqGrid.getFormField("Inventory_T").bind('blur', function () {
                                var Inventory_T = myJqGrid.getFormField("Inventory_T").val();
                                myJqGrid.getFormField("Inventory").val(Inventory_T * 1000);
                            });
                            myJqGrid.getFormField("MinWarmContent_T").bind('blur', function () {
                                var MinWarmContent_T = myJqGrid.getFormField("MinWarmContent_T").val();
                                myJqGrid.getFormField("MinWarmContent").val(MinWarmContent_T * 1000);
                            });
                            myJqGrid.getFormField("MaxWarmContent_T").bind('blur', function () {
                                var MaxWarmContent_T = myJqGrid.getFormField("MaxWarmContent_T").val();
                                myJqGrid.getFormField("MaxWarmContent").val(MaxWarmContent_T * 1000);
                            });

                            myJqGrid.getFormField("Price_T").bind('blur', function () {
                                var Price_T = myJqGrid.getFormField("Price_T").val();
                                myJqGrid.getFormField("Price").val(Price_T / 1000);
                            });
                        },
                        beforeSubmit: beforeFormSubmit
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleStop: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(true);
                            myJqGrid.setFormFieldDisabled('IsUsed', false);
                            myJqGrid.setFormFieldDisabled('ID', false);
                        }
                    });
                },
                handleEditPrice: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(true);

                            myJqGrid.setFormFieldDisabled('Price', false);
                            myJqGrid.setFormFieldDisabled('Price_T', false);
                            myJqGrid.setFormFieldDisabled('ID', false);

                            myJqGrid.getFormField("Price_T").val(data.Price * 1000);
                            myJqGrid.getFormField("Price_T").bind('blur', function () {
                                var Price_T = myJqGrid.getFormField("Price_T").val();
                                myJqGrid.getFormField("Price").val(Price_T / 1000);
                            });
                        }
                    });
                },
                handleEditInventory: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.getFormField("Inventory_T").bind('blur', function () {
                                var Inventory_T = myJqGrid.getFormField("Inventory_T").val();
                                myJqGrid.getFormField("Inventory").val(Inventory_T * 1000);
                            });
                            myJqGrid.setFormFieldsUnEdit(true);
                            myJqGrid.setFormFieldDisabled('Inventory', false);
                            myJqGrid.setFormFieldDisabled('Inventory_T', false);
                            myJqGrid.getFormField("Inventory_T").val(data.Inventory / 1000);
                            
                            myJqGrid.getFormField("Inventory").val(data.Inventory);
                            myJqGrid.setFormFieldDisabled('ID', false);
                        }
                    });
                },
                handleEditBrightRate: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(true);
                            myJqGrid.setFormFieldDisabled('BrightRate', false);
                            myJqGrid.setFormFieldDisabled('ID', false);
                        }
                    });
                },
                handleEditDarkRate: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(true);
                            myJqGrid.setFormFieldDisabled('DarkRate', false);
                            myJqGrid.setFormFieldDisabled('ID', false);
                        }
                    });
                },
                //已启用
                handleIused: function () {
                    myJqGrid.refreshGrid("IsUsed = 1");
                },
                //已停用
                handleNotUsed: function () {
                    myJqGrid.refreshGrid("IsUsed = 0");
                }
            }
    });
    //
    $('#StuffTypeID').bind('change', function () {
        if ($(this).val() == "CE") {
            $('#Spec').addClass('requiredval');
        }
        else {
            $('#Spec').removeClass('requiredval');
        }
    });
    //
    function beforeFormSubmit(formId) {

        var stuffType = myJqGrid.getFormFieldValue("StuffTypeID");
        var spec = myJqGrid.getFormFieldValue("Spec");

        if (stuffType == "CE" && spec == "") {
            $("select[name='Spec']").addClass("input-validation-error");
            showError('验证错误', '水泥必须指定材料品种');
            return false;
        }
        $("select[name='Spec']").removeClass("input-validation-error");

        //提交修改前先检查该原料是不是已经使用，如果已使用就禁止修改
        var stuffId = myJqGrid.getFormFieldValue("ID");

        return true;
    }


    //ztree树列表
    var treeSettings = {
        check: {
            enable: true
        },
        view: { selectedMulti: false },
        data: {
            simpleData: {
                enable: true
            },
            key: { title: 'title' }
        },
        async: {
            enable: true,
            url: opts.TreeUrl,
            autoParam: ["id", "name", "level"]
            // otherParam: { "otherParam": "zTreeAsyncTest" }　
        },
        callback: {
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            }
        }
    };
    $.fn.zTree.init($('#sysFuncs'), treeSettings);
}