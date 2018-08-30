function systemmsgIndexInit(opts) {
    var xheditorSettings = { skin: 'nostyle', tools: 'Blocktag,Fontface,Bold,Italic,Underline,Strikethrough,FontColor,|,Align,List,Outdent,Indent,|,Preview,Fullscreen', editorRoot: '/Content/' };
    function titleFormatter(cellvalue, options, rowObject) {
        return "<a href='#' msgdata-id='" + rowObject["ID"] + "'>" + cellvalue + "</a>";
    };
    function titleUnFormatter(cellvalue, options, cell) {
        return $('a', cell).html();
    }
    var buttonGroup = ["010307", "010309"];
    var mySystemMsgGrid = new MyGrid({
        renderTo: 'MySystemMsgGrid'
		    , title: '我的消息'
        //, width: '100%'
            , autoWidth: true
            , buttonRenderTo: 'buttonToolbar'
            , buttons: buttons0
            , height: gGridWithTitleHeight - 7
		    , storeURL: opts.storeUrl
		    , sortByField: 'IsRead,SystemMsg.SendTime'
            , sortOrder: 'ASC,DESC'
		    , primaryKey: 'ID'
            , dialogWidth: 650
            , dialogHeight: 500
            , groupField: 'SystemMsg.DuringTime'
		    , groupingView: { groupColumnShow: [false], groupDataSorted: [true], groupText: ['<b>{0}&nbsp;(<font color=red>{1}</font>封)</b>'], groupOrder: ['ASC'] }
            , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                  { label: '  ', name: 'IsRead', index: 'IsRead', sortable: false, width: 18, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { "0": "<span class='ui-icon-red ui-icon-mail-closed'></span>", "1": "<span class='ui-icon ui-icon-mail-open'></span>"} }
                , { label: '<span style="display: block; height: 19px; float: left; background: url(/Content/themes/default/images/fj2.gif) repeat-y scroll 0px 0px transparent; width: 17px;"></span>', name: 'SystemMsg.HasAttachments', index: 'SystemMsg.HasAttachments', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { "0": "", "1": "<span style='display: block; height: 19px; float: left; background: url(/Content/themes/default/images/fj2.gif) repeat-y scroll 0px 0px transparent; width: 17px;'></span>" }, width: 20, sortable: false }
                , { label: '发件人', name: 'SystemMsg.Sender', index: 'SystemMsg.Sender', width: 150 }
                , { label: '编号', name: 'ID', index: 'ID', width: 90, hidden: true }
                , { label: 'UserID', name: 'UserID', index: 'UserID', hidden: true }
                , { label: '标题', name: 'SystemMsg.MsgTitle', index: 'SystemMsg.MsgTitle', width: 650, formatter: titleFormatter, unformat: titleUnFormatter }
                , { label: '内容摘要', name: 'SystemMsg.OperateObj', index: 'SystemMsg.OperateObj', width: 200, hidden: true }
                , { label: 'Url', name: 'SystemMsg.Url', index: 'SystemMsg.Url', hidden: true }
                , { label: '父节点', name: 'SystemMsg.PID', index: 'SystemMsg.PID', hidden: true }
                , { label: '时间', name: 'SystemMsg.SendTime', index: 'SystemMsg.SendTime', formatter: 'datetime', width: 150 }
                , { label: '时间间隔', name: 'SystemMsg.DuringTime', index: 'SystemMsg.DuringTime', width: 150, formatter: duringTimeFmt }
                , { label: '消息类型', name: 'SystemMsg.MsgType', index: 'SystemMsg.MsgType', width: 90, align: 'center' }
                , { label: '附件2', name: 'SystemMsg.Attachments', index: 'SystemMsg.Attachments', formatter: attachmentFmt2, hidden: true }
                , { label: '处理状态', name: 'DealStatus', index: 'DealStatus', hidden: true }
                , { label: '受理人', name: 'SystemMsg.Assignee', index: 'SystemMsg.Assignee', hidden: true }
                , { label: '创建人', name: 'Builder', index: 'Builder', hidden: true }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'date', hidden: true }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', hidden: true }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'date', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    mySystemMsgGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    mySystemMsgGrid.refreshGrid();
                },
                // 发送消息
                handleAdd: function (btn) {

                    $("#userlistgrid").hide();
                    $("input#choseUserBtn").val("选择联系人");
                    mySystemMsgGrid.handleAdd({
                        title: '发消息',
                        loadFrom: '/SystemMsg.mvc/WriteMsg #WriteMsg ',
                        formId: 'WriteMsgForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            File.num = 1;
                            File.urls = {};
                            cleanLinkMan();
                            expandLinkMan();
                            //switchEditMode();
                            $("#WriteMsgForm").attr("action", "/Attachment.mvc/Upload");
                            //给ID赋值：当前时间的时间戳new Date().getTime()
                            $("#ID").val(new Date().getTime());
                            var editor2 = $('#OperateObj');
                            setTimeout(function () { editor2.xheditor(xheditorSettings); }, 300);

                        }
                        , buttons:
                        {
                            "关闭": function () {
                                if ($("#userlistgrid").dialog("isOpen")) {
                                    $("#userlistgrid").dialog("option", "hide", "fade");
                                    $("#userlistgrid").dialog("close");
                                    cleanLinkMan();
                                }
                                $(this).dialog("close");
                            }, "发送": function () {
                                //表单一般性验证
                                $.validator.unobtrusive.parse(document);
                                if (!$("#WriteMsgForm").valid()) {
                                    var valMsg = $('[data-valmsg-summary=true]', this);
                                    if (valMsg.length > 0) {
                                        valMsg.hide();
                                        showError('验证错误', valMsg.html());
                                    }
                                    return;
                                }
                                var postData = $("#WriteMsg form").serialize();
                                var _opt = { url: btn.data.Url, postData: postData };
                                //1、先传附件
                                //上传附件
                                if ($("form#WriteMsgForm > input[type=file]").length > 0) { //有附件需要上传
                                    uploadFile(_opt);
                                } else {
                                    handleSave(_opt);
                                }

                            }, "存草稿": function () {
                                var postData = $("#WriteMsg form").serialize();
                                postData = _combinePostData(postData, { DealStatus: 1 });
                                var _opt = { url: btn.data.Url, postData: postData };
                                if (isEmpty($("#MsgTitle").val())) {
                                    $("input[name='MsgTitle']").addClass("input-validation-error");
                                    showError("验证错误", "主题 字段是必须的");
                                    return false;
                                }
                                if ($("form#WriteMsgForm > input[type='file']").length > 0) { //有附件需要上传
                                    uploadFile(_opt);
                                    suitcaseMsg();
                                } else {
                                    handleSave(_opt);
                                    suitcaseMsg();
                                }
                            }
                        }
                    });
                },
                handleEdit: function (btn) {
                    mySystemMsgGrid.handleEdit({
                        loadFrom: 'My-FormDiv',
                        btn: btn
                    });
                }
                //删除
                , handleDelete: function (btn) {
                    mySystemMsgGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //彻底删除
                , handleThoroughDel: function (btn) {
                    mySystemMsgGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //删除草稿
                , handleDeleteSuitCase: function (btn) {
                    suitcaseGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //撤销已发送
                , handleRevocation: function (btn) {
                    if (!suitcaseGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var selectedRecord = suitcaseGrid.getSelectedRecord();
                    if (selectedRecord.DealStatus != 0) {
                        showMessage('提示', '该记录并非已发送消息！');
                        return;
                    }

                    showConfirm(
                        "特别提示",
                        "您确定要撤销吗？",
                        function () {//OK
                            ajaxRequest(
                                btn.data.Url,
                                { SystemMsgID: selectedRecord.ID },
                                false,
                                function (resp) {
                                    if (resp.Result) {
                                        showMessage('提示', '撤销成功！');
                                        suitcaseGrid.refreshGrid();
                                        return;
                                    } else {
                                        showMessage('提示', '撤销失败！请检查数据是否完整');
                                        return;
                                    }
                                }
                            );
                        },
                        function () {//CANCLE
                            //do something
                        }
                    );

                }
                //标记为已读
                , handleRead: function (btn) {

                    var keys = mySystemMsgGrid.getSelectedKeys();
                    if (keys.length > 0) {
                        var requestURL = btn.data.Url;
                        var postParams = { ids: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            mySystemMsgGrid.reloadGrid();
                        });
                    }
                    else {
                        showMessage('提示', '没有选择任何记录！');
                    }
                }
                //标记为未读
                , handleUnRead: function (btn) {

                    var keys = mySystemMsgGrid.getSelectedKeys();
                    if (keys.length > 0) {
                        var requestURL = btn.data.Url;
                        var postParams = { ids: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            mySystemMsgGrid.reloadGrid();
                        });
                    }
                    else {
                        showMessage('提示', '没有选择任何记录！');
                    }
                }
                //移至回收站
                , handleTrash: function (btn) {
                    var keys = mySystemMsgGrid.getSelectedKeys();
                    if (keys.length > 0) {
                        var requestURL = btn.data.Url;
                        var postParams = { ids: keys };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            mySystemMsgGrid.reloadGrid();
                        });
                    }
                    else {
                        showMessage('提示', '没有选择任何记录！');
                    }
                }
                //转发
                , handleForward: function (btn) {
                    ForwardOrReply("Forward", btn.data.Url, true);
                }
            }
    });
    mySystemMsgGrid.addListeners("gridComplete", function () {
        hideGroupButton(
            "buttonToolbar",
            buttonGroup
        );
    });

    var suitcaseGrid = new MyGrid({
        renderTo: 'suitcaseGrid'
		    , title: '我的草稿箱'
            , autoWidth: true
            , buttonRenderTo: 'buttonToolbar'
            , height: gGridWithTitleHeight - 7
		    , storeURL: opts.suitcaseUrl
		    , sortByField: 'SendTime'
            , sortOrder: 'DESC'
		    , primaryKey: 'ID'
            , dialogWidth: 650
            , dialogHeight: 500
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
		    , initArray: [
                { label: '<span style="display: block; height: 19px; float: left; background: url(/Content/themes/default/images/fj2.gif) repeat-y scroll 0px 0px transparent; width: 17px;"></span>', name: 'HasAttachments', index: 'HasAttachments', formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { "0": "", "1": "<span style='display: block; height: 19px; float: left; background: url(/Content/themes/default/images/fj2.gif) repeat-y scroll 0px 0px transparent; width: 17px;'></span>" }, width: 20, sortable: false }
                , { label: '发件人', name: 'Sender', index: 'Sender', width: 80, hidden: true }
                , { label: '编号', name: 'ID', index: 'ID', width: 90, hidden: true }
                , { label: '收件人', name: 'UserID', index: 'UserID', width: 280, formatter: recieverFmt, unformat: recieverUnFmt }
                , { label: '标题', name: 'MsgTitle', index: 'MsgTitle', width: 700 }
                , { label: '内容摘要', name: 'OperateObj', index: 'OperateObj', width: 250, hidden: true }
                , { label: 'Url', name: 'Url', index: 'Url', hidden: true }
                , { label: '父节点', name: 'PID', index: 'PID', hidden: true }
                , { label: '时间', name: 'SendTime', index: 'SendTime', formatter: 'datetime', width: 150 }
                , { label: '处理状态', name: 'DealStatus', index: 'DealStatus', hidden: true }
                , { label: '附件', name: 'Attachments', index: 'Attachments', formatter: attachmentFmt2, hidden: true }
                , { label: '创建人', name: 'Builder', index: 'Builder', hidden: true }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'date', hidden: true }
                , { label: '修改人', name: 'Modifier', index: 'Modifier', hidden: true }
                , { label: '修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'date', hidden: true }
		    ]
            , functions: {
                handleReload: function (btn) {
                    suitcaseGrid.reloadGrid();
                }

            }
    });
    suitcaseGrid.addListeners("gridComplete", function () {
        aexpandUserList();
        hideGroupButton(
            "buttonToolbar",
            buttonGroup
        );
    });
    //双击草稿箱中的记录则再次编辑
    suitcaseGrid.addListeners('rowdblclick', function (id, record, selBool) {

        var newSystemMsgID;
        var newTitle = " ";
        var bindAttach = false;
        var dealStatus = record.DealStatus;
        if (dealStatus == 1) {//如果是草稿则消息编号不变，发送时不再新增消息主体与已存在的附件（新附件除外），只将消息分发至用户
            newSystemMsgID = record.ID;
            newTitle = "编辑草稿";
            bindAttach = true;
        } else if (dealStatus == 0) {//如果是已发送，则作为新的消息进行发送。新消息编号重新生成
            newSystemMsgID = new Date().getTime();
            newTitle = "编辑已发送";
            bindAttach = false; //若编辑已发送则不再绑定已存在附件，需重新上传附件
        } else {
            showMessage("警告！", "非法操作！");
            return false;
        }
        suitcaseGrid.handleEdit({
            title: newTitle,
            loadFrom: '/SystemMsg.mvc/WriteMsg #WriteMsg ',
            formId: 'WriteMsgForm',
            afterFormLoaded: function () {
                File.urls = {};
                cleanLinkMan();
                expandLinkMan();
                //switchEditMode();
                $("#WriteMsgForm").attr("action", "/Attachment.mvc/Upload");
                $("#ID").val(newSystemMsgID);
                var _userid = record.UserID;
                if (record.UserID != 'null')
                    $("#repUserID").html(_userid);
                $.each(_userid.split(","), function (i, n) {
                    linkMan.getJqGrid().jqGrid('setSelection', n);
                });
                var editor2 = $('#OperateObj');
                setTimeout(function () { editor2.xheditor(xheditorSettings); }, 300);

                //附件展现
                if (bindAttach) {
                    var _attachments = record["Attachments"];
                    $("#filelist").append(_attachments);
                    var isExistAttachments = $("#filelist > span");
                    if (isExistAttachments.length > 0) {
                        $("#filelisttr").fadeIn("fast", function () {
                            File.count = isExistAttachments.length;
                            File.num = isExistAttachments.length;
                        });
                    }
                }

            },
            buttons: {
                "关闭": function () {
                    if ($("#userlistgrid").dialog("isOpen")) {
                        $("#userlistgrid").dialog("option", "hide", "fade");
                        $("#userlistgrid").dialog("close");
                        cleanLinkMan();
                    }
                    $(this).dialog("close");
                }, "发送": function () {
                    //表单一般性验证
                    $.validator.unobtrusive.parse(document);
                    if (!$("#WriteMsgForm").valid()) {
                        var valMsg = $('[data-valmsg-summary=true]', this);
                        if (valMsg.length > 0) {
                            valMsg.hide();
                            showError('验证错误', valMsg.html());
                        }
                        return;
                    }
                    var postData = $("#WriteMsg form").serialize();
                    postData = _combinePostData(postData, { DealStatus: dealStatus });
                    var _opt = { url: opts.sendsuitcaseUrl, postData: postData };
                    //1、先传附件
                    //上传附件
                    if ($("form#WriteMsgForm > input[type=file]").length > 0) { //有附件需要上传
                        uploadFile(_opt);
                    } else {
                        handleSave(_opt);
                    }
                    setTimeout(function () { suitcaseGrid.refreshGrid(); }, 300);

                }
            }
        });
    });
    var linkMan = new MyGrid({
        renderTo: 'userlistgrid'
            , width: '130'
            , height: 350
		    , storeURL: opts.linkManUrl
		    , sortByField: 'Department.DepartmentName'
            , groupField: 'DepartmentName'
            , groupingView: { groupColumnShow: [false], groupText: ['{0}(<font color=red>{1}</font>)'], groupCollapse: true, groupOrder: ['desc'] }
		    , primaryKey: 'ID'
		    , setGridPageSize: 1000
		    , showPageBar: false
            , singleSelect: false
            , multiboxonly: false
            , toolbarSearch: false
		    , initArray: [
                { label: '登陆用户名', name: 'ID', index: 'ID', width: 10, hidden: true }
                , { label: '联系人列表', name: 'TrueName', index: 'TrueName', width: 80, sortable: false }
                , { label: '部门', name: 'DepartmentName', jsonmap: 'Department.DepartmentName', index: 'Department.DepartmentName', width: 20 }
		    ]
            , functions: {
                handleReload: function (btn) {
                    linkMan.reloadGrid();
                },
                handleRefresh: function (btn) {
                    linkMan.refreshGrid(filterCondition);
                }

            }
    });
    //点击事件。
    linkMan.addListeners('rowclick', function (id, record, selBool) {

        $("form textarea[name='UserID']").val(linkMan.getSelectedKeys().toString());
        $("#repUserID").html(linkMan.getSelectedKeys().toString());
    });
    linkMan.addListeners("gridComplete", function () {
        $("td[dir='ltr'],[class='ui-pg-button ui-state-disabled']", "#userlistgrid-pager_center").remove();
        $("#userlistgrid-pager_right").remove();
        $("#userlistgrid").find('div.ui-jqgrid-hdiv').andSelf().hide();
        $("#userlistgrid div.grid-toolbar").remove();
        $("#userlistgrid table").css("border-collapse", "inherit");
        //去除grid的圆角
        $("div #gbox_userlistgrid-datagrid").removeClass("ui-corner-all");
    });
    $("#userlistgrid").dialog({
        title: "选择联系人",
        autoOpen: false,
        width: 50,
        hide: 'slide',
        show: 'slide',
        open: function () {
            $(".ui-dialog-titlebar-close").fadeOut();
        },
        close: function () {
            $(".ui-dialog-titlebar-close").fadeIn();
        }
    });
    window.handleSave = function (opt) {
        ajaxRequest(opt.url, opt.postData, false, function (resp) {
            if (resp.Result) {
                showMessage("提示", "消息发送成功");
                //如果联系人列表是显示状态，则在发送完消息后关闭联系人列表
                if ($("#userlistgrid").dialog("isOpen")) {
                    $("#userlistgrid").dialog("option", "hide", "fade");
                    $("#userlistgrid").dialog("close");
                }
                $("#xloading").dialog("close");
                $("#WriteMsg").parent("#MyGrid-Dialog").dialog("close");
                $("#WriteMsgForm")[0].reset();
                cleanLinkMan();
                mySystemMsgGrid.refreshGrid();
            } else {
                $("#xloading").dialog("option", "title", "操作异常");
                $("#xloading").html(resp.Message);
                $("#xloading").dialog("open");
                return false;
            }
        });
    };

    //转发
    window.ForwardOrReply = function (OperateType, Url, isAddAttach, msgdata) {
        var record = null;
        if (isEmpty(msgdata)) {
            if (!mySystemMsgGrid.isSelectedOnlyOne()) {
                showMessage('提示', '请选择一条记录进行操作！');
                return;
            }
            var msgrd = mySystemMsgGrid.getSelectedRecord();
            record = reJsonByPrefix(msgrd, "SystemMsg");
        } else {
            record = msgdata;
        }

        if (OperateType == "Forward") {
            title = "转发";
        } else if (OperateType == "Reply") {
            title = "回复";
        } else {
            showError("错误提示", "非法操作！");
            return false;
        }
        mySystemMsgGrid.showWindow({
            title: title,
            loadFrom: '/SystemMsg.mvc/WriteMsg #WriteMsg ',
            jsonprefix: 'SystemMsg',
            formId: 'WriteMsgForm',
            afterLoaded: function () {
                File.num = 1;
                File.urls = {};
                cleanLinkMan();
                expandLinkMan();
                //switchEditMode();
                $("#WriteMsgForm").attr("action", "/Attachment.mvc/Upload");
                $("#ID").val(new Date().getTime());
                $("#MsgTitle").val(title + "：" + $("#MsgTitle").val());
                $("#OperateObj").val("");
                var _userid = record["Sender"];
                if (OperateType == "Reply") {
                    if (_userid != 'null')
                        $("#repUserID").html(_userid);
                    $.each(_userid.split(","), function (i, n) {
                        linkMan.getJqGrid().jqGrid('setSelection', n);
                    });
                }
                var editor2 = $('#OperateObj');
                setTimeout(function () {
                    editor2.xheditor(xheditorSettings);
                    editor2.xheditor().appendHTML("<p>&nbsp;</p><p>&nbsp;</p><p>--------------------原始邮件--------------------</p><p style='margin-top:1px;margin-bottom:1px;'><b>发件人：</b>" + "\"" + record['Sender'] + "\"" + "</p><p style='margin-top:1px;margin-bottom:1px;'><b>发送时间：</b>" + EDateFormat(record['SendTime'], 0) + "</p><p style='margin-top:1px;margin-bottom:1px;'><b>收件人：</b>" + "\"" + record['UserID'] + "\"" + "</p><p style='margin-top:1px;margin-bottom:1px;'><b>主题：</b>" + record['MsgTitle'] + "</p><p style='margin-top:5px;padding-left:20px;'>" + record['OperateObj'] + "</p>");
                }, 300);

                //附件展现
                if (isAddAttach) {
                    var _attachments = record["Attachments"];
                    $("#filelist").append(_attachments);
                    var isExistAttachments = $("#filelist > span");
                    if (isExistAttachments.length > 0) {
                        $("#filelisttr").fadeIn("fast", function () {
                            File.count = isExistAttachments.length;
                            File.num = isExistAttachments.length;
                        });
                    }
                    $('span[att-id]').hide();
                }

            },
            buttons: {
                "关闭": function () {
                    if ($("#userlistgrid").dialog("isOpen")) {
                        $("#userlistgrid").dialog("option", "hide", "fade");
                        $("#userlistgrid").dialog("close");
                        cleanLinkMan();
                    }
                    $(this).dialog("close");
                }, "发送": function () {
                    //表单一般性验证
                    $.validator.unobtrusive.parse(document);
                    if (!$("#WriteMsgForm").valid()) {
                        var valMsg = $('[data-valmsg-summary=true]', this);
                        if (valMsg.length > 0) {
                            valMsg.hide();
                            showError('验证错误', valMsg.html());
                        }
                        return;
                    }
                    var postData = $("#WriteMsg form").serialize();
                    var existAttachIds = new Array();
                    $.each($("#filelist a[att-id]"), function (i, n) { existAttachIds.push($(n).attr("att-id")) });
                    postData = _combinePostData(postData, { existAttachIds: existAttachIds });
                    var _opt = { url: Url, postData: postData };
                    //1、先传附件
                    //上传附件
                    if ($("form#WriteMsgForm > input[type=file]").length > 0) { //有附件需要上传
                        uploadFile(_opt);
                    } else {
                        handleSave(_opt);
                    }
                    //setTimeout(function () { mySystemMsgGrid.refreshGrid(); }, 300);

                }
            }
        });
    };

    //上传附件
    window.uploadFile = function (_opt) {
        var curYear = new Date().getFullYear().toString();
        var curMonth = (new Date().getMonth() + 1).toString();
        curMonth = curMonth.length == 1 ? "0" + curMonth : curMonth;
        var options = {
            type: 'POST',
            dataType: 'json',
            data: { objectType: "SysMsg", objectId: $("#ID").val(), uploadPath: curYear.concat(curMonth) },
            target: '',
            url: '/Attachment.mvc/Upload',
            beforeSubmit: function (arr, $form, options) {
                //弹出上传附件提示
                $("#xloading").dialog("option", "title", "上传附件");
                $("#xloading").html("正在上传附件，请稍候……");
                $("#xloading").dialog("open");
            },
            error: function (xht) {
                //如果失败了提示重传
                $("#xloading").dialog("option", "title", "上传异常");
                $("#xloading").html("文件总大小＞20MB，超出了限制范围");
                return false;
            },
            //resetForm: true, //上传成功后清空表单
            success: function (resp) {
                if (resp.Result == "false" || resp.Result == false) {
                    $("#xloading").dialog("option", "title", "上传异常");
                    $("#xloading").html(resp.Message);
                    return false;
                }
                //上传成功后先关闭提示窗口
                $("#xloading").dialog("close");
                showMessage("提示", "附件上传成功");
                //2、再保存消息
                handleSave(_opt);
            }
        };
        $("#WriteMsgForm").unbind("submit");
        $("#WriteMsgForm").submit(function () {
            $(this).ajaxSubmit(options);
            return false;
        });
        $("#WriteMsgForm").submit();
    };

    //已读
    window.ReadedMsg = function () {
        mySystemMsgGrid.getJqGrid().setCaption("我的消息 - 已读");
        buttonGroup = ["010309", "010307"];
        $("#myfilesFolderParent").fadeOut("fast", function () { $("#suitcaseGrid").fadeOut("fast", function () { $("#MySystemMsgGrid").fadeIn() }); });
        mySystemMsgGrid.refreshGrid("IsRead = 1 AND DealStatus = 0");

    };

    //未读
    window.UnReadMsg = function () {
        mySystemMsgGrid.getJqGrid().setCaption("我的消息 - 未读");
        buttonGroup = ["010309", "010307"];
        $("#myfilesFolderParent").fadeOut("fast", function () { $("#suitcaseGrid").fadeOut("fast", function () { $("#MySystemMsgGrid").fadeIn() }); });
        mySystemMsgGrid.refreshGrid("IsRead = 0 AND DealStatus = 0");

    };

    //已发送
    window.SendedMsg = function () {
        buttonGroup = [
                "010302", //标记为已读
                "010303", //标记为未读
                "010306", //彻底删除
                "010308", //转发
                "010309", //删除草稿
                "010304" //删除
            ];
        suitcaseGrid.setTitle("我的已发送");
        suitcaseGrid.refreshGrid("DealStatus = 0");
        suitcaseGrid.getJqGrid().setGridWidth(mySystemMsgGrid.getJqGrid().getGridParam("width"), false);
        mySystemMsgGrid.getJqGrid().resetSelection();
        $("#myfilesFolderParent").fadeOut("fast", function () { $("#MySystemMsgGrid").fadeIn(); $("#suitcaseGrid").fadeIn("fast"); });
    };

    //草稿箱
    window.suitcaseMsg = function () {
//        //创建自定义按钮【删除草稿】
//        if ($("button[value='suitcase_del']").length > 0) {
//            $("button[value='suitcase_del']").show();
//        } else {
//            $("<button/>", { text: "删除草稿", value: 'suitcase_del' })
//            .button({ icons: { primary: "ui-icon-trash"} })
//            .click(handleDeleteSuitCase)
//            .appendTo($("#buttonToolbar"));
//        }
//        buttonGroup = [
//                "010302",
//                "010303",
//                "010304",
//                "010307", //撤销已发送
//                "010308", //转发
//                "010306" //彻底删除
//            ];
        buttonGroup = [
                "010302", //标记为已读
                "010303", //标记为未读
                "010306", //彻底删除
                "010307",
                "010308", //转发
                "010304" //删除
            ];
        suitcaseGrid.setTitle("我的草稿箱");
        suitcaseGrid.refreshGrid("DealStatus = 1");
        suitcaseGrid.getJqGrid().setGridWidth(mySystemMsgGrid.getJqGrid().getGridParam("width"), false);
        mySystemMsgGrid.getJqGrid().resetSelection();
        $("#myfilesFolderParent").fadeOut("fast", function () { $("#MySystemMsgGrid").fadeIn(); $("#suitcaseGrid").fadeIn("fast"); });

    };

    //已删除
    window.trashMsg = function () {
        mySystemMsgGrid.getJqGrid().setCaption("我的消息 - 已删除");
        buttonGroup = [//要隐藏的一组功能按钮ID
                "010309", //删除草稿
                "010307", //撤销已发送
                "010308", //转发
                "010304"//删除
            ];
        $("#myfilesFolderParent").fadeOut("fast", function () { $("#suitcaseGrid").fadeOut("fast", function () { $("#MySystemMsgGrid").fadeIn() }) });
        mySystemMsgGrid.refreshGrid("DealStatus = -1");
    };

    //清空回收站
    window.handleCleartrash = function () {
        //确认操作提示
        showConfirm("警告提示", "清空已删除的消息将无法找回，您确定要<font color=green><b>清空</b></font>吗？", function () {
            ajaxRequest(
                opts.cleartrashUrl,
                {

            },
                true,
                function () {
                    mySystemMsgGrid.refreshGrid();
                }
            );
            $(this).dialog("close");
        });
    };

    //我的附件夹
    window.myFiles = function () {
        buttonGroup = [
                "010302",
                "010303",
                "010304",
                "010307", //撤销已发送
                "010308", //转发
                "010306" //彻底删除
            ];
        hideGroupButton(
            "buttonToolbar",
            buttonGroup
        );
        $("#MySystemMsgGrid").fadeOut("fast", function () {
            $("#myfilesFolder,#myfilesFolderC").height($("#MySystemMsgGrid").height() - 7); //包括标题、内容、底部工具按钮的高度
            $("#myfilescontent").height($("#MySystemMsgGrid").height() - 10 - 50); //展示文件内容的高度
            $("#myfilesFolderParent").fadeIn("fast");
            $("#filesinfo").hide();
        });
        //读取附件（收件箱中的附件）
        $.ajax({
            url: "/Attachment.mvc/GetMyFiles",
            data: {},
            type: "POST",
            beforeSend: function (xhr) {
                $("#load_myfilescontent-datagrid").show();
            },
            success: function (resp) {
                $("#load_myfilescontent-datagrid").hide();
                //$("#myfilescontent>div:not(:first)").remove();
                if (resp && resp.rows) {
                    var files = resp.rows;
                    $.each(files, function (i, n) {
                        var fileEl = $("<div style='width:95px;height:129px;float:left;margin:10px 10px 0 10px;text-align:center;background:transparent; position:relative; border:1px dotted #fff;' mymsgid='" + n.MyMsgID + "' attachid='" + n.AttachID + "' filesize='" + n.FileSize + "'><div><img src='/Content/erpimage/filetype/" + n.FileType.substr(1) + ".png' width='95px' height='95px' alt='无图片' title='" + n.Title + "' /></div><div style='width:95px; position:absolute;' title='" + n.Title + "'>" + n.Title.substring(0, 15) + "</div><div class='oper' style='height:34px; width:100%; float:left; position:relative;z-index:1; display:none;'></div></div>");
                        fileEl.hover(
                            function () {
                                $(this).addClass("filehoverd");
                                $(this).children(".oper").show();

                            }, function () {
                                $(this).removeClass("filehoverd");
                                if ($(this).children("img").length == 0) {
                                    $(this).children(".oper").hide();
                                }

                            }
                        );
                        fileEl.toggle(
                            function () {
                                $(this).addClass("fileselected");
                                $(this).append($("<img src='/Content/erpimage/filetype/tick.gif' style='position:absolute;left:40px;bottom:10px;z-index:99999;' />"));
                                checkSelectedFileCounts();
                            }, function () {
                                $(this).removeClass("fileselected");
                                $(this).children("img").remove();
                                checkSelectedFileCounts();
                            }
                        );
                        var downloadbtn = $("<div>", {
                            "class": "downloadbtn",
                            html: fileEl

                        });
                        downloadbtn.hover(
                            function () {
                                $(this).children("button").show();
                                $(this).css("border", "1px dashed #C6C6C6")
                            }, function () {
                                $(this).children("button").hide();
                                $(this).css("border", "1px dashed #FFFFFF")
                            }
                        );
                        downloadbtn.append($("<button style='float:left; text-align:center; display:none;margin:0px 10px 0px 10px; color:red;letter-spacing:3px;z-index:9999;opacity:0.7;filter:alpha(opacity=70);' onclick=tdownload('" + n.FileUrl + "')>下载</button>")).appendTo($("#myfilescontent"));
                    })
                };

                //创建右键菜单
                //自定义右键上下文
                var objDownload = {
                    text: "下载",
                    func: function () {
                        var _attachid = $(this).children().first().attr("attachid").toString();
                        ajaxDownload(_attachid.split(","));
                    }
                }, objTurnTo = {
                    text: "转至原始邮件",
                    func: function () {
                        //do
                    }
                };
                var attachMenuData = [
	                [objDownload, objTurnTo]
                ];
                $(".downloadbtn").smartMenu(attachMenuData, {
                    name: "mail"
                })
            }
        });

        //设置底部工具栏
        $("#batchDownload").button({
            icons: {
                primary: "ui-icon-circle-arrow-s"
            },
            disabled: true
        });
        $("#batchDownload").removeClass("ui-corner-all").addClass("ui-corner-top");
    };
    //模拟提交
    window.simulationSubmit = function (action, inputname, val) {
        var simform = $("<form>");
        simform.attr("style", "display:none");
        simform.attr("target", "");
        simform.attr("method", "post");
        simform.attr("action", action);

        var inputEl = $("<input>");
        inputEl.attr("type", "hidden");
        inputEl.attr("name", inputname);
        inputEl.attr("value", val);

        $("body").append(simform);
        simform.append(inputEl);
        simform.submit();
        simform.remove();
    };
    //单个下载
    window.tdownload = function (fileurl) {
        alert(fileurl);
        return false;
    };
    //批量打包下载
    $("#batchDownload").unbind("click");
    $("#batchDownload").bind("click", function () {
        batchDownload();
    });
    window.batchDownload = function () {
        //alert("您选择了" + fetchSelectedFiles().length + "个文件");
        var _files = fetchSelectedFiles();
        if (_files.length == 0) {
            showMessage("提示", "您没有选择任何附件");
            return false;
        }
        var _attachmentids = new Array();
        $.each(_files, function (i, n) {
            _attachmentids.push($(n).children().first().attr("attachid").toString());
        });
        //window.open("/Attachment.mvc/BatchDownloadFiles/files");
        ajaxDownload(_attachmentids);
    };

    window.ajaxDownload = function (ids) {//ids为数组
        $.post(
            "/Attachment.mvc/CheckDownloadFiles",
            { attachmentIds: JSON.stringify(ids) },
            function (res) {
                if (!res.Result) {
                    showMessage("提示", res.Message);
                    return false;
                } else {
                    simulationSubmit("/Attachment.mvc/BatchDownloadFiles", "attachIds", JSON.stringify(res.Data));
                }
            }
        );
    };
    //获取选择的文件
    window.fetchSelectedFiles = function () {
        var selectedFiles = $(".downloadbtn").filter(function (index) { return $(this).children().first().hasClass("fileselected") });
        return selectedFiles;
    };
    //根据检测到选择文件的个数，判断是否禁用批量下载按钮
    window.checkSelectedFileCounts = function () {
        var selectedFiles = fetchSelectedFiles();
        //计算文件总大小
        var fileTotalsize = 0;
        var fileTotalsizeWithUnit;
        var sizeUnit = "KB";
        $.each(selectedFiles, function (i, n) {
            fileTotalsize += parseFloat($(n).children().first().attr("filesize"));
        });
        sizeUnit = (fileTotalsize / 1024) <= 1024 ? "KB" : "MB";
        fileTotalsizeWithUnit = formatFloat(((fileTotalsize / 1024) < 1024 ? (fileTotalsize / 1024) : (fileTotalsize / 1024 / 1024)), 2) + " " + sizeUnit;
        if (selectedFiles.length > 0) {
            $("#batchDownload").button("option", "disabled", false);
            $("#filesinfo").show()
            .children("#filecounts").html(selectedFiles.length)
            .next("#allfilesize").html(fileTotalsizeWithUnit)
        } else {
            $("#batchDownload").button("option", "disabled", true);
            $("#filesinfo").hide()
            .children("#filecounts").html(selectedFiles.length)
            .next("#allfilesize").html(fileTotalsizeWithUnit)
        }
    };
    //添加联系人
    function expandLinkMan() {
        $("input#choseUserBtn").unbind("click");
        $("input#choseUserBtn").toggle(
                function () {
                    $(this).blur();
                    $(this).val("隐藏联系人").css("color", "#CCCCCC");
                    $(this).attr("disabled", true);
                    setTimeout(function () { $("input#choseUserBtn").attr("disabled", false).css("color", "#222222") }, 500);
                    var myGridDialogPos = $("#MyGrid-Dialog").parent().offset();
                    $("#userlistgrid").dialog("option", "height", $("#MyGrid-Dialog").parent().height());
                    $("#userlistgrid").dialog("option", "position", [myGridDialogPos.left + $("#MyGrid-Dialog").parent().width() + 13, myGridDialogPos.top]);
                    $("#userlistgrid").dialog("option", "show", "slide");
                    $("#userlistgrid").dialog("open");
                    return false;


                },
                function () {
                    $(this).blur();
                    $(this).val("选择联系人").css("color", "#CCCCCC");
                    $(this).attr("disabled", true);
                    setTimeout(function () { $("input#choseUserBtn").attr("disabled", false).css("color", "#222222") }, 500);
                    $("#userlistgrid").dialog("option", "hide", "slide");
                    $("#userlistgrid").dialog("close");
                    return false;
                });
    };

    //清空联系人
    window.cleanLinkMan = function () {
        $("form textarea[name='UserID']").val("");
        $("#repUserID").html("");
        linkMan.getJqGrid().resetSelection();
    };

    //全选联系人
    window.selectAll = function () {
        var allids = linkMan.getJqGrid().getDataIDs();
        $.each(allids, function (i, n) {
            linkMan.getJqGrid().jqGrid('setSelection', n);
        });
    };

    //切换编辑模式
    function switchEditMode() {
        $("a#switchEidtMode").unbind("click");
        $("a#switchEidtMode").toggle(
        function () {
            $(this).blur();
            $(this).text("+切换至编辑器模式+").css("color", "#F00");
            $('#OperateObj').width(498);
            $('#OperateObj').xheditor(false);
        },
        function () {
            $(this).blur();
            $(this).text("+切换至纯文本模式+").css("color", "#222222");
            $('#OperateObj').width(500);
            $('#OperateObj').xheditor(xheditorSettings);
        });
    };

    //触发创建file控件
    window.addFileType = function () {
        $("input[current=true]").trigger("click");
    };

    //阻止浏览器默认提交，防止页面刷新
    $("form").bind("submit", function (event) {
        event.preventDefault();
        //return fasle;//也可如此
    });

    //展开收缩发件人列表中联系人
    function aexpandUserList() {
        var aexpand = $("a").filter(function (index) {
            return isEmpty($(this).attr("id")) ? false : $(this).attr("id").indexOf("expandUserList") != -1;
        });
        aexpand.unbind("click");
        aexpand.toggle(
        function () {
            $(this).text("收缩"); //<a>标签的文字
            var asiblings = $(this).siblings("span");
            asiblings.html(asiblings.attr("rel")).parent("td").attr("title", asiblings.attr("rel") + "[" + $(this).text() + "]");
        },
        function () {
            $(this).text("展开");
            var asiblings = $(this).siblings("span");
            asiblings.html(asiblings.attr("rel").substr(0, 25).concat("...")).parent("td").attr("title", asiblings.attr("rel").substr(0, 25).concat("...") + "[" + $(this).text() + "]");

        }
    );
    };

    $('#msgDialogDetail').dialog({
        modal: true,
        autoOpen: false,
        bgiframe: true,
        resizable: true,
        width: 800,
        height: 600,
        title: '浏览邮件内容',
        buttons:
        {
            "关闭": function () { $(this).dialog("close"); },
            "转发": function () {
                var _msgCacheData = $("#msgDataCache").data("msgcache");
                var closeCallback = function (cacheData) { ForwardOrReply("Forward", opts.forwardUrl, true, cacheData); };
                $(this).dialog("close");
                closeCallback(_msgCacheData);
            },
            "回复": function () {
                var _msgCacheData = $("#msgDataCache").data("msgcache");
                var closeCallback = function (cacheData) { ForwardOrReply("Reply", opts.forwardUrl, false, cacheData); };
                $(this).dialog("close");
                closeCallback(_msgCacheData);
            },
            "带附件回复": function () {
                var _msgCacheData = $("#msgDataCache").data("msgcache");
                var closeCallback = function (cacheData) { ForwardOrReply("Reply", opts.forwardUrl, true, cacheData); };
                $(this).dialog("close");
                closeCallback(_msgCacheData);
            }

        },
        close: function () {
            $("#msgDataCache").removeData("msgcache");
        }
    });
    var data = {};
    $('a[msgdata-id]').live('click', function (e) {
        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;

        var dialogPosition = $(this).offset();
        var dialog = $('#msgDialogDetail');
        dialog.dialog('option', 'position', [dialogPosition.center, dialogPosition.center]);
        dialog.dialog('open');

        data = mySystemMsgGrid.getRecordByKeyValue($(this).attr('msgdata-id'));
        mySystemMsgGrid.getJqGrid().resetSelection();
        mySystemMsgGrid.getJqGrid().setSelection($(this).attr('msgdata-id'));
        dialog.empty();
        data = reJsonByPrefix(data, "SystemMsg");
        $("#msgDataCache").removeData("msgcache");
        $("#msgDataCache").data("msgcache", data); //缓存数据方便使用
        $('#tmplSystemMsg').tmpl(data).appendTo('#msgDialogDetail');
        if (dialog.dialog('isOpen') && (data.IsRead == "false" || data.IsRead == false)) { //当对话框已打开并且为未读时
            var requestURL = opts.toreadedUrl;
            var postParams = { ids: data.ID };
            ajaxRequest(requestURL, postParams, false, function (response) {
                mySystemMsgGrid.reloadGrid();
            });
        }
        $("span[attrsendtime]").text(EDateFormat($("span[attrsendtime]").text()));
        $.each($('#attchdiv a[att-id]'), function (i, n) {
            var downloadpath = $(n).attr("href");
            $(n).removeAttr("href");
            $("<a>", {
                href: 'javascript:void(0);',
                text: '[下载]',
                style: 'margin-left:5px;',
                click: function () {
                    window.open(downloadpath, "下载");
                }
            }).appendTo($(n).parent()[0]);
        });
        $('span[att-id]').hide();
        $("#jjxx").unbind("click");
        
        $("#jjxx").toggle(
            function () {
                $(this).text("详细信息");
                $("#mailinfo").slideUp();
                $(this).siblings("span").toggleClass('ui-icon-arrowthickstop-1-s');
            },
            function () {
                $(this).text("精简信息");
                $("#mailinfo").slideDown();
                $(this).siblings("span").toggleClass('ui-icon-arrowthickstop-1-s');
            }
        );
    });

}