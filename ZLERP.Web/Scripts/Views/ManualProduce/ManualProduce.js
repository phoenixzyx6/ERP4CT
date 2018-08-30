function manualProduceIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: opts.storeUrl
		, primaryKey: 'ID'
		, setGridPageSize: 100
        , dialogWidth: 500
        , dialogHeight: 420
		, showPageBar: true
        , singleSelect: false
        , initArray: [
            { label: '手动生产编号', name: 'ID', index: 'ID', width: 80 }
            , { label: '生产登记号', name: 'DispatchID', index: 'DispatchID', width: 80 }
            , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80 }
            , { label: '运输单号', name: 'ShipDocID', index: 'ShipDocID', width: 80 }   
            , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 200 }
            , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 80 }
            , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
            , { label: '调度方量', name: 'SendCube', index: 'SendCube', align: 'right', width: 80 }
            , { label: '手动方量', name: 'ManualCube', index: 'ManualCube', align: 'right', width: 80 }
            , { label: '调度罐次', name: 'SendPot', index: 'TotalPot', align: 'right', width: 80 }
            , { label: '开始罐次', name: 'BeginPot', index: 'BeginPot', align: 'right', width: 80 }
            , { label: '手动罐次', name: 'ManualPot', index: 'ManualPot', align: 'right', width: 80 }
            //, { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', width: 80 }
            , { label: '生产线编号', name: 'ProductLineID', index: 'ProductLineID', width: 80 }
            , { label: '操作员', name: 'Operator', index: 'Operator', width: 80 }
            , { label: '生产时间', name: 'ProduceTime', index: 'ProduceTime', formatter: 'datetime', width: 120 }
            , { label: '手动原因', name: 'ManualReason', index: 'ManualReason', width: 200 }
            , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }            
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
            },
            handleAdd: function (btn) {
                myJqGrid.handleAdd({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        DispatchIDBlur();
                    },
                    postCallBack: function (response) {
                    }
                });
            },
            handleEdit: function (btn) {
                myJqGrid.handleEdit({
                    loadFrom: 'MyFormDiv',
                    btn: btn,
                    afterFormLoaded: function () {
                        DispatchIDBlur();
                    }
                });
            }, 
            handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    //通过生产登记号获取生产详细信息
    DispatchIDBlur = function () {
        var dispatchIDField = myJqGrid.getFormField("DispatchID");
        dispatchIDField.unbind('change');
        dispatchIDField.bind('change', function () {
            var dispatchID = dispatchIDField.val();
            if (!isEmpty(dispatchID)) {
                var requestURL = opts.getDispatchInfoByIDUrl;
                var postParams = { id: dispatchID };
                ajaxRequest(requestURL, postParams, false, function (response) {
                    if (response) {
                        myJqGrid.getFormField("TaskID").val(response.TaskID);
                        myJqGrid.getFormField("ShipDocID").val(response.ShipDocID);
                        myJqGrid.getFormField("ProjectName").val(response.ProjectName);
                        myJqGrid.getFormField("ConStrength").val(response.ConStrength);
                        myJqGrid.getFormField("ProductLineID").val(response.ProductLineID);
                        myJqGrid.getFormField("CarID").val(response.CarID);
                        myJqGrid.getFormField("SendCube").val(response.SendCube);
                        myJqGrid.getFormField("SendPot").val(response.SendPot);
                        myJqGrid.getFormField("Operator").val(response.Operator);
                    }
                })
            }
        })
    }

    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        myJqGridItems.refreshGrid("ManualProductID='" + id + "'");
    });

    var myJqGridItems = new MyGrid({
        renderTo: 'MyJqGridItems'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons1
        , height: gGridHeight
		, storeURL: '/ManualProductItems.mvc/Find'
		, primaryKey: 'ID'
		, setGridPageSize: 100
        , dialogWidth: 500
        , dialogHeight: 420
		, showPageBar: true
        , singleSelect: false
        , initArray: [
            { label: '生产编号', name: 'ID', index: 'ID', width: 80, hidden: true }
            , { label: '筒仓ID', name: 'SiloID', index: 'SiloID', width: 120, hidden: true }
             , { label: '筒仓', name: 'SiloName', index: 'SiloName', width: 120 }
            , { label: '材料ID', name: 'StuffID', index: 'StuffID', width: 80, hidden: true }
            , { label: '材料名称', name: 'StuffName', index: 'StuffName', width: 120 }
            , { label: '消耗数量', name: 'ActualAmount', index: 'ActualAmount', width: 80 }
            , { label: '主表ID', name: 'ManualProductID', index: 'ManualProductID', width: 80, hidden: true }
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGridItemsItems.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGridItemsItems.refreshGrid();
            },
            handleAdd: function (btn) {
                if (!myJqGrid.isSelectedOnlyOne()) {
                        showError("提示", "请选择左边表格一条记录");
                        return false;
                }
                var selectrecord = myJqGrid.getSelectedRecord();

                myJqGridItems.handleAdd({
                    loadFrom: 'MyFormDivItems',
                    btn: btn,
                    width: 400,
                    height:200,
                    beforeSubmit: function () {
                       
                        return true;
                    },
                    afterFormLoaded: function () {

                        $("#ManualProductID").val(selectrecord.ID);
                    },
                    postCallBack: function (response) {
                    }
                });
            },
            handleEdit: function (btn) {              
                var selectrecord = myJqGrid.getSelectedRecord();
                myJqGridItems.handleEdit({
                    loadFrom: 'MyFormDivItems',
                    btn: btn,
                    width: 400,
                    height: 200,
                    afterFormLoaded: function () {
                        $("#ManualProductID").val(selectrecord.ID);
                    }
                });
            },
            handleDelete: function (btn) {
                myJqGridItems.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });

    $('#StuffID').bind('blur', function () {
        bindSelectData($('#SiloID'), opts.findSiloUrl, { condition: "stuffid='" + $(this).val() + "'" });
    });
}