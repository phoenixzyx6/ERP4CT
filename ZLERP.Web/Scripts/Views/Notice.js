function noticeIndexInit(opts) {
    var xheditorSettings = {skin:'nostyle', tools: 'Blocktag,Fontface,FontSize,Bold,Italic,Underline,Strikethrough,FontColor,BackColor,|,Align,List,Outdent,Indent,|,Link,Img,|,Source,Preview,Fullscreen', editorRoot: '/Content/' };
    function titleFormatter(cellvalue, options, rowObject) {
        return "<a href='#' data-id='" + rowObject["ID"] + "'>" + cellvalue + "</a>";
    };
    function titleUnFormatter(cellvalue, options, cell) {
        return $('a', cell).html();
    }

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'IsTop Desc,ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '公告ID', name: 'ID', index: 'ID', width: 50, search:false }
                , { label: '公告标题', name: 'Title', index: 'Title', width: 400, formatter: titleFormatter, unformat: titleUnFormatter }
                , { label: '公告内容', name: 'Content', index: 'Content', hidden: true }
                , { label: '附件', name: 'Attachments', hidden: true, formatter: attachmentFmt }
                , { label: '置顶', name: 'IsTop', index: 'IsTop', align: 'center', width: 50, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
          		, { label: ' 创建人', name: 'TrueName',jsonmap:'CreateUser.TrueName', index: 'CreateUser.TrueName' }
           		, { label: ' 创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', search:false }
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
                            $('#MyFormDiv').show();
                            var editor = $('#Content');
                            editor.xheditor(false);
                            setTimeout(function () { editor.xheditor(xheditorSettings); }, 300);
                            $('#Attachments').empty();
                        },
                        postCallBack: function (response) {
                            if (response.Result && response.Data * 1 > 0) {
                                attachmentUpload(response.Data);
                            }
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                        ,
                        afterFormLoaded: function () {
                            $('#MyFormDiv').show();
                            var editor = $('#Content');
                            editor.xheditor(false);
                            setTimeout(function () { editor.xheditor(xheditorSettings); }, 300);
                            
                            var attDiv = $('#Attachments');
                            attDiv.empty();
                            attDiv.append(data["Attachments"]);
                            $('a[att-id]').show();
                        },
                        postCallBack: function (response) {
                            if (response.Result) {
                                attachmentUpload(data["ID"]);
                            }
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

        $('#dialogDetail').dialog({modal:true,autoOpen: false, bgiframe: true, resizable: true, width: 800, height: 450, title: '通知公告' });
        $('a[data-id]').live('click', function (e) {
            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;

            var dialogPosition = $(this).offset();
            var dialog = $('#dialogDetail');
            dialog.dialog('option', 'position', [dialogPosition.center, dialogPosition.center]);
            dialog.dialog('open');
            var data = myJqGrid.getRecordByKeyValue($(this).attr('data-id'));
            dialog.empty();

            $('#tmplDetail').tmpl(data).appendTo('#dialogDetail');
            $('a[att-id]').hide();
        });
        $('a[att-id]').live('click', function (e) {
            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;
            var caller = $(this);


            ajaxRequest(opts.deleteAttachmentUrl,
             { id: caller.attr('att-id') },
             false, 
             function (response) {
                if (response.Result) {
                    caller.parent('li').remove();
                }
            });
        });

        function attachmentUpload(objectId) {
            /*		$("#loading")
            .ajaxStart(function(){
            $(this).show();
            })
            .ajaxComplete(function(){
            $(this).hide();
            });*/
            var fileElement = $('#uploadFile');
            if (fileElement.val() == "") return;

            $.ajaxFileUpload
		(
			{
			    url: opts.uploadUrl + '?objectType=Notice&objectId=' + objectId,
			    secureuri: false,
			    fileElementId: 'uploadFile',
			    dataType: 'json',
			    beforeSend: function () {
			        $("#loading").show();
			    },
			    complete: function () {
			        $("#loading").hide();
			    },
			    success: function (data, status) {
			        if (data.Result) {
			            showMessage('附件上传成功');
                        myJqGrid.reloadGrid();
			        }
			        else {
			            showError('附件上传失败', data.Message);
			        }
			    },
			    error: function (data, status, e) {
			        showError(e);
			    }
			}
		);

            return false;

        }

       
}