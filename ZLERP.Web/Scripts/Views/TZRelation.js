//转退料处理
function tzrelationInit(opts) {
    //状态列值处理 0 录入 1 分车  2 合车
    function lockFmt(cellvalue, options, rowObject) {
        if (cellvalue == '0') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "录入";
            }

        }
        else if (cellvalue == '1') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "分车";
            }

        }
        else if (cellvalue == '2') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "合车";
            }

        }
        else if (cellvalue == '3') {
            var style = "color:blue;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "整车转发";
            }

        }
        else {
            var txt = '';
        }

        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function lockUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }
//    //左边Grid
//    var myJqGridM = new MyGrid({
//        renderTo: 'MyJqGridM'
//        //, width: '100%'
//            , autoWidth: true
//            , buttons: buttons1
//            , height: gGridHeight
//		    , storeURL: opts.storeUrl
//		    , sortByField: 'ID'
//		    , primaryKey: 'ID'
//		    , setGridPageSize: 30
//		    , showPageBar: true
//            , dialogWidth: 600
//            , dialogHeight: 300
//		    , initArray: [
//                { label: '源运输单号', name: 'SourceShipDocID', index: 'SourceShipDocID', width: 80 },
//                { label: '源车号', name: 'SourceCarID', index: 'SourceShippingDocument.CarID', width: 60 },
//                { label: '源工程名称', name: 'SourceProjectName', index: 'SourceShippingDocument.ProjectName' }
//		    ]
//		    , autoLoad: true
//            , functions: {
//                handleReload: function (btn) {
//                    myJqGrid.reloadGrid();
//                },
//                handleRefresh: function (btn) {
//                    myJqGrid.refreshGrid('1=1');
//                },
//                handleAdd: function (btn) {
//                    myJqGrid.handleAdd({
//                        loadFrom: 'MyFormDiv',
//                        btn: btn
//                    });
//                },
//                handleEdit: function (btn) {
//                    myJqGrid.handleEdit({
//                        loadFrom: 'MyFormDiv',
//                        btn: btn,
//                        prefix: 'csa'
//                    });
//                }
//                , handleDelete: function (btn) {
//                    myJqGrid.deleteRecord({
//                        deleteUrl: btn.data.Url
//                        , postCallBack: function (response) {
//                            myitemJqGrid.reloadGrid();
//                            m1Grid.reloadGrid();
//                        }
//                    });
//                }
//                , handleStat: function (btn) {
//                    var pid = myJqGrid.getSelectedKey();
//                    if (pid.length > 0) {
//                        var requestURL = StatUrl;
//                        var postParams = { id: pid };
//                        ajaxRequest(requestURL, postParams, true, function (response) {
//                            m1Grid.refreshGrid("ConStrAssessID='" + pid + "'");
//                            myJqGrid.reloadGrid();
//                        }, null, btn);
//                    }
//                    else {
//                        showMessage('提示', "请选择一条主表信息");
//                    }
//                }

//            }
    //        });


        //右下Grid
        var myJqGridH = new MyGrid({
            renderTo: 'MyJqGridH'
            ,title:'转退料记录列表'
            ,autoWidth: true
            //, buttons: buttons0
            , hetght: 1000
		    , storeURL: opts.findShipDocHisUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 600
            , dialogHeight: 300
            , storeCondition: 'IsLock<>-1'
            , groupField: 'IsLock'
            , groupingView: { groupColumnShow: [true], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'] }
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 60, hidden: true }
                , { label: '转退料编号', name: 'ParentID', index: 'ParentID', width: 150, hidden: true }
                , { label: '初始源运输单号', name: 'SourceShipDocID', index: 'SourceShipDocID', width: 150 }
                , { label: '初始源工程名称', name: 'SourceProjectName', index: 'SourceShippingDocument.ProjectName' }
                , { label: '初始源运输车号', name: 'SourceCarID', index: 'SourceShippingDocument.CarID', width: 150 }
                , { label: '初始源剩退方量', name: 'SourceCube', index: 'SourceCube', width: 150, align: 'center' }
                , { label: '初始源砼强度', name: 'SourceConStrength', index: 'SourceShippingDocument.ConStrength', width: 150 }
                , { label: '操作状态', name: 'IsLock', index: 'IsLock', formatter: lockFmt, unformat: lockUnFmt, width: 150 }

                , { label: '源车号', name: 'operationnum', index: 'operationnum', width: 100 }
                , { label: '源方量', name: 'operationcube', index: 'operationcube', width: 100 }
                , { label: '目标车号', name: 'CarID', index: 'CarID', width: 100 }
                , { label: '目标方量', name: 'Cube', index: 'Cube', width: 100 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime' }
                , { label: '方式', name: 'operation', index: 'operation', width: 100, hidden: true }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: 'csa'
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , postCallBack: function (response) {
                            myitemJqGrid.reloadGrid();
                            m1Grid.reloadGrid();
                        }
                    });
                }
                , handleStat: function (btn) {
                    var pid = myJqGrid.getSelectedKey();
                    if (pid.length > 0) {
                        var requestURL = StatUrl;
                        var postParams = { id: pid };
                        ajaxRequest(requestURL, postParams, true, function (response) {
                            m1Grid.refreshGrid("ConStrAssessID='" + pid + "'");
                            myJqGrid.reloadGrid();
                        }, null, btn);
                    }
                    else {
                        showMessage('提示', "请选择一条主表信息");
                    }
                }

            }
        });

    //上边Grid
    var _carid;
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight*2/3
		    , storeURL: opts.storeUrl
		    , sortByField: 'TZRalationID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 600
            , singleSelect: true
            , dialogHeight: 320
		    , showPageBar: true
            , groupField: 'TzRalationFlag'
            , groupingView: { groupColumnShow: [true], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupOrder: ['desc'], groupSummary: [false], groupColumnShow: true, showSummaryOnHide: true, groupCollapse: false, minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 60 }
                , { label: '操作状态', name: 'IsLock', index: 'IsLock', formatter: lockFmt, unformat: lockUnFmt, width: 60 }
                , { label: '转退料单号', name: 'TzRalationFlag', index: 'TzRalationFlag', width: 100 }
                , { label: '源运输单号', name: 'SourceShipDocID', index: 'SourceShipDocID', width: 80 }
                , { label: '源工程名称', name: 'SourceProjectName', index: 'SourceShippingDocument.ProjectName' }
                , { label: '源运输车号', name: 'SourceCarID', index: 'SourceShippingDocument.CarID', width: 80 }
                , { label: '源剩退方量', name: 'SourceCube', index: 'SourceCube', width: 80, align: 'center' }
                , { label: '源砼强度', name: 'SourceConStrength', index: 'SourceShippingDocument.ConStrength', width: 80 }
                , { label: '目标运输单号', name: 'TargetShipDocID', index: 'TargetShipDocID', width: 80 }
                , { label: '目标工程名称', name: 'TargetProjectName', index: 'TargetShippingDocument.ProjectName' }
                , { label: '目标运输车号', name: 'CarID', index: 'CarID', width: 80 }
                , { label: '目标剩退方量', name: 'Cube', index: 'Cube', width: 80, align: 'center' }
                , { label: '目标砼强度', name: 'TargetConStrength', index: 'TargetShippingDocument.ConStrength', width: 80 }
                , { label: '司机', name: 'DriverUser.TrueName', index: 'DriverUser.TrueName', width: 80 }
                , { label: '剩退类别', name: 'ReturnType', index: 'ReturnType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ReturnType'} }
                , { label: '处理方式', name: 'ActionType', index: 'ActionType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ActionType'} }
                , { label: '剩退方量', name: 'ActionCube', index: 'ActionCube', width: 80, align: 'center' }
                , { label: '剩退时间', name: 'ActionTime', index: 'ActionTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '剩退原因', name: 'ReturnReason', index: 'ReturnReason' }
                , { label: '是否审核', name: 'IsAudit', index: 'IsAudit', width: 60, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '是否完成', name: 'IsCompleted', index: 'IsCompleted', width: 60, formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', width: 60 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: '处理人', name: 'DealMan', index: 'DealMan', width: 60 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: 'A/H', name: 'AH', index: 'AH', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AH'} }
                , { label: '毛重(T)', name: 'TotalWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '皮重(T)', name: 'CarWeight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '净重(T)', name: 'Weight', width: 60, formatter: Kg2TFmt, unformat: T2KgFmt }
                , { label: '换算率(T/m³)', name: 'Exchange', width: 80, formatter: Kg2TFmt, unformat: T2KgFmt }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                //增加退转料
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        width: 520,
                        height: 300,
                        afterFormLoaded: function () {
                            $('table.info', '#MyFormDiv').remove();
                            myJqGrid.setFormFieldDisabled('CarID', false);
                            myJqGrid.setFormFieldDisabled('Cube', false);
                        },
                        beforeSubmit: function () {
                            var actionType = myJqGrid.getFormFieldValue('ActionType');
                            if (actionType == 'AT2')
                                myJqGrid.setFormFieldValue('IsCompleted', true);
                            else
                                myJqGrid.setFormFieldValue('IsCompleted', false);
                            var lastCube = parseFloat($('#spSendCube').text());
                            var cube = myJqGrid.getFormFieldValue('Cube');
                            var thisCube = parseFloat(cube);
                            if (thisCube > lastCube) {
                                showError('错误', '剩退方量不能大于上车运送方量，如果整车退回，请使用整车转发功能。');
                                myJqGrid.getFormField('Cube').addClass('input-validation-error');
                                return false;
                            }
                            else
                                return true;
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                },
                handleDelete: function (btn) {
                    if (myJqGrid.isSelectedOnlyOne('请选择一条记录进行删除。')) {
                        var record = myJqGrid.getSelectedRecord();
                        if (record.IsCompleted == "false") {
                            myJqGrid.deleteRecord({
                                deleteUrl: btn.data.Url
                            });
                        }
                        else {
                            showError('错误', '该转退料记录已经完成，不能删除');
                            return;
                        }
                    }
                },
                handleAudit: function (btn) {
                    if (myJqGrid.isSelectedOnlyOne('请选择一条记录进行审核。')) {
                        var record = myJqGrid.getSelectedRecord();
                        if (record.IsCompleted == "false") {
                            showError('错误', '该转退料记录没有完成，不能审核');
                            return;
                        }

                        if (record.IsAudit == "true") {
                            showError('错误', '该转退料记录已经审核！');
                            return;
                        }
                        showConfirm('确认', '是否确实要审核选中的记录？', function () {
                            ajaxRequest(btn.data.Url,
                                { id: myJqGrid.getSelectedKey() },
                                true,
                                function (response) {
                                    if (response.Result) {
                                        myJqGrid.reloadGrid();
                                    }
                                }
                            );
                            $(this).dialog('close');
                        });
                    }
                },
                handleMarkAction: function (btn) {
                    if (myJqGrid.isSelectedOnlyOne('请选择一条记录进行处理。')) {
                        var record = myJqGrid.getSelectedRecord();
                        if (!(record.IsCompleted == 'false' && record.AH == 'Auto')) {
                            showError('错误', '该转退料记录不需要进行处理！');
                            return;
                        }
                        myJqGrid.handleEdit({
                            loadFrom: 'MyFormDiv',
                            btn: btn,
                            afterFormLoaded: function () {
                                $('table.info', '#MyFormDiv').remove();
                                myJqGrid.setFormFieldDisabled('CarID', true);
                                //myJqGrid.setFormFieldDisabled('Cube', true);
                            }
                        });
                    }
                },
                //整车转发
                handleForward: function (btn) {
                    var caridinfo = new Array();
                    myJqGrid.handleAdd({
                        loadFrom: 'forwardForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            $('table.info', '#forwardForm').remove();
                        },
                        beforeSubmit: function () {
                            caridinfo[0] = myJqGrid.getFormFieldValue('CarID1');
                            //alert(caridinfo[0]); //转发

                            var targetTaskId = myJqGrid.getFormFieldValue('TargetTaskId');
                            if (isEmpty(targetTaskId)) {
                                showError('错误', '请指定要转发到哪个任务单。');
                                $("input[name='ProjectName']").addClass("input-validation-error");
                                return false;
                            }
                            else
                                return true;
                        },
                        postCallBack: function (response) {
                            if (response && response.Result) {
                                showConfirm('打印新运输单', '是否要打印新的运输单？', function () {
                                    $(this).dialog("close");
                                    //processShipDocPrint('print', response.Data);
                                    printShippingDoc('print', response.Data.ID);
                                });
                            }
                            else {
                                showError(response.Message);
                            }
                        }
                        , postData: { _caridz: caridinfo }
                    });
                },
                //转退料分车
                handleSplitByZT: function (btn) {
                    //未完成
                    if (!myJqGrid.isSelectedOnlyOne('请选择一条转退料记录')) {
                        return false;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    if (data.IsCompleted != 'false') {
                        showError('错误', "此转退料已完成，不能修改。");
                        return false;
                    }
                    if (data.IsLock == '2') {
                        showError('错误', "此转退料已分车，不能修改。");
                        return false;
                    }

                    _carid = data.CarID;
                    var _returnType = data.ReturnType;
                    var _sourceCube = data.Cube;
                    var _TzRFlag = data.TzRalationFlag;

                    var carIDArr = new Array();
                    var carCubeArr = new Array();
                    var actionTypeArr = new Array();

                    myJqGrid.handleEdit({
                        loadFrom: 'splitFormByZT',
                        btn: btn,
                        width: 700,
                        height: 430,
                        afterFormLoaded: function () {

                            //移除整车转发
                            $('#ReturnType', '#splitFormByZT').children('option[value=RT0]').remove();

                            _sourceCube = data.Cube;
                            myJqGrid.getFormField("SourceCube").val(_sourceCube);

                            $('#Cube1', '#splitFormByZT').removeClass('text requiredval');
                            $('#Cube2', '#splitFormByZT').removeClass('text requiredval');
                            $('#Cube3', '#splitFormByZT').removeClass('text requiredval');
                            $('#Cube4', '#splitFormByZT').removeClass('text requiredval');
                            $('#Cube5', '#splitFormByZT').removeClass('text requiredval');

                            $('#ActionType1', '#splitFormByZT').removeClass('text requiredval');
                            $('#ActionType2', '#splitFormByZT').removeClass('text requiredval');
                            $('#ActionType3', '#splitFormByZT').removeClass('text requiredval');
                            $('#ActionType4', '#splitFormByZT').removeClass('text requiredval');
                            $('#ActionType5', '#splitFormByZT').removeClass('text requiredval');

                            $('#ActionType1', '#splitFormByZT').removeClass('input-validation-error');
                            $('#ActionType2', '#splitFormByZT').removeClass('input-validation-error');
                            $('#ActionType3', '#splitFormByZT').removeClass('input-validation-error');
                            $('#ActionType4', '#splitFormByZT').removeClass('input-validation-error');
                            $('#ActionType5', '#splitFormByZT').removeClass('input-validation-error');

                            $('table.info', '#splitFormByZT').remove();
                        },
                        beforeSubmit: function () {

                            var checking = true;
                            var errorMsg = "";

                            carIDArr[0] = myJqGrid.getFormFieldValue('CarID1');
                            carIDArr[1] = myJqGrid.getFormFieldValue('CarID2');
                            carIDArr[2] = myJqGrid.getFormFieldValue('CarID3');
                            carIDArr[3] = myJqGrid.getFormFieldValue('CarID4');
                            carIDArr[4] = myJqGrid.getFormFieldValue('CarID5');

                            carCubeArr[0] = myJqGrid.getFormFieldValue('Cube1');
                            carCubeArr[1] = myJqGrid.getFormFieldValue('Cube2');
                            carCubeArr[2] = myJqGrid.getFormFieldValue('Cube3');
                            carCubeArr[3] = myJqGrid.getFormFieldValue('Cube4');
                            carCubeArr[4] = myJqGrid.getFormFieldValue('Cube5');

                            carCubeArr[0] = isEmpty(carCubeArr[0]) ? 0 : parseFloat(carCubeArr[0]);
                            carCubeArr[1] = isEmpty(carCubeArr[1]) ? 0 : parseFloat(carCubeArr[1]);
                            carCubeArr[2] = isEmpty(carCubeArr[2]) ? 0 : parseFloat(carCubeArr[2]);
                            carCubeArr[3] = isEmpty(carCubeArr[3]) ? 0 : parseFloat(carCubeArr[3]);
                            carCubeArr[4] = isEmpty(carCubeArr[4]) ? 0 : parseFloat(carCubeArr[4]);

                            actionTypeArr[0] = myJqGrid.getFormFieldValue('ActionType1');
                            actionTypeArr[1] = myJqGrid.getFormFieldValue('ActionType2');
                            actionTypeArr[2] = myJqGrid.getFormFieldValue('ActionType3');
                            actionTypeArr[3] = myJqGrid.getFormFieldValue('ActionType4');
                            actionTypeArr[4] = myJqGrid.getFormFieldValue('ActionType5');

                            var sourceCube = myJqGrid.getFormFieldValue('SourceCube');
                            sourceCube = isEmpty(sourceCube) ? 0 : parseFloat(sourceCube);

                            var lastCube = parseFloat($('#spSendCube').text());
                            //                            if (sourceCube > lastCube) {
                            //                                errorMsg += "剩退方量不能大于上车运送方量。";
                            //                                myJqGrid.getFormField('SourceCube').addClass('input-validation-error');
                            //                                checking = false;
                            //                            }

                            if (isEmpty(carIDArr[0]) && isEmpty(carIDArr[1]) && isEmpty(carIDArr[2]) && isEmpty(carIDArr[3]) && isEmpty(carIDArr[4])) {
                                errorMsg += "<br>保存前请进行分装操作。";
                                checking = false;
                            }

                            if (!isEmpty(carIDArr[0])) {
                                if (isEmpty(carCubeArr[0])) {
                                    $('#Cube1', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[0])) {
                                    $('#ActionType1', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[1])) {
                                if (isEmpty(carCubeArr[1])) {
                                    $('#Cube2', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[1])) {
                                    $('#ActionType2', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[2])) {
                                if (isEmpty(carCubeArr[2])) {
                                    $('#Cube3', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[2])) {
                                    $('#ActionType3', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[3])) {
                                if (isEmpty(carCubeArr[3])) {
                                    $('#Cube4', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[3])) {
                                    $('#ActionType4', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[4])) {
                                if (isEmpty(carCubeArr[4])) {
                                    $('#Cube5', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[4])) {
                                    $('#ActionType5', '#splitFormByZT').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if ((!isEmpty(carIDArr[0]) && isEmpty(carIDArr[1]) && carIDArr[0] == carIDArr[1] && actionTypeArr[0] == actionTypeArr[1])
                                || (!isEmpty(carIDArr[0]) && isEmpty(carIDArr[2]) && carIDArr[0] == carIDArr[2] && actionTypeArr[0] == actionTypeArr[2])
                                || (!isEmpty(carIDArr[0]) && isEmpty(carIDArr[3]) && carIDArr[0] == carIDArr[3] && actionTypeArr[0] == actionTypeArr[3])
                                || (!isEmpty(carIDArr[0]) && isEmpty(carIDArr[4]) && carIDArr[0] == carIDArr[4] && actionTypeArr[0] == actionTypeArr[4])
                                || (!isEmpty(carIDArr[1]) && isEmpty(carIDArr[2]) && carIDArr[1] == carIDArr[2] && actionTypeArr[1] == actionTypeArr[2])
                                || (!isEmpty(carIDArr[1]) && isEmpty(carIDArr[3]) && carIDArr[1] == carIDArr[3] && actionTypeArr[1] == actionTypeArr[3])
                                || (!isEmpty(carIDArr[1]) && isEmpty(carIDArr[4]) && carIDArr[1] == carIDArr[4] && actionTypeArr[1] == actionTypeArr[4])
                                || (!isEmpty(carIDArr[2]) && isEmpty(carIDArr[3]) && carIDArr[2] == carIDArr[3] && actionTypeArr[2] == actionTypeArr[3])
                                || (!isEmpty(carIDArr[2]) && isEmpty(carIDArr[4]) && carIDArr[2] == carIDArr[4] && actionTypeArr[2] == actionTypeArr[4])
                                || (!isEmpty(carIDArr[3]) && isEmpty(carIDArr[4]) && carIDArr[3] == carIDArr[4] && actionTypeArr[3] == actionTypeArr[4])) {
                                errorMsg += "<br>请选择不同的车进行分装。";
                                checking = false;
                            }

                            if (carCubeArr[0] * 1000 + carCubeArr[1] * 1000 + carCubeArr[2] * 1000 + carCubeArr[3] * 1000 + carCubeArr[4] * 1000 != sourceCube * 1000) {
                                errorMsg += "<br>源剩退方量与分装方量之和不相等。";
                                checking = false;
                            }

                            if (checking == false || !checking) {
                                showError('验证错误', errorMsg);
                                return false;
                            }
                            //alert(_sourceCube); //分车
                            return true;
                        },
                        postData: { returnType: _returnType, scube: _sourceCube, carIDArr: carIDArr, carCubeArr: carCubeArr, actionTypeArr: actionTypeArr,tzrFlag:_TzRFlag }
                    });
                }
                , handleSplit: function (btn) {
                    var carIDArr = new Array();
                    var carCubeArr = new Array();
                    var actionTypeArr = new Array();

                    myJqGrid.handleAdd({
                        loadFrom: 'splitForm',
                        btn: btn,
                        width: 600,
                        height: 430,
                        afterFormLoaded: function () {
                            $('#Cube1', '#splitForm').removeClass('text requiredval');
                            $('#Cube2', '#splitForm').removeClass('text requiredval');
                            $('#Cube3', '#splitForm').removeClass('text requiredval');
                            $('#Cube4', '#splitForm').removeClass('text requiredval');
                            $('#Cube5', '#splitForm').removeClass('text requiredval');

                            $('#ActionType1', '#splitForm').removeClass('text requiredval');
                            $('#ActionType2', '#splitForm').removeClass('text requiredval');
                            $('#ActionType3', '#splitForm').removeClass('text requiredval');
                            $('#ActionType4', '#splitForm').removeClass('text requiredval');
                            $('#ActionType5', '#splitForm').removeClass('text requiredval');

                            $('#ActionType1', '#splitForm').removeClass('input-validation-error');
                            $('#ActionType2', '#splitForm').removeClass('input-validation-error');
                            $('#ActionType3', '#splitForm').removeClass('input-validation-error');
                            $('#ActionType4', '#splitForm').removeClass('input-validation-error');
                            $('#ActionType5', '#splitForm').removeClass('input-validation-error');

                            $('table.info', '#splitForm').remove();
                        },
                        beforeSubmit: function () {
                            var checking = true;
                            var errorMsg = "";

                            carIDArr[0] = myJqGrid.getFormFieldValue('CarID1');
                            carIDArr[1] = myJqGrid.getFormFieldValue('CarID2');
                            carIDArr[2] = myJqGrid.getFormFieldValue('CarID3');
                            carIDArr[3] = myJqGrid.getFormFieldValue('CarID4');
                            carIDArr[4] = myJqGrid.getFormFieldValue('CarID5');

                            carCubeArr[0] = myJqGrid.getFormFieldValue('Cube1');
                            carCubeArr[1] = myJqGrid.getFormFieldValue('Cube2');
                            carCubeArr[2] = myJqGrid.getFormFieldValue('Cube3');
                            carCubeArr[3] = myJqGrid.getFormFieldValue('Cube4');
                            carCubeArr[4] = myJqGrid.getFormFieldValue('Cube5');

                            carCubeArr[0] = isEmpty(carCubeArr[0]) ? 0 : parseFloat(carCubeArr[0]);
                            carCubeArr[1] = isEmpty(carCubeArr[1]) ? 0 : parseFloat(carCubeArr[1]);
                            carCubeArr[2] = isEmpty(carCubeArr[2]) ? 0 : parseFloat(carCubeArr[2]);
                            carCubeArr[3] = isEmpty(carCubeArr[3]) ? 0 : parseFloat(carCubeArr[3]);
                            carCubeArr[4] = isEmpty(carCubeArr[4]) ? 0 : parseFloat(carCubeArr[4]);

                            actionTypeArr[0] = myJqGrid.getFormFieldValue('ActionType1');
                            actionTypeArr[1] = myJqGrid.getFormFieldValue('ActionType2');
                            actionTypeArr[2] = myJqGrid.getFormFieldValue('ActionType3');
                            actionTypeArr[3] = myJqGrid.getFormFieldValue('ActionType4');
                            actionTypeArr[4] = myJqGrid.getFormFieldValue('ActionType5');

                            var sourceCube = myJqGrid.getFormFieldValue('SourceCube');
                            sourceCube = isEmpty(sourceCube) ? 0 : parseFloat(sourceCube);

                            var lastCube = parseFloat($('#spSendCube').text());
                            if (sourceCube > lastCube) {
                                errorMsg += "剩退方量不能大于上车运送方量。";
                                myJqGrid.getFormField('SourceCube').addClass('input-validation-error');
                                checking = false;
                            }

                            if (isEmpty(carIDArr[0]) && isEmpty(carIDArr[1]) && isEmpty(carIDArr[2]) && isEmpty(carIDArr[3]) && isEmpty(carIDArr[4])) {
                                errorMsg += "<br>保存前请进行分装操作。";
                                checking = false;
                            }

                            if (!isEmpty(carIDArr[0])) {
                                if (isEmpty(carCubeArr[0])) {
                                    $('#Cube1', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[0])) {
                                    $('#ActionType1', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[1])) {
                                if (isEmpty(carCubeArr[1])) {
                                    $('#Cube2', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[1])) {
                                    $('#ActionType2', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[2])) {
                                if (isEmpty(carCubeArr[2])) {
                                    $('#Cube3', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[2])) {
                                    $('#ActionType3', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[3])) {
                                if (isEmpty(carCubeArr[3])) {
                                    $('#Cube4', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[3])) {
                                    $('#ActionType4', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carIDArr[4])) {
                                if (isEmpty(carCubeArr[4])) {
                                    $('#Cube5', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(actionTypeArr[4])) {
                                    $('#ActionType5', '#splitForm').addClass('input-validation-error');
                                    errorMsg += "<br>处理方式 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if ((!isEmpty(carIDArr[0]) && isEmpty(carIDArr[1]) && carIDArr[0] == carIDArr[1] && actionTypeArr[0] == actionTypeArr[1])
                                || (!isEmpty(carIDArr[0]) && isEmpty(carIDArr[2]) && carIDArr[0] == carIDArr[2] && actionTypeArr[0] == actionTypeArr[2])
                                || (!isEmpty(carIDArr[0]) && isEmpty(carIDArr[3]) && carIDArr[0] == carIDArr[3] && actionTypeArr[0] == actionTypeArr[3])
                                || (!isEmpty(carIDArr[0]) && isEmpty(carIDArr[4]) && carIDArr[0] == carIDArr[4] && actionTypeArr[0] == actionTypeArr[4])
                                || (!isEmpty(carIDArr[1]) && isEmpty(carIDArr[2]) && carIDArr[1] == carIDArr[2] && actionTypeArr[1] == actionTypeArr[2])
                                || (!isEmpty(carIDArr[1]) && isEmpty(carIDArr[3]) && carIDArr[1] == carIDArr[3] && actionTypeArr[1] == actionTypeArr[3])
                                || (!isEmpty(carIDArr[1]) && isEmpty(carIDArr[4]) && carIDArr[1] == carIDArr[4] && actionTypeArr[1] == actionTypeArr[4])
                                || (!isEmpty(carIDArr[2]) && isEmpty(carIDArr[3]) && carIDArr[2] == carIDArr[3] && actionTypeArr[2] == actionTypeArr[3])
                                || (!isEmpty(carIDArr[2]) && isEmpty(carIDArr[4]) && carIDArr[2] == carIDArr[4] && actionTypeArr[2] == actionTypeArr[4])
                                || (!isEmpty(carIDArr[3]) && isEmpty(carIDArr[4]) && carIDArr[3] == carIDArr[4] && actionTypeArr[3] == actionTypeArr[4])) {
                                errorMsg += "<br>请选择不同的车进行分装。";
                                checking = false;
                            }

                            if (carCubeArr[0] * 1000 + carCubeArr[1] * 1000 + carCubeArr[2] * 1000 + carCubeArr[3] * 1000 + carCubeArr[4] * 1000 != sourceCube * 1000) {
                                errorMsg += "<br>源剩退方量与分装方量之和不相等。";
                                checking = false;
                            }

                            if (checking == false || !checking) {
                                showError('验证错误', errorMsg);
                                return false;
                            }

                            return true;
                        },
                        postData: { carIDArr: carIDArr, carCubeArr: carCubeArr, actionTypeArr: actionTypeArr }
                    });
                },
                handleMerge: function (btn) {
                    var sourceShipDocIDArr = new Array();
                    var sourceCubeArr = new Array();
                    var returnTypeArr = new Array();

                    myJqGrid.handleAdd({
                        loadFrom: 'mergeForm',
                        btn: btn,
                        width: 600,
                        height: 430,
                        afterFormLoaded: function () {
                            $('#Cube1', '#mergeForm').removeClass('text requiredval');
                            $('#Cube2', '#mergeForm').removeClass('text requiredval');
                            $('#Cube3', '#mergeForm').removeClass('text requiredval');
                            $('#Cube4', '#mergeForm').removeClass('text requiredval');
                            $('#Cube5', '#mergeForm').removeClass('text requiredval');

                            $('#ReturnType1', '#mergeForm').removeClass('text requiredval');
                            $('#ReturnType2', '#mergeForm').removeClass('text requiredval');
                            $('#ReturnType3', '#mergeForm').removeClass('text requiredval');
                            $('#ReturnType4', '#mergeForm').removeClass('text requiredval');
                            $('#ReturnType5', '#mergeForm').removeClass('text requiredval');

                            $('#ReturnType1', '#mergeForm').removeClass('input-validation-error');
                            $('#ReturnType2', '#mergeForm').removeClass('input-validation-error');
                            $('#ReturnType3', '#mergeForm').removeClass('input-validation-error');
                            $('#ReturnType4', '#mergeForm').removeClass('input-validation-error');
                            $('#ReturnType5', '#mergeForm').removeClass('input-validation-error');

                            $('table.info', '#mergeForm').remove();
                        },
                        beforeSubmit: function () {
                            var checking = true;
                            var errorMsg = "";

                            var carID1 = myJqGrid.getFormFieldValue('CarID1');
                            var carID2 = myJqGrid.getFormFieldValue('CarID2');
                            var carID3 = myJqGrid.getFormFieldValue('CarID3');
                            var carID4 = myJqGrid.getFormFieldValue('CarID4');
                            var carID5 = myJqGrid.getFormFieldValue('CarID5');

                            sourceShipDocIDArr[0] = myJqGrid.getFormFieldValue('SourceShipDocID1');
                            sourceShipDocIDArr[1] = myJqGrid.getFormFieldValue('SourceShipDocID2');
                            sourceShipDocIDArr[2] = myJqGrid.getFormFieldValue('SourceShipDocID3');
                            sourceShipDocIDArr[3] = myJqGrid.getFormFieldValue('SourceShipDocID4');
                            sourceShipDocIDArr[4] = myJqGrid.getFormFieldValue('SourceShipDocID5');

                            var lastCube1 = myJqGrid.getFormFieldValue("LastCube1");
                            var lastCube2 = myJqGrid.getFormFieldValue("LastCube2");
                            var lastCube3 = myJqGrid.getFormFieldValue("LastCube3");
                            var lastCube4 = myJqGrid.getFormFieldValue("LastCube4");
                            var lastCube5 = myJqGrid.getFormFieldValue("LastCube5");

                            lastCube1 = isEmpty(lastCube1) ? 0 : parseFloat(lastCube1);
                            lastCube2 = isEmpty(lastCube2) ? 0 : parseFloat(lastCube2);
                            lastCube3 = isEmpty(lastCube3) ? 0 : parseFloat(lastCube3);
                            lastCube4 = isEmpty(lastCube4) ? 0 : parseFloat(lastCube4);
                            lastCube5 = isEmpty(lastCube5) ? 0 : parseFloat(lastCube5);

                            sourceCubeArr[0] = myJqGrid.getFormFieldValue('Cube1');
                            sourceCubeArr[1] = myJqGrid.getFormFieldValue('Cube2');
                            sourceCubeArr[2] = myJqGrid.getFormFieldValue('Cube3');
                            sourceCubeArr[3] = myJqGrid.getFormFieldValue('Cube4');
                            sourceCubeArr[4] = myJqGrid.getFormFieldValue('Cube5');

                            sourceCubeArr[0] = isEmpty(sourceCubeArr[0]) ? 0 : parseFloat(sourceCubeArr[0]);
                            sourceCubeArr[1] = isEmpty(sourceCubeArr[1]) ? 0 : parseFloat(sourceCubeArr[1]);
                            sourceCubeArr[2] = isEmpty(sourceCubeArr[2]) ? 0 : parseFloat(sourceCubeArr[2]);
                            sourceCubeArr[3] = isEmpty(sourceCubeArr[3]) ? 0 : parseFloat(sourceCubeArr[3]);
                            sourceCubeArr[4] = isEmpty(sourceCubeArr[4]) ? 0 : parseFloat(sourceCubeArr[4]);

                            returnTypeArr[0] = myJqGrid.getFormFieldValue('ReturnType1');
                            returnTypeArr[1] = myJqGrid.getFormFieldValue('ReturnType2');
                            returnTypeArr[2] = myJqGrid.getFormFieldValue('ReturnType3');
                            returnTypeArr[3] = myJqGrid.getFormFieldValue('ReturnType4');
                            returnTypeArr[4] = myJqGrid.getFormFieldValue('ReturnType5');

                            if (isEmpty(carID1) && isEmpty(carID2) && isEmpty(carID3) && isEmpty(carID4) && isEmpty(carID5)) {
                                errorMsg += "<br>保存前请进行合并操作!";
                                checking = false;
                            }

                            if (!isEmpty(carID1)) {
                                if (isEmpty(sourceCubeArr[0])) {
                                    $('#Cube1', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[0])) {
                                    $('#ReturnType1', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[0] <= 0) {
                                    $('#Cube1', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[0] > lastCube1) {
                                    $('#Cube1', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于上车运送方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID2)) {
                                if (isEmpty(sourceCubeArr[1])) {
                                    $('#Cube2', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[1])) {
                                    $('#ReturnType2', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[1] <= 0) {
                                    $('#Cube2', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[1] > lastCube2) {
                                    $('#Cube2', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于上车运送方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID3)) {
                                if (isEmpty(sourceCubeArr[2])) {
                                    $('#Cube3', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[2])) {
                                    $('#ReturnType3', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[2] <= 0) {
                                    $('#Cube3', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[2] > lastCube3) {
                                    $('#Cube3', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于上车运送方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID4)) {
                                if (isEmpty(sourceCubeArr[3])) {
                                    $('#Cube4', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[3])) {
                                    $('#ReturnType4', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[3] <= 0) {
                                    $('#Cube4', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[3] > lastCube4) {
                                    $('#Cube4', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于上车运送方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID5)) {
                                if (isEmpty(sourceCubeArr[4])) {
                                    $('#Cube5', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[4])) {
                                    $('#ReturnType5', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[4] <= 0) {
                                    $('#Cube5', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[4] > lastCube5) {
                                    $('#Cube5', '#mergeForm').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于上车运送方量。";
                                    checking = false;
                                }
                            }

                            if ((!isEmpty(carID1) && !isEmpty(carID2) && carID1 == carID2 && returnTypeArr[0] == returnTypeArr[1])
                                || (!isEmpty(carID1) && !isEmpty(carID3) && carID1 == carID3 && returnTypeArr[0] == returnTypeArr[2])
                                || (!isEmpty(carID1) && !isEmpty(carID4) && carID1 == carID4 && returnTypeArr[0] == returnTypeArr[3])
                                || (!isEmpty(carID1) && !isEmpty(carID5) && carID1 == carID5 && returnTypeArr[0] == returnTypeArr[4])
                                || (!isEmpty(carID2) && !isEmpty(carID3) && carID2 == carID3 && returnTypeArr[1] == returnTypeArr[2])
                                || (!isEmpty(carID2) && !isEmpty(carID4) && carID2 == carID4 && returnTypeArr[1] == returnTypeArr[3])
                                || (!isEmpty(carID2) && !isEmpty(carID5) && carID2 == carID5 && returnTypeArr[1] == returnTypeArr[4])
                                || (!isEmpty(carID3) && !isEmpty(carID4) && carID3 == carID4 && returnTypeArr[2] == returnTypeArr[3])
                                || (!isEmpty(carID3) && !isEmpty(carID5) && carID3 == carID5 && returnTypeArr[2] == returnTypeArr[4])
                                || (!isEmpty(carID4) && !isEmpty(carID5) && carID4 == carID5 && returnTypeArr[3] == returnTypeArr[4])) {
                                errorMsg += "<br>请选择不同的车进行合并。";
                                checking = false;
                            }

                            var targetCube = myJqGrid.getFormFieldValue("Cube");
                            targetCube = isEmpty(targetCube) ? 0 : parseFloat(targetCube);
                            if (sourceCubeArr[0] * 1000 + sourceCubeArr[1] * 1000 + sourceCubeArr[2] * 1000 + sourceCubeArr[3] * 1000 + sourceCubeArr[4] * 1000 != targetCube * 1000) {
                                errorMsg += "<br>合并方量之和与目标剩退方量不相等!";
                                checking = false;
                            }

                            if (checking == false || !checking) {
                                showError('验证错误', errorMsg);
                                return false;
                            }

                            return true;
                        },
                        postData: { sourceShipDocIDArr: sourceShipDocIDArr, sourceCubeArr: sourceCubeArr, returnTypeArr: returnTypeArr }
                    });
                },



                //转退料合并
                handleMergeZT: function (btn) {
                    var sourceShipDocIDArr = new Array();
                    var sourceCubeArr = new Array();
                    var returnTypeArr = new Array();
                    var returnTypeArrTarget = new Array();

                    myJqGrid.handleAdd({
                        loadFrom: 'mergeFormZT',
                        btn: btn,
                        width: 600,
                        height: 430,
                        afterFormLoaded: function () {
                            $('#Cube1', '#mergeFormZT').removeClass('text requiredval');
                            $('#Cube2', '#mergeFormZT').removeClass('text requiredval');
                            $('#Cube3', '#mergeFormZT').removeClass('text requiredval');
                            $('#Cube4', '#mergeFormZT').removeClass('text requiredval');
                            $('#Cube5', '#mergeFormZT').removeClass('text requiredval');

                            $('#ReturnType1', '#mergeFormZT').removeClass('text requiredval');
                            $('#ReturnType2', '#mergeFormZT').removeClass('text requiredval');
                            $('#ReturnType3', '#mergeFormZT').removeClass('text requiredval');
                            $('#ReturnType4', '#mergeFormZT').removeClass('text requiredval');
                            $('#ReturnType5', '#mergeFormZT').removeClass('text requiredval');

                            $('#ReturnType1', '#mergeFormZT').removeClass('input-validation-error');
                            $('#ReturnType2', '#mergeFormZT').removeClass('input-validation-error');
                            $('#ReturnType3', '#mergeFormZT').removeClass('input-validation-error');
                            $('#ReturnType4', '#mergeFormZT').removeClass('input-validation-error');
                            $('#ReturnType5', '#mergeFormZT').removeClass('input-validation-error');

                            $('table.info', '#mergeFormZT').remove();
                        },
                        beforeSubmit: function () {
                            var checking = true;
                            var errorMsg = "";

                            var maxcube = myJqGrid.getFormFieldValue('maxcube');
                            var carID = myJqGrid.getFormFieldValue('CarID');
                            returnTypeArrTarget[0] = myJqGrid.getFormFieldValue('ReturnType6');

                            var carID1 = myJqGrid.getFormFieldValue('CarID1');
                            var carID2 = myJqGrid.getFormFieldValue('CarID2');
                            var carID3 = myJqGrid.getFormFieldValue('CarID3');
                            var carID4 = myJqGrid.getFormFieldValue('CarID4');
                            var carID5 = myJqGrid.getFormFieldValue('CarID5');

                            sourceShipDocIDArr[0] = myJqGrid.getFormFieldValue('SourceShipDocID1');
                            sourceShipDocIDArr[1] = myJqGrid.getFormFieldValue('SourceShipDocID2');
                            sourceShipDocIDArr[2] = myJqGrid.getFormFieldValue('SourceShipDocID3');
                            sourceShipDocIDArr[3] = myJqGrid.getFormFieldValue('SourceShipDocID4');
                            sourceShipDocIDArr[4] = myJqGrid.getFormFieldValue('SourceShipDocID5');

                            var lastCube1 = myJqGrid.getFormFieldValue("LastCube1");
                            var lastCube2 = myJqGrid.getFormFieldValue("LastCube2");
                            var lastCube3 = myJqGrid.getFormFieldValue("LastCube3");
                            var lastCube4 = myJqGrid.getFormFieldValue("LastCube4");
                            var lastCube5 = myJqGrid.getFormFieldValue("LastCube5");

                            lastCube1 = isEmpty(lastCube1) ? 0 : parseFloat(lastCube1);
                            lastCube2 = isEmpty(lastCube2) ? 0 : parseFloat(lastCube2);
                            lastCube3 = isEmpty(lastCube3) ? 0 : parseFloat(lastCube3);
                            lastCube4 = isEmpty(lastCube4) ? 0 : parseFloat(lastCube4);
                            lastCube5 = isEmpty(lastCube5) ? 0 : parseFloat(lastCube5);

                            sourceCubeArr[0] = myJqGrid.getFormFieldValue('Cube1');
                            sourceCubeArr[1] = myJqGrid.getFormFieldValue('Cube2');
                            sourceCubeArr[2] = myJqGrid.getFormFieldValue('Cube3');
                            sourceCubeArr[3] = myJqGrid.getFormFieldValue('Cube4');
                            sourceCubeArr[4] = myJqGrid.getFormFieldValue('Cube5');

                            sourceCubeArr[0] = isEmpty(sourceCubeArr[0]) ? 0 : parseFloat(sourceCubeArr[0]);
                            sourceCubeArr[1] = isEmpty(sourceCubeArr[1]) ? 0 : parseFloat(sourceCubeArr[1]);
                            sourceCubeArr[2] = isEmpty(sourceCubeArr[2]) ? 0 : parseFloat(sourceCubeArr[2]);
                            sourceCubeArr[3] = isEmpty(sourceCubeArr[3]) ? 0 : parseFloat(sourceCubeArr[3]);
                            sourceCubeArr[4] = isEmpty(sourceCubeArr[4]) ? 0 : parseFloat(sourceCubeArr[4]);

                            returnTypeArr[0] = myJqGrid.getFormFieldValue('ReturnType1');
                            returnTypeArr[1] = myJqGrid.getFormFieldValue('ReturnType2');
                            returnTypeArr[2] = myJqGrid.getFormFieldValue('ReturnType3');
                            returnTypeArr[3] = myJqGrid.getFormFieldValue('ReturnType4');
                            returnTypeArr[4] = myJqGrid.getFormFieldValue('ReturnType5');

                            if (isEmpty(carID1) && isEmpty(carID2) && isEmpty(carID3) && isEmpty(carID4) && isEmpty(carID5)) {
                                errorMsg += "<br>保存前请进行合并操作!";
                                checking = false;
                            }

                            if ($('#tr1', '#mergeFormZT').css('display') == 'block') {
                                //必须要填剩退类别
                                //alert($('#ReturnType6', '#mergeFormZT').val());
                                if ($('#ReturnType6', '#mergeFormZT').val() == '') {
                                    $('#ReturnType6', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID1)) {
                                if (isEmpty(sourceCubeArr[0])) {
                                    $('#Cube1', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[0])) {
                                    $('#ReturnType1', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[0] <= 0) {
                                    $('#Cube1', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[0] > lastCube1) {
                                    $('#Cube1', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于转料方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID2)) {
                                if (isEmpty(sourceCubeArr[1])) {
                                    $('#Cube2', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[1])) {
                                    $('#ReturnType2', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[1] <= 0) {
                                    $('#Cube2', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[1] > lastCube2) {
                                    $('#Cube2', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于转料方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID3)) {
                                if (isEmpty(sourceCubeArr[2])) {
                                    $('#Cube3', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[2])) {
                                    $('#ReturnType3', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[2] <= 0) {
                                    $('#Cube3', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[2] > lastCube3) {
                                    $('#Cube3', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于转料方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID4)) {
                                if (isEmpty(sourceCubeArr[3])) {
                                    $('#Cube4', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[3])) {
                                    $('#ReturnType4', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[3] <= 0) {
                                    $('#Cube4', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[3] > lastCube4) {
                                    $('#Cube4', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于转料方量。";
                                    checking = false;
                                }
                            }

                            if (!isEmpty(carID5)) {
                                if (isEmpty(sourceCubeArr[4])) {
                                    $('#Cube5', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>方量 字段是必需的。";
                                    checking = false;
                                }

                                if (isEmpty(returnTypeArr[4])) {
                                    $('#ReturnType5', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退类别 字段是必需的。";
                                    checking = false;
                                }

                                if (sourceCubeArr[4] <= 0) {
                                    $('#Cube5', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量必须大于0。";
                                    checking = false;
                                }

                                if (sourceCubeArr[4] > lastCube5) {
                                    $('#Cube5', '#mergeFormZT').addClass('input-validation-error');
                                    errorMsg += "<br>剩退方量不能大于转料方量。";
                                    checking = false;
                                }
                            }

                            if (sourceCubeArr[0] + sourceCubeArr[1] + sourceCubeArr[2] + sourceCubeArr[3] + sourceCubeArr[4] > maxcube) {
                                errorMsg += "<br>合并方量不能大于车辆容重" + maxcube + "方。";
                                checking = false;
                            }



                            if ((!isEmpty(carID1) && !isEmpty(carID2) && carID1 == carID2 && returnTypeArr[0] == returnTypeArr[1])
                                || (!isEmpty(carID1) && !isEmpty(carID3) && carID1 == carID3 && returnTypeArr[0] == returnTypeArr[2])
                                || (!isEmpty(carID1) && !isEmpty(carID4) && carID1 == carID4 && returnTypeArr[0] == returnTypeArr[3])
                                || (!isEmpty(carID1) && !isEmpty(carID5) && carID1 == carID5 && returnTypeArr[0] == returnTypeArr[4])
                                || (!isEmpty(carID2) && !isEmpty(carID3) && carID2 == carID3 && returnTypeArr[1] == returnTypeArr[2])
                                || (!isEmpty(carID2) && !isEmpty(carID4) && carID2 == carID4 && returnTypeArr[1] == returnTypeArr[3])
                                || (!isEmpty(carID2) && !isEmpty(carID5) && carID2 == carID5 && returnTypeArr[1] == returnTypeArr[4])
                                || (!isEmpty(carID3) && !isEmpty(carID4) && carID3 == carID4 && returnTypeArr[2] == returnTypeArr[3])
                                || (!isEmpty(carID3) && !isEmpty(carID5) && carID3 == carID5 && returnTypeArr[2] == returnTypeArr[4])
                                || (!isEmpty(carID4) && !isEmpty(carID5) && carID4 == carID5 && returnTypeArr[3] == returnTypeArr[4])) {
                                errorMsg += "<br>请选择不同的车进行合并。";
                                checking = false;
                            }

                            var targetCube = myJqGrid.getFormFieldValue("Cube");
                            targetCube = isEmpty(targetCube) ? 0 : parseFloat(targetCube);
                            if (sourceCubeArr[0] * 1000 + sourceCubeArr[1] * 1000 + sourceCubeArr[2] * 1000 + sourceCubeArr[3] * 1000 + sourceCubeArr[4] * 1000 != targetCube * 1000) {
                                errorMsg += "<br>合并方量之和与目标剩退方量不相等!";
                                checking = false;
                            }

                            //目标与源不能一样
                            if (carID1 == carID || carID2 == carID || carID3 == carID || carID4 == carID || carID5 == carID) {
                                errorMsg += "<br>源车号不能与目标车号一样！";
                                checking = false;
                            }

                            if (checking == false || !checking) {
                                showError('验证错误', errorMsg);
                                return false;
                            }

                            return true;
                        },
                        postData: { sourceShipDocIDArr: sourceShipDocIDArr, sourceCubeArr: sourceCubeArr, returnTypeArr: returnTypeArr, returnTypeArrTarget: returnTypeArrTarget }
                    });
                }

                , tzHistory: function (btn) {
                    showDocHis(btn);
                }


               , handlePrint: function (btn) {
                   if (myJqGrid.isSelectedOnlyOne('请选择一条记录进行打印')) {
                       var record = myJqGrid.getSelectedRecord();
                       if (isEmpty(record.TargetShipDocID) || record.IsCompleted == "false") {
                           showError('错误', '该转退料记录没有完成或目标工地未设置，不能打印');
                           return;
                       }
                       showConfirm('确认', '确认打印？', function () {
                           printShippingDoc('print', record.TargetShipDocID);
                           $(this).dialog('close');
                       });
                   }
               }
            }
    });

       //grid-单击行事件
       myJqGrid.addListeners('rowclick', function (id, record, selBool) {
           var tzRalationFlag = record["TzRalationFlag"];
           myJqGridH.refreshGrid("TzRalationFlag='" + tzRalationFlag + "' and IsLock<>-1");
       });

           function lockFmt1(cellvalue, options, rowObject) {
               if (cellvalue == '0') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "录入";
                   }

               }
               else if (cellvalue == '1') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "分车";
                   }

               }
               else if (cellvalue == '2') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "合车";
                   }

               }
               else if (cellvalue == '-1') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "删除(分车)";
                   }
               }
               else if (cellvalue == '3') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "修改";
                   }
               }
               else if (cellvalue == '4') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "修改（磅秤）";
                   }
               }
               else if (cellvalue == '5') {
                   var style = "color:green;";
                   if (typeof (options.colModel.formatterStyle) != "undefined") {
                       var txt = options.colModel.formatterStyle[1];
                   } else {
                       var txt = "暗扣";
                   }
               }
               else {
                   var txt = '';
               }

               return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
           }

           function lockUnFmt1(cellvalue, options, cell) {
               return $('span', cell).attr('rel');
           }

           var myJqGrid1 = new MyGrid({
               renderTo: 'shipDocHisGrid'
            , width: 720
            , height: 240
            , storeURL: opts.findShipDocHisUrl
            , sortByField: 'BuildTime'
            , sortOrder: 'ASC'
            , primaryKey: 'ID'
            , setGridPageSize: 30
            , singleSelect: true
            , showPageBar: true
            , toolbarSearch: false
            , emptyrecords: '当前无任何修改'
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 60, hidden: true }
                , { label: '转退料编号', name: 'ParentID', index: 'ParentID', width: 150, hidden: true }
                , { label: '初始源运输单号', name: 'SourceShipDocID', index: 'SourceShipDocID', width: 150 }
                , { label: '初始源工程名称', name: 'SourceProjectName', index: 'SourceShippingDocument.ProjectName' }
                , { label: '初始源运输车号', name: 'SourceCarID', index: 'SourceShippingDocument.CarID', width: 150 }
                , { label: '初始源剩退方量', name: 'SourceCube', index: 'SourceCube', width: 150, align: 'center' }
                , { label: '初始源砼强度', name: 'SourceConStrength', index: 'SourceShippingDocument.ConStrength', width: 150 }
                , { label: '操作状态', name: 'IsLock', index: 'IsLock', formatter: lockFmt1, unformat: lockUnFmt1, width: 150 }

                , { label: '源车号', name: 'operationnum', index: 'operationnum', width: 100 }
                , { label: '源方量', name: 'operationcube', index: 'operationcube', width: 100 }
                , { label: '目标车号', name: 'CarID', index: 'CarID', width: 100 }
                , { label: '目标方量', name: 'Cube', index: 'Cube', width: 100 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime' }
                , { label: '方式', name: 'operation', index: 'operation', width: 100, hidden: true }
		    ]
		    , autoLoad: false
           });

        

       //查看运输单对应的历史记录
       function showDocHis(b) {
           var title = "转退料历史记录";
           $("#shipDocHisWindow").dialog({ modal: true, autoOpen: false, bgiframe: true, resizable: false, width: 750, height: 340, title: title,
               close: function (event, ui) {
                   $(this).dialog("destroy");
                   myJqGrid1.getJqGrid().jqGrid('clearGridData'); //关闭窗口时清除grid中的数据
               }
           });
           $('#shipDocHisWindow').dialog('open');
           myJqGrid1.refreshGrid();
       }


    bindCarChangeEvent('#forwardForm');
    bindCarChangeEvent('#MyFormDiv');
    //SplitCarChange();
    //MergeCarChange();
    MergeCarChangeZT();
    SplitZTCarChange();

    //移除整车转发
    $('#ReturnType').children('option[value=RT0]').remove();
    $('#ReturnType', '#splitForm').children('option[value=RT0]').remove();
    $('#ReturnType1', '#mergeForm').children('option[value=RT0]').remove();
    $('#ReturnType2', '#mergeForm').children('option[value=RT0]').remove();
    $('#ReturnType3', '#mergeForm').children('option[value=RT0]').remove();
    $('#ReturnType4', '#mergeForm').children('option[value=RT0]').remove();
    $('#ReturnType5', '#mergeForm').children('option[value=RT0]').remove();

    $('#ReturnType1', '#mergeFormZT').children('option[value=RT0]').remove();
    $('#ReturnType2', '#mergeFormZT').children('option[value=RT0]').remove();
    $('#ReturnType3', '#mergeFormZT').children('option[value=RT0]').remove();
    $('#ReturnType4', '#mergeFormZT').children('option[value=RT0]').remove();
    $('#ReturnType5', '#mergeFormZT').children('option[value=RT0]').remove();
    $('#ReturnType6', '#mergeFormZT').children('option[value=RT0]').remove();
    
    //绑定车号改变事件
    function bindCarChangeEvent(container) {
        $('#CarID', container).bind('change', function () {
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                $('table.info', container).remove(); //上车发货信息 移除
                //窗体高度、宽度
                $('#MyFormDiv').height(218); 
                
                if (response.Result) {//AJAX成功
                    $('#MyFormDiv').height(300);
                    
                    $('#tmplShipDoc').tmpl(response.Data).insertAfter('table.mvcform', container); //上车发货信息 插入下面
                    myJqGrid.setFormFieldValue('Driver', response.Data.Driver); //设置 当班司机
                    myJqGrid.setFormFieldValue('SourceShipDocID', response.Data.ID)
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#CarID', container).val('');
                }
            });
        });
    }

    function dropvalue(obj) {
        var carid1 = $('#CarID1', '#splitFormByZT').val();
        var carid2 = $('#CarID2', '#splitFormByZT').val();
        var carid3 = $('#CarID3', '#splitFormByZT').val();
        var carid4 = $('#CarID4', '#splitFormByZT').val();
        var carid5 = $('#CarID5', '#splitFormByZT').val();
//        alert(carid1 + "|" + carid2 + "|" + carid3 + "|" + carid4 + "|" + carid5 + ":" + _carid);
        var i = 0;
        if (carid1 == _carid)
            i++;
        if (carid2 == _carid)
            i++;
        if (carid3 == _carid)
            i++;
        if (carid4 == _carid)
            i++;
        if (carid5 == _carid)
            i++;
//        alert(i);
        if (i != 1) {
            return false;
        }
        else {
            if ($(obj).val() == _carid)
                return true;
            else {
                return false;
            }
        }
    }

    function SplitZTCarChange() {
        $('#SourceCarID', '#splitFormByZT').unbind('change');
        $('#SourceCarID', '#splitFormByZT').bind('change', function () {
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                $('table.info', '#splitFormByZT').remove();
                if (response.Result) {
                    $('#tmplShipDoc').tmpl(response.Data).insertAfter('table.mvcform', '#splitFormByZT');
                    myJqGrid.setFormFieldValue('Driver', response.Data.Driver);
                    myJqGrid.setFormFieldValue('SourceShipDocID', response.Data.ID);
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceCarID', '#splitFormByZT').val('');
                }
            });
        });

        //整车转发选择车号不能选择有转退料的
        $('#CarID1', '#forwardForm').unbind('change');
        $('#CarID1', '#forwardForm').bind('change', function () {
            var carId = $(this).val(); 
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID1', '#forwardForm').val('');
                }
                else {

                }
            });
        });

        $('#CarID1', '#splitFormByZT').unbind('change');
        $('#CarID1', '#splitFormByZT').bind('change', function () {
            $('#Cube1', '#splitFormByZT').removeClass('text requiredval');
            $('#ActionType1', '#splitFormByZT').removeClass('text requiredval');
            var carId = $(this).val();
            if (dropvalue(this)) {
                $('#Cube1', '#splitFormByZT').addClass('text requiredval');
                $('#ActionType1', '#splitFormByZT').addClass('text requiredval');
                return;
            }
            ajaxRequest(opts.GetUnCompletedUrl2, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID1', '#splitFormByZT').val('');
                    $('#Cube1', '#splitFormByZT').removeClass('text requiredval');
                    $('#ActionType1', '#splitFormByZT').removeClass('text requiredval');
                }
                else {
                    $('#Cube1', '#splitFormByZT').addClass('text requiredval');
                    $('#ActionType1', '#splitFormByZT').addClass('text requiredval');
                }
            });
        });

        $('#CarID2', '#splitFormByZT').unbind('change');
        $('#CarID2', '#splitFormByZT').bind('change', function () {
            $('#Cube2', '#splitFormByZT').removeClass('text requiredval');
            $('#ActionType2', '#splitFormByZT').removeClass('text requiredval');
            var carId = $(this).val();
            if (dropvalue(this)) {
                $('#Cube2', '#splitFormByZT').addClass('text requiredval');
                $('#ActionType2', '#splitFormByZT').addClass('text requiredval');
                return;
            }
            ajaxRequest(opts.GetUnCompletedUrl2, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID2', '#splitFormByZT').val('');
                    $('#Cube2', '#splitFormByZT').removeClass('text requiredval');
                    $('#ActionType2', '#splitFormByZT').removeClass('text requiredval');
                }
                else {
                    $('#Cube2', '#splitFormByZT').addClass('text requiredval');
                    $('#ActionType2', '#splitFormByZT').addClass('text requiredval');
                }
            });
        });

        $('#CarID3', '#splitFormByZT').unbind('change');
        $('#CarID3', '#splitFormByZT').bind('change', function () {
            $('#Cube3', '#splitFormByZT').removeClass('text requiredval');
            $('#ActionType3', '#splitFormByZT').removeClass('text requiredval');
            var carId = $(this).val();
            if (dropvalue(this)) {
                $('#Cube3', '#splitFormByZT').addClass('text requiredval');
                $('#ActionType3', '#splitFormByZT').addClass('text requiredval');
                return;
            }
            ajaxRequest(opts.GetUnCompletedUrl2, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID3', '#splitFormByZT').val('');
                    $('#Cube3', '#splitFormByZT').removeClass('text requiredval');
                    $('#ActionType3', '#splitFormByZT').removeClass('text requiredval');
                }
                else {
                    $('#Cube3', '#splitFormByZT').addClass('text requiredval');
                    $('#ActionType3', '#splitFormByZT').addClass('text requiredval');
                }
            });
        });

        $('#CarID4', '#splitFormByZT').unbind('change');
        $('#CarID4', '#splitFormByZT').bind('change', function () {
            $('#Cube4', '#splitFormByZT').removeClass('text requiredval');
            $('#ActionType4', '#splitFormByZT').removeClass('text requiredval');
            var carId = $(this).val();
            if (dropvalue(this)) {
                $('#Cube4', '#splitFormByZT').addClass('text requiredval');
                $('#ActionType4', '#splitFormByZT').addClass('text requiredval');
                return;
            }
            ajaxRequest(opts.GetUnCompletedUrl2, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID4', '#splitFormByZT').val('');
                    $('#Cube4', '#splitFormByZT').removeClass('text requiredval');
                    $('#ActionType4', '#splitFormByZT').removeClass('text requiredval');
                }
                else {
                    $('#Cube4', '#splitFormByZT').addClass('text requiredval');
                    $('#ActionType4', '#splitFormByZT').addClass('text requiredval');
                }
            });
        });

        $('#CarID5', '#splitFormByZT').unbind('change');
        $('#CarID5', '#splitFormByZT').bind('change', function () {
            $('#Cube5', '#splitFormByZT').removeClass('text requiredval');
            $('#ActionType5', '#splitFormByZT').removeClass('text requiredval');
            var carId = $(this).val();
            if (dropvalue(this)) {
                $('#Cube5', '#splitFormByZT').addClass('text requiredval');
                $('#ActionType5', '#splitFormByZT').addClass('text requiredval');
                return;
            }
            ajaxRequest(opts.GetUnCompletedUrl2, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID5', '#splitFormByZT').val('');
                    $('#Cube5', '#splitFormByZT').removeClass('text requiredval');
                    $('#ActionType5', '#splitFormByZT').removeClass('text requiredval');
                }
                else {
                    $('#Cube5', '#splitFormByZT').addClass('text requiredval');
                    $('#ActionType5', '#splitFormByZT').addClass('text requiredval');
                }
            });
        });
    }

    function SplitCarChange() {
        $('#SourceCarID', '#splitForm').unbind('change');
        $('#SourceCarID', '#splitForm').bind('change', function () {
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                $('table.info', '#splitForm').remove();
                if (response.Result) {
                    var r = response;
                    ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                        if (!response.Result) {
                            showError('错误', response.Message);
                            $('#SourceCarID', '#splitForm').val('');
                        }
                        else {
                            $('#tmplShipDoc').tmpl(r.Data).insertAfter('table.mvcform', '#splitForm');
                            myJqGrid.setFormFieldValue('Driver', r.Data.Driver);
                            myJqGrid.setFormFieldValue('SourceShipDocID', r.Data.ID);
                        }
                    });


                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceCarID', '#splitForm').val('');
                }
            });


        });

        $('#CarID1', '#splitForm').unbind('change');
        $('#CarID1', '#splitForm').bind('change', function () {            
            $('#Cube1', '#splitForm').removeClass('text requiredval');
            $('#ActionType1', '#splitForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID1', '#splitForm').val('');
                    $('#Cube1', '#splitForm').removeClass('text requiredval');
                    $('#ActionType1', '#splitForm').removeClass('text requiredval');
                }
                else {
                    $('#Cube1', '#splitForm').addClass('text requiredval');
                    $('#ActionType1', '#splitForm').addClass('text requiredval');
                }
            });
        });

        $('#CarID2', '#splitForm').unbind('change');
        $('#CarID2', '#splitForm').bind('change', function () {
            $('#Cube2', '#splitForm').removeClass('text requiredval');
            $('#ActionType2', '#splitForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID2', '#splitForm').val('');
                    $('#Cube2', '#splitForm').removeClass('text requiredval');
                    $('#ActionType2', '#splitForm').removeClass('text requiredval');
                }
                else {
                    $('#Cube2', '#splitForm').addClass('text requiredval');
                    $('#ActionType2', '#splitForm').addClass('text requiredval');
                }
            });
        });

        $('#CarID3', '#splitForm').unbind('change');
        $('#CarID3', '#splitForm').bind('change', function () {            
            $('#Cube3', '#splitForm').removeClass('text requiredval');
            $('#ActionType3', '#splitForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID3', '#splitForm').val('');
                    $('#Cube3', '#splitForm').removeClass('text requiredval');
                    $('#ActionType3', '#splitForm').removeClass('text requiredval');
                }
                else {
                    $('#Cube3', '#splitForm').addClass('text requiredval');
                    $('#ActionType3', '#splitForm').addClass('text requiredval');
                }
            });
        });

        $('#CarID4', '#splitForm').unbind('change');
        $('#CarID4', '#splitForm').bind('change', function () {
            $('#Cube4', '#splitForm').removeClass('text requiredval');
            $('#ActionType4', '#splitForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID4', '#splitForm').val('');
                    $('#Cube4', '#splitForm').removeClass('text requiredval');
                    $('#ActionType4', '#splitForm').removeClass('text requiredval');
                }
                else {
                    $('#Cube4', '#splitForm').addClass('text requiredval');
                    $('#ActionType4', '#splitForm').addClass('text requiredval');
                }
            });
        });

        $('#CarID5', '#splitForm').unbind('change');
        $('#CarID5', '#splitForm').bind('change', function () {
            $('#Cube5', '#splitForm').removeClass('text requiredval');
            $('#ActionType5', '#splitForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID5', '#splitForm').val('');
                    $('#Cube5', '#splitForm').removeClass('text requiredval');
                    $('#ActionType5', '#splitForm').removeClass('text requiredval');
                }
                else {
                    $('#Cube5', '#splitForm').addClass('text requiredval');
                    $('#ActionType5', '#splitForm').addClass('text requiredval');
                }
            });
        });
    }


    //转退料合并 下拉框
    function MergeCarChangeZT() {
        $('#CarID1', '#mergeFormZT').unbind('change');
        $('#CarID1', '#mergeFormZT').bind('change', function () {
            $('#Cube1', '#mergeFormZT').removeClass('text requiredval');
            $('#ReturnType1', '#mergeFormZT').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl1, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID1', '#mergeFormZT').val(response.Data.ID);
                    $('#LastCube1', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube1', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube1', '#mergeFormZT').addClass('text requiredval');
                    $('#ReturnType1', '#mergeFormZT').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的转退料记录' : response.Message);
                    $('#SourceShipDocID1', '#mergeFormZT').val('');
                    $('#LastCube1', '#mergeFormZT').val('');
                    $('#CarID1', '#mergeFormZT').val('');
                    $('#Cube1', '#mergeFormZT').removeClass('text requiredval');
                    $('#ReturnType1', '#mergeFormZT').removeClass('text requiredval');
                }
            });
        });

        $('#CarID2', '#mergeFormZT').unbind('change');
        $('#CarID2', '#mergeFormZT').bind('change', function () {
            $('#Cube2', '#mergeFormZT').removeClass('text requiredval');
            $('#ReturnType2', '#mergeFormZT').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl1, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID2', '#mergeFormZT').val(response.Data.ID);
                    $('#LastCube2', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube2', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube2', '#mergeFormZT').addClass('text requiredval');
                    $('#ReturnType2', '#mergeFormZT').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的转退料记录' : response.Message);
                    $('#SourceShipDocID2', '#mergeFormZT').val('');
                    $('#LastCube2', '#mergeFormZT').val('');
                    $('#CarID2', '#mergeFormZT').val('');
                    $('#Cube2', '#mergeFormZT').removeClass('text requiredval');
                    $('#ReturnType2', '#mergeFormZT').removeClass('text requiredval');
                }
            });
        });

        $('#CarID3', '#mergeFormZT').unbind('change');
        $('#CarID3', '#mergeFormZT').bind('change', function () {
            $('#Cube3', '#mergeFormZT').removeClass('text requiredval');
            $('#ReturnType3', '#mergeFormZT').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl1, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID3', '#mergeFormZT').val(response.Data.ID);
                    $('#LastCube3', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube3', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube3', '#mergeFormZT').addClass('text requiredval');
                    $('#ReturnType3', '#mergeFormZT').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的转退料记录' : response.Message);
                    $('#SourceShipDocID3', '#mergeFormZT').val('');
                    $('#LastCube3', '#mergeFormZT').val('');
                    $('#CarID3', '#mergeFormZT').val('');
                    $('#Cube3', '#mergeFormZT').removeClass('text requiredval');
                    $('#ReturnType3', '#mergeFormZT').removeClass('text requiredval');
                }
            });
        });

        $('#CarID4', '#mergeFormZT').unbind('change');
        $('#CarID4', '#mergeFormZT').bind('change', function () {
            $('#Cube4', '#mergeFormZT').removeClass('text requiredval');
            $('#ReturnType4', '#mergeFormZT').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl1, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID4', '#mergeFormZT').val(response.Data.ID);
                    $('#LastCube4', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube4', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube4', '#mergeFormZT').addClass('text requiredval');
                    $('#ReturnType4', '#mergeFormZT').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的转退料记录' : response.Message);
                    $('#SourceShipDocID4', '#mergeFormZT').val('');
                    $('#LastCube4', '#mergeFormZT').val('');
                    $('#CarID4', '#mergeFormZT').val('');
                    $('#Cube4', '#mergeFormZT').removeClass('text requiredval');
                    $('#ReturnType4', '#mergeFormZT').removeClass('text requiredval');
                }
            });
        });

        $('#CarID5', '#mergeFormZT').unbind('change');
        $('#CarID5', '#mergeFormZT').bind('change', function () {
            $('#Cube5', '#mergeFormZT').removeClass('text requiredval');
            $('#ReturnType5', '#mergeFormZT').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl1, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID5', '#mergeFormZT').val(response.Data.ID);
                    $('#LastCube5', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube5', '#mergeFormZT').val(response.Data.Cube);
                    $('#Cube5', '#mergeFormZT').addClass('text requiredval');
                    $('#ReturnType5', '#mergeFormZT').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的转退料记录' : response.Message);
                    $('#SourceShipDocID5', '#mergeFormZT').val('');
                    $('#LastCube5', '#mergeFormZT').val('');
                    $('#CarID5', '#mergeFormZT').val('');
                    $('#Cube5', '#mergeFormZT').removeClass('text requiredval');
                    $('#ReturnType5', '#mergeFormZT').removeClass('text requiredval');
                }
            });
        });

        $('#CarID', '#mergeFormZT').unbind('change');
        $('#CarID', '#mergeFormZT').bind('change', function () {
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl1, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID', '#mergeFormZT').val('');
                    $('#tr1', '#mergeFormZT').css('display', 'none');
                    $('#ReturnType6', '#mergeFormZT').removeClass('text requiredval');
                }
                else {
                    $('#maxcube', '#mergeFormZT').val(response.Data.MaxCube);
                    if (response.Data.Modifier == 'yes') {
                        $('#tr1', '#mergeFormZT').css('display', 'block');
                        $('#ReturnType6', '#mergeFormZT').addClass('text requiredval');
                    }
                    else {
                        $('#tr1', '#mergeFormZT').css('display', 'none');
                        $('#ReturnType6', '#mergeFormZT').removeClass('text requiredval');
                    }
                }
            });
        });

    }



    function MergeCarChange() {
        $('#CarID1', '#mergeForm').unbind('change');
        $('#CarID1', '#mergeForm').bind('change', function () {
            $('#Cube1', '#mergeForm').removeClass('text requiredval');
            $('#ReturnType1', '#mergeForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID1', '#mergeForm').val(response.Data.ID);
                    $('#LastCube1', '#mergeForm').val(response.Data.ParCube);
                    $('#Cube1', '#mergeForm').addClass('text requiredval');
                    $('#ReturnType1', '#mergeForm').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceShipDocID1', '#mergeForm').val('');
                    $('#LastCube1', '#mergeForm').val('');
                    $('#CarID1', '#mergeForm').val('');
                    $('#Cube1', '#mergeForm').removeClass('text requiredval');
                    $('#ReturnType1', '#mergeForm').removeClass('text requiredval');
                }
            });
        });

        $('#CarID2', '#mergeForm').unbind('change');
        $('#CarID2', '#mergeForm').bind('change', function () {
            $('#Cube2', '#mergeForm').removeClass('text requiredval');
            $('#ReturnType2', '#mergeForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID2', '#mergeForm').val(response.Data.ID);
                    $('#LastCube2', '#mergeForm').val(response.Data.ParCube);
                    $('#Cube2', '#mergeForm').addClass('text requiredval');
                    $('#ReturnType2', '#mergeForm').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceShipDocID2', '#mergeForm').val('');
                    $('#LastCube2', '#mergeForm').val('');
                    $('#CarID2', '#mergeForm').val('');
                    $('#Cube2', '#mergeForm').removeClass('text requiredval');
                    $('#ReturnType2', '#mergeForm').removeClass('text requiredval');
                }
            });
        });

        $('#CarID3', '#mergeForm').unbind('change');
        $('#CarID3', '#mergeForm').bind('change', function () {
            $('#Cube3', '#mergeForm').removeClass('text requiredval');
            $('#ReturnType3', '#mergeForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID3', '#mergeForm').val(response.Data.ID);
                    $('#LastCube3', '#mergeForm').val(response.Data.ParCube);
                    $('#Cube3', '#mergeForm').addClass('text requiredval');
                    $('#ReturnType3', '#mergeForm').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceShipDocID3', '#mergeForm').val('');
                    $('#LastCube3', '#mergeForm').val('');
                    $('#CarID3', '#mergeForm').val('');
                    $('#Cube3', '#mergeForm').removeClass('text requiredval');
                    $('#ReturnType3', '#mergeForm').removeClass('text requiredval');
                }
            });
        });

        $('#CarID4', '#mergeForm').unbind('change');
        $('#CarID4', '#mergeForm').bind('change', function () {
            $('#Cube4', '#mergeForm').removeClass('text requiredval');
            $('#ReturnType4', '#mergeForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID4', '#mergeForm').val(response.Data.ID);
                    $('#LastCube4', '#mergeForm').val(response.Data.ParCube);
                    $('#Cube4', '#mergeForm').addClass('text requiredval');
                    $('#ReturnType4', '#mergeForm').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceShipDocID4', '#mergeForm').val('');
                    $('#LastCube4', '#mergeForm').val('');
                    $('#CarID4', '#mergeForm').val('');
                    $('#Cube4', '#mergeForm').removeClass('text requiredval');
                    $('#ReturnType4', '#mergeForm').removeClass('text requiredval');
                }
            });
        });

        $('#CarID5', '#mergeForm').unbind('change');
        $('#CarID5', '#mergeForm').bind('change', function () {
            $('#Cube5', '#mergeForm').removeClass('text requiredval');
            $('#ReturnType5', '#mergeForm').removeClass('text requiredval');
            var carId = $(this).val();
            ajaxRequest(opts.getLastDocUrl, { carId: carId }, false, function (response) {
                if (response.Result) {
                    $('#SourceShipDocID5', '#mergeForm').val(response.Data.ID);
                    $('#LastCube5', '#mergeForm').val(response.Data.ParCube);
                    $('#Cube5', '#mergeForm').addClass('text requiredval');
                    $('#ReturnType5', '#mergeForm').addClass('text requiredval');
                }
                else {
                    showError('错误', isEmpty(response.Message) ? '未找到该车的发货记录' : response.Message);
                    $('#SourceShipDocID5', '#mergeForm').val('');
                    $('#LastCube5', '#mergeForm').val('');
                    $('#CarID5', '#mergeForm').val('');
                    $('#Cube5', '#mergeForm').removeClass('text requiredval');
                    $('#ReturnType5', '#mergeForm').removeClass('text requiredval');
                }
            });
        });

        $('#CarID', '#mergeForm').unbind('change');
        $('#CarID', '#mergeForm').bind('change', function () {
            var carId = $(this).val();
            ajaxRequest(opts.GetUnCompletedUrl, { carId: carId }, false, function (response) {
                if (!response.Result) {
                    showError('错误', response.Message);
                    $('#CarID', '#mergeForm').val('');
                }
            });
        });

    }
}