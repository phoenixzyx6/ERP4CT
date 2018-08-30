//操作员权限设置
function userIndexInit(opts) {
    $('#sysFuncs').height(gGridHeight + 80);
    function copyUserPower(userId, copyFrom) {
        if (isEmpty(copyFrom) || isEmpty(userId))
            return;
        ajaxRequest(opts.copyPowerUrl,
        { userId: userId, copyFrom: copyFrom },
        false,
        function (response) {
            if (response.Result) {
                //清空当前用户缓存
                delete (userFuncCache[userId]);
                showMessage('权限复制成功');
            }
            else {
                showError('权限复制失败');
            }
        });
    }
    var filterCondition = "UserType<>'01'";
    var currentUser = opts.currentUser;
    if (currentUser != "admin") {
        filterCondition += " and ID<>'admin'";
    }
    var myJqGrid = new MyGrid({
        renderTo: 'My-JqGrid'
            , title: "用户列表"
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: filterCondition
		    , sortByField: 'Department.DepartmentName'
            , groupField: 'DepartmentName'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupCollapse: false, groupOrder: ['desc'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , primaryKey: 'ID'
		    , dialogWidth: 780
            , dialogHeight: 450
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
		    , initArray: [
                { label: '登陆用户名', name: 'ID', index: 'ID', width: 100 }
                , { label: '真实姓名', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: '部门', name: 'DepartmentName', jsonmap: 'Department.DepartmentName', index: 'Department.DepartmentName', width: 100 }
                , { label: '职务', name: 'UserType', index: 'UserType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'UserType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('UserType')} }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '允许外网访问', name: 'IsVisited', index: 'IsVisited', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: 'Email', name: 'Email', index: 'Email' }
                , { label: '联系电话', name: 'Tel', index: 'Tel' }
                , { label: '手机', name: 'Mobile', index: 'Mobile' }
                , { label: '所属公司', name: 'CompanyID', index: 'CompanyID', width: 50 }
                , { label: '直接上级', name: 'ManagerID', index: 'ManagerID', width: 50 }
                , { label: '到职日期', name: 'ValidFrom', index: 'ValidFrom', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '离职日期', name: 'ValidTo', index: 'ValidTo', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 50, search: false }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', search: false }
                , { label: '用户编号', name: 'UserCode', index: 'UserCode' }
		    ]
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid(filterCondition);
                },
                handleAdd: function (btn) {
                    $('#Password').removeClass('ignore');
                    $('#Password').addClass('requiredval');

                    myJqGrid.handleAdd({
                        title: "新增用户[初始密码为：<font color='red'>123456</font>]",
                        btn: btn,
                        loadFrom: 'My-FormDiv',
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', false);
                            $('#Password').attr('disabled', false);
                        },
                        postCallBack: function (response) {
                            var userId = myJqGrid.getFormFieldValue('ID');
                            var copyFrom = myJqGrid.getFormFieldValue('CopyPower');
                            if (response.Result) {
                                copyUserPower(userId, copyFrom);
                            }
                        }
                    });
                },
                handleEdit: function (btn) {
                    $('#Password').addClass('ignore');
                    $('#Password').removeClass('requiredval');
                    myJqGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'My-FormDiv',
                        getUrl: opts.getUserInfoUrl,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', true);
                            $('#Password').attr('disabled', false);
                        },
                        beforeSubmit: function () {
                            var pwd = myJqGrid.getFormFieldValue('Password');
                            if (isEmpty(pwd)) {
                                //未填写密码则不提交密码，防止更新密码为空
                                $('#Password').attr('disabled', true);
                            }
                            else {
                                //因为前面忽略了Password字段的验证，手动验证密码长度
                                var pwdField = myJqGrid.getFormField('Password');
                                if (pwdField) {
                                    var minLen = parseInt(pwdField.attr('data-val-length-min'));
                                    var maxLen = parseInt(pwdField.attr('data-val-length-max'));

                                    if (pwd.length < minLen || pwd.length > maxLen) {
                                        showError('验证错误', '密码长度只能是' + minLen + '到' + maxLen + '之间的字符');
                                        return false;
                                    }
                                }
                            }
                            return true;
                        },
                        postCallBack: function (response) {
                            var userId = myJqGrid.getFormFieldValue('ID');
                            var copyFrom = myJqGrid.getFormFieldValue('CopyPower');
                            if (response.Result) { copyUserPower(userId, copyFrom); }
                        }
                    });
                }
                , handleDelete: function (btn) {
                    if (opts.currentUser == myJqGrid.getSelectedKey()) {
                        showMessage('提示', '不能删除自己！');
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleSavePower: function (btn, e) {
                    //保存权限  
                    var userId = myJqGrid.getSelectedKey();
                    var treeObj = $.fn.zTree.getZTreeObj('sysFuncs');
                    var nodes = treeObj.getCheckedNodes();
                    if (userId) {
                        var ids = [];
                        for (var i = 0; i < nodes.length; i++) {
                            ids.push(nodes[i].id);
                        }
                        ajaxRequest(
                            btn.data.Url,
                            { userIds: userId, powers: ids },
                            true,
                            function () {
                                //清空当前用户缓存
                                delete (userFuncCache[userId]);
                            }
                        );
                    }
                    else {
                        //未选择用户
                    }
                },
//                handleSendMsg: function (btn) {
//                    var record = myJqGrid.getSelectedRecord();
//                    myJqGrid.handleEdit({
//                        btn: btn,
//                        loadFrom: 'Msg-Div',
//                        width: 300,
//                        height: 200,
//                        afterFormLoaded: function () {
//                            myJqGrid.setFormFieldValue('Mobile_Num', record.Mobile);
//                        }
//                    });
//                },
//                handleDimission: function (btn) {
//                    myJqGrid.handleEdit({
//                        loadFrom: 'Dimission-div',
//                        btn: btn,
//                        width: 300,
//                        height: 200,
//                        afterFormLoaded: function () {
//                            myJqGrid.setFormFieldValue("IsUsed", false);
//                            myJqGrid.setFormFieldValue("ValidTo", dataTimeFormat(new Date(), 'Y-m-d'));
//                        }
//                    });
//                },

                handlePwd: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'Pwd-div',
                        btn: btn,
                        width: 300,
                        height: 200
                    });
                }
            }
        });

        var myJqGrid2 = new MyGrid({
            renderTo: 'My-JqGrid2'
            , title: "用户角色"
            , autoWidth: true           
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: opts.getUsersUrl
            , storeCondition: "1=1"
		    , sortByField: 'ID'
            , sortorder: 'ASC'
            //, groupField: 'ID'
            //, groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupCollapse: false, groupOrder: ['desc'], minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , primaryKey: 'ID'
		    , dialogWidth: 800
            , dialogHeight: 450
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: false
            , rowNumbers: false
            , autoLoad: false
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 100, hidden: true }
                , { label: '角色名称', name: 'UserID', index: 'UserID', width: 80, hidden: true }
                , { label: '角色名称', name: 'Role.RoleName', index: 'Role.RoleName', width: 220 }
		    ]
            , functions: {
                handleReload: function (btn) {
                    myJqGrid2.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid2.refreshGrid(filterCondition);
                },
                handleAddUserRole: function (btn) {
                    myJqGrid2.showWindow({
                        title: "添加用户角色",
                        btn: btn,
                        loadFrom: 'roleDiv',
                        width: 360,
                        height: 490,
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var keys = unRoleJqGrid.getSelectedKeys();
                                if (isEmpty(keys) || keys.length == 0) {
                                    showMessage("提示", "请选择至少一条记录进行保存");
                                    return;
                                }

                                var ckuserid = document.getElementById("ckuserid").value;
                                if (ckuserid == "") {
                                    showError("提示", "用户ID不能为空！");
                                    return;
                                }
                                ajaxRequest(
                                    opts.AddUserRoleUrl,
                                    {
                                        userId: ckuserid,
                                        ids: keys
                                    },
                                    true,
                                    function () {
                                        //清空当前用户缓存
                                        delete (userFuncCache[ckuserid]);
                                        //重新刷新权限菜单树
                                        loadUserFuncs(ckuserid);

                                        myJqGrid2.reloadGrid();
                                    }
                                );
                                $(this).dialog('close');
                            }
                        },
                        afterFormLoaded: function () {
                            //myJqGrid2.refreshGrid();
                        },
                        postCallBack: function (response) {

                        }
                    });
                    var id = myJqGrid.getSelectedKey();
                    document.getElementById("ckuserid").value = id; //已选择用户ID
                    //传递给下一个界面过滤条件
                    unRoleJqGrid.getJqGrid().jqGrid('setGridParam', { postData: { userId: id} }).trigger("reloadGrid");
                }
                //移除用户角色
                , handleRemoveUserRole: function (btn) {
                    if (opts.currentUser == myJqGrid2.getSelectedKey()) {
                        showMessage('提示', '不能删除自己！');
                        return;
                    }
                    var keys = myJqGrid2.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录");
                        return;
                    }
                    showConfirm('确认', '是否确定继续操作？', function () {
                        ajaxRequest(
                                    opts.removeUserRoleUrl,
                                    {
                                        ids: keys
                                    },
                                    true,
                                    function () {
                                        myJqGrid2.reloadGrid();
                                        var ckuserid = document.getElementById("ckuserid").value;
                                        //清空当前用户缓存
                                        delete (userFuncCache[ckuserid]);
                                        //重新刷新权限菜单树
                                        loadUserFuncs(ckuserid);
                                    }
                                );
                    });
                }

            }
        });

        //角色列表
        var unRoleJqGrid = new MyGrid({
            renderTo: 'roleList'
            //,title:"用户角色"
            //, buttons: buttons1
            , height: gGridHeight * 1.5/ 3
		    , storeURL: opts.getUnUserRoleUrl
            , storeCondition: "1=1"
		    , sortByField: 'ID'
            , sortorder: 'ASC'
		    , primaryKey: 'ID'
		    , dialogWidth: 880
            , dialogHeight: 600
		    , setGridPageSize: 30
		    , showPageBar: true
            , rowNumbers: false
            , excelExport: false
            , singleSelect: false
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '角色名称', name: 'RoleName', index: 'RoleName', width: 120 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 120 }
		    ]
            , functions: {
                handleReload: function (btn) {
                    unRoleJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    unRoleJqGrid.refreshGrid();
                },
                handleRoleAddUser: function (btn) {

                   
                }
            }
        });

    //缓存用户权限
    var userFuncCache = {};
    myJqGrid.addListeners('rowclick', function (id, record, rowIndex, cellIndex) {
        myJqGrid2.refreshGrid("UserID='" + id + "'");
        if (userFuncCache.hasOwnProperty(id)) {
            bindUserFuncs(userFuncCache[id]);
        }
        else {
            loadUserFuncs(id);
        }
    });
    function bindUserFuncs(data) {
        var treeObj = $.fn.zTree.getZTreeObj('sysFuncs');
        treeObj.checkAllNodes(false);
        //绑定用户已有权限        　                                      
        for (var i = 0; i < data.length; i++) {
            var func = data[i];
            var node = treeObj.getNodesByParam("id", func.ID);
            //console.dir(node[0]);

            if (node && node.length > 0) {
                treeObj.checkNode(node[0], true, false, false);
            }
        }
    }
    function loadUserFuncs(userId) {
        $.post(
                opts.getUserFuncsUrl,
                { userId: userId },
                function (data, textStatus) {
                    bindUserFuncs(data);
                    userFuncCache[userId] = data;
                },
                'json'
             );
    }

    //ztree
    var treeSettings = {
        check: {
            enable: true
        },
        view: { selectedMulti: false },
        data: {
            simpleData: {
                enable: true
            },
            key: { title: 'title' }
        },
        async: {
            enable: true,
            url: opts.sysFuncTreeUrl,
            autoParam: ["id", "name", "level"]
            // otherParam: { "otherParam": "zTreeAsyncTest" }　
        },
        callback: {
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            }
        }
    };
    $.fn.zTree.init($('#sysFuncs'), treeSettings);

}