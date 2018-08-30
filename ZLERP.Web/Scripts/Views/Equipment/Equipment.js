function equipmentIndexInit(options) {
    var EquipGrid = new MyGrid({
        renderTo: 'EquipGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 800
            , dialogHeight: 300
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '设备编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '设备名称', name: 'EquipmentName', index: 'EquipmentName', width: 120 }
                , { label: '购置日期', name: 'PurchaseDate', index: 'PurchaseDate', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '设备大类编号', name: 'ClassBID', index: 'ClassBID', hidden: true }
                , { label: '设备大类', name: 'ClassB.ClassBName', index: 'ClassB.ClassBName' }
                , { label: '设备中类编号', name: 'ClassMID', index: 'ClassMID', hidden: true }
                , { label: '设备中类', name: 'ClassMName', index: 'ClassMName',sortable:false}
                , { label: '设备细类编号', name: 'ClassSID', index: 'ClassSID', hidden: true }
                , { label: '设备细类', name: 'Classs.ClassSName', index: 'Classs.ClassSName' }
                , { label: '厂商编号', name: 'SupplyID', index: 'SupplyID', hidden: true }
                , { label: '厂商', name: 'SupplyName', index: 'SupplyName', sortable: false }
                , { label: '厂牌', name: 'Brand', index: 'Brand', hidden: true }
                , { label: '部门编号', name: 'DepartmentID', index: 'DepartmentID', hidden: true }
                , { label: '部门', name: 'DepartmentName', index: 'DepartmentName', sortable: false }
                , { label: '规格', name: 'Spec', index: 'Spec' }
                , { label: '设备状态', name: 'EquipStatus', index: 'EquipStatus', width: 60 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    EquipGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    EquipGrid.handleAdd({
                        loadFrom: 'EquipForm',
                        btn: btn,
                        afterFormLoaded: function () {

                            ClassMList.empty();
                            ClassSList.empty();
                        }
                    });
                },
                handleEdit: function (btn) {
                    EquipGrid.handleEdit({
                        loadFrom: 'EquipForm',
                        btn: btn,
                        prefix: 'Equipment',
                        afterFormLoaded: function () {
                            var classbid = EquipGrid.getSelectedRecord()["ClassBID"];
                            var classmid = EquipGrid.getSelectedRecord()["ClassMID"];
                            var classsid = EquipGrid.getSelectedRecord()["ClassSID"];
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
                        }
                    });
                }
                , handleDelete: function (btn) {
                    EquipGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    EquipGrid.addListeners("rowclick", function (id, record, selBool) {
        EquipItemGrid.refreshGrid("EquipmentID ='" + id + "'");
        var eqrecord = EquipGrid.getSelectedRecord();
     /*   bindSelectData(
            $("select[name='EquipmentItem.MaintenanceID']"),
            options.listMaintenanceUrl,
            {
                textField: "ItemName",
                valueField: "ID",
                condition: "ClassBID ='" + eqrecord.ClassBID + "'"
            },
            function () {

            }
        );*/
    });
    var EquipItemGrid = new MyGrid({
        renderTo: 'EquipItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
            , dialogWidth: 400
            , dialogHeight: 300
		    , storeURL: options.itemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID', hidden: true }
                , { label: '设备代码', name: 'EquipmentID', index: 'EquipmentID', hidden: true }
                , { label: '项目代码', name: 'MaintenanceID', index: 'MaintenanceID', hidden:true }
                , { label: '保养项目', name: 'ItemName', index: 'ItemName', width: 150, sortable: false }
                , { label: '期限（天）', name: 'Days', index: 'Days', width:65 }
                , { label: '最近保养日', name: 'LatelyMaint', index: 'LatelyMaint', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '下次保养日期', name: 'NextTimeMaint', index: 'NextTimeMaint', formatter: 'date', width: 80, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    EquipItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!EquipGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请先选择一种设备！');
                        return;
                    }
                    var eqrecord = EquipGrid.getSelectedRecord();
                    EquipItemGrid.handleAdd({
                        loadFrom: 'EquipItemForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#EquipmentItem_EquipmentID").val(eqrecord.ID);
                            bindSelectData(
                                $("select[name='EquipmentItem.MaintenanceID']"),
                                options.listMaintenanceUrl,
                                {
                                    textField: "ItemName",
                                    valueField: "ID",
                                    condition: "ClassBID ='" + eqrecord.ClassBID + "'"
                                },
                                function () {

                                }
                            );
                        }
                    });
                },
                handleEdit: function (btn) {
                    EquipItemGrid.handleEdit({
                        loadFrom: 'EquipItemForm',
                        btn: btn,
                        prefix: 'EquipmentItem'
                    });
                }
                , handleDelete: function (btn) {
                    EquipItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    var ClassBList = $("select[name='Equipment.ClassBID']");
    var ClassMList = $("#Equipment_ClassMID");
    var ClassSList = $("#Equipment_ClassSID");
    ClassBList.unbind("change");
    ClassBList.bind("change", function (e) {
        var bid = $(this).val();
        if (bid) {
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
        } else {
            ClassMList.empty();
        }
    });
    ClassMList.unbind("change");
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
}