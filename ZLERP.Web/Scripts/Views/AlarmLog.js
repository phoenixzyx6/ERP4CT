

function alarmLogIndexInit(options) {
    var alarmLogGrid = new MyGrid({
        renderTo: 'MyJqGrid'
            , height: gGridHeight
            , autoWidth: true
		    , storeURL: options.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , setGridPageSize: 100
		    , showPageBar: true
            , singleSelect: true
            , initArray: [
          { name: 'ID', index: 'ID', hidden:true }
        , { label: '车辆编号', name: 'CarID', index: 'CarID', align: 'center', width: 90  }
        , { label: '车牌号', name: 'CarNo', index: 'CarNo', width: 120 }
   		, { label: '报警类型', name: 'AlarmTypeID', index: 'AlarmTypeID', align: 'left', width: 120, formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'GPSAlarmType' }, stype: 'select', searchoptions: { value: dicToolbarSearchValues('GPSAlarmType')} }
        , { label: '当前司机', name: 'DriverName', index: 'DriverName', width: 100, align:'left', searchoptions: { sopt: ['cn']} }
   		, { label: '报警说明', name: 'AlarmData', index: 'AlarmData', width: 400, formatter: LatlonOpre, searchoptions: { sopt: ['cn']} }
   		, { label: '报警时间', name: 'AlarmTime', index: 'AlarmTime', width: 200, align: 'left', formatter: 'datetime',search:false }
   		   ]
	    , autoLoad: false
    });


    function doSearchAlarmLog() {
        var condition = "1=1";
        var carID = $("[name='CarID']", "#searchbar").val();
        var AlarmType = $("[name='DicID']", "#searchbar").val();
        var StartTime = $("[name='startTime']", "#searchbar").val();
        var endTime = $("[name='endTime']", "#searchbar").val();
        if (!isEmpty(carID)) {
            condition += " and CarID = '" + carID + "'";
        }
        if (!isEmpty(AlarmType)) { 
            condition += " and AlarmTypeID ='"+ AlarmType +"'";
        }
        if (!isEmpty(StartTime)) {
            condition += " and AlarmTime >= '" + StartTime + "'";
        }
        if (!isEmpty(endTime)) {
            condition += " and AlarmTime <= '" + endTime + "'";
        }

        alarmLogGrid.refreshGrid(condition);
    }

    function LatlonOpre(cellvalue, options, rowObject) {
        if (cellvalue.indexOf("当前卸料地点") >= 0 && cellvalue.indexOf("（经度") > 0) {
            var lonlat = cellvalue.match(/\d+\.?\d+/g);
            cellvalue = cellvalue.replace("（经度", "<a href='javascript:void(0)' data-map-id ='" + rowObject.ID + "' data-lon = "+ lonlat[0]+ " data-lat = "+ lonlat[1] +"><bluefont>（经度");
            cellvalue = cellvalue.replace("）", "）</bluefont></a>");
        }
        return cellvalue;
    }

    $('a[data-map-id]').die('click');
    $('a[data-map-id]').live('click', function (e) {
        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;

        $('#div_map_dialog').empty();
        var data = alarmLogGrid.getRecordByKeyValue($(this).attr('data-map-id'));
        var lon = $(this).attr('data-lon');
        var lat = $(this).attr('data-lat');
        $('#div_map_dialog').dialog({ modal: true, autoOpen: true, bgiframe: false, resizable: false, width: 950, height: 540, open: function () {
            var mapDialog = new abcMap();        //绘制地图
            var opts = {
                render: "div_map_dialog",
                cLng: companyinfo.lon,
                cLat: companyinfo.lat,
                level: 14
            };
            mapDialog.init(opts);

            mapDialog.geocodeSearch(lon, lat, function (resultStr) {
                if ($('#ui-dialog-title-div_map_dialog').length > 0) {
                    $('#ui-dialog-title-div_map_dialog').html("卸料参考地址： " + resultStr);
                }
            });
            var content = "<table class='tb_cr_img'><tr><td><img src='" + iconArr.trackIcon + "'/></td></tr></tr><tr><td><span>" + data.CarID + "</span></td></tr></table>";
            mapDialog.addPoints([{ lng: lon, lat: lat, iurl: iconArr.trackIcon, content: content, id: 'car' + data.CarID}], true);
        },
        close: function () { $('#div_map_dialog').empty(); }

        });
    });

    var dates = $("#startTime, #endTime").datetimepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 2,
        showSecond: true,
        timeFormat: 'hh:mm:ss',
        onSelect: function (selectedDate) {
            var option = this.id == "startTime" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
    });

    $('#AlarmSearchButton').unbind('click');
    $('#AlarmSearchButton').bind('click', doSearchAlarmLog);
}