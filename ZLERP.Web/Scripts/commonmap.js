
var iconArr = {
    trackIcon: "../../Content/erpimage/mapimage/map/b-1.gif",
    trackDot: "../../Content/erpimage/mapimage/map/b-2.gif",
    yardIcon: "../../Content/erpimage/mapimage/map/GOLF1-32.gif",
    hotmarkIcon: "../../Content/erpimage/mapimage/map/map_mark.gif",
    bankIcon: "../../Content/erpimage/mapimage/map/bank.png",
    circleDot: "../../Content/erpimage/mapimage/map/circle.png",
    bookIcon: "../../Content/erpimage/mapimage/map/book.png",
    foodIcon: "../../Content/erpimage/mapimage/map/food.png",
    gasStationIcon: "../../Content/erpimage/mapimage/map/gasStation.png",
    hospitalIcon: "../../Content/erpimage/mapimage/map/hospital.png",
    hotelIcon: "../../Content/erpimage/mapimage/map/hotel.png",
    movieIcon: "../../Content/erpimage/mapimage/map/movie.png",
    shopIcon: "../../Content/erpimage/mapimage/map/shop.png",
    stationIcon: "../../Content/erpimage/mapimage/map/station.png",
    startIcon: "../../Content/erpimage/mapimage/map/markerS.png",
    endIcon: "../../Content/erpimage/mapimage/map/markerE.png",
    travIcon: "../../Content/erpimage/mapimage/map/trav.png",
    factIcon: "../../Content/erpimage/mapimage/map/FARM1-32.gif",
    carImageRoot: "../../Content/erpimage/mapimage/map/Car/"
};
 


//页面载入时，读取所有工地信息
//if (gSysConfig.Gps == 'true') {
//    ajaxRequest('Company.mvc/GetAllCompanyForMap', null, false, function (response) {
//        if (response) {
//            companyInfo = response;
//            if (isEmpty(companyInfo[0].lon) || isEmpty(companyInfo[0].lat)) {
//                companyInfo[0].lon = 112.88677443;
//                companyInfo[0].lat = 28.21513581;
//            }
//        }
//    });
//}
//else {
//    companyInfo = [{
//        lon : 112.88677443,
//        lat : 28.21513581
//    }];
//}

 

function MapIconLayerFmt(cellvalue, options, cell) {
    var MapIconDic = gDics["HotMarkIcon"];
    var imgUrl = cellvalue;
    $.each(MapIconDic, function (n, v) {
        if (v.ID == cellvalue) {
            imgUrl = "<img  src='../../Content/erpimage/mapimage/map/" + v.DicName + "'/>";
            imgUrl = '<span rel="' + cellvalue + '">' + imgUrl + '</span>';
            return false;
        }
    });

    return imgUrl;
}

//----------------------------------------------   ///获取图标 begin
function getCarIcon(data) {
    var dirc = parseInt(data["Direction"]);
    var carType = parseInt(data["CarType"]);
    if (isNaN(carType) || carType == 'CT1') carType = 1;
    else carType = 2; //暂时没有第三套车辆图表
    if (isNaN(dirc)) dirc = 0;
    else {
        dirc = Math.floor(((dirc + 15) % 360) / 30);
    }
    var speed = parseInt(data["Speed"]);
    var isStop = false, accFlag = "0", isRoll = false;
    if (isNaN(speed) || speed == 0) isStop = true;
    if (data["AccFlag"] == "1") accFlag = "1";
    if (data["BeaterStatus"] == "2") isRoll = true;
    if (data.AlarmCount > 0) {
        if (isRoll) {
            return iconArr.carImageRoot + carType + "/roll1.gif";
        } else {
            if (isStop)
                return iconArr.carImageRoot + carType + "/red/stop.png";
            else
                return iconArr.carImageRoot + carType + "/red/" + (dirc * 30) + ".gif";
        }
    }
    else {
        if (isRoll) {
            return iconArr.carImageRoot + carType + "/roll.gif";
        } else {
            if (isStop) {
                if (accFlag == "1")
                    return iconArr.carImageRoot + carType + "/green/stop.png";
                else
                    return iconArr.carImageRoot + carType + "/stop.gif";
            }
            else {
                return iconArr.carImageRoot + carType + "/green/" + (dirc * 30) + ".gif";
            }
        }
    }
}
//----------------------------------------------   ///获取图标 end
//----------------------------------------------   ///获取图标 end

 
 

function carInfoWindow(data) {

    var content =
    "更新时间:<bluefont>" + data["Sendtime"]
    + "</bluefont><br />任务信息:<bluefont>" + data["TaskName"]
    + "</bluefont><br />方量:<bluefont>" + data["ParCube"]
    + "</bluefont><br />速度:<bluefont>" + data["Speed"] + "<nbsp />(km/h)"
    + "</bluefont><nbsp /><nbsp /><nbsp /><nbsp /><nbsp /><nbsp /><nbsp />方向:<bluefont>" + data["Direction"] + "</bluefont><br />"
    + "本日车数:<bluefont>" + data["DayCount"] + "</bluefont><nbsp /><nbsp /><nbsp /><nbsp /><nbsp /><nbsp /><nbsp />"
    + "本月车数:<bluefont>" + data["MonthCount"] + "</bluefont><br /><bluefont>";
    var count = 1;

    if (gSysConfig.AddOilFn == true || gSysConfig.AddOilFn == 'true') {
        if (count == 1) {
            content = content + "</bluefont>油量:<bluefont>" + data["Oil"];
        } else {
            content = content + "</bluefont><nbsp />油量:<bluefont>" + data["Oil"];
        }
        count = count + 1;
    }
    if (gSysConfig.AddAccFlagFn == true || gSysConfig.AddAccFlagFn == 'true') {
        if (count == 1) {
            content = content + "</bluefont>车辆状态:<bluefont>" + showAcc(data["AccFlag"]);
        } else {
            content = content + "</bluefont><nbsp />车辆状态:<bluefont>" + showAcc(data["AccFlag"]);
        }
        count = count + 1;
    }
    
    if ((gSysConfig.AddBeaterStatusFn == true || gSysConfig.AddBeaterStatusFn == 'true') && (data["CarType"] == 'CT1')) {

        content = content + "</bluefont><br />背罐状态:<bluefont>" + showBeater(data["BeaterStatus"]);
    }
    if (gSysConfig.AddDistanceFn == true || gSysConfig.AddDistanceFn == 'true') {
        content = content + "</bluefont><nbsp />GPS里程:<bluefont>" + data["Distance"] + "<nbsp />(km)";
    }

    content = content + "</bluefont><br />";
    var alarmcount = parseInt(data["AlarmCount"]);

    if (alarmcount > 0) {

        content = content + "报警信息:<redfont><br />";
        var jsondata = eval(data["AlarmInfos"]);

        for (var i = 0; i < jsondata.length; i++) {
            content = content + (i + 1) + ". " + jsondata[i].alarmInfo + "<br />";
        }
        content = content + "</readfont><br />";
    }

    return content;
}


function showAlarm(cellvalue, options, rowObject) {
    if (cellvalue == "0") {
        var celltitle = "无报警";
        return "<span class='noalarm' title='" + celltitle + "'><span/>";
    } else {
        var celltitle = "报警数：" + cellvalue;
        return "<span class='alarm' title='" + celltitle + "'><span/>";
    }
}
function showAcc(cellvalue, options, rreowObject) {
   
    if (cellvalue == "1") {
        var celltitle = "点火";
    } else if (cellvalue == "0") {
        var celltitle = "熄火";
    } else {
        var celltitle = "未知";
    }
    return '<span rel="' + cellvalue + '">' + celltitle + '</span>'
}

function regeocode(cellvalue, options, rowObj) {


    var lon = rowObj[6];
    var lat = rowObj[7];
  
    var geocoderOption = {
        range: 300, //范围
        crossnum: 0, //道路交叉口数
        roadnum: 1, //路线记录数
        poinum: 0//POI点数
    };
     
    var geocoder = new MMap.Geocoder(geocoderOption);

    geocoder.regeocode(new MMap.LngLat(lon, lat), function (data) {

        if (data.status == 'E0' && data.list.length > 0) {
            var addr = '';
            if (isEmpty(data.list[0].city.name))
                addr = data.list[0].province.name;
            else
                addr = data.list[0].city.name;
            addr += data.list[0].district.name;
            if (data.list[0].roadlist.length > 0) {
                addr += data.list[0].roadlist[0].name;
            }
             
            return addr;

        }
        else
            return '';
    });

}

function showBeater(cellvalue, options, rowObject) {
    //0：熄火，1：搅拌，2：卸料，3：停止，4：未知
    if (cellvalue == "0") {
        var celltitle = "熄火";
    } else if (cellvalue == "1") {
        var celltitle = "搅拌";
    } else if (cellvalue == "2") {
        var celltitle = "卸料";
    } else if (cellvalue == "3") {
        var celltitle = "停止";
    } else {
        var celltitle = "未知";
    }
    return '<span rel="' + cellvalue + '">' + celltitle + '</span>';
}
function showDirct(cellvalue, options, rowObject) {
    var ratotion = 3; //0, 1, 2,和3
    var titlevalue = cellvalue;
    var cellvalue = cellvalue - 45;
    if (cellvalue >= -45 && cellvalue < 45) { ratotion = 0; } else if (cellvalue >= 45 && cellvalue < 135) { ratotion = 1; } else if (cellvalue >= 135 && cellvalue < 225) { ratotion = 2; } else if (cellvalue >= 225 && cellvalue < 315) { ratotion = 3; } else { ratotion = 0; }
    var cssstr = "margin-top:3px;-webkit-transform: rotate(" + cellvalue + "deg);-moz-transform: rotate(" + cellvalue + "deg);-o-transform: rotate(" + cellvalue + "deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=" + ratotion + ");";
    return "<img style='" + cssstr + "' src='../../Content/images/mapimages/pointer.png' alt='" + titlevalue + "' title='顺时针" + titlevalue + "度' heith='20px'/>";
}

   
 