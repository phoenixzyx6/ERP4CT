//客户配比　
 function custMixpropIndexInit(opts) {
        var ConsGrid = new MyGrid({
            renderTo: 'CusGridDiv'
		    , title: '客户配比'
            //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridWithTitleHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth:700
            , dialogHeight:300 
		    , setGridPageSize: 30
		    , showPageBar: true 
            , singleSelect: true
		    , initArray: [
                {label:' 配比代号', name: 'MixpropCode', index:'MixpropCode', width: 80 }   	 
                ,{label:' 砼强度', name: 'ConStrength', index:'ConStrength', width: 60, stype: 'select', searchoptions: { dataUrl: opts.conStrengthSelectUrl} }   	 
                ,{label:' 坍落度', name: 'Slump', index:'Slump', width: 60}   	 
                ,{label:' 水泥型号', name: 'OXY', index:'OXY', width: 60 }  
                ,{label:' 抗渗等级', name: 'ImpGrade', index:'ImpGrade', width: 60 }   	 
                ,{label:' 骨料粒径', name: 'CarpRadii', index:'CarpRadii', width: 60, align: 'right' }   	 
                ,{label:' 设计容重', name: 'Weight', index:'Weight', width: 60, align: 'right', formatter: rzFmt, unformat: rzUnFmt }   	 
                ,{label:' 水灰比', name: 'WCRate', index:'WCRate',width: 60, align: 'right' }  
                 	 
                ,{label:' 细度目数', name: 'Mesh', index:'Mesh', width: 60 }   	 
                ,{label:' 配合比例', name: 'MixpropRate', index:'MixpropRate', width: 60 }   	 
                ,{label:' 砂率', name: 'SCRate', index:'SCRate', width: 60, align:'right' }   	 
                ,{label:' 外加剂种类', name: 'AdmixtureType', index:'AdmixtureType', width: 80 }   	 
                ,{label:' 混凝土类别', name: 'CementType', index:'CementType', width: 80 }   	 
                ,{label:' 砂含水率', name: 'SIWRate', index:'SIWRate', width: 60 }   	 
                ,{label:' 石含水率', name: 'RIWRate', index:'RIWRate', width: 60 }   	 
                ,{label:' 砂含石率', name: 'SIRRate', index:'SIRRate', width: 60 }
                ,{label:' 客户配比号', name: 'ID', index:'ID', width: 80}   	 
                  	 
		    ]
            , functions: {
                handleReload: function (btn) {
                    ConsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ConsGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                    ConsGrid.handleAdd({                         
                        loadFrom: 'CusFormDiv'
                        ,btn:btn 
                    });
                },
                handleEdit: function (btn) {
                    ConsGrid.handleEdit({
                        btn:btn
                        ,loadFrom: 'CusFormDiv' 
                        ,prefix:'cmp',
                        /*,postCallBack: function (response) { alert('handle edit callback..'+response.Message); }
                        ,reloadGrid: false*/
                    });
                }
                , handleDelete: function (btn) {
                    ConsGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        ,postCallBack: function (response) {
                            ConsGrid.reloadGrid();
                            ConItemsGrid.reloadGrid();
                        }
                    });
                }
            }
        });
        var ConItemsGrid = new MyGrid({
            renderTo: 'ConItemsGridDiv'
		    , title: '材料用量'
            //, width: '100%'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridWithTitleHeight
		    , storeURL: opts.itemStoreUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , dialogWidth:500
            , dialogHeight:300
		    //, groupField: 'UserType'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad:false 
            , storeCondition: '1<>1'
            , editSaveUrl: opts.updateItemUrl
		    , initArray: [
                {label:' 客户配比子表编号', name: 'ID', index:'ID', editable:true ,editrules: {required: true},hidden:true}   	 
                ,{label:' 客户配比号', name: 'CustMixpropID', index:'CustMixpropID', editable:true,hidden:true } 
                ,{label:' 材料类型', name: 'StuffTypeName', index:'StuffInfo.StuffType.StuffTypeName', jsonmap:'StuffTypeName', width:100 }    	 
                ,{label:' 材料名称', name: 'StuffName', index:'StuffInfo.StuffName', jsonmap:'StuffName', width:100 }   	 
                ,{label:' 原料编号', name: 'StuffID', index:'StuffID', editable:true,width:80,hidden:true }
                ,{label:' 理论用量', name: 'StandardAmount', index:'StandardAmount', editable:true,width:80, hidden: true }    	 
                ,{label:' 材料用量', name: 'Amount', index:'Amount', editable:true,width:80 }  	 
                  	 
		    ]
            , functions: {
                handleReload: function (btn) {
                    ConItemsGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    ConItemsGrid.refreshGrid();
                },
                handleAdd: function (btn) {
                var formulaid = ConsGrid.getSelectedKey();
                    if (formulaid.length > 0) {
                    ConItemsGrid.handleAdd({
                        title: btn.data.FuncDesc
                        ,loadFrom: 'CusItemsFormDiv'
                        ,postUrl: btn.data.Url
                        ,afterFormLoaded:function(){
                            ConItemsGrid.setFormFieldValue('cmpitems.CustMixpropID',formulaid);  
                            ConItemsGrid.setFormFieldReadOnly('cmpitems.CustMixpropID',true);
                        }
                        ,postCallBack: function (response) {
                            ConsGrid.reloadGrid();
                        }
                    });
                    }
                    else {
                        showError("警告", "请在左侧选择一条配比信息");
                    }
                    
                },
                handleEdit: function (btn) {
                    ConItemsGrid.handleEdit({
                        title: btn.data.FuncDesc
                        ,loadFrom: 'CusItemsFormDiv'
                        ,postUrl: btn.data.Url
                        ,prefix:'cmpitems'
                        ,postCallBack: function (response) {
                            ConsGrid.reloadGrid();
                        }
                        /*,width: 600
                        ,height: 400 
                        ,getUrl: ''
                        ,postCallBack: function (response) { alert('handle edit callback..'+response.Message); }
                        ,reloadGrid: false*/
                    });
                }
                , handleDelete: function (btn) {
                    ConItemsGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                        /*,singleDelete:false
                        , reloadGrid: true*/
                        , postCallBack: function (response) { ConsGrid.reloadGrid(); }
                    });
                }
                , handleExport: function (btn) {
                    var formulaid = ConsGrid.getSelectedKey();
                    if (formulaid.length > 0) {
                        ajaxRequest(opts.exportStuffUrl, { formulaid: formulaid }, false, function (response) {
                            if (response.Result) {
                                showMessage("消息", "请根据表格填写材料用量");
                                ConItemsGrid.refreshGrid("CustMixpropID='" + formulaid + "'");
                                ConsGrid.reloadGrid();
                            }
                            else {
                                showError("消息", response.Message);
                            }
                        }, null, btn);
                    }
                    else {
                        showMessage('提示', "请先保存配比信息");
                    }
                }
            }
        });

        ConsGrid.addListeners('rowclick', function (id, record,selBool) {
            ConItemsGrid.refreshGrid("CustMixpropID='"+id+"'");
        });

        ConItemsGrid.addListeners("afterSubmitCell", function () {
            ConsGrid.reloadGrid();
        });
    }