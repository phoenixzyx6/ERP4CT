function admexamIndexInit(storeUrl) {
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
             , dialogWidth: 1024
            , dialogHeight: 500
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '委托号', name: 'CommissionID', index: 'CommissionID', hidden: true }
                 , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                 , { label: '原料', name: 'StuffName', index: 'StuffName', width: 120 }
                , { label: '样品特性', name: 'SampIdentity', index: 'SampIdentity', width: 80 }
                , { label: '试验时间', name: 'ExamTime', index: 'ExamTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge'] }, width: 80 }
                , { label: '报告时间', name: 'ReportTime', index: 'ReportTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge'] }, width: 80 }
                , { label: '代表数量(T)', name: 'DeputyNum', index: 'DeputyNum', width: 80 }
                , { label: '出厂编号', name: 'OutID', index: 'OutID' }
                , { label: '放射性检测编号', name: 'RadioExamID', index: 'RadioExamID' }
                , { label: '报告编号', name: 'ReportID', index: 'ReportID' }
                , { label: '烘干瓶重(g)', name: 'DryBotWeight', index: 'DryBotWeight' }
                , { label: '烘干瓶和样品重(g)', name: 'DryBSWeight', index: 'DryBSWeight' }
                , { label: '烘干瓶和烘干样品重(g)', name: 'DryBDrySWeight', index: 'DryBDrySWeight' }
                , { label: '固含量%', name: 'SContent', index: 'SContent' }
                , { label: '平均值%', name: 'AvaValue', index: 'AvaValue' }
                , { label: '减水率', name: 'SubWaRate', index: 'SubWaRate' }
                , { label: '常压泌水率比', name: 'ApWaRate', index: 'ApWaRate' }
                , { label: '密度', name: 'Density', index: 'Density' }
                , { label: '水泥净浆流动度', name: 'CEFlow', index: 'CEFlow' }
                , { label: 'PH值', name: 'PHValue', index: 'PHValue' }
                , { label: '抗压强度比%-3天', name: 'ImyRate3', index: 'ImyRate3' }
                , { label: '抗压强度比%-7天', name: 'ImyRate7', index: 'ImyRate7' }
                , { label: '抗压强度比%-28天', name: 'ImyRate28', index: 'ImyRate28' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '标准判定', name: 'StandJudge', index: 'StandJudge' }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
                 , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist', hidden: true }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult', hidden: true }
                , { label: '指标1', name: 'zb1', index: 'zb1', hidden: true }
                , { label: '指标2', name: 'zb2', index: 'zb2', hidden: true }
                , { label: '指标3', name: 'zb3', index: 'zb3', hidden: true }
                , { label: '指标4', name: 'zb4', index: 'zb4', hidden: true }
                , { label: '指标5', name: 'zb5', index: 'zb5', hidden: true }
                , { label: '指标6', name: 'zb6', index: 'zb6', hidden: true }
                , { label: '指标7', name: 'zb7', index: 'zb7', hidden: true }
                , { label: '指标8', name: 'zb8', index: 'zb8', hidden: true }
                , { label: '指标9', name: 'zb9', index: 'zb9', hidden: true }
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