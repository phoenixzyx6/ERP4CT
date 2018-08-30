var produceLines = [];
var CarConfig = {
    "005001": { cls: "car005001", detailID: "d_rest" },
    "005002": { cls: "car005002", detailID: "d_rest" },
    "005003": { cls: "car005003", detailID: "d_rest" },
    "005004": { cls: "car005004", detailID: "d_stand" },
    "005005": { cls: "car005005", detailID: "d_running" },
    "005006": { cls: "car005006", detailID: "d_running" },
    "005007": { cls: "car005007", detailID: "d_running" },
    "005008": { cls: "car005008", detailID: "d_pro" }
};
function initLeftPagejs() {
            //设置菜单事件
    $(".sd_menu").click(sd_menuClick);
//    //        //加载生产线和车辆
    loadProduceLineAndCar();

    $(".sd_allmenu").css({ width: "auto" });
   
    $(".content_div").bind("carChange", sd_reloadCar);
   
}
function sd_menuClick() {
    var main = $(this);
    var detail = main.next();
    if (detail.is(":visible"))
        main.find(".floatright").removeClass("sd_upIcon").addClass("sd_downIcon");
    else
        main.find(".floatright").removeClass("sd_downIcon").addClass("sd_upIcon");
    detail.slideToggle(100);
}
function loadProduceLineAndCar() {
    DoAjax('/ProductLine.mvc/GetProduceLinesByFactoryId', null, createProduceLines);
}
function initContextmenu() {
    $([{ id: "cm_rest", fn: restMenuClick },
            { id: "cm_stand", fn: standMenuClick },
            { id: "cm_running", fn: runningMenuClick },
            { id: "cm_input", fn: inputMenuClick}])
            .each(function (i, item) {
                $("#" + item.id)
                    .find(".sd_contextMenuItem")
                    .hover(function () { $(this).addClass("sd_MenuItemOver") }, function () { $(this).removeClass("sd_MenuItemOver") })
                    .click(function (event) {
                        $(this).parent().hide(100);
                        item.fn($(this).attr("menuId"), $(this).parent().parent());
                        //阻止触发父节点的事件
                        event.stopPropagation();
                        //return false; //阻止触发父节点的事件
                    });
            });
}

function addContextMenu() {
    initContextmenu();
    $("#d_rest").attr("refMenu", "cm_rest");
    $("#d_stand").attr("refMenu", "cm_stand");
    $("#d_running").attr("refMenu", "cm_running");
    $("div[id*=d_pro]").attr("refMenu", "cm_input");
    bindCarContextMenu($(".sd_detail .sd_car"));
}
function bindCarContextMenu(src) {
    src.mouseleave(hiddenMenu)
            .click(showMenu);
}
function hiddenMenu() { $(this).find(".sd_contextMenu").hide(100); }
function showMenu() {
    var src = $(this);
    var cxt = $("#" + src.parents().filter(".sd_detail:last").attr("refMenu"));
    src.append(cxt);
    cxt.show(100);
}

function restMenuClick(menuId, src) {
 
    var id = src.attr("carId");
    var state = "";
    switch (menuId) {
        case "rest":
            state = "005002"; break;
        case "modify":
            state = "005003"; break;
        case "stand":
            state = "005004"; break;
    }
    if (state == "") {
        showError("状态值异常，请重新登陆系统。");
        return;
    }
    if (state == '005004') {
        DoAjax('/Car.mvc/ShiftMixerCarStatus', {carId : id,operType :'Up',status:2}, function (data) {
            if (callbackSuccess(data)) {
                refreshSingleCar(id);
            }
        });
    }
    else {
        changeCarState('/Car.mvc/GPSChangeCarStatus', { id: id, code: state }, id);
    }
}
function changeCarState(url,postParam,id) {
    DoAjax(url, postParam, function (data) {
        if (callbackSuccess(data)) {
            refreshSingleCar(id);
        }
    });
}
function standMenuClick(menuId, src) {
   
    switch (menuId) {
        case "rest":
        case "modify":
            restMenuClick(menuId, src);
            return;
    }
    var id = src.attr("carId");
    if (menuId.substr(0, 2) == "pl") {
        showPage('/CarTask/CreateForShedule/' + id + "?productlineId=" + menuId.substr(2),
                                "发车信息", "500", "530");
    }
    else
        showError("状态值异常，请重新登陆系统。");
}
function runningMenuClick(menuId, src) {
    
    var id = src.attr("carId");
    switch (menuId) {
        case "back": //车辆回厂
            changeCarState('/ShippingDocument.mvc/CarReturn', { carId: id },id);
            return;
    }
    if (state == "") {
        showError("状态值异常，请重新登陆系统。");
        return;
    }
}
function inputMenuClick(menuId, src) {
    
    var id = src.attr("carId");
    switch (menuId) {
        case "run": //发车
            changeCarState('/Car.mvc/GPSChangeCarStatus', { id: id, code: '005005' }, id);
            return;
//        case "stand": //撤回到生产线
//            changeCarState('/ShippingDocument.mvc/CarReturn', { carId: id }, id);
//            return;
    }
    if (state == "") {
        showError("状态值异常，请重新登陆系统。");
        return;
    }
}
function callbackSuccess(data) {
    if (data.Result === true) {
        showMessage("提示",data.Message);
    } else {
        showError("错误",data.Message);
    }
    return data.Result === true;
}

function refreshSingleCar(carId) {
    DoAjax('/Schedule.mvc/GetAllCarAndStatus', { id: carId }, function (data) {
        allCars[carId] = data[0];
        $(".content_div").trigger("carChange", data[0]);
    });
}
function sd_reloadCar(src, item) {
   
    var obj = $("div.sd_car[carid='" + item.Id + "']");
    var html = getCarHtml(item);
    var parent = getCarParent(item);
    if (parent.attr("id") == obj.parent().attr("id")) {
        var config = CarConfig[item.StatusCode];
        obj.children("div.icon").removeAttr("class").addClass("icon " + config.cls);
    }
    else {
        obj.parent().append(obj.find(".sd_contextMenu"));
        obj.remove();
        var cur = $(html);
        parent.append(cur);
        bindCarContextMenu(cur);
        refreshCarCount();
    }
}

function createTaskSuccess(data, id) {
    if (callbackSuccess(data)) {
        closeDialog();
        refreshSingleCar(id);
    }
}
function createProduceLines(data) {
    produceLines = data;
    $(".sd_allmenu [name='sc_plMenu']").remove();
    
    $.each(data, function (i, item) {
        var menuHtml = '<div class="sd_menu" name="sc_plMenu">' +
                                            '<div class="left" />' +
                                            '<div class="center">' +
                                                '<div class=" floatright icon sd_downIcon" />' +
                                                '<div class="icon car005008 " />' +
                                                item.Name + '<span class="cnt"></span></div>' +
                                            '<div class="right" /></div>';
        var detail = '<div class="sd_detail ui-corner-all"  id=\"d_pro' + item.Id + '\"/>';
        var obj = $(menuHtml);
        $("#car005005").before(obj);
        obj.click(sd_menuClick);
        $("#car005005").before($(detail));

        //添加菜单项 --同步 关闭--
//        if (home_soft_isAlone) {
//            $("#cm_stand").append($('<div class="sd_contextMenuItem" menuid="pl' + item.Id + '"><div class="icon car005008" />进入' + item.Name + '</div>'));
//        }
    });
    loadCar();
}
function loadCar() {
    DoAjax('/Schedule.mvc/GetAllCarAndStatus', null, createCars);
}
function createCars(data) {
    var carInfo = {};
    $(".sd_allmenu .sd_detail").empty();
    $.each(data, function (i, item) {
        carInfo[item.Id] = item;
        var html = getCarHtml(item);
        getCarParent(item).append($(html));
    });
    allCars = carInfo;
   
    //统计数量
    refreshCarCount();
    //添加菜单点击事件
    addContextMenu();

    //显示所有菜单
    $(".sd_menu").click();
}
function getCarHtml(item) {
    
    var statusCode = item.StatusCode;
    var config = CarConfig[item.StatusCode];
    return '<div class="sd_car ui-corner-all" carId="' + item.Id + '"><div>' + item.CustomNo + '</div>';

}
function getCarParent(item) {
    var config = CarConfig[item.StatusCode];
    var detailId = config.detailID;
    //进料，在生产线上
    if (item.StatusCode == "005008")
        detailId += item.ProduceLineID;
    return $("#" + detailId);
}

function refreshCarCount() {
    $(".sd_menu").each(function (i, item) {
        var count = $(item).next().find(".sd_car").length;
        $(item).find(".cnt").text(" ( " + count + " )");
    });
}