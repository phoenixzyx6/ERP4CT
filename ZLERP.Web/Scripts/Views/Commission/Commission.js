function commissionIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'CommissionGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 600
            , dialogHeight: 300
		    , initArray: [
                { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80, hidden: true }
                , { label: '建设单位', name: 'ConstructUnit', index: 'ProduceTask.ConstructUnit', width: 180 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProduceTask.ProjectName', width: 180 }
                , { label: '理论配比编号', name: 'CustMixpropID', index: 'CustMixpropID', width: 80, hidden: true }
                , { label: '配比代号', name: 'MixpropCode', index: 'CustMixprop.MixpropCode', width: 80 }
                , { label: '砼强度', name: 'ConStrength', index: 'ConStrength', width: 80 }
                , { label: '委托日期', name: 'CommissionDate', index: 'CommissionDate', formatter: 'date', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 160 }
                , { label: '委托号', name: 'ID', index: 'ID', width: 100 }
                 , { label: '创建人', name: 'Builder', index: 'Builder' }
                , { label: '修改人', name: 'Modifier', index: 'Modifier' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'CommissionFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'CommissionEditFormDiv',
                        btn: btn,
                        prefix: 'Commission'
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handlePrint: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    else {
                        var url = "/Reports/QCC/R710618.aspx?uid=" + myJqGrid.getSelectedKey();
                        window.open(url);
                    }
                }
                , handlePrintJJB: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条记录进行操作！');
                        return;
                    }
                    else {
                        var url = "/Reports/QCC/R710802.aspx?uid=" + myJqGrid.getSelectedKey();
                        window.open(url);
                    }
                }
            }
    });

    myJqGrid.addListeners('rowclick', function (id, record, selBool) {
        $("#Commission_CommissionDate").val(record.CommissionDate);
        $("#Commission_ID").val(id);
        $("#CommissionItem_CommissionID").val(id);
        $("#Commission_TaskID").val(record.TaskID);
        $("#Commission_FormulaID").val(record.FormulaID);
        mySubGrid.refreshGrid("CommissionID='" + id + "'");
    });



    var mySubGrid = new MyGrid({
        renderTo: 'CommissionItemsGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight * 0.45
		    , storeURL: opts.itemstoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
            , singleSelect: true
        //, editSaveUrl: opts.itemupdate
		    , initArray: [
                  { label: '流水号', name: 'ID', index: 'ID', hidden: true }
                , { label: '委托号', name: 'CommissionID', index: 'CommissionID', hidden: true }
                , { label: '检测项目', name: 'ExamineItemName', index: 'ExamineItemName', width: 120, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ExamType'} }
                , { label: '检测项目编号', name: 'ExamineItemNameID', index: 'ExamineItemNameID', width: 120 }
                , { label: '操作', name: 'act', index: 'act', width: 120, sortable: false, align: "center" }
		    ]
            , functions: {
                handleReload: function (btn) {
                    mySubGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    mySubGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    SaveItems();
                    return false;
                },
                handleEdit: function (btn) {
                    mySubGrid.handleEdit({
                        loadFrom: 'CommissionItemsForm',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    mySubGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

    mySubGrid.addListeners("gridComplete", function () {
        var ids = mySubGrid.getJqGrid().jqGrid('getDataIDs');
        var pid = myJqGrid.getJqGrid().jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            be = "<input class='identityButton'  type='button' value='输入数据' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleInputData(" + ids[i] + ");\"  />";
            ce = "<input class='identityButton'  type='button' value='预览报告' onmouseout=\"jQuery(this).removeClass('ui-state-hover');\" onmouseover=\"jQuery(this).addClass('ui-state-hover');\" onclick=\"handleShowData(" + ids[i] + ");\"  />";
            mySubGrid.getJqGrid().jqGrid('setRowData', ids[i], { act: be + ce });

        }

    });

    window.handleInputData = function (id) {
        var selectrecord = mySubGrid.getRecordByKeyValue(id);
        var examid = selectrecord.ExamineItemNameID;
        var examname = selectrecord.ExamineItemName;
        var _width = 900;
        var _height = 600;
        var loadFromString = '';
        var postUrlString = '';
        var getUrlString = '';
        if (examname == "CEE") {
            loadFromString = '/CEExam.mvc/Index #MyFormDiv form';
            postUrlString = '/CEExam.mvc/Update';
            getUrlString = '/CEExam.mvc/Get';
            _width = 750;
            _height = 600;
        }
        else if (examname == "FAE") {
            loadFromString = '/FAExam.mvc/Index #MyFormDiv form';
            postUrlString = '/FAExam.mvc/Update';
            getUrlString = '/FAExam.mvc/Get';
            _width = 880;
            _height = 630;
        }
        else if (examname == "CAE") {
            loadFromString = '/CAExam.mvc/Index #MyFormDiv form';
            postUrlString = '/CAExam.mvc/Update';
            getUrlString = '/CAExam.mvc/Get';
            _width = 980;
            _height = 500;
        }
        else if (examname == "AIR1E") {
            loadFromString = '/AIR1Exam.mvc/Index #MyFormDiv form';
            postUrlString = '/AIR1Exam.mvc/Update';
            getUrlString = '/AIR1Exam.mvc/Get';
            _width = 750;
            _height = 400;
        }
        else if (examname == "AIR2E") {
            loadFromString = '/AIR2Exam.mvc/Index #MyFormDiv form';
            postUrlString = '/AIR2Exam.mvc/Update';
            getUrlString = '/AIR2Exam.mvc/Get';
            _width = 750;
            _height = 450;
        }
        else if (examname == "ADME") {
            loadFromString = '/ADMExam.mvc/Index #MyFormDiv form';
            postUrlString = '/ADMExam.mvc/Update';
            getUrlString = '/ADMExam.mvc/Get';
            _width = 1024;
            _height = 500;
        }
        else if (examname == "ADM2E") {
            loadFromString = '/ADM2Exam.mvc/Index #MyFormDiv form';
            postUrlString = '/ADM2Exam.mvc/Update';
            getUrlString = '/ADM2Exam.mvc/Get';
        }
        else if (examname == "KYE") {
            loadFromString = '/QualityExam.mvc/Index #MyFormDiv form';
            postUrlString = '/QualityExam.mvc/Update';
            getUrlString = '/QualityExam.mvc/Get';

            mySubGrid.handleAdd({
                loadFrom: loadFromString,
                postUrl: postUrlString,
                width: 540,
                height: 320,
                afterFormLoaded: function () {
                    var formid = $("#MyGrid-Dialog>form").first()[0].attributes.id.nodeValue;
                    setFormValuesWithPre(formid, getUrlString, { id: examid }, null, "QualityExam");
                }
            });
            return;
        }
        else if (examname == "KCE") {
            loadFromString = '/QualityExam.mvc/Index #MyFormDiv form';
            postUrlString = '/QualityExam.mvc/Update';
            getUrlString = '/QualityExam.mvc/Get';

            mySubGrid.handleAdd({
                loadFrom: loadFromString,
                postUrl: postUrlString,
                width: 540,
                height: 320,
                afterFormLoaded: function () {
                    var formid = $("#MyGrid-Dialog>form").first()[0].attributes.id.nodeValue;
                    setFormValuesWithPre(formid, getUrlString, { id: examid }, null, "QualityExam");
                }
            });
            return;
        }
        else {
            showAlert("提示", "本资料不支持在当页录入数据！");
            return;
        }
        mySubGrid.handleAdd({
            loadFrom: loadFromString,
            postUrl: postUrlString,
            width: _width,
            height: _height,
            afterFormLoaded: function () {
                var formid = $("#MyGrid-Dialog>form").first()[0].attributes.id.nodeValue;
                setFormValues(formid, getUrlString, { id: examid }, null);
            }
        });
    };

    window.handleShowData = function (id) {
        var selectrecord = mySubGrid.getRecordByKeyValue(id);
        var pid = myJqGrid.getSelectedKey();
        var examid = selectrecord.ExamineItemNameID;
        var examname = selectrecord.ExamineItemName;
        var url = "";
        if (examname == "CEE") {
            url = "/Reports/QCC/R710611.aspx?uid=" + examid + "&pid=" + pid;
        }

        else if (examname == "FAE") {
            url = "/Reports/QCC/R710612.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "CAE") {
            url = "/Reports/QCC/R710613.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "AIR1E") {
            url = "/Reports/QCC/R710614.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "AIR2E") {
            url = "/Reports/QCC/R710615.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "ADME") {
            url = "/Reports/QCC/R710616.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "ADM2E") {
            url = "/Reports/QCC/R710617.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "KYE") {
            url = "/Reports/QCC/R710809.aspx?uid=" + examid + "&pid=" + pid;
        }
        else if (examname == "KCE") {
            url = "/Reports/QCC/R710810.aspx?uid=" + examid + "&pid=" + pid;
        }

        else if (examname == "PHB") {
            url = "/Reports/QCC/R710801.aspx?uid=" + examid + "&pid=" + pid;
        }
        window.open(url);
    };


    //保存项目明细
    window.SaveItems = function () {
        var id = myJqGrid.getSelectedKey();
        if (isEmpty(id)) {
            showMessage('提示', '请选择一条记录进行操作！');
            return false;
        }
        if ($("#CommissionItem_ExamineItemNameID").val().length == 0) {
            showMessage('提示', '请先录入原始记录！');
            return;
        }
        $.post(
                "/CommissionItem.mvc/Add",
                $("#CommissionItemsForms").serialize(),
                function (data) {
                    if (!data.Result) {
                        showError(data.Message);
                        return false;
                    }
                    else {
                        showMessage(data.Message);
                    }
                    mySubGrid.refreshGrid("CommissionID='" + id + "'");
                }
        );
    };

    $("#CommissionItem_ExamineItemName").bind("change", function () {
        var id = $("#CommissionItem_ExamineItemName").val();
        var geturl = "";
        if (id == "PHB") {
            geturl = opts.listCustUrl;
            bindSelectData($("#CommissionItem_ExamineItemNameID"), geturl, { textField: 'MixpropCode', valueField: 'ID', orderBy: 'MixpropCode', ascending: true, condition: "" }, function () { return; });
        }
        else {
            switch (id) {
                case "CEE":
                    {
                        geturl = opts.listCEExamUrl;
                        break;
                    }
                case "FAE":
                    {
                        geturl = opts.listFAExamUrl;
                        break;
                    }
                case "CAE":
                    {
                        geturl = opts.listCAExamUrl;
                        break;
                    }
                case "AIR1E":
                    {
                        geturl = opts.listAIR1ExamUrl;
                        break;
                    }
                case "AIR2E":
                    {
                        geturl = opts.listAIR2ExamUrl;
                        break;
                    }
                case "ADME":
                    {
                        geturl = opts.listADMExamUrl;
                        break;
                    }
                case "ADME2":
                    {
                        geturl = opts.listADM2ExamUrl;
                        break;
                    }
                case "KYE":
                    {
                        geturl = opts.listKYExamUrl;
                        break;
                    }
                case "KCE":
                    {
                        geturl = opts.listKYExamUrl;
                        break;
                    }
                default:
                    break;
            }
            if (geturl.length > 0)
                bindSelectData($("#CommissionItem_ExamineItemNameID"), geturl, { textField: 'ID', valueField: 'ID', orderBy: 'ID', ascending: true, condition: "" }, function () { return; });
        }
    });
}