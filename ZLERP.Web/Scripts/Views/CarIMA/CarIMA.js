function carimaIndexInit(storeUrl) {
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
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '车辆号', name: 'CarID', index: 'CarID', width: 60 }
                , { label: '车辆牌照', name: 'CarNo', index: 'CarNo', width: 100 }
                , { label: '保险日期限', name: 'InsuranceDate', index: 'InsuranceDate', formatter: 'date', width: 100 }
                , { label: '商险日期限', name: 'BusInsuranceDate', index: 'BusInsuranceDate', width: 100, formatter: 'date' }
                , { label: '保险是否办理', name: 'InsuranceIsHandle', index: 'InsuranceIsHandle', width: 100, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '否', '1': '是' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '车辆年审日期', name: 'CarAnnualVerDate', index: 'CarAnnualVerDate', width: 100, formatter: 'date' }
                , { label: '车辆年审否', name: 'CarAnnualVerIsHandle', index: 'CarAnnualVerIsHandle', width: 100, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '否', '1': '是' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '运营证年审日', name: 'CerAnnualVerDate', index: 'CerAnnualVerDate', formatter: 'date', width: 100 }
                , { label: '运营证年审否', name: 'CerAnnualVerIsHandle', index: 'CerAnnualVerIsHandle', width: 100, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '否', '1': '是' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '第一次维护否', name: 'FMaintainIsHandle', index: 'FMaintainIsHandle', width: 100, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '否', '1': '是' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '第一次下次时间', name: 'FMaintainNextTime', index: 'FMaintainNextTime', width: 100, formatter: 'date' }
                , { label: '第二次维护否', name: 'SMaintainIsHandle', index: 'SMaintainIsHandle', width: 100, align: 'center', formatter: booleanFmt, formatterStyle: { '0': '否', '1': '是' }, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '第二次下次时间', name: 'SMaintainNextTime', index: 'SMaintainNextTime', width: 100, formatter: 'date' }
                , { label: '路桥', name: 'RoadBridge', index: 'RoadBridge', width: 100 }

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
