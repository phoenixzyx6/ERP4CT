function equipmtlyIndexInit(opts) {
    var EquipMtLyGrid = new MyGrid({
        renderTo: 'EquipMtLyGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
             , dialogWidth: 500
            , dialogHeight: 310
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '维修单号', name: 'ID', index: 'ID', width: 80, align: 'center' }
                , { label: '维修日期', name: 'MtDate', index: 'MtDate', formatter: 'date', width: 80, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '支领单号', name: 'MntZlID', index: 'MntZlID', width: 80, align: 'center', hidden: true }
                , { label: '设备大类', name: 'ClassBName', index: 'ClassBName', width: 100 }
                , { label: '设备大类', name: 'ClassBID', index: 'ClassBID', width: 100, hidden: true }
                , { label: '设备中类', name: 'ClassMName', index: 'ClassMName', width: 100 }
                , { label: '设备中类', name: 'ClassMID', index: 'ClassMID', width: 100, hidden: true }
                , { label: '设备细类', name: 'ClassSName', index: 'ClassSName', width: 100 }
                , { label: '设备细类', name: 'ClassSID', index: 'ClassSID', width: 100, hidden: true }
                , { label: '设备名称', name: 'EquipmentName', index: 'EquipmentName', width: 100 }
                , { label: '设备名称', name: 'EquipmentID', index: 'EquipmentID', width: 100, hidden: true }
                , { label: '胎号', name: 'TyreID', index: 'TyreID' }
                , { label: '是否委外', name: 'IsEntrust', index: 'IsEntrust', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '金额', name: 'Sumx', index: 'Sumx', width: 50, align: 'center', hidden: true }
                , { label: '合同/工程', name: 'ContractName', index: 'ContractName', width: 120 }
                , { label: '合同/工程', name: 'ContractID', index: 'ContractID', width: 120, hidden: true }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '年/月', name: 'YM', index: 'YM', formatter: 'date', hidden: true }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    EquipMtLyGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipMtLyGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    EquipMtLyGrid.handleAdd({
                        loadFrom: 'EquipMtLyForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#actiontype").val("add");
                            $("#EquipMtLy_MntZlID").attr("disabled", false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = EquipMtLyGrid.getSelectedRecord();
                    EquipMtLyGrid.handleEdit({
                        loadFrom: 'EquipMtLyForm',
                        btn: btn,
                        prefix: 'EquipMtLy',
                        afterFormLoaded: function () {
                            $("#actiontype").val("edit");
                            $("#EquipMtLy_MntZlID").attr("disabled", true);
                            //获取中小类
                            //中类数据绑定
                            bindSelectData(
                                ClassMList,
                                opts.listClassMUrl,
                                {
                                    textField: "ClassMName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + data.ClassBID + "' AND ClassID = '" + opts.classTypeID + "'"
                                },
                                function () {
                                    ClassMList.val(data.ClassMID);
                                    bindSelectData(
                                        ClassSList,
                                        opts.listClassSUrl,
                                        {
                                            textField: "ClassSName",
                                            valueField: "ID",
                                            condition: "ClassBID ='" + data.ClassBID + "' AND ClassMID='" + data.ClassMID + "' AND  ClassID = '" + opts.classTypeID + "'"
                                        },
                                        function () {
                                            ClassSList.val(data.ClassSID);
                                            //取得设备数据
                                            //设备数据绑定
                                            bindSelectData(
                                                EquipList,
                                                opts.listEquipUrl,
                                                {
                                                    textField: "EquipmentName",
                                                    valueField: "ID",
                                                    condition: "ClassBID='" + data.ClassBID + "' and ClassMID " + (isEmpty(data.ClassMID) ? "is null" : "='" + data.ClassMID + "'") + " and ClassSID " + (isEmpty(data.ClassSID) ? "is null" : "='" + data.ClassSID + "'") + ""
                                                },
                                                function () {
                                                    EquipList.val(data.EquipmentID);
                                                }
                                            );
                                        }
                                    );
                                }
                            );
                        }
                    });
                }
                , handleDelete: function (btn) {
                    EquipMtLyGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        EquipMtLyGrid.addListeners('rowclick', function (id, record, selBool) {
            EquipMtLyItemGrid.refreshGrid("EquipMtLyID='" + id + "'");
            var _data = EquipMtLyGrid.getSelectedRecord();
            $("#classbid").val(_data.ClassBID);
        });
    //类别change事件绑定（设备）
    var MntZlIDList = $("#EquipMtLy_MntZlID"); //支领单号
    var ClassBList = $("#EquipMtLy_ClassBID");
    var ClassMList = $("#EquipMtLy_ClassMID");
    var ClassSList = $("#EquipMtLy_ClassSID");
    var EquipList = $("#EquipMtLy_EquipmentID");
    //支领单号change事件
    MntZlIDList.bind("change", function (e) {
        var mntzlid = $(this).val();
        if (mntzlid) {
            //1、先从支领主表中查询设备大、中、小类编号及设备编号 xyl
            ajaxRequest(
                    opts.getMntZlByIdUrl,
                    { id: mntzlid },
                    false,
                    function (response) {
                        if (response.Result) {
                            var data = response.Data;
                            var classbid = data.ClassBID;
                            var classmid = data.ClassMID;
                            var classsid = data.ClassSID;
                            var equipid = data.EquipmentID;
                            ClassBList.val(classbid);
                            //2、根据大类编号，获取中类数据，并设定中类 xyl
                            bindSelectData(
                                ClassMList,
                                opts.listClassMUrl,
                                {
                                    textField: "ClassMName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + classbid + "' AND ClassID = '" + opts.classTypeID + "'"
                                },
                                function () {
                                    ClassMList.val(classmid); //设定中类值 xyl
                                    //3、根据大类、中类编号，获取细类，并设定细类 xyl
                                    ClassSList.empty();
                                    bindSelectData(
                                        ClassSList,
                                        opts.listClassSUrl,
                                        {
                                            textField: "ClassSName",
                                            valueField: "ID",
                                            condition: "ClassBID ='" + classbid + "' AND ClassMID='" + classmid + "' AND  ClassID = '" + opts.classTypeID + "'"
                                        },
                                        function () {
                                            ClassSList.val(classsid); //设定细类值 xyl
                                            //4、根据大类、中类、细类编号，获取设备，并设定设备 xyl
                                            EquipList.empty();
                                            bindSelectData(
                                                EquipList,
                                                opts.listEquipUrl,
                                                {
                                                    textField: "EquipmentName",
                                                    valueField: "ID",
                                                    condition: "ClassBID='" + classbid + "' and ClassMID='" + classmid + "' and ClassSID " + (isEmpty(classsid) ? "is null" : "='" + classsid + "'") + ""
                                                },
                                                function () {
                                                    EquipList.val(equipid); //设定设备值 xyl
                                                }
                                            );

                                        }
                                    );


                                }
                            );
                        }
                    }
                );
        }
    });
    ClassBList.bind("change", function (e) {
        var bid = $(this).val();
        if (bid) {
            //中类数据绑定
            bindSelectData(
                ClassMList,
                opts.listClassMUrl,
                {
                    textField: "ClassMName",
                    valueField: "ID",
                    condition: "ClassBID='" + bid + "' AND ClassID = '" + opts.classTypeID + "'"
                },
                function () {

                    ClassSList.empty();


                }
            );

        } else {
            EquipList.empty();
            ClassMList.empty();
        }
    });
    ClassMList.bind("change", function () {
        _bid = ClassBList.val();
        mid = $(this).val();
        if (mid) {
            bindSelectData(
                ClassSList,
                opts.listClassSUrl,
                {
                    textField: "ClassSName",
                    valueField: "ID",
                    condition: "ClassBID ='" + _bid + "' AND ClassMID='" + mid + "' AND  ClassID = '" + opts.classTypeID + "'"
                },
                function () {

                }
            );

        } else {
            EquipList.empty();
            ClassSList.empty();
        }
    });

    ClassSList.bind("change", function () {
        _bid = ClassBList.val();
        _mid = ClassMList.val();
        sid = $(this).val();
        //设备数据绑定
        bindSelectData(
            EquipList,
            opts.listEquipUrl,
            {
                textField: "EquipmentName",
                valueField: "ID",
                condition: "ClassBID='" + _bid + "' and ClassMID='" + _mid + "' and ClassSID " + (isEmpty(sid) ? "is null" : "='" + sid + "'") + ""
            },
            function () {
                //ClassSList.empty();
            }
        );
    });
    //导入支领明细
    window.importZlItems = function () {
        //先保存维修领用主表
        //判断是新增还是修改
        var actiontype = $("#actiontype").val();
        var mntzlid = $("#EquipMtLy_MntZlID").val();
        var actionurl;
        actionurl = actiontype == "add" ? opts.equipMtLyAddUrl : opts.equipMtLyUpdateUrl;
        ajaxRequest(
            actionurl,
            $("#EquipMtLyForm form").serialize(),
            false,
            function (data) {
                if (!data.Result) {
                    showError("出错啦！", data.Message);
                    return;
                } else {
                    $("#EquipMtLyForm").dialog("close");
                    EquipMtLyGrid.refreshGrid('1=1');
                    var EquipMtLyID = actiontype == "add" ? data.Data : $("#EquipMtLy_ID").val();
                    EquipMtLyItemGrid.showWindow({
                        title: '导入支领明细',
                        width: 900,
                        height: 300,
                        loadFrom: 'ImportMntZlItemForm',
                        afterLoaded: function () {
                            MntZlItemGrid.refreshGrid("MntZlID='" + mntzlid + "' and MntZlItemID not in (select MntZlItemID from EquipMtLyItem)");
                        },
                        buttons:
                        { "关闭": function () {
                            $(this).dialog("close");
                        }, "确定": function () {
                            var ids = MntZlItemGrid.getSelectedKeys();
                            if (ids.length <= 0) {
                                $(this).dialog("close");
                                return false;
                            } else {
                                ajaxRequest("/EquipMtLy.mvc/ImportEntitys", { ids: ids, equipmtlyid: EquipMtLyID }, true, function (data) {
                                    $("#ImportMntZlItemForm").dialog("close");
                                    EquipMtLyItemGrid.refreshGrid("EquipMtLyID='" + EquipMtLyID + "'");
                                });
                            }

                        }
                        }
                    });
                }
            }
        );
    };
    var EquipMtLyItemGrid = new MyGrid({
        renderTo: 'EquipMtLyItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
            , dialogWidth: 460
            , dialogHeight: 280
		    , storeURL: opts.itemStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID', hidden: true }
                , { label: '维修单号', name: 'EquipMtLyID', index: 'EquipMtLyID', hidden: true }
                , { label: '维修项目ID', name: 'MaintenanceID', index: 'MaintenanceID', hidden: true }
                , { label: '维修项目', name: 'MaintenanceName', index: 'MaintenanceName' }
                , { label: '零件ID', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '零件', name: 'PartName', index: 'PartName', hidden: true }
                , { label: '数量', name: 'Amount', index: 'Amount', width: 60, align: 'center', editable: true, editrules: { required: true} }
                , { label: '用途厂牌', name: 'PurposeMill', index: 'PurposeMill' }
                , { label: '领用部门ID', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
                , { label: '领用部门', name: 'DepartmentName', index: 'DepartmentName' }
                , { label: '领用人ID', name: 'UserID', index: 'UserID', width: 80, hidden: true }
                , { label: '领用人', name: 'TrueName', index: 'TrueName', width: 80 }
                , { label: '是否考核', name: 'IsAssess', index: 'IsAssess', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '支领明细单号', name: 'MntZlItemID', index: 'MntZlItemID', hidden: true }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    EquipMtLyItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipMtLyItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!EquipMtLyGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请在左侧列表选择一个维修单进行操作！');
                        return;
                    }
                    var selectrecord = EquipMtLyGrid.getSelectedRecord();
                    var mntzlid = selectrecord.MntZlID;
                    var EquipMtLyID = selectrecord.ID;
                    EquipMtLyItemGrid.showWindow({
                        title: '导入支领明细',
                        width: 900,
                        height: 300,
                        loadFrom: 'ImportMntZlItemForm',
                        afterLoaded: function () {
                            MntZlItemGrid.refreshGrid("MntZlID='" + mntzlid + "' and MntZlItemID not in (select MntZlItemID from EquipMtLyItem)");
                        },
                        buttons:
                        { "关闭": function () {
                            $(this).dialog("close");
                        }, "确定": function () {
                            var ids = MntZlItemGrid.getSelectedKeys();
                            if (ids.length <= 0) {
                                $(this).dialog("close");
                                return false;
                            } else {
                                ajaxRequest("/EquipMtLy.mvc/ImportEntitys", { ids: ids, equipmtlyid: EquipMtLyID }, true, function (data) {
                                    $("#ImportMntZlItemForm").dialog("close");
                                    EquipMtLyItemGrid.refreshGrid("EquipMtLyID='" + EquipMtLyID + "'");
                                });
                            }

                        }
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = EquipMtLyItemGrid.getSelectedRecord();
                    EquipMtLyItemGrid.handleEdit({
                        loadFrom: 'EquipMtLyItemForm',
                        prefix: 'EquipMtLyItem',
                        btn: btn,
                        afterFormLoaded: function () {
                            EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.EquipMtLyID', true);
                            // z EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.PartID', true);
                            //EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.Amount', true);
                            EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.IsAssess', true);
                            EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.UserID', true);
                            var ClassBID = $("#classbid").val();
                            //取得大类下面的保养数据
                            var MaintenanceList = $("#EquipMtLyItem_MaintenanceID");
                            bindSelectData(
                                MaintenanceList,
                                opts.listMaintenanceUrl,
                                {
                                    textField: "ItemName",
                                    valueField: "ID",
                                    condition: "ClassBID ='" + ClassBID + "'"
                                },
                                function () {
                                    MaintenanceList.val(data.MaintenanceID);
                                }
                            );
                        }
                    });
                }
                , handleDelete: function (btn) {
                    EquipMtLyItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                },
                handleImmediatelyAdd: function (btn) {
                    if (!EquipMtLyGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请在左侧列表选择一个维修单进行操作！');
                        return;
                    }
                    var selectrecord = EquipMtLyGrid.getSelectedRecord();
                    var mntzlid = selectrecord.ID;
                    EquipMtLyItemGrid.handleAdd({
                        loadFrom: 'AddEquipMtLyItemForm',
                        prefix: 'EquipMtLyItem',
                        btn: btn,
                        afterFormLoaded: function () {
                            EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.EquipMtLyID', false);
                            // z EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.PartID', false);
                            //EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.Amount', true);
                            EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.IsAssess', false);
                            EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.UserID', false);
                            EquipMtLyItemGrid.setFormFieldValue('EquipMtLyItem.EquipMtLyID', mntzlid);
                            EquipMtLyItemGrid.setFormFieldReadOnly('EquipMtLyItem.EquipMtLyID', true);
                            //EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.PartID', true);
                            //EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.Amount', true);
                            //EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.IsAssess', true);
                            //EquipMtLyItemGrid.setFormFieldDisabled('EquipMtLyItem.UserID', true);
                            var ClassBID = $("#classbid").val();
                            //取得大类下面的保养数据
                            //var MaintenanceList = $("#Add_EquipMtLyItem_MaintenanceID");
                            //                            bindSelectData(
                            //                                MaintenanceList,
                            //                                opts.listMaintenanceUrl,
                            //                                {
                            //                                    textField: "ItemName",
                            //                                    valueField: "ID",
                            //                                    condition: "ClassBID ='" + ClassBID + "'"
                            //                                }
                            //                            );
                        }
                    });
                }
            }
    });

    //支领明细
    var MntZlItemGrid = new MyGrid({
        renderTo: 'MntZlItemGrid'
            , autoWidth: true
        //, height: gGridHeight * 0.44
		    , storeURL: opts.MntZlItemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: false
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID', width:50, align: 'center' }
                , { label: '支领单号', name: 'MntZlID', index: 'MntZlID', hidden: true }
                , { label: '领用项目ID', name: 'MaintenanceID', index: 'MaintenanceID', hidden: true }
                , { label: '领用项目', name: 'ItemName', index: 'ItemName', width: 100 }
                , { label: '零件ID', name: 'PartID', index: 'PartID', hidden: true }
                , { label: '零件', name: 'PartName', index: 'PartName', width: 80 }
                , { label: '支领数量', name: 'amount', index: 'amount', width: 60 }
                , { label: '零件大类ID', name: 'ClassBID', index: 'ClassBID', hidden: true }
                , { label: '零件大类', name: 'ClassBName', index: 'ClassBName', width: 80 }
                , { label: '零件中类ID', name: 'ClassMID', index: 'ClassMID', hidden: true }
                , { label: '零件中类', name: 'ClassMName', index: 'ClassMName', width: 80 }
                , { label: '零件细类ID', name: 'ClassSID', index: 'ClassSID', hidden: true }
                , { label: '零件细类', name: 'ClassSName', index: 'ClassSName', width: 80 }
                , { label: '用途厂牌', name: 'PurposeMill', index: 'PurposeMill', width: 80 }
                , { label: '单位', name: 'Unit', index: 'Unit', width: 60 }
                , { label: '是否考核', name: 'IsAssess', index: 'IsAssess', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 80 }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    MntZlItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MntZlItemGrid.refreshGrid('1=1');
                }
            }
    });
}