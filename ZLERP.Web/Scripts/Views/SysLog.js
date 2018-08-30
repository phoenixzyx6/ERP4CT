function syslogsIndexInit(storeUrl) {
    function viewDetail(cellvalue, options, rowObject) {　
        return '<a data='+rowObject["ID"]+' href="javascript:void 0;">查看对象</a>';
    }

    function viewRow(id) {
        var rowObject = myJqGrid.getRecordByKeyValue(id);
        var dialog = $('#dlgObjectDetail');
        dialog.empty();
        dialog.dialog({ autoOpen: false, bgiframe: true, resizable: true, height:300, width: 400, title: '对象数据' });
        var objData = rowObject['ObjectData'];
        if (objData) {
        try{
            var obj = $.parseJSON(objData);
            var html = $('<ul>');
            for (var d in obj) {
                html.append("<li><label>" + d + ":</label>" + obj[d] + "</li>");
            }
            html.appendTo(dialog);
            dialog.dialog('open');

        }
        catch(e)
        {
            alert(objData);
        }
      }
        
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid' 
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
		    , initArray: [
                { label: '日志ID', name: 'ID', index: 'ID', width: 50 }
                , { label: '类型', name: 'LogType', index: 'LogType', width: 80 }
                , { label: '用户IP', name: 'UserIP', index: 'UserIP',width:80 }
                , { label: 'Url', name: 'Url', index: 'Url' }
                , { label: '备注', name: 'Memo', index: 'Memo' }
                , { label: '对象ID', name: 'ObjectId', index: 'ObjectId', width: 50 }
                , { label: '对象类型', name: 'ObjectType', index: 'ObjectType' }
                , { label: '对象数据', name: 'ObjectData', index: 'ObjectData', hidden: true, viewable: true }
                , { label: '操作人', name: 'Builder', index: 'Builder', width: 50 }
                , { label: '时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '查看', name: '', formatter: viewDetail }
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
    $('a[data]').live('click', function (e) {
        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;
        viewRow($(this).attr('data'));
    });
}