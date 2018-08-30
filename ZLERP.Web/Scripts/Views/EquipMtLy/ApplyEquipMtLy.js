function equipmtlyIndexInit(opts) {

    function planstateFmt(cellvalue, options, rowObject) {
        if (cellvalue == "1") {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "已提交";
            }

        }
        else if (cellvalue == "0") {
            var style = "color:red;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[0];
            } else {
                var txt = "未提交";
            }
        }
        else {
            var txt = '';
        }

        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function planstateUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }


    var EquipMtLyGrid = new MyGrid({
        renderTo: 'EquipMtLyGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
            , dialogWidth: 500
            , dialogHeight: 380
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '维修单号', name: 'ID', index: 'ID', width: 80, align: 'center' }
                , { label: '预估金额', name: 'summoney', index: 'summoney', width: 100 }
                , { label: '维修日期', name: 'MtDate', index: 'MtDate', formatter: 'date', width: 80, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '设备大类', name: 'ClassBName', index: 'ClassBName', width: 100 }
                , { label: '设备大类', name: 'ClassBID', index: 'ClassBID', width: 100, hidden: true }
                , { label: '设备中类', name: 'ClassMName', index: 'ClassMName', width: 100 }
                , { label: '设备中类', name: 'ClassMID', index: 'ClassMID', width: 100, hidden: true }
                , { label: '设备细类', name: 'ClassSName', index: 'ClassSName', width: 100 }
                , { label: '设备细类', name: 'ClassSID', index: 'ClassSID', width: 100, hidden: true }
                , { label: '设备名称', name: 'EquipmentName', index: 'EquipmentName', width: 100 }
                , { label: '设备名称', name: 'EquipmentID', index: 'EquipmentID', width: 100, hidden: true }
                , { label: '发现人', name: 'Finder', index: 'Finder', width: 100 }
                , { label: '发现时间', name: 'FindTime', index: 'FindTime', formatter: 'date', width: 100, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '申请人', name: 'ApplyMan', index: 'ApplyMan', width: 100 }
                , { label: '申请时间', name: 'ApplyTime', index: 'ApplyTime', formatter: 'date', width: 100, align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '故障描述', name: 'TroubleDes', index: 'TroubleDes' }
                , { label: '状态', name: 'mtlystate', index: 'mtlystate', formatter: planstateFmt, unformat: planstateUnFmt, align: 'center' }
		    ]
		    , autoLoad: true
            , functions: {
                handlesubmit: function (btn) {
                    var keys = EquipMtLyGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要提交的记录!");
                        return;
                    }
                    var record = EquipMtLyGrid.getSelectedRecord();
                    if (record.mtlystate == "0") {
                        ajaxRequest(btn.data.Url, { id: record.ID }, true, function (response) {
                            if (response.Result) {
                                EquipMtLyGrid.reloadGrid();
                            }
                        });
                    }
                    else {
                        showMessage('提示', "不能重复提交!");
                        return;
                    }
                },
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
                        }
                    });
                },
                handleEdit: function (btn) {
                    var data = EquipMtLyGrid.getSelectedRecord();

                    if (data.mtlystate != '0') {
                        showMessage('提示', "已经提交!");
                        return;
                    }

                    EquipMtLyGrid.handleEdit({
                        loadFrom: 'EquipMtLyForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            $("#actiontype").val("edit");
                            //获取中小类
                            //中类数据绑定
                            bindSelectData(
                                ClassMList,
                                opts.listClassMUrl,
                                {
                                    textField: "ClassMName",
                                    valueField: "ID",
                                    condition: "ClassBID='" + data.ClassBID + "' AND ClassID = 'EquipType'"
                                },
                                function () {
                                    ClassMList.val(data.ClassMID);
                                    bindSelectData(
                                        ClassSList,
                                        opts.listClassSUrl,
                                        {
                                            textField: "ClassSName",
                                            valueField: "ID",
                                            condition: "ClassBID ='" + data.ClassBID + "' AND ClassMID='" + data.ClassMID + "' AND  ClassID = 'EquipType'"
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
        var ClassBList = $("#ClassBID");
        var ClassMList = $("#ClassMID");
        var ClassSList = $("#ClassSID");
        var EquipList = $("#EquipmentID");


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
                    condition: "ClassBID='" + bid + "' AND ClassID = 'EquipType'"
                },
                function () {

                    ClassSList.empty();


                }
            );
                bindSelectData(
            EquipList,
            opts.listEquipUrl,
            {
                textField: "EquipmentName",
                valueField: "ID",
                condition: "ClassBID='" + bid + "' and ClassMID is null and ClassSID  is null"
            });
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
                    condition: "ClassBID ='" + _bid + "' AND ClassMID='" + mid + "' AND  ClassID = 'EquipType'"
                },
                function () {

                }
            );

            bindSelectData(
            EquipList,
            opts.listEquipUrl,
            {
                textField: "EquipmentName",
                valueField: "ID",
                condition: "ClassBID='" + _bid + "' AND ClassMID='" + mid + "' and ClassSID  is null"
            } );
            
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

}