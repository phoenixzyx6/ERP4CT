function qualityexamIndexInit(storeUrl, itemstoreUrl, itemUpdateUrl, itemAddUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight / 2
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupField: 'QualityType'
		    , initArray: [
                { label: '品质编号', name: 'ID', index: 'ID' }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', hidden: true }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID',hidden:true }
                , { label: '委托号', name: 'CommissionID', index: 'CommissionID', hidden: true }
                , { label: '填表时间', name: 'ExamDate', index: 'ExamDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength' }
                , { label: '养护条件', name: 'ConserveCondition', index: 'ConserveCondition' }
                , { label: '试块规格', name: 'ExamNum', index: 'ExamNum' }
                , { label: '制作人员', name: 'DoMan', index: 'DoMan' }
                , { label: '试验人员', name: 'ExamMan', index: 'ExamMan' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '品质类型', name: 'QualityType', index: 'QualityType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ExamType'} }
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
                        btn: btn,
                        prefix: 'QualityExam'
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        mySubGrid.refreshGrid("QualityExamID='" + id + "'");
    });

    var mySubGrid = new MyGrid({
        renderTo: 'MySubGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight / 2
		    , storeURL: itemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , editSaveUrl: itemUpdateUrl
            , autoLoad: false
		    , initArray: [
                { label: '试块编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '品质编号', name: 'QualityExamID', index: 'QualityExamID', hidden: true }                
                , { label: '试块编号', name: 'ExamBodyID', index: 'ExamBodyID', editable: true, width: 70 }
                , { label: '制作时间', name: 'DoTime', index: 'DoTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge'] }, width: 100 }
                , { label: '测试时间', name: 'TestTime', index: 'TestTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge'] }, width: 100 }
                , { label: '龄期', name: 'AgeTime', index: 'AgeTime', editable: true, width: 70 }
                , { label: '尺寸', name: 'Sizex', index: 'Sizex', editable: true, width: 70 }
                , { label: '荷载1/渗水', name: 'Load1', index: 'Load1', editable: true, width: 70 }
                , { label: '强度1/渗水', name: 'Strength1', index: 'Strength1', editable: true, width: 70 }
                , { label: '荷载2/渗水', name: 'Load2', index: 'Load2', editable: true, width: 70 }
                , { label: '强度2/渗水', name: 'Strength2', index: 'Strength2', editable: true, width: 70 }
                , { label: '荷载3/渗水', name: 'Load3', index: 'Load3', editable: true, width: 70 }
                , { label: '强度3/渗水', name: 'Strength3', index: 'Strength3', editable: true, width: 70 }
                , { label: '换算系数', name: 'Modulus', index: 'Modulus', editable: true, width: 70 }
                , { label: '强度代表值', name: 'StrengthValue', index: 'StrengthValue', editable: true, width: 70 }
                , { label: '达到设计强度%', name: 'ExamBlockGroupID', index: 'ExamBlockGroupID', editable: true, width: 120 }

		    ]
            , functions: {
                handleReload: function (btn) {
                    mySubGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    mySubGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var id = myJqGrid.getSelectedKey();
                    if (isEmpty(id)) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return false;
                    }
                    else {
                        var requestURL = itemAddUrl;
                        var postParams = { QEId: id };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            mySubGrid.refreshGrid("QualityExamID='" + id + "'");
                        });
                    }
                },
                handleEdit: function (btn) {
                    mySubGrid.handleEdit({
                        loadFrom: 'MySubFormDiv',
                        btn: btn,
                        prefix: 'QualityExamItem'
                    });
                }
                , handleDelete: function (btn) {
                    mySubGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}