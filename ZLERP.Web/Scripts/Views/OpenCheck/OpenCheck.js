function opencheckIndexInit(storeUrl, getTaskUrl, getCustMixpropUrl, getCustMixpropItemsUrl, getOpenCheckUrl, currentUser) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , dialogWidth: 1050
            , dialogHeight: 600
		    , initArray: [
                { label: '开盘编号', name: 'ID', index: 'ID', width: 80 }
                , { label: '开盘时间', name: 'OpenTime', index: 'OpenTime', formatter: 'date', width: 120, searchoptions: { dataInit: function (elem) { $(elem).datepicker(); }, sopt: ['ge']} }                
                , { label: '客户配比号', name: 'CustMixpropID', index: 'CustMixpropID', width: 80, hidden: true }
                , { label: '配比代号', name: 'MixpropCode', index: 'MixpropCode', width: 80 }
                , { label: '任务单号', name: 'TaskID', index: 'TaskID', width: 80 }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 120 }
                , { label: '施工单位', name: 'ConstructUnit', index: 'ConstructUnit', width: 120 }
                , { label: '备注', name: 'Remark', index: 'Remark' }
                , { label: '鉴定结果', name: 'CheckResult', index: 'CheckResult' }
                , { label: '鉴定人', name: 'CheckMan', index: 'CheckMan', width: 80 }
                , { label: '负责人', name: 'Principal', index: 'Principal' }
                , { label: ' 已出开盘', name: 'IsOut', index: 'IsOut', align: 'center', width: 50, formatter: booleanFmt, unformat: booleanUnFmt }
   		        , { label: '出机坍落度', name: 'Slump', index: 'Slump' }
                , { label: '和易性', name: 'MixGrade', index: 'MixGrade' }
                , { label: '其他性能', name: 'OtherCap', index: 'OtherCap' }
                , { label: '调整方式', name: 'AdjustMode', index: 'AdjustMode' }
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
                        afterFormLoaded: function () {
                            myJqGrid.getFormField("CheckMan").val(currentUser);
                            myJqGrid.setFormFieldReadOnly('CheckMan', true);

                            var CustMixpropIdField = myJqGrid.getFormField("CustMixpropID");
                            CustMixpropIdField.unbind('change');
                            CustMixpropIdField.bind('change', function () {
                                var CustMixpropId = $(this).val();
                                if (CustMixpropId) {
                                    ajaxRequest(getCustMixpropUrl,
                                                { id: CustMixpropId },
                                                  false,
                                                  function (response) {
                                                      if (response.Result) {
                                                          myJqGrid.setFormFieldValue('RIWRate', response.Data.RIWRate);
                                                          myJqGrid.setFormFieldValue('SCRate', response.Data.SCRate);
                                                          myJqGrid.setFormFieldValue('WCRate', response.Data.WCRate);
                                                          myJqGrid.setFormFieldValue('SIRRate', response.Data.SIRRate);
                                                          myJqGrid.setFormFieldValue('SIWRate', response.Data.SIWRate);
                                                          myJqGrid.setFormFieldValue('DeSlump', response.Data.Slump);
                                                          myJqGrid.setFormFieldValue('ConStrength', response.Data.ConStrength);
                                                      }
                                                      else {
                                                          showMessage(response.Message);
                                                      }
                                                  }
                                                );
                                    ajaxRequest(getCustMixpropItemsUrl,
                                                { id: CustMixpropId },
                                                  false,
                                                  function (response) {
                                                      if (response.Result) {
                                                          myJqGrid.setFormFieldValue('WAAmount', response.Data.WAAmount);
                                                          myJqGrid.setFormFieldValue('CEAmount', response.Data.CEAmount);
                                                          myJqGrid.setFormFieldValue('CAAmount', response.Data.CAAmount);
                                                          myJqGrid.setFormFieldValue('FAAmount', response.Data.FAAmount);
                                                          myJqGrid.setFormFieldValue('ADM1Amount', response.Data.ADM1Amount);
                                                          myJqGrid.setFormFieldValue('ADM2Amount', response.Data.ADM2Amount);
                                                          myJqGrid.setFormFieldValue('AIR1Amount', response.Data.AIR1Amount);
                                                          myJqGrid.setFormFieldValue('AIR2Amount', response.Data.AIR2Amount);
                                                      }
                                                      else {
                                                          showMessage(response.Message);
                                                      }
                                                  }
                                                );
                                }
                                else {
                                    showMessage("不存在");
                                }
                            });
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        getUrl: getOpenCheckUrl
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }, handle28d: function (btn) {
                    var uom = GetDateBySpan(28);
                    condition = "OpenTime between '" + uom + " 00:00:00' and '" + uom + " 23:59:59'";
                    myJqGrid.refreshGrid(condition);
                }
            }
    });

}
    