function dbHelperInit(opts) {

    $('#btnBackupDB').click(function () {
        //backup db
        $('#dlgBackupOptions').dialog({
            width: 280,
            height: 200,
            modal: true,
            buttons: {
                '取消': function (btn, opt) {
                    $(this).dialog("close");
                },
                '备份': function (btn) {
                    var caller = $(btn.target);
                    caller.attr('disabled', true);
                    caller.addClass('ui-state-disabled'); 
                    $(this).parent().mask('正在备份...');
                    var options = {};
                    options.fileName = $("[name='fileName']").val();
                    options.compress = $("[name='compress']").attr('checked');
                    var dialog = $(this);
                   
                    jQuery.ajax({
                        url: opts.backupDBUrl,
                        type: 'POST',
                        data: options,
                        success: function (response) {
                            showMessage('提示', response.Message);
                            
                            dialog.dialog("close");
                        },
                        complete: function (xhr, textStatus) {
                            caller.attr('disabled', false);
                            caller.removeClass('ui-state-disabled');
                            dialog.parent().unmask();
                        }
                    });
                }

            }
        });



    });

    $('#btnDownload').click(function () {
        $('#dlgBackupFiles').empty();
        ajaxRequest(opts.listfileUrl, {}, false, function (response) {
            if (response && response.length>0) {
                var ul = $('<ol>');
                for (var i = 0; i < response.length; i++) {
                    var file = response[i];
                    ul.append('<li><a target="_blank" title="下载备份文件" href="' + file.Url + '">' + file.Name + '(' + file.Size + ')</a>　-　' + file.CreateDate + '</li>');
                }
                ul.appendTo('#dlgBackupFiles');
                $('#dlgBackupFiles').dialog({ width: 400, height: 400 });
            }
            else {
                showMessage('没有检测到任何备份文件');
            }

        }, null, $(this));
    });
    $('#btnTablesInfo').click(function () {
        $('#dlgTablesInfo').dialog({ width: 700, height: 600, modal: true,
            buttons: {

                "确认": function () {
                    $(this).dialog("close");

                }
            }
        });
        var myJqGrid = new MyGrid({
            renderTo: 'innerTablesInfo'
            , width: '685'
            , forceFit: true
            , height: '350'
		    , storeURL: opts.tablesInfoUrl
		    , sortByField: 'Name'
            , sortOrder: 'ASC'
		    , primaryKey: 'Name'
		    , setGridPageSize: 1000
		    , showPageBar: false
            , loadonce: true
            , singleSelect: true
            , rowNumbers: true
		    , initArray: [
                { label: '表名', name: 'Name', index: 'Name' }
                 , { label: '描述', name: 'Desc', index: 'Desc', sorttype: 'text' }
                , { label: '数据行数', name: 'Rows', sorttype: 'int', align: 'right', index: 'Rows', width: 60 }
                , { label: '数据大小', name: 'Data', index: 'Data', align: 'right', width: 60 }
                , { label: '索引大小', name: 'IndexSize', index: 'IndexSize', align: 'right', width: 60 }
                , { label: '空闲', name: 'UnUsed', index: 'UnUsed', align: 'right', width: 60 }
                , { label: '总大小', name: 'Reserved', index: 'Reserved', align: 'right', width: 60 }

		    ]
        });
        var dbGrid = new MyGrid({
            renderTo: 'innerDBInfo'
            , width: '685'
            , forceFit: true
            , height: 'auto'
		    , storeURL: opts.dbfileInfoUrl
		    , sortByField: 'Name'
            , sortOrder: 'ASC'
		    , primaryKey: 'Name'
		    , setGridPageSize: 100
		    , showPageBar: false
            , loadonce: true
            , singleSelect: true
            , rowNumbers: true
		    , initArray: [
                { label: '名称', name: 'Name', index: 'Name' }
                 , { label: '文件', name: 'FileName', index: 'FileName', sorttype: 'text', width: 300 }
                , { label: '大小', name: 'Size', align: 'right', index: 'Size', width: 60 }
                , { label: '最大限制', name: 'MaxSize', index: 'MaxSize', align: 'right', width: 60 }

		    ]
        });

    });
}