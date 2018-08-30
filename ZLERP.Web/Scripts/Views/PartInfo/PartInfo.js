function partinfoIndexInit(options) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
        , autoWidth: true
        , buttons: buttons0
        , height: gGridHeight
		, storeURL: options.storeUrl
		, sortByField: 'ID'
        , dialogWidth: 480
        , dialogHeight: 300
		, primaryKey: 'ID'
		, setGridPageSize: 30
		, showPageBar: true
        , groupField: 'ClassBName'

		, initArray: [
            { label: '配件代号', name: 'ID', index: 'ID', width: 80 }
            , { label: '配件名称', name: 'PartName', index: 'PartName', width: 100 }
            , { label: '配件规格', name: 'PartSpec', index: 'PartSpec', width: 80 }
            , { label: '大类', name: 'GreatClassID', index: 'GreatClassID', width: 70, hidden:true }
            , { label: '大类名称', name: 'ClassBName', index: 'ClassBName', width: 70, sortable: false }
            , { label: '中类', name: 'MiddClassID', index: 'MiddClassID', width: 70, hidden:true }
            , { label: '中类名称', name: 'ClassMName', index: 'ClassMName', width: 70, sortable: false }
            , { label: '小类', name: 'MinClassID', index: 'MinClassID', width: 70, hidden:true }
            , { label: '细类名称', name: 'ClassSName', index: 'ClassSName', width: 70, sortable: false }
            , { label: '供应商', name: 'SupplyName', index: 'SupplyName', width: 120 }
            , { label: '厂牌', name: 'BreadName', index: 'BreadNameDic.DicName', width: 120, sortable: false }
            , { label: '是否常用', name: 'IsOften', index: 'IsOften', width: 60, formatter: booleanFmt, unformat: booleanUnFmt }
            , { label: '当前库存', name: 'Inventory', index: 'Inventory', width: 60 }
            , { label: '单位', name: 'UnitID', index: 'UnitID', width: 60 }
            , { label: '上限值', name: 'LowerLimit', index: 'LowerLimit', width: 60 }
            , { label: '下限值', name: 'UpperLimit', index: 'UpperLimit', width: 60 }
            
		]
		, autoLoad: true
        , functions: {
            handleReload: function (btn) {
                myJqGrid.reloadGrid();
            },
            handleRefresh: function (btn) {
                myJqGrid.refreshGrid();
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
                    btn: btn
                });
            }
            , handleDelete: function (btn) {
                myJqGrid.deleteRecord({
                    deleteUrl: btn.data.Url
                });
            }
        }
    });
    var classTypeID = options.classTypeID;
    $("#GreatClassID").change(function (select) {
        var bid = $(this).val();
        //var parent_id = "PartClass";
        var params = {
            textField: "ClassMName",
            valueField: "ID",
            condition: "ClassBID='" + bid + "' AND ClassID = '" + classTypeID + "'"
        };
        bindSelectData($("#MiddClassID"), options.listClassMUrl, params);
    });

    $("#MiddClassID").change(function (select) {
        var _bid = $("#GreatClassID").val();
        var mid = $(this).val();
        var params = {
            textField: "ClassSName",
            valueField: "ID",
            condition: "ClassBID ='" + _bid + "' AND ClassMID='" + mid + "' AND  ClassID = '" + options.classTypeID + "'"
        };
        bindSelectData($("#MinClassID"), options.listClassSUrl, params);
    });
    
}