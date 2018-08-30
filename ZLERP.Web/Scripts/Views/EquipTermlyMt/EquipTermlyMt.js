function equiptermlymtIndexInit(options) {
    var EquipTermlyMtGrid = new MyGrid({
        renderTo: 'EquipTermlyMtGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: 'allbutton'
            , height: gGridHeight
            , dialogWidth: 500
            , dialogHeight: 320
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '保养单号', name: 'ID', index: 'ID', width: 80, align: 'center' }
                , { label: '保养日期', name: 'MtDate', index: 'MtDate', formatter: 'date', width: 80, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '设备大类ID', name: 'ClassBID', index: 'ClassBID', hidden: true }
                , { label: '设备大类', name: 'ClassBName', index: 'ClassBName' }
                , { label: '设备中类ID', name: 'ClassMID', index: 'ClassMID', hidden: true }
                , { label: '设备中类', name: 'ClassMName', index: 'ClassMName' }
                , { label: '设备细类ID', name: 'ClassSID', index: 'ClassSID', hidden: true }
                , { label: '设备细类', name: 'ClassSName', index: 'ClassSName' }
                , { label: '设备名称ID', name: 'EquipmentID', index: 'EquipmentID', hidden: true }
                , { label: '设备名称', name: 'EquipmentName', index: 'EquipmentName' }
                , { label: '保养项目ID', name: 'MaintenanceID', index: 'MaintenanceID', hidden: true }
                , { label: '保养项目', name: 'ItemName', index: 'ItemName' }
                , { label: '应保养日期', name: 'BeMtDate', index: 'BeMtDate', formatter: 'date', width: 80, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '保养延迟原因', name: 'DelayReason', index: 'DelayReason' }
                , { label: '是否委外', name: 'IsEntrust', index: 'IsEntrust', width: 60, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '应付金额', name: 'PayableSum', index: 'PayableSum', width: 60 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '年/月', name: 'YM', index: 'YM', formatter: 'ym' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    EquipTermlyMtGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipTermlyMtGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    EquipTermlyMtGrid.handleAdd({
                        loadFrom: 'EquipTermlyMtForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    var data = EquipTermlyMtGrid.getSelectedRecord();
                    EquipTermlyMtGrid.handleEdit({
                        loadFrom: 'EquipTermlyMtForm',
                        btn: btn,
                        prefix: 'EquipTermlyMt',
                        afterFormLoaded: function () {

                            //获取中小类
                            //中类数据绑定
                            bindSelectData(
                                ClassMList,
                                options.listClassMUrl,
                                {
                                    textField: "ClassMName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + data.ClassBID + "' AND ClassID = '" + options.classTypeID + "'"
                                },
                                function () {
                                    ClassMList.val(data.ClassMID);
                                    bindSelectData(
                                        ClassSList,
                                        options.listClassSUrl,
                                        {
                                            textField: "ClassSName",
                                            valueField: "ID",
                                            condition: "ClassBID ='" + data.ClassBID + "' AND ClassMID='" + data.ClassMID + "' AND  ClassID = '" + options.classTypeID + "'"
                                        },
                                        function () {
                                            ClassSList.val(data.ClassSID);
                                        }
                                    );
                                }
                            );
                            //取得设备数据
                            //设备数据绑定
                            bindSelectData(
                                EquipList,
                                options.listEquipUrl,
                                {
                                    textField: "EquipmentName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + data.ClassBID + "' and ClassMID " + (isEmpty(data.ClassMID) ? "is null" : "='" + data.ClassMID + "'") + " and ClassSID " + (isEmpty(data.ClassSID) ? "is null" : "='" + data.ClassSID + "'") + ""
                                },
                                function () {
                                    EquipList.val(data.EquipmentID);
                                }
                            );
                            //取得大类下面的保养数据
                            bindSelectData(
                                MaintenanceList,
                                options.listMaintenanceUrl,
                                {
                                    textField: "ItemName",
                                    valueField: "ID",
                                    condition: "ClassBID ='" + data.ClassBID + "'"
                                },
                                function () {
                                    MaintenanceList.val(data.MaintenanceID);
                                }
                            );
                        }
                    });
                }
                , handleDelete: function (btn) {
                    EquipTermlyMtGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //类别change事件绑定（设备）
    var ClassBList = $("#EquipTermlyMt_ClassBID");
    var ClassMList = $("#EquipTermlyMt_ClassMID");
    var ClassSList = $("#EquipTermlyMt_ClassSID");
    var EquipList = $("#EquipTermlyMt_EquipmentID");
    var MaintenanceList = $("#EquipTermlyMt_MaintenanceID");
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

                    //设备数据绑定
                    bindSelectData(
                        EquipList,
                        options.listEquipUrl,
                        {
                            textField: "EquipmentName",
                            valueField: "ID",
                            condition: "ClassBID='" + bid + "' and ClassMID " + (isEmpty(ClassMList.val()) ? "is null" : "='" + ClassMList.val() + "'") + " and ClassSID " + (isEmpty(ClassSList.val()) ? "is null" : "='" + ClassSList.val() + "'") + ""
                        },
                        function () {

                        }
                    );
                }
            );
            //选择设备大类后立即查找保养项目
            bindSelectData(
                MaintenanceList,
                options.listMaintenanceUrl,
                {
                    textField: "ItemName",
                    valueField: "ID",
                    condition: "ClassBID ='" + bid + "'"
                },
                function () {

                }
            );
        } else {
            MaintenanceList.empty();
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
                options.listClassSUrl,
                {
                    textField: "ClassSName",
                    valueField: "ID",
                    condition: "ClassBID ='" + _bid + "' AND ClassMID='" + mid + "' AND  ClassID = '" + options.classTypeID + "'"
                },
                function () {
                    //设备数据绑定
                    bindSelectData(
                        EquipList,
                        options.listEquipUrl,
                        {
                            textField: "EquipmentName",
                            valueField: "ID",
                            condition: "ClassBID='" + _bid + "' and ClassMID='" + mid + "' and ClassSID " + (isEmpty(ClassSList.val()) ? "is null" : "='" + ClassSList.val() + "'") + ""
                        },
                        function () {
                            //ClassSList.empty();
                        }
                    );
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
            options.listEquipUrl,
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
    EquipTermlyMtGrid.addListeners("rowclick", function (id, record, selBool) {
        $("#EquipTermlyMtItem_EquipTermlyMtID").val(id);
        EquipTermlyMtItemGrid.refreshGrid("EquipTermlyMtID = '" + id + "'");
    });
    var EquipTermlyMtItemGrid = new MyGrid({
        renderTo: 'EquipTermlyMtItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , buttonRenderTo: 'itemsbutton'
            , height: gGridWithTitleHeight - 41
		    , storeURL: options.itemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , singleSelect: true
            , rowNumbers: false
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '保养单号', name: 'EquipTermlyMtID', index: 'EquipTermlyMtID', hidden: true }
                , { label: '领用零件ID', name: 'PartID', index: 'PartID', width: 100, hidden: true }
                , { label: '领用零件', name: 'PartName', index: 'PartName', width: 100 }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 77 }
                , { label: '数量', name: 'Amount', index: 'Amount', width: 77, align: 'center', editable: true, formatter: 'number', editrules: { number: true} }
                , { label: '金额', name: 'Sumx', index: 'Sumx', width: 77 }
                , { label: '部门ID', name: 'DepartmentID', index: 'DepartmentID', width: 100, hidden: true }
                , { label: '部门', name: 'DepartmentName', index: 'DepartmentName', width: 100, editable: true, edittype: 'select', editoptions: { dataUrl: '/Department.mvc/Departments' }, editrules: { required: true}, alias:'DepartmentID' }
                , { label: '领用人ID', name: 'UserID', index: 'UserID', width: 100, hidden: true }
                , { label: '领用人', name: 'TrueName', index: 'TrueName', width: 100, editable: true, edittype: 'select', editoptions: { dataUrl: '/User.mvc/Users' }, editrules: { required: true }, alias: 'UserID' }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 190 }
                , { label: '操作', name: 'myac', width: 70, fixed: true, sortable: false, resize: false, formatter: 'actions',
                    formatoptions: { keys: true, editbutton: false }
                }
		    ]
		    , autoLoad: false
            , editSaveUrl: options.itemUpdateUrl
            , functions: {
                handleReload: function (btn) {
                    EquipTermlyMtItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipTermlyMtItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    EquipTermlyMtItemGrid.handleAdd({
                        loadFrom: 'EquipTermlyMtItemForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    EquipTermlyMtItemGrid.handleEdit({
                        loadFrom: 'EquipTermlyMtItemForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    EquipTermlyMtItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleItemSave: function (btn) {
                    itemSave(btn);
                }
            }
    });

        EquipTermlyMtItemGrid.addListeners("gridComplete", function () {
            EquipTermlyMtItemGrid.getJqGrid().jqGrid('setGridParam', { editurl: options.itemDeleteUrl });
        });
        EquipTermlyMtItemGrid.addListeners("afterSubmitCell", function () {
            EquipTermlyMtItemGrid.reloadGrid();
        });
    window.itemSave = function (btn) {
        //先判断一下有没有选主表记录
        var EquipTermlyMtID = $("#EquipTermlyMtItem_EquipTermlyMtID").val();
        if (isEmpty(EquipTermlyMtID)) {
            showMessage("提示:", "请先选择一个保养单号");
            return;
        }
        ajaxRequest(
            //options.itemAddUrl,
            btn.data.Url,
            $("#EquipTermlyMtItemForm form").serialize(),
            true,
            function (data) {
                $("#EquipTermlyMtItemForm form")[0].reset();
                $("#EquipTermlyMtItem_EquipTermlyMtID").val(EquipTermlyMtID);
                EquipTermlyMtItemGrid.refreshGrid("EquipTermlyMtID='" + EquipTermlyMtID + "'");
            }
        );
        };
        $("#EquipTermlyMtItem_Amount").blur(function (e) {
            $("#EquipTermlyMtItem_Sumx").val($("#EquipTermlyMtItem_UnitPrice").val() * (isNaN(parseFloat($("#EquipTermlyMtItem_Amount").val())) ? 0 : parseFloat($("#EquipTermlyMtItem_Amount").val())));
        })
}