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
		    , showPageBar: true
            , dialogWidth: 400
            , dialogHeight: 280
		    , initArray: [
                { label: '帐套号', name: 'ID', index: 'ID', width: 100 }
                , { label: '启动状态', name: 'IsRun', index: 'IsRun', width: 100, formatter: booleanFmt, formatterStyle: { '0': '未启用', '1': '已启用' }, unformat: booleanUnFmt }
                , { label: '帐套名称', name: 'YearValue', index: 'YearValue', width: 100 }
                , { label: '开始时间', name: 'BeginDate', index: 'BeginDate', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '数据库名', name: 'DBName', index: 'DBName', width: 100 }
                , { label: '文件路径', name: 'DBPath', index: 'DBPath', width: 300 }
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
                    showConfirm("提示", "创建帐套之前，请务必确认现有数据库已备份成功。</br>数据库已备份成功？", function () {
                        myJqGrid.handleAdd({
                            loadFrom: 'MyFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                myJqGrid.setFormFieldDisabled('IsRun', true);
                                myJqGrid.setFormFieldDisabled('IsRun', true);
                                myJqGrid.setFormFieldReadOnly('ID', false);
                                myJqGrid.setFormFieldReadOnly('YearValue', false);
                                myJqGrid.setFormFieldReadOnly('DBPath', false);
                            }
                            , beforeSubmit: function () {
                                if (!(IsLicit(myJqGrid.getFormField("ID").val()))) {
                                    showError("提示", "帐套号或者帐套名存在非法字符");
                                    return false;
                                }
                                return true;
                            }
                        });
                    }, null);
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('IsRun', true);
                            myJqGrid.setFormFieldReadOnly('ID', false);
                            myJqGrid.setFormFieldReadOnly('YearValue', false);
                            myJqGrid.setFormFieldReadOnly('DBPath', false);


                        }
                    });
                }
                ,
                handleRun: function (btn) {
                    var record = myJqGrid.getSelectedRecord();

                    if (isEmpty(record.DBName)) {
                        showError("警告", "该帐套未生成数据库，不能启用！");
                        return false;
                    }

                    showConfirm("提示", "启用帐套将引起部分数据归档删除，请务必确认已将备份文件还原至选定的帐套数据库。</br>数据库已还原成功？", function () {
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                myJqGrid.setFormFieldDisabled('IsRun', false);
                                myJqGrid.setFormFieldReadOnly('ID', true);
                                myJqGrid.setFormFieldReadOnly('YearValue', true);
                                myJqGrid.setFormFieldReadOnly('DBPath', true);
                            }
                        });
                    }, null);
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}