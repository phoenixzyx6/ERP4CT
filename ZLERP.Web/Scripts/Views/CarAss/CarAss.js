function carassIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '辅助单号', name: 'ID', index: 'ID', width: 90 }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 90 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProduceTask.ProjectName'  }
                , { label: '车辆代号', name: 'CarID', index: 'CarID', width: 90 }
                , { label: '车牌号', name: 'CarNo', index: 'Car.CarNo', width: 90 }
                , { label: '司机编号', name: 'UserID', index: 'UserID', width: 90 }
                , { label: '司机姓名', name: 'TrueName', index: 'User.TrueName', width: 90 }
                , { label: '出车时间', name: 'AssDate', index: 'AssDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '辅助类型', name: 'AssType', index: 'AssType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AssType'} }
                , { label: '趟次', name: 'AssTimes', index: 'AssTimes', width: 90 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                 , { label: '是否改变车辆状态', name: 'IsChangeCar', index: 'IsChangeCar', hidden: true }
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

}