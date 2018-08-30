//采购计划
function stockPlanInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '计划单号', name: 'ID', index: 'ID', width: 80 }
                , { label: '计划日期', name: 'PlanDate', index: 'PlanDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: ' 计划员', name: 'PlanMan', index: 'PlanMan', width: 80 }
                , { label: '供应商编号', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: '供应商', name: 'SupplyName', jsonmap:'SupplyName', index: 'SupplyInfo.SupplyName'}
                , { label: '采购合同', name: 'StockPactID', index: 'StockPactID', hidden: true, sortable: false }
                , { label: '采购合同', name: 'PactName', index: 'StockPact.PactName' }
                , { label: '原料', name: 'StuffName', index: 'StuffInfo.StuffName', width: 80  }
                , { label: ' 计划数量(T)', name: 'PlanAmount', index: 'PlanAmount', width: 80, align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: ' 原料编号', name: 'StuffID', index: 'StuffID', hidden: true }
                , { label: '产地', name: 'SourceAddr', index: 'SourceAddr', width: 80 }
                , { label: '客户', name: 'CustName', index: 'CustName' }
                , { label: '运输商', name: 'Conveyancer', index: 'Conveyancer', width: 80 }
                , { label: '运输方式', name: 'TransportMode', index: 'TransportMode', width: 80 }
                , { label: '称重依据', name: 'WeighBy', index: 'WeighBy', width: 80 }
                , { label: ' 计量单位', name: 'GageUnit', index: 'GageUnit' }
                , { label: ' 采购单价', name: 'Price', index: 'Price' }
                , { label: '开始日期', name: 'BeginDate', index: 'BeginDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '结束日期', name: 'EndDate', index: 'EndDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '执行状态', name: 'ExecStatus', index: 'ExecStatus' }
                , { label: '质量标准', name: 'QualityNeed', index: 'QualityNeed' }
                , { label: ' 审核人', name: 'Auditor', index: 'Auditor' }
                , { label: ' 审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '审核意见', name: 'AuditInfo', index: 'AuditInfo' }
                , { label: '结束理由', name: 'CloseReason', index: 'CloseReason' }
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
                            myJqGrid.getFormField("PlanMan").val(opts.currentUser);
                            myJqGrid.setFormFieldReadOnly('PlanMan', true);
                            myJqGrid.getFormField("PlanAmount_T").bind('blur', function () {
                                var PlanAmount_T = myJqGrid.getFormField("PlanAmount_T").val();
                                myJqGrid.getFormField("PlanAmount").val(PlanAmount_T * 1000);
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.getFormField("PlanAmount_T").val(data.PlanAmount / 1000);

                            myJqGrid.getFormField("PlanAmount_T").bind('blur', function () {
                                var PlanAmount_T = myJqGrid.getFormField("PlanAmount_T").val();
                                myJqGrid.getFormField("PlanAmount").val(PlanAmount_T * 1000);
                            });
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleAuditing: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'AuditingFormDiv',
                        btn: btn,
                        width: 480,
                        height: 250
                    });
                }
            }
    });
}