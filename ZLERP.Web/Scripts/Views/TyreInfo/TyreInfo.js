function tyreinfoIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
            , dialogHeight: 300
            , groupField: 'CurrentStatus'
            , groupingView: { groupDataSorted: true }
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '轮胎编号', name: 'ID', index: 'ID', width: 60 }
                , { label: '进货日期', name: 'StockDate', index: 'StockDate', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '轮胎类别', name: 'TyreType', index: 'TyreType', width: 60 }
                , { label: '轮胎型号', name: 'TyreModel', index: 'TyreModel', width: 80 }
                , { label: '轮胎规格', name: 'TyreSpec', index: 'TyreSpec', width: 60 }
                , { label: '车辆代号', name: 'CarID', index: 'CarID', width: 60 }
                , { label: '当前所在位置', name: 'InstallPlace', index: 'InstallPlace', width: 100 }
                , { label: '生产厂家', name: 'Manufacture', index: 'Manufacture' }
                , { label: '轮胎品牌', name: 'BreedCode', index: 'BreedCode', width: 60 }
                , { label: '耐用里程', name: 'MileageAble', index: 'MileageAble', width: 60 }
                , { label: '使用里程', name: 'MileageUsed', index: 'MileageUsed', width: 60 }
                , { label: '当前状态', name: 'CurrentStatus', index: 'CurrentStatus', align: 'center', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TyreStatus' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('TyreStatus')} }
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
                            myJqGrid.setFormFieldReadOnly('ID', false);
                            myJqGrid.setFormFieldDisabled('InstallPlace', true);
                            myJqGrid.setFormFieldDisabled('CarID', true);
                            myJqGrid.getFormField('CurrentStatus').val('TyreStatus1');
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {

                            myJqGrid.setFormFieldReadOnly('ID', true);
                            myJqGrid.setFormFieldDisabled('InstallPlace', true);
                            myJqGrid.setFormFieldDisabled('CarID', true);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleInstallTyre: function (btn) {

                    var record = myJqGrid.getSelectedRecord();
                    if (record['CurrentStatus'] != 'TyreStatus1') {
                        showMessage("消息", "只能安装待用的轮胎！");
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'NewTyreFormDiv',
                        btn: btn,
                        height: 150,
                        width: 500,
                        postUrl: options.InstallTyreUrl,
                        afterFormLoaded: function () {
                            
                            myJqGrid.getFormField('CarID').bind('change', function () {

                                bindSelectData(myJqGrid.getFormField('InstallPlace'),
                                            options.CarClassUrl,
                                            { textField: 'TyrPlace',
                                                valueField: 'TyrPlace',
                                                foreignValue: myJqGrid.getFormField('CarID').val() 
                                            });

                            });


                        }
                    });
                }
            }
    });

}