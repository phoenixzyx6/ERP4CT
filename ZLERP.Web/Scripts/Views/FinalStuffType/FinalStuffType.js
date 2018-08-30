
function finalStuffTypeInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            //, buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , dialogWidth: 300
            , dialogHeight: 200
		    , setGridPageSize: 30
		    , showPageBar: false
            , singleSelect: true
            , editSaveUrl: opts.updateUrl
		    , initArray: [
                { label: '原材料大类编号', name: 'ID', index: 'ID', editable: false }
                , { label: '原材料大类名称', name: 'FinalStuffTypeName', index: 'FinalStuffTypeName', editable: false }
                , { label: '单方最小配比量', name: 'MinContent', index: 'MinContent', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
		        , { label: '单方最大配比量', name: 'MaxContent', index: 'MaxContent', editable: true, editrules: { number: true }, formatoptions: { thousandsSeparator: ","} }
            ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                }
            }
    });
    myJqGrid.addListeners("afterSubmitCell", function () {
        myJqGrid.reloadGrid();
    });
}
 