function priceConfirmInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: 'ISNULL(TotalPrice,0)=0 AND ISNULL(TotalTransPrice,0)=0 AND Lifecycle<>-1'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 800
            , dialogHeight: 400
		    , showPageBar: true
            , groupField: 'SupplyName'
            , groupingView: { groupText: ['{0}(<font color=red>{1}</font>)'],minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: '流水号', name: 'ID', index: 'ID', width: 80 }
                , { label: ' 结算状态', name: 'FootStatus', index: 'FootStatus', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: '进厂时间', name: 'InDate', index: 'InDate', formatter: 'datetime', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 原料', name: 'StuffName', index: 'StuffInfo.StuffName', width: 80 }
                , { label: ' 供货厂商', name: 'SupplyName', index: 'SupplyInfo.SupplyName' }
                , { label: ' 运输公司', name: 'TransportName', index: 'TransportInfo.SupplyName' }
                , { label: ' 运输车号', name: 'CarNo', index: 'CarNo', width: 80 }
                , { label: '司机', name: 'Driver', index: 'Driver', width: 80 }
        //, { label: '运送数量(吨)', name: 'TransportNum', align: 'right', index: 'TransportNum', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: '入库数量(吨)', name: 'InNum', index: 'InNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
                , { label: '结算数量(吨)', name: 'FootNum', index: 'FootNum', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
                , { label: '原料单价', name: 'UnitPrice', index: 'UnitPrice', align: 'right', width: 60, formatter: 'currency' }
                , { label: '运输单价', name: 'TransUnitPrice', index: 'TransUnitPrice', align: 'right', width: 60, formatter: 'currency' }
                , { label: '原料总价', name: 'TotalPrice', index: 'TotalPrice', align: 'right', width: 60, formatter: 'currency' }
                , { label: '运输总价', name: 'TotalTransPrice', index: 'TotalTransPrice', align: 'right', width: 60, formatter: 'currency' }
                , { label: 'A/H', name: 'AH', index: 'AH', width: 50, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AH'} }

		    ]
		    , autoLoad: true
            , functions: {
                handleRefresh: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleUnExecuted: function (btn) {
                    //未结算
                    myJqGrid.refreshGrid('ISNULL(TotalPrice,0)=0 AND ISNULL(TotalTransPrice,0)=0  AND Lifecycle<>-1');
                },
                handleExecuted: function (btn) {
                    //已结算
                    myJqGrid.refreshGrid('ISNULL(TotalPrice,0)>0 OR ISNULL(TotalTransPrice,0)>0');
                },
                handleExecute: function (btn) {
                    var ids = myJqGrid.getSelectedKeys();
                    if (ids.length == 0) {
                        showMessage("提示", "请至少选择一条记录进行结算");
                        return false;
                    }
                    
                    ajaxRequest(opts.executeUrl, { ids: ids }, true, function (response) {
                        if (response && response.Result) {
                            myJqGrid.reloadGrid();
                        }
                    }, null, btn);
                }
            }
    });

}
 