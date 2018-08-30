/*
自动设置ReportViewer高度
*/
var _reportViewerId = 'ReportViewer1';
var ReportViewerLayout;
//当报表加载完成后，绑定报表事件 
//这个方法是在每次ReportViewer加载完成后自动调用的，主要是防止控件重新加载导致Dom结构发生变化
function pageLoad() {
    //初始化一些默认值
    ReportViewerLayout = {
        reportSpace: $(".reportspace"),
        baseHeight: $(".reportspace").height(),
        reportSpaceParent: $(".Area"),
        reportViewer: null,
        getReportDisabled: function () {
            if (!ReportViewerLayout.reportViewer) {
                return false;
            } else {
                return ReportViewerLayout.reportViewer.get_isLoading();
            }
        },
        needReset: true,
        needDefault: false,
        fixedTableDiv: function () {
            return $("[id$='fixedTable'] > tbody > tr:last >td:last >div");
        }
    };

    var newViewer = $find($("[id$='" + _reportViewerId + "']").attr("id"));
    if (newViewer != ReportViewerLayout.reportViewer) {
        ReportViewerLayout.reportViewer = $find($("[id$='" + _reportViewerId + "']").attr("id")); 
        //在报表状态变化时，重设报表布局 
        ReportViewerLayout.reportViewer.add_propertyChanged(reportViewerPropertyChanged);
    }
}
//报表属性更改时，触发相应方法

function reportViewerPropertyChanged(sender, e) {
    //window.document.title = ("get_isLoading:" + $find($("[id$='" + _reportViewerId + "']").attr("id")).get_isLoading());
    var propertyName = e.get_propertyName();
    if (propertyName === "isLoading" || propertyName === "reportAreaContentType") { 
        checkForReportLayout();
    }
} //检查是否需要重新设置布局
function checkForReportLayout() {
    var vrDisabled = ReportViewerLayout.getReportDisabled();
    var fixedTableDiv = ReportViewerLayout.fixedTableDiv();
    if (vrDisabled) { 
        //查询中，重置布局为默认，等待下一次设置 
        if (ReportViewerLayout.needDefault) { 
            defaultReportLayout(); 
            ReportViewerLayout.needDefault = false; 
        } 
        ReportViewerLayout.needReset = true;
    } else if (ReportViewerLayout.needReset && fixedTableDiv.height() > 0 && fixedTableDiv.height() < fixedTableDiv.attr("scrollHeight")) {
        setReportLayout();
        ReportViewerLayout.needReset = false;
        ReportViewerLayout.needDefault = true;
    }
} //重设报表布局为默认
function defaultReportLayout() {
    var fixedTable = $("[id$='fixedTable']");
    var fixedTableDiv = $("[id$='fixedTable'] > tbody > tr:last >td:last >div");
    fixedTableDiv.height(ReportViewerLayout.baseHeight);
    ReportViewerLayout.reportSpace.height(ReportViewerLayout.baseHeight);
    ReportViewerLayout.reportSpaceParent.height(ReportViewerLayout.baseHeight);
}
//根据报表高度设置报表布局
function setReportLayout() {
    var fixedTable = $("[id$='fixedTable']");
    var fixedTableDiv = ReportViewerLayout.fixedTableDiv();
    fixedTableDiv.css("overflow-y", "hidden");
    var h = fixedTableDiv.attr("scrollHeight") - 30; //预防有水平滚动条
    fixedTableDiv.height(h + 50);
    h = fixedTable.height();
    if (h > ReportViewerLayout.baseHeight) {
        ReportViewerLayout.reportSpace.height(h);
        ReportViewerLayout.reportSpaceParent.height(h);
        // this.parent.document.getElementById('reportFrame').style.height = h + 30 + 'px';
    } else {
      
        ReportViewerLayout.reportSpace.height(ReportViewerLayout.baseHeight);
        ReportViewerLayout.reportSpaceParent.height(ReportViewerLayout.baseHeight);
       
        //  this.parent.document.getElementById('reportFrame').style.height = ReportViewerLayout.baseHeight + 30 + 'px';
    }

}