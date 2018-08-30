function adm2examIndexInit(storeUrl) {
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
            , dialogWidth: 900
            , dialogHeight: 600
		    , initArray: [
                { label: '试验编号', name: 'ID', index: 'ID' }
                 , { label: '原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                 , { label: '原料', name: 'StuffName', index: 'StuffName' }
                , { label: '委托号', name: 'CommissionID', index: 'CommissionID' }
                , { label: '试验时间', name: 'ExamTime', index: 'ExamTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '报告时间', name: 'ReportTime', index: 'ReportTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '代表数量(T)', name: 'DeputyNum', index: 'DeputyNum' }
                , { label: '出厂编号', name: 'OutID', index: 'OutID' }
                , { label: '出厂日期', name: 'OutDate', index: 'OutDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '进厂日期', name: 'InDate', index: 'InDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '采样日期', name: 'SampDate', index: 'SampDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '成型日期', name: 'MoldDate', index: 'MoldDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '水泥品种', name: 'CementBreed', index: 'CementBreed' }
                , { label: '强度等级', name: 'ConStrength', index: 'ConStrength' }
                , { label: '批号', name: 'BatchNo', index: 'BatchNo' }
                , { label: '比表面积', name: 'SurArea', index: 'SurArea' }
                , { label: '方法', name: 'Method', index: 'Method' }
                , { label: '试样重g', name: 'SampWeight', index: 'SampWeight' }
                , { label: '筛余干重g', name: 'SuDryWeight', index: 'SuDryWeight' }
                , { label: '筛余百分比%', name: 'SuPer', index: 'SuPer' }
                , { label: '加水时间', name: 'AddWaTime', index: 'AddWaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '始凝时间', name: 'BeginFreTime', index: 'BeginFreTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '终凝时间', name: 'EndFreTime', index: 'EndFreTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '针距底板2~3mm时间', name: 'BoTime', index: 'BoTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '针沉入净浆1~0.5mm时间', name: 'PinBoTime', index: 'PinBoTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '水中7天基准长度L0', name: 'Wa7L0', index: 'Wa7L0' }
                , { label: '水中7天初始长度L', name: 'Wa7L', index: 'Wa7L' }
                , { label: '水中7天龄期长度L1', name: 'Wa7L1', index: 'Wa7L1' }
                , { label: '水中7天限制膨胀率', name: 'Wa7Lim', index: 'Wa7Lim' }
                , { label: '空中21天基准长度L0', name: 'Air21L0', index: 'Air21L0' }
                , { label: '空中21天初始长度L', name: 'Air21L', index: 'Air21L' }
                , { label: '空中21天龄期长度L1', name: 'Air21L1', index: 'Air21L1' }
                , { label: '空中21天限制膨胀率', name: 'Air21Lim', index: 'Air21Lim' }
                , { label: '水中28天基准长度L0', name: 'Wa28L0', index: 'Wa28L0' }
                , { label: '水中28天初始长度L', name: 'Wa28L', index: 'Wa28L' }
                , { label: '水中28天龄期长度L1', name: 'Wa28L1', index: 'Wa28L1' }
                , { label: '水中28天限制膨胀率', name: 'Wa28Lim', index: 'Wa28Lim' }
                , { label: '其他', name: 'Other', index: 'Other' }
                , { label: '限制膨胀率结论', name: 'LimCon', index: 'LimCon' }
                , { label: '温度(3天)', name: 'Temp3', index: 'Temp3' }
                , { label: '湿度(3天)%', name: 'Hum3', index: 'Hum3' }
                , { label: '试验时间(3天)', name: 'ExamTime3', index: 'ExamTime3', formatter: 'date' }
                , { label: '抗折强度平均值(3天)', name: 'Imz3', index: 'Imz3' }
                , { label: '强度平均值(3天)', name: 'Str3', index: 'Str3' }
                , { label: '抗压强度平均值(3天)', name: 'Imy3', index: 'Imy3' }
                , { label: '温度(7天)', name: 'Temp7', index: 'Temp7' }
                , { label: '湿度(7天)%', name: 'Hum7', index: 'Hum7' }
                , { label: '试验时间(7天)', name: 'ExamTime7', index: 'ExamTime7' }
                , { label: '抗折强度平均值(7天)', name: 'Imz7', index: 'Imz7' }
                , { label: '强度平均值(7天)', name: 'Str7', index: 'Str7' }
                , { label: '抗压强度平均值(7天)', name: 'Imy7', index: 'Imy7' }
                , { label: '温度(28天)', name: 'Temp28', index: 'Temp28' }
                , { label: '湿度(28天)%', name: 'Hum28', index: 'Hum28' }
                , { label: '试验时间(28天)', name: 'ExamTime28', index: 'ExamTime28' }
                , { label: '抗折强度平均值(28天)', name: 'Imz28', index: 'Imz28' }
                , { label: '强度平均值(28天)', name: 'Str28', index: 'Str28' }
                , { label: '抗压强度平均值(28天)', name: 'Imy28', index: 'Imy28' }
                , { label: '温度(快测)', name: 'TempQe', index: 'TempQe' }
                , { label: '湿度(快测)%', name: 'HumQe', index: 'HumQe' }
                , { label: '试验时间(快测)', name: 'ExamTimeQe', index: 'ExamTimeQe' }
                , { label: '抗折强度平均值(快测)', name: 'ImzQe', index: 'ImzQe' }
                , { label: '强度平均值(快测)', name: 'StrQe', index: 'StrQe' }
                , { label: '抗压强度平均值(快测)', name: 'ImyQe', index: 'ImyQe' }
                , { label: '有效时间', name: 'AvaTime', index: 'AvaTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '标准判定', name: 'StandJudge', index: 'StandJudge' }
                , { label: '审批人', name: 'Assessor', index: 'Assessor' }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
                , { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '检测依据', name: 'ExamGist', index: 'ExamGist' }
                , { label: '检测结论', name: 'ExamResult', index: 'ExamResult' }
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