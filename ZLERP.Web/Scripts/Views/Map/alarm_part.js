var alarm_part = new function () {
    var render = null;
    var toggleShow = false;
    var table_width = 400;
    var table_height = 300;
    var jqgrid = null;
    var trackIcon = iconArr.trackIcon;
    var pointIcon = null;
    var intervalFn = null;
    this._maplet = null;
    this.opts = {
        pgurl: "../../AlarmLog.mvc/InnerIndex",
        tburl: "../../AlarmLog.mvc/GetAlarmInfos",
        centerPoint: window.centerPoint,
        inetrval: 60
    };
    this.init = function (ops) {
        if (!isEmpty(jqgrid)) return;
        $.extend(this.opts, ops);
        this.afterInsertBlock();
    };
    this.afterInsertBlock = function () {
        $("#" + this.opts.afterRender)
        .after("<div id=\"Part_AlarmBlock\" class=\"alarmBlock\"><span id=\"Part_AlarmNum\"></span></div><div id=\"Part_AlarmTips\" class=\"alarmTips\"><div id=\"Part_AlarmTable\"></div></div>");
        render = $("#Part_AlarmTips");

        $(".alarmBlock").click(tipsToggle);

        this.initTable();

    };
    var tipsToggle = function () {

        $(".alarmTips").toggle("slide", { direction: "right" }, 500, function () {
            toggleShow = !toggleShow;

            if (!toggleShow) {
                jQuery('#Part_AlarmTips-datagrid')[0].clearToolbar();
                jqgrid.refreshGrid('1=1');
            }
        });
        return false;
    };
    this.initMap = function () {
        this._maplet = new Maplet("pgMap");
        this._maplet.clickToCenter = false;
        var cll = this.opts.centerPoint.split(",");
        var midPoint = new MPoint(cll[0], cll[1]);
        this._maplet.centerAndZoom(midPoint, 12);
        this._maplet.showControl(false);
        this._maplet.showOverview(false, false);
        pointIcon = new MIcon(trackIcon, 32, 32);
    };
    function LatlonOpre(cellvalue, options, rowObject) {
        if (cellvalue.indexOf("当前卸料地点") >= 0 && cellvalue.indexOf("（经度") > 0) {
            var lonlat = cellvalue.match(/\d+\.?\d+/g);
            cellvalue = cellvalue.replace("（经度", "<a href='javascript:alarm_part.showMap(" + lonlat[0] + "," + lonlat[1] + ")'><bluefont>（经度");
            cellvalue = cellvalue.replace("）", "）</bluefont></a>");
        }
        return cellvalue;
    };
    this.showMap = function (lon, lat) {
        if (this._maplet) {
            this._maplet.clearOverlays(false);
            var midPoint = new MPoint(lon, lat);
            this._maplet.centerAndZoom(midPoint, 12);
            var carMark = new MMarker(midPoint, pointIcon);
            this._maplet.addOverlay(carMark);

            $("#pgMap").dialog({
                title: "地图位置",
                width: '600',
                height: '400',
                modal: true,
                autoOpen: true
            });
        }
    };
    this.initTable = function () {


        var table_height = $("#Part_AlarmTips").height() - 60;
        var table_width = $("#Part_AlarmTips").width() - 4;

        jqgrid = new MyGrid({
            renderTo: 'Part_AlarmTips'
            , height: table_height
            , width: table_width
		    , storeURL: '/AlarmLog.mvc/GetAlarmInfos'
		    , sortByField: 'carid'
		    , primaryKey: 'id'
            , showPageBar: false
            , toolbarSearch: true
		    , singleSelect: true
            , initArray: [
                { name: 'id', index: 'id', hidden: true }
              , { label: '车辆编号', name: 'carid', index: 'carid', align: 'center', width: 60 }
              , { label: '车牌号', name: 'carNo', index: 'carNo', width: 80 }
   		      , { label: '报警类型', name: 'alarmTypeID', index: 'alarmTypeID', align: 'center', width: 80, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'GPSAlarmType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('GPSAlarmType')} }
              , { label: '当前司机', name: 'driver', index: 'driver', width: 100, align: 'left', searchoptions: { sopt: ['cn']} }
   		      , { label: '报警说明', name: 'alarmInfo', index: 'alarmInfo', width: 230, searchoptions: { sopt: ['cn']} }
   		      , { label: '报警时间', name: 'alarmTime', index: 'alarmTime', formatter: 'datetime', search: false }
   		   ]
	    , autoLoad: true

        });

        jqgrid.addListeners("gridComplete", function () {

            if (isEmpty(jqgrid)) { window.clearTimeout(intervalFn); return; }
            var records = jqgrid.getGridParam("records");

            alarm_part.upAlarmNum(records);

            if (intervalFn != null) {
                window.clearTimeout(intervalFn);
            }
            intervalFn = window.setTimeout(function () { alarm_part.reloadTable(); }, alarm_part.opts.interval * 1000);

        });

        jqgrid.getJqGrid().jqGrid('filterToolbar', { stringResult: true, defaultSearch: 'cn' });

    };
    this.upAlarmNum = function (num) {
        if ($("#Part_AlarmNum") != null) {
            if (num > 0) {
                $("#Part_AlarmNum").html(num);
            } else {
                $("#Part_AlarmNum").html(0);
            }
        }
    };
    this.reloadTable = function () {
        if (!isEmpty(jqgrid))
            jqgrid.reloadGrid();
    };
}
