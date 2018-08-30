
function IconLayerAndHotMarkIndexInit(opts) {

    var IconLayerGrid = new MyGrid({
        renderTo: 'IconLayerGriddiv'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.IconLayerStoreUrl
            , storeCondition: "IsUsed = 1"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 380
            , dialogHeight: 130
		    , showPageBar: true
		    , initArray: [
                  { name: 'ID', index: 'ID', hidden: true }
                , { label: '图层名称', name: 'Name', index: 'Name', width: 300 }
                , { label: '是否有效', name: 'IsUsed', index: 'IsUsed', align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '无效', '1': '有效' }, stype: 'select', searchoptions: { value: booleanSearchValues() }, width: 60 }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    IconLayerGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    IconLayerGrid.refreshGrid('1=1');
                },
                //已启用
                handleIsUsed: function () {
                    IconLayerGrid.refreshGrid("IsUsed = 1");
                },
                //已停用
                handleNotUsed: function () {
                    IconLayerGrid.refreshGrid("IsUsed = 0");
                },
                handleAdd: function (btn) {
                    IconLayerGrid.handleAdd({
                        loadFrom: 'IconLayerForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    IconLayerGrid.handleEdit({
                        loadFrom: 'IconLayerForm',
                        prefix: 'IconLayer',
                        btn: btn
                    });
                }
                , handleSetUsed: function (btn) {
                    var keys = IconLayerGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择要启用的图层!");
                        return;
                    }

                    showConfirm("确认信息", "确认要启用选中的图层?", function (btn) {
                        ajaxRequest(
                            opts.IconLayerStatusUrl,
                            { ids: keys, status: true },
                            true,
                            function (response) {
                                IconLayerGrid.reloadGrid();
                            }
                        );

                        $(this).dialog('close');
                    });
                }
                , handleSetUnused: function (btn) {
                    var keys = IconLayerGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择要停用的图层!");
                        return;
                    }

                    showConfirm("确认信息", "确认要停用选中的图层?", function (btn) {
                        ajaxRequest(
                            opts.IconLayerStatusUrl,
                            { ids: keys, status: false },
                            true,
                            function (response) {
                                IconLayerGrid.reloadGrid();
                            }
                        );

                        $(this).dialog('close');
                    });
                }
                , handleAddHotMark: function (btn) {

                    if (IconLayerGrid.isSelectedOnlyOne('请选择要在哪个图层上增加热点!')) {
                        var record = IconLayerGrid.getSelectedRecord();
                        HotMarkGrid.handleAdd({
                            loadFrom: 'HotMarkForm',
                            title: '增加热点，图层【' + record.Name + '】',
                            btn: btn,
                            afterFormLoaded: function () {
                                HotMarkGrid.setFormFieldValue('HotMark.IconLayerID', record.ID);
                            }
                        });
                    }
                }
            }
    });

    var HotMarkGrid = new MyGrid({
        renderTo: 'HotMarkGridDiv'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: opts.HotMarkStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 460
            , dialogHeight: 250
		    , showPageBar: true
		    , initArray: [
                  { name: 'ID', index: 'ID', hidden: true }
                , { name: 'IconLayerID', index: 'IconLayerID', hidden: true }
                , { label: '热点名称', name: 'Name', index: 'Name', width: 300 }
                , { label: '经度', name: 'Longtidue', index: 'Longtidue', width: 80 }
                , { label: '纬度', name: 'Latitude', index: 'Latitude', width: 80 }
                , { label: '热点图标', name: 'StyleClass', index: 'StyleClass', formatter: MapIconLayerFmt, unformat: booleanUnFmt, align: 'center', width: 60 }

		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    HotMarkGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    HotMarkGrid.refreshGrid('1=1');
                },
                handleEdit: function (btn) {
                    HotMarkGrid.handleEdit({
                        loadFrom: 'HotMarkForm',
                        btn: btn,
                        prefix: 'HotMark',
                        afterFormLoaded: function () {
                            var record = HotMarkGrid.getSelectedRecord();
                           
                            $(':radio[value = "' + record.StyleClass + '"]').attr("checked", "checked");
                        }
                    });
                }
                , handleDelete: function (btn) {
                    HotMarkGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
        var html = '';
        $.each(gDics["HotMarkIcon"], function (i, n) {
            if (i == 0) {               //默认选择第一个
                html += ' <span> <input type="radio" name="HotMark.StyleClass" value="' + n.ID + '" checked="checked" /><img  src="../../Content/erpimage/mapimage/map/' + n.DicName + '"  alt="' + n.TreeCode + '" /></span>';
            }
            else {
                html += ' <span> <input type="radio" name="HotMark.StyleClass" value="' + n.ID + '"/><img  src="../../Content/erpimage/mapimage/map/' + n.DicName + '"  alt="' + n.TreeCode + '" /></span>';
            }
            if ((i + 1) % 10 == 0) {
                html += "</br>";
            }
        });
        $(html).appendTo($("#IconArea"));


        IconLayerGrid.addListeners('rowclick', function (id, record, selBool) {
            HotMarkGrid.refreshGrid("IconLayerID='" + id + "'");
        });
}
 