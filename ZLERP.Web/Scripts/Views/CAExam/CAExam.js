function caexamIndexInit(storeUrl) {
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
             , dialogWidth: 980
            , dialogHeight: 500
		   , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID' }
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
                , { label: '吸水率', name: 'DrinkRate', index: 'DrinkRate' }
                , { label: '针片状颗粒含量', name: 'GrainContent', index: 'GrainContent' }
                , { label: '压碎指标值', name: 'CrushVal', index: 'CrushVal' }
                , { label: '级配区域', name: 'CarpGradeReg', index: 'CarpGradeReg' }
                , { label: '干样重G1(g)', name: 'DrySampWeight', index: 'DrySampWeight' }
                , { label: '湿样重G2(g)', name: 'WetSampWeight', index: 'WetSampWeight' }
                , { label: '平均%', name: 'AvaRate', index: 'AvaRate' }
                , { label: 'S100', name: 'S100', index: 'S100',hidden:true }
                , { label: 'S800', name: 'S800', index: 'S800', hidden: true }
                , { label: 'S630', name: 'S630', index: 'S630', hidden: true }
                , { label: 'S500', name: 'S500', index: 'S500', hidden: true }
                , { label: 'S400', name: 'S400', index: 'S400', hidden: true }
                , { label: 'S315', name: 'S315', index: 'S315', hidden: true }
                , { label: 'S250', name: 'S250', index: 'S250', hidden: true }
                , { label: 'S200', name: 'S200', index: 'S200', hidden: true }
                , { label: 'S160', name: 'S160', index: 'S160', hidden: true }
                , { label: 'S10', name: 'S10', index: 'S10', hidden: true }
                , { label: 'S50', name: 'S50', index: 'S50', hidden: true }
                , { label: 'S25', name: 'S25', index: 'S25', hidden: true }
                , { label: 'Sy_1', name: 'Sy_1', index: 'Sy_1', hidden: true }

                , { label: 'Sy100', name: 'Sy100', index: 'Sy100', hidden: true }
                , { label: 'Sy800', name: 'Sy800', index: 'Sy800', hidden: true }
                , { label: 'Sy630', name: 'Sy630', index: 'Sy630', hidden: true }
                , { label: 'Sy500', name: 'Sy500', index: 'Sy500', hidden: true }
                , { label: 'Sy400', name: 'Sy400', index: 'Sy400', hidden: true }
                , { label: 'Sy315', name: 'Sy315', index: 'Sy315', hidden: true }
                , { label: 'Sy250', name: 'Sy250', index: 'Sy250', hidden: true }
                , { label: 'Sy200', name: 'Sy200', index: 'Sy200', hidden: true }
                , { label: 'Sy160', name: 'Sy160', index: 'Sy160', hidden: true }
                , { label: 'Sy10', name: 'Sy10', index: 'Sy10', hidden: true }
                , { label: 'Sy50', name: 'Sy50', index: 'Sy50', hidden: true }
                , { label: 'Sy25', name: 'Sy25', index: 'Sy25', hidden: true }
                , { label: 'Sy_2', name: 'Sy_2', index: 'Sy_2', hidden: true }

                , { label: 'Ss100', name: 'Ss100', index: 'Ss100', hidden: true }
                , { label: 'Ss800', name: 'Ss800', index: 'Ss800', hidden: true }
                , { label: 'Ss630', name: 'Ss630', index: 'Ss630', hidden: true }
                , { label: 'Ss500', name: 'Ss500', index: 'Ss500', hidden: true }
                , { label: 'Ss400', name: 'Ss400', index: 'Ss400', hidden: true }
                , { label: 'Ss315', name: 'Ss315', index: 'Ss315', hidden: true }
                , { label: 'Ss250', name: 'Ss250', index: 'Ss250', hidden: true }
                , { label: 'Ss200', name: 'Ss200', index: 'Ss200', hidden: true }
                , { label: 'Ss160', name: 'Ss160', index: 'Ss160', hidden: true }
                , { label: 'Ss10', name: 'Ss10', index: 'Ss10', hidden: true }
                , { label: 'Ss50', name: 'Ss50', index: 'Ss50', hidden: true }
                , { label: 'Ss25', name: 'Ss25', index: 'Ss25', hidden: true }
                , { label: 'Ss_2', name: 'Ss_2', index: 'Ss_2', hidden: true }

                , { label: '合计', name: 'TotalNum', index: 'TotalNum' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '级配判定', name: 'CarpJudge', index: 'CarpJudge' }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }

    , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist',width:200 }
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