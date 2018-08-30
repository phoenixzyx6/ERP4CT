//公司管理
function companyIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'My-JqGrid'　
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: ' 公司编号', name: 'ID', index: 'ID', width:80, hidden:true }
                , { label: ' 公司名称', name: 'CompName', index: 'CompName' }
                , { label: ' 公司地址', name: 'CompAddr', index: 'CompAddr' }
                , { label: ' 国家', name: 'Country', index: 'Country', width: 50 }
                , { label: ' 区域', name: 'Area', index: 'Area', width: 50 }
                , { label: ' 省份', name: 'Province', index: 'Province', width: 50 }
                , { label: ' 负责人', name: 'Principal', index: 'Principal', width: 50 }
                , { label: ' 联系人', name: 'LinkMan', index: 'LinkMan', width: 50 }
                , { label: ' 电话', name: 'Tel', index: 'Tel', width: 85 }
                , { label: ' 邮箱', name: 'Email', index: 'Email', width: 100 }
                , { label: ' 传真', name: 'Fax', index: 'Fax', width: 85 }
                , { label: ' 网址', name: 'WebSite', index: 'WebSite' }
                , { label: ' 邮编', name: 'PostCode', index: 'PostCode', width: 50 }
                , { label: ' 注册资金', name: 'RegAsset', index: 'RegAsset', width: 80 }
                , { label: ' 员工数', name: 'Employees', index: 'Employees', width: 60 }                
                , { label: ' 位置纬度', name: 'Latitude', index: 'Latitude', width: 80 }
                , { label: ' 位置经度', name: 'Longtide', index: 'Longtide', width: 80 }
                , { label: ' 公司范围', name: 'Range', index: 'Range', width: 80 }
                , { label: ' 误差', name: 'Excursion', index: 'Excursion', width: 80 }
                , { label: ' 备注', name: 'Remark', index: 'Remark' }

		    ]
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        btn: btn,
                        loadFrom: 'My-FormDiv'

                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        btn: btn,
                        loadFrom: 'My-FormDiv'
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}