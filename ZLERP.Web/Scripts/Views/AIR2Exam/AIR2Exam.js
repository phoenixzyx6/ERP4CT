function air2examIndexInit(storeUrl) {
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
            , dialogHeight: 450
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID' }
                , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                 , { label: '原料', name: 'StuffName', index: 'StuffName' }
                , { label: '试验时间', name: 'ExamTime', index: 'ExamTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '报告时间', name: 'ReportTime', index: 'ReportTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '代表数量(T)', name: 'DeputyNum', index: 'DeputyNum' }
                , { label: '出厂编号', name: 'OutID', index: 'OutID' }
                , { label: '放射性检测编号', name: 'RadioExamID', index: 'RadioExamID' }
                , { label: '等级', name: 'Grade', index: 'Grade' }
                , { label: '活性指数7天', name: 'ActIndex7', index: 'ActIndex7' }
                , { label: '活性指数28天', name: 'ActIndex28', index: 'ActIndex28' }
                , { label: '含水量', name: 'WaContent', index: 'WaContent' }
                , { label: '烧失量', name: 'BurnLossNum', index: 'BurnLossNum' }
                , { label: '密度', name: 'Density', index: 'Density' }
                , { label: '玻璃体含量', name: 'GlaContent', index: 'GlaContent' }
                , { label: '比表面积', name: 'Area', index: 'Area' }
                , { label: '三氧化硫', name: 'SO3', index: 'SO3' }
                , { label: '氯离子', name: 'Clion', index: 'Clion' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '标准判定', name: 'StandJudge', index: 'StandJudge' }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
              , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist' }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult',width:500 }

                , { label: '委托号', name: 'CommissionID', index: 'CommissionID' }
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