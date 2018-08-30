function contractpumpIndexInit(options) {
    var ContractGrid = new MyGrid({
        renderTo: 'ContractGrid'
		    //, title: '合同列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.contractStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 18
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
		    , initArray: [
                { label: '合同编号', name: 'ID', index: 'ID', width:80 }
                , { label: '客户编码', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'Customer.CustName',width:200}
                , { label: '合同号', name: 'ContractNo', index: 'ContractNo',hidden:true }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 180 }
                , { label: '建设单位', name: 'BuildUnit', index: 'BuildUnit',hidden:true }
                , { label: '项目地址', name: 'ProjectAddr', index: 'ProjectAddr',hidden:true }
                , { label: '签订总方量', name: 'SignTotalCube', index: 'SignTotalCube',width:100,hidden:true }
                , { label: '签订日期', name: 'SignDate', index: 'SignDate', formatter: 'date', hidden:true }
                
		    ]
            , functions: {
                handleReload: function (btn) {
                    ContractGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractGrid.refreshGrid();
                }      
            }
    });
    ContractGrid.addListeners('rowclick', function (id, record, selBool) {
        $("#ContractIDs").val(id);
        $("#ContractID").val(id);
        $("#SignDate").val(record.SignDate);
        $("#CustName").val(record.CustName);
        $("#ContractName").val(record.ContractName);
        $("#SignTotalCube").val(record.SignTotalCube);
        ContractPumpGrid.getJqGrid().jqGrid('setGridParam', { editurl: "/ContractPump.mvc/Delete" });
        ContractPumpGrid.refreshGrid("ContractID='" + id + "'");
    });
    //导入泵车
    window.ImportPump = function () {
        var _ContractID = $("[name='ContractIDs']").val();
        if (isEmpty(_ContractID)) {
            showMessage('提示', '请选择一条记录进行操作！');
            return false;
        }
        ContractPumpGrid.showWindow({
            title: '导入泵车',
            width: 400,
            height: 300,
            loadFrom: 'ImportPumpForm',
            afterLoaded: function () {
                pumpTypeGrid.refreshGrid("ParentID='CastMode'");
            },
            buttons:
                { "关闭": function () {
                    $(this).dialog("close");
                },"确定": function () {
                    var ids = pumpTypeGrid.getSelectedKeys();
                    if (ids.length <= 0) {
                        $(this).dialog("close");
                        return false;
                    } else {
                        ajaxRequest("/ContractPump.mvc/ImportEntitys", { ids: ids, contractID: _ContractID }, true, function (data) {
                            $("#ImportPumpForm").dialog("close");
                            ContractPumpGrid.refreshGrid("ContractID='" + _ContractID + "'");
                        });
                    }

                }
                }
        });
    };
    //保存泵车价格
    window.pumpPriceSave = function () {
        var _ContractID = $("[name='ContractIDs']").val();
        if (isEmpty(_ContractID)) {
            showMessage('提示', '请选择一条记录进行操作！');
            return false;
        }
        $.post(
                "/ContractPump.mvc/Add",
                $("#ContractPumpForms").serialize(),
                function (data) {
                    //
                    if (!data.Result) {
                        showError("出错啦！", data.Message);
                        return false;
                    }
                    //$("#ContractPumpForms")[0].reset();
                    $("#PumpType").val("");
                    $("#PumpPrice").val("");
                    ContractPumpGrid.refreshGrid("ContractID='" + _ContractID + "'");
                }
        );
    };
    //合同泵车价格设定
    var ContractPumpGrid = new MyGrid({
        renderTo: 'ContractPumpGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight * 0.44
		    , storeURL: options.contractPumpStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , storeCondition: '1<>1'
		    , initArray: [
                { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 合同编号', name: 'ContractID', index: 'ContractID', hidden: true }
                , { label: ' 日期', name: 'SetDate', index: 'SetDate', width: 100, formatter: 'date', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: ' 泵车类型', name: 'PumpType', index: 'PumpType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CastMode' }, width: 120, editable: true, edittype: 'select', editoptions: { value: dicToolbarSearchValues('CastMode') }, editrules: { required: true} }
                , { label: ' 泵车价格', name: 'PumpPrice', index: 'PumpPrice', editable: true, formatter: 'number', editrules: { number: true }, align: 'right', width: 100 }
                , { label: '操作', name: 'delac', width: 80, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
		    ]
		    , autoLoad: false
            , editSaveUrl: options.pumpPriceUpdateUrl
            , functions: {
                handleReload: function (btn) {
                    ContractPumpGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractPumpGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    ContractPumpGrid.handleAdd({
                        loadFrom: 'ContractPumpForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    ContractPumpGrid.handleEdit({
                        loadFrom: 'ContractPumpForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    ContractPumpGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //泵车类型
    var pumpTypeGrid = new MyGrid({
        renderTo: 'pumpTypeGrid'
            , autoWidth: true
            , buttons: buttons0
            //, height: gGridHeight * 0.44
		    , storeURL: '/Dic.mvc/Find'
            , storeCondition: "ParentID='CastMode'"
		    , sortByField: 'ID'
		    , primaryKey: 'TreeCode'
		    , setGridPageSize: 30
		    , showPageBar: false
		    , initArray: [
                { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 泵车类型', name: 'DicName', index: 'DicName', sortable:false}
		    ]
		    , autoLoad: false
            //, show
            , functions: {
                handleReload: function (btn) {
                    ContractPumpGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ContractPumpGrid.refreshGrid('1=1');
                }
            }
    });

}