function rewardsIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 480
            , dialogHeight: 380
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width: 40 }
                , { label: '奖惩对象', name: 'UserID', index: 'UserID', width: 100, hidden: true }
                , { label: '奖惩对象', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: '所在部门', name: 'DepartmentName', index: 'DepartmentName', width: 80, sortable: false }
                , { label: '处理类别', name: 'RewardsType', index: 'RewardsType', align: 'center', width: 80 }
                , { label: '奖惩事由', name: 'RewardsReason', index: 'RewardsReason', width: 300 }
                , { label: '奖惩方式', name: 'RewardsMode', index: 'RewardsMode', width: 80 }
                , { label: '处理结果', name: 'ProcessingResult', index: 'ProcessingResult', width: 200 }
                , { label: '已通知公告', name: 'IsNotice', index: 'IsNotice', width: 70, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '通知公告ID', name: 'NoticeID', index: 'NoticeID', width: 70, hidden: true }
                , { label: '批准日期', name: 'ApproveDate', index: 'ApproveDate', formatter: 'date', width: 80 }
                , { label: '批准人', name: 'ApproveMan', index: 'ApproveMan', width: 80 }
                , { label: '生效日期', name: 'ExcuteDate', index: 'ExcuteDate', formatter: 'date', width: 80 }
                , { label: '执行人', name: 'ExcuteMan', index: 'ExcuteMan', width: 80 }
                , { label: '撤销日期', name: 'RevocationDate', index: 'RevocationDate', formatter: 'date', width: 80 }
                , { label: '撤销原由', name: 'RevocationReason', index: 'RevocationReason' }
                , { label: '状态', name: 'IsEffective', index: 'IsEffective', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { "0": "无效", "1": "有效"} }

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
                            $("#IsNotice").attr("disabled", false).show();
                            $("input[name='IsNotice']").attr("disabled", false);
                            $("#Tip").html("");
                        }
                    });
                },
                handleEdit: function (btn) {
                    var selectrecord = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            if (selectrecord.IsNotice == "true" || selectrecord.IsNotice == 1) {
                                $("#IsNotice").attr("disabled", true);
                                $("input[name='IsNotice']").attr("disabled", true);
                                $("#Tip").html("(此信息已生成通知公告，请到【桌面管理->通知公告】进行管理)");
                            } else {
                                $("#IsNotice").attr("disabled", false).show();
                                $("input[name='IsNotice']").attr("disabled", false);
                                $("#Tip").html("");
                            }
                            myJqGrid.setFormFieldValue("ApproveMan2", selectrecord.ApproveMan);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleRevocation: function (btn) {
                    var selectrecord = myJqGrid.getSelectedRecord();
                    if (selectrecord.IsEffective == "false") {
                        showMessage('提示', '您选择的奖惩记录已撤销');
                        return;
                    }
                    myJqGrid.handleEdit({
                        title: '撤销奖惩',
                        width: 400,
                        height: 300,
                        loadFrom: 'RevocationFormDiv',
                        btn: btn,
                        reloadGrid: false,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldValue("IsEffective", false);

                        }
                        , postCallBack: function (response) {
                            myJqGrid.refreshGrid();
                        }
                    });
                }
            }
    });

}