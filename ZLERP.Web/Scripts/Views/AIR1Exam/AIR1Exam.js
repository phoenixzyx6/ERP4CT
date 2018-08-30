function air1examIndexInit(storeUrl) {
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
            , dialogWidth: 750
            , dialogHeight: 400
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID' }
                , { label: '试验时间', name: 'ExamTime', index: 'ExamTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '报告时间', name: 'ReportTime', index: 'ReportTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                 , { label: '原料', name: 'StuffName', index: 'StuffName' }
                , { label: '委托号', name: 'CommissionID', index: 'CommissionID' }
                , { label: '代表数量(T)', name: 'DeputyNum', index: 'DeputyNum' }
                , { label: '批号', name: 'BatchID', index: 'BatchID' }
                , { label: '放射性检测编号', name: 'RadioExamID', index: 'RadioExamID' }
                , { label: '细度%', name: 'FineDegree', index: 'FineDegree' }
                , { label: '需水量比', name: 'NeedWaRate', index: 'NeedWaRate' }
                , { label: '烧失量', name: 'BurnLossNum', index: 'BurnLossNum' }
                , { label: '含水率', name: 'WaRate', index: 'WaRate' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '标准判定', name: 'StandJudge', index: 'StandJudge' }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
                , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist' }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult',width:500 }
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