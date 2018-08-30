function ceexamIndexInit(storeUrl) {
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
            , dialogHeight: 600
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID',width:80 }
                
                 , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                 , { label: '原料', name: 'StuffName', index: 'StuffName' }
                , { label: '试验时间', name: 'ExamTime', index: 'ExamTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '报告时间', name: 'ReportTime', index: 'ReportTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '代表数量(T)', name: 'DeputyNum', index: 'DeputyNum' }
                , { label: '放射性检测编号', name: 'RadioExamID', index: 'RadioExamID' }
                , { label: '安定性', name: 'Invariability', index: 'Invariability' }
                , { label: '细度%', name: 'FineDegree', index: 'FineDegree' }
                , { label: '标准稠度用水量', name: 'StanWaRate', index: 'StanWaRate' }
                , { label: '烧失量', name: 'BurnLossNum', index: 'BurnLossNum' }
                , { label: '始凝时间', name: 'BeginFreTime', index: 'BeginFreTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '终凝时间', name: 'EndFreTime', index: 'EndFreTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['bd', 'ed', 'eq', 'gt', 'lt']} }
                , { label: '抗折d3-1', name: 'KZd3_1', index: 'KZd3_1' }
                , { label: '抗折d3-2', name: 'KZd3_2', index: 'KZd3_2' }
                , { label: '抗折d3-3', name: 'KZd3_3', index: 'KZd3_3' }
                , { label: '抗折d28-1', name: 'KZd28_1', index: 'KZd28_1' }
                , { label: '抗折d28-2', name: 'KZd28_2', index: 'KZd28_2' }
                , { label: '抗折d28-3', name: 'KZd28_3', index: 'KZd28_3' }
                , { label: '抗压d3-1', name: 'KYd3_1', index: 'KYd3_1' }
                , { label: '抗压d3-2', name: 'KYd3_2', index: 'KYd3_2' }
                , { label: '抗压d3-3', name: 'KYd3_3', index: 'KYd3_3' }
                , { label: '抗压d3-4', name: 'KYd3_4', index: 'KYd3_4' }
                , { label: '抗压d3-5', name: 'KYd3_5', index: 'KYd3_5' }
                , { label: '抗压d3-6', name: 'KYd3_6', index: 'KYd3_6' }
                , { label: '抗压d28-1', name: 'KYd28_1', index: 'KYd28_1' }
                , { label: '抗压d28-2', name: 'KYd28_2', index: 'KYd28_2' }
                , { label: '抗压d28-3', name: 'KYd28_3', index: 'KYd28_3' }
                , { label: '抗压d28-4', name: 'KYd28_4', index: 'KYd28_4' }
                , { label: '抗压d28-5', name: 'KYd28_5', index: 'KYd28_5' }
                , { label: '抗压d28-6', name: 'KYd28_6', index: 'KYd28_6' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '标准判定', name: 'StandJudge', index: 'StandJudge', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Judge'} }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
              , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist',width:200 }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult',width:600 }
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
                }, handle3d: function (btn) {
                    var uom = GetDateBySpan(2);
                    condition = "ExamTime between '" + uom + " 00:00:00' and '" + uom + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }, handle28d: function (btn) {
                    var uom = GetDateBySpan(27);
                    condition = "ExamTime between '" + uom + " 00:00:00' and '" + uom + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }
            }
    });

}