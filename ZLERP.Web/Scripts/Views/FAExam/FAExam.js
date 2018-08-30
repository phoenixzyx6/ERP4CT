function faexamIndexInit(storeUrl) {
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
             , dialogWidth: 880
            , dialogHeight: 630
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID', width: 80 }
                
                 , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                 , { label: '原料', name: 'StuffName', index: 'StuffName' }
                , { label: '试验时间', name: 'ExamTime', index: 'ExamTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '报告时间', name: 'ReportTime', index: 'ReportTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '代表数量(T)', name: 'DeputyNum', index: 'DeputyNum' }
                , { label: '含泥量', name: 'ClayRate', index: 'ClayRate' }
                , { label: '泥块含量', name: 'ClayMete', index: 'ClayMete' }
                , { label: '表观密度', name: 'SurDensity', index: 'SurDensity' }
                , { label: '松散堆积密度', name: 'StackDensity', index: 'StackDensity' }
                , { label: '空隙率(紧密密度)', name: 'TightDensity', index: 'TightDensity' }
                , { label: '放射性检测编号', name: 'RadioExamID', index: 'RadioExamID' }
                , { label: '报告编号', name: 'ReportID', index: 'ReportID' }
                , { label: '细度模数', name: 'Mesh', index: 'Mesh' }
                , { label: '吸水率', name: 'DrinkRate', index: 'DrinkRate' }
                , { label: '级配', name: 'CarpGrade', index: 'CarpGrade' }
                , { label: '10.0mm以上颗粒含量%', name: 'GrainNum100', index: 'GrainNum100', hidden: true }
                , { label: '5.0mm', name: 'GrainNum50', index: 'GrainNum50', hidden: true }
                , { label: '2.5mm', name: 'GrainNum25', index: 'GrainNum25', hidden: true }
                , { label: '1.25mm', name: 'GrainNum125', index: 'GrainNum125', hidden: true }
                , { label: '0.63mm', name: 'GrainNum063', index: 'GrainNum063', hidden: true }
                , { label: '0.315mm', name: 'GrainNum315', index: 'GrainNum315', hidden: true }
                , { label: '0.160mm', name: 'GrainNum160', index: 'GrainNum160', hidden: true }
                , { label: '0.080mm', name: 'GrainNum080', index: 'GrainNum080', hidden: true }
                , { label: '筛底', name: 'SiftBelow', index: 'SiftBelow', hidden: true }

                , { label: '10.0mm以上颗粒含量%', name: 'fjsy100_1', index: 'fjsy100_1', hidden: true }
                , { label: '5.0mm', name: 'fjsy50_1', index: 'fjsy50_1', hidden: true }
                , { label: '2.5mm', name: 'fjsy25_1', index: 'fjsy25_1', hidden: true }
                , { label: '1.25mm', name: 'fjsy125_1', index: 'fjsy125_1', hidden: true }
                , { label: '0.63mm', name: 'fjsy063_1', index: 'fjsy063_1', hidden: true }
                , { label: '0.315mm', name: 'fjsy315_1', index: 'fjsy315_1', hidden: true }
                , { label: '0.160mm', name: 'fjsy160_1', index: 'fjsy160_1', hidden: true }
                , { label: '0.080mm', name: 'fjsy080_1', index: 'fjsy080_1', hidden: true }
                , { label: '筛底', name: 'fjsy_1', index: 'fjsy_1', hidden: true }

                , { label: '10.0mm以上颗粒含量%', name: 'ljsy100_1', index: 'ljsy100_1', hidden: true }
                , { label: '5.0mm', name: 'ljsy50_1', index: 'ljsy50_1', hidden: true }
                , { label: '2.5mm', name: 'ljsy25_1', index: 'ljsy25_1', hidden: true }
                , { label: '1.25mm', name: 'ljsy125_1', index: 'ljsy125_1', hidden: true }
                , { label: '0.63mm', name: 'ljsy063_1', index: 'ljsy063_1', hidden: true }
                , { label: '0.315mm', name: 'ljsy315_1', index: 'ljsy315_1', hidden: true }
                , { label: '0.160mm', name: 'ljsy160_1', index: 'ljsy160_1', hidden: true }
                , { label: '0.080mm', name: 'ljsy080_1', index: 'ljsy080_1', hidden: true }
                , { label: '筛底', name: 'ljsy_1', index: 'ljsy_1', hidden: true }

                , { label: '10.0mm以上颗粒含量%', name: 'syl100_2', index: 'syl100_2', hidden: true }
                , { label: '5.0mm', name: 'syl50_2', index: 'syl50_2', hidden: true }
                , { label: '2.5mm', name: 'syl25_2', index: 'syl25_2', hidden: true }
                , { label: '1.25mm', name: 'syl125_2', index: 'syl125_2', hidden: true }
                , { label: '0.63mm', name: 'syl063_2', index: 'syl063_2', hidden: true }
                , { label: '0.315mm', name: 'syl315_2', index: 'syl315_2', hidden: true }
                , { label: '0.160mm', name: 'syl160_2', index: 'syl160_2', hidden: true }
                , { label: '0.080mm', name: 'syl080_2', index: 'syl080_2', hidden: true }
                , { label: '筛底', name: 'syl_2', index: 'syl_2', hidden: true }

                , { label: '10.0mm以上颗粒含量%', name: 'fjsy100_2', index: 'fjsy100_2', hidden: true }
                , { label: '5.0mm', name: 'fjsy50_2', index: 'fjsy50_2', hidden: true }
                , { label: '2.5mm', name: 'fjsy25_2', index: 'fjsy25_2', hidden: true }
                , { label: '1.25mm', name: 'fjsy125_2', index: 'fjsy125_2', hidden: true }
                , { label: '0.63mm', name: 'fjsy063_2', index: 'fjsy063_2', hidden: true }
                , { label: '0.315mm', name: 'fjsy315_2', index: 'fjsy315_2', hidden: true }
                , { label: '0.160mm', name: 'fjsy160_2', index: 'fjsy160_2', hidden: true }
                , { label: '0.080mm', name: 'fjsy080_2', index: 'fjsy080_2', hidden: true }
                , { label: '筛底', name: 'fjsy_2', index: 'fjsy_2', hidden: true }

                , { label: '10.0mm以上颗粒含量%', name: 'ljsy100_2', index: 'ljsy100_2', hidden: true }
                , { label: '5.0mm', name: 'ljsy50_2', index: 'ljsy50_2', hidden: true }
                , { label: '2.5mm', name: 'ljsy25_2', index: 'ljsy25_2', hidden: true }
                , { label: '1.25mm', name: 'ljsy125_2', index: 'ljsy125_2', hidden: true }
                , { label: '0.63mm', name: 'ljsy063_2', index: 'ljsy063_2', hidden: true }
                , { label: '0.315mm', name: 'ljsy315_2', index: 'ljsy315_2', hidden: true }
                , { label: '0.160mm', name: 'ljsy160_2', index: 'ljsy160_2', hidden: true }
                , { label: '0.080mm', name: 'ljsy080_2', index: 'ljsy080_2', hidden: true }
                , { label: '筛底', name: 'ljsy_2', index: 'ljsy_2', hidden: true }

                , { label: '湿样重(kg)', name: 'WetSampWeight', index: 'WetSampWeight' }
                , { label: '干样重(kg)', name: 'DrySampWeight', index: 'DrySampWeight' }
                , { label: '含水率%', name: 'WaRate', index: 'WaRate' }
                , { label: '平均%', name: 'AvaRate', index: 'AvaRate' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '标准判定', name: 'StandJudge', index: 'StandJudge', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Judge'} }
                , { label: '让步接收判定', name: 'ConcessionJudge', index: 'ConcessionJudge', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Judge'} }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }

    , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist',width:150 }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult', width: 200 }
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