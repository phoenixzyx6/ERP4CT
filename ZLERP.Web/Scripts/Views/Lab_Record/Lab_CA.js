function lab_recordIndexInit(storeUrl) {
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
            , storeCondition: "FinalStuffTypeID='CA'"
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
                , { label: '日期', name: 'Date', index: 'Date', formatter: 'datetime', width: 120 }
                , { label: '运输车号', name: 'Carno', index: 'Carno', width: 80 }
                , { label: '材料大类id', name: 'FinalStuffTypeID', index: 'FinalStuffTypeID', hidden: true }
                , { label: '材料入库id', name: 'stuffinid', index: 'stuffinid', hidden: true }
                , { label: '材料id', name: 'Stuffid', index: 'Stuffid', hidden: true }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName' }
                , { label: '生产厂家id', name: 'Supplyid', index: 'Supplyid', hidden: true }
                , { label: '生产厂家', name: 'SupplyName', index: 'SupplyName' }
                , { label: '发车时间', name: 'StartDate', index: 'StartDate', formatter: 'date', width: 120 }
                , { label: '到站时间', name: 'EndDate', index: 'EndDate', formatter: 'date', width: 120 }
                , { label: '铅封是否完好', name: 'IsWhole', index: 'IsUsed', align: 'center', width: 100, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '样品编号', name: 'ShowpeieNo', index: 'ShowpeieNo' }
                , { label: '单车车重(T)', name: 'Weight', index: 'Weight', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
                , { label: '累计重量(T)', name: 'SumWeight', index: 'SumWeight', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 80 }
                , { label: '罐编号', name: 'Siloid', index: 'Siloid', hidden: true }
                , { label: '筒仓', name: 'SiloName', index: 'SiloName', width: 80 }
                , { label: '收料人员', name: 'InMan', index: 'InMan', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid("FinalStuffTypeID = 'CA'");
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid("FinalStuffTypeID = 'CA'");
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
                }, 
                handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleReport1: function (btn) {
                    if (!CAJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = CAJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720201.aspx?uid=" + selectedRecord.ID);
                },
                handleReport2: function (btn) {
                    if (!CAJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = CAJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720202.aspx?uid=" + selectedRecord.ID);
                }
            }
    });  
    var CAJqGrid = new MyGrid({
        renderTo: 'CAJqGrid'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight / 2 - 20
            , dialogWidth: 700
            , dialogHeight: 550
		    , storeURL: "/Lab_CA.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
               , { label: '父id', name: 'Lab_RecordID', index: 'Lab_RecordID', hidden: true }
               , { label: '规格', name: 'GUIGE', index: 'GUIGE' }
               , { label: '规格', name: 'StuffName', index: 'StuffName', hidden: true }
               , { label: '规格', name: 'SupplyName', index: 'SupplyName', hidden: true }
               , { label: '样品说明', name: 'Description', index: 'Description' }
               , { label: '检测依据', name: 'Depend', index: 'Depend', hidden: true }
               , { label: '日期', name: 'Date', index: 'Date', formatter: 'datetime', hidden: true }
               , { label: '试验室温度', name: 'Temperature', index: 'Temperature', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    CAJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    CAJqGrid.refreshGrid();
                }
                ,
                handleCA: function (btn) {
                    CAJqGrid.handleEdit({
                        loadFrom: 'CAFormDiv',
                        prefix: 'Lab_CA',
                        btn: btn
                    });
                },
                handleAddCAItems: function (btn) {
                    if (!CAJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = CAJqGrid.getSelectedRecord();
                    CAItemsJqGrid.handleAdd({
                        loadFrom: 'CAItemsFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            CAItemsJqGrid.setFormFieldValue("Lab_CA_Items.Lab_CAID", selectrecord.ID);
                        }
                        , postCallBack: function (response) {
                            CAItemsJqGrid.refreshGrid("Lab_CAID='" + selectrecord.ID + "'");
                        }
                    });
                }
            }
    });

    var CAItemsJqGrid = new MyGrid({
        renderTo: 'CAItemsJqGrid'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight / 2
            , dialogWidth: 700
            , dialogHeight: 550
		    , storeURL: "/Lab_CA_Items.mvc/Find"
		    , sortByField: 'NO'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
               , { label: '父id', name: 'Lab_CAID', index: 'Lab_CAID', hidden: true }
               , { label: '样品编号', name: 'NO', index: 'NO' }
               , { label: 'PH', name: 'R', index: 'PH', hidden: true }
               , { label: 'PH', name: 'YRQ', index: 'SL', hidden: true }
               , { label: 'PH', name: 'YRH', index: 'WSL', hidden: true }
               , { label: 'PH', name: 'S1', index: 'PW', hidden: true }
               , { label: 'PH', name: 'S2', index: 'WPW', hidden: true }
               , { label: 'PH', name: 'S3', index: 'GWPW', hidden: true }
               , { label: 'PH', name: 'S4', index: 'LD', hidden: true }
               , { label: 'PH', name: 'S5', index: 'JS', hidden: true }
               , { label: 'PH', name: 'S6', index: 'WS', hidden: true }
               , { label: 'PH', name: 'SD', index: 'T', hidden: true }
               , { label: 'PH', name: 'LQ', index: 'TH', hidden: true }
               , { label: 'PH', name: 'LH', index: 'YQ', hidden: true }
               , { label: 'PH', name: 'LKQ', index: 'Q', hidden: true }
               , { label: 'PH', name: 'LKH', index: 'YG', hidden: true }
               , { label: 'PH', name: 'YJL', index: 'G', hidden: true }
               , { label: 'PH', name: 'SHA', index: 'JC', hidden: true }
               , { label: 'PH', name: 'YSP', index: 'JZ', hidden: true }
               , { label: 'PH', name: 'SP', index: 'WC', hidden: true }
               , { label: 'PH', name: 'DG', index: 'WZ', hidden: true }
               , { label: 'PH', name: 'DV', index: 'J3dH1', hidden: true }
                , { label: 'PH', name: 'DTS', index: 'J3dH2', hidden: true }
                , { label: 'PH', name: 'JG', index: 'J3dH3', hidden: true }
                , { label: 'PH', name: 'JV', index: 'J3dD1', hidden: true }
                , { label: 'PH', name: 'JTS', index: 'J3dD2', hidden: true }
                , { label: 'PH', name: 'Y1', index: 'J3dD3', hidden: true }
                , { label: 'PH', name: 'Y2', index: 'J7dH1', hidden: true }
                , { label: 'PH', name: 'Y3', index: 'J7dH2', hidden: true }
                , { label: 'PH', name: 'Y4', index: 'J7dH3', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    CAItemsJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    CAItemsJqGrid.refreshGrid();
                },
                handleEdit: function (btn) {
                    CAItemsJqGrid.handleEdit({
                        loadFrom: 'CAItemsFormDiv',
                        prefix: 'Lab_CA_Items',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    CAItemsJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    myJqGrid.addListeners("rowclick", function (id, record) {
        CAJqGrid.refreshGrid("Lab_RecordID='" + id + "'");
    });
    CAJqGrid.addListeners("rowclick", function (id, record) {
        CAItemsJqGrid.refreshGrid("Lab_CAID='" + id + "'");
    });
}