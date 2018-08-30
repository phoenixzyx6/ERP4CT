function settlementIndexInit(opts) {
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: true
            , dialogWidth: 800
		    , initArray: [
                { label: '结算单号', name: 'ID', index: 'ID', width: 80 }
                , { label: '合同编号', name: 'ContractID', index: 'ContractID', width: 80 }
                , { label: '合同名称', name: 'ContractName', index: 'ContractName', width: 300, sortable: false }
                , { label: '结算日期', name: 'CreateDate', index: 'CreateDate', width: 80 }
                , { label: '结算开始日期', name: 'BeginDate', index: 'BeginDate', width: 80 }
                , { label: '结算截止日期', name: 'EndDate', index: 'EndDate', width: 80 }
                , { label: '计价方式', name: 'PriceType', index: 'PriceType', width: 80, align: 'center', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ValuationType'} }
                , { label: '浮动比例', name: 'Rate', index: 'Rate' }
                , { label: '已结算', name: 'IsClosed', index: 'IsClosed', width: 50, align: 'center', formatter: booleanFmt, unformat: booleanUnFmt }
                , { label: '结算人', name: 'Executor', index: 'Executor', search: false }
                , { label: '结算执行时间', name: 'ExecuteTime', index: 'ExecuteTime', formatter: 'datetime', search: false }
                , { label: '审核状态', name: 'AuditStatus', index: 'AuditStatus', search: false, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'AuditStatus' }, width: 60 }
                , { label: '审核时间', name: 'AuditTime', index: 'AuditTime', formatter: 'date', search: false }
                , { label: '审核意见', name: 'AuditInfo', index: 'AuditInfo', search: false }
                , { label: '审核人', name: 'Auditor', index: 'Auditor', search: false }
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
                            $('#btnQuery').button({ disabled: false });
                            $("input[name='ContractName']").attr("disabled", false).next("button").attr("disabled", false);
                            $('#fsOtherPrice table').remove();
                            $('#fsItemPrice table').remove();
                            $('#fsPumpPrice table').remove();
                        }
                    });
                },
                handleEdit: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record['IsClosed'] == 'true') {
                        showError('已结算的结算单不允许再做修改！');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        afterFormLoaded: function () {
                            $('#btnQuery').button({ disabled: true });
                            $("input[name='ContractName']").attr('disabled', true).next('button').attr('disabled', true);
                            $('#fsOtherPrice table').remove();
                            $('#fsItemPrice table').remove();
                            $('#fsPumpPrice table').remove();
                            getSettlementItems(myJqGrid.getFormFieldValue("ID"));
                        }
                    });
                }
                , handleDelete: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record['IsClosed'] == 'true') {
                        showError('已结算的结算单不允许删除！');
                        return;
                    }
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handleExecute: function (btn) {
                    //结算
                    var record = myJqGrid.getSelectedRecord();
                    //                    if (record && record['IsClosed'] == 'true') {
                    //                        showError('该结算单已结算！');
                    //                        return;
                    //                    }
                  
                    if (isEmpty(record)) {
                        showMessage('没有选择可结算的结算单！');
                        return;
                    }

                    if (record && record.AuditStatus == '1') {
                        showMessage('该结算单已审核，不允许再结算');
                        return;
                    }
                    else {
                        showConfirm('确认结算', '真的要结算该结算单吗？结算后该结算单将不可修改，结算前请确认价格已经设置正确。', function () {
                            var dlg = $(this);
                            ajaxRequest(opts.executeUrl, { id: record["ID"] }, true, function (response) {
                                myJqGrid.reloadGrid();
                                dlg.dialog("close");
                            });
                        });
                    }
                }
                , handleAudit: function (btn) {
                    var record = myJqGrid.getSelectedRecord();
                    if (record && record.AuditStatus == '1') {
                        showMessage('该结算单已审核');
                        return;
                    }
                    myJqGrid.handleEdit({
                        loadFrom: 'AuditForm',
                        width: 400,
                        height: 240,
                        btn: btn
                    });
                }
            }
    });
    //查询合同价格设定
        function getValueItems() {
            var contractId = myJqGrid.getFormFieldValue('ContractID');
            if (contractId == "") {
                showError('验证错误', "请选择合同");
                return;
            }
            var form = $('#' + myJqGrid.getFormId());
            if (!form.valid()) {
                var valMsg = $('[data-valmsg-summary=true]');
                if (valMsg.length > 0) {
                    valMsg.hide();
                    showError('验证错误', valMsg.html());
                }
                return;
             }
                var btn = $(this);
                btn.button({ disabled: true });
                var data = form.serialize();
                $.ajax({
                    url: opts.findValueItemsUrl,
                    data: data,
                    success: function (response) {
                        var index = 0;
                        if (response.Result) {
                            showSettlementItems(response.Data);
                        }

                        btn.button({ disabled: false });
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        handleServerError(XMLHttpRequest, textStatus, errorThrown);
                        btn.button({ disabled: false });
                    }
                });
                
           
    }
    $('#btnQuery').bind('click', getValueItems);
    //修改
    function getSettlementItems(settlementId) {
        ajaxRequest(opts.findSettlementItemsUrl, { settlementId: settlementId }, false,
        function (response) {
            
            if (response.Result) {
                showSettlementItems(response.Data);
            }
        });
    }
    function showSettlementItems(data) {
        $('#fsOtherPrice table').remove();
        $('#fsItemPrice table').remove();
        $('#fsPumpPrice table').remove();
        var index = 0;
        //其它加价
        if (data.OtherPrice != null && data.OtherPrice.length > 0) {
            var items = data.OtherPrice;

            var table = $('<table border="1">').append('<tr><th>加价项目</th><th>计算方式</th><th>价格</th></tr>');
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                var idField = "";
                if (item.ID) {
                    idField = '<input type="hidden" value="' + item.ID + '" name="items[' + index + '].ID"/>';
                }
                table.append('<tr><td>' + item.PriceType + idField + '<input type="hidden" name="items[' + index + '].PriceType" value="other"/><input type="hidden" name="items[' + index + '].TypeCode" value="' + item.ContractItemsID + '"/></td><td>' + item.TypeCode + '</td>'
                         + '<td><input type="text" name="items[' + (index++) + '].UnitPrice" value="' + item.UnitPrice + '"/></td></tr>');

            }
            table.appendTo('#fsOtherPrice');
        }
        //合同明细价格
        if (data.ItemPrice != null && data.ItemPrice.length > 0) {
            var itemPrice = data.ItemPrice;

            var table = $('<table border="1">').append('<tr><th>强度</th><th>特性</th><th>非泵价格</th><th>泵送费</th><th>泵送价格</th><th>特性加价</th><th>砂浆价格</th></tr>');
            for (var i = 0; i < itemPrice.length; i++) {
                var item = itemPrice[i];
                var idField = "";
                if (item.ID) {
                    idField = '<input type="hidden" value="' + item.ID + '" name="items[' + index + '].ID"/>';
                }
                table.append('<tr><td>' + item.TypeCode + idField + '<input type="hidden" name="items[' + index + '].PriceType" value="citem"/><input type="hidden" name="items[' + index + '].ContractItemsID" value="' + item.ContractItemsID + '"/></td><td>' + item.PriceType + '</td><td>'
                         + '<input type="text" name="items[' + index + '].UnitPrice" value="' + item.UnitPrice + '"/></td><td>'
                         + '<input type="text" name="items[' + index + '].PumpPrice" value="' + item.PumpPrice + '"/></td><td>'
                         + '<input type="text" disabled="disabled" value="' + (item.UnitPrice + item.PumpPrice) + '"/></td><td>'
                         + '<input type="text" name="items[' + index + '].IdentityPrice" value="' + item.IdentityPrice + '"/></td><td>'
                         + '<input type="text" name="items[' + (index++) + '].SlurryPrice" value="' + item.SlurryPrice + '"/></td><td></tr>');

            }
            table.appendTo('#fsItemPrice');
        }
        else {
            showError('注意','该期间没有发货记录');
        }
        //泵车价格
        if (data.PumpPrice != null && data.PumpPrice.length > 0) {
            var items = data.PumpPrice;

            var table = $('<table border="1">').append('<tr><th>泵车类型</th><th>价格</th></tr>');
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                var idField = "";
                if (item.ID) {
                    idField = '<input type="hidden" value="' + item.ID + '" name="items[' + index + '].ID"/>';
                }
                var pumpTypeName = dicNormalRenderer(item.TypeCode, 'PumpType');
                table.append('<tr><td>' + pumpTypeName + idField + '<input type="hidden" name="items[' + index + '].PriceType" value="pump"/><input type="hidden" name="items[' + index + '].TypeCode" value="' + item.TypeCode + '"/></td><td>'
                         + '<input type="text" name="items[' + (index++) + '].UnitPrice" value="' + item.UnitPrice + '"/></td></tr>');

            }
            table.appendTo('#fsPumpPrice');
        }
    }
}