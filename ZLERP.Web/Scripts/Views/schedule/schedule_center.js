var tooltip;
var pj_showDetail = false;
var pj_expand = false;
var col_num = 2;
function initTooltip() {
    tooltip = $.pnotify({
        pnotify_title: "详细信息",
        pnotify_hide: false,
        pnotify_closer: true,
        pnotify_history: false,
        pnotify_insert_brs: false,
        pnotify_animate_speed: 100,
        pnotify_opacity: .9,
        pnotify_width: "300px",
        pnotify_notice_icon: "ui-icon ui-icon-comment",
        pnotify_stack: false,
        pnotify_before_open: function (pnotify) {
            // This prevents the notice from displaying when it's created.
            pnotify.pnotify({
                pnotify_before_open: null
            });
            return false;
        }
    });
}
function pjcarClick(event) {
    var carId = $(this).attr("carid");
    tooltip.pnotify({ pnotify_text: $("#tooltipTmp").tmpl(allCars[carId]).html() });
    tooltip.pnotify_display();
    if ((event.clientX + 312) <= $('#container').width()) {
        tooltip.css({ 'top': event.clientY + 10, 'left': event.clientX });
    }
    else {
        tooltip.css({ 'top': event.clientY + 10, 'left': event.clientX - 300 });
    }
    return false;
}


function changeLayout(v) {
    var cwdth = $(".container-fluid").width();
    if (v == 1) {
        $(".mediumListIconTextItem").width((cwdth - 85));
        col_num = 1;
    } else if (v == 2) {
        var setW = parseInt((cwdth - 75) / 2);
        if (setW < 480) {
            showMessage("提示", "屏幕空间不足显示成双列");
        } else {
            $(".mediumListIconTextItem").width(setW);
            col_num = 2;
        }
    } else {
        var setW = parseInt((cwdth - 95) / 3);
        if (setW < 480) {
            showMessage("提示", "屏幕空间不足显示成三列");
        } else {
            $(".mediumListIconTextItem").width(setW);
            col_num = 3;
        }
    }
}



function loadCarData() {
    
    if (allCars == null) {
        window.setTimeout(loadCarData, 10);
        return;
    }
 
    refreshTask();
}
function refreshTask() {

    return;
    DoAjax('/ProduceTask.mvc/GetAllUncompletedTasks', null, function (data) {
        $(".pj").empty();

        var html = '';
        $.each(data, function (i, item) {
            if (col_num == 1)
                html = ' <div class="mediumListIconTextItem task" tkid="' + item.ID + '" style="float:left;width:94%;padding:5px">';
            else if (col_num == 2)
                html = ' <div class="mediumListIconTextItem task" tkid="' + item.ID + '" style="float:left;width:46%;padding:5px">';
            else
                html = ' <div class="mediumListIconTextItem task" tkid="' + item.ID + '" style="float:left;width:33%;padding:5px">';
            html += '<div class="taskDes">' + item.ID + ' -- ' + item.ProjectName + ' -- ' + item.ConsPos + ' --  [' + item.ConStrength + ']-- ' + item.CastMode + ' -- ' + FixedTwoNumber(item.Distance) + ' km</div>';
            if (!isEmpty(item.Distance)) {
                html += '<span  style="display:none"><text>距离:<span class="pj_distance">' + FixedTwoNumber(item.Distance) + '</span>km</text></span>';
            }
            else {
                html += '<span  style="display:none"><text>距离:未知</text></span>';
            }
            html += '<div class="produceLine" title = "生产线装料"></div> <div class="factoryIcon" title = "搅拌站"></div>';
            html += '<div class ="CubeInfo" title = "方量累计"><span class="tk_PlanCube" title="计划方量"></span>方<br/>';
            html += '<span class="tk_ProvidedCube" title="已完成方量"></span>方<br/><span class="tk_Times" title="已完成车次"></span>次</div>';
            html += '<div class="atBuilding" title = "工地卸料"></div><div class="buildingIcon" title = "工地"></div>';
            html += '<div class="path"><div class="go" title = "路上"></div><div class="info"></div><div class="back" title = "返回中"></div> </div></div>';
            $(".pj").append(html);
            var task = $(".task[tkid='" + item.ID + "']");
            task.find(".tk_ProvidedCube").text(item.ProvidedCube);
            task.find(".tk_PlanCube").text(item.PlanCube);
            task.find(".tk_Times").text(item.ProvidedTimes);
            task.addClass("uncompleted");

            if (i == data.length - 1) {         //加载完全部的任务单后执行

                $.each(allCars, function (i, item) {
                    if (item) {
                        pjcarChange(this, item);
                    }
                });
            }
        }); 
    });
    
}
function pjcarChange(src, item) {
   
    var cars = $(".pj .pjcar[carid='" + item.Id + "']");
    //运行中和生产中的状态
    var arrStatus = { "005005": "go", "005006": "atBuilding", "005007": "back", "005008": "produceLine" };
    var clsName = arrStatus[item.StatusCode];

    if (clsName) {
      
        //车辆在地图上显示
        if (cars.length > 0) {
            //更改图标
            
            //cars.find(".icon").removeAttr("class").addClass("icon car" + item.StatusCode);
            //车辆显示位置改变
            var car = $(".task .pjcar[carid='" + item.Id + "']");
            if (!car.parent().is("." + clsName) || $(".task[tkid='" + item.TaskID + "'] .pjcar[carid='" + item.Id + "']").length == 0) {
                $(".pj  .task[tkid='" + item.TaskID + "']  ." + clsName).append(car);
            }
 
        }
        //车辆不在地图上显示
        else {
            var html = '<div class="pjcar" carid="' + item.Id + '" ><span>' + item.CustomNo + '</span></div>';
//            $(".pj .main[pjid=" + item.ProjectID + "] .cars").append($(html).click(pjcarClick));
            $(".pj  .task[tkid='" + item.TaskID + "']  ." + clsName).append($(html).click(pjcarClick));
            cars = $(".pj .pjcar[carid='" + item.Id + "']");
        }
        var car = $(".task .pjcar[carid='" + item.Id + "']");
        //更新车辆的路线的位置
        if (clsName == "go" || clsName == "back") {
            //debugger;

            var pjDistance = parseFloat($(".pj .task[tkid='" + item.TaskID + "'] .pj_distance").text());
            
            var carDistance = 0;
            if (clsName == "go") carDistance = parseFloat(item["ProjectDistance"]);
            if (clsName == "back") carDistance = pjDistance - parseFloat(item["FactoryDistance"]);

            if (!isNaN(pjDistance) && !isNaN(carDistance)) {
            
                var position = carDistance / pjDistance;
                if (position > 0.95) position = 0.95;
                if (position < 0.05) position = 0.05;
                position = (position * 100) + "%";
                car.css({ right: position });
            }
            else {
                
                car.removeAttr("style");
            }
        }
        //更新车辆状态,停车,gps无效
        //停车

        if (isShowGps) {
            if (item.IsGpsError === true)
                cars.addClass("gpsInvalid");        
            else
                cars.removeClass("gpsInvalid");
             
        }
        if (isShowGprs) {
            if (item.IsGprsError === true)
                cars.addClass("gprsInvalid");
            else
                cars.removeClass("gprsInvalid");
        }

        if (isBeaterStatus) {
            if (item.BeaterStatus == "2")
                cars.addClass("unload");
            else
                cars.removeClass("unload");
        }

        if (item.AccFlag === "0")
            cars.addClass("carStop");
        else
            cars.removeClass("carStop");


     
    }
    //在休养或待命中
    else {
        cars.remove();
    }
  
}
function initCenterPagejs() {
    initTooltip();

    $(".pj .main .factoryIcon, .pj .main .info, .pj .main .dtIcon").click(function () {
        var cur = $(this).parent();
        if (cur.next().is(":visible"))
            cur.find(".dtIcon").removeClass("sd_upIcon").addClass("sd_downIcon");
        else
            cur.find(".dtIcon").removeClass("sd_downIcon").addClass("sd_upIcon");
        cur.next().slideToggle(100);
    });
     
    //tooltip
    $(".pjcar").click(pjcarClick);
   
    $(".content_div").bind("carChange", pjcarChange);
    $(".content_div").click(function () { tooltip.pnotify_remove(); });
    window.setTimeout(loadCarData, 10);
    $("#unloaddiv").one("click", function () {
        tooltip.pnotify_remove();
    });
    
}