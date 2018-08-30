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
            , groupField: 'ProjectName'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupCollapse: true, groupOrder: ['desc']}
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: ' 路程编号', name: 'ID', index: 'ID', editable: false, editrules: { required: true } }
                , { label: ' 工程名称', name: 'ProjectName', index: 'ProjectName', width: 100, editable: false }
                , { label: ' 浇筑方式', name: 'CastModeName', index: 'CastModeName', width: 100, editable: false }
                , { label: ' 路程(m)', name: 'distance', index: 'distance', width: 100, editable: true, editrules: { required: true} }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    ProjectGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ProjectGrid.refreshGrid('1=1');
                },
                handleEdit: function (btn) {
                    ProjectGrid.handleEdit({
                        loadFrom: 'projectForm',
                        btn: btn
                    });
                },
                handleAdd: function (btn) {
                    ProjectGrid.handleAdd({
                        loadFrom: 'projectForm',
                        btn: btn
                    });
                }
            }
    });

}