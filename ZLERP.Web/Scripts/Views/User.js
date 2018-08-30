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
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
            , storeCondition: filterCondition
		    , sortByField: 'Department.DepartmentName'
            , groupField: 'DepartmentName'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupCollapse: false, groupOrder: ['desc'],minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
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
                , { label: '班类型', name: 'ClassType', index: 'ClassType', width: 50, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'DriverClassType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('DriverClassType')} }

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
                        title: "新增用户(初始密码为：<font color='red'>123456</font>)",
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
                }
                , handleSavePower: function (btn, e) {
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
                handleSendMsg: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    myJqGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'Msg-Div',
                        width: 300,
                        height: 200,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldValue('Mobile_Num', record.Mobile);
                        }
                    });
                },
                handleDimission: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'Dimission-div',
                        btn: btn,
                        width: 300,
                        height: 200,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldValue("IsUsed", false);
                            myJqGrid.setFormFieldValue("ValidTo", dataTimeFormat(new Date(), 'Y-m-d'));
                        }
                    });
                },
                handlePwd: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'Pwd-div',
                        btn: btn,
                        width: 300,
                        height: 200
                    });
                }
                , handleViewDriver: function (btn) {
                    myJqGrid.refreshGrid("UserType ='01'");
                }
            }
    });
    //缓存用户权限
    var userFuncCache = {};
    myJqGrid.addListeners('rowclick', function (id, record, rowIndex, cellIndex) {
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