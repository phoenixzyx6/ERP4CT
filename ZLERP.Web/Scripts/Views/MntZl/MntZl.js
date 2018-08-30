function mntzlIndexInit(options) {
    var MntZlGrid = new MyGrid({
        renderTo: 'MntZlGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 700
            , dialogHeight: 300
		    , storeURL: options.storeUrl
        //, multikey: 'ctrlKey'
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '支领单号', name: 'ID', index: 'ID', width: 80, align: 'center' }
                , { label: '支领日期', name: 'ZlDate', index: 'ZlDate', formatter: 'date', width: 80, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '领用人ID', name: 'UserID', index: 'UserID', hidden: true }
                , { label: '领用人', name: 'TrueName', index: 'TrueName', width: 80, align: 'center' }
                , { label: '领用部门ID', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
                , { label: '领用部门', name: 'DepartmentName', index: 'DepartmentName', width: 100, align: 'center' }
                , { label: '设备大类ID', name: 'ClassBID', index: 'ClassBID', hidden: true }
                , { label: '设备大类', name: 'ClassBName', index: 'ClassBName', width: 100, align: 'center' }
                , { label: '设备中类ID', name: 'ClassMID', index: 'ClassMID', hidden: true }
                , { label: '设备中类', name: 'ClassMName', index: 'ClassMName', width: 100, align: 'center' }
                , { label: '设备细类ID', name: 'ClassSID', index: 'ClassSID', hidden: true }
                , { label: '设备细类', name: 'ClassSName', index: 'ClassSName', width: 100, align: 'center' }
                , { label: '设备名称ID', name: 'EquipmentID', index: 'EquipmentID', hidden: true }
                , { label: '设备名称', name: 'EquipmentName', index: 'EquipmentName' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus'} }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '审核人', name: 'Auditor', index: 'Auditor' }
                , { label: '审核意见', name: 'AuditInfo', index: 'AuditInfo' }
                , { label: '二次审核状态', name: 'ReAuditStatus', index: 'ReAuditStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus'} }
                , { label: '二次审核时间', name: 'ReAuditTime', index: 'ReAuditTime', formatter: 'date', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '二次审核人', name: 'ReAuditor', index: 'ReAuditor' }
                , { label: '二次审核意见', name: 'ReAuditInfo', index: 'ReAuditInfo' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    MntZlGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MntZlGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    MntZlGrid.handleAdd({
                        loadFrom: 'MntZlForm',
                        btn: btn,
                        formId: 'myMntZlForm',
                        afterFormLoaded: function () {

                            ClassMList.empty();
                            ClassSList.empty();
                            EquipList.empty();
                            MntZlGrid.refreshGrid('1=1');
                        }
                    });
                },
                handleEdit: function (btn) {


                    var data = MntZlGrid.getSelectedRecord();
                    var auditValue = data.ReAuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能修改');
                        return;
                    }
                    MntZlGrid.handleEdit({
                        loadFrom: 'MntZlForm',
                        btn: btn,
                        prefix: 'MntZl',
                        afterFormLoaded: function () {
                            var classbid = MntZlGrid.getSelectedRecord()["ClassBID"];
                            var classmid = MntZlGrid.getSelectedRecord()["ClassMID"];
                            var classsid = MntZlGrid.getSelectedRecord()["ClassSID"];
                            var equipid = MntZlGrid.getSelectedRecord()["EquipmentID"];
                            bindSelectData(
                                ClassMList,
                                options.listClassMUrl,
                                {
                                    textField: "ClassMName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + classbid + "' AND ClassID = '" + options.classTypeID + "'"
                                },
                                function () {
                                    ClassMList.val(classmid);
                                    ClassSList.empty();
                                }
                            );
                            bindSelectData(
                                    ClassSList,
                                        options.listClassSUrl,
                                        {
                                            textField: "ClassSName",
                                            valueField: "ID",
                                            condition: "ClassBID ='" + classbid + "' AND ClassMID='" + classmid + "' AND  ClassID = '" + options.classTypeID + "'"
                                        },
                                        function () {
                                            ClassSList.val(classsid);
                                        }
                            );
                            bindSelectData(
                                EquipList,
                                options.listEquipUrl,
                                {
                                    textField: "EquipmentName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + classbid + "'"
                                },
                                function () {
                                    EquipList.val(equipid);
                                }
                            );
                        }
                    });
                }
                , handleDelete: function (btn) {

                    var data = MntZlGrid.getSelectedRecord();
                    var auditValue = data.ReAuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能删除');
                        return;
                    }
                    MntZlGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //审核
                , handleAudit: function (btn) {
                    var ids = MntZlGrid.getSelectedKeys();
                    if (ids.length == 0) {
                        showMessage('提示', '请选择审核的记录！');
                        return;
                    } else {

                        //若只选取一条，则单独审核，弹出审核框
                        if (ids.length == 1) {
                            var auditValue = MntZlGrid.getSelectedRecord().AuditStatus;
                            if (auditValue == 1) {
                                showMessage('提示', '该记录已审核！');
                                return;
                            }
                            MntZlGrid.handleEdit({
                                btn: btn,
                                width: 350,
                                height: 250,
                                prefix: 'MntZl',
                                loadFrom: 'AuditForm',
                                afterFormLoaded: function () {
                                    $("#MntZl_AuditStatus").val(1);

                                }
                            });
                        } else { //批量审核，不填写审核意见，且都为审核通过
                            ajaxRequest("/MntZl.mvc/BatchAudit", { ids: ids }, true, function (response) {
                                MntZlGrid.refreshGrid("1=1");
                            });

                        }

                    }

                }
                //经理审核
                , handleManageAudit: function (btn) {
                    if (!MntZlGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    var auditValue = MntZlGrid.getSelectedRecord().AuditStatus;
                    if (auditValue != 1) {
                        showMessage('提示', '该记录还未审核或审核不通过！');
                        return;
                    }
                    var ReAuditStatus = MntZlGrid.getSelectedRecord().ReAuditStatus;
                    if (ReAuditStatus == 1) {
                        showMessage('提示', '该记录已审核！');
                        return;
                    }
                    MntZlGrid.handleEdit({
                        btn: btn,
                        width: 350,
                        height: 250,
                        prefix: 'MntZl',
                        loadFrom: 'reAuditForm',
                        afterFormLoaded: function () {
                            MntZlGrid.refreshGrid('1=1');

                        }
                    });
                }
                //查看已审核的支领记录
                , handleViewChecked: function () {
                    MntZlGrid.refreshGrid("AuditStatus = 1");
                }
                //查看未审核的支领记录
                , handleViewUnchecked: function (btn) {
                    MntZlGrid.refreshGrid("AuditStatus != 1");
                }
            }
    });
    window.NextStep = function () {
        $.validator.unobtrusive.parse(document);
        if (!$("#myMntZlForm").valid()) {
            var valMsg = $('[data-valmsg-summary=true]');
            if (valMsg.length > 0) {
                valMsg.hide();
                showError('验证错误', valMsg.html());
            }
            return;
        }
        var postData = $("#myMntZlForm").serialize();
        var classBidEquip = $("#MntZl_ClassBID").val();
        var MaintenanceList = $("#MntZlItem_MaintenanceID");
        ajaxRequest("/MntZl.mvc/Add", postData, false, function (response) {
            if (!response.Result) {
                showError("出错啦！", response.Message);
                return false;
            }
            $("#MntZlForm").dialog("close");
            MntZlGrid.refreshGrid();
            MntZlItemGrid.showWindow({
                title: '添加支领明细',
                width: 500,
                height: 300,
                loadFrom: 'MntZlItemForm',
                afterLoaded: function () {
                    var zlid = response.Data;
                    $("#MntZlItem_MntZlID").val(zlid);
                    bindSelectData(
                        MaintenanceList,
                        options.listMaintenanceUrl,
                        {
                            textField: "ItemName",
                            valueField: "ID",
                            condition: "ClassBID ='" + classBidEquip + "'"
                        },
                        function () {

                        }
                    );
                },
                buttons: {
                    '关闭': function () {
                        $(this).dialog("close");
                    },
                    '保存': function () {
                        var _zlid = $("#MntZlItem_MntZlID").val();
                        $.post(
                            options.itemAddUrl,
                            $("#MntZlItemForm form").serialize(),
                            function (data) {
                                if (!data.Result) {
                                    showError("出错啦！", data.Message);
                                    return false;
                                }
                                $("#MntZlItemForm form")[0].reset();
                                $("#MntZlItemForm").dialog("close");
                                MntZlItemGrid.refreshGrid("MntZlID='" + _zlid + "'");
                            }
                        );
                    },
                    '保存继续': function () {
                        var _zlid = $("#MntZlItem_MntZlID").val();
                        $.post(
                            options.itemAddUrl,
                            $("#MntZlItemForm form").serialize(),
                            function (data) {
                                if (!data.Result) {
                                    showError("出错啦！", data.Message);
                                    return false;
                                }
                                $("#MntZlItemForm form")[0].reset();
                                $("#MntZlItem_MntZlID").val(_zlid);
                                MntZlItemGrid.refreshGrid("MntZlID='" + _zlid + "'");
                            }
                        );
                    }
                }
            });

        });
    };

    MntZlGrid.addListeners("rowclick", function (id, record, selBool) {
        $("#sbbid").val(record.ClassBID);
        MntZlItemGrid.refreshGrid("MntZlID='" + id + "'");
    });
    //支领明细数据列表
    var MntZlItemGrid = new MyGrid({
        renderTo: 'MntZlItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
            , dialogWidth: 500
            , dialogHeight: 300
		    , storeURL: options.itemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , singleSelect: true
            , rowNumbers: true
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID', hidden: true }
                , { label: '支领单号', name: 'MntZlID', index: 'MntZlID', hidden: true }
                , { label: '领用项目ID', name: 'MaintenanceID', index: 'MaintenanceID', hidden: true }
                , { label: '领用项目', name: 'ItemName', index: 'ItemName', width: 120 }
                , { label: '零件ID', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '零件', name: 'PartName', index: 'PartName' }
                , { label: '支领数量', name: 'amount', index: 'amount', width: 60 }
                , { label: '零件大类ID', name: 'ClassBID', index: 'ClassBID', hidden: true }
                , { label: '零件大类', name: 'ClassBName', index: 'ClassBName' }
                , { label: '零件中类ID', name: 'ClassMID', index: 'ClassMID', hidden: true }
                , { label: '零件中类', name: 'ClassMName', index: 'ClassMName' }
                , { label: '零件细类ID', name: 'ClassSID', index: 'ClassSID', hidden: true }
                , { label: '零件细类', name: 'ClassSName', index: 'ClassSName' }
                , { label: '用途厂牌', name: 'PurposeMill', index: 'PurposeMill' }
                , { label: '单位', name: 'Unit', index: 'Unit', width: 60 }
                , { label: '是否考核', name: 'IsAssess', index: 'IsAssess', width: 60, align:'center', formatter: booleanFmt, unformat: booleanUnFmt}
                , { label: '备注', name: 'Remark', index: 'Remark', width: 180 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    MntZlItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MntZlItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!MntZlGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请先选择一个支领单号！');
                        return;
                    }
                    var data = MntZlGrid.getSelectedRecord();
                    var auditValue = data.ReAuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能再新增');
                        return;
                    }

                    var selectrecord = MntZlGrid.getSelectedRecord();
                    var MaintenanceList = $("#MntZlItem_MaintenanceID");
                    MntZlItemGrid.handleAdd({
                        loadFrom: 'MntZlItemForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#MntZlItem_MntZlID").val(selectrecord.ID);
                            
                            //绑定领用项目数据
                            bindSelectData(
                                MaintenanceList,
                                options.listMaintenanceUrl,
                                {
                                    textField: "ItemName",
                                    valueField: "ID",
                                    condition: "ClassBID ='" + selectrecord.ClassBID + "'"
                                },
                                function () {
                                    
                                }
                            );
                        }
                    });
                },
                    handleEdit: function (btn) {

                        var data = MntZlGrid.getSelectedRecord();
                        var auditValue = data.ReAuditStatus;
                        if (auditValue == 1) {
                            showMessage('提示', '已审核的数据不能修改');
                            return;
                        }
                    var MaintenanceList = $("#MntZlItem_MaintenanceID");
                    MntZlItemGrid.handleEdit({
                        loadFrom: 'MntZlItemForm',
                        prefix: 'MntZlItem',
                        btn: btn,
                        afterFormLoaded: function () {
                            var MaintenanceID = MntZlItemGrid.getSelectedRecord()["MaintenanceID"];
                            bindSelectData(
                                MaintenanceList,
                                options.listMaintenanceUrl,
                                {
                                    textField: "ItemName",
                                    valueField: "ID",
                                    condition: "ClassBID ='" + $("#sbbid").val() + "'"
                                },
                                function () {
                                    $("#MntZlItem_MaintenanceID").val(MaintenanceID);
                                }
                            );
                        }
                    });
                }
                , handleDelete: function (btn) {

                    var data = MntZlGrid.getSelectedRecord();
                    var auditValue = data.ReAuditStatus;
                    if (auditValue == 1) {
                        showMessage('提示', '已审核的数据不能删除');
                        return;
                    }
                    MntZlItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //类别change事件绑定（设备）
    var ClassBList = $("#MntZl_ClassBID");
    var ClassMList = $("#MntZl_ClassMID");
    var ClassSList = $("#MntZl_ClassSID");
    var EquipList = $("#MntZl_EquipmentID");
    ClassBList.bind("change", function (e) {
        var bid = $(this).val();
        if (bid) {
            //中类数据绑定
            bindSelectData(
                ClassMList,
                options.listClassMUrl,
                {
                    textField: "ClassMName",
                    valueField: "ID",
                    condition: "ClassBID='" + bid + "' AND ClassID = '" + options.classTypeID + "'"
                },
                function () {
                    ClassSList.empty();
                }
            );
            //设备数据绑定
            bindSelectData(
                EquipList,
                options.listEquipUrl,
                {
                    textField: "EquipmentName",
                    valueField: "ID",
                    condition: "ClassBID='" + bid + "'"
                },
                function () {
                    //ClassSList.empty();
                }
            );
        } else {
            ClassMList.empty();
        }
    });
    ClassMList.bind("change", function () {
        _bid = ClassBList.val();
        mid = $(this).val();
        if (mid) {
            bindSelectData(
                ClassSList,
                options.listClassSUrl,
                {
                    textField: "ClassSName",
                    valueField: "ID",
                    condition: "ClassBID ='" + _bid + "' AND ClassMID='" + mid + "' AND  ClassID = '" + options.classTypeID + "'"
                },
                function () {

                }
            );

        } else {
            ClassSList.empty();
        }
    });

    //类别change事件绑定（零件）
    var ClassBList_Part = $("#MntZlItem_ClassBID");
    var ClassMList_Part = $("#MntZlItem_ClassMID");
    var ClassSList_Part = $("#MntZlItem_ClassSID");
    var List_Part = $("#MntZlItem_PartID");
    ClassBList_Part.bind("change", function () {
        var bid_part = $(this).val();
        if (bid_part) {
            bindSelectData(
                ClassMList_Part,
                options.listClassMUrl,
                {
                    textField: "ClassMName",
                    valueField: "ID",
                    condition: "ClassBID='" + bid_part + "' AND ClassID = '" + options.p1 + "'"
                },
                function () {
                    ClassSList_Part.val("");
                }
            );
            bindSelectData(
                List_Part,
                options.listPartInfoUrl,
                {
                    textField: "PartName",
                    valueField: "ID",
                    condition: "GreatClassID='" + bid_part + "'"
                },
                function () {
                    //ClassSList.empty();
                }
            );
        } else {
            ClassMList_Part.val("");
        }
    });
    //零件中类change事件
    ClassMList_Part.bind("change", function () {
        _bid_part = ClassBList_Part.val();
        mid_part = $(this).val();
        if (mid_part) {
            bindSelectData(
                ClassSList_Part,
                options.listClassSUrl,
                {
                    textField: "ClassSName",
                    valueField: "ID",
                    condition: "ClassBID ='" + _bid_part + "' AND ClassMID='" + mid_part + "' AND  ClassID = '" + options.p1 + "'"
                },
                function () {

                }
            );
            //绑定零件数据
            var searchpartcondi = isEmpty(_bid_part) ? "MiddClassID='" + mid_part + "'" : "GreatClassID='" + _bid_part + "' AND MiddClassID='" + mid_part + "'";
            bindSelectData(
                List_Part,
                options.listPartInfoUrl,
                {
                    textField: "PartName",
                    valueField: "ID",
                    condition: searchpartcondi
                },
                function () {
                    //ClassSList.empty();
                }
            );
        } else {
            ClassSList_Part.val("");
        }
    });

    /*直接选择零件时
    * 1、取得零件的大中小类型，然后填充大中小类型的值
    * 2、获取零件的库存
    /*/
    List_Part.change(function () {
        var selectedVal = $(this).val();
        if (selectedVal) {
            ajaxRequest(
                "/PartInfo.mvc/GetPartInfo",
                {
                    Id: selectedVal
                },
                false,
                function (response) {
                    if (!isEmpty(response)) {
                        var data = response;
                        ClassBList_Part.val(data.GreatClassID);
                        ClassMList_Part.val(data.MiddClassID);
                        ClassSList_Part.val(data.MinClassID);
                    } else {
                        showError("错误：", "该零件不存在！");
                        return false;
                    }

                }
            );
        }
    });

}