//机组
function productLineInit(opts) {
    var siloGrid = new MyGrid({
        renderTo: 'silogrid'
            , width: 610
            , height: 330
		    , storeURL: opts.findSiloStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    //, setGridPageSize: 30
		    , showPageBar: false
            , singleSelect: true
            , autoLoad: false
            , buttons: [{ FuncName: '可分配', FuncDesc: '只显示可分配的筒仓', Icon: 'refresh', HandlerName: 'getAvailableSilo', ButtonPos: 0 }
                , { FuncName: '未分配', FuncDesc: '只显示未分配的筒仓', Icon: 'refresh', HandlerName: 'notAssignSilo', ButtonPos: 0 }
                , { FuncName: '全部', FuncDesc: '显示全部筒仓', Icon: 'refresh', HandlerName: 'getAllSilo', ButtonPos: 0 }
            ]
		    , initArray: [
                { label: ' 筒仓编号', name: 'ID', index: 'ID' }
                , { label: ' 筒仓名称', name: 'SiloName', index: 'SiloName'}
                , { label: ' 筒仓简码', name: 'SiloShortName', index: 'SiloShortName'}
                , { name: 'StuffID', index: 'StuffID', hidden: true }
//                , { label: ' 原料名称', name: 'StuffName', index: 'StuffName'}
                , { label: ' 当前容量', name: 'Content', index: 'Content'}
                , { label: ' 最小容量', name: 'MinContent', index: 'MinContent'}
                , { label: ' 最大容量', name: 'MaxContent', index: 'MaxContent'}
                , { label: ' 最小报警容量', name: 'MinWarm', index: 'MinWarm'}
                , { label: ' 最大报警容量', name: 'MaxWarm', index: 'MaxWarm'}
                , { label: ' 排序', name: 'OrderNum', index: 'OrderNum'} 
		    ]
            , functions: {
                getAvailableSilo: function () {
                    refreshSiloList(0);
                }, notAssignSilo: function () {
                    refreshSiloList(1);
                }, getAllSilo: function () {
                    refreshSiloList(2);
                }
            }
    });

    function refreshSiloList(type) {
        var id = productLineGrid.getSelectedKey();
        siloGrid.getJqGrid().jqGrid('setGridParam', { postData: { pid: id, type: type} }).trigger("reloadGrid");
    }

    var productLineGrid = new MyGrid({
        renderTo: 'productLineGrid'
		    , title: '机组列表'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: 100
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: false
		    , initArray: [
                { label: '机组编号', name: 'ID', index: 'ID', align: 'center', width: 80 }
                , { label: '生产线', name: 'ProductLineName', index: 'ProductLineName', align: 'center', width: 50 }
                , { label: '盘容量', name: 'DishNum', index: 'DishNum', width: 50, align: 'center' }
                , { label: '每小时产量', name: 'HourNum', index: 'HourNum', width: 60, align: 'center' }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: 'IP地址', name: 'IP', index: 'IP' }
                , { label: '端口号', name: 'Port', index: 'Port', width: 50, align: 'center' }
                , { label: '上位机类型', name: 'KZType', index: 'KZType', width: 80, align: 'center' }
                , { label: '上位机版本', name: 'KZVersion', index: 'KZVersion', width: 80, align: 'center' }
                , { label: '排序', name: 'OrderNum', index: 'OrderNum', width: 50, align: 'center' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ]
            , functions: {
                handleReload: function (btn) {
                    productLineGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    productLineGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    productLineGrid.handleAdd({
                        title: btn.data.FuncDesc
                        , loadFrom: 'productline-form'
                        , postUrl: btn.data.Url
                        , afterFormLoaded: function () {
                            alert(btn.data.Url);
                            productLineGrid.setFormFieldReadOnly("ID", false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    productLineGrid.handleEdit({
                        title: btn.data.FuncDesc
                        , loadFrom: 'productline-form'
                        , postUrl: btn.data.Url
                        , afterFormLoaded: function () {
                            //                            alert(btn.data.Url);
                            productLineGrid.setFormFieldReadOnly("ID", true);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    productLineGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , assignBale: assignSilo
            }
    });


    function assignSilo(btn) {
        if (productLineGrid.isSelectedOnlyOne('请选择一条记录！')) {
            productLineGrid.showWindow({
                title: btn.data.FuncDesc,
                width: 650,
                height: 480,
                loadFrom: 'silogrid-dialog'
                    , buttons: {
                        "关闭": function () {
                            $(this).dialog("close");
                        }
                    }
            });
            var id = productLineGrid.getSelectedKey();
            siloGrid.getJqGrid().jqGrid('setGridParam', { postData: { pid: id} }).trigger("reloadGrid");
        }
    }


    var siloProductLineGrid = new MyGrid({
        renderTo: 'baleproductlineGrid'
		    , title: '筒仓列表'
            , autoWidth: true
            , buttons: buttons1
            , height: 300
		    , storeURL: opts.findSiloUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 500
            , dialogHeight: 300
		    , setGridPageSize: 30
            , editSaveUrl: opts.updateSiloProductLineUrl
		    , initArray: [
                { label: '筒仓编号', name: 'SiloID', index: 'SiloID', sortable:false }
                , { name: 'ProductLineID', index: 'ProductLineID', hidden: true, sortable: false }
                , { label: '筒仓名称', name: 'SiloName', index: 'SiloName', sortable: false }
                , { label: '材料名称', name: 'StuffName', index: 'StuffName', sortable: false }
                , { label: '筒仓顺序', width: 60, name: 'OrderNum', index: 'OrderNum', sortable: false, editable: gSysConfig["LockBaleOrder"] == "false", editrules: { required: true, integer: true, minValue: 1, maxValue: 24} }
                , { label: '比率', width: 60, name: 'Rate', index: 'Rate', editable: true, sortable: false, editrules: { required: true, float: true, minValue: 0, maxValue: 1} }
                , { label: '秤位', name: 'MeasureID', jsonmap: 'MeasureInfo.MeasureName', sortable: false, editable: gSysConfig["LockBaleOrder"] == "false", sortable: false, index: 'MeasureID', edittype: "select", editoptions: { value: _measureOptions} }
		    ]
            , functions: {
                handleRefresh: function (btn) {
                    siloProductLineGrid.reloadGrid();
                }
                , handleDelete: function (btn) {
                    siloProductLineGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
            , autoLoad: false
    });

    //单击其中一条然后下面加载机组的筒仓
    productLineGrid.addListeners('rowclick', function (id, record, selBool) {
        siloProductLineGrid.getJqGrid().jqGrid('setGridParam', { postData: { pid: id} }).trigger("reloadGrid");
    });

    //筒仓列表双击事件
    siloGrid.addListeners('rowdblclick', function (id, record, selBool) {
        var pid = productLineGrid.getSelectedKey();
        var parsms = { pid: pid, sid: id };
        ajaxRequest(opts.assignSiloUrl, parsms, true, function (response) {
            if (response.Result) {
                siloGrid.reloadGrid();
                siloProductLineGrid.reloadGrid();
            }
        });
    });

}