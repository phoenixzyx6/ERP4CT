function departmentIndexInit(storeUrl) {
    var myJqGrid = new MyGrid({
        renderTo: 'department-grid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 300
            , dialogHeight: 300
		    , setGridPageSize: -1
		    , showPageBar: false
            , singleSelect: true
            , treeGrid: true
            , treeGridModel: 'adjacency'
            , expandColumn: 'DepartmentName'
            , expandColClick: true
            , autoLoad: true
		    , initArray: [
                  { label: ' 部门名称', name: 'DepartmentName', index: 'DepartmentName' }
                , { label: ' 部门编号', name: 'ID', index: 'ID',width:80, hidden:true }
                , { name: 'CompanyID', index: 'CompanyID', hidden: true }
                , { name: 'ParentID', index: 'ParentID', hidden: true }
                , { name: 'ManagerID', index: 'ManagerID', hidden: true }
                , { label: ' 是否叶子节点', name: 'IsLeaf', index: 'IsLeaf', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                
                , { label: ' 所属公司', name: 'CompanyName', index: 'Company.CompName' }
                , { label: ' 上级部门', name: 'ParentDepartmentName', index: 'ParentDepartment.DepartmentName' }
                , { label: ' 主管', name: 'ManagerName', index: 'Manager.TrueName', sortable: false, width: 50 }
                , { name: 'ManagerID', index: 'ManagerID', hidden: true }
                , { label: ' 创建人', name: 'Builder', index: 'Builder', width: 50 }
                , { label: ' 创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
                , { label: ' 最后修改人', name: 'Modifier', index: 'Modifier', width: 70 }
                , { label: ' 最后修改时间', name: 'ModifyTime', index: 'ModifyTime', formatter: 'date', formatoptions: { newformat: "ISO8601Long" }, searchoptions: { dataInit: function (elem) { $(elem).datetimepicker(); }, sopt: ['ge']} }
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
                        btn: btn
                        , loadFrom: 'departmentForm'
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'departmentForm'
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