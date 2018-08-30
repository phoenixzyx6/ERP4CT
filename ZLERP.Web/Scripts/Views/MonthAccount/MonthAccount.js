//材料月结
function customerIndexInit(storeUrl,storeUrl2, getPinYin) {

    //左边的Grid
    var monthAccount1Grid = new MyGrid({
        renderTo: 'monthAccount1'
        , autoWidth: true
            , height: gGridHeight
            , primaryKey: 'MonthAccountId'
            , sortByField: 'Month'
            , showPageBar: true
            , setGridPageSize: 30
            , buttons: buttons0
            , dialogWidth: 300
            , dialogHeight: 150
            , storeURL: storeUrl
            , storeCondition: '1=1'
            , autoLoad: true
		    , initArray: [
                { label: '月结月份', name: 'Month', index: 'Month', width: 60 },
   		        { label: '备注', name: 'Meno', index: 'Meno' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    monthAccount1Grid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    monthAccount1Grid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    customersgrid.showWindow({
                        btn: btn,
                        title: '月结处理',
                        width: 370,
                        height: 230,
                        loadFrom: 'custForm',
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "月结确认": function () {

                                showConfirm('确认', '确定要执行月结处理，是否确定继续操作？', function () {
                                    //执行ajax请求
                                    var postData = $("#custForm form").serialize();
                                    var postUrl = btn.data.Url;

                                    var month = $("[name='Month']").val()   //
                                    if (month == "") {
                                        showError('错误', '月份不能为空！');
                                        return;
                                    }

                                    document.getElementById("showProgress").style.display = ""; //显示执行提示
                                    $(".ui-dialog-buttonset").hide(); //下面按钮隐藏
                                    //附加额外的参数
                                    ajaxRequest(postUrl, postData, true, function (response) {
                                        var closeDialog = true;

                                        if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                            closeDialog = false;
                                        }

                                        //窗口关闭处理                        
                                        if (response.Result && closeDialog) {
                                            $("#custForm").dialog('close');
                                            $("#custForm form")[0].reset();
                                        }
                                        document.getElementById("showProgress").style.display = "none"; //隐藏执行提示
                                        $(".ui-dialog-buttonset").show(); //
                                    });
                                    customersgrid.refreshGrid();
                                });
                            }
                        }
                    })
                },
                handleAdd2: function (btn) {//对应菜单功能代码
                    customersgrid.showWindow({
                        btn: btn,
                        title: '月结处理(存储过程)',
                        width: 370,
                        height: 230,
                        loadFrom: 'custForm',
                        buttons: {
                            "关闭": function () {
                                $(this).dialog('close');
                            }, "月结确认": function () {

                                showConfirm('确认', '确定要执行月结处理，是否确定继续操作？', function () {
                                    //执行ajax请求
                                    var postData = $("#custForm form").serialize();
                                    var postUrl = btn.data.Url;

                                    var month = $("[name='Month']").val()   //
                                    if (month == "") {
                                        showError('错误', '月份不能为空！');
                                        return;
                                    }

                                    document.getElementById("showProgress").style.display = ""; //显示执行提示
                                    $(".ui-dialog-buttonset").hide(); //下面按钮隐藏
                                    //附加额外的参数
                                    ajaxRequest(postUrl, postData, true, function (response) {
                                        var closeDialog = true;

                                        if (!isEmpty(this.closeDialog) && !this.closeDialog) {
                                            closeDialog = false;
                                        }

                                        //窗口关闭处理                        
                                        if (response.Result && closeDialog) {
                                            $("#custForm").dialog('close');
                                            $("#custForm form")[0].reset();
                                        }
                                        document.getElementById("showProgress").style.display = "none"; //隐藏执行提示
                                        $(".ui-dialog-buttonset").show(); //
                                    });
                                    customersgrid.refreshGrid();
                                });
                            }
                        }
                    })
                },
                handleEdit: function (btn) {
                    monthAccount1Grid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        prefix: 'check',
                        afterFormLoaded: function () {
                            var checkid = monthAccount1Grid.getSelectedKey();
                            subGrid.refreshGrid("CheckID='" + checkid + "'");
                        }
                    });
                }
                , handleDelete: function (btn) {
                    monthAccount1Grid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                //月结盘点
                , handleMonthPd: function (btn) {
                    monthAccount1Grid.handleAdd({
                        loadFrom: 'MyFormDivMonth',
                        btn: btn,
                        closeDialog: false,
                        width: 700,
                        height: 600,
                        afterFormLoaded: function () {
                            var checkid = monthAccount1Grid.getSelectedKey();
                            subGridMonth.refreshGrid("SiloID IN (SELECT SiloID from SiloProductLine)");
                        },
                        postCallBack: function (response) {
                            monthAccount1Grid.getFormField("check.ID").val(response.Data);
                            var checkid = monthAccount1Grid.getFormField("check.ID").val();
                            subGridMonth.refreshGrid("CheckID='" + checkid + "'");
                        }
                    });
                }
            }
        });

   //右边的Grid
    var customersgrid = new MyGrid({
        renderTo: 'custid'
            , autoWidth: true
            , height: gGridHeight
            , primaryKey: 'MonthAccountId'
            , sortByField: 'MonthAccountId'
            , showPageBar: true
            , setGridPageSize: 30
            , buttons: buttons1
            , dialogWidth: 300
            , dialogHeight: 150
            , storeURL: storeUrl2
            , storeCondition: '1=1'
            , autoLoad: false
            , initArray: [
   		        { label: '月结月份', name: 'Month', index: 'Month', width: 200 },
                //{ label: '筒仓名称', name: 'SiloName', index: 'SiloName' },
                { label: '材料代码', name: 'Stuffid', index: 'Stuffid' },
                { label: '材料名称', name: 'StuffName', index: 'StuffName' },
   		        { label: '月结数量', name: 'Currentcount', index: 'Currentcount' },
   		        { label: '备注', name: 'Meno', index: 'Meno' }

		    ]
            , functions: {
                handleRefresh: function (btn) {
                    customersgrid.refreshGrid('1=1');
                },
                
                handleEdit: function (btn) {
                    customersgrid.handleEdit({
                        btn: btn,
                        loadFrom: 'custForm'
                    });
                },
                handleDelete: function (btn) {
                    customersgrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        , reloadGrid: true

                    });
                },
                ShowAll: function (btn) {
                    customersgrid.refreshGrid('1=1');
                }
            }
    });

    //左边grid-单击行事件
    monthAccount1Grid.addListeners('rowclick', function (id, record, selBool) {
        var month=record["Month"];
        customersgrid.refreshGrid("Month='" + month + "'");

    });
} 