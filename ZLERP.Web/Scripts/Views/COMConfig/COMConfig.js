function comconfigIndexInit(storeUrl) {
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
		    , initArray: [
                { label: 'COM代号', name: 'ID', index: 'ID', width: 100 }
                , { label: 'COM名称', name: 'COMName', index: 'COMName', width: 100 }
                , { label: '是否打开', name: 'IsOpen', index: 'IsOpen', width: 100, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: 'COM口', name: 'PortName', index: 'PortName', width: 100 }
                , { label: '波特率', name: 'BaudRate', index: 'BaudRate', width: 100 }
                , { label: '校验位', name: 'Parity', index: 'Parity', width: 100 }
                , { label: '数据位', name: 'DataBits', index: 'DataBits', width: 100 }
                , { label: '停止位', name: 'StopBits', index: 'StopBits', width: 100 }
                , { label: '起始字符', name: 'BeginCode', index: 'BeginCode', width: 100 }
                , { label: '截止字符', name: 'EndCode', index: 'EndCode', width: 100 }
                , { label: '数据模式', name: 'DataModel', index: 'DataModel', width: 100 }
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
                }
            }
    });

}