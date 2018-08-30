//合同工程明细
function projectIndexInit(options) {
    var ProjectGrid = new MyGrid({
        renderTo: 'ProjectGrid'
        //, width: '100%'
            , title: '合同工程明细'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
            , editSaveUrl: options.projectUpdateUrl
		    , sortByField: 'ID'
            , groupField: 'ContractName'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupCollapse: true, groupOrder: ['desc'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: ' 工程编号', name: 'ID', index: 'ID', editable: false, editrules: { required: true } }
                , { label: ' 项目地址', name: 'ProjectAddr', index: 'ProjectAddr', width: 100, editable: true }
                , { label: ' 工程名称', name: 'ProjectName', index: 'ProjectName', width: 100, editable: true, editrules: { required: true} }
                , { label: ' 经度', name: 'Longitude', index: 'Longitude', hidden: true }
                , { label: ' 纬度', name: 'Latitude', index: 'Latitude', hidden: true }
                , { label: ' 工地范围', name: 'PlaceRange', index: 'PlaceRange', width: 50, editable: true }
                , { label: ' 误差值', name: 'ErrorValue', index: 'ErrorValue', width: 50, editable: true }
                , { label: ' 顺序', name: 'CPOrder', index: 'CPOrder', width: 40, editable: true, editrules: { required: true} }
                , { label: ' 是否显示', name: 'IsShow', index: 'IsShow', width: 50, editrules: { required: true }, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: ' 工程运距', name: 'Distance', width: 60, index: 'Distance', editable: true }
                , { label: ' 建设单位', name: 'BuildUnit', index: 'BuildUnit', editable: true }
                , { label: ' 施工单位', name: 'ConstructUnit', index: 'ConstructUnit', editable: true }
                , { label: '工地联系人', name: 'LinkMan', index: 'LinkMan', editable: true }
                , { label: ' 工地电话', name: 'Tel', index: 'Tel', editable: true }
                , { label: ' 合同编号', name: 'ContractID', width: 80, index: 'ContractID', editable: false }
                , { label: ' 合同名称', name: 'ContractName', index: 'ContractName', editable: false }
                , { label: ' 备注', name: 'Remark', index: 'Remark', editable: true }
        //, { label: ' 经度', name: 'Longitude', index: 'Longitude', editable: true }
        //, { label: ' 纬度', name: 'Latitude', index: 'Latitude', editable: true }
        //, { label: ' 工地范围', name: 'PlaceRange', index: 'PlaceRange', editable: true }
        //, { label: ' 误差值', name: 'ErrorValue', index: 'ErrorValue', editable: true }
        //, { label: ' 顺序', name: 'CPOrder', index: 'CPOrder', editable: true, editrules: { required: true} }
        //, { label: ' 是否显示', name: 'IsShow', index: 'IsShow', editable: true, editrules: { required: true} }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    ProjectGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ProjectGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    ProjectGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEnable: function (btn) {
                    if (!ProjectGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录");
                        return false;
                    }
                    var keys = ProjectGrid.getSelectedKeys();
                    ajaxRequest("/Project.mvc/MapEnable", { ids: keys }, true, function (data) {
                        ProjectGrid.reloadGrid();
                    });
                },
                handleDisable: function (btn) {
                    if (!ProjectGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条记录");
                        return false;
                    }
                    var keys = ProjectGrid.getSelectedKeys();
                    ajaxRequest("/Project.mvc/MapDisable", { ids: keys }, true, function (data) {
                        ProjectGrid.reloadGrid();
                    });
                },
                handleEdit: function (btn) {
                    ProjectGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    ProjectGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}