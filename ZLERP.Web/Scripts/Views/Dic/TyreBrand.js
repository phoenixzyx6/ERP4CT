﻿ function dicIndexInit(options){
        var mygrid = new MyGrid({
            renderTo: 'grid',		  
         
            expandColumn: 'DicName',                 
            expandColClick: true
            , autoWidth: true
          , storeCondition:{nodeid:options.ParentID}
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , singleSelect: true
            , dialogWidth: 500
            , dialogHeight:300
		   // , groupField: 'UserType'
		    , setGridPageSize: -1
		    , showPageBar: false            
		    , initArray: [
			             {label:'品牌名称', name: 'DicName', index:'DicName', width:200, editable:true}   	 
                        ,{label:'字典编号', name: 'ID', index:'ID', hidden:true}
   		                ,{label:'节点代码', name: 'TreeCode', index:'TreeCode',  hidden:true}   	 
   		                ,{label:'描述', name: 'Des', index:'Des', editable:true}   	 
   		                ,{label:'父节点', name: 'ParentID', index:'ParentID', hidden:true}   	 
   		                ,{label:'是否叶子节点', name: 'IsLeaf', index:'IsLeaf', width:50,align:'center',formatter:booleanFmt, unformat:booleanUnFmt,hidden:true}   	 
   		                ,{label:'排序', name: 'OrderNum', index:'OrderNum', hidden:true}   	 
   		                ,{label:'深度', name: 'Deep', index:'Deep', editable:true,hidden:true}  
                        ,{label:' 联系地址', name: 'Field1', index:'Field1', editable:true }   	 
   		                ,{label:' 联系电话', name: 'Field2', index:'Field2', editable:true }   	 
   		                ,{label:' 联系人', name: 'Field3', index:'Field3', editable:true }   	 
   		                ,{label:' 扩展字段4', name: 'Field4', index:'Field4', hidden:true }   	
		    ]
		    , autoLoad: true
            , functions: {
                handleRefresh: function (btn) {
                    mygrid.getJqGrid().jqGrid('setGridParam', {postData: { nodeid: options.ParentID}}).trigger("reloadGrid");
                },
                handleAdd: function (btn) {
                    mygrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        afterFormLoaded:function(){
                            mygrid.setFormFieldReadOnly('ID',true);
                            var idValue = options.ParentID+""+new Date().valueOf();
                            mygrid.setFormFieldValue('ID',idValue);    
                        },
                        beforeSubmit:function(){
                             mygrid.setFormFieldValue('TreeCode', mygrid.getFormField("DicName").val());    
                             return true;
                        }
                        ,postCallBack:function(){
                            mygrid.getJqGrid().jqGrid('setGridParam', {postData: { nodeid: options.ParentID}}).trigger("reloadGrid");
                        },
                        reloadGrid:false,
                        btn:btn
                    });
                },
                handleEdit: function (btn) {
                    mygrid.handleEdit({
                        btn:btn,
                        afterFormLoaded:function(){
                            mygrid.setFormFieldReadOnly('ID',true);
                        },
                        reloadGrid:false,
                        postCallBack:function(){
                            mygrid.getJqGrid().jqGrid('setGridParam', {postData: { nodeid: options.ParentID}}).trigger("reloadGrid");
                        },
                        loadFrom: 'MyFormDiv'
                    });
                }
                , handleDelete: function (btn) {
                    mygrid.deleteRecord({
                        deleteUrl: btn.data.Url
                         ,singleDelete:true
                        , reloadGrid: true                       
                    });
                }
                
            }
        });
 
}