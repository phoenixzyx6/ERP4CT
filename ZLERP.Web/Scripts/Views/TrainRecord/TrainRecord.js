function trainrecordIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'TrainRecordGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 480
            , dialogHeight: 255
		    , storeURL: options.storeUrl
		    , sortByField: 'BeginDate'
		    , primaryKey: 'ID'
            , groupField: 'TrueName'
            , groupingView: { groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [true] }
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 60 }
                , { label: '受训对象ID', name: 'UserID', index: 'UserID', hidden: true }
                , { label: '受训对象', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: '所属部门', name: 'DepartmentName', index: 'DepartmentName', width: 100, sortable: false}
                , { label: '职务', name: 'UserType', index: 'UserType', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'UserType'} }
                , { label: '培训项目编号', name: 'TrainingID', index: 'TrainingID', hidden: true }
                , { label: '培训项目名称', name: 'TrainName', index: 'TrainName', sortable: false, width: 200 }
                , { label: '开始日期', name: 'BeginDate', index: 'BeginDate', formatter: 'date', width: 80, align: 'center' }
                , { label: '结束日期', name: 'EndDate', index: 'EndDate', formatter: 'date', width: 80, align: 'center' }
                , { label: '培训费用(个人)', name: 'TrainCost', index: 'TrainCost', formatter: 'currency', width: 90, summaryType: 'sum', summaryTpl: '合计: <font color=red>{0}</font>' }
                , { label: '培训心得', name: 'TrainResult', index: 'TrainResult', width: 250 }
                , { label: '培训成绩', name: 'TrainCredit', index: 'TrainCredit', width: 60 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
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
                        loadFrom: 'TrainRecordForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'TrainRecordForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleAddResult: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'TrainResultForm',
                        btn: btn,
                        afterFormLoaded: function () {
                        }
                    });
                }
            }
    });

}