function msgIndexInit(opts) {
    var msgGrid = new MyGrid({
        renderTo: 'MsgGrid'
        //, width: '100%'
            , title: '系统消息列表'
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: 'buttonToolbar'
            , height: gGridHeight + 7
            , dialogWidth: 400
            , dialogHeight: 300
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 100
		    , showPageBar: false
            , singleSelect: true
            , rowNumbers: true
		    , initArray: [
                { label: '消息编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '消息标题', name: 'MsgTitle', index: 'MsgTitle', width: 100 }
                , { label: '消息类别', name: 'MsgType', index: 'MsgType', width: 60, align:'center'}
                , { label: '所属模块', name: 'BelongFuncID', index: 'BelongFuncID', width: 100, hidden:true }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 250 }
                , { label: '创建者', name: 'Builder', index: 'Builder', width: 80 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 150, formatter: 'datetime' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    msgGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    msgGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    msgGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    msgGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    msgGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //下拉列表帅选待分配的用户
    $("#DepartmentID").attr("disabled", true);
    $("#DepartmentID").unbind("change");
    $("#DepartmentID").bind("change", function () {
        var _departmentid = $(this).val();
        var condition = "UserID NOT IN (" + useridTostring + ")";
        if (_departmentid) {
            condition = "DepartmentID='" + _departmentid + "' and UserID NOT IN (" + useridTostring + ")";
        }
        DFPUserGrid.refreshGrid(condition);
    });
    //点击消息列表的某一记录，列出该记录的已分配用户。
    msgGrid.addListeners('rowclick', function (id, record, selBool) {
        YFPUserGrid.getJqGrid().setCaption("该消息已分配给以下用户（消息名称：<font color=red>" + record.MsgTitle + "</font>）");
        YFPUserGrid.refreshGrid("MsgID='" + id + "'");
        $("#DepartmentID").attr("disabled", false);
        $("#DepartmentID").val("");
    });

    //分配用户
    window.ImportUser = function () {
        if (msgGrid.isSelectedOnlyOne('请先选择一条消息记录记录！')) {
            var ids = DFPUserGrid.getSelectedKeys(); //选择的待分配用户
            var idsclone = ids.slice(0); //此处一定要克隆，否则影响选择的数据
            if (idsclone.length > 0) { //如果选择了待分配用户
                var YFPUserArray = new Array();
                $.each(YFPUserGridRecords, function (i, n) {
                    YFPUserArray.push(n.UserID);
                });
                var flag = false;
                do {
                    $.each(idsclone, function (i, n) {//过滤已经分配了的用户
                        var pos = jQuery.inArray(n, YFPUserArray);
                        if (pos != -1) {
                            idsclone.splice(i, 1);
                            flag = true;
                            return false;
                        } else {
                            flag = false;
                        }
                    });
                } while (flag);
                //将选择的用户添加到消息用户关系表
                var _MsgID = msgGrid.getSelectedRecord().ID;
                ajaxRequest(
                    opts.batchImportUser,
                    {
                        MsgID: _MsgID,
                        UserIDs: idsclone
                    },
                    true,
                    function (response) {
                        YFPUserGrid.refreshGrid("MsgID='" + _MsgID + "'")
                    }
                );
            } else {
                return false;
            }
        }
    };

    //删除用户
    window.handleDeleteUser = function (id) {
        showConfirm("确认信息", "您确定删除该用户？", function () {
            $.post(
                    opts.userDeleteUrl
                    , { ID: id }
                    , function (data) {
                        if (!data.Result) {
                            showError("出错啦！", data.Message);
                            return false;
                        }
                        YFPUserGrid.reloadGrid();
                    }

                );
            $(this).dialog("close");
        });
    };

    //已分配用户
    var YFPUserGrid = new MyGrid({
        renderTo: 'YFPUserGrid'
            , autoWidth: true
            , title: '已分配用户'
        //, height: gGridHeight
            , height: gGridWithTitleHeight - 255
		    , storeURL: opts.yfpuser
		    , sortByField: 'User.Department.DepartmentName'
            , primaryKey: 'ID'
		    , setGridPageSize: 150
		    , showPageBar: false
            , autoLoad: false
            , singleSelect: true
            , rowNumbers: false
		    , initArray: [
                  { label: 'ID', name: 'ID', index: 'ID', width: 100, hidden: true }
                , { label: '×', name: 'act', index: 'act', width: 20, sortable: false, align: "center", fixed: true }
                , { label: '消息编号', name: 'MsgID', index: 'MsgID', width: 100, hidden: true }
                , { label: '登陆用户名', name: 'UserID', index: 'UserID', width: 100 }
                , { label: '真实姓名', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: '部门', name: 'DepartmentName', jsonmap: 'User.Department.DepartmentName', index: 'User.Department.DepartmentName', width: 100 }
                , { label: '职务', name: 'UserType', index: 'UserType', width: 120, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'UserType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('UserType')} }

		    ]
            , functions: {
                handleReload: function (btn) {
                    YFPUserGrid.reloadGrid();
                }
                , handleRefresh: function (btn) {
                    YFPUserGrid.refreshGrid(filterCondition);
                }
                , handleDelete: function (btn) {
                    YFPUserGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }

            }
    });

    //已分配用户列表加载完后执行的操作
    var YFPUserGridRecords = new Array();
    var useridTostring = "''";
    YFPUserGrid.addListeners("gridComplete", function () {
        var ids = YFPUserGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            ce = "<div onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleDeleteUser(" + ids[i] + ");\" class='ui-pg-div ui-inline-del' style='float:left;margin-left:3px;height:15px;line-height:15px;color:red;' title='删除所选记录'><span>✘</span></div>";
            YFPUserGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: ce });
        }
        YFPUserGridRecords = YFPUserGrid.getAllRecords();

        var useridArray = new Array();
        $.each(YFPUserGridRecords, function (i, n) {
            useridArray.push("'" + n.UserID + "'");

        });
        if (useridArray.length > 0) {
            useridTostring = useridArray.toString();
        } else {
            useridTostring = "''";
        }
        //帅选待分配的用户
        if (!isEmpty($("#DepartmentID").val())) {
            DFPUserGrid.refreshGrid("DepartmentID='" + $("#DepartmentID").val() + "' and UserID NOT IN (" + useridTostring + ")");
        } else {
            DFPUserGrid.refreshGrid("UserID NOT IN (" + useridTostring + ")");
        }
    });

    //待分配用户
    var filterCondition2 = "UserType<>'01'";
    var DFPUserGrid = new MyGrid({
        renderTo: 'DFPUserGrid'
            , autoWidth: true
            , title: '待分配用户（<font color="#FF0000">小窍门：1、双击用户名即可分配；2、多选时请按住Ctrl键</font>）'
            , height: 200
		    , storeURL: opts.dfpuser
            , storeCondition: filterCondition2
		    , sortByField: 'Department.DepartmentName'
            , primaryKey: 'ID'
		    , setGridPageSize: 150
		    , showPageBar: false
            , singleSelect: false
            , multiboxonly: false
            , multikey: 'ctrlKey'
            , rowNumbers: false
		    , initArray: [
                { label: '登陆用户名', name: 'UserID', index: 'ID', jsonmap: 'ID', width: 100 }
                , { label: '真实姓名', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: '部门', name: 'DepartmentName', jsonmap: 'Department.DepartmentName', index: 'Department.DepartmentName', width: 100 }
                , { label: '职务', name: 'UserType', index: 'UserType', width: 150, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'UserType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('UserType')} }
		    ]
            , functions: {
                handleReload: function (btn) {
                    DFPUserGrid.reloadGrid();
                }
                , handleRefresh: function (btn) {
                    DFPUserGrid.refreshGrid(filterCondition);
                }
                , handleDelete: function (btn) {
                    DFPUserGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                
            }
        });
        DFPUserGrid.addListeners('gridComplete', function () {
                  $('#DFPUserGrid tbody :input').css("display","none");
            
        });

        DFPUserGrid.addListeners('rowdblclick', function (id, record, selBool) {
        if (!msgGrid.isSelectedOnlyOne()) {
            showMessage('提示', '请先选择一条要分配用户的消息！');
            return;
        }
        //待数据发送服务器返回成功时，从待分配列表删除，同时在已分配列表中增加
        var MsgSelectedRecord = msgGrid.getSelectedRecord();
        ajaxRequest(
            opts.assignUserUrl,
            {
                MsgID: MsgSelectedRecord.ID,
                UserID: id
            },
            true,
            function (response) {
                if (response.Result) {
                    //YFPUserGrid.refreshGrid();
                    YFPUserGrid.getJqGrid().addRowData(response.Data, record);
                    DFPUserGrid.getJqGrid().delRowData(id);
                }
            }
        );

    });
    $("div .DFPUserGrid-toolbar").remove();
}