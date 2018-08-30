//车辆加油
function caroilIndexInit(opts) {

    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'AddDate'
		    , primaryKey: 'ID'
            //  , groupField: 'Car.CarNo'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 530
            , dialogHeight: 350
            , singleSelect: true
            , rowNumbers: true
            , footerRow:true
           //, groupingView: { groupSummary: [true], groupDataSorted: true }
		    , initArray: [
                { label: '加油单号', name: 'ID', index: 'ID', width: 80 }
                , { label: '是否确认', name: 'Status', index: 'Status', sortable: false, width: 50, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
                , { label: '车辆代号', name: 'CarID', align: 'right', index: 'CarID', sortable: false, width: 80 }
                , { label: '车辆代号', name: 'Car.CarNo', align: 'right', index: 'Car.CarNo', sortable: false, width: 80 }
                , { label: '司机', name: 'Driver', index: 'Driver', width: 80 }
                , { label: '加油枪号', name: 'OilMechID', index: 'OilMechID', align: 'center', width: 80 }
                , { label: '油库名称', name: 'SiloName', jsonmap: 'Silo.SiloName', index: 'Silo.SiloName', align: 'center', width: 80 }
                , { label: '油料名称', name: 'StuffName', jsonmap: 'StuffInfo.StuffName', index: 'StuffInfo.StuffName', align: 'center', width: 80 }
                , { label: '加油日期', name: 'AddDate', index: 'AddDate', formatter: 'datetime', align: 'center', searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { name: 'CarID', hidden: true }
                , { name: 'SiloID', hidden: true }
                , { name: 'StuffID', hidden: true }
                , { label: '加油量', name: 'Amount', index: 'Amount', width: 100, align: 'right', summaryType: 'sum', summaryTpl: '小计:{0}L' }
                , { label: '车辆仪表公里数', name: 'ThisKM', index: 'ThisKM', width: 100, align: 'right' }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width: 60, align: 'right', summaryType: 'avg', summaryTpl: '￥{0}' }
                , { label: '总价', name: 'TotalPrice', index: 'TotalPrice', width: 60, align: 'right', summaryType: 'sum', summaryTpl: '￥{0}' }

        //, { label: '上次里程数', name: 'LastKM', index: 'LastKM', width: 80, align: 'right' }
        //, { label: '里程数', name: 'KiloMeter', index: 'KiloMeter', width: 80, align: 'right' }
                , { label: '备注', name: 'Memo', index: 'Memo' }
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
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            var mydate = new Date();
                            var a = mydate.toLocaleString();
                            $("#AddDate").val(moment().format('YYYY-MM-DD hh:mm:ss'));

                            var carid = $('#CarID').val();
                            $('#CarID').bind('change', carListChange)
                        }
                        , beforeSubmit: calcKiloMeter
                    });
                },
                handleEdit: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.Status == '1') {
                        showError('该记录已进行确认，不允许修改！');
                        return;
                    };
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn/* ,
                       afterFormLoaded: function () {
                            $('#CarID').unbind('change');
                            var record = myJqGrid.getSelectedRecord();
                            myJqGrid.getFormField("Amount_T").val(record.Amount / 1000);

                            myJqGrid.getFormField("Amount_T").bind('blur', function () {
                                var Amount_T = myJqGrid.getFormField("Amount_T").val();
                                myJqGrid.getFormField("Amount").val(Amount_T * 1000);
                            });
                        }*/
                        , beforeSubmit: calcKiloMeter
                    });
                },
                handleEditOilPrice: function (btn) {
                    //                    myJqGrid.handleEdit({
                    //                        loadFrom: 'SetPriceDiv',
                    //                        btn: btn
                    //                    });
                    myJqGrid.showWindow({
                        btn: btn,
                        title: '油价修改（大量数据修改时请不要进行其他操作，否则有可能造成数据错误）',
                        width: 450,
                        height: 250,
                        autoOpen: false,
                        loadFrom: 'SetPriceDiv',
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "执行修改":
                                function () {

                                    //执行ajax请求
                                    var postData = $("#SetPriceDiv form").serialize();
                                    var postUrl = btn.data.Url;
                                    var sdate = $("[name='CarOilPrice.BeginTime']").val(); //开始日期
                                    var edate = $("[name='CarOilPrice.EndTime']").val();   //结束日期
                                    if (sdate == "") {
                                        showError('错误', '开始日期不能为空！');
                                        return;
                                    }
                                    if (edate == "") {
                                        showError('错误', '结束日期不能为空！');
                                        return;
                                    }
                                    if (compareTime(sdate + " 00:00:00", edate + " 23:59:59")) {
                                        showError('错误', '开始日期不能大于结束日期！');
                                        return;
                                    }
                                    var price = $("[name='CarOilPrice.Price']").val()   //价格
                                    if (price == "") {
                                        showError('错误', '价格不能为空！');
                                        return;
                                    }
                                    if (price <= 0) {
                                        showError('错误', '价格必须大于0！');
                                        return;
                                    }

                                    document.getElementById("showProgress").style.display = ""; //显示执行提示
                                    $(".ui-dialog-buttonset").button({ disabled: true }); //下面按钮禁用
                                    //$("#SetPriceDiv").dialog("option", "hide", "slide");隐藏/显示-动画样式
                                    $(".ui-dialog-titlebar-close ui-corner-all").hide();
                                    //附加额外的参数
                                    ajaxRequest(postUrl, postData, true, function (response) {
                                        var closeDialog = true;
                                        if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                            closeDialog = false;
                                        }

                                        //窗口关闭处理                        
                                        if (response.Result && closeDialog) {

                                            $("#SetPriceDiv").dialog('close');
                                            $("#SetPriceDiv form")[0].reset();
                                        }
                                        $(".ui-dialog-buttonset").button({ disabled: false }); //下面按钮启用
                                        document.getElementById("showProgress").style.display = "none"; //隐藏执行提示
                                    });
                                    myJqGrid.refreshGrid();
                                }
                        }
                    })
                }
                ,
                handleConfirm: function (btn) {
                    myJqGrid.showWindow({
                        btn: btn,
                        title: '加油确认（大量数据修改时请不要进行其他操作，否则有可能造成数据错误）',
                        width: 350,
                        height: 150,
                        loadFrom: 'ConfirmDiv',
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "执行确认": function () {
                                //执行ajax请求
                                var postData = $("#ConfirmDiv form").serialize();
                                var postUrl = btn.data.Url;

                                //                                var rate = $("#rate").val();   //换算率
                                //                                if (rate == "") {
                                //                                    showError('错误', '油料换算率不能为空！');
                                //                                    return;
                                //                                }
                                //                                if (isNaN(rate)) {
                                //                                    showError('错误', '油料换算率不是数字！');
                                //                                    return;
                                //                                }
                                //                                if (rate<=0) {
                                //                                    showError('错误', '油料换算率不能小于等于0！');
                                //                                    return;
                                //                                }
                                document.getElementById("showProgress2").style.display = ""; //显示执行提示
                                $(".ui-dialog-buttonset").hide(); //下面按钮隐藏
                                //附加额外的参数
                                ajaxRequest(postUrl, postData, true, function (response) {
                                    var closeDialog = true;

                                    if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                        closeDialog = false;
                                    }

                                    //窗口关闭处理                        
                                    if (response.Result && closeDialog) {
                                        $("#ConfirmDiv").dialog('close');
                                        $("#ConfirmDiv form")[0].reset();
                                    }
                                    document.getElementById("showProgress2").style.display = "none"; //隐藏执行提示
                                    $(".ui-dialog-buttonset").show(); //
                                });
                                myJqGrid.refreshGrid();
                            }
                        }
                    })
                }
                , handleDelete: function (btn) {
                    var data = myJqGrid.getSelectedRecord();
                    if (data && data.Status == '1') {
                        showError('该记录已进行确认，不允许删除！');
                        return;
                    };
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
    //车辆选中事件处理
    function carListChange(select) {
        var carid = select.target.value;
        if (isEmpty(carid)) {
            return;
        }
        ajaxRequest(opts.getKiloMeter, { carid: carid }, false, function (response) {
            if (response.Result) {
                myJqGrid.getFormField("LastKM").val(response.Data);
            };
        });
    }
    $('#SiloID').bind('change', function () {
        var siloId = $(this).val();
        if (isEmpty(siloId))
            return;
        var stuff = $('#StuffID');
        stuff.empty();
        ajaxRequest(opts.getSiloUrl,
        { id: siloId },
        false,
        function (response) {
            if (response.Result && response.Data.StuffInfo) {

                stuff.append($('<option value="' + response.Data.StuffInfo.ID + '">' + response.Data.StuffInfo.StuffName + '</option>'));
            }
        });
    });
    function calcTotalPrice() {
        var amount = $('#Amount').val() * 1;

        var unitprice = $('#UnitPrice').val() * 1;
        $('#TotalPrice').val(amount * unitprice);
    }
    $('#Amount').bind('change', calcTotalPrice);
    $('#UnitPrice').bind('change', calcTotalPrice);

    function calcKiloMeter() {
        var lastKM = $('#LastKM').val() * 1;
        var thisKM = $('#ThisKM').val() * 1;
        if (thisKM < lastKM) {
            showError('本次量程表数不应小于上次里程表数');
            return;
        }
        //$('#KiloMeter').val(thisKM - lastKM);
        return true;
    }

    
    //$('#ThisKM').bind('change', calcKiloMeter);
    //$('#LastKM').bind('change', calcKiloMeter);
}