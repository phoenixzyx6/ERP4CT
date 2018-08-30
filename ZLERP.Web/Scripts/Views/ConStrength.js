//砼强度
function conStrengthIndexInit(storeUrl) {
    var ConStrengthGrid = new MyGrid({
        renderTo: 'ConStrengthid'

            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ConStrengthCode'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 350
            , dialogHeight: 300
		    , initArray: [
                  { label: ' 编号', name: 'ID', index: 'ID', hidden: true }
                , { label: ' 强度代号', name: 'ConStrengthCode', index: 'ConStrengthCode', width: 80 }
                , { label: ' 等级（MPa）', name: 'Level', index: 'Level' }
                , { label: ' 牌价', name: 'BrandPrice', index: 'BrandPrice', align: 'right', width: 80 }
                , { label: ' 建议售价', name: 'AdvisePrice', index: 'AdvisePrice', align: 'right', width: 80 }
                , { label: ' 泵送加价', name: 'BumpAddPrice', index: 'BumpAddPrice', align: 'right', width: 80 }
                , { label: ' 换算率(T/m³)', name: 'Exchange', index: 'Exchange', align: 'right', formatter: Kg2TFmt, unformat: T2KgFmt, width: 100 }
                , { label: ' 序号', name: 'OrderNum', index: 'OrderNum', width: 50, align: 'center', hidden: true }

		    ]
            , functions: {
                handleReload: function (btn) {
                    ConStrengthGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ConStrengthGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    ConStrengthGrid.handleAdd({
                        loadFrom: 'ConStrengthForm',
                        btn: btn
                    });
                },
                handleEdit: function (btn) {
                    var data = ConStrengthGrid.getSelectedRecord();
                    ConStrengthGrid.handleEdit({
                        loadFrom: 'ConStrengthForm',
                        btn: btn,
                        afterFormLoaded: function () {
                             ConStrengthGrid.getFormField("Exchange").val(data.Exchange / 1000); 
                        }
                    });
                }
                , handleDelete: function (btn) {
                    ConStrengthGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });
        ConStrengthGrid.getJqGrid().jqGrid('setGridWidth',$("#ConStrengthid").width());
    }