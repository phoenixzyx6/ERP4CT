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
        this.mapOption.center = new AMap.LngLat(opts.cLng, opts.cLat); //������˾������

        this.mapOption.level = opts.level; //��ͼ��ʾ�����ż��� Ĭ��Ϊ[3,18]��3Ϊ�����ͼ��18Ϊ�ֵ�����ͼ��
        this.mapOption.jogEnable = true; //��ͼ�Ƿ�ʹ�û���Ч����Ĭ��ֵΪtrue�������Կɱ�setStatus/getStatus ��������
        this.mapOption.continuousZoomEnable = true;

        this.map = new AMap.Map(this.mapOption.render, this.mapOption);

        var trafficLayer = new AMap.TileLayer.Traffic();
        trafficLayer.setMap(this.map);

        var map = this.map;

        this.map.plugin(["AMap.ToolBar", "AMap.Scale"], function () {//���ͼ��Ӳ�� ���ع�����
            this.toolbar = new AMap.ToolBar(); //����������
            this.toolbar.autoPosition = false; //���ع����� ��ֹ�Զ���λ
            map.addControl(this.toolbar);
            var scale = new AMap.Scale(); //���ر�����  
            map.addControl(scale);
            //this.mousetool = new AMap.MouseTool(map); //�����깤�߲��
            //map.bind(mousetool, "draw", function (e) {
            //var toolType = e._type;
            //mousetool.close(); //�����ƽ�����ر���깤��
            //});

        });

        this.isOK = true;
    };

    this.setZoomAndCenter = function (lng, lat, level) {//�������ĺ���������
        if (lat == "" || lat == undefined || lng == "" || lng == undefined || level == "" || level == undefined) {
            return;
        }
        this.map.setZoomAndCenter(level, new AMap.LngLat(lng, lat));
    };

    //map�ĵ��ע
    this.addPoints = function (pointArray, isCenter) {
        //����һ��pointArray������������
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
            //iurl ͼ���url
            if (pointArray[i]["iurl"] != "") {
                var sizeIcon = new AMap.Size(32, 32); //Ĭ��ͼ��Ĵ�С
                if (pointArray[i]["imgSize"] != null) { //�����������imgSize,��ȡ���С
                    var imgSize = pointArray[i]["imgSize"];
                    sizeIcon = new AMap.Size(imgSize.width, imgSize.height);
                }
                icon = new AMap.Icon({ size: sizeIcon, image: pointArray[i]["iurl"], imageOffset: new AMap.Pixel(0, 0) }); //sizeͼ���С  imageͼ���ַ imageOffset����ڴ�ͼ��ȡͼλ��0,0��ƫ�� �����Ǹ���
            }

            var markerOption = {
                position: new AMap.LngLat(lng, lat), //��γ��
                offset: new AMap.Pixel(-16, -16), //ƫ����
                autoMove: true, //�Ƿ��Զ��ƶ�
                draggable: false//�Ƿ���϶�
            };
            if (pointArray[i]["offsetx"] != "" && pointArray[i]["offsetx"] != undefined) {
                markerOption.offset = new AMap.Pixel((0 - parseFloat(pointArray[i]["offsetx"])), -16); //ΪmarkerOption���ƫ��
            }
            if (pointArray[i]["id"] != "" && pointArray[i]["id"] != undefined) { //ΪmarkerOption���id
                markerOption.id = pointArray[i]["id"];
            }
            if (pointArray[i]["content"] != "" && pointArray[i]["content"] != undefined) { //ΪmarkerOption�������
                markerOption.content = pointArray[i]["content"];
            }
            if (icon != null) { //ΪmarkerOption���ͼ��
                markerOption.icon = icon;
            }
            var marker = new AMap.Marker(markerOption); //���Ĭ�ϱ�׼��


            //this.map.addOverlays(marker); //���ظ������ zjy
            marker.extData = pointArray[i]["id"];
            //�Ƴ��ϵ�
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


            // ������
            if (pointArray[i]["tipTxt"] != "" && pointArray[i]["tipTxt"] != undefined) {
                //alert(pointArray[i]["tipTxt"]);
                _openInfoWindow(marker, pointArray[i]["tipTxt"]); //tipTxt ���ͼ��չʾ������
            }
            // click event ���ڵ��ͼ����١����첽������Ϣ���� 
            if (pointArray[i]["clickEv"] != "" && pointArray[i]["clickEv"] != undefined) {
                eval(pointArray[i]["clickEv"])(pointArray[i]["id"]); //��getCrAddrEv��������id
            }
            // rightclick event �һ��¼�
            if (pointArray[i]["rtclickEv"] != "" && pointArray[i]["rtclickEv"] != undefined) {
                eval(pointArray[i]["rtclickEv"])(pointArray[i]["id"]); //����bindCarContextMenu��������id
            }
        }
        // ����ʱ�������ĵ�
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
        //���ص�ۺϲ��--��Ҫ��Χ����(δ��������2014.4.13 -- shenlg)
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
            //�ص�ҳ�溯��
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
            tileUrl: "http://mt{1,2,3,0}.google.cn/vt/lyrs=m@142&hl=zh-CN&gl=cn&x=[x]&y=[y]&z=[z]&s=Galil"//��ͨȡͼ����
        });
        this.map.addLayer(google);
    };
    this.removeGoogleLayer = function () {
        this.map.removeLayer("abc_layer_google");
    };
    this._openInfoWindow = function (marker, content) {//markerͼ�� content����
        var inforWindow = new AMap.InfoWindow({ //InfoWindow��Ϣ���� ͬʱֻ�ܴ�һ�� 

            //content��������Ϣ���ڴ�ʱ��ϵͳҪ��������ʾ���ı��ַ�����DOM�ڵ�
            //offset �����˴���Ϣ���ڵĶ�����ê��λ��ƫ������
            //autoMove �Ƿ��Զ�������Ϣ��������Ұ�� 
            //size ��Ϣ���ڴ�С ��
            //isCustom �Ƿ��Զ��崰�� ��
            content: content
                , autoMove: true
        });
        var map = this.map;

        //this.map.bind(marker, "click", function (e) {//�¼� һ������,һ���������¼�,һ�����ú��� zjy
        AMap.event.addListener(marker, "click", function (e) {//�¼� һ������,һ���������¼�,һ�����ú���

            inforWindow.open(map, marker.getPosition()); //�ڵ�ͼ�ϵ���
        });
        this.lastInfowindow = inforWindow; //���һ�������򱣴�
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