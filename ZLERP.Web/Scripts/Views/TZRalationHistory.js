//转退料处理
function tzrelationInit(opts) {

    function lockFmt(cellvalue, options, rowObject) {
        if (cellvalue == '0') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "录入";
            }

        }
        else if (cellvalue == '1') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "分车";
            }

        }
        else if (cellvalue == '2') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "合车";
            }

        }
        else if (cellvalue == '-1') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "删除(分车)";
            }
        }
        else if (cellvalue == '3') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "修改";
            }
        }
        else if (cellvalue == '4') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "修改（磅秤）";
            }
        }
        else if (cellvalue == '5') {
            var style = "color:green;";
            if (typeof (options.colModel.formatterStyle) != "undefined") {
                var txt = options.colModel.formatterStyle[1];
            } else {
                var txt = "暗扣";
            }
        }
        else {
            var txt = '';
        }

        return '<span rel="' + cellvalue + '" style="' + style + '">' + txt + '</span>'
    }

    function lockUnFmt(cellvalue, options, cell) {
        return $('span', cell).attr('rel');
    }
    var myJqGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: ' BuildTime'
		    , primaryKey: 'ID'
		    , setGridPageSize: 30
            , dialogWidth: 600
            , singleSelect: true
            , dialogHeight: 320
		    , showPageBar: true
		    , initArray: [
                { label: '编号', name: 'ID', index: 'ID', width: 60, hidden: true }
                , { label: '转退料编号', name: 'ParentID', index: 'ParentID', width: 150, hidden: true }
                , { label: '初始源运输单号', name: 'SourceShipDocID', index: 'SourceShipDocID', width: 150 }
                , { label: '初始源工程名称', name: 'SourceProjectName', index: 'SourceShippingDocument.ProjectName' }
                , { label: '初始源运输车号', name: 'SourceCarID', index: 'SourceShippingDocument.CarID', width: 150 }
                , { label: '初始源剩退方量', name: 'SourceCube', index: 'SourceCube', width: 150, align: 'center' }
                , { label: '初始源砼强度', name: 'SourceConStrength', index: 'SourceShippingDocument.ConStrength', width: 150 }
                , { label: '操作状态', name: 'IsLock', index: 'IsLock', formatter: lockFmt, unformat: lockUnFmt,width: 150 }

                , { label: '源车号', name: 'operationnum', index: 'operationnum', width: 100 }
                , { label: '源方量', name: 'operationcube', index: 'operationcube', width: 100 }
                , { label: '目标车号', name: 'CarID', index: 'CarID', width: 100 }
                , { label: '目标方量', name: 'Cube', index: 'Cube', width: 100 }
                , { label: '创建时间', name: 'BuildTime', index: 'BuildTime', formatter: 'datetime' }
                , { label: '方式', name: 'operation', index: 'operation', width: 100, hidden: true }
		    ]
		    , autoLoad: true
            , functions: {
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                }
               }
    });

}