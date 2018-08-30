var isInitTree = false;
var _abcMap = new abcMap();

var mapCMenu = null;
var carC = null;
var proC = null;
var qprojMenu = null;

var overlayCache = {};
var overLayerObj = null;
var curpolyid = "";
var isAllowCloseMouseTool = true;
var polygonEditor = null;
var boundPoints = new Array();
var inpolygonOption = { strokeColor: "#ff0000", fillColor: "#f5deb3" };
var outpolygonOption = { strokeColor: "#0f0", fillColor: "#f5deb3" };
var isRightClick = false;
function initPagejs() {

    initLayout();

    initMap();

    initPageElement();

    initCarInfoDiv();

    uplaodPageEvent();

    initTrack();

    routeInit();

    $("#bounddiv").dialog({
        title: "报警区域",
        width: 340,
        height: 180,
        modal: true,
        autoOpen: false,
        buttons: [{ text: '取消', click: function () { $(this).dialog("close"); } }, { text: '确定', click: SetBoundCmp}]
    });

    _abcMap.map.plugin(["AMap.MouseTool"], function () {
        mousetool = new AMap.MouseTool(_abcMap.map);

        //_abcMap.map.bind(mousetool, "draw", completedDraw);zjy
        AMap.event.addListener(mousetool, "draw", completedDraw); 

    });

}

//鼠标工具绘制完成之后
function completedDraw(returnOverLayer) {
    var overLayerType = returnOverLayer._type;

    if (isAllowCloseMouseTool) mousetool.close();
    switch (overLayerType) {
        //根据不同的覆盖物类型，做相应的处理 
        case "marker": //点标注
        case "polygon": //多边形
            var newPath = ArrayUnique(returnOverLayer.getPath());


            //            _abcMap.map.removeOverlays(returnOverLayer.id);zjy
            var _mk = _abcMap.map.getAllOverlays();
            for (var i = 0; i < _mk.length; i++) {
                if (_mk[i].extData == returnOverLayer.extData) {
                    _mk[i].setMap();
                    break;
                }
            }


            if (newPath.length < 3) {
                removeTempBound();
                var options = {
                    label: "设置",
                    icons: {
                        primary: "ui-icon-wrench"
                    }
                };
                $("#setPen").button("option", options);
                $("#show").button("enable");
                $("#onekeyinout").button("enable");
                $("#rule").button("enable");
                showError("提醒：", "区域设置最少需要3点，请重新绘制");
                return;
            }
            var polygon = new AMap.Polygon({
                id: "curaddpoly",
                path: newPath,
                strokeColor: "#1791fc",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#1791fc",
                fillOpacity: 0.35
            });

            //            _abcMap.map.addOverlays(polygon);zjy
            polygon.extData = "curaddpoly";
            polygon.setMap(_abcMap.map);


            overLayerObj = polygon;
            _abcMap.map.plugin(["AMap.PolyEditor"], function () {
                polygonEditor = new AMap.PolyEditor(_abcMap.map, overLayerObj);
                polygonEditor.open();
            });
        case "polyline": //折线
        case "circle": //圆
        default:

    }

}

//----------------------------------------------   ///页面布局 begin
function initLayout() {
    var contentHeight = $("#container").innerHeight() - 10;  //地图的模块内容的高度
    $("#main_content").attr("style", "width: 100%; border: 1px solid blue;height:" + contentHeight + "px");

    outerLayout = $('div.content_div').layout({
        center__paneSelector: ".center_div"
		, east__paneSelector: ".list_div"
		, east__size: 250
		, spacing_open: 10
		, spacing_closed: 10
        , resizable: false
        , sliderTip: "显示/隐藏侧边栏"
        , togglerTip_open: "关闭"
        , togglerTip_closed: "打开"
        , center__onresize: function (pane, $Pane) {
            innerLayout.resizeAll();
        }
    });
    innerLayout = $('div.center_div').layout({
        center__paneSelector: ".map_div"
        , south__paneSelector: ".info_div"
		, south__size: 250
		, spacing_open: 10
		, spacing_closed: 10
        , resizerTip: "可调整大小"
        , sliderTip: "显示/隐藏侧边栏"
        , togglerTip_open: "关闭"
        , togglerTip_closed: "打开"
        , initClosed: true
        , center__onresize: function (pane, $Pane) {
            resizeMapabc($Pane.innerWidth() - 4, $Pane.innerHeight() - 17);
            jQuery("#carinfojqgrd").jqGrid('setGridWidth', $("div.info_div").width() - 2);
            jQuery("#carinfojqgrd").jqGrid('setGridHeight', $("div.info_div").height() - 25);
        }
        , south__onresize: function (pane, $Pane) {
            jQuery("#carinfojqgrd").jqGrid('setGridWidth', $Pane.innerWidth() - 2);
        }
    });
}
//----------------------------------------------   ///页面布局 end
//----------------------------------------------   ///地图相关 begin
function initMap() {

    var dvHeight = $("div.map_div").height() - $("div.btns_div").height() - 6;
    var dvWidth = $("div.center_div").width() - 2;
    $("#dvMap").height(dvHeight);
    $("#dvMap").width(dvWidth);

    var opts = {
        render: "dvMap",
        cLng: factoryLng,
        cLat: factoryLat,
        level: 14
    };

    _abcMap.init(opts);

    //2、绑定右击事件

    initMapContextMenu();


    //    //3.加站的图标
    addFactoryInMap();
    //4.初始化地图上的图标说明
    //initIconTips();

    //5.初始化tips
    //initMapTips();

    //6.按钮初始化
    //显示/隐藏电子围栏
    $("#show").button({
        icons: {
            primary: "ui-icon-folder-open"
        }
    }).click(function () {
        var options;
        if ($.trim($(this).text()) == "显示") {
            options = {
                label: "隐藏",
                icons: {
                    primary: "ui-icon-folder-collapsed"
                }
            };
            ShowAllBoundInMap();
        } else {
            options = {
                label: "显示",
                icons: {
                    primary: "ui-icon-folder-open"
                }
            };
            clearAllBound();
        }
        $(this).button("option", options);
    });
    //设置电子围栏
    $("#setPen").button({
        text: true,
        icons: {
            primary: "ui-icon-wrench"
        }
    }).click(function (btn) {
        var options;
        if ($.trim($(this).text()) == "设置") {
            options = {
                label: "撤销",
                icons: {
                    primary: "ui-icon-arrowreturnthick-1-w"
                }
            };
            mousetool.polygon();
            $("#show").button("disable");
            $("#onekeyinout").button("disable");
            $("#rule").button("disable");
        } else {
            options = {
                label: "设置",
                icons: {
                    primary: "ui-icon-wrench"
                }
            };
            if (overLayerObj == null) {
                
                showError("错误", "区域尚未绘制完成，双击或右击结束绘制。");
                return false;
            }
            removeTempBound();
            $("#show").button("enable");
            $("#onekeyinout").button("enable");
            $("#rule").button("enable");
            _abcMap.map.clearbind("plugin");
        }
        $(this).button("option", options);
    });

    //保存电子围栏
    $("#savePen").button({
        text: true,
        icons: {
            primary: "ui-icon-disk"
        }
    }).click(function (btn) {
        //定义多边形的参数选项
        if (overLayerObj != null && overLayerObj._type == "polygon") {//多边形
            boundPoints = overLayerObj.getPath();
            if (boundPoints == null || boundPoints.length < 3) {
                showError("错误！", "尚未设置限制区域"); return;
            }
            if (boundPoints.length >= 20) {
                showError("错误", "限制区域的顶点数不能超过20个");
                //maplet.removeOverlay(overLayerObj);
                return;
            }

            $("#bounddiv").dialog('open');
        } else {
            showMessage('提示', "尚未设置限制区域"); return;
        }

    });
    //放大地图
    $("#magnify").button({
        text: true,
        icons: {
            primary: "ui-icon-zoomin"
        }
    }).click(function () {
        _abcMap.map.zoomIn();

    });
    //缩小地图
    $("#dwindle").button({
        text: true,
        icons: {
            primary: "ui-icon-zoomout"
        }
    }).click(function () {
        _abcMap.map.zoomOut();
    });
    //一键缩放(界面上车的位置自适应)
    $("#onekeyinout").button({
        text: true,
        icons: {
            primary: "ui-icon-search"
        }
    }).click(function () {
        var allMarkers = _abcMap.map.getOverlaysByType("marker");

        allMarkers = $.grep(allMarkers, function (n, i) {
            return n.id.indexOf("car") == -1;
        }, true);
        if (allMarkers.length == 0) {
            _abcMap.map.setFitView();
        } else {
            _abcMap.map.setFitView(allMarkers);
        }
    });
    //测距
    $("#rule").button({
        text: true,
        icons: {
            primary: "ui-icon-minus"
        }
    }).click(function () {
        mousetool.rule();
    });
    $("#pen,#tool").buttonset();

}

function addFactoryInMap() {
    var fctopt = { lng: factoryLng, lat: factoryLat, iurl: iconArr.factIcon, clickEv: "", tipTxt: "", content: "", id: "factory_mark_s0" };
    _abcMap.addPoints([fctopt], true);
}
function resizeMapabc(w, h) {
    $("#dvMap").height(h + 10);
    $("#dvMap").width(w);
}
function returnCenter() {
    _abcMap.map.panTo(new AMap.LngLat(factoryLng, factoryLat));
}
function initMapContextMenu() {

    var content = $("<div></div>");

    var ct_ul = $("<ul class='mc_menu'></ul>");

    if (projectsetright) {

        $("<li onclick='updateMapProject()'> > 标定已有工地</li>").appendTo(ct_ul);
    }

    if (homemarksetright) {

        $("<li onclick='updateCompany()'> > 搅拌站标定</li>").appendTo(ct_ul);
    }


    if (hotmarksetright) {

    }


    //$("<li onclick='javascript:_abcMap.map.clearOverlaysByType(\"polyline\")'> > 清除所有地图线路</li>").appendTo(ct_ul); zjy
    $("<li onclick='javascript:_abcMap.map.remove(\"polyline\");mapCMenu.close();'> > 清除所有地图线路</li>").appendTo(ct_ul);


    ct_ul.appendTo(content);
    var contextMenuOption = {
        content: content.html()
        //, width: 200
        , isCustom: true
       // , className: "mc_menu"  zjy
    };

    mapCMenu = new AMap.ContextMenu(contextMenuOption);

    //_abcMap.map.bind(_abcMap.map, "rightclick", function (e) { zjy
    AMap.event.addListener(_abcMap.map, "rightclick", function (e) {

        if (isRightClick) {
            isRightClick = false;
            return;
        }
        _abcMap.eventLngLat = { lng: e.lnglat.lng, lat: e.lnglat.lat };
        mapCMenu.open(_abcMap.map, e.lnglat);
    });

}

//车辆的右键-站标定
function updateCompany(contextMenuItem, contextMenu, overlay) {

    if (isEmpty(_abcMap.eventLngLat.lng) || isEmpty(_abcMap.eventLngLat.lat)) {
        showMessage("提示", "未获取到定位信息，请重试！");
        return;
    }
    $("#markCompanyDiv").dialog({
        title: "搅拌站标定",
        width: 520,
        height: 330,
        modal: true,
        closeOnEscape: false,
        autoOpen: false,
        close: function (event, ui) {
            $('#markCompanyDiv>form')[0].reset();

            $(this).dialog("destroy");

            $("#container").append($("#markCompanyDiv"));


        },
        buttons: [{ text: '取消', click: CloseCompanyMarkFn }, { text: '保存', click: MarkCompany}]
    });
    $("#markCompanyDiv").dialog('open');
    $("#markCompanyDiv [name='Longtide']").val(_abcMap.eventLngLat.lng);
    $("#markCompanyDiv [name='Latitude']").val(_abcMap.eventLngLat.lat);

    $("#markCompanyDiv input[name='ID']").unbind('change');
    $("#markCompanyDiv input[name='ID']").bind('change', function () {
        var item = $("input[name='CName']").data("autocomplete").selectedItem;

        if (!isEmpty(item)) {
            var label = item.label;

            var str = "";
            var regx = new RegExp("<span.*?>.*?<\/span>", "img");
            var sregx = new RegExp("<span.*?>", "g");
            var eregx = new RegExp("<\/span>", "g");
            var result = label.match(regx).join("=").replace(sregx, "").replace(eregx, "").split("=");


            $("#markCompanyDiv [name='CompanyID']").val(item.value);
            $("#markCompanyDiv [name='CompName']").val(item.text);
            $("#markCompanyDiv [name='CompAddr']").val(result[1]);
            $("#markCompanyDiv [name='LinkMan']").val(result[2]);
            $("#markCompanyDiv [name='Tel']").val(result[3]);
            $("#markCompanyDiv [name='Principal']").val(result[4]);
        }
        else {
            $("#markCompanyDiv [name='CompanyID']").val("");
            $("#markCompanyDiv [name='CompName']").val("");
            $("#markCompanyDiv [name='CompAddr']").val("");
            $("#markCompanyDiv [name='LinkMan']").val("");
            $("#markCompanyDiv [name='Tel']").val("");
            $("#markCompanyDiv [name='Principal']").val("");
        }
    });


    mapCMenu.close();
}

function MarkCompany() {
    var Longitude = $("#markCompanyDiv [name='Longtide']").val();
    var Latitude = $("#markCompanyDiv [name='Latitude']").val();
    var PlaceRange = $("#markCompanyDiv [name='Range']").val();
    if (isEmpty(Longitude) || isEmpty(Latitude) || isEmpty(PlaceRange) || PlaceRange <= 0) {
        showError("经纬度或站范围格式不正确！");
        return;
    }

    ajaxRequest('/Company.mvc/UpdateCompanyForMap', $("#markCompanyDiv>form").serialize(), true, function (response) {
        if (response.Result) {
            $("#markCompanyDiv").dialog('close');
            companyinfo.lon = response.Data.Longtide;
            companyinfo.lat = response.Data.Latitude;
            companyinfo.range = response.Data.Range;
            _abcMap.removeMark("factory_mark_s0");
            factoryLng = companyinfo.lon;
            factoryLat = companyinfo.lat;
            addFactoryInMap();

        }
    });
}
function CloseCompanyMarkFn() {
    $("#markCompanyDiv").dialog("close");
}

//右击车辆图标调用的方法
function bindCarContextMenu(mkid) {


    //    var mk = _abcMap.map.getOverlays(mkid);zjy
    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == mkid) {
            mk = _mk[i];
            break;
        }
    }


    var content = $("<div></div>");
    var ct_ul = $("<ul class='mc_menu'></ul>");//zjy
    $("<li onclick=\"hideTheMker('" + mkid + "')\"> > 隐藏该车辆 </li>").appendTo(ct_ul);
    if (projectsetright) {
        //        if (home_soft_isAlone) {
        //            $("<li onclick=\"addMapProject('" + mkid + "')\"> > 此处增加新工地 </li>").appendTo(ct_ul);
        //        }
        $("<li onclick=\"updateMapProject('" + mkid + "')\"> > 标定已有工地</li>").appendTo(ct_ul);
    }
    ct_ul.appendTo(content);
    var contextMenuOption = {
        content: content.html()
       // , width: 200
        , isCustom: true
        //, className: "mc_menu" zjy
    };
    carC = new AMap.ContextMenu(contextMenuOption);

    //_abcMap.map.bind(mk, "rightclick", function (e) {zjy
    AMap.event.addListener(mk, "rightclick", function (e) {

        carC.open(_abcMap.map, mk.getPosition());
    });
}
function bindPrjContextMenu(mkid) {

    //    var mk = _abcMap.map.getOverlays(mkid);zjy
    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == mkid) {
            mk = _mk[i];
            break;
        }
    }

    
    var content = $("<div></div>");
    var ct_ul = $("<ul class='mc_menu'></ul>");//zjy


//    $("<li onclick=\"showProjRange('" + getProjIdByMarkerId(mk.id) + "')\"> > 显示工地范围 </li>").appendTo(ct_ul);
//    $("<li onclick=\"hideTheProjRange('" + getProjIdByMarkerId(mk.id) + "')\"> > 隐藏工地范围 </li>").appendTo(ct_ul);
//    $("<li onclick=\"showTheProjPath('" + getProjIdByMarkerId(mk.id) + "')\"> > 显示去工地路线 </li>").appendTo(ct_ul);
//    $("<li onclick=\"hideTheProjPath('" + getProjIdByMarkerId(mk.id) + "')\"> > 隐藏去工地路线 </li>").appendTo(ct_ul);

//    if (projrangesetright) {
//        $("<li onclick=\"editProjRange('" + getProjIdByMarkerId(mk.id) + "')\"> > 修改工地范围 </li>").appendTo(ct_ul);
//    }
    //    $("<li onclick=\"hideTheProj('" + getProjIdByMarkerId(mk.id) + "')\"> > 隐藏该工地 </li>").appendTo(ct_ul);  zjy
    $("<li onclick=\"showProjRange('" + getProjIdByMarkerId(mk.extData) + "')\"> > 显示工地范围 </li>").appendTo(ct_ul);
    $("<li onclick=\"hideTheProjRange('" + getProjIdByMarkerId(mk.extData) + "')\"> > 隐藏工地范围 </li>").appendTo(ct_ul);
    $("<li onclick=\"showTheProjPath('" + getProjIdByMarkerId(mk.extData) + "')\"> > 显示去工地路线 </li>").appendTo(ct_ul);
    $("<li onclick=\"hideTheProjPath('" + getProjIdByMarkerId(mk.extData) + "')\"> > 隐藏去工地路线 </li>").appendTo(ct_ul);

    if (projrangesetright) {
        $("<li onclick=\"editProjRange('" + getProjIdByMarkerId(mk.extData) + "')\"> > 修改工地范围 </li>").appendTo(ct_ul);
    }
    $("<li onclick=\"hideTheProj('" + getProjIdByMarkerId(mk.extData) + "')\"> > 隐藏该工地 </li>").appendTo(ct_ul);



    ct_ul.appendTo(content);
    var contextMenuOption = {
        content: content.html()
        //, width: 200 
        , isCustom: true
        //, className: "mc_menu" zjy
    };
    proC = new AMap.ContextMenu(contextMenuOption);//zjy

    //_abcMap.map.bind(mk, "rightclick", function (e) { zjy
    AMap.event.addListener(mk, "rightclick", function (e) {

        proC.open(_abcMap.map, mk.getPosition());
    });

}
function showProjRange(projId) {

    //    var mk = _abcMap.map.getOverlays(getMarkerIdByProjId(projId));zjy
    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == getMarkerIdByProjId(projId)) {
            mk = _mk[i];
            break;
        }
    }


    var treeObj = $.fn.zTree.getZTreeObj("treeDemo_Project");
    var node = treeObj.getNodeByParam("dbId", "p2_" + projId, null);
    var range = 500;
    if (node != null) {
        var oinfo = node.otherinfo;
        var leinfo = oinfo.substring(0, oinfo.indexOf(","));
        range = parseFloat(leinfo);
    }

    var cacheKey = "pjrange" + projId;
    var position = mk.getPosition();
    //加圆形
    _abcMap.addCircle(cacheKey, position.lng, position.lat, range);

    proC.close();
}
function hideTheMker(id) {
    _abcMap.removeMark(id);
    var _zTree = $.fn.zTree.getZTreeObj("treeDemo_Car");
    var _node = _zTree.getNodeByParam("dbId", 'c2_' + id.substr(3, id.length - 1), null);
    if (!isEmpty(_node)) {
        _zTree.checkNode(_node, null, false, zTreeOnCheck);
    }

    carC.close();
}
function hideTheProj(prjId) {
    _abcMap.removeMark(getMarkerIdByProjId(prjId));
    var _zTree = $.fn.zTree.getZTreeObj("treeDemo_Project");
    var _node = _zTree.getNodeByParam("dbId", 'p2_' + prjId, null);
    if (!isEmpty(_node)) {
        _zTree.checkNode(_node, null, false, zTreeOnCheck);
    }
    hideTheProjRange(prjId);
}
function hideTheProjRange(id) {
    var cacheKey2 = "pjrange" + id;
    _abcMap.removeMark(cacheKey2);

    proC.close();
}
var proj_path_ids = new Array();
function hideTheProjPath(id) {

    $.each(proj_path_ids, function (i, d) {
        if (d.indexOf("proj_path_" + id + "_") >= 0) {
            _abcMap.removeMark(d);
        }
    });


    proC.close();
}
function showTheProjPath(id) {
    DoAjax('/ProjectRoute.mvc/GetPathByPjid', { ProjectId: id }, function (data) { 
    //ajaxRequest('/ProjectRoute.mvc/GetPathByPjid', { ProjectId: id }, function (data){
        if (data != null && data.Result) {
            var colors = ['#32CD32', '#8B4513', '#DC143C'];
            $.each(data.Data, function (i, path) {
                var nodes = new Array();
                var pathArr = path.LonLatStr.split(';');
                //过滤空字符串的数组元素
                pathArr = $.grep(pathArr, function (n, i) {
                    return n == "";
                }, true);
                if (pathArr.length > 0) {
                    $.each(pathArr, function (i, node) {
                        var tmpNodeArr = node.split(",");
                        if (tmpNodeArr.length == 2) {//经纬度完整的才能算点
                            nodes.push({ lng: tmpNodeArr[0], lat: tmpNodeArr[1] });
                        }
                    });
                    //地图显示
                    _abcMap.addLine("proj_path_" + id + "_" + i, colors[i % 3], nodes);
                    proj_path_ids.push("proj_path_" + id + "_" + i);
                }
            });
        } else {
            showError(data.Message);
        }
    });


    proC.close();
}
function editProjRange(id) {
    $("#MProjId").val("");
    $("#MProjRange").val(""); $("#MProjRange").removeClass("TextBoxError2");

    if (!isEmpty(id)) {

        $("#MProjId").val(id);

        $("#projrangediv").dialog({
            title: "设置工地范围",
            width: 250,
            height: 150,
            modal: true,
            autoOpen: true,
            buttons: [{ text: '取消', click: function () { $(this).dialog("destroy"); } }, { text: '确定', click: setRangeComplete}]
        });
    }


    proC.close();
}

function setRangeComplete() {
    var range = $("#MProjRange").val();
    var projId = $("#MProjId").val();
    if (!isEmpty(range)) {
        /*清空页面的缓存和地图overlay*/
        var cacheKey = getMarkerIdByProjId(projId);
        _abcMap.removeMark(cacheKey);
        var cacheKey2 = "pjrange" + projId;
        _abcMap.removeMark(cacheKey2);
        //        $('#EditRange').ajaxSubmit({ success: cmpSuccessFn });
        ajaxRequest('/Project.mvc/UpdateProjRange', { ID: projId, PlaceRange: range }, false, function (response) {
            if (response.Result)
                cmpSuccessFn(response);
        });
    } else {
        $('#MProjRange').addClass("TextBoxError2");
    }
}
function cmpSuccessFn(data) {
    if (data.Result) {
        var msg = data.Message;

        var namerangestr = msg.substring(msg.indexOf("*(") + 2, msg.indexOf(")*"));
        var namerange = namerangestr.split(",");
        var name = namerange[0]; var range = namerange[1];
        var treeObj = $.fn.zTree.getZTreeObj("treeDemo_Project");
        //var node = treeObj.getNodeByParam("name", name, null);
        var node = treeObj.getNodeByParam("dbId", "p2_" + namerange[2], null);
        if (node != null) {
            var oinfo = node.otherinfo;
            var leinfo = oinfo.substring(oinfo.indexOf(","));

            node.otherinfo = range + leinfo;
            node.checked = true;
            treeObj.updateNode(node);

            var pjdata = doPrjData(node);
            _abcMap.addPoints([pjdata], false);

            var id = node.dbId.substring(3);
            var lonlat = node.lonlat.split(",");
            var cacheKey = "pjrange" + id;
            //加圆形
            _abcMap.addCircle(cacheKey, lonlat[0], lonlat[1], range);
        }
        //关闭对话框
        $('#projrangediv').dialog("destroy");
        showMessage("提示", "设置工地[" + name + "]范围=" + range + "成功");
    }
    else
        showError(data.Message);
}

function findHotMarkIconByStyle(markStyle) {
    var theicon = iconArr.trackIcon;
    if (markStyle == "bankMark") {
        theicon = iconArr.bankIcon;
    } else if (markStyle == "oilMark") {
        theicon = iconArr.gasStationIcon;
    } else if (markStyle == "foodMark") {
        theicon = iconArr.foodIcon;
    } else if (markStyle == "shopMark") {
        theicon = iconArr.shopIcon;
    } else if (markStyle == "bookMark") {
        theicon = iconArr.bookIcon;
    } else if (markStyle == "hotelMark") {
        theicon = iconArr.hotelIcon;
    } else if (markStyle == "hospitalMark") {
        theicon = iconArr.hospitalIcon;
    } else if (markStyle == "movieMark") {
        theicon = iconArr.movieIcon;
    } else if (markStyle == "stationMark") {
        theicon = iconArr.stationIcon;
    } else if (markStyle == "travMark") {
        theicon = iconArr.travIcon;
    }
    return theicon;
}
function addMapProject(mkid) {
    var ptLng = 0; var ptLat = 0;
    if (mkid == undefined || mkid == null) {
        if (!isEmpty(_abcMap.eventLngLat.lng) && !isEmpty(_abcMap.eventLngLat.lat)) {
            ptLat = _abcMap.eventLngLat.lat;
            ptLng = _abcMap.eventLngLat.lng;
        }
    } else {
        //        var mk = _abcMap.map.getOverlays(mkid);zjy
        var mk;
        var _mk = _abcMap.map.getAllOverlays();
        for (var i = 0; i < _mk.length; i++) {
            if (_mk[i].extData == mkid) {
                mk = _mk[i];
                break;
            }
        }


        if (mk != null && mk != undefined) {
            ptLat = _abcMap.eventLngLat.lat;
            ptLng = _abcMap.eventLngLat.lng;
        }
    }
    if (ptLat > 0 && ptLng > 0) {
        showPage('/Project/Create?lon=' + ptLng + "&lat=" + ptLat, "新增工地", 647, 350);

    } else {
        showMessage("提示", "未获取到定位信息，请重试！");
    }
}



function updateMapProject(mkid) {
    var ptLng = 0; var ptLat = 0;
    if (mkid == undefined || mkid == null) {
        if (!isEmpty(_abcMap.eventLngLat.lng) && !isEmpty(_abcMap.eventLngLat.lat)) {
            ptLat = _abcMap.eventLngLat.lat;
            ptLng = _abcMap.eventLngLat.lng;
        }
    } else {
        //        var mk = _abcMap.map.getOverlays(mkid);zjy
        var mk;
        var _mk = _abcMap.map.getAllOverlays();
        for (var i = 0; i < _mk.length; i++) {
            if (_mk[i].extData == mkid) {
                mk = _mk[i];
                break;
            }
        }




        if (mk != null && mk != undefined) {
            ptLat = mk.getPosition().lat;
            ptLng = mk.getPosition().lng;
        }
    }

    if (ptLat > 0 && ptLng > 0) {
        //        showPage('/Project/UpdateLonLat?cbFn=refreshzTree&lon=' + ptLng + "&lat=" + ptLat, "已有工地标定", 598, 200);
        $("#markProjectDiv").dialog({
            title: "标定工地",
            width: 520,
            height: 330,
            modal: true,
            closeOnEscape: false,
            autoOpen: false,
            close: function (event, ui) {
                $('#markProjectDiv>form')[0].reset();
                $(this).dialog("destroy");

                $("#container").append($("#markProjectDiv"));
            },
            buttons: [{ text: '取消', click: CloseProjectMarkFn }, { text: '保存', click: MarkProject}]
        });
        $("#markProjectDiv").dialog('open');
        $("#markProjectDiv [name='Longitude']").val(ptLng);
        $("#markProjectDiv [name='Latitude']").val(ptLat);

        $("#markProjectDiv input[name='ID']").unbind('change');
        $("#markProjectDiv input[name='ID']").bind('change', function () {
            var item = $("input[name='PName']").data($(this).val() + "_cache");
            if (!isEmpty(item)) {
                var label = item.label;

                var str = "";
                var regx = new RegExp("<span.*?>.*?<\/span>", "img");
                var sregx = new RegExp("<span.*?>", "g");
                var eregx = new RegExp("<\/span>", "g");
                var result = label.match(regx).join("=").replace(sregx, "").replace(eregx, "").split("=");


                $("#markProjectDiv [name='ProjectID']").val(item.value);
                $("#markProjectDiv [name='ProjectName']").val(item.text);
                $("#markProjectDiv [name='ProjectAddr']").val(result[1]);
                $("#markProjectDiv [name='LinkMan']").val(result[2]);
                $("#markProjectDiv [name='Tel']").val(result[3]);
                $("#markProjectDiv [name='Remark']").val(result[4]);
            }
            else {
                $("#markProjectDiv [name='ProjectID']").val("");
                $("#markProjectDiv [name='ProjectName']").val("");
                $("#markProjectDiv [name='ProjectAddr']").val("");
                $("#markProjectDiv [name='LinkMan']").val("");
                $("#markProjectDiv [name='Tel']").val("");
                $("#markProjectDiv [name='Remark']").val("");
            }
        });

    } else {
        showMessage("提示", "未获取到定位信息，请重试！");
    }

    if (mkid == undefined || mkid == null)
        mapCMenu.close();
    else
        carC.close();
}


function MarkProject() {
    var Longitude = $("#markProjectDiv [name='Longitude']").val();
    var Latitude = $("#markProjectDiv [name='Latitude']").val();
    var PlaceRange = $("#markProjectDiv [name='PlaceRange']").val();
    if (isEmpty(Longitude) || isEmpty(Latitude) || isEmpty(PlaceRange) || PlaceRange <= 0) {
        showError("经纬度或工地范围格式不正确！");
        return;
    }

    ajaxRequest('/Project.mvc/Update', $("#markProjectDiv>form").serialize(), true, function (response) {
        if (response.Result) {
            var pId = $("#markProjectDiv>form").find(":input[name='ID']").val();
            $("#markProjectDiv").dialog('close');

            var _zTree = $.fn.zTree.getZTreeObj("treeDemo_Project");
            var _node = _zTree.getNodeByParam("dbId", 'p2_' + pId, null);
            if (!isEmpty(_node) && !_node.checked) {
                _zTree.checkNode(_node, null, true, zTreeOnCheck);
            }

            refreshzTree();
        }
    });
}

function CloseProjectMarkFn() {
    $("#markProjectDiv").dialog("close");
}

//----------------------------------------------   ///地图相关 end


//----------------------------------------------   ///地图显示车辆 begin
function getCrAddrEv(mkid) {//mkid车辆id

    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == mkid) {
            mk = _mk[i];
            break;
        }
    }
    //var mk = _abcMap.map.getOverlays(mkid); zjy


    //_abcMap.map.bind(mk, "click", function (e) { zjy
    AMap.event.addListener(mk, "click", function (e) {

//        var rowid = mk.id.substring(3); //car86 86 zjy
        var rowid = mk.extData.substring(3);

        var data = $("#carinfojqgrd").jqGrid('getRowData', rowid);
        var lng = parseFloat(data.Longtidue);
        var lat = parseFloat(data.Latitude);
        var inforWindow = new AMap.InfoWindow({ content: carInfoWindow(data) });//加载html内容
        inforWindow.open(_abcMap.map, mk.getPosition());
        _abcMap.lastInfowindow = inforWindow;

    });

}
function carInfoWindow(data) {
    var titles = "<h3>车辆信息：" + data.CustomNo + "</h3>";
    var content =
    "更新时间:<bluefont>" + data["Sendtime"]
    + "</bluefont><br />任务信息:<bluefont>" + data["TaskName"]
    + "</bluefont><br />本车方量:<redfont>" + (isEmpty(data["TaskCube"]) ? 0 : data["TaskCube"]) + "方<nbsp />"
    + "</redfont>速度:<bluefont>" + data["Speed"] + "km/h<nbsp />"
    + "</bluefont>方向:<bluefont>" + data["Direction"]
    + "</bluefont><br />参考地址:<bluefont id='cankao_dizhi'>" + data["Addr"] + '</bluefont><br />';
    var count = 1;
    if (gSysConfig.AddAccFlagFn == true || gSysConfig.AddAccFlagFn == 'true') {
        if (count == 1) {
            content = content + "车辆状态:<bluefont>" + showAcc(data["AccFlag"]);
        } else {
            content = content + "</bluefont><nbsp />车辆状态:<bluefont>" + showAcc(data["AccFlag"]);
        }
        count = count + 1;
    }
    if (gSysConfig.AddOilFn == true || gSysConfig.AddOilFn == 'true') {
        if (count == 1) {
            content = content + "</bluefont>油量:<bluefont>" + data["Oil"];
        } else {
            content = content + "</bluefont><nbsp />油量:<bluefont>" + data["Oil"] + "L";
        }
        count = count + 1;
    }

    if ((gSysConfig.AddBeaterStatusFn == true || gSysConfig.AddBeaterStatusFn == 'true') && (data["CarType"] == 'CT1')) {
        content = content + "</bluefont><br />背罐状态:<bluefont>" + showBeater(data["BeaterStatus"]);
    }
    if (gSysConfig.AddDistanceFn == true || gSysConfig.AddDistanceFn == 'true') {
        content = content + "</bluefont><nbsp />GPS里程:<bluefont>" + data["Distance"] + "km";
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
    var btnstr = "<a href='javascript:void(0)' onclick='javascript:openCarInfoAccd(\"" + data.CustomNo + "\")'>查询当前状态</a> <a href='javascript:void(0)' onclick='javascript:openTrackAccd(" + data.Id + ")'>查询历史轨迹</a></a>";

    return "<div class='mapTips'>" + titles + content + btnstr + "</div>";
}
function openTrackAccd(id) {
    $("#carList").val(id);
    $("#trackPlayAccd").click();
}
function openCarInfoAccd(no) {
    $("#tb_s_CustomNo").val(no);
    refreshCarList(no, 1);
    innerLayout.open("south");
}
function setInfoAddr(s) {
    if (!isEmpty(s)) {
        $("#cankao_dizhi").html(s);
    }
}
//----------------------------------------------   ///地图显示车辆 end

//----------------------------------------------   ///地图显示工地 begin
var pjMarkClickEv = function (mkid) {

    //    var mk = _abcMap.map.getOverlays(mkid); zjy
    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == mkid) {
            mk = _mk[i];
            break;
        }
    }


    
    //_abcMap.map.bind(mk, "click", function (e) { zjy
    AMap.event.addListener(mk, "click", function (e) {

        //ajaxRequest('/Project.mvc/GetSingleProjInfo', { id: getProjIdByMarkerId(mk.id) }, false, function (data) {  zjy
        ajaxRequest('/Project.mvc/GetSingleProjInfo', { id: getProjIdByMarkerId(mk.extData) }, false, function (data) {



            var inforWindow = new AMap.InfoWindow({ content: projInfoTips(data) });
            inforWindow.open(_abcMap.map, mk.getPosition());
            _abcMap.lastInfowindow = inforWindow;

        });
    });
};
function getProjIdByMarkerId(mkid) {

    return mkid.substring(3);
}
function getMarkerIdByProjId(pjId) {
    return "prj" + pjId;
}
function projInfoTips(data) {
    var titles = "<h3>" + data.Name + "</h3>";
    var content = "联系人：<bluefont>" + data.LinkMan
                    + "</bluefont>&nbsp;&nbsp;&nbsp;&nbsp;联系电话：<bluefont>" + data.LinkTel
                    + "</bluefont><br />参考地址：<bluefont id='cankao_dizhi'>" + data.Address
                    + "</bluefont><br />";
    content = content + "今日完成：<redfont>" + data.DayCube + "方</redfont> 本月完成：<redfont>"
            + data.MonCube + "方</redfont><br />";
    if (data.carNos != null && data.carNos != "") {
        content = content + "工地用车：<redfont>" + data.carNos + "</redfont><br />";
    }
    var tsks = "<bluefont>";
    var sx = 0;
    $.each(data.TskVms, function (i, item) {
        tsks = tsks + (i + 1) + "." + item.TaskNo + "-" + item.ConsPos + "-[" + item.Grade + "]-计划<redfont>" + item.PlanCube + "</redfont>方-已供<redfont>" + item.ProvidedCube + "</redfont>方<br />";
        sx = i + 1;
    });
    tsks = tsks + "</bluefont>";
    if (sx > 0) tsks = "未完成任务单：<br />" + tsks;
    content = (content + tsks).replace(/null/g, '无');

    var btnstr = "";
    if (data.Address == null || data.Address == "null" || data.Address == "") {
        var lng = parseFloat(data.Lng);
        var lat = parseFloat(data.Lat);
        _abcMap.geocodeSearch(lng, lat, "setInfoAddr");
    }
    return "<div class='mapTips'>" + titles + content + btnstr + "</div>";
}
//----------------------------------------------   ///地图显示工地 end

function refreshPageList() {
    if ($('#carinfojqgrd').length == 0) {
        clearTimeout(settimeout_variable);
        return;
    }
    $("#tb_s_CustomNo").val("自编号");
    $("#tb_s_carNo").val("车号");
    refreshCarList();
    setTimeout("refreshPageList()", reftimes);
}

function refreshCarList(s, t) {
    if (s != null || typeof (s) != 'undefined' || s != 'undefined') {
        s = $("#tb_s_CustomNo").val();
        s = s.replace(/(^\s*)|(\s*$)/g, "");
    }
    if (s == "" || s == 'undefined') { s = ""; $("#tb_s_CustomNo").val("自编号"); }
    if (t == null || typeof (t) == 'undefined' || t == 'undefined') {
        $("#carinfojqgrd").jqGrid('setGridParam', { url: "/ABC.mvc/GetCarDataInfo?customNo=" + s, page: 1 }).trigger("reloadGrid");
    } else {
        $("#carinfojqgrd").jqGrid('setGridParam', { url: "/ABC.mvc/GetCarDataInfo?equalT=1&customNo=" + s, page: 1 }).trigger("reloadGrid");
    }
}

function initCarInfoDiv() {
    var table_height = $("div.info_div").height() - 22;
    var table_width = $("div.info_div").width() - 4;
    $('#carinfojqgrd').jqGrid({
        url: '/ABC.mvc/GetCarDataInfo',
        datatype: 'json',
        mtype: 'POST',
        colModel: [
                    { name: 'Id', index: 'Id', hidden: true, width: 30 }
                    , { name: 'TId', index: 'TId', hidden: true, width: 30 }
                    , { name: 'AlarmCount', index: 'AlarmCount', hidden: true, width: 30 }
                    , { label: '报警', name: 'IsAlarm', index: 'IsAlarm', formatter: showAlarm, width: 40, align: "center" }
                    , { label: "<div class='th_s_div'><input type='text' title='根据自编号搜索' id='tb_s_CustomNo' value=\"自编号\" onfocus=\"this.value=''\" onblur=\"refreshCarList(this.value)\" /></div>", name: 'CustomNo', index: 'CustomNo', width: 80, align: 'center' }
                    , { name: 'CarType', index: 'CarType', hidden: true }
                    , { name: 'Longtidue', index: 'Longtidue', width: 60, hidden: true }
                    , { name: 'Latitude', index: 'Latitude', width: 60, hidden: true }
                    , { label: '车辆状态', name: 'AccFlag', index: 'AccFlag', formatter: showAcc, unformat: dicCodeUnFmt, align: "center", width: 50 }
                    , { label: '罐状态', name: 'BeaterStatus', index: 'BeaterStatus', width: 50, formatter: showBeater, unformat: dicCodeUnFmt, align: "center" }
                    , { label: '作业状态', name: 'StatusName', index: 'StatusName', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'GpsStatus' }, width: 50, align: "center" }
                    , { label: '任务状态', name: 'TaskStatus', index: 'TaskStatus', formatter: dicCodeFmt, unformat: dicCodeUnFmt, formatoptions: { parentId: 'ShipStatus' }, width: 50, align: "center" }
                    , { label: '任务名称', name: 'TaskName', index: 'TaskName', width: 120 }
                    , { label: '本车方量', name: 'TaskCube', index: 'TaskCube', width: 60, align: "right" }
                    , { label: "最后更新时间", name: 'Sendtime', index: 'Sendtime', width: 120, align: 'right' }
                    , { name: 'SimNo', index: 'SimNo', width: 60, hidden: true }
                    , { name: 'Oil', index: 'Oil', width: 60, align: "right", hidden: true }
                    , { label: "速度", name: 'Speed', index: 'Speed', width: 40, align: "right" }
                    , { name: 'Height', index: 'Height', width: 40, align: "right", hidden: true }
                    , { label: "方向", name: 'Direction', index: 'Direction', width: 40, align: "center" }
                    , { name: 'Distance', index: 'Distance', width: 90, align: "right", hidden: disthidden }
                    , { label: '司机', name: 'Driver', index: 'Driver', width: 80 }
                    , { label: '司机电话', name: 'DriverTel', index: 'DriverTel', width: 100 }
                    , { label: '参考地址', name: 'Addr', width: 240 }
                    , { name: 'AlarmInfos', index: 'AlarmInfos', mapping: 'alarmInfostr', width: 60, hidden: true }
                    ],
        cmTemplate: { sortable: false },
        width: table_width,
        height: table_height,
        shrinkToFit: false,
        rowNum: 1000000,
        gridComplete: function () {
            //地理位置逆编码
            var geocoderOption = {
                range: 300, //范围
                crossnum: 0, //道路交叉口数
                roadnum: 1, //路线记录数
                poinum: 0//POI点数
            };



            var geocoder = new AMap.Geocoder(geocoderOption);


            var ids = $(this).getDataIDs();
            $.each(ids, function (i, n) {

                var row = jQuery('#carinfojqgrd').jqGrid('getRowData', n);

                //geocoder.regeocode(new AMap.LngLat(row.Longtidue, row.Latitude), function (data) { zjy
                geocoder.getAddress(new AMap.LngLat(row.Longtidue, row.Latitude), function (data) {


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

                        jQuery("#carinfojqgrd").jqGrid().setCell(n, 'Addr', addr);

                    }

                });
            });

            if (!isInitTree) {
                window.setTimeout(initzTree, 100);
                isInitTree = true;
                return;
            }

            //更新地图显示的点
            var checkedcidss = "";
            var ids = $("#carinfojqgrd").jqGrid('getDataIDs'); //车辆ids
            var _zTree1 = $.fn.zTree.getZTreeObj("treeDemo_Car");

            var cc_nodes = _zTree1.getNodesByFilter(carcheckedfilter2); // 查找节点集合

            var cc_nodes1 = _zTree1.getNodesByFilter(carcheckedfilter); // 查找节点集合
            var checkedcidss1 = "";

            $.each(cc_nodes, function (index, node) {
                checkedcidss = checkedcidss.concat("," + node.dbId.substring(3) + ",");
            });
            var mk4cars = [];
            $.each(ids, function (index, id) {
                if (checkedcidss.indexOf("," + id + ",") > -1) {
                    var d = doCarData(id);
                    if (d != null) {
                        mk4cars.push(d);
                    }
                }
            });
            if (mk4cars.length > 0) {
                _abcMap.addPoints(mk4cars, false);

            }
        },
        subGrid: true,
        subGridRowExpanded: function (subgrid_id, row_id) {
            var subgrid_table_id;
            subgrid_table_id = subgrid_id + "_t";
            var rowData = $("#carinfojqgrd").jqGrid('getRowData', row_id);
            var alarmCount = rowData["AlarmCount"];
            var tid = rowData["TId"];
            if (alarmCount != "0") {
                $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "'  cellpadding='0' cellspacing='0'></table>");
                $("#" + subgrid_table_id).jqGrid({
                    datatype: "local",
                    colNames: ['报警内容'],
                    colModel: [{ name: 'Content', index: 'Content', width: 600, sortable: false}],
                    height: "auto",
                    shrinkToFit: false,
                    width: 705
                });
                var jsondata = eval(rowData["AlarmInfos"]);
                for (var i = 0; i < jsondata.length; i++) {
                    jQuery("#" + subgrid_table_id).jqGrid('addRowData', i + 1, { Content: jsondata[i].alarmInfo });
                }
            }
        }
    });
}


//----------------------------------------------   ///初始化jqgrid end
function uplaodPageEvent() {
    $("#main").one("unload", function () {
        $(".pageDialog").each(function (i, item) { $(item).dialog("close"); });
        $.pnotify_remove_all();
        settimeout_variable = null;
    });
    settimeout_variable = setTimeout("refreshPageList()", reftimes);
}
function initPageElement() {


    var icons = { header: "ui-icon-circle-arrow-e", headerSelected: "ui-icon-circle-arrow-s" };
    $("#accordion").accordion({ fillSpace: true, icons: icons });
    $("#accordion").attr("display", "display");

    //图标说明
    $("#btns_div").html($("#iconTipsContains").html());


    $(".timebox").attr("readOnly", "readOnly");

    $("#trace").slider({
        orientation: "horizontal",
        range: "min",
        max: 1,
        value: 1,
        step: 1,
        slide: function (event, ui) {
            showTrack(ui.value);
        }
    });

    $("#speed").slider({
        orientation: "horizontal",
        range: "min",
        value: 800,
        min: 200,
        max: 1600,
        step: 100,
        slide: function (event, ui) {
            $("#thetimer").val(ui.value);
        }
    });

    $(".jq_btn_track").button();
    $(".jq_btn").button();
    $(".jq_btn_track").button("disable");
    $("#playerDiv").buttonset();
    $("#playerDiv>label").width(60);

    $("#playerDiv span").attr("style", "text-align:center");

    $("#playBtn").button("disable");
    $("#stopBtn").button("disable");
    $("#pauseBtn").button("disable");
}


/**车辆驾车导航相关 begin*/

var route_pjid, xys_points, routeType, routeMkIds, avoidName = {}, avoidRegion = {}, returnPts; distance = 0;
function routeInit() {
    /*表单初始化*/
    routeMkIds = new Array();
    returnPts = "";
    avoidName.value = "避让区";
    avoidRegion.value = "";

    routeType = getDom('routeType');

    var route_pjid = $("#projLngLatList").val();


    /*表单初始化结束*/

    $("#projLngLatList").bind("change", projLngLatSelectChanged);
}
function savePathToProj() {
    if (!isEmpty(route_pjid) && !isEmpty(distance) && !isEmpty(returnPts)) {
        DoAjax('/ProjectRoute.mvc/SavePath', { ProjectId: route_pjid, Distance: distance, LonLatStr: returnPts }, function (data) {
            if (data.Result) {
                showMessage('提示', data.Message);
            } else {
                showError('错误', data.Message);
            }
        });
    }
}
function clearRouteOverlays() {


    //    _abcMap.map.removeOverlays(routeMkIds);zjy
    var _mk = _abcMap.map.getAllOverlays();
    for (var j = 0; j < routeMkIds.length; j++) {
        for (var i = 0; i < _mk.length; i++) {
            if (_mk[i].extData == routeMkIds[j]) {
                _mk[i].setMap();
                break;
            }
        }
    }

}
function projLngLatSelectChanged() {
    var id_lnglat = $("#projLngLatList").val().split("$");
    route_pjid = parseFloat(id_lnglat[0]);
    //    xys_points.value = factoryLng + "," + factoryLat + ";" + id_lnglat[1];
}
function getDom(s) {
    return document.getElementById(s);
}
function region(s) {
    if (s.charAt(s.length - 1) == ':') {
        s = s.substring(0, s.length - 1);
    }
    return s.split(':');
}
//返回坐标数组
function dataEdit(s) {

    if (s.charAt(s.length - 1) == ';') {
        s = s.substring(0, s.length - 1);
    }
    var a = s.split(';');
    var b = [];
    for (var i = 0, l = a.length; i < l; i++) {
        var c = a[i].split(',');
        b.push(new AMap.LngLat(c[0], c[1]));
    }
    return b;
}
function search() {
    routeSearch();
}
var coor = new Array();

//展开结果列表
function opentable(routelineID, num) {
    _abcMap.map.setFitView();
    var showid = "routeline_id" + num;
    var showids = document.getElementById(showid);
    showids.style.display = (showids.style.display == "none" ? "block" : "none");
    if (showids.style.display == "block") {
        for (var i = 0; i < 3; i++) {
            var id = "routeline_id" + i;
            document.getElementById(id).style.display = "none";
        }
        var showid = "routeline_id" + num;
        document.getElementById(showid).style.display = "";
    }
}
function routeSearch() {
    coor = [];
    returnPts = "";
    var PjId = $("#projLngLatList").val();
    if (isEmpty(PjId)) {
        showMessage('提示', '没有导航终点');
        return;
    }
    var route = new AMap.RouteSearch({
        routeType: routeType.options[routeType.selectedIndex].value
        , avoidName: avoidName.value
        , avoidRegion: avoidRegion.value
    });

    route_pjid = PjId;
    var PrjLonLat = $.fn.zTree.getZTreeObj("treeDemo_Project").getNodeByParam("dbId", "p2_" + PjId, null).lonlat;
    xys_points = factoryLng + "," + factoryLat + ";" + PrjLonLat;
    route.getNaviPath(dataEdit(xys_points), function (data) {

        if (data.status == 'E0') {

//            _abcMap.map.removeOverlays(routeMkIds);zjy
            var _mk = _abcMap.map.getAllOverlays();
            for (var j = 0; j < routeMkIds.length; j++) {
                for (var i = 0; i < _mk.length; i++) {
                    if (_mk[i].extData == routeMkIds[j]) {
                        _mk[i].setMap();
                        break;
                    }
                }
            }


            var bounds = dataEdit(data.bounds);
            _abcMap.map.setBounds(new AMap.Bounds(bounds[0], bounds[1]));
            var s = '', coors = ''; distance = 0;



            for (var i = 0, l = data.count; i < l; i++) {
                s += "<div id='divid" + i + "' onmouseover='showLineById(" + i + ",this)' onmouseout='onmouseout_LineStyle(" + i + ",this)' onclick='onclickLine(" + i + ",this)' style=\"font-size: 12px;cursor:pointer;border-bottom:1px solid #C1FFC1;\"><table><tr><td>" + (i + 1) + "." + data.list[i].textInfo + "</td></tr></table></div>";
                coors += data.list[i].coor + ';';
                coor.push(data.list[i].coor);
                if (data.list[i].roadLength.indexOf("千米") >= 0) {
                    distance += parseFloat(data.list[i].roadLength.substr(0, data.list[i].roadLength.length - 2));
                }
                else {
                    distance += formatFloat(parseFloat(data.list[i].roadLength.substr(0, data.list[i].roadLength.length - 1)) / 1000, 3);
                }
            }


            document.getElementById('result').innerHTML = s;



//            _abcMap.map.addOverlays(new AMap.Polyline({
//                id: "rout_all",
//                path: dataEdit(coors),
//                strokeColor: "#0066CC",
//                strokeOpacity: 0.8,
//                strokeWeight: 6
            //            }));  zjy
            var _tmp1 = new AMap.Polyline({
                id: "rout_all",
                path: dataEdit(coors),
                strokeColor: "#0066CC",
                strokeOpacity: 0.8,
                strokeWeight: 6
            });
            _tmp1.extData = "rout_all";
            _tmp1.setMap(_abcMap.map);



            returnPts = coors;

            var a = dataEdit(xys_points);
            var start = a[0], end = a[a.length - 1];


//            _abcMap.map.addOverlays(new AMap.Marker({
//                id: 'rout_start',
//                icon: iconArr.startIcon,
//                offset: new AMap.Pixel(-8, -9),
//                draggable: false,
//                position: start
//            }));
//            _abcMap.map.addOverlays(new AMap.Marker({
//                id: 'rout_end',
//                icon: iconArr.endIcon,
//                offset: new AMap.Pixel(-8, -9),
//                draggable: false,
//                position: end
//            }));   zjy
           var _tmp2 =  new AMap.Marker({
                id: 'rout_start',
                icon: iconArr.startIcon,
                offset: new AMap.Pixel(-8, -9),
                draggable: false,
                position: start
            });
           var _tmp3 = new AMap.Marker({
                id: 'rout_end',
                icon: iconArr.endIcon,
                offset: new AMap.Pixel(-8, -9),
                draggable: false,
                position: end
            });
            _tmp2.extData = "rout_start";
            _tmp2.setMap(_abcMap.map);
            _tmp3.extData = "rout_end";
            _tmp3.setMap(_abcMap.map);




            routeMkIds.push("rout_start");
            routeMkIds.push("rout_all");
            routeMkIds.push("rout_end");
            var b = region(avoidRegion.value);
            if (b[0] != '') {
                for (var i = b.length - 1; i >= 0; i--) {


//                    _abcMap.map.addOverlays(new AMap.Polygon({
//                        id: 'pol' + i,
//                        path: dataEdit(b[i]),
//                        strokeColor: '#FF0000'
                    //                    }));  zjy

                    var _tmp4 = new AMap.Polygon({
                        id: 'pol' + i,
                        path: dataEdit(b[i]),
                        strokeColor: '#FF0000'
                    });
                    _tmp4.extData = 'pol' + i;
                    _tmp4.setMap(_abcMap.map);

                }
            }
        } else if (data.status == 'E1') {
            getDom('result').innerHTML = "未查找到任何结果!<br />建议：<br />1.请确保所有字词拼写正确。<br />2.尝试不同的关键字。<br />3.尝试更宽泛的关键字。";
        } else {
            if (data.error && data.error.code && data.error.description) {
                getDom('result').innerHTML = '错误信息：' + data.error.code + ',' + data.error.description;
            } else if (data.status) {
                getDom('result').innerHTML = "错误信息：" + data.status + "请对照API Server v2.0.0 简明提示码对照表查找错误类型";
            }
        }
    });


}
//鼠标移动到结果列表某项时的变化
var route_polyline;
function showLineById(i, thiss) {
    thiss.style.background = '#CAE1FF';
    route_polyline = new AMap.Polyline({
        id: "route_polyline" + i,
        path: dataEdit(coor[i]),
        strokeColor: "#00FFFF",
        strokeOpacity: 0.8,
        strokeWeight: 6
    });


    //    _abcMap.map.addOverlays(route_polyline); zjy
    route_polyline.extData = "route_polyline" + i;
    route_polyline.setMap(_abcMap.map);


}
function onmouseout_LineStyle(i, thiss) {
    thiss.style.background = "";


//    _abcMap.map.removeOverlays("route_polyline" + i);zjy
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == "route_polyline" + i) {
            _mk[i].setMap();
            break;
        }
    }


}
function onclickLine(i, thiss) {
    thiss.style.background = '#CAE1FF';
    _abcMap.map.setFitView([route_polyline]);
}

/**车辆驾车导航相关 end*/

/****越界报警设置 begin****/
function removeTempBound() {

    if (polygonEditor != null) polygonEditor.close();

    //console.dir(overLayerObj);


//    _abcMap.map.removeOverlays("curaddpoly"); //删除当前绘制的polygon类型覆盖物  zjy
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == "curaddpoly") {
            _mk[i].setMap();
            break;
        }
    }


    if (overLayerObj != null) {


    }
    overLayerObj = null; boundPoints = new Array();
}

function CloseBoundFn() {
    $("#bounddiv").dialog("close");
}

function SetBoundCmp() {
    //如果选择了第二个，需要对其变色
    if ($("#IsOutAlarm").val() == "0") {

        overLayerObj.setOptions(inpolygonOption);
    } else {
        overLayerObj.setOptions(outpolygonOption);
    }
    if (boundPoints == null) {
        showError("错误", "区域数据已清除或绘制的区域已保存，请重新绘制后再保存！");
    }

    var boundstrs = "";

    for (var i = 0; i < boundPoints.length; i++)
        boundstrs += boundPoints[i].toString() + ";";
    var bname = $("#boundName").val();
    var oJson = { isOutAlarm: $("#IsOutAlarm").val(), bounds: boundstrs, boundName: bname };

    ajaxRequest('/LimitRegion.mvc/SetBound', oJson, false, function (response) {

        sbcmpSuccessFn(response);

    });
}
function sbcmpSuccessFn(data) {

    if (data.Result) {

        CloseBoundFn();
        var options = {
            label: "设置",
            icons: {
                primary: "ui-icon-wrench"
            }
        };
        $("#setPen").button("option", options);
        $("#show").button("enable");
        $("#onekeyinout").button("enable");
        $("#rule").button("enable");
        showMessage('提示', data.Message);
        var rmsg = data.Message;
        if (polygonEditor != null) polygonEditor.close();
        overLayerObj = null;
        boundPoints = new Array();

    }
    else {
        showError("错误", data.Message);
    }
}
//显示
function ShowAllBoundInMap() {
    removeTempBound();
    ajaxRequest('/LimitRegion.mvc/GetBoundInfo', {}, false, function (response) {
        if (response.records > 0) {
            ShowAllBoundHandle(response.rows);
        }
    });
}

function ShowAllBoundHandle(datas) {

    $.each(datas, function (index, data) {
        //alert(data["ID"]);
        AddBoundOverlay(data["ID"], data);
    });
}

function AddBoundOverlay(id, data) {
    //如果存在对象，则刷新属性，设置显示
    var cacheKey = "bound" + id;
    var overlay = overlayCache[cacheKey];
    var dotstr = data["DotsStr"];
    if (isEmpty(dotstr) || dotstr == "") {
        if (overlay) {

            //            _abcMap.map.removeOverlays(cacheKey); zjy
            var _mk = _abcMap.map.getAllOverlays();
            for (var i = 0; i < _mk.length; i++) {
                if (_mk[i].extData == cacheKey) {
                    _mk[i].setMap();
                    break;
                }
            }




        }
        showMessage("提示", data["Name"] + "没有区域数据");
        return;
    }
    if (overlay) {
        //需先增加，再更新
        overlay.show();
        //_abcMap.map.addOverlays(overlay);
    }
    else {
        var dots = dotstr.split(';');
        var pots = new Array(); var x = 0;
        $.each(dots, function (index, dot) {
            if (dot != null && dot != "") {
                var lonlat = dot.split(",");
                if (lonlat.length == 2) {
                    pots.push(new AMap.LngLat(lonlat[0], lonlat[1]));
                }
            }
        });
        if (pots.length >= 3) {
            //如果不存在，则创建新对象
            //可以建立两种刷子，表示两种报警
            var polygonOption = outpolygonOption;
            if (!data["IsOutAlarm"]) {
                polygonOption = inpolygonOption;
            }

            polygonOption.id = cacheKey;
            polygonOption.path = pots;
            var newOverlay = new AMap.Polygon(polygonOption);



            //_abcMap.map.addOverlays(newOverlay); //加载多边形覆盖物 zjy
            newOverlay.extData = cacheKey;
            newOverlay.setMap(_abcMap.map);



            overlayCache[cacheKey] = newOverlay;
            var content = $("<div></div>");
            var ct_ul = $("<ul class='mc_menu'></ul>");//zjy
            $("<li onclick=\"hideLimitRegion('" + cacheKey + "')\"> > 暂时隐藏 </li>").appendTo(ct_ul);
            $("<li onclick=\"deleteLimitRegion('" + cacheKey + "','" + id + "')\"> > 删除此区域 </li>").appendTo(ct_ul);
            $("<li onclick=\"modifyLimitRegion('" + cacheKey + "','" + id + "'," + data["IsOutAlarm"] + ")\"> > 更改限制类型 </li>").appendTo(ct_ul);
            ct_ul.appendTo(content);
            var contextMenuOption = {
                content: content.html()
                , width: 200
                , isCustom: true
                //, className: "mc_menu" zjy
            };
            qprojMenu = new AMap.ContextMenu(contextMenuOption);

           // _abcMap.map.bind(newOverlay, "rightclick", func5 = function (e) { zjy
            AMap.event.addListener(newOverlay, "rightclick", func5 = function (e) { 


                isRightClick = true;
                qprojMenu.open(_abcMap.map, e.lnglat);

            });
        }
    }
}

//隐藏
function clearAllBound() {
    $.each(overlayCache, function (index, overlay) {
        if (index.indexOf("bound") == 0) {
            if (overlay) {
                //_abcMap.map.removeOverlays(overlay);
                overlay.hide();
            }
        }
    });
}

//单个隐藏
function hideLimitRegion(polygonid) {
    //_abcMap.map.getOverlays(polygonid).hide();zjy
    var mk;
    var _mk = _abcMap.map.getAllOverlays();
    for (var i = 0; i < _mk.length; i++) {
        if (_mk[i].extData == polygonid) {
            mk = _mk[i];
            break;
        }
    }
    mk.hide();

    qprojMenu.close();//zjy
}

//移除并永久删除
function deleteLimitRegion(polygonid, id) {
    ajaxRequest('/LimitRegion.mvc/Delete', { id: id }, true, function (response) {
        if (response.Result) {
            //            _abcMap.map.removeOverlays(_abcMap.map.getOverlays(polygonid));zjy
            var mk;
            var _mk = _abcMap.map.getAllOverlays();
            for (var i = 0; i < _mk.length; i++) {
                if (_mk[i].extData == polygonid) {
                    mk = _mk[i];
                    break;
                }
            }
            mk.setMap();
        }
    });

    qprojMenu.close(); //zjy
}

//更改限制类型
function modifyLimitRegion(polygonid, id, IsOutAlarm) {
    ajaxRequest('/LimitRegion.mvc/Update', { id: id, IsOutAlarm: !IsOutAlarm }, true, function (response) {
        if (response.Result) {
            //            var _polygon = _abcMap.map.getOverlays(polygonid);zjy
            var _polygon;
            var _mk = _abcMap.map.getAllOverlays();
            for (var i = 0; i < _mk.length; i++) {
                if (_mk[i].extData == polygonid) {
                    _polygon = _mk[i];
                    break;
                }
            }

            delete overlayCache[polygonid];

            ShowAllBoundInMap();
        }
    });

    qprojMenu.close(); //zjy
}
/****越界报警设置 end****/