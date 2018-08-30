//客户管理
function customerIndexInit(storeUrl, getPinYin) {

    var customersgrid = new MyGrid({
        renderTo: 'custid'
            , autoWidth: true
            , height: gGridHeight
            , primaryKey: 'ID'
            , sortByField: 'ID'
            , showPageBar: true
            , setGridPageSize: 30
            , buttons: buttons0
            , dialogWidth: 700
            , dialogHeight: 400
            , storeURL: storeUrl
            , storeCondition: 'IsUse=1'
            , autoLoad: true
            , initArray: [
			    { label: '客户编码', name: 'ID', index: 'ID', width: 100 },
                { label: '是否启用', name: 'IsUse', index: 'IsUse', formatter: booleanFmt, unformat: booleanUnFmt, width: 60, stype: 'select', searchoptions: { value: booleanSearchValues()} },
   		        { label: '客户名称', name: 'CustName', index: 'CustName', width: 200 },
   		        { label: '简称', name: 'ShortName', index: 'ShortName' },
   		        { label: '客户类型', name: 'CustType', index: 'CustType', stype: 'select', searchoptions: { value: dicToolbarSearchValues('CustType') }, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'CustType' }, width: 80, align: 'center' },
   		        { label: '注册资金', name: 'RegFund', index: 'RegFund', width: 80 },
   		        { label: '企业印鉴', name: 'CachetPath', index: 'CachetPath' },
   		        { label: '营业地址', name: 'BusinessAddr', index: 'BusinessAddr' },
   		        { label: '发票地址', name: 'InvoiceAddr', index: 'InvoiceAddr' },
   		        { label: '营业电话', name: 'BusinessTel', index: 'BusinessTel' },
   		        { label: '营业传真', name: 'BusinessFax', index: 'BusinessFax' },
   		        { label: 'Email', name: 'Email', index: 'Email' },
   		        { label: '邮政编码', name: 'PostCode', index: 'PostCode', width: 80 },
   		        { label: '负责人性别', name: 'PrincipalSex', index: 'PrincipalSex', stype: 'select', searchoptions: { value: dicToolbarSearchValues('Gender') }, width: 80, align: 'center' },
   		        { label: '负责人', name: 'Principal', index: 'Principal', width: 80 },
   		        { label: '负责人电话', name: 'PrincipalTel', index: 'PrincipalTel' },
   		        { label: '联系人', name: 'LinkMan', index: 'LinkMan', width: 80 },
   		        { label: '联系人电话', name: 'LinkTel', index: 'LinkTel' },
   		        { label: '负责采购人', name: 'Buyer', index: 'Buyer', width: 80 },
   		        { label: '地点代码', name: 'AddrCode', index: 'AddrCode' },
   		        { label: '信用额度', name: 'CreditQuota', index: 'CreditQuota', width: 80 },
   		        { label: '是否审核', name: 'IsAudit', index: 'IsAudit', formatter: booleanFmt, unformat: booleanUnFmt, width: 60, stype: 'select', searchoptions: { value: booleanSearchValues()} },
   		        { label: '税务代码', name: 'TaxCode', index: 'TaxCode' },
   		        { label: '备注', name: 'Remark', index: 'Remark' }

		    ]
            , functions: {
                handleRefresh: function (btn) {
                    customersgrid.refreshGrid('IsUse=1');
                },
                handleAdd: function (btn) {
                    customersgrid.handleAdd({
                        btn: btn,
                        loadFrom: 'custForm',
                        postCallBack: function (response) {
                            if (response.Result) {
                                var custId = response.Data.toString();
                                $('<div title="绑定银行信息"><p>客户 ' + custId + ' 资料保存成功，是否继续绑定银行信息？</p></div>').dialog({
                                    resizable: false,
                                    show: "fade",
                                    hide: "fade",
                                    modal: true,
                                    buttons: {
                                        "取消": function (btn) {
                                            $(this).dialog("close");
                                        },
                                        "继续": function (btn) {
                                            $(btn.currentTarget).button({ disabled: true });
                                            var customerRecord = customersgrid.getRecordByKeyValue(custId);
                                            var custName = customerRecord.CustName;
                                            customersgrid.handleAdd({
                                                title: '绑定银行信息',
                                                width: 350,
                                                height: 300,
                                                loadFrom: '/Bank.mvc/Index #MyFormDiv form',
                                                formId: 'BankForm',
                                                postUrl: '/Bank.mvc/Add',
                                                closeDialog: false
                                                , afterFormLoaded: function () {
                                                    customersgrid.setFormFieldValue("CustomerID", custId);
                                                    customersgrid.setFormFieldValue("CustName", custName);
                                                }
                                                , postCallBack: function (response) {
                                                    $("#BankForm")[0].reset();
                                                    customersgrid.setFormFieldValue("CustomerID", custId);
                                                    customersgrid.setFormFieldValue("CustName", custName);
                                                }
                                                , reloadGrid: false
                                            });
                                            $(this).dialog("close");
                                        }
                                    }
                                });
                            }
                        }
                    });
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
        $("#CustName").bind("change", function () {
            var val = customersgrid.getFormField("CustName").val();
            ajaxRequest(getPinYin, { chn: val }, false, function (response) {
                if (response.Result) {
                    customersgrid.getFormField("ShortName").val(response.Data);
                }
            });
        });
} 