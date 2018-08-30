function transpriceIndexInit(opts) {
    var transportGrid = new MyGrid({
        renderTo: 'TransportGrid'
        ,title:'运输商'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridWithTitleHeight
		    , storeURL: opts.transportStoreUrl
            , storeCondition: "SupplyKind in ('Su3', 'Su5')"
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true 
            , singleSelect: true
		    , initArray: [
                { label: '厂商代号', name: 'ID', index: 'ID', width: 80 }
                , { label: ' 厂家全称', name: 'SupplyName', index: 'SupplyName' }
                , { label: '简称', name: 'ShortName', index: 'ShortName' }
                , { label: '厂商类型', name: 'SupplyKind', index: 'SupplyKind', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'SuType'} }
                
                , { label: ' 负责人', name: 'Principal', index: 'Principal', width: 80 }
                , { label: ' 厂家地址', name: 'SupplyAddr', index: 'SupplyAddr' }
		    ] 
            , functions: {
                handleReload: function (btn) {
                    transportGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    transportGrid.reloadGrid();
                } 
            }
        });
        
        var transPriceGrid = new MyGrid({
            renderTo: 'TransPriceGrid'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: opts.transPriceStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 300
            , dialogHeight: 300
            , autoLoad: false
            , singleSelect: true 
            , toolbarRefresh:false
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width:50 }
                , { label: '运输厂商', name: 'TransportInfo.SupplyName', index: 'TransportInfo.SupplyName' }
                , { label: '供货厂商', name: 'SupplyInfo.SupplyName', index: 'SupplyInfo.SupplyName' }
                , { name: 'TransportID', hidden:true  }
                , { name: 'SupplyID', hidden: true } 
                , { label: '日期', name: 'PriceDate', index: 'PriceDate', width:80 }
                , { label: '单价', name: 'UnitPrice', index: 'UnitPrice', width:60, align:'right', formatter:'currency'}
                , { label: '计价方式', name: 'CalcType', index: 'CalcType', width: 60, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'TransPriceMethod'} }
                , { label: '备注', name: 'Remark', index: 'Remark' }
		    ] 
            , functions: { 
                handleRefresh: function (btn) {
                    transPriceGrid.reloadGrid();
                },
                handleAdd: function (btn) {
                    if (!transportGrid.isSelectedOnlyOne()) {
                        showError('提示', '请先选择一个运输商！');
                        return;
                    }
                    transPriceGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            transPriceGrid.setFormFieldValue("TransportID", transportGrid.getSelectedKey());
                        }
                    });
                },
                handleEdit: function (btn) {
                    transPriceGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
                    });
                }
                , handleDelete: function (btn) {
                    transPriceGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        //rowclick 事件定义 ,如果定义了 表格编辑处理，则改事件无效。
        transportGrid.addListeners('rowclick', function (id, record, selBool) {
            transPriceGrid.refreshGrid("TransportID='" + id + "'");
        });
}