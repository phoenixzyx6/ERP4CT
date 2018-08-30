function PurchaseMainIndexInit(opts) {
    var main_id = '';
    var Purchase_ID = '';


    var TaskGrid = new MyGrid({
        renderTo: 'MyJqGrid'
		    , title: "采购执行"
            , buttons: buttons0
            , autoWidth: true
            , height: gGridHeight / 2 - 20
		    , storeURL: opts.storeUrl
            , sortByField: 'ID'
		    , primaryKey: 'ID'
            , storeCondition: "lifecycle=0"
		    , setGridPageSize: 30
		    , showPageBar: true
            , singleSelect: false
		    , initArray: [
   		        { label: '采购编号', name: 'ID', index: 'ID', width: 70 }
   		        , { label: '采购数量', name: 'Main_Num', index: 'Main_Num', width: 60, search: false }
                , { label: '已购买数量', name: 'Main_num1', index: 'Main_num1', width: 100, search: false }
                , { label: '物资ID', name: 'GoodsID', index: 'GoodsID', width: 60, search: false, hidden: true }
                , { label: '物资名称', name: 'GoodsName', index: 'GoodsName', width: 200 }
                , { label: '库存数量', name: 'ContentNum', index: 'ContentNum', width: 100 }
                , { label: '采购状态', name: 'Main_Sate', index: 'Main_Sate', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Main_Sate' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('Main_Sate') }, width: 80 }
                , { label: '单价', name: 'Main_Price', index: 'Main_Price', width: 100, search: false }
                , { label: '总价', name: 'Main_Sumprice', index: 'Main_Sumprice', width: 100, search: false }
                , { label: '开票金额', name: 'Main_Money', index: 'Main_Money', width: 100, search: false }
                , { label: '现金金额', name: 'Main_NoMoney', index: 'Main_NoMoney', width: 100, search: false }
                , { label: '备注', name: 'Remark', index: 'Remark', width: 300, search: false }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 100, search: false, formatter: 'date' }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 100, search: false }
   	        ]
		    , autoLoad: true
            , functions: {
                handleRefresh: function (btn) {
                    TaskGrid.refreshGrid();
                }
                , handleDelete: function (btn) {
                    var data = TaskGrid.getSelectedRecord();
                    var sateValue = data.Main_Sate;
                    if (sateValue == 1 || sateValue == 'true') {
                        showMessage('提示', '已完成单据不能删除！');
                        return;
                    }
                    TaskGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }

            }
    });

    TaskGrid.addListeners('rowclick', function (id, record, selBool) {
        tmpGrid.refreshGrid("Main_ID='" + id + "'");
        main_id = id;
    });
    //-------------------------------------------------------------------------------------------------------------------------
    var ParaArr;
    var tmpGrid = new MyGrid({
        renderTo: 'PurchaseGrid'
		    , title: '采购执行子项'
            , height: 220
            , autoWidth: true
            , dialogWidth: 500
            , dialogHeight: 400
		    , storeURL: opts.purchaseStoreUrl
		 	, sortByField: 'ID'
            , sortOrder: 'ASC'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
		    , showPageBar: true
            , autoLoad: false
            , singleSelect: true
            , buttons: buttons1
		    , initArray: [
                 { label: ' 子项编号', name: 'ID', index: 'ID', hidden: false, width: 100 }
                , { label: '物资ID', name: 'GoodsID', index: 'GoodsID', width: 60, search: false, hidden: true }
                , { label: '物质名称', name: 'GoodsName', index: 'GoodsName', width: 180 }
                , { label: '数量', name: 'Purchase_Num', index: 'Purchase_Num', hidden: false, width: 100 }
                , { label: '入库数量', name: 'Purchase_Num1', index: 'Purchase_Num1', hidden: false, width: 100 }
                , { label: '单价', name: 'Purchase_Price', index: 'Purchase_Price', width: 80, sortable: false }
                , { label: '总价', name: 'Purchase_Money', index: 'Purchase_Money', width: 100, sortable: false }
                , { label: '状态', name: 'Purchase_State', index: 'Purchase_State', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'Main_Sate' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('Main_Sate') }, width: 80 }
                , { label: '开票金额', name: 'Purchase_StickMoney', index: 'Purchase_StickMoney', width: 100 }
                , { label: '现金金额', name: 'Purchase_NoMoney', index: 'Purchase_NoMoney', width: 100 }
                , { label: '采购人电话', name: 'Purchase_ManTel', index: 'Purchase_ManTel', width: 100 }
                , { label: '采购人', name: 'Purchase_Man', index: 'Purchase_Man', width: 100 }
                , { label: '采购时间', name: 'Purchase_Date', index: 'Purchase_Date', width: 100, formatter: 'date' }
                , { label: '票据编号', name: 'Purchase_StickNo', index: 'Purchase_StickNo', width: 300, hidden: true }


                , { label: '合同名称', name: 'PurchaseContract_Name', index: 'PurchaseContract_Name', hidden: false }
                , { label: '辅料供货单位电话', name: 'PurchaseContract_SupplyTel', index: 'PurchaseContract_SupplyTel', hidden: false }
                , { label: '辅料供货单位', name: 'PurchaseContract_Supply', index: 'PurchaseContract_Supply', hidden: false }
                , { label: '运输单位电话', name: 'PurchaseContract_SupplyTel1', index: 'PurchaseContract_SupplyTel1', hidden: false }
                , { label: '运输单位', name: 'PurchaseContract_Supply1', index: 'PurchaseContract_Supply1', hidden: false }
                , { label: '签订日期', name: 'PurchaseContract_Date', index: 'PurchaseContract_Date', hidden: false, formatter: 'date' }
                , { label: '供货起始时间', name: 'PurchaseContract_StartDate', index: 'PurchaseContract_StartDate', hidden: false, formatter: 'date' }
                , { label: '供货截至时间', name: 'PurchaseContract_EndDate', index: 'PurchaseContract_EndDate', hidden: false, formatter: 'date' }

                , { label: '备注', name: 'Remark', index: 'Remark', width: 300, search: false, hidden: true }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', width: 100, search: false, formatter: 'date' }
                , { label: '创建人', name: 'Builder', index: 'Builder', width: 100, search: false }
		    ]
            , functions: {
                //入库
                handleGoodsIn: function (btn) {
                    var record = tmpGrid.getSelectedRecord();
                    if (record.Purchase_State == 'undefined' || record.Purchase_State == null) {
                        showMessage('提示', "请选择要修改的子表记录!");
                        return;
                    }
                    if (record.Purchase_State == '1') {
                        showMessage('提示', "不能修改已经完成的子表记录!");
                        return;
                    }
                    ParaArr = new Array();

                    tmpGrid.handleEdit({
                        loadFrom: 'MyFormDiv1',
                        btn: btn,
                        height: 200,
                        afterFormLoaded: function () {
                            tmpGrid.setFormFieldDisabled('GoodsName', true);
                            tmpGrid.setFormFieldDisabled('num1', true);
                            tmpGrid.setFormFieldDisabled('nums', true);
                            $('#num1').val(record.Purchase_Num1);
                            $('#nums').val(record.Purchase_Num);
                            $('#purchase_id1').val(record.ID);
                            supplyChange();
                        }
                        , beforeSubmit: function () {
                            if (($('#nums').val() * 1 - $('#num1').val() * 1) < $('#num2').val() * 1) {
                                showMessage('提示', "超出采购数量!");
                                return false;
                            }
                            //if ($('#num2').val() * 1 > 0) {
                            if(true)
                            {
                                ParaArr[0] = $('#purchase_id1').val();
                                ParaArr[1] = $('#num2').val();
                                return true;
                            }
                            else {
                                showMessage('提示', "入库数量必须大于0 !");
                                return false;
                            }

                        }
                        , postData: { ParaArr: ParaArr }
                        , postCallBack: function (response) {
                            TaskGrid.reloadGrid();
                        }
                    })
                },
                //修改子项
                handleUpdatePurchase: function (btn) {
                    var record = tmpGrid.getSelectedRecord();
                    if (record.Purchase_State == 'undefined' || record.Purchase_State == null) {
                        showMessage('提示', "请选择要修改的子表记录!");
                        return;
                    }
                    if (record.Purchase_State == '1') {
                        //showMessage('提示', "不能修改已经完成的子表记录!");
                        //return;
                    }
                    //                    if (record.Purchase_Num1 * 1 > 0) {
                    //                        showMessage('提示', "已经开始入库，不能修改!");
                    //                        return;
                    //                    }

                    ParaArr = new Array();

                    tmpGrid.handleAdd({
                        loadFrom: 'MyFormDiv'
                        , postCallBack: function (response) {
                            TaskGrid.reloadGrid();
                        }
                        , btn: btn
                        , beforeSubmit: function () {
                            //都不能为空
                            var goodsname = $('#goodsname').val();
                            var num = $('#num').val();
                            var rknum = $('#rknum').val();
                            var price = $('#price').val();
                            var money = $('#money').val();
                            var tele = $('#tele').val();
                            var man = $('#man').val();
                            var date1 = $('#date1').val();
                            var stickmoney = $('#stickmoney').val();
                            var nomoney = $('#nomoney').val();
                            var stickno = $('#stickno').val();
                            var contractname = $('#contractname').val();
                            var supplytel = $('#supplytel').val();
                            var supply = tmpGrid.getFormField("supply").val();
                            var supplyid = tmpGrid.getFormField("SupplyID").val();
                            //var supply = $('#supply').val();
                            //var supply = $('#supply').find('option:selected').text();
                            var one = $('#one').val();
                            var tow = $('#tow').val();
                            var date2 = $('#date2').val();
                            var date3 = $('#date3').val();
                            var date4 = $('#date4').val();

                            var supplytel1 = $('#supplytel1').val();
                            var supply1 = $('#supply1').find('option:selected').text();



                            var errorMsg = '';
                            var checking = true;

                            if (isEmpty1(num)) {
                                errorMsg += "<br>保存前请填写采购【数量】。";
                                checking = false;
                            }
//                            if (num * 1 <= 0) {
//                                errorMsg += "<br>【数量】必须大于0。";
//                                checking = false;
//                            }
                            //                            if (isEmpty1(num1)) {
                            //                                errorMsg += "<br>保存前请填写采购【入库数量】。";
                            //                                checking = false;
                            //                            }
                            //                            if (num1 * 1 <= 0) {
                            //                                errorMsg += "<br>【入库数量】必须大于0。";
                            //                                checking = false;
                            //                            }
                            if (num * 1 < rknum * 1) {
                                showMessage('提示', "数量不能小于已入库数量!");
                                return;
                            }
                            if (isEmpty1(price)) {
                                errorMsg += "<br>保存前请填写采购【单价】。";
                                checking = false;
                            }
                            if (price * 1 <= 0) {
                                errorMsg += "<br>【单价】必须大于0。";
                                checking = false;
                            }

                            if (isEmpty1(money)) {
                                errorMsg += "<br>保存前请填写采购【总价】。";
                                checking = false;
                            }
//                            if (money * 1 <= 0) {
//                                errorMsg += "<br>【总价】必须大于0。";
//                                checking = false;
//                            }

                            if (isEmpty1(tele)) {
                                errorMsg += "<br>保存前请填写【采购人电话】。";
                                checking = false;
                            }
                            if (isEmpty1(man)) {
                                errorMsg += "<br>保存前请填写【采购人】。";
                                checking = false;
                            }
                            if (isEmpty1(date1)) {
                                errorMsg += "<br>保存前请填写【采购时间】。";
                                checking = false;
                            }
                            if (isEmpty1(stickmoney)) {
                                errorMsg += "<br>保存前请填写【开票金额】。";
                                checking = false;
                            }
                            if (stickmoney * 1 < 0) {
                                errorMsg += "<br>【开票金额】必须大于等于0。";
                                checking = false;
                            }

                            if (isEmpty1(nomoney)) {
                                errorMsg += "<br>保存前请填写【现金金额】。";
                                checking = false;
                            }
                            if (nomoney * 1 < 0) {
                                errorMsg += "<br>【现金金额】不能小于0。";
                                checking = false;
                            }
                            if (isEmpty1(contractname)) {
                                errorMsg += "<br>保存前请填写【合同名称】。";
                                checking = false;
                            }
                            if (isEmpty1(supplytel)) {
                                errorMsg += "<br>保存前请填写【辅料单位电话】。";
                                checking = false;
                            }
                            var supplyFileid = tmpGrid.getFormField("SupplyID");
                            if (supplyFileid.val() == "") {
                                errorMsg += "<br>保存前请填写【辅料供货单位】。";
                                checking = false;
                            }
                            //                            if (isEmpty1(supplytel1)) {
                            //                                errorMsg += "<br>保存前请填写【运输单位电话】。";
                            //                                checking = false;
                            //                            }
                            //                            if (isEmpty1(supply1)) {
                            //                                errorMsg += "<br>保存前请填写【运输单位】。";
                            //                                checking = false;
                            //                            }
                            //                            if (isEmpty1(one)) {
                            //                                errorMsg += "<br>保存前请填写【甲方人】。";
                            //                                checking = false;
                            //                            }
                            //                            if (isEmpty1(tow)) {
                            //                                errorMsg += "<br>保存前请填写【乙方人】。";
                            //                                checking = false;
                            //                            }
                            if (isEmpty1(date2)) {
                                errorMsg += "<br>保存前请填写【签订日期】。";
                                checking = false;
                            }
                            if (isEmpty1(date3)) {
                                errorMsg += "<br>保存前请填写【供货起始时间】。";
                                checking = false;
                            }
                            if (isEmpty1(date4)) {
                                errorMsg += "<br>保存前请填写【供货截至时间】。";
                                checking = false;
                            }
                            //                            if ((stickmoney * 1 + nomoney * 1) != money) {
                            //                                errorMsg += "<br>开票金额与挂账金额不等于总价【供货截至时间】。";
                            //                                checking = false;
                            //                            }
                            if (checking == false || !checking) {
                                showError('验证错误', errorMsg);
                                return false;
                            }

                            ParaArr[0] = $('#purchase_id').val();
                            ParaArr[1] = num;
                            ParaArr[2] = price;
                            ParaArr[3] = money;
                            ParaArr[4] = tele;
                            ParaArr[5] = man;
                            ParaArr[6] = date1;
                            ParaArr[7] = stickmoney;
                            ParaArr[8] = nomoney;
                            ParaArr[9] = stickno;
                            ParaArr[10] = contractname;
                            ParaArr[11] = supplytel;
                            ParaArr[12] = supply
                            ParaArr[13] = one;
                            ParaArr[14] = tow;
                            ParaArr[15] = date2;
                            ParaArr[16] = date3;
                            ParaArr[17] = date4;

                            ParaArr[18] = supplytel1;
                            ParaArr[19] = supply1;
                            ParaArr[20] = supplyid;
                            return checking;
                        },
                        afterFormLoaded: function () {
                            supplyChange();
                            //填充物资名称 物资名称不能修改
                            $('#goodsname').val(record.GoodsName);
                            $('#goodsname')[0].disabled = true;

                            $('#num1')[0].disabled = true;

                            //加载项
                            ajaxRequest("/Purchase.mvc/GetObj", { id: record.ID }, false, function (response) {
                                if (response.Result) {
                                    $('#purchase_id').val(response.Data[0]);
                                    $('#num').val(response.Data[1]);
                                    //$('#num1').val(response.Data[18]);
                                    $('#price').val(response.Data[2]);
                                    $('#money').val(response.Data[3]);
                                    $('#tele').val(response.Data[4]);
                                    $('#man').val(response.Data[5]);
                                    $('#date1').val(response.Data[6]);
                                    $('#stickmoney').val(response.Data[7]);
                                    $('#nomoney').val(response.Data[8]);
                                    $('#stickno').val(response.Data[9]);
                                    $('#contractname').val(response.Data[10]);
                                    $('#supplytel').val(response.Data[11]);

                                    //$('#supply').val(response.Data[12]);
                                    tmpGrid.getFormField("supply").val(response.Data[12]);

                                    $('#one').val(response.Data[13]);
                                    $('#tow').val(response.Data[14]);
                                    $('#date2').val(response.Data[15]);
                                    $('#date3').val(response.Data[16]);
                                    $('#date4').val(response.Data[17]);

                                    $('#supplytel1').val(response.Data[18]);
                                    $('#supply1').val(response.Data[19]);
                                    tmpGrid.getFormField("SupplyID").val(response.Data[20]);
                                }
                            });
                            $("#rknum").val(record.Purchase_Num1);
                            //更新不可以修改数量
                            $('#num')[0].disabled = false;
                        }
                        , postData: { ParaArrt: ParaArr }
                    });
                },
                //提交
                handleSubmitPurchase: function (btn) {
                    var record = tmpGrid.getSelectedRecord();
                    if (record.Purchase_State == 'undefined' || record.Purchase_State == null) {
                        showMessage('提示', "请选择要提交的记录!");
                        return;
                    }
                    if (record.Purchase_State == '1') {
                        showMessage('提示', "已经提交，不能重复提交!");
                        return;
                    }

                    showConfirm("确认提交", "确认要提交选中的记录", function () {
                        if (record.Purchase_State == "0") {
                            var ids = record.ID;
                            ajaxRequest(btn.data.Url, { id: ids }, true, function (response) {
                                if (response.Result) {
                                    tmpGrid.reloadGrid();
                                    TaskGrid.reloadGrid();
                                }
                            });
                        }
                        else {
                            showMessage('提示', "不能重复提交!");
                            return;
                        }
                    });
                },
                handleadd: function (btn) {
                    var keys = TaskGrid.getSelectedKeys();
                    if (keys.length == 0) {
                        showMessage('提示', "请选择执行计划主表记录!");
                        return;
                    }

                    var record = TaskGrid.getSelectedRecord();


                    if (record.Main_Sate == 'undefined' || record.Main_Sate == null) {
                        showMessage('提示', "请选择主表记录!");
                        return;
                    }

                    if (record.Main_Sate == '1') {
                        showMessage('提示', "任务已经完成不能新增子记录!");
                        return;
                    }

                    ParaArr = new Array();

                    tmpGrid.handleAdd({
                        loadFrom: 'MyFormDiv'
                        , btn: btn
                        , beforeSubmit: function () {
                            //都不能为空
                            var goodsname = $('#goodsname').val();
                            var num = $('#num').val();
                            //var num1 = $('#num1').val();
                            var price = $('#price').val();
                            var money = $('#money').val();
                            var tele = $('#tele').val();
                            var man = $('#man').val();
                            var date1 = $('#date1').val();
                            var stickmoney = $('#stickmoney').val();
                            var nomoney = $('#nomoney').val();
                            var stickno = $('#stickno').val();
                            var contractname = $('#contractname').val();
                            var supplytel = $('#supplytel').val();
                            //var supply = $('#supply').val();
                            var supply = tmpGrid.getFormField("supply").val();
                            var supplyid = tmpGrid.getFormField("SupplyID").val();
                            var one = $('#one').val();
                            var tow = $('#tow').val();
                            var date2 = $('#date2').val();
                            var date3 = $('#date3').val();
                            var date4 = $('#date4').val();

                            var supplytel1 = $('#supplytel1').val();
                            var supply1 = $('#supply1').find('option:selected').text();


                            var errorMsg = '';
                            var checking = true;
                            if (isEmpty1(num)) {
                                errorMsg += "<br>保存前请填写采购【数量】。";
                                checking = false;
                            }
//                            if (num * 1 <= 0) {
//                                errorMsg += "<br>【数量】必须大于0。";
//                                checking = false;
//                            }

                            //                            if (isEmpty1(num1)) {
                            //                                errorMsg += "<br>保存前请填写采购【入库数量】。";
                            //                                checking = false;
                            //                            }
                            //                            if (num1 * 1 <= 0) {
                            //                                errorMsg += "<br>【入库数量】必须大于0。";
                            //                                checking = false;
                            //                            }

                            if (isEmpty1(price)) {
                                errorMsg += "<br>保存前请填写采购【单价】。";
                                checking = false;
                            }
                            if (price * 1 <= 0) {
                                errorMsg += "<br>【单价】必须大于0。";
                                checking = false;
                            }

                            if (isEmpty1(money)) {
                                errorMsg += "<br>保存前请填写采购【总价】。";
                                checking = false;
                            }
//                            if (money * 1 <= 0) {
//                                errorMsg += "<br>【总价】必须大于0。";
//                                checking = false;
//                            }

                            if (isEmpty1(tele)) {
                                errorMsg += "<br>保存前请填写【采购人电话】。";
                                checking = false;
                            }
                            if (isEmpty1(man)) {
                                errorMsg += "<br>保存前请填写【采购人】。";
                                checking = false;
                            }
                            if (isEmpty1(date1)) {
                                errorMsg += "<br>保存前请填写【采购时间】。";
                                checking = false;
                            }
                            if (isEmpty1(stickmoney)) {
                                errorMsg += "<br>保存前请填写【开票金额】。";
                                checking = false;
                            }
                            if (stickmoney * 1 < 0) {
                                errorMsg += "<br>【开票金额】必须大于等于0。";
                                checking = false;
                            }

                            if (isEmpty1(nomoney)) {
                                errorMsg += "<br>保存前请填写【现金金额】。";
                                checking = false;
                            }
                            if (nomoney * 1 < 0) {
                                errorMsg += "<br>【现金金额】不能小于0。";
                                checking = false;
                            }
                            if (isEmpty1(contractname)) {
                                errorMsg += "<br>保存前请填写【合同名称】。";
                                checking = false;
                            }
                            if (isEmpty1(supplytel)) {
                                errorMsg += "<br>保存前请填写【辅料供货单位电话】。";
                                checking = false;
                            }
                            var supplyFileid = tmpGrid.getFormField("SupplyID");
                            if (supplyFileid.val() == "") {
                                errorMsg += "<br>保存前请填写【辅料供货单位】。";
                                checking = false;
                            }
                            //                            if (isEmpty1(supplytel1)) {
                            //                                errorMsg += "<br>保存前请填写【运输单位电话】。";
                            //                                checking = false;
                            //                            }
                            //                            if (isEmpty1(supply1)) {
                            //                                errorMsg += "<br>保存前请填写【运输单位】。";
                            //                                checking = false;
                            //                            }
                            //                            if (isEmpty1(one)) {
                            //                                errorMsg += "<br>保存前请填写【甲方人】。";
                            //                                checking = false;
                            //                            }
                            //                            if (isEmpty1(tow)) {
                            //                                errorMsg += "<br>保存前请填写【乙方人】。";
                            //                                checking = false;
                            //                            }
                            if (isEmpty1(date2)) {
                                errorMsg += "<br>保存前请填写【签订日期】。";
                                checking = false;
                            }
                            if (isEmpty1(date3)) {
                                errorMsg += "<br>保存前请填写【供货起始时间】。";
                                checking = false;
                            }
                            if (isEmpty1(date4)) {
                                errorMsg += "<br>保存前请填写【供货截至时间】。";
                                checking = false;
                            }
                            //                            if ((stickmoney * 1 + nomoney * 1) != money) {
                            //                                errorMsg += "<br>开票金额与挂账金额不等于总价【供货截至时间】。";
                            //                                checking = false;
                            //                            }
                            if (checking == false || !checking) {
                                showError('验证错误', errorMsg);
                                return false;
                            }

                            if ((record.Main_Num * 1 - record.Main_num1 * 1) < $('#num').val() * 1) {
                                showMessage('提示', "已经超过购买额度!");
                                return;
                            }

                            ParaArr[0] = main_id;
                            ParaArr[1] = num;
                            ParaArr[2] = price;
                            ParaArr[3] = money;
                            ParaArr[4] = tele;
                            ParaArr[5] = man;
                            ParaArr[6] = date1;
                            ParaArr[7] = stickmoney;
                            ParaArr[8] = nomoney;
                            ParaArr[9] = stickno;
                            ParaArr[10] = contractname;
                            ParaArr[11] = supplytel;
                            ParaArr[12] = supply;
                            ParaArr[13] = one;
                            ParaArr[14] = tow;
                            ParaArr[15] = date2;
                            ParaArr[16] = date3;
                            ParaArr[17] = date4;

                            ParaArr[18] = supplytel1;
                            ParaArr[19] = supply1;
                            ParaArr[20] = supplyid;
                            return checking;
                        }
                                , afterFormLoaded: function () {
                                    supplyChange();
                                    //填充物资名称 物资名称不能修改
                                    $('#goodsname').val(record.GoodsName);
                                    $('#goodsname')[0].disabled = true;
                                    //$('#num1').val(record.Purchase_Num1);
                                    //$('#num1')[0].disabled = true;
                                    //合同名称
                                    var nowdate = new Date();
                                    var m = nowdate.getMonth() + 1
                                    var d = nowdate.getFullYear() + "年" + m + "月" + nowdate.getDate() + "日"
                                    d = d + record.GoodsName + '采购合同';
                                    $('#contractname').val(d);
                                    //填充用户和电话
                                    ajaxRequest("/Purchase.mvc/GetNameAndTel", null, false, function (response) {
                                        if (response.Result) {
                                            $("#man").val(response.Data[0]);
                                            $("#tele").val(response.Data[1]);
                                        }
                                    });
                                    //添加可以修改数量
                                    $('#num')[0].disabled = false;
                                }
                                , postData: { ParaArrt: ParaArr }

                    });

                }
            }
    });

    tmpGrid.addListeners('rowclick', function (id, record, selBool) {
        //tmpGrid.refreshGrid("Purchase_ID='" + id + "'");
        Purchase_ID = id;
    });

    $(function () {

        //                //文本更改
        //                $("#supply").change(function () {
        //                    var str = $(this).val();
        //                    $('#supplytel').val(str);
        //                    //                    if (!isEmpty(str)) {
        //                    //                        ajaxRequest("/Purchase.mvc/GetLastTel", { name: str }, false, function (response) {
        //                    //                            if (response.Result) {
        //                    //                                $('#supplytel').val(response.Data == null ? "" : response.Data);
        //                    //                            }
        //                    //                        });

        //                    //                    }
        //                    //                    else {
        //                    //                        $('#supplytel').val();
        //                    //                    }
        //                });

        //        $("#supply").change(function () {
        //            var str = $(this).val();
        //            $('#supplytel').val(str);
        //        });
        $("#supply1").change(function () {
            var str = $(this).val();
            $('#supplytel1').val(str);
        });

        //总价数量单价换算
        $('#num').blur(function () {
            var num = $('#num').val();
            var price = $('#price').val();
            var money = $('#money').val();

            if (isEmpty(price) && isEmpty(money))
                return;
            if (isEmpty(price))
                return $('#price').val((money * 1) / (num * 1));
            return $('#money').val((price * 1) * (num * 1));
        });
        $('#price').blur(function () {
            var num = $('#num').val();
            var price = $('#price').val();
            var money = $('#money').val();

            if (isEmpty(num) && isEmpty(money))
                return;
            if (isEmpty(num))
                return $('#num').val((money * 1) / (price * 1));
            //if (isEmpty(money))
            return $('#money').val((price * 1) * (num * 1));
        });
        $('#money').blur(function () {
            var num = $('#num').val();
            var price = $('#price').val();
            var money = $('#money').val();

            if (isEmpty(price) && isEmpty(num))
                return;
            if (isEmpty(price))
                return $('#price').val((money * 1) / (num * 1));
            if (isEmpty(num))
                return $('#num').val((money * 1) / (price * 1));
        });
    });
    //供应商改变事件
    supplyChange = function () {
        //var supplyFileid = $("#MyFormDiv :text[name='SupplyID']"); 
        var supplyFileid = tmpGrid.getFormField("SupplyID");
        supplyFileid.unbind('change');
        supplyFileid.bind('change', function (event) {
            var str = supplyFileid.val();
            if (!isEmpty(str)) {
                ajaxRequest("/SupplyInfo.mvc/GetSupplyTel", { supplyid: str }, false, function (response) {
                    if (response.Result) {
                        $('#supplytel').val(response.Data == null ? "" : response.Data);
                    }
                });

            }
            else {
                $('#supplytel').val();
            }
        });
    };
}