var sd_RefreshTaskData = true; var intervalHandle; var allCars = null;

function initLayout() {
    $("#main_content").height(gGridHeight + 88);
  
    outerLayout = $('div.content_div').layout({
        center__paneSelector: ".center_div"
		    , west__paneSelector: ".west_div"
		    , west__size: 220
            , spacing_open: 6  // ALL panes
		    , spacing_closed: 20 // ALL panes
            , resizable: false
            , togglerTip_open: "关闭"//pane打开时，当鼠标移动到边框上按钮上，显示的提示语  
            , togglerTip_closed: "打开"//pane关闭时，当鼠标移动到边框上按钮上，显示的提示语 
            , center__onresize: function (pane, $Pane) {
                //reloadMap($Pane.innerWidth() - 2, $("#dvMap").height());
            }
    });

}
function initPagejs() {
    initLayout();
    

    $(".center_div").load('/Schedule.mvc/Center', { id: _facId });
    $(".west_div").load('/Schedule.mvc/Left', { id: _facId });
    intervalHandle = window.setInterval(sd_refreshAllCar, intervalSpan);
    
    function sd_refreshAllCar() {

        DoAjax('/Schedule.mvc/GetAllCarAndStatus', null, function (data) {
            if ($('.west_div').length <= 0) {
                return;
            }
            sd_RefreshTaskData = false;
            $.each(data, function (i, item) {
                allCars[item.Id] = item;
                $(".content_div").trigger("carChange", item);
            });
            sd_RefreshTaskData = true;

            refreshTask();
        });
    }
    //当页面卸载时触发的事件
    $("#unloaddiv").one("click", function () {
        if (intervalHandle)
            window.clearInterval(intervalHandle);
    });
}

function refreshAlarmGrid() {
    if ($('#schedule_south_div').length == 0) {
        if (AlarmRefreshTimer) {
            clearInterval(AlarmRefreshTimer);
        }
    }
    else {
        alarmGrid.reloadGrid();
    }
}
