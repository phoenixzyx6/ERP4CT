function carrepairIndexInit(storeUrl) {
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

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            //, groupField: 'CarID'
            //, groupingView: { groupDataSorted: true }
		    , initArray: [
                { label: '维修编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '车号', name: 'CarID', index: 'CarID', width: 80 }
                , { label: '维修类型', name: 'RepairType', index: 'RepairType', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CarRepairType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('CarRepairType')} }
                , { label: '维修时间', name: 'RepairTime', index: 'RepairTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '返厂时间', name: 'ReturnTime', index: 'ReturnTime', formatter: 'datetime', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '维修费用', name: 'RepairCost', index: 'RepairCost', formatter: 'currency', width: 80 }
                , { label: '预估费用', name: 'summoney', index: 'summoney', formatter: 'currency', width: 80 }
                , { label: '送修人', name: 'CarMan', index: 'CarMan', width: 80 }
                , { label: '维修地点', name: 'RepairAddr', index: 'RepairAddr' }                
                , { label: '维修原因', name: 'RepairReason', index: 'RepairReason' }
                , { label: '维修描述', name: 'RepairDes', index: 'RepairDes' }
                , { label: '维修人', name: 'RepairMan', index: 'RepairMan' }
                , { label: '状态', name: 'mtlystate', index: 'mtlystate', formatter: planstateFmt, unformat: planstateUnFmt, align: 'center', hidden: true }
		    ]
		    , autoLoad: true
            , functions: {
                handlesubmit: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择要提交的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.mtlystate == "0") {
                        ajaxRequest(btn.data.Url, { id: record.ID }, true, function (response) {
                            if (response.Result) {
                                myJqGrid.reloadGrid();
                            }
                        });
                    }
                    else {
                        showMessage('提示', "不能重复提交!");
                        return;
                    }
                },

                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    

                    myJqGrid.handleAdd({
                        width: 480,
                        height: 350,
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data.mtlystate != '0') {
                        showMessage('提示', "已经提交!");
                        return;
                    }

                    myJqGrid.handleEdit({
                        width: 480,
                        height: 350,
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data.mtlystate != '0') {
                        showMessage('提示', "已经提交!");
                        return;
                    }

                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}