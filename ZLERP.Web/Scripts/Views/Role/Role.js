function roleIndexInit(opts) {
    $('#sysFuncs').height(gGridHeight + 80);

    var filterCondition = "1=1";
    var currentUser = opts.currentUser;
    if (currentUser != "admin") {
        filterCondition += " and ID<>'admin'";
    }

    //1.角色列表
    var myJqGrid = new MyGrid({
        renderTo: 'My-JqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: "1=1"
		    , sortByField: 'ID'
            , sortorder: 'ASC'
		    , primaryKey: 'ID'
		    , dialogWidth: 780
            , dialogHeight: 450
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: true
            , excelExport: false
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 100 }
                , { label: '角色名称', name: 'RoleName', index: 'RoleName', width: 120 }
                , { label: '备 注', name: 'Remark', index: 'Remark', width: 280 }
                , { label: '是否管理员', name: 'IsAdministrator', index: 'IsAdministrator', width: 70, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
		    ]
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        //title: "",
                        btn: btn,
                        loadFrom: 'My-FormDiv',
                        width: 300,
                        height:200,
                        afterFormLoaded: function () {

                        },
                        postCallBack: function (response) {

                        }
                    });
                },
                handleEdit: function (btn) {
                    $('#Password').addClass('ignore');
                    $('#Password').removeClass('requiredval');
                    myJqGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'My-FormDiv',
                        width: 300,
                        height: 200,
                        //getUrl: opts.getUserInfoUrl,
                        afterFormLoaded: function () {
                            //myJqGrid.setFormFieldReadOnly('ID', true);
                            //$('#Password').attr('disabled', false);
                        },
                        beforeSubmit: function () {
//                            var pwd = myJqGrid.getFormFieldValue('Password');
//                            if (isEmpty(pwd)) {
//                                //未填写密码则不提交密码，防止更新密码为空
//                                $('#Password').attr('disabled', true);
//                            }
//                            else {
//                                //因为前面忽略了Password字段的验证，手动验证密码长度
//                                var pwdField = myJqGrid.getFormField('Password');
//                                if (pwdField) {
//                                    var minLen = parseInt(pwdField.attr('data-val-length-min'));
//                                    var maxLen = parseInt(pwdField.attr('data-val-length-max'));

//                                    if (pwd.length < minLen || pwd.length > maxLen) {
//                                        showError('验证错误', '密码长度只能是' + minLen + '到' + maxLen + '之间的字符');
//                                        return false;
//                                    }
//                                }
//                            }
                            return true;
                        },
                        postCallBack: function (response) {
                            //var userId = myJqGrid.getFormFieldValue('ID');
                            //var copyFrom = myJqGrid.getFormFieldValue('CopyPower');
                            //if (response.Result) { copyUserPower(userId, copyFrom); }
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
                }
                , handleSaveRoleFuncs: function (btn, e) {
                    //保存权限  
                    var roleId = myJqGrid.getSelectedKey();
                    var treeObj = $.fn.zTree.getZTreeObj('sysFuncs');
                    var nodes = treeObj.getCheckedNodes();
                    if (roleId) {
                        var ids = [];
                        for (var i = 0; i < nodes.length; i++) {
                            ids.push(nodes[i].id);
                        }
                        ajaxRequest(
                            btn.data.Url,
                            { roleIds: roleId, powers: ids },
                            true,
                            function () {
                                //清空当前用户缓存
                                delete (roleFuncCache[roleId]);
                            }
                        );
                    }
                    else {
                        //未选择用户
                    }
                },
                handleAllotRoleUser: function (btn) {
                    myJqGrid.showWindow({
                        title: "角色/分配权限",
                        //btn: btn,
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }},
                        loadFrom: 'allotRoleUserForm',
                        width: 750,
                        height:580,
                        afterFormLoaded: function () {

                        },
                        postCallBack: function (response) {

                        }
                    });
                }
            }
    });
    //缓存角色权限
    var roleFuncCache = {};
    myJqGrid.addListeners('rowclick', function (id, record, rowIndex, cellIndex) {
        if (roleFuncCache.hasOwnProperty(id)) {
            bindUserFuncs(roleFuncCache[id]);
        }
        else {
            loadUserFuncs(id);
        }
    });
    function bindUserFuncs(data) {
        var treeObj = $.fn.zTree.getZTreeObj('sysFuncs');
        treeObj.checkAllNodes(false);
        //绑定角色已有权限        　                                      
        for (var i = 0; i < data.length; i++) {
            var func = data[i];
            var node = treeObj.getNodesByParam("id", func.ID);
            //console.dir(node[0]);

            if (node && node.length > 0) {
                treeObj.checkNode(node[0], true, false, false);
            }
        }
    }
    //加载角色菜单
    function loadUserFuncs(roleId) {
        $.post(
                opts.getRoleFuncsUrl,
                { roleId: roleId },
                function (data, textStatus) {
                    bindUserFuncs(data);
                    roleFuncCache[roleId] = data;
                },
                'json'
             );
    }
    //ztree
    var treeSettings = {
        check: {
            enable: true
        },
        view: { selectedMulti: false,
                dblClickExpand:false },
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
            //onRightClick: OnRightClick,
            onClick: onClick,
            onAsyncError: function (event, treeId, node, XMLHttpRequest, textStatus, errorThrown) {
                handleServerError(XMLHttpRequest, textStatus, errorThrown);
            }
        }
    };
    var zTree=$.fn.zTree.init($('#sysFuncs'), treeSettings);
    function onClick(e, treeId, treeNode) {
        
        zTree.expandNode(treeNode);
    }

    //2.角色列表
    var roleJqGrid = new MyGrid({
        renderTo: 'roleList'
        //, autoWidth: true
            , buttons: buttons1
            , height: gGridHeight * 1.8 / 3
		    , storeURL: opts.storeUrl
            , storeCondition: filterCondition
		    , sortByField: 'ID'
            , sortorder: 'ASC'
		    , primaryKey: 'ID'
		    , dialogWidth: 780
            , dialogHeight: 450
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , rowNumbers: false
            , excelExport: false
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '角色名称', name: 'RoleName', index: 'RoleName', width: 120 }
                , { label: '备 注', name: 'Remark', index: 'Remark', width: 180 }

		    ]
            , functions: {
                handleReload: function (btn) {
                    roleJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    roleJqGrid.refreshGrid(filterCondition);
                },
                handleRoleAddUser: function (btn) {
                    var keys = roleJqGrid.getSelectedRecord();
                    if (keys.ID==null) {
                        showMessage("提示", "请选择至少一个角色");
                        return;
                    }

                    roleJqGrid.showWindow({
                        title: "添加角色用户",
                        btn: btn,
                        loadFrom: 'unRoleUserForm',
                        width: 360,
                        height: 490,
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "确定": function () {
                                var keys = unRoleUserJqGrid.getSelectedKeys();
                                if (isEmpty(keys) || keys.length == 0) {
                                    showMessage("提示", "请选择至少一条记录进行保存");
                                    return;
                                }
                                
                                var roleid = document.getElementById("ckroleid").value;
                                if (roleid == "") {
                                    showError("提示", "角色ID不能为空！");
                                    return;
                                }

                                ajaxRequest(
                                    opts.AddRoleUserUrl,
                                    {
                                        roleId: roleid,
                                        ids: keys
                                    },
                                    true,
                                    function () {
                                        //$(btn.currentTarget).button({ disabled: false });
                                        userJqGrid.reloadGrid();
                                    }
                                );
                                //userJqGrid.refreshGrid();
                                $(this).dialog('close');
                                //roleJqGrid.refreshGrid();
                                
                            }
                        },
                        afterFormLoaded: function () {
                            userJqGrid.refreshGrid();
                        },
                        postCallBack: function (response) {

                        }
                    });
                    var id = roleJqGrid.getSelectedKey();
                    document.getElementById("ckroleid").value = id; //已选择角色ID
                    unRoleUserJqGrid.getJqGrid().jqGrid('setGridParam', { postData: { roleId: id} }).trigger("reloadGrid");
                }
            }
    });

        roleJqGrid.addListeners('rowclick', function (id, record, selBool) {
            userJqGrid.refreshGrid("roleid='" + id + "'");
            
        });
       
        //2.1已添加的角色用户
        var userJqGrid = new MyGrid({
            renderTo: 'userList'
            //, autoWidth: true
            , buttons: buttons2
            //, width:200
            , height: gGridHeight *1.8/3
		    , storeURL: opts.getUsersUrl
            , storeCondition: "1=1"
		    , sortByField: 'ID'
            , sortorder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 100
		    , showPageBar: true
            //, singleSelect: true
            , rowNumbers: false
            , autoLoad: false
            , excelExport: false
            , toolbarRefresh: false
		    , initArray: [
                { label: '编码', name: 'ID', index: 'ID', width: 80 ,hidden:true} 
                ,{label: '登陆用户名', name: 'UserID', index: 'UserID', width: 140 }
                , { label: '登陆用户名', name: 'User.TrueName', index: 'User.TrueName', width: 160 }

		    ]
            , functions: {
                handleReload: function (btn) {
                    userJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    userJqGrid.refreshGrid();
                },
                handleRemoveRoleUser: function (btn) {
                    var keys = userJqGrid.getSelectedKeys();
                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择至少一条记录");
                        return;
                    }
                    ajaxRequest(
                                    opts.removeRoleUserUrl,
                                    {
                                        ids: keys
                                    },
                                    true,
                                    function () {
                                        userJqGrid.reloadGrid();
                                    }
                                );

                }
            }
        });

        //3.未添加的角色用户
        var unRoleUserJqGrid = new MyGrid({
            renderTo: 'unroleUserList'
            //, autoWidth: true
            //, width:200
            , height: gGridHeight / 2
		    , storeURL: opts.getUnRoleUserUrl
            , storeCondition: "1=1"
		    , sortByField: 'ID'
            , sortorder: 'DESC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 100
		    , showPageBar: true
            //, singleSelect: true
            , rowNumbers: false
            , autoLoad: false
            , excelExport: false
            , toolbarRefresh: false
		    , initArray: [
                { label: '登陆用户名', name: 'ID', index: 'ID', width: 140 }
                , { label: '登陆用户名', name: 'TrueName', index: 'TrueName', width: 160 }

		    ]
            , functions: {
                handleReload: function (btn) {
                    unRoleUserJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    unRoleUserJqGrid.refreshGrid(filterCondition);
                }
            }
        });
}