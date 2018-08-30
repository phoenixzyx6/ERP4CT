
function tranaccIndexInit(storeUrl) {
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
            , dialogWidth: 800
            , dialogHeight: 300
            //, groupField: 'AccCarID'
            //, groupingView: { groupDataSorted: true }
		    , initArray: [
                  { label: '事故编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '当事人', name: 'AccMan', index: 'AccMan', width: 80 }
                , { label: '事故车辆', name: 'AccCarID', index: 'AccCarID', width: 80 }
                , { label: '事故时间', name: 'AccTime', index: 'AccTime', formatter: 'datetime', width: 130, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '事故地点', name: 'AccAddr', index: 'AccAddr', width: 80 }
                , { label: '事故种类', name: 'AccClass', index: 'AccClass', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AccClass' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('AccClass')} }
                , { label: '保险公司', name: 'InsureCorp', index: 'InsureCorp', width: 80 }
                , { label: '保险单号', name: 'InsureNo', index: 'InsureNo', width: 80 }
                , { label: '损失部分', name: 'LossPart', index: 'LossPart', width: 80 }
                , { label: '损失金额', name: 'LossAmount', index: 'LossAmount', formatter: 'currency', width: 80 }
                , { label: '事故原因', name: 'AccReason', index: 'AccReason' }
                , { label: '事故描述', name: 'AccDes', index: 'AccDes' }

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
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly("ID", false);
                            
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly("ID", true);
                        
                        }
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