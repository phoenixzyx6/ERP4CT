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
            , storeCondition: "FinalStuffTypeID='ADM'"
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
                    myJqGrid.reloadGrid("FinalStuffTypeID = 'ADM'");
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid("FinalStuffTypeID = 'ADM'");
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
                },
                handleReport1: function (btn) {
                    if (!ADMJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = ADMJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720601.aspx?uid=" + selectedRecord.ID);
                },
                handleReport2: function (btn) {
                    if (!ADMJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = ADMJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720602.aspx?uid=" + selectedRecord.ID);
                },
                handleReport3: function (btn) {
                    if (!ADMJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = ADMJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720603.aspx?uid=" + selectedRecord.ID);
                },
                handleReport4: function (btn) {
                    if (!ADMJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = ADMJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720604.aspx?uid=" + selectedRecord.ID);
                },
                handleReport5: function (btn) {
                    if (!ADMJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条实验记录进行操作！');
                        return;
                    }
                    var selectedRecord = ADMJqGrid.getSelectedRecord();
                    window.open("/Reports/Lab/R720605.aspx?uid=" + selectedRecord.ID);
                }
            }
    });  
    var ADMJqGrid = new MyGrid({
        renderTo: 'ADMJqGrid'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight / 2 - 20
            , dialogWidth: 700
            , dialogHeight: 550
		    , storeURL: "/Lab_ADM.mvc/Find"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
               , { label: '父id', name: 'Lab_RecordID', index: 'Lab_RecordID', hidden: true }
               , { label: '品种', name: 'Type', index: 'Type' }
               , { label: '样品说明', name: 'Description', index: 'Description' }
               , { label: '检测依据', name: 'One_Depend', index: 'One_Depend', hidden: true }
               , { label: '日期', name: 'One_Date', index: 'One_Date', formatter: 'datetime', hidden: true }
               , { label: '试验室温度', name: 'One_Temperature', index: 'One_Temperature', hidden: true }
               , { label: '试验室湿度', name: 'One_Wet', index: 'One_Wet', hidden: true }
               , { label: 'PH', name: 'Two_Depend', index: 'Two_Depend', hidden: true }
               , { label: 'PH', name: 'Two_Date', index: 'Two_Date', formatter: 'datetime', hidden: true }
               , { label: 'PH', name: 'Two_Temperature', index: 'Two_Temperature', hidden: true }
               , { label: 'PH', name: 'Two_Wet', index: 'Two_Wet', hidden: true }
               , { label: 'PH', name: 'SHA', index: 'SHA', hidden: true }
               , { label: 'PH', name: 'SHI', index: 'SHI', hidden: true }
               , { label: 'PH', name: 'W', index: 'W', hidden: true }
               , { label: 'PH', name: 'SHUI', index: 'SHUI', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    ADMJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    ADMJqGrid.refreshGrid();
                }
                ,
                handleADM: function (btn) {
                    ADMJqGrid.handleEdit({
                        loadFrom: 'ADMFormDiv',
                        prefix: 'Lab_ADM',
                        btn: btn
                    });
                },
                handleAddADMItems: function (btn) {
                    if (!ADMJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectrecord = ADMJqGrid.getSelectedRecord();
                    ADMItemsJqGrid.handleAdd({
                        loadFrom: 'ADMItemsFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            ADMItemsJqGrid.setFormFieldValue("Lab_ADM_Items.Lab_ADMID", selectrecord.ID);
                        }
                        , postCallBack: function (response) {
                            ADMItemsJqGrid.refreshGrid("Lab_ADMID='" + selectrecord.ID + "'");
                        }
                    });
                }
            }
    });

    var ADMItemsJqGrid = new MyGrid({
        renderTo: 'ADMItemsJqGrid'
            , autoWidth: true
            , buttons: buttons2
            , height: gGridHeight / 2
            , dialogWidth: 700
            , dialogHeight: 550
		    , storeURL: "/Lab_ADM_Items.mvc/Find"
		    , sortByField: 'NO'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: false
            , autoLoad: false
		    , initArray: [
                { label: '记录id', name: 'ID', index: 'ID', hidden: true }
               , { label: '父id', name: 'Lab_ADMID', index: 'Lab_ADMID', hidden: true }
               , { label: '样品编号', name: 'NO', index: 'NO' }
               , { label: 'PH', name: 'PH', index: 'PH', hidden: true }
               , { label: 'PH', name: 'SL', index: 'SL', hidden: true }
               , { label: 'PH', name: 'WSL', index: 'WSL', hidden: true }
               , { label: 'PH', name: 'PW', index: 'PW', hidden: true }
               , { label: 'PH', name: 'WPW', index: 'WPW', hidden: true }
               , { label: 'PH', name: 'GWPW', index: 'GWPW', hidden: true }
               , { label: 'PH', name: 'LD', index: 'LD', hidden: true }
               , { label: 'PH', name: 'JS', index: 'JS', hidden: true }
               , { label: 'PH', name: 'WS', index: 'WS', hidden: true }
               , { label: 'PH', name: 'T', index: 'T', hidden: true }
               , { label: 'PH', name: 'TH', index: 'TH', hidden: true }
               , { label: 'PH', name: 'YQ', index: 'YQ', hidden: true }
               , { label: 'PH', name: 'Q', index: 'Q', hidden: true }
               , { label: 'PH', name: 'YG', index: 'YG', hidden: true }
               , { label: 'PH', name: 'G', index: 'G', hidden: true }
               , { label: 'PH', name: 'JC', index: 'JC', hidden: true }
               , { label: 'PH', name: 'JZ', index: 'JZ', hidden: true }
               , { label: 'PH', name: 'WC', index: 'WC', hidden: true }
               , { label: 'PH', name: 'WZ', index: 'WZ', hidden: true }
               , { label: 'PH', name: 'J3dH1', index: 'J3dH1', hidden: true }
                , { label: 'PH', name: 'J3dH2', index: 'J3dH2', hidden: true }
                , { label: 'PH', name: 'J3dH3', index: 'J3dH3', hidden: true }
                , { label: 'PH', name: 'J3dD1', index: 'J3dD1', hidden: true }
                , { label: 'PH', name: 'J3dD2', index: 'J3dD2', hidden: true }
                , { label: 'PH', name: 'J3dD3', index: 'J3dD3', hidden: true }
                , { label: 'PH', name: 'J7dH1', index: 'J7dH1', hidden: true }
                , { label: 'PH', name: 'J7dH2', index: 'J7dH2', hidden: true }
                , { label: 'PH', name: 'J7dH3', index: 'J7dH3', hidden: true }
                , { label: 'PH', name: 'J7dD1', index: 'J7dD1', hidden: true }
                , { label: 'PH', name: 'J7dD2', index: 'J7dD2', hidden: true }
                , { label: 'PH', name: 'J7dD3', index: 'J7dD3', hidden: true }
                , { label: 'PH', name: 'J28dH1', index: 'J28dH1', hidden: true }
                , { label: 'PH', name: 'J28dH2', index: 'J28dH2', hidden: true }
                , { label: 'PH', name: 'J28dH3', index: 'J28dH3', hidden: true }
                , { label: 'PH', name: 'J28dD1', index: 'J28dD1', hidden: true }
                , { label: 'PH', name: 'J28dD2', index: 'J28dD2', hidden: true }
                , { label: 'PH', name: 'J28dD3', index: 'J28dD3', hidden: true }
                , { label: 'PH', name: 'W3dH1', index: 'W3dH1', hidden: true }
                , { label: 'PH', name: 'W3dH2', index: 'W3dH2', hidden: true }
                , { label: 'PH', name: 'W3dH3', index: 'W3dH3', hidden: true }
                , { label: 'PH', name: 'W3dD1', index: 'W3dD1', hidden: true }
                , { label: 'PH', name: 'W3dD2', index: 'W3dD2', hidden: true }
                , { label: 'PH', name: 'W3dD3', index: 'W3dD3', hidden: true }
                , { label: 'PH', name: 'W7dH1', index: 'W7dH1', hidden: true }
                , { label: 'PH', name: 'W7dH2', index: 'W7dH2', hidden: true }
                , { label: 'PH', name: 'W7dH3', index: 'W7dH3', hidden: true }
                , { label: 'PH', name: 'W7dD1', index: 'W7dD1', hidden: true }
                , { label: 'PH', name: 'W7dD2', index: 'W7dD2', hidden: true }
                , { label: 'PH', name: 'W7dD3', index: 'W7dD3', hidden: true }
                , { label: 'PH', name: 'W28dH1', index: 'W28dH1', hidden: true }
                , { label: 'PH', name: 'W28dH2', index: 'W28dH2', hidden: true }
                , { label: 'PH', name: 'W28dH3', index: 'W28dH3', hidden: true }
                , { label: 'PH', name: 'W28dD1', index: 'W28dD1', hidden: true }
                , { label: 'PH', name: 'W28dD2', index: 'W28dD2', hidden: true }
                , { label: 'PH', name: 'W28dD3', index: 'W28dD3', hidden: true }
                , { label: 'PH', name: 'J1', index: 'J1', hidden: true }
                , { label: 'PH', name: 'J2', index: 'J2', hidden: true }
                , { label: 'PH', name: 'J3', index: 'J3', hidden: true }
                , { label: 'PH', name: 'J4', index: 'J4', hidden: true }
                , { label: 'PH', name: 'J5', index: 'J5', hidden: true }
                , { label: 'PH', name: 'J6', index: 'J6', hidden: true }
                , { label: 'PH', name: 'J7', index: 'J7', hidden: true }
                , { label: 'PH', name: 'J8', index: 'J8', hidden: true }
                , { label: 'PH', name: 'J9', index: 'J9', hidden: true }
                , { label: 'PH', name: 'J10', index: 'J10', hidden: true }
                , { label: 'PH', name: 'J11', index: 'J11', hidden: true }
                , { label: 'PH', name: 'J12', index: 'J12', hidden: true }
                , { label: 'PH', name: 'J13', index: 'J13', hidden: true }
                , { label: 'PH', name: 'W1', index: 'W1', hidden: true }
                , { label: 'PH', name: 'W2', index: 'W2', hidden: true }
                , { label: 'PH', name: 'W3', index: 'W3', hidden: true }
                , { label: 'PH', name: 'W4', index: 'W4', hidden: true }
                , { label: 'PH', name: 'W5', index: 'W5', hidden: true }
                , { label: 'PH', name: 'W6', index: 'W6', hidden: true }
                , { label: 'PH', name: 'W7', index: 'W7', hidden: true }
                , { label: 'PH', name: 'W8', index: 'W8', hidden: true }
                , { label: 'PH', name: 'W9', index: 'W9', hidden: true }
                , { label: 'PH', name: 'W10', index: 'W10', hidden: true }
                , { label: 'PH', name: 'W11', index: 'W11', hidden: true }
                , { label: 'PH', name: 'W12', index: 'W12', hidden: true }
                , { label: 'PH', name: 'W13', index: 'W13', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    ADMItemsJqGrid.reloadGrid();
                }
                ,
                handleRefresh: function (btn) {
                    ADMItemsJqGrid.refreshGrid();
                },
                handleEdit: function (btn) {
                    ADMItemsJqGrid.handleEdit({
                        loadFrom: 'ADMItemsFormDiv',
                        prefix: 'Lab_ADM_Items',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    ADMItemsJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    myJqGrid.addListeners("rowclick", function (id, record) {
        ADMJqGrid.refreshGrid("Lab_RecordID='" + id + "'");
    });
    ADMJqGrid.addListeners("rowclick", function (id, record) {
        ADMItemsJqGrid.refreshGrid("Lab_ADMID='" + id + "'");
    });
}