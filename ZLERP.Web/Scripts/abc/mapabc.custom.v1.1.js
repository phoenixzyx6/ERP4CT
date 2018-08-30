/*!
* MapAbc Custom Plugin
* version: 1.0 (14-3-2014)-- 1.2 (15-4-2014)
* &:syw
* @requires jQuery v1.5 or later
*
*/
//;(function ($) {
abcMap = function () {

    this.init = function (opts) {

        $.extend(this.mapOption, opts);
        this.mapOption.center = new AMap.LngLat(opts.cLng, opts.cLat); //创建公司点坐标

        this.mapOption.level = opts.level; //地图显示的缩放级别 默认为[3,18]。3为世界地图，18为街道级地图。
        this.mapOption.jogEnable = true; //地图是否使用缓动效果，默认值为true。此属性可被setStatus/getStatus 方法控制
        this.mapOption.continuousZoomEnable = true;

        this.map = new AMap.Map(this.mapOption.render, this.mapOption);

        var trafficLayer = new AMap.TileLayer.Traffic();
        trafficLayer.setMap(this.map);

        var map = this.map;

        this.map.plugin(["AMap.ToolBar", "AMap.Scale"], function () {//向地图添加插件 加载工具条
            this.toolbar = new AMap.ToolBar(); //创建工具条
            this.toolbar.autoPosition = false; //加载工具条 禁止自动定位
            map.addControl(this.toolbar);
            var scale = new AMap.Scale(); //加载比例尺  
            map.addControl(scale);
            //this.mousetool = new AMap.MouseTool(map); //添加鼠标工具插件
            //map.bind(mousetool, "draw", function (e) {
            //var toolType = e._type;
            //mousetool.close(); //鼠标绘制结束后关闭鼠标工具
            //});

        });

        this.isOK = true;
    };

    this.setZoomAndCenter = function (lng, lat, level) {//设置中心和收缩级别
        if (lat == "" || lat == undefined || lng == "" || lng == undefined || level == "" || level == undefined) {
            return;
        }
        this.map.setZoomAndCenter(level, new AMap.LngLat(lng, lat));
    };

    //map的点标注
    this.addPoints = function (pointArray, isCenter) {
        //其中一个pointArray对象是这样的
        //clickEv: "getCrAddrEv"
        //content: "<table class='tb_cr_img'><tr><td><img src='../../Content/erpimage/mapimage/map/Car/1/stop.gif'/></td></tr><tr><td><span> 86 </span></td></tr></table>"
        //id: "car86"
        //iurl: ""
        //lat: "29.3918909795322"
        //lng: "106.424725909907"
        //rtclickEv: "bindCarContextMenu"
        //tipTxt: ""

        // pointArray:[{lng:'',lat:'',iurl:'',tipTxt:'',clickEv:'',rtclickEv:'',content:'',id:''}]
        if (pointArray == null || pointArray == undefined || pointArray.length < 1) {
            return;
        }
        var icon = null, content = "", lng = "", lat = "";

        var _t1;

        for (var i = 0; i < pointArray.length; i++) {
            lng = pointArray[i]["lng"], lat = pointArray[i]["lat"];

            if (lat == "" || lng == "") { continue; }
            //iurl 图标的url
            if (pointArray[i]["iurl"] != "") {
                var sizeIcon = new AMap.Size(32, 32); //默认图标的大小
                if (pointArray[i]["imgSize"] != null) { //如果集合中有imgSize,则取其大小
                    var imgSize = pointArray[i]["imgSize"];
                    sizeIcon = new AMap.Size(imgSize.width, imgSize.height);
                }
                icon = new AMap.Icon({ size: sizeIcon, image: pointArray[i]["iurl"], imageOffset: new AMap.Pixel(0, 0) }); //size图标大小  image图标地址 imageOffset相对于大图的取图位置0,0是偏移 上左是负数
            }

            var markerOption = {
                position: new AMap.LngLat(lng, lat), //经纬度
                offset: new AMap.Pixel(-16, -16), //偏移量
                autoMove: true, //是否自动移动
                draggable: false//是否可拖动
            };
            if (pointArray[i]["offsetx"] != "" && pointArray[i]["offsetx"] != undefined) {
                markerOption.offset = new AMap.Pixel((0 - parseFloat(pointArray[i]["offsetx"])), -16); //为markerOption添加偏移
            }
            if (pointArray[i]["id"] != "" && pointArray[i]["id"] != undefined) { //为markerOption添加id
                markerOption.id = pointArray[i]["id"];
            }
            if (pointArray[i]["content"] != "" && pointArray[i]["content"] != undefined) { //为markerOption添加内容
                markerOption.content = pointArray[i]["content"];
            }
            if (icon != null) { //为markerOption添加图标
                markerOption.icon = icon;
            }
            var marker = new AMap.Marker(markerOption); //添加默认标准点


            //this.map.addOverlays(marker); //加载覆盖物↑ zjy
            marker.extData = pointArray[i]["id"];
            //移除老的
            var mk;
            var _mk = _abcMap.map.getAllOverlays();
            for (var _i = 0; _i < _mk.length; _i++) {
                if (_mk[_i].extData == marker.extData) {
                    mk = _mk[_i];
                    mk.setMap();
                    break;
                }
            }
            marker.setMap(this.map);


            // 弹出框
            if (pointArray[i]["tipTxt"] != "" && pointArray[i]["tipTxt"] != undefined) {
                //alert(pointArray[i]["tipTxt"]);
                _openInfoWindow(marker, pointArray[i]["tipTxt"]); //tipTxt 点击图标展示的内容
            }
            // click event 用于点击图标后再《！异步加载信息！》 
            if (pointArray[i]["clickEv"] != "" && pointArray[i]["clickEv"] != undefined) {
                eval(pointArray[i]["clickEv"])(pointArray[i]["id"]); //调getCrAddrEv方法传入id
            }
            // rightclick event 右击事件
            if (pointArray[i]["rtclickEv"] != "" && pointArray[i]["rtclickEv"] != undefined) {
                eval(pointArray[i]["rtclickEv"])(pointArray[i]["id"]); //调用bindCarContextMenu方法传入id
            }
        }
        // 单点时设置中心点
        if (isCenter && pointArray.length == 1 && lat != "" && lng != "") {
            this.map.panTo(new AMap.LngLat(lng, lat));
        }
    };

    this.addLine = function (id, color, pointArray) { // [{lng:'',lat:''},{lng:'',lat:''}]
        if (pointArray == null || pointArray == undefined || pointArray.length < 1) {
            return;
        }
        var opts = {
            id: id,
            path: new Array(),
            strokeColor: color,
            strokeOpacity: 0.8,
            strokeWeight: 5,
            strokeStyle: 'solid'
        };
        for (var i = 0; i < pointArray.length; i++) {
            opts.path.push(new AMap.LngLat(pointArray[i]["lng"], pointArray[i]["lat"]));
        }
        var polyline = new AMap.Polyline(opts);


        // this.map.addOverlays(polyline);zjy
        polyline.extData = id;
        polyline.setMap(this.map);

        return polyline;
    };
    this.addPolygon = function (id, pointArray) { // [{lng:'',lat:''},{lng:'',lat:''}]
        if (pointArray == null || pointArray == undefined || pointArray.length < 1) {
            return;
        }
        this.polygonOption.path = new Array();
        for (var i = 0; i < pointArray.length; i++) {
            this.polygonOption.path.push(new AMap.LngLat(pointArray[i]["lng"], pointArray[i]["lat"]));
        }
        this.polygonOption.id = id;
        var polygon = new AMap.Polygon(this.polylineOption);

        //this.map.addOverlays(polygon);zjy
        polygon.extData = id;
        polygon.setMap(this.map);

    };
    this.addCircle = function (id, lng, lat, radius) {

        this.circleOption.center = new AMap.LngLat(lng, lat);
        this.circleOption.radius = radius;
        this.circleOption.id = id;
        var circle = new AMap.Circle(this.circleOption);

        //this.map.addOverlays(circle);zjy
        circle.extData = id;
        circle.setMap(this.map);

    };
    this.clearMap = function () {
        this.map.clearOverlays();
        this.map.clearInfoWindow();
    };
    //clear id or ids
    this.removeMark = function (id) {
        //this.map.removeOverlays(id);zjy
        var _mk = this.map.getAllOverlays();
        for (var i = 0; i < _mk.length; i++) {
            if (_mk[i].extData == id) {
                _mk[i].setMap();
                break;
            }
        }
    };


    this.clearOverlaysByType = function (type) {
        //        this.map.clearOverlaysByType(type); zjy
        this.map.remove(type);
    };


    this.initMarkerClusterer = function (markers) {
        //加载点聚合插件--需要外围测试(未经过测试2014.4.13 -- shenlg)
        var markerClusterer = this.markerClusterer;
        var map = this.map;
        this.map.plugin("AMap.MarkerClusterer", function () {
            var markerClustererOptions = {
                gridSize: 80,
                minClusterSize: 3
            };
            markerClusterer = new AMap.MarkerClusterer(map, markers, markerClustererOptions);
        });
    };
    this.hideToolbar = function () {
        this.map.removeControl(this.toolbar);
    };
    this.geocodeSearch = function (lng, lat, fn) {
        if (lat == "" || lat == undefined || lng == "" || lng == undefined) { return; }
        var lnglatXY = new AMap.LngLat(lng, lat);
        var geocoderOption = {
            range: 1000,
            crossnum: 2,
            roadnum: 3,
            poinum: 1
        };


        var geo = new AMap.Geocoder(geocoderOption);



        //geo.regeocode(lnglatXY, function (data)  { zjy
        geo.getAddress(lnglatXY, function (data) {

            var resultStr = "";
            if (data.state = "E0" && data.list.length > 0) {
                if (isEmpty(data.list[0].city.name))
                    resultStr = data.list[0].province.name;
                else
                    resultStr = data.list[0].city.name;
                resultStr += data.list[0].district.name;
                if (data.list[0].roadlist.length > 0) {
                    resultStr += data.list[0].roadlist[0].name;
                }

            }
            //回调页面函数
            eval(fn)(resultStr);
        });
    };
    this.addTileLayer_TRAFFIC = function () {
        var tmc_layer = new AMap.TileLayer.Tmc({
            id: "abc_layer_tmc",
            zIndex: 2,
            refresh: 20000,
            opacity: 0.8
        });
        this.map.addLayer(tmc_layer);
    };
    this.removeTileLayer_TRAFFIC = function () {
        this.map.removeLayer("abc_layer_tmc");
    };
    this.addGoogleLayer = function () {
        var google = new AMap.TileLayer({
            id: "abc_layer_google", //ID
            zIndex: 2,
            tileUrl: "http://mt{1,2,3,0}.google.cn/vt/lyrs=m@142&hl=zh-CN&gl=cn&x=[x]&y=[y]&z=[z]&s=Galil"//普通取图规则
        });
        this.map.addLayer(google);
    };
    this.removeGoogleLayer = function () {
        this.map.removeLayer("abc_layer_google");
    };
    this._openInfoWindow = function (marker, content) {//marker图样 content内容
        var inforWindow = new AMap.InfoWindow({ //InfoWindow信息窗口 同时只能打开一个 

            //content包含了信息窗口打开时，系统要在其中显示的文本字符串或DOM节点
            //offset 包含了从信息窗口的顶部到锚定位置偏移量。
            //autoMove 是否自动调整信息窗口至视野内 
            //size 信息窗口大小 。
            //isCustom 是否自定义窗口 。
            content: content
                , autoMove: true
        });
        var map = this.map;

        //this.map.bind(marker, "click", function (e) {//事件 一个对象,一个待监听事件,一个调用函数 zjy
        AMap.event.addListener(marker, "click", function (e) {//事件 一个对象,一个待监听事件,一个调用函数

            inforWindow.open(map, marker.getPosition()); //在地图上弹出
        });
        this.lastInfowindow = inforWindow; //最后一个弹出框保存
    };

    this.map = {};
    this.isOK = false;
    this.markerClusterer = null;
    this.eventLngLat = { lng: null, lat: null };
    this.toolbar = null;
    this.mapOption = {};
    this.lastInfowindow = null;
    this.polylineOption = { path: new Array(), strokeColor: '#32CD32', strokeOpacity: 0.1, strokeWeight: 2, strokeStyle: 'solid' };
    this.polygonOption = { path: new Array(), strokeColor: '#1E90FF', strokeOpacity: 0.5, strokeWeight: 1, fillColor: '#1E90FF', fillOpacity: 0.5 };
    this.circleOption = { center: null, radius: 5, strokeColor: "#1E90FF", strokeOpacity: 0.9, strokeWeight: 1, fillColor: "#1E90FF", fillOpacity: 0.5 };
}; 
//})(jQuery);