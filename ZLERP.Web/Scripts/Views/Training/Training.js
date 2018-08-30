function trainingIndexInit(options) {
    var xheditorSettings = { skin: 'nostyle', tools: 'Blocktag,Fontface,Bold,Italic,Underline,Strikethrough,FontColor,|,Align,List,Outdent,Indent,|,Preview,Fullscreen', editorRoot: '/Content/' };
    function titleFormatter(cellvalue, options, rowObject) {
        return "<a href='#' data-id='" + rowObject["ID"] + "'>" + cellvalue + "</a>";
    }
    function titleUnFormatter(cellvalue, options, cell) {
        return $('a', cell).html();
    }
    var myJqGrid = new MyGrid({
        renderTo: 'TrainingGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 500
            , dialogHeight: 455
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '项目编号', name: 'ID', index: 'ID', width:80 }
                , { label: '培训项目', name: 'TrainName', index: 'TrainName', formatter: titleFormatter, unformat: titleUnFormatter, width: 200 }
                , { label: '培训日期', name: 'TrainDate', index: 'TrainDate', formatter: 'date', width: 80 }
                , { label: '培训内容', name: 'TrainContent', index: 'TrainContent', hidden:true }
                , { label: '培训费用', name: 'TrainCost', index: 'TrainCost', width: 60, formatter: 'currency' }
                , { label: '培训讲师', name: 'TrainTeacher', index: 'TrainTeacher', width: 80 }
                , { label: '预计人数', name: 'Plans', index: 'Plans', width: 80 }
                , { label: '参与人数', name: 'Infact', index: 'Infact', width: 80 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 200 }
                , { label: '发布人', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '发布时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', width: 150 }
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
                        loadFrom: 'TrainingForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            var editor = $('#TrainContent');
                            editor.xheditor(false);
                            setTimeout(function () { editor.xheditor(xheditorSettings); }, 300);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'TrainingForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            var editor = $('#TrainContent');
                            editor.xheditor(false);
                            setTimeout(function () { editor.xheditor(xheditorSettings); }, 300);
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
        $('#dialogDetail').dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: true, width: 500, height: 350, title: '培训内容（详细描述）' });
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
        
}