
function stuffTypeInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'OrderNum'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
		    , dialogWidth: 300
            , dialogHeight: 220
		    , setGridPageSize: 30
            , groupField: 'TypeID'
            , groupingView: { groupDataSorted: true, minusicon: 'ui-icon-circle-minus', plusicon: 'ui-icon-circle-plus' }
		    , showPageBar: true
            , editSaveUrl: opts.updateUrl
		    , initArray: [
                { label: ' 类型编号', name: 'ID', index: 'ID', editable: false, editrules: { required: true} }
                , { label: ' 类型名称', name: 'StuffTypeName', index: 'StuffTypeName', editable: false }
                , { label: '所属类别', name: 'TypeID', index: 'TypeID', sortable: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'SType'} }
		        , { label: '材料大类', name: 'FinalStuffType', index: 'FinalStuffType', editable: true, hidden: true }
                , { label: '是否启用', name: 'IsUsed', index: 'IsUsed', formatter: booleanFmt, unformat: booleanUnFmt, width: 60 }
                , { label: ' 排序', name: 'OrderNum', index: 'OrderNum', width: 80, editable: true, editrules: { required: true, number: true} }
            ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {

                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        beforeSubmit: function () {

                            if (myJqGrid.getFormField("TypeID").val() == "StuffType" && myJqGrid.getFormField("FinalStuffType").val() == "") {
                                $("input[name='FinalStuffType']").addClass("input-validation-error");
                                showError('验证错误', '材料大类是必需的');
                                return false;
                            }
                            return true;

                        },
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldReadOnly('ID', false);
                            myJqGrid.setFormFieldDisabled('TypeID', false);
                            myJqGrid.setFormFieldDisabled('FinalStuffType', true);
                            myJqGrid.getFormField("TypeID").unbind();
                            myJqGrid.getFormField("TypeID").bind('change', function () {

                                if (myJqGrid.getFormField("TypeID").val() == "StuffType") {

                                    myJqGrid.setFormFieldDisabled("FinalStuffType", false);
                                }
                                else {
                                    myJqGrid.setFormFieldDisabled("FinalStuffType", true);
                                    myJqGrid.getFormField("FinalStuffType").val("");
                                }

                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            myJqGrid.setFormFieldDisabled('TypeID', true);
                            myJqGrid.setFormFieldDisabled('FinalStuffType', true);
                        }
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
 