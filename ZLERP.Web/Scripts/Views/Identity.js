//特性维护
function identityIndexInit(storeUrl) {
    var IdentityGrid = new MyGrid({
        renderTo: 'IdentityGrid'
		    , title: '特性列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridWithTitleHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth: 350
            , dialogHeight: 250
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: ' 编号', name: 'ID', index: 'ID' }
                , { label: ' 特性代号', name: 'IdentityCode', index: 'IdentityCode', hidden:true }
                , { label: ' 特性类型', name: 'IdentityType', index: 'IdentityType', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'IdenType'} }
                , { label: ' 特性', name: 'IdentityName', index: 'IdentityName'}
                , { label: ' 特性价格', name: 'IdentityPrice', index: 'IdentityPrice' }

		    ]
            , functions: {
                handleReload: function (btn) {
                    IdentityGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    IdentityGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    IdentityGrid.handleAdd({
                        btn: btn
                        , loadFrom: 'IdentityForm'

                    });
                },
                handleEdit: function (btn) {
                    IdentityGrid.handleEdit({
                        btn: btn
                        , loadFrom: 'IdentityForm'
                    });
                }
                , handleDelete: function (btn) {
                    IdentityGrid.deleteRecord({
                        deleteUrl: btn.data.Url

                    });
                }
            }
    });

       // IdentityGrid.getJqGrid().jqGrid('setGridWidth', $("#IdentityGrid").width());
    $('#IdentityCode').bind('blur', function () {
        $('#IdentityName').val($(this).val());
    });
}