
function cardriverpriceIndexInit(storeUrl) {

    var MyJqGridMain = new MyGrid({
        renderTo: 'MyJqGridMain'
        //, width: '100%'
            , title: '价格时间区间'
            , autoWidth: true
            , buttons: buttons1
            , height: gGridHeight
		    , storeURL: "/CarDriverPriceMain.mvc/Find"
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 400
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
		    , initArray: [
                { label: '主项编号', name: 'ID', index: 'ID', hidden: true }
                , { label: '开始日期', name: 'StartDate', index: 'StartDate', formatter: 'date' }
                , { label: '结束日期', name: 'EndDate', index: 'EndDate', formatter: 'date' }
                , { label: '备注', name: 'Meno', index: 'Meno' }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    MyJqGridMain.reloadGrid();
                },
                handleRefresh: function (btn) {
                    MyJqGridMain.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    
                    MyJqGridMain.handleAdd({
                        loadFrom: 'MyFormDivMain',
                        btn: btn,
                        beforeSubmit: function () {
                            var sdate = $("#StartDate").val();
                            if (isEmpty(sdate)) {
                                //设置为必填警示颜色
                                $("#StartDate").addClass('input-validation-error');
                                return false;
                            }
                            $("#StartDate").removeClass("input-validation-error");

                            var edate = $("#EndDate").val();
                            if (isEmpty(edate)) {
                                //设置为必填警示颜色
                                $("#EndDate").addClass('input-validation-error');
                                return false;
                            }
                            $("#EndDate").removeClass("input-validation-error");

                            if (compareTime(sdate + " 00:00:00", edate + " 00:00:00")) {
                                showError('错误', '开始时间不能大于结束时间！');
                                return;
                            }
                            return true;
                        }
                    });
                },
                handleEdit: function (btn) {
                    MyJqGridMain.handleEdit({
                        loadFrom: 'MyFormDivMain',
                        btn: btn,
                        beforeSubmit: function () {
                            var sdate = $("#StartDate").val();
                            if (isEmpty(sdate)) {
                                //设置为必填警示颜色
                                $("#StartDate").addClass('input-validation-error');
                                return false;
                            }
                            $("#StartDate").removeClass("input-validation-error");

                            var edate = $("#EndDate").val();
                            if (isEmpty(edate)) {
                                //设置为必填警示颜色
                                $("#EndDate").addClass('input-validation-error');
                                return false;
                            }
                            $("#EndDate").removeClass("input-validation-error");

                            if (compareTime(sdate + " 00:00:00", edate + " 00:00:00")) {
                                showError('错误', '开始时间不能大于结束时间！');
                                return;
                            }
                            return true;
                        }
                    });
                }
                , handleDelete: function (btn) {
                    MyJqGridMain.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
            }
        });
        MyJqGridMain.addListeners('rowclick', function (id, record, selBool) {
            myJqGrid.refreshGrid("CarDriverPriceMainId='" + id + "'");
        });

        //----------------------------------------------------右边Gird------------------------------------------------------
        var myJqGrid = new MyGrid({
            renderTo: 'MyJqGrid'
            , title: '项目价格列表'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: storeUrl
		    , sortByField: 'ID'
            , sortOrder: "ASC"
		    , primaryKey: 'ID'
            , dialogWidth: 400
            , dialogHeight: 300
		    , setGridPageSize: 30
		    , showPageBar: true
            , groupingView: { groupSummary: [true], groupText: ['<font style="color:red">{0}</font>(<b>{1}</b>)'] }
            , groupField: 'TypeId'
             , editSaveUrl: "/CarDriverPrice.mvc/Update"
		    , initArray: [
                { label: 'ID', name: 'ID', index: 'ID', hidden: true }
                , { label: '类型', name: 'TypeId', index: 'TypeId', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: "KmPriceType" }, stype: "select", searchoptions: { value: dicToolbarSearchValues("KmPriceType")} }
                , { label: '主表编号', name: 'CarDriverPriceMainId', index: 'CarDriverPriceMainId', editable: true, editrules: { required: true }, hidden: true }
                , { label: '开始日期', name: 'StartDate', index: 'StartDate', formatter: 'date', width: 120, hidden: true }
                , { label: '结束日期', name: 'EndDate', index: 'EndDate', formatter: 'date', width: 120, hidden: true }
                , { label: '类型名称', name: 'TypeName', index: 'TypeName', hidden: true }
                , { label: '开始里程', name: 'StartKm', index: 'StartKm' }
                , { label: '结束里程', name: 'EndKm', index: 'EndKm' }
                , { label: '单价', name: 'Price', index: 'Price', editable: true }
		    ]
		    , autoLoad: false
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    if (!MyJqGridMain.isSelectedOnlyOne()) {
                        showError("提示", "请选择左边表格一条记录");
                        return false;
                    }
                    var selectrecord = MyJqGridMain.getSelectedRecord();

                    myJqGrid.handleAdd({
                        loadFrom: 'MyFormDiv',
                        btn: btn,
                        beforeSubmit: function () {
                            var startKm = $("#StartKm").val();
                            var endKm = $("#EndKm").val();

                            if (endKm * 1<startKm * 1) {
                                showError('错误', '开始里程不能大于结束里程！');
                                return;
                            }
                            return true;
                        },
                        afterFormLoaded: function () {

                            $("#CarDriverPriceMainId").val(selectrecord.ID);
                        }
                    });
                },
                handleEdit: function (btn) {
                    myJqGrid.handleEdit({
                        loadFrom: 'MyFormDiv',
                        btn: btn
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
