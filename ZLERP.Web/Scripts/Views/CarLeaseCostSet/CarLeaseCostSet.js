function IndexInit(storeUrl) {
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
             , dialogWidth: 450
            , dialogHeight: 300
		    , showPageBar: true
            , groupingView: { groupSummary: [true], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            , groupField: 'CarID'
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 60 }
                , { label: '费用项目', name: 'CostItemName', index: 'CostItemName', width: 100, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "CarLeaseCostType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("CarLeaseCostType")} }
                , { label: '单位', name: 'Unit', index: 'Unit', width: 60 }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 80 }
                , { label: '数量', name: 'Amount', index: 'Amount', width: 100, align: 'center' }
                , { label: '金额', name: 'Money', index: 'Money', width: 100 }
                , { label: '发生时间', name: 'CostDate', index: 'CostDate', formatter: 'date', width: 100 }
                , { label: '备注', name: 'Meno', index: 'Meno', width: 100 }
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
                            CarInfoChange();
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            CarInfoChange();
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
    //选择车号获取对应的车牌牌照
    CarInfoChange = function () {
        var carIdField = $($("[name='CarID']")[0]);
        carIdField.unbind('change');
        carIdField.bind('change', function (event) {
            var id = carIdField.val();
            if (id == "") {
                return;
            }
            ajaxRequest(
            "/Car.mvc/Get",
            { id: id },
            false,
            function (response) {
                if (response.ResultCode == 0) {
                    $($("[name='CarNo']")[0]).val(response.Data.CarNo);
                }
            }
            );
        });
    }

}
