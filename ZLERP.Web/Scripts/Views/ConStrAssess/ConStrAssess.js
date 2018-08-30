function constrassessIndexInit(storeUrl1, storeUrl2, storeUrl3, AdditemUrl, UpdateitemUrl1, UpdateitemUrl2, StatUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'GridDiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight / 2
		    , storeURL: storeUrl1
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 600
            , dialogHeight: 300
		    , initArray: [
                { label: '评定编号', name: 'ID', index: 'ID', width: 70 }
                , { label: '统计方法', name: 'StatMethod', index: 'StatMethod', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'StatMethod' }, width: 70 }
                , { label: '结论', name: 'StatResult', index: 'StatResult', width: 70 }
                , { label: '强度等级', name: 'ConStrength', index: 'ConStrength', width: 70 }
                , { label: '强度值来源', name: 'StrSource', index: 'StrSource', width: 70 }
                , { label: '强度组数', name: 'StrCount', index: 'StrCount', width: 70 }
                , { label: '标准差', name: 'StanDiff', index: 'StanDiff', width: 70 }
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID' }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID' }
                , { label: '检测规程', name: 'AssessCriterion', index: 'AssessCriterion' }
                , { label: '成型起始日期', name: 'MoldDateFrom', index: 'MoldDateFrom', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '成型截止日期', name: 'MoldDateTo', index: 'MoldDateTo', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '统计日期', name: 'StatDate', index: 'StatDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }



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
                        prefix: 'csa'
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , postCallBack: function (response) {
                            myitemJqGrid.reloadGrid();
                            m1Grid.reloadGrid();
                        }
                    });
                }
                , handleStat: function (btn) {
                    var pid = myJqGrid.getSelectedKey();
                    if (pid.length > 0) {
                        var requestURL = StatUrl;
                        var postParams = { id: pid };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            m1Grid.refreshGrid("ConStrAssessID='" + pid + "'");
                            myJqGrid.reloadGrid();
                        }, null, btn);
                    }
                    else {
                        showMessage('提示', "请选择一条主表信息");
                    }
                }

            }
    });

    var myitemJqGrid = new MyGrid({
        renderTo: 'ItemsGridDiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: storeUrl2
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 500
            , dialogHeight: 200
            , editSaveUrl: UpdateitemUrl1
		    , initArray: [
                { label: '流水号', name: 'ID', index: 'ID', hidden: true }
                , { label: '试块编号', name: 'ExamID', index: 'ExamID', width: 70, editable: true }
                , { label: '1#强度', name: 'Exam1Str', index: 'Exam1Str', width: 70, editable: true }
                , { label: '2#强度', name: 'Exam2Str', index: 'Exam2Str', width: 70, editable: true }
                , { label: '3#强度', name: 'Exam3Str', index: 'Exam3Str', width: 70, editable: true }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myitemJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myitemJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    var pid = myJqGrid.getSelectedKey();
                    if (pid.length > 0) {
                        myitemJqGrid.handleAdd({
                            loadFrom: 'ItemsFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                myitemJqGrid.setFormFieldValue('csaitem.ConStrAssessID', pid);
                                myitemJqGrid.setFormFieldReadOnly('csaitem.ConStrAssessID', true);
                            }
                        });
                    }
                    else {
                        showMessage('提示', "请在左侧选择一条主表信息");
                    }
                },
                handleAutoAdd: function (btn) {
                    var pid = myJqGrid.getSelectedKey();
                    if (pid.length > 0) {
                        var requestURL = AdditemUrl;
                        var postParams = { pid: pid };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            myitemJqGrid.reloadGrid();
                        }, null, btn);
                    }
                    else {
                        showMessage('提示', "请在左侧选择一条主表信息");
                    }
                }
                , handleDelete: function (btn) {
                    myitemJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        myitemJqGrid.refreshGrid("ConStrAssessID='" + id + "'");
        m1Grid.refreshGrid("ConStrAssessID='" + id + "'");
    });

    var m1Grid = new MyGrid({
        renderTo: 'm1GridDiv'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight / 2
		    , storeURL: storeUrl3
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '子表编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '砼强度评定编号', name: 'ConStrAssessID', index: 'ConStrAssessID', hidden: true }
                , { label: '1#强度', name: 'Exam1Str', index: 'Exam1Str', width: 70 }
                , { label: '2#强度', name: 'Exam2Str', index: 'Exam2Str', width: 70 }
                , { label: '3#强度', name: 'Exam3Str', index: 'Exam3Str', width: 70 }
                , { label: 'Fcu,min', name: 'Fcumin', index: 'Fcumin', width: 70 }
                , { label: 'Fcu,max', name: 'Fcumax', index: 'Fcumax', width: 70 }
                , { label: 'mFcu', name: 'mFcu', index: 'mFcu', width: 70 }
                , { label: 'Fcuk', name: 'Fcuk', index: 'Fcuk', width: 70 }
                , { label: 'Fcuk+0.7a', name: 'FcukAddPar', index: 'FcukAddPar', width: 70 }
                , { label: 'Fcuk-0.7a', name: 'FcukSubPar', index: 'FcukSubPar', width: 70 }
                , { label: 'AFcuk', name: 'AFcuk', index: 'AFcuk', width: 70 }
                , { label: '结论', name: 'Result', index: 'Result' }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    m1Grid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    m1Grid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    m1Grid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    m1Grid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    m1Grid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}