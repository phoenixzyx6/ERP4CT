 
function unloadRecordIndexInit(options) {
    var unloadRecordGrid = new MyGrid({
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
          { name: 'ID', index: 'ID', hidden: true }
        , { label: '车辆编号', name: 'CarID', index: 'CarID', align: 'center', width: 70 }
        , { label: '车牌号', name: 'CarNo', index: 'CarNo', width: 80 }
        , { label: '司机姓名', name: 'DriverName', index: 'DriverName', width:80 }
   		, { label: '发货单编号', name: 'ShipDocID', index: 'ShipDocID', align:'center',width: 80 }
        , { label: '混凝土信息', name : 'ConcreteInfo',index :'ConcreteInfo', width :160}
        , { label: '工地编号', name : 'ProjectID',index :'ProjectID', width :80}
        , { label: '工地名称', name: 'ProjectName', index: 'ProjectName', width: 180,  searchoptions: { sopt: ['cn']} }
   		, { label: '工地卸料', name: 'IsInProject', index: 'IsInProject',align:'center', width: 60, formatter: booleanFmt, unformat: booleanUnFmt, stype: 'select', searchoptions: { value: booleanSearchValues()} }
   		, { label: '卸料时间', name: 'UnloadTime', index: 'UnloadTime', width: 120, align: 'left', formatter: 'datetime', search: false }
        , { label: "经度", name: 'Longitidue', index: 'Longitidue', width: 50, hidden: true }
        , { label: "纬度", name: 'Latitude', index: 'Latitude', width: 50, hidden: true }
        , { label: "操作", name: 'operation', index: 'operation', width: 50 ,align:'center',formatter:LatlonShow, search: false ,sortable :false}
   		   ]
	    , autoLoad: false
       
    });


    function doSearchUnloadRecord() {
        var condition = "1=1";
        var carID = $("[name='CarID']", "#searchbar").val();
        
        var StartTime = $("[name='startTime']", "#searchbar").val();
        var endTime = $("[name='endTime']", "#searchbar").val();
        if (!isEmpty(carID)) {
            condition += " and CarID = '" + carID + "'";
        }
        
        if (!isEmpty(StartTime)) {
            condition += " and UnloadTime >= '" + StartTime + "'";
        }
        if (!isEmpty(endTime)) {
            condition += " and UnloadTime <= '" + endTime + "'";
        }

        unloadRecordGrid.refreshGrid(condition);
    }

    function LatlonShow(cellvalue, options, rowObject) {
        var cell = "<a href='javascript:void(0)' data-map-id ='" + rowObject.ID + "'><bluefont>查看</bluefont></a>";
        return cell;
    }


    $('a[data-map-id]').die('click');

    $('a[data-map-id]').live('click', function (e) {
        if (e.preventDefault)
            e.preventDefault();
        else
            e.returnValue = false;

        $('#div_map_dialog').empty();
        var data = unloadRecordGrid.getRecordByKeyValue($(this).attr('data-map-id'));

        $('#div_map_dialog').dialog({ modal: true, autoOpen: true, bgiframe: false, resizable: false, width: 950, height: 540, open: function () {
            var mapDialog = new abcMap();        //绘制地图
            var opts = {
                render: "div_map_dialog",
                cLng: companyinfo.lon,
                cLat: companyinfo.lat,
                level: 14
            };
            mapDialog.init(opts);
            mapDialog.geocodeSearch(data.Longitidue, data.Latitude, function (resultStr) {
                if ($('#ui-dialog-title-div_map_dialog').length > 0) {
                    $('#ui-dialog-title-div_map_dialog').html("卸料参考地址： " + resultStr);
                }
            });
            var content = "<table class='tb_cr_img'><tr><td><img src='" + iconArr.trackIcon + "'/></td></tr></tr><tr><td><span>" + data.CarID + "</span></td></tr></table>";
            mapDialog.addPoints([{ lng: data.Longitidue, lat: data.Latitude, iurl: iconArr.trackIcon, content: content, id: 'car' + data.CarID}], true);
        }, 
        close: function () {
            $('#div_map_dialog').empty();
        }

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

    $('#UnloadSearchButton').unbind('click');
    $('#UnloadSearchButton').bind('click', doSearchUnloadRecord);
}