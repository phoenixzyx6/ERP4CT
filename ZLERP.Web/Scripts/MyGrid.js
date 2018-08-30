/*
MyGrid使用说明:
var mygrid = new MyGrid({
        renderTo: 'first-grid'
		, title: '用户列表'                       
        , width: '100%'
        , autoWidth: true 
        , buttons: @Html.GetButtons(0) // 获取flag = 0的按钮
        , height: 340
		, storeURL: '/User.mvc/Find'
        //, toolbarSearch:true
		, sortByField: 'ID'
		, primaryKey: 'ID'
		//, groupField: 'UserType'     //按某个字段进行分组
		, setGridPageSize: 30
		, showPageBar: true
        //, singleSelect: true        //默认为多选
        , editSaveUrl: '/User.mvc/Update'  //表格编辑指定url
        , treeGrid: false  //是否为TreeGrid
        treeGridModel: 'adjacency', 
        expandColumn: '',
        expandColClick: true,
        sortOrder: 'DESC',
        dialogWidth: 600, //默认增加，编辑对话框宽度
        dialogHeight: 400//默认增加，编辑对话框高度
		, initArray: [
			{ label: '用户ID', name: 'ID', index: 'ID', width: 90, sortable: true },
            { label: '真实姓名', name: 'TrueName', index: 'TrueName', width: 100, editable: true },
            { label: '邮箱地址', name: 'Email', index: 'Email', width: 100, editable: true },
            { label: '电话', name: 'Tel', index: 'Tel', width: 100 },
            { label: '手机', name: 'Mobile', index: 'Mobile', width: 80, align: "right" },
            { label: '部门', name: 'DepartmentID', index: 'DepartmentID', width: 80, align: "right" },
            { label: '上级领导', name: 'ManagerID', index: 'ManagerID', width: 80, align: "right" },
            { label: '用户类别', name: 'UserType', index: 'UserType', width: 150, sortable: false }
		]
		, autoLoad: true             //是否首次自动加载数据
        , functions: {
            handleReload: function (btn) {
                mygrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                mygrid.refreshGrid('1=1');
            },
            handleAdd: function (btn) {
                mygrid.handleAdd({
                    title: '新建客户从DIV加载',
                    width: 600,
                    height: 400,
                    loadFrom: 'MyFormDiv',
                    //formId: 'myform',
                    postUrl: '/User.mvc/Add',
                    postCallBack: function (response) {
                        alert('handleadd callback..');
                    }
                });
            },
            handleEdit: function (btn) {
                //alert(mygrid.getSelectedKey());
                mygrid.handleEdit({
                    title: '新建客户从URL加载',
                    width: 600,
                    height: 400,
                    loadFrom: 'MyFormDiv',
                    //getUrl: '',
                    postUrl: '/User.mvc/Update'
                    //,postCallBack: function (response) { alert('handle edit callback..'+response.Message); }
                    //,reloadGrid: false
                });
            }
            , handleDelete: function (btn) {
                mygrid.deleteRecord({
                    deleteUrl: '/User.mvc/Delete'
                    //,singleDelete:false
                    , reloadGrid: true
                    , postCallBack: function (response) { alert('delete callback ' + response.Message); }
                });
            }
            , handleEditFromURL: function (btn) {
                mygrid.handleEdit({
                    title: '新建客户从URL加载',
                    width: 600,
                    height: 400,
                    loadFrom: '/User.mvc/LoadForm',
                    getUrl: 'User.mvc/Get',
                    postUrl: '/User.mvc/Update'
                    //,postCallBack: function (response) { alert('handle edit callback..'+response.Message); }
                    //,reloadGrid: false
                });
            },
            handleAddFromURL: function () {
                mygrid.handleAdd({
                    title: '新建客户从URL加载',
                    width: 600,
                    height: 400,
                    loadFrom: '/User.mvc/LoadForm',
                    //formId: 'myform',
                    postUrl: '/User.mvc/Add'
                    , postCallBack: function (response) { alert('handleadd callback..' + response.Message); }
                    //,reloadGrid:false
                });
            }
        }
    });

    // 修改后回调函数处理,表格编辑事件回调。
    mygrid.addListeners('afterSubmitCell', function (response) {
        alert("afterSubmitCell callBack：" + response.Message);
    });

 
    //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
    mygrid.addListeners('rowclick', function (id, record,selBool) {
        alert("rowclick："+ id +"\n select status:"+ selBool);
    });
    
    //rowdblclick 事件定义
    mygrid.addListeners('rowdblclick', function (id, record, rowIndex, cellIndex) {
        alert(id + "\n" + record['KHMC']+"\n"+rowIndex+"\n"+cellIndex);
    });

    //cellclick 单元格点击事件
    mygrid.addListeners('cellclick', function (id, record, colIndex, cellcontent) {
        alert(id + "\n" + record['KHMC'] + "\n" + colIndex + "\n" + cellcontent);
    });

*/

//override the jqgrid defaults
//by Sky 2012/03/13
jQuery.extend(jQuery.jgrid.defaults, {
    errorCell: function (serverresponse, status) {
        handleServerError(serverresponse, status);
    },
    loadError: function (xhr, status, error) {
        handleServerError(xhr, status, "加载数据出错");
    }
});
//设置validator的忽略样式
$.validator.defaults.ignore = ".ignore";
MyGrid = function (GridJson) {
    //常用函数定义
    var defaults = {
        treeGrid: false,
        treeGridModel: 'adjacency',
        expandColumn: '',
        expandColClick: true,
        sortOrder: 'DESC',
        dialogWidth: 600,
        dialogHeight: 400,
        toolbarSearch: true,
        altRows: true,
        autoLoad: true,
        hidegrid: false,
        rowNumbers: false,
        gridview: true,
        scroll: false,
        hoverrows: false,
        footerrow: false,
        userDataOnFooter: false,
        toolbarRefresh: true,
        loadonce: false,
        //列拖动
        columnReOrder: false,
        advancedSearch: false,
        excelExport: true
    };
    GridJson = $.extend(defaults, GridJson);
    var renderDiv = GridJson.renderTo;
    var toobarClsName = renderDiv + "-toolbar";
    var dataGridId = renderDiv + "-datagrid";
    var pageBarId = renderDiv + "-pager";
    $('#' + renderDiv).append("<div id='" + toobarClsName + "' class='grid-toolbar " + toobarClsName + "'></div><table id='" + dataGridId + "'></table>");
    //分页栏构造
    if (GridJson.showPageBar == undefined || GridJson.showPageBar) {
        $('#' + renderDiv).append("<div id='" + pageBarId + "'></div>");
        pageBarId = '#' + pageBarId;
    } else {
        pageBarId = '';
    }

    this.getJqGrid = function () {
        return $("#" + dataGridId);
    };

    this.showMask = function () {

    };


    this.closeMask = function () {

    };
    //合并form参数和附加的postData对象
    _combinePostData = function (formData, postData) {
        //附加额外的参数
        if (typeof (postData) == 'object' && !$.isEmptyObject(postData)) {
            formData += "&" + jQuery.param(postData, true);
        }
        return formData;
    };

    // ############   相关参数处理 #####################
    //是否自动加载数据
    var autoLoad = true;
    if (!GridJson.autoLoad) {
        autoLoad = false;
    }
    //pageSize定义
    var pageSize = -1;
    if (!isEmpty(GridJson.setGridPageSize) && parseInt(GridJson.setGridPageSize) > 0) {
        pageSize = GridJson.setGridPageSize;
    }
    var multiSelect = true;
    if (!isEmpty(GridJson.singleSelect) && GridJson.singleSelect == true) {
        multiSelect = false;
    }

    var primaryKeyField = 'id';
    if (!isEmpty(GridJson.primaryKey)) {
        primaryKeyField = GridJson.primaryKey;
    }

    var sortByField = primaryKeyField;
    if (!isEmpty(GridJson.sortByField)) {
        sortByField = GridJson.sortByField;
    }

    var groupingBool = false;
    var groupViewConfig = {};
    if (!isEmpty(GridJson.groupField)) {
        groupingBool = true;
        groupViewConfig = { groupField: [GridJson.groupField] };
        //允许重写groupingView
        if (!isEmpty(GridJson.groupingView)) {
            $.extend(groupViewConfig, GridJson.groupingView);
        }
    }



    // ##############  grid操作函数定义  ############################
    //本页重新加载grid
    this.reloadGrid = function (condition) {
        var options = { url: GridJson.storeURL, datatype: "json" };
        if (!isEmpty(condition)) {
            options = { url: GridJson.storeURL, datatype: "json", postData: { condition: condition} };
        }
        jQuery("#" + dataGridId).jqGrid('setGridParam', options).trigger("reloadGrid");
    };

    //重新刷新grid，且定位到第1页
    this.refreshGrid = function (condition) {
        var options = { url: GridJson.storeURL, datatype: "json", page: 1 };
        if (!isEmpty(condition)) {
            options = { url: GridJson.storeURL, datatype: "json", page: 1, postData: { condition: condition} };
        }
        return jQuery("#" + dataGridId).jqGrid('setGridParam', options).trigger("reloadGrid");
    };

    this.setTitle = function (titleName) {

        jQuery("#" + dataGridId).setCaption(titleName);

    };

    /*
    getGridParam("url")： 获取当前的AJAX的URL
　　getGridParam("sortname")：排序的字段
　　getGridParam("sortorder")：排序的顺序
　　getGridParam("selrow")：得到选中行的ID
　　getGridParam("page")：当前的页数
　　getGridParam("rowNum")：当前有多少行
　　getGridParam("datatype")：得到当前的datatype
　　getGridParam("records")：得到总记录数
　　getGridParam("selarrrow")：可以多选时，返回选中行的ID
    */
    this.getGridParam = function (paramName) {
        var value = jQuery("#" + dataGridId).jqGrid('getGridParam', paramName);
        return value;
    };

    /*
    setGridParam({url:newvalue})：可以设置一个grid的ajax url，可配合trigger("reloadGrid")使用
　　setGridParam({sortname:newvalue})：设置排序的字段
　　setGridParam({sortorder:newvalue})：设置排序的顺序asc or desc
　　setGridParam({page:newvalue})：设置翻到第几页
　　setGridParam({rowNum:newvalue})：设置当前每页显示的行数
　　setGridParam({datatype:newvalue})：设置新的datatype(xml,json)

    */
    this.setGridParam = function (optionJson, isRelaod) {
        /*并不是修改每一个参数都需要重新relaod的，默认情况下为重新relaod
        * modify by xyl on 2012-3-13
        */
        var isRelaodGrid = isEmpty(isRelaod) ? true : isRelaod;
        if (isRelaodGrid) {
            jQuery("#" + dataGridId).jqGrid('setGridParam', optionJson).trigger("reloadGrid");
        } else {
            jQuery("#" + dataGridId).jqGrid('setGridParam', optionJson);
        }
    };

    this.isSelectedOnlyOne = function (msg) {
        if (multiSelect) {
            var selectedId = this.getGridParam('selarrrow');
            if (selectedId.length == 1) {
                return true;
            }
        }
        else {
            if (!isEmpty(this.getGridParam('selrow'))) {
                return true;
            }
        }
        if (!isEmpty(msg)) {
            showMessage('提示', msg);
        }
        return false;
    };

    //获得所选的单行
    this.getSelectedKey = function () {
        var record = this.getSelectedRecord();
        var keys = record[primaryKeyField];
        return isEmpty(keys) ? '' : keys;
    };

    //获得所选的所有行
    this.getSelectedKeys = function () {
        var selectedId = this.getGridParam('selarrrow');
        return selectedId;
    };

    //通过Key值获取当前行
    this.getRecordByKeyValue = function (keyvalue) {
        var record = jQuery("#" + dataGridId).jqGrid('getRowData', keyvalue);
        return record;
    };

    //获取所选的所有行数据
    this.getSelectedRecords = function () {
        var selectedId = this.getGridParam('selarrrow');
        var records = [];
        for (var i = 0; i < selectedId.length; i++) {
            records.push(this.getRecordByKeyValue(selectedId[i]));
        }
        return records;
    };

    //获取所选单行的数据
    this.getSelectedRecord = function () {
        var id = this.getGridParam('selrow'); //所选行id,即主键
        var record = jQuery("#" + dataGridId).jqGrid('getRowData', id);
        return record;
    };

    //获取Grid表格数据
    this.getAllRecords = function () {
        var ids = jQuery("#" + dataGridId).getDataIDs();
        var records = [];
        for (var i = 0; i < ids.length; i++) {
            records.push(this.getRecordByKeyValue(ids[i]));
        }
        return records;
    };

    //手动设置某行记录被选中
    this.setSelection = function (id) {
        jQuery("#" + dataGridId).jqGrid('setSelection', id);
    };

    ////////////////////////////////////设置某一列为日期范围搜索 by:Sky 2013/05/09
    function beforeToolBarSearch() {
        var postData = $(this).jqGrid('getGridParam', 'postData');
        var strFilters = postData.filters;
        var filters = $.parseJSON(strFilters);
        var reDateExact = /^(\d{4}\-\d{2}\-\d{2})$/i; //指定查一天的数据
        var reDateRange = /^(\d{4}\-\d{2}\-\d{2}) \- (\d{4}\-\d{2}\-\d{2})$/i; //指定日期范围
        var newRules = [];
        if (filters && typeof (filters.rules) !== 'undefined' && filters.rules.length > 0) {
            for (var i = 0; i < filters.rules.length; i++) {
                var rule = filters.rules[i];
                if (rule.op == "range") {
                    if (reDateRange.test(rule.data)) {
                        var matches = rule.data.match(reDateRange);
                        newRules.push({ field: rule.field, op: 'ge', data: matches[1] });
                        newRules.push({ field: rule.field, op: 'le', data: matches[2] });

                    }
                    else if (reDateExact.test(rule.data)) {
                        //指定某一天
                        var matches = rule.data.match(reDateExact);
                        newRules.push({ field: rule.field, op: 'ge', data: matches[1] });
                        newRules.push({ field: rule.field, op: 'le', data: matches[1] + ' 23:59:59.997' });
                    }
                }
                else {
                    newRules.push(rule);
                }
            }
            filters.rules = newRules;
            var newFilters = JSON.stringify(filters);
            postData.filters = newFilters;

            $(this).jqGrid('setGridParam', { postData: postData });
            return false;
        }

    };


    this.setRangeSearch = function (columnName) {

        _grid.jqGrid('setColProp', columnName,
                                        {
                                            stype: 'text',
                                            searchoptions: {
                                                sopt: ['range', 'bd', 'ed'],
                                                dataInit: function (el) {
                                                    //console.dir(el);
                                                    if (el.id.toString().indexOf('gs_') != -1) {
                                                        $(el).daterangepicker({ onClose: function () {
                                                            _grid[0].triggerToolbar();
                                                        }
                                                        });
                                                    } else {
                                                        $(el).datetimepicker();
                                                    }
                                                }
                                            }
                                        });
        _grid.jqGrid('filterToolbar', { stringResult: true, beforeSearch: beforeToolBarSearch });
    };
    ///END/////////////////////////////设置某一列为日期范围搜索 by:Sky 2013/05/09

    this.getAllIds = function () {
        var ids = jQuery("#" + dataGridId).getDataIDs();
        return ids;
    };

    //group 分组处理
    this.groupByField = function (fieldName) {
        jQuery("#" + dataGridId).jqGrid('groupingGroupBy', fieldName);
    };

    this.clearGroup = function () {
        jQuery("#" + dataGridId).jqGrid('groupingRemove', true);
    };


    //################ 表格事件处理 ###############################
    var eventFunc = Array();
    //行选择前事件
    this.beforeSelectRow = function (rowid, e) {
        var callBack = eventFunc['beforeselect'];
        if (jQuery.isFunction(callBack)) {
            var record = jQuery("#" + dataGridId).jqGrid('getRowData', rowid); //获得选择行数据,根据行id和'getRowData'
            jQuery(callBack(rowid, record));

        }
        return true;
    };
    //单击事件
    this.onSelectRow = function (id, selbool) {
        var callBack = eventFunc['rowclick'];
        if (jQuery.isFunction(callBack)) {
            var record = jQuery("#" + dataGridId).jqGrid('getRowData', id);
            jQuery(callBack(id, record, selbool));
        }
    };
    //双击事件
    this.onDBClickRow = function (id, rowIndex, cellIndex, e) {
        var callBack = eventFunc['rowdblclick'];
        if (jQuery.isFunction(callBack)) {
            var record = jQuery("#" + dataGridId).jqGrid('getRowData', id);
            jQuery(callBack(id, record, rowIndex, cellIndex));
        }
    };
    //选中某个单元格
    this.onCellClick = function (id, colIndex, cellcontent, e) {
        var callBack = eventFunc['cellclick'];
        if (jQuery.isFunction(callBack)) {
            var record = jQuery("#" + dataGridId).jqGrid('getRowData', id);
            jQuery(callBack(id, record, colIndex, cellcontent));
        }
    };
    //gridComplete事件
    this.onGridComplete = function () {
        var callBack = eventFunc['gridComplete'];
        if (jQuery.isFunction(callBack)) {
            jQuery(callBack);
        }
        if (GridJson.treeGrid) {
            jQuery("#" + dataGridId).jqGrid("SortTree", this.p.sortname, this.p.sortorder);
        }
    };

    this.addListeners = function (event, callback) {
        var func = eventFunc[event];
        if (isEmpty(func)) {
            eventFunc[event] = callback;
        }
    };
    //表格 提交
    this.handleFormSubmit = function (options) {
        $.validator.unobtrusive.parse(document);
        var formId = options.formId;
        if (!$("#" + formId).valid()) {
            var valMsg = $('[data-valmsg-summary=true]');
            if (valMsg.length > 0) {
                valMsg.hide();
                showError('验证错误', valMsg.html());
            }
            return;
        }
        //beforeSubmit事件
        if (options.beforeSubmit && jQuery.isFunction(options.beforeSubmit)) {
            var result = options.beforeSubmit(formId);
            if (!result)
                return;
        }

        var postData = $("#" + formId).serialize();
        //附加额外的参数
        postData = _combinePostData(postData, options.postData);
        //执行ajax请求
        ajaxRequest(options.postUrl, postData, true, function (response) {
            var postCallBack = options.postCallBack;
            executeFunction(postCallBack, response);
        }, null, options.btn);
    };

    //#################  jqGrid表格初始化  ##################################
    var formPanelId;
    //增加 功能
    this.handleAdd = function (options) {
        if (options.btn) {
            var defaults = {
                title: options.btn.data.FuncDesc,
                postUrl: options.btn.data.Url
            };
            options = $.extend(defaults, options);
        }

        var divOrUrl = options.loadFrom;
        var postUrl = options.postUrl;
        var formId = 'mygrid-form-' + Math.floor(Math.random() * 1000);
        if (!isEmpty(options.formId)) {
            formId = options.formId;
        }
        formPanelId = formId;
        var dialogDiv = divOrUrl;
        if (divOrUrl.indexOf('/') != -1) {
            $("#MyGrid-Dialog").empty();
            dialogDiv = "MyGrid-Dialog";
            $("#MyGrid-Dialog").load(divOrUrl, '', function () {
                initErpControls($(this));
                $('#MyGrid-Dialog form').attr("id", formId);
                $('#MyGrid-Dialog form').attr("action", postUrl);
                $("#" + formId)[0].reset();
                //清除之前的验证错误样式
                if ($('#' + formId).length > 0 && $.data($('#' + formId)[0], 'validator')) {
                    $.data($('#' + formId)[0], 'validator').resetForm();
                }
                if (!isEmpty(options.afterFormLoaded)) {
                    executeFunction(options.afterFormLoaded, formId);
                }
                $.validator.unobtrusive.parse(document);

            });
        } else {
            $("#" + divOrUrl + " form").attr("id", formId);
            $("#" + divOrUrl + " form").attr("action", postUrl);
            $("#" + formId)[0].reset();
            //清除之前的验证错误样式
            if ($('#' + formId).length > 0 && $.data($('#' + formId)[0], 'validator')) {
                $.data($('#' + formId)[0], 'validator').resetForm();
            }
            if (!isEmpty(options.afterFormLoaded)) {
                executeFunction(options.afterFormLoaded, formId);
            }
            $.validator.unobtrusive.parse(document);
        }
        $("#" + dialogDiv).dialog({
            title: options.title,
            width: options.width ? options.width : GridJson.dialogWidth,
            height: options.height ? options.height : GridJson.dialogHeight,
            modal: true,
            autoOpen: true,
            closeOnEscape: true,
            closeText: ''
            , buttons: options.buttons ? options.buttons : {
                "关闭": function () {
                    //$("#" + dialogDiv).dialog('close');
                    $(this).dialog("close");
                }, "保存": function (btn) {
                    if (!$("#" + formId).valid()) {
                        //$($("#" + formId)).submit();
                        var valMsg = $('[data-valmsg-summary=true]', this);
                        if (valMsg.length > 0) {
                            valMsg.hide();
                            showError('验证错误', valMsg.html());
                        }
                        return;
                    }
                    $(btn.currentTarget).button({ disabled: true });

                    //beforeSubmit事件
                    if (options.beforeSubmit && jQuery.isFunction(options.beforeSubmit)) {
                        var result = options.beforeSubmit(formId);
                        if (!result) {
                            $(btn.currentTarget).button({ disabled: false });
                            return;
                        }
                    }



                    var reloadGrid = true;
                    if (options.reloadGrid != undefined && options.reloadGrid == false) {
                        reloadGrid = false;
                    }
                    var postData = $("#" + formId).serialize();
                    //附加额外的参数
                    postData = _combinePostData(postData, options.postData);
                    //执行ajax请求
                    ajaxRequest(postUrl, postData, true, function (response) {
                        //窗口关闭处理
                        var closeDialog = true;
                        if (!isEmpty(options.closeDialog) && !options.closeDialog) {
                            closeDialog = false;
                        }

                        if (reloadGrid && response.Result) {
                            jQuery("#" + dataGridId).jqGrid('setGridParam', { url: GridJson.storeURL, datatype: "json" }).trigger("reloadGrid");
                        }
                        var postCallBack = options.postCallBack;
                        executeFunction(postCallBack, response);
                        //窗口关闭处理
                        if (response.Result && closeDialog) {
                            $("#" + dialogDiv).dialog('close');
                            //提交成功才reset
                            $("#" + formId)[0].reset();
                        }

                        if (response.Result && !closeDialog) {
                            $(btn.currentTarget).button({ disabled: false });
                        }

                        //保存不成功且没有callback时，重新启用按钮
                        if (!response.Result) {
                            $(btn.currentTarget).button({ disabled: false });
                        }
                    });
                }
            }
        });
        $("#" + dialogDiv).dialog({
            close: function (event, ui) {
                $(this).dialog("destroy");
                if (divOrUrl.indexOf('/') == -1) {
                    //var myform = $("#" + dialogDiv).html();
                    //$("#" + dialogDiv).remove();
                    //$("#container").append('<div id="' + dialogDiv + '">' + myform + '</div>');
                    $("#container").append($("#" + dialogDiv));
                }
            }
        });
    };

    /*
    显示窗口，便于弹出表格等情况处理.
    */
    this.showWindow = function (options) {
        var divOrUrl = options.loadFrom;
        var formId = 'mygrid-form-' + Math.floor(Math.random() * 1000);
        if (!isEmpty(options.formId)) {
            formId = options.formId;
        }
        var dialogDiv = divOrUrl;
        if (divOrUrl.indexOf('/') != -1) {
            $("#MyGrid-Dialog").empty();
            dialogDiv = "MyGrid-Dialog";
            $("#MyGrid-Dialog").load(options.loadFrom, '', function () {
                initErpControls($(this));
                $('#MyGrid-Dialog form').attr("id", formId);
                if (!isEmpty(options.afterLoaded)) {
                    executeFunction(options.afterLoaded, '');
                }
            });
        } else {
            if (!isEmpty(options.afterLoaded)) {
                executeFunction(options.afterLoaded, '');
            }
        }
        $("#" + dialogDiv).dialog({
            title: options.title,
            width: options.width ? options.width : GridJson.dialogWidth,
            height: options.height ? options.height : GridJson.dialogHeight,
            modal: true,
            autoOpen: true,
            resizable: options.resizable == false ? options.resizable : true,
            closeOnEscape: true,
            buttons: options.buttons
        });
        $("#" + dialogDiv).dialog({
            close: function (event, ui) {
                $(this).dialog("destroy");
                if (divOrUrl.indexOf('/') == -1) {
                    $("#container").append($("#" + dialogDiv));
                }
            }
        });
    };
    //编辑 功能
    this.handleEdit = function (options) {
        if (options.btn) {
            var defaults = {
                title: options.btn.data.FuncDesc,
                postUrl: options.btn.data.Url
            };
            options = $.extend(defaults, options);
        }
        if (!this.isSelectedOnlyOne()) {
            showMessage('提示', '请选择一条记录进行操作！');
            return;
        }
        var divOrUrl = options.loadFrom;//显示界面
        var postUrl = options.postUrl;//编辑POST路径
        var formId = 'mygrid-form-' + Math.floor(Math.random() * 1000);
        if (!isEmpty(options.formId)) {
            formId = options.formId;
        }
        formPanelId = formId;
        var dialogDiv = divOrUrl;
        if (divOrUrl.indexOf('/') != -1) {
            dialogDiv = "MyGrid-Dialog";
            $("#MyGrid-Dialog").empty();
            $("#MyGrid-Dialog").load(divOrUrl, '', function () {
                $('#MyGrid-Dialog form').attr("id", formId);
                $('#MyGrid-Dialog form').attr("action", postUrl);
                //清除之前的验证错误样式
                if ($('#' + formId).length > 0 && $.data($('#' + formId)[0], 'validator')) {
                    $.data($('#' + formId)[0], 'validator').resetForm();
                }
                //文档加载完毕后进行数据处理
                var keyValue = jQuery("#" + dataGridId).jqGrid('getGridParam', 'selrow');
                if (options.getUrl != undefined) {
                    //从url加载数据
                    setFormValues(formId, options.getUrl, { id: keyValue }, options.afterFormLoaded);
                } else {
                    //从grid加载数据
                    jQuery("#" + dataGridId).jqGrid('GridToForm', keyValue, "#" + formId, options.prefix, options.jsonprefix);
                    if (!isEmpty(options.afterFormLoaded)) {
                        executeFunction(options.afterFormLoaded, formId);
                    }
                }
                $.validator.unobtrusive.parse(document);
            });
        } else {
            //修改目标action及id
            $('#' + divOrUrl + ' form').attr("id", formId);
            $('#' + divOrUrl + ' form').attr("action", postUrl);
            //清除之前的验证错误样式
            if ($('#' + formId).length > 0 && $.data($('#' + formId)[0], 'validator')) {
                $.data($('#' + formId)[0], 'validator').resetForm();
            }
            //加载数据处理
            if (!isEmpty(options.getUrl)) {
                var keyValue = this.getSelectedKey();
                //从url加载数据
                setFormValues(formId, options.getUrl, { id: keyValue }, options.afterFormLoaded);
            } else {
                //从grid加载数据
                var gsr = this.getGridParam('selrow');
                jQuery("#" + dataGridId).jqGrid('GridToForm', gsr, "#" + formId, options.prefix, options.jsonprefix);
                if (!isEmpty(options.afterFormLoaded)) {
                    executeFunction(options.afterFormLoaded, formId);
                }
            }
            $.validator.unobtrusive.parse(document);
        }

        $("#" + dialogDiv).dialog({
            title: options.title,
            width: options.width ? options.width : GridJson.dialogWidth,
            height: options.height ? options.height : GridJson.dialogHeight,
            modal: true,
            autoOpen: true,
            buttons: options.buttons ? options.buttons : {
                "关闭": function () {
                    $("#" + dialogDiv).dialog('close');
                }, "保存": function (btn) {
                    if (!$("#" + formId).valid()) {
                        //$($("#" + formId)).submit();
                        var valMsg = $('[data-valmsg-summary=true]', this);
                        if (valMsg.length > 0) {
                            valMsg.hide();
                            showError('验证错误', valMsg.html());
                        }
                        return;
                    }

                    //beforeSubmit事件
                    if (options.beforeSubmit && jQuery.isFunction(options.beforeSubmit)) {
                        var result = options.beforeSubmit(formId);
                        if (!result)
                            return;
                    }

                    $(btn.currentTarget).button({ disabled: true });
                    var reloadGrid = true;
                    if (options.reloadGrid != undefined && options.reloadGrid == false) {
                        reloadGrid = false;
                    }
                    //执行ajax请求
                    var postData = $("#" + formId).serialize();
                    //附加额外的参数
                    postData = _combinePostData(postData, options.postData);
                    ajaxRequest(postUrl, postData, true, function (response) {
                        if (reloadGrid && response.Result) {
                            jQuery("#" + dataGridId).jqGrid('setGridParam', { url: GridJson.storeURL, datatype: "json" }).trigger("reloadGrid");
                        }
                        var postCallBack = options.postCallBack;
                        executeFunction(postCallBack, response);
                        var closeDialog = true;

                        if (!isEmpty(options.closeDialog) && !options.closeDialog) {
                            closeDialog = false;
                        }

                        //窗口关闭处理                        
                        if (response.Result && closeDialog) {
                            $("#" + dialogDiv).dialog('close');
                            $("#" + formId)[0].reset();
                        }
                        if (response.Result && !closeDialog) {
                            $(btn.currentTarget).button({ disabled: false });
                        }

                        //保存不成功且没有callback时，重新启用按钮
                        if (!response.Result) {
                            $(btn.currentTarget).button({ disabled: false });
                        }
                    });
                }
            },
            close: function (event, ui) {
                $(this).dialog("destroy");
                if (divOrUrl.indexOf('/') == -1) {
                    //var myform = $("#" + dialogDiv).html();
                    //$("#" + dialogDiv).remove();
                    $("#container").append($("#" + dialogDiv));
                }
            }
        });
        //        $("#" + dialogDiv).dialog({
        //            close: function (event, ui) {
        //                $(this).dialog("destroy");
        //                if (divOrUrl.indexOf('/') == -1) {
        //                    //var myform = $("#" + dialogDiv).html();
        //                    //$("#" + dialogDiv).remove();
        //                    $("#container").append($("#" + dialogDiv));
        //                }
        //            }
        //        });
    };

    this.getFormId = function () {
        return formPanelId;
    };

    this.setFormId = function (formId) {
        formPanelId = formId;
    };

    this.resetForm = function () {
        if (!isEmpty($("#" + formPanelId)[0])) {
            $("#" + formPanelId)[0].reset();
        }
    };

    this.getFormField = function (fieldName) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
            return "";
        }
        else
            return field;
    };

    this.getFormFieldValue = function (fieldName) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
            return "";
        }
        if (field.is("input:radio") || field.is("input:checkbox")) {
            return field.attr('checked');
        } else {
            return $.trim(field.val());
        }
    };

    this.setFormFieldValue = function (fieldName, value) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
        }
        if (field.is("input:radio") || field.is("input:checkbox")) {
            field.attr('checked', value);
        } else {
            //alert(formPanelId+"\n"+fieldName + "\n" + value + "\n" + field);
            field.val(value);
        }
    };

    this.setFormFieldReadOnly = function (fieldName, value) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
        }
        field[0].readOnly = value;
    };

    this.setFormFieldDisabled = function (fieldName, value) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
        }

        if (value == true) {
            field[0].disabled = value;
        } else {
            field.removeAttr("disabled");

        }

    };

    this.setFormFieldChecked = function (fieldName, value) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
        }
        field[0].checked = value;
        //field.attr("checked", true);
    };

    this.setFormFieldHidden = function (fieldName, value) {
        var field = $("[name='" + fieldName + "']", "#" + formPanelId);
        if (isEmpty(field)) {
            showError('错误', '没有找到 ' + fieldName + " 的表单项!");
        }
        field[0].hidden = value;
    };


    this.setFormFieldsUnEdit = function (value) {
        var input_fields = $("#" + formPanelId + " input");
        var select_fields = $("#" + formPanelId + " select");
        var checkbox_fields = $("#" + formPanelId + " input[type=checkbox]");
        for (var i = 0; i < input_fields.length; i++) {
            input_fields[i].disabled = value;
        }
        for (var i = 0; i < select_fields.length; i++) {
            select_fields[i].disabled = value;
        }
        for (var i = 0; i < checkbox_fields.length; i++) {
            checkbox_fields[i].disabled = value;
        }
    };
    //删除 功能
    this.deleteRecord = function (options) {
        var deleteUrl = options.deleteUrl;
        //只能删除一个
        var singleDelete = false;
        if (!isEmpty(options.singleDelete) && options.singleDelete) {
            singleDelete = true;
        }
        //刷新grid
        var reloadGrid = true;
        if (!isEmpty(options.reloadGrid) && options.reloadGrid == false) {
            reloadGrid = false;
        }
        if (singleDelete && !this.isSelectedOnlyOne()) {
            showMessage('提示', '请选择一条记录进行操作！');
            return;
        }
        var keyValue = this.getSelectedKey();
        if (multiSelect) {
            keyValue = this.getGridParam('selarrrow');
        }
        if (isEmpty(keyValue) || keyValue.length == 0) {
            showMessage('提示', '请选择要删除的记录!');
            return;
        }
        showConfirm("确认删除", "确认要删除选中的记录", function () {
            ajaxRequest(deleteUrl, { id: keyValue }, true, function (response) {
                if (reloadGrid && response.Result) {
                    jQuery("#" + dataGridId).jqGrid('setGridParam', { url: GridJson.storeURL, datatype: "json" }).trigger("reloadGrid");
                }
                var postCallBack = options.postCallBack;
                executeFunction(postCallBack, response);
            });
            $(this).dialog('close');
        });


    };

    //editSaveUrl不为空，单元格显示可编辑状态
    var cellEditAble = false;
    if (!isEmpty(GridJson.editSaveUrl)) {
        cellEditAble = true;
    }

    var cellSubmit = "remote";
    if (!isEmpty(GridJson.cellsubmit)) {
        cellSubmit = "clientArray";
    }

    var subGridAble = false;
    if (!isEmpty(GridJson.subGridModel)) {
        subGridAble = true;
    }

    var autoWidth = false;
    if (!isEmpty(GridJson.autoWidth) && GridJson.autoWidth == true) {
        autoWidth = true;
    }

    var multiboxonly = true;
    var multikey = "";
    if (GridJson.multiboxonly == false) {
        multiboxonly = false;

    }
    if (!isEmpty(GridJson.multikey)) {
        multikey = GridJson.multikey;
    }
    var storeCondition = {};
    if (!isEmpty(GridJson.storeCondition)) {
        if (typeof (GridJson.storeCondition) == 'object') {
            storeCondition = GridJson.storeCondition;
        } else {
            storeCondition = { condition: GridJson.storeCondition };
        }
    }

    var _grid = jQuery("#" + dataGridId).jqGrid({
        url: GridJson.storeURL,
        datatype: "json",
        mtype: 'POST',
        width: GridJson.width,
        treeGrid: GridJson.treeGrid,
        treeGridModel: GridJson.treeGridModel,
        ExpandColumn: GridJson.expandColumn,
        ExpandColClick: GridJson.expandColClick,
        hidegrid: GridJson.hidegrid,
        multiboxonly: multiboxonly,
        multikey: multikey,
        autowidth: autoWidth,
        height: GridJson.height,
        colModel: GridJson.initArray,
        //loadui:'block',
        loadBeforeSend: function (xhr, settings) {
            if (!autoLoad) {
                autoLoad = true;
                return false;
            }
        },
        /*  ajaxGridOptions: {
        //请求之前判断首次是否加载
        beforeSend: function (e) {
        if (!autoLoad) {
        e.abort();
        autoLoad = true;
        return false;
        }
        }
        },*/
        forceFit: true,
        shrinkToFit: false,
        cellEdit: cellEditAble,
        cellurl: GridJson.editSaveUrl,
        cellsubmit: cellSubmit,
        afterSubmitCell: function (ajax, id, nm, v, iRow, iCol) {
            try {
                var json = eval("(" + ajax.responseText + ")");
                //编辑回调函数处理
                var cellEditCallBack = eventFunc['afterSubmitCell'];
                if (cellEditCallBack != undefined && jQuery.isFunction(cellEditCallBack)) {
                    jQuery(cellEditCallBack(json));
                }
                return [json.Result, json.Message];
            } catch (e) {
                return [false, "操作失败，请求异常！"];
            }
        },
        beforeEditCell: function (rowid, cellname, value, iRow, iCol) {
            var cellEditCallBack = eventFunc['beforeEditCell'];
            if (cellEditCallBack != undefined && jQuery.isFunction(cellEditCallBack)) {
                var record = jQuery("#" + dataGridId).jqGrid('getRowData', rowid);
                jQuery(cellEditCallBack(rowid, record, cellname, value, iRow, iCol));
            }
        },
        beforeSaveCell: function (rowid, cellname, value, iRow, iCol) {
            var cellEditCallBack = eventFunc['beforeSaveCell'];
            if (cellEditCallBack != undefined && jQuery.isFunction(cellEditCallBack)) {
                var record = jQuery("#" + dataGridId).jqGrid('getRowData', rowid);
                jQuery(cellEditCallBack(rowid, record, cellname, value, iRow, iCol));
            }
        },
        afterSaveCell: function (rowid, cellname, value, iRow, iCol) {
            var cellEditCallBack = eventFunc['afterSaveCell'];
            if (cellEditCallBack != undefined && jQuery.isFunction(cellEditCallBack)) {
                var record = jQuery("#" + dataGridId).jqGrid('getRowData', rowid);
                jQuery(cellEditCallBack(rowid, record, cellname, value, iRow, iCol));
            }
        },
        postData: storeCondition,
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: GridJson.primaryKey
        },
        altclass: GridJson.altclass,
        altRows: GridJson.altRows,
        rowNum: pageSize,
        multiselect: multiSelect,
        rowList: [10, 20, 30, 50, 100],
        pager: pageBarId,
        subGrid: GridJson.subGrid,
        subGridModel: GridJson.subGridModel,
        subGridUrl: GridJson.subGridUrl,
        subGridRowExpanded: GridJson.subGridRowExpanded,
        sortname: sortByField,
        viewrecords: true,
        emptyrecords: GridJson.emptyrecords,
        grouping: groupingBool,
        groupingView: groupViewConfig,
        sortable: GridJson.columnReOrder,
        sortorder: GridJson.sortOrder,
        caption: GridJson.title,
        onSelectRow: this.onSelectRow,
        gridview: GridJson.gridview,
        scroll: GridJson.scroll,
        hoverrows: GridJson.hoverrows,
        footerrow: GridJson.footerrow,
        userDataOnFooter: GridJson.userDataOnFooter,
        ondblClickRow: this.onDBClickRow,
        onCellSelect: this.onCellClick
        , beforeSelectRow: this.beforeSelectRow
        , rownumbers: GridJson.rowNumbers
        , loadonce: GridJson.loadonce
        , treeReader: {
            leaf_field: "leaf"
        }
        , gridComplete: this.onGridComplete
    });

//    //表头合并
//    jQuery("#" + dataGridId).jqGrid('setGroupHeaders', {
//        useColSpanStyle: true,
//        groupHeaders: [
//                        { startColumnName: 'D7_1', numberOfColumns: 3, titleText: '<em>7d</em>' },
//                        { startColumnName: 'D28_1', numberOfColumns: 3, titleText: '28d' },
//                        { startColumnName: 'S7_1', numberOfColumns: 3, titleText: '<em>7S</em>' },
//                        { startColumnName: 'S28_1', numberOfColumns: 3, titleText: '28S' }
//            ]
//    });

    //附加搜索工具栏
    if (!isEmpty(pageBarId)) {
        jQuery("#" + dataGridId).jqGrid('navGrid', pageBarId, { edit: false, add: false, del: false, search: false, refresh: GridJson.toolbarRefresh });
        var showSearchToolBar = false;
        if (GridJson.toolbarSearch) {
            jQuery("#" + dataGridId).jqGrid('navButtonAdd', pageBarId, { caption: "查询", buttonicon: 'ui-icon ui-icon-search', onClickButton: function () {
                if (showSearchToolBar) {
                    jQuery("#" + dataGridId)[0].toggleToolbar();
                } else {
                    $("#" + dataGridId).jqGrid('filterToolbar', { stringResult: true });
                    jQuery("#" + dataGridId)[0].clearToolbar();
                    showSearchToolBar = true;
                }
            }
            });
        }
        if (GridJson.excelExport) {
            jQuery("#" + dataGridId).jqGrid('navButtonAdd', pageBarId, { caption: "导出", buttonicon: 'ui-icon ui-icon-disk', onClickButton: function () {
                jQuery("#" + dataGridId).jqGrid('excelExport', { url: "excel" });
            }
            });
        }
        if (GridJson.advancedSearch) {
            jQuery("#" + dataGridId).jqGrid('navButtonAdd', pageBarId, { caption: "高级查询", buttonicon: 'ui-icon ui-icon-search', onClickButton: function () {
                jQuery("#" + dataGridId).jqGrid('searchGrid', { multipleSearch: true, showQuery: true });

            }
            });
        }
        
    }

    var buttonRenderDiv = GridJson.buttonRenderTo;
    //系统按钮处理
    if (!isEmpty(GridJson.buttons)) {
        var length = GridJson.buttons.length;
        //jQuery("#" + dataGridId).jqGrid('navSeparatorAdd', pageBarId);
        for (var i = 0; i < length; i++) {
            var funcId = GridJson.buttons[i].ID;
            var funcName = GridJson.buttons[i].FuncName;
            var funcDesc = GridJson.buttons[i].FuncDesc;
            var iconCls = GridJson.buttons[i].Icon;
            var handlerName = GridJson.buttons[i].HandlerName;

            //底部工具栏处理
            if (GridJson.buttons[i].ButtonPos == 1 && !isEmpty(pageBarId)) {

                jQuery("#" + dataGridId).jqGrid('navButtonAdd', pageBarId, { caption: funcName, buttonicon: iconCls, onClickButton: GridJson.functions[handlerName], title: funcDesc });
            } else {//顶部工具栏处理
                var button = $('<button type="button" title="' + funcDesc + '" value="' + funcId + '" class="tbtn ' + iconCls + '">' + funcName + '</button>');
                if (!isEmpty(buttonRenderDiv)) {
                    $("#" + buttonRenderDiv).append(button);
                } else {
                    $("#" + renderDiv + " ." + toobarClsName).append(button);
                }
                button.bind('click', GridJson.buttons[i], GridJson.functions[handlerName]);
            }
        }
    }

};