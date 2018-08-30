function projectrouteIndexInit(opts) {
    var projectMap = new abcMap();        //绘制地图
    mapInit();
    var myJqGrid = new MyGrid({
        renderTo: 'projectrouteGrid'
        //, width: '100%'
            , autoWidth: true
            , buttons: buttons0
            , height: gGridHeight
		    , storeURL: opts.storeUrl
		    , sortByField: 'ID'
		    , primaryKey: 'ID'
            , groupField: 'ProjectName'
            , groupingView: { groupColumnShow: [false], groupText: ['<b>{0}(<font color=red>{1}</font>)</b>'], groupCollapse: false, groupOrder: ['desc'] }
		    , setGridPageSize: 30
		    , showPageBar: true
            , excelExport: false
		    , initArray: [
                { label: '线路编号', name: 'ID', index: 'ID' }
                , { label: '工程编号', name: 'ProjectId', index: 'ProjectId', hidden: true }
                , { label: '工程名称', name: 'ProjectName', index: 'ProjectName', width: 200 }
                , { label: '是否主线', name: 'IsPrimary', index: 'IsPrimary', formatter: booleanFmt, unformat: booleanUnFmt, width: 90, align: "center" }
                , { label: '频次', name: 'Times', index: 'Times', width: 110 }
		    ]
		    , autoLoad: true
            , functions: {
                handleReload: function (btn) {
                    myJqGrid.reloadGrid();
                },
                handleRefresh: function (btn) {
                    myJqGrid.refreshGrid('1=1');
                },
                handleAdd: function (btn) {
                    myJqGrid.handleAdd({
                        loadFrom: 'projectrouteDiv',
                        btn: btn
                    });
                },
                handleSetPrimary: function (btn) {
                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条路线进行标定");
                        return false;
                    }
                    var data = myJqGrid.getSelectedRecord();
                    ajaxRequest('/ProjectRoute.mvc/SetPrimary', { RouteId: data.ID, ProjectId: data.ProjectId }, false, function (response) {
                        if (response.Result) {
                            myJqGrid.refreshGrid();
                            showMessage("提示", response.Message);
                            return false;
                        } else {
                            showMessage("出错了", response.Message);
                            return false;
                        }
                    });
                }
                , handleDelete: function (btn) {
                    myJqGrid.deleteRecord({
                        deleteUrl: btn.data.Url
                    });
                }
                , handlePlay: function (btn) {

                    if (!myJqGrid.isSelectedOnlyOne()) {
                        showMessage("提示", "请选择一条路线进行播放");
                        return false;
                    }

                    var data = myJqGrid.getSelectedRecord();
                    ajaxRequest('/ProjectRoute.mvc/Play', { RouteId: data.ID }, false, function (response) {
                        if (response.Result === false) {
                            showMessage("提示", response.Message);
                        } else {
                            var _data = response.Data;
                            var _lonlatstrArr = _data.LonLatStr.split(";");

                            thePoints = $.grep(_lonlatstrArr, function (n, i) {
                                return n == "";
                            }, true);
                            var distance = _data.Distance;
                            if (!isRun) {
                                aryPts = new Array();
                                $.each(thePoints, function (index, point) {
                                    var tmpArr = point.split(",");
                                    if (tmpArr.length == 2) {
                                        linePts[index] = new MMap.LngLat(parseFloat(tmpArr[0]), parseFloat(tmpArr[1]));
                                        aryPts[index] = { lng: parseFloat(tmpArr[0]), lat: parseFloat(tmpArr[1]) };
                                        dotMarks[index] = new MMap.Marker({
                                            id: "track_mark_" + index,
                                            position: new MMap.LngLat(parseFloat(tmpArr[0]), parseFloat(tmpArr[1])),
                                            icon: iconArr.circleDot,
                                            offset: new MMap.Pixel(-12, -11)
                                        });
                                        mkidArr.push("track_mark_" + index);
                                    }

                                });

                                //开始播放
                                if (timer1) window.clearInterval(timer1);
                                counter = 0;
                                $.each(mkidArr, function (i, id) {
                                    projectMap.removeMark(id);
                                });
                                mkidArr = new Array();
                                projectMap.removeMark("track_mark_s0");
                                projectMap.map.addOverlays(trackMark);

                                timer1 = window.setInterval(function () {


                                    if (dotMarks.length > 1) {
                                        addLine(counter);

                                        var lnglat = dotMarks[counter].getPosition();
                                        var bounds = projectMap.map.getBounds();
                                        if (lnglat.lng <= bounds.southwest.lng || lnglat.lng >= bounds.northeast.lng || lnglat.lat <= bounds.southwest.lat || lnglat.lat >= bounds.northeast.lat) {
                                            projectMap.map.panTo(lnglat);

                                        }
                                        trackMark.setPosition(aryPts[counter + 1]);

                                        //projectMap.map.addOverlays(dotMarks[counter]); //加载覆盖物

                                        counter++;
                                    }
                                    if (counter >= aryPts.length - 1) {
                                        if (timer1) window.clearInterval(timer1);
                                        projectMap.map.addOverlays(dotMarks[counter]);
                                        isRun = false;
                                        showAlert("友情提示", "路线播放完毕，大约" + formatFloat(parseFloat(distance), 2) + "公里");
                                    }
                                }, 100);
                                isRun = true;
                            } else {
                                showMessage("提示", "在播放状态下无法进行再次播放");
                                return false;
                            }
                        }
                    });
                }
            }
    });

    initTrack(); ////初始化轨迹回放工具
    function mapInit() {
        $("#projectMap").height($("#container").height());
        $("#projectMap").width($("#container").width() - $("#projectrouteGrid").width());
        var opts = {
            render: "projectMap",
            cLng: companyinfo.lon,
            cLat: companyinfo.lat,
            level: 16
        };
        projectMap.init(opts);
        var fctopt = { lng: companyinfo.lon, lat: companyinfo.lat, iurl: iconArr.factIcon, clickEv: "", tipTxt: "", content: "", id: "factory_mark_s0" };
        projectMap.addPoints([fctopt], true);
        //MarkHome();
        //maplet.addControl(new MStandardControl());
        //设置信息窗口的大小
        //maplet.setIwStdSize(240, 180);

        //设置当鼠标点击地图时，不将其自动设置为中心点
        //maplet.clickToCenter = false;
        //maplet.showOverview(false, false);
    }

    function addLine(i) {
        var color = "#32CD32";

        projectMap.addLine("polyline_" + i, color, [aryPts[i], aryPts[i + 1]]);
        mkidArr.push("polyline_" + i);
    }
}