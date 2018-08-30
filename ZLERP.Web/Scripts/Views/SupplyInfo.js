function supplyInfoInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 500
            , dialogHeight: 420
		    , showPageBar: true
            , editSaveUrl: opts.updateStroeUrl
		    , initArray: [
                { label: '厂商代号', name: 'ID', index: 'ID', width: 80 }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                //, { label: '是否启用', name: 'IsUsed', index: 'IsUsed', align: 'center', width: 60, formatter: Checktypeformatter,unformat:ChecktypeUnformat, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'checkbox', editoptions: { value: "true:false"} }
                , { label: '简称', name: 'ShortName', index: 'ShortName' }
                , { label: '厂商类型', name: 'SupplyKind', index: 'SupplyKind', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'SuType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('SuType')} }
                , { label: '厂家全称', name: 'SupplyName', index: 'SupplyName' }
                , { label: '负责人', name: 'Principal', index: 'Principal', width: 80 }
                , { label: '厂家地址', name: 'SupplyAddr', index: 'SupplyAddr' }
                , { label: '发票地址', name: 'InvoiceAddr', index: 'InvoiceAddr' }
                , { label: '开户银行', name: 'BankName', index: 'BankName' }
                , { label: '开户银行帐号', name: 'BankAccount', index: 'BankAccount' }
                , { label: '营业电话', name: 'BusinessTel', index: 'BusinessTel' }
                , { label: '营业传真', name: 'BusinessFax', index: 'BusinessFax' }
                , { label: '邮政编码', name: 'PostCode', index: 'PostCode' }
                , { label: '负责人电话', name: 'PrincipalTel', index: 'PrincipalTel' }
                , { label: '联络人', name: 'LinkMan', index: 'LinkMan' }
                , { label: '联络人手机', name: 'LinkTel', index: 'LinkTel' }
                , { label: '供货类型', name: 'SupplyType', index: 'SupplyType' }
                , { label: '信誉等级', name: 'CreditWorthiness', index: 'CreditWorthiness' }
                , { label: 'Email', name: 'Email', index: 'Email' }
                , { label: '含税否', name: 'IsTax', index: 'IsTax', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '是否内转', name: 'IsNz', index: 'IsNz', align: 'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues() }, editable: true, edittype: 'select', editoptions: { value: booleanSelectValues()} }
                , { label: '备注', name: 'Remark', index: 'Remark' }

		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {//加载
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {//刷新
                    myJqGrid.refreshGrid('1=1');
                },
                handleIsUsed: function () {//已启用
                    myJqGrid.refreshGrid("IsUsed = 1");
                },
                handleNotUsed: function () {//已停用
                    myJqGrid.refreshGrid("IsUsed = 0");
                },
                handleAdd: function (btn) {//增加
                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('ID', false);
                        }
                    });
                },
                handleEdit: function (btn) {//修改
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv', //加载窗体
                        btn: btn,
                        afterFormLoaded: function () {//窗体加载后
                            myJqGrid.setFormFieldDisabled('ID', true); //设置窗体字段不可用
                        }
                    });
                }
                , handleDelete: function (btn) {//删除
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url//删除路径
                    });

                }
                , handleSetUsed: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showError("提示", "请选择要启用的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.IsUsed=="true") {
                        showError("提示", "此记录已启用!");
                        return;
                    }
                    showConfirm("确认信息", "确认要启用选中的记录?", function (btn) {
                        ajaxRequest(
                            opts.updateUsedStatusUrl, //URL
                            {ids: keys, usedStatus: true }, //参数
                            true,
                            function (response) {
                                if (response.Result) {//执行成功
                                    myJqGrid.reloadGrid(); //重新加载
                                }
                            }
                        );

                        $(this).dialog('close'); //关闭对话框窗体
                    });
                }

                , handleSetUnused: function (btn) {
                    var keys = myJqGrid.getSelectedKeys();

                    if (isEmpty(keys) || keys.length == 0) {
                        showMessage("提示", "请选择要停用的记录!");
                        return;
                    }
                    var record = myJqGrid.getSelectedRecord();
                    if (record.IsUsed=="false") {
                        showError("提示", "此记录已停用!");
                        return;
                    }
                    showConfirm("确认信息", "确认要停用选中的记录?", function (btn) {
                        ajaxRequest(
                            opts.updateUsedStatusUrl,
                            { ids: keys, usedStatus: false },
                            true,
                            function (response) {
                                myJqGrid.reloadGrid();
                            }
                        );

                        $(this).dialog('close');
                    });
                }
            }
    });
//    function sexFmt(cellvalue, options, rowObject){
//        var temp = "<img src=/Content/erpimage/mapimage/icon/"
//        if(cellvalue==1){
//            temp = temp + "adminAdd.png";
//        } else if(cellvalue==2){
//            temp = temp + "accept.png";
//        } else {
//            temp = temp + "add.png";
//        }
//        temp = temp + "/>"
//        return temp;
//    }
    function Checktypeformatter(cellvalue, options, rowObject) {
        if (cellvalue == true || cellvalue == "true") {
            return "<input type='checkbox' checked='true'/>";            
        }
        else {
            return "<input type='checkbox'/>"; ;            
        }
    }
    function ChecktypeUnformat(cellvalue, options, rowObject) {
        if (cellvalue == true || cellvalue == "true") {
            return true;
        }
        else {
            return false;
        }
    }

    //grid行操作栏按钮
    myJqGrid.addListeners("rowclick", function (id, record, selBool) {
        //var idkey= myJqGrid.getSelectedKey(); //获取表格ID
        var record = myJqGrid.getSelectedRecord();
        if (record.IsUsed) {
            //alert("used");
        }
        else {
            //alert("notused");
        }
    });
   
    //自动生成厂家拼音码
    $("#ID").bind("change", function () {
        var val = myJqGrid.getFormField("ID").val();
        ajaxRequest(opts.getPinYin, { chn: val }, false, function (response) {
            if (response.Result) {
                myJqGrid.getFormField("ShortName").val(response.Data);
            }
        });
    });

    
}