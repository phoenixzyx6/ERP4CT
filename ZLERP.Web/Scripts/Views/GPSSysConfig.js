function sysConfigInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'grid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: "ConfigType ='GPS'"
		    , sortByField: 'ID'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
		    , dialogWidth: 500
            , dialogHeight: 260
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , editSaveUrl: opts.updateUrl
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 50 }
                , { label: '配置名称', name: 'ConfigName', index: 'ConfigName' }
                , { label: '数据类型', name: 'DateType', index: 'DateType', width: 60 }
                , { label: '数据值', name: 'ConfigValue', index: 'ConfigValue', editable: true, width: 120 }
                , { label: '配置描述', name: 'ConfigInfo', index: 'ConfigInfo', width: 300 }
                , { label: '配置类别', name: 'ConfigType', index: 'ConfigType', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'SetClass'} }
                , { label: '排序', name: 'OrderNum', index: 'OrderNum', search: false, width: 30, align: 'center', width: 60 }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        btn: btn,
                        loadFrom: 'MyFormDiv',
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('ConfigName', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'MyFormDiv',
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('ConfigName', true);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleRefreshCache: function (btn) {
                    ajaxRequest(btn.data.Url, null, true);
                }

            }
        });


        window.handleAddSys = function (btn) {
            var btn1 = [];
            btn1.data = [];
            btn1.data.Url = "/SysConfig.mvc/Add";
            btn1.data.FuncDesc = "系统全局配置/增加";
            myJqGrid.handleAdd({
                btn: btn1,
                loadFrom: 'MyFormDiv',
                afterFormLoaded: function () {
                    myJqGrid.setFormFieldDisabled('ConfigName', false);
                }
            });
        };
}