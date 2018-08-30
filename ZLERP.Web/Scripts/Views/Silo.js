//库位信息
function siloIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'My-JqGrid'
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: 'rendertoButton'
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
            , editSaveUrl: opts.updateUrl
            , rowNumbers: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 50 }
                , { label: '筒仓名称', name: 'SiloName', index: 'SiloName', editable: true, editrules: { required: true} }
                , { label: '筒仓简码', name: 'SiloShortName', index: 'SiloShortName', editable: true, editrules: { required: true} }
                , { hidden: true, name: 'StuffID', index: 'StuffID' }
                , { label: '原料名称', name: 'StuffName', index: 'StuffInfo.StuffName' }
                , { label: '原料简称', name: 'StuffShortName', index: 'StuffInfo.StuffShortName' }
//                , { label: '机组', name: 'ProductLineName', index: 'ProductLineName', width: 50 }
                , { label: '当前容量(吨)', name: 'Content', index: 'Content', formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right', width: 80, summaryType: 'sum', summaryTpl: '小计:{0}L' }
                , { label: '最小容量(吨)', name: 'MinContent', index: 'MinContent', formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right', width: 80 }
                , { label: '最大容量(吨)', name: 'MaxContent', index: 'MaxContent', formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right', width: 80 }
                , { label: '最小报警容量(吨)', name: 'MinWarm', index: 'MinWarm', formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right', width: 100 }
                , { label: '最大报警容量(吨)', name: 'MaxWarm', index: 'MaxWarm', formatter: Kg2TFmt, unformat: T2KgFmt, align: 'right', width: 100 }
                , { label: '排序', name: 'OrderNum', index: 'OrderNum', search: false, align: 'center', width: 50, editable: true,editrules: { required: true, number: true } }

		    ]
            , functions: {
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.setFormFieldsUnEdit(false);
                    myJqGrid.handleAdd({
                        btn: btn
                        , loadFrom: 'My-FormDiv'
//                        , afterFormLoaded: function () {
//                           
//                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'My-FormDiv'
                        , afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(false);
                            myJqGrid.getFormField("Content_T").val(data.Content / 1000);
                            myJqGrid.getFormField("MinContent_T").val(data.MinContent / 1000);
                            myJqGrid.getFormField("MaxContent_T").val(data.MaxContent / 1000);
                            myJqGrid.getFormField("MaxWarm_T").val(data.MaxWarm / 1000);
                            myJqGrid.getFormField("MinWarm_T").val(data.MinWarm / 1000);
                            myJqGrid.setFormFieldDisabled('Content_T', true);
//                            myJqGrid.getFormField("Content_T").bind('blur', function () {
//                                var Content_T = myJqGrid.getFormField("Content_T").val();
//                                myJqGrid.getFormField("Content").val(Content_T * 1000);
//                            });
//                            myJqGrid.getFormField("MinContent_T").bind('blur', function () {
//                                var MinContent_T = myJqGrid.getFormField("MinContent_T").val();
//                                myJqGrid.getFormField("MinContent").val(MinContent_T * 1000);
//                            });
//                            myJqGrid.getFormField("MaxContent_T").bind('blur', function () {
//                                var MaxContent_T = myJqGrid.getFormField("MaxContent_T").val();
//                                myJqGrid.getFormField("MaxContent").val(MaxContent_T * 1000);
//                            });
//                            myJqGrid.getFormField("MaxWarm_T").bind('blur', function () {
//                                var MaxWarm_T = myJqGrid.getFormField("MaxWarm_T").val();
//                                myJqGrid.getFormField("MaxWarm").val(MaxWarm_T * 1000);
//                            });
//                            myJqGrid.getFormField("MinWarm_T").bind('blur', function () {
//                                var MinWarm_T = myJqGrid.getFormField("MinWarm_T").val();
//                                myJqGrid.getFormField("MinWarm").val(MinWarm_T * 1000);
//                            });
                        }

                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }, changeStuff: function (btn) {
                    myJqGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'My-FormDiv'
                        , afterFormLoaded: function () {
                            myJqGrid.setFormFieldsUnEdit(true);
                            myJqGrid.setFormFieldDisabled('StuffID', false);
                            myJqGrid.setFormFieldDisabled('ID', false);
                        }

                    });
                }
            }


    });

        $("#Content_T").bind('blur', function () {
            var Content_T = $("#Content_T").val();
            $("#Content").val(Content_T * 1000);
        });
        $("#MinContent_T").bind('blur', function () {
            var MinContent_T = $("#MinContent_T").val();
            $("#MinContent").val(MinContent_T * 1000);
        });
        $("#MaxContent_T").bind('blur', function () {
            var MaxContent_T = $("#MaxContent_T").val();
            $("#MaxContent").val(MaxContent_T * 1000);
        });
        $("#MaxWarm_T").bind('blur', function () {
            var MaxWarm_T = $("#MaxWarm_T").val();
            $("#MaxWarm").val(MaxWarm_T * 1000);
        });
        $("#MinWarm_T").bind('blur', function () {
            var MinWarm_T = $("#MinWarm_T").val();
            $("#MinWarm").val(MinWarm_T * 1000);
        });

        //机组下拉框选择事件
        $("#ProductLineID").bind("change", function (e) {
            var pvalue = $(this).val();
            var condition = "1=1";
            if (pvalue) {
                condition = "SiloID IN (SELECT SiloID from SiloProductLine where ProductLineID = '" + pvalue + "')";
            }
            myJqGrid.refreshGrid(condition);
        });
}