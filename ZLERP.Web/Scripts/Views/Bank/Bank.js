function bankIndexInit(opts) {
    var customersgrid = new MyGrid({
        renderTo: 'CustomerGrid'
            , autoWidth: true
            , height: gGridHeight
            , primaryKey: 'ID'
            , sortByField: 'ID'
            , showPageBar: true
            , setGridPageSize: 30
        //, buttons: buttons0
            , dialogWidth: 700
            , dialogHeight: 400
            , storeURL: opts.CustomerStoreUrl
            , storeCondition: 'IsUse=1 and IsAudit =1'
            , autoLoad: true
            , excelExport: false
            , initArray: [
			    { label: '客户编码', name: 'ID', index: 'ID', width: 100, hidden: true },
   		        { label: '客户名称', name: 'CustName', index: 'CustName', width: 200 },
   		        { label: '简称', name: 'ShortName', index: 'ShortName', width: 110 },
   		        { label: '注册资金', name: 'RegFund', index: 'RegFund', width: 80 },
   		        { label: '营业地址', name: 'BusinessAddr', index: 'BusinessAddr' },
   		        { label: '发票地址', name: 'InvoiceAddr', index: 'InvoiceAddr' },
   		        { label: '负责人', name: 'Principal', index: 'Principal', width: 80 },
   		        { label: '负责人电话', name: 'PrincipalTel', index: 'PrincipalTel' },
   		        { label: '联系人', name: 'LinkMan', index: 'LinkMan', width: 80 },
   		        { label: '联系人电话', name: 'LinkTel', index: 'LinkTel' },
   		        { label: '信用额度', name: 'CreditQuota', index: 'CreditQuota', width: 80 },
		    ]
            , functions: {
                handleRefresh: function (btn) {
                    customersgrid.refreshGrid('IsUse=1');
                },
                handleAdd: function (btn) {
                    customersgrid.handleAdd({
                        btn: btn,
                        loadFrom: 'custForm'

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
    customersgrid.addListeners('rowclick', function (id, record, selBool) {
        bankGird.refreshGrid("CustomerID='" + id + "'");
    });
    var bankGird = new MyGrid({
        renderTo: 'BankGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , buttonRenderTo: 'buttonToolbar'
            , height: gGridHeight
            , dialogWidth: 350
            , dialogHeight: 300
		    , storeURL: opts.BankStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 90 }
                , { label: '客户编码', name: 'CustomerID', index: 'CustomerID', hidden: true }
                , { label: '客户名称', name: 'CustName', index: 'Customer.CustName', width: 250, searchoptions: { sopt: ['cn'] }, hidden: true }
                , { label: '开户行', name: 'BankName', index: 'BankName', width: 250 }
                , { label: '开户名', name: 'AccountName', index: 'AccountName', width: 150 }
                , { label: '帐号', name: 'Account', index: 'Account', width: 150 }
                , { label: '是否为担保', name: 'IsGuarantee', index: 'IsGuarantee', width: 70, formatter: booleanFmt, unformat: booleanUnFmt, align:"center" }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', width: 70, formatter: booleanFmt, unformat: booleanUnFmt, formatterStyle: { '0': '未启用', '1': '已启用' }, align: "center" }
		    ]
		    , autoLoad: false
            , storeCondition: '1<>1'
            , functions: {
                handleReload: function (btn) {
                    bankGird.reloadGrid();
                },
                handleRefresh: function (btn) {
                    bankGird.refreshGrid();
                },
                handleAdd: function (btn) {
                    if (!customersgrid.isSelectedOnlyOne()) {
                        showMessage('提示', '请选择一条客户记录进行操作！');
                        return;
                    }
                    var selectrecord = customersgrid.getSelectedRecord();
                    bankGird.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        closeDialog:false,
                        afterFormLoaded: function () {
                            bankGird.setFormFieldValue("CustomerID", selectrecord.ID);
                            bankGird.setFormFieldValue("CustName", selectrecord.CustName);
                        }
                    });
                },
                handleEdit: function (btn) {
                    bankGird.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    bankGird.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}