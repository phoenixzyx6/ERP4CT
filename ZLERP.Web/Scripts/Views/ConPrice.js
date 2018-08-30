//砼信息单价设定
function conpriceIndexInit(storeUrl) {
    var conpriceGrid = new MyGrid({
        renderTo: 'conpriceGrid'
            , autoWidth: true
            , title: '砼信息单价表'
            , buttons: buttons0
            , height: gGridWithTitleHeight
            , dialogWidth: 400
            , dialogHeight: 300
		    , storeURL: storeUrl
		    , sortByField: 'ConStrengthCode'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupField: 'SettingDate'
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '强度', name: 'ConStrengthCode', index: 'ConStrengthCode', width: 60, align: 'center' }
                , { label: '设定日期', name: 'SettingDate', index: 'SettingDate', formatter: 'date', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }
                , { label: '信息价', name: 'InfoPrice', index: 'InfoPrice', align: 'right', formatter: 'number', formatoptions: { thousandsSeparator: "," }, width: 100 }
                , { label: '泵送价格', name: 'PumpPrice', index: 'PumpPrice', align: 'right', formatter: 'number', formatoptions: { thousandsSeparator: "," }, width: 100 }
                , { label: '砂浆价格', name: 'SlurryPrice', index: 'SlurryPrice', align: 'right', formatter: 'number', formatoptions: { thousandsSeparator: "," }, width: 100 }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 250 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    conpriceGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    conpriceGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    conpriceGrid.handleAdd({
                        loadFrom: 'conpriceForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            conpriceGrid.setFormFieldDisabled('ConStrengthCode', false);
                            conpriceGrid.setFormFieldDisabled('SettingDate', false);
                        }
                    });
                },
                handleEdit: function (btn) {
                    conpriceGrid.handleEdit({
                        loadFrom: 'conpriceForm',
                        btn: btn,
                        afterFormLoaded: function () {
                            conpriceGrid.setFormFieldDisabled('ConStrengthCode', true);
                            conpriceGrid.setFormFieldDisabled('SettingDate', true);
                        }
                    });
                }
                , handleDelete: function (btn) {
                    conpriceGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
    });

}