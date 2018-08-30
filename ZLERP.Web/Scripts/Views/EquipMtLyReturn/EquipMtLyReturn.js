function equipmtlyreturnIndexInit(opts) {
    var EquipMtLyReturnGrid = new MyGrid({
        renderTo: 'EquipMtLyReturnGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 550
            , dialogHeight: 420
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '退回单号', name: 'ID', index: 'ID' }
                , { label: '退回日期', name: 'ReturnDate', index: 'ReturnDate', formatter: 'date' }
                , { label: '支领单号', name: 'MntZlID', index: 'MntZlID' }
                , { label: '设备大类', name: 'ClassBID', index: 'ClassBID' }
                , { label: '设备中类', name: 'ClassMID', index: 'ClassMID' }
                , { label: '设备细类', name: 'ClassSID', index: 'ClassSID' }
                , { label: '设备名称', name: 'EquipmentID', index: 'EquipmentID' }
                , { label: '是否委外', name: 'IsEntrust', index: 'IsEntrust' }
                , { label: '金额', name: 'Sumx', index: 'Sumx' }
                , { label: '应付金额', name: 'BeSumx', index: 'BeSumx' }
                , { label: '物资审批', name: 'MApprove', index: 'MApprove' }
                , { label: '部门审批', name: 'DApprove', index: 'DApprove' }
                , { label: '领料人', name: 'DrawMan', index: 'DrawMan' }
                , { label: '发料人', name: 'SendMan', index: 'SendMan' }
                , { label: '申请日期', name: 'ApplyDate', index: 'ApplyDate', formatter: 'date' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '年/月', name: 'YM', index: 'YM', formatter: 'date' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    EquipMtLyReturnGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipMtLyReturnGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    EquipMtLyReturnGrid.handleAdd({
                        loadFrom: 'EquipMtLyReturnForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    var data = EquipMtLyReturnGrid.getSelectedRecord();
                    EquipMtLyReturnGrid.handleEdit({
                        loadFrom: 'EquipMtLyReturnForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#actiontype").val("edit");
                            $("#MntZlID").attr("disabled", true);
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
                    EquipMtLyReturnGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //类别change事件绑定（设备）
    var MntZlIDList = $("#MntZlID"); //支领单号
    var ClassBList = $("#ClassBID");
    var ClassMList = $("#ClassMID");
    var ClassSList = $("#ClassSID");
    var EquipList = $("#EquipmentID");
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
        var mntzlid = $("#MntZlID").val();
        var actionurl;
        actionurl = actiontype == "add" ? opts.equipMtLyReturnAddUrl : opts.equipMtLyReturnUpdateUrl;
        ajaxRequest(
            actionurl,
            $("#EquipMtLyReturnForm form").serialize(),
            false,
            function (data) {
                if (!data.Result) {
                    showError("出错啦！", data.Message);
                    return;
                } else {
                    $("#EquipMtLyReturnForm").dialog("close");
                    EquipMtLyReturnGrid.refreshGrid('1=1');
                    var EquipMtLyReturnID = actiontype == "add" ? data.Data : $("#ID").val();
                    EquipMtLyItemGrid.showWindow({
                        title: '导入退回明细',
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
                                    EquipMtLyItemGrid.refreshGrid("EquipMtLyReturnID='" + EquipMtLyReturnID + "'");
                                });
                            }

                        }
                        }
                    });
                }
            }
        );
    };
    var EquipMtLyReturnItemGrid = new MyGrid({
        renderTo: 'EquipMtLyReturnItemGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: opts.itemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '序号', name: 'ID', index: 'ID' }
                , { label: '退回单号', name: 'EquipMtLyReturnID', index: 'EquipMtLyReturnID' }
                , { label: '零件', name: 'PartID', index: 'PartID' }
                , { label: '数量', name: 'Amount', index: 'Amount' }
                , { label: '部门', name: 'DepartmentID', index: 'DepartmentID' }
                , { label: '领用人', name: 'UserID', index: 'UserID' }
                , { label: 'MntZlItemID', name: 'MntZlItemID', index: 'MntZlItemID' }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    EquipMtLyReturnItemGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    EquipMtLyReturnItemGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    EquipMtLyReturnItemGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    EquipMtLyReturnItemGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    EquipMtLyReturnItemGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
}