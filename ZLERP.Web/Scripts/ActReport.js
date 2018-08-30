/*
ActReport获取方法

使用示例
//页面加入：
<object id="Report" classid="clsid:BE05092E-8D70-48F7-8341-1C707C3A3535" width="0" height="0" >
</object> 
       
//打印发货单
printShippingDoc(type, shipDocId);
type: 打印类型，可使用'print':直接打印， 'preview':打印预览, 'design':打印设计

示例：打印预览发货单号为201203020003的发货单
printShippingDoc('preview', '201203020003')


 
//JS调用：
var ActReport = getActReport("Report");
if (ActReport) {
    ActReport.loadReport(reportTemplate);
    ActReport.DicData = data;
    ActReport.WebPrint();
}
*/
//是否缓存报表模板
var _cacheTemplate = false;
function OnPrintCompleted() {
    if (printCompleteDelegate) {
        printCompleteDelegate();
        printCompleteDelegate = null;        
    }
    else if (commonWindow) {
        commonWindow.hide();
    }
}
var actReportInstallPath = '/Content/Files/ActReport.exe';
function getActReport(objectId) {
    var ActReport = document.getElementById(objectId);
    if ((ActReport == null) || (typeof (ActReport.loadReport) == "undefined")) {
        if (confirm('ActReport控件没有安装，是否要现在下载打印控件?')) {
            window.location.href = actReportInstallPath;
        }
        return null;
    }
    else {
        ActReport.UploadUrl = '/Home.mvc/UploadReport?uploadPath=Report&r=1';
        return ActReport;
    }
}
///打印发货单
function printShippingDoc(type, shipDocId) {
    $.post(
        '/ShippingDocument.mvc/Get',
         { id: shipDocId },
         function (result) {
             if (result && result.Result) {
                 var data = result.Data;
                 //以下为取的任务单中字段来补充发货单信息 xyl
                 var taskid = data.TaskID;
                 $.post(
                    '/ProduceTask.mvc/Get',
                    { id: taskid },
                    function (resp) {
                        if (resp && resp.Result) {
                            resultData = resp.Data; 
                            data['BetonTag'] = resultData.BetonTag; //砼标记
                            data['OtherDemand'] = resultData.OtherDemand; //其他要求
                            data['CarpGrade'] = resultData.CarpGrade; //级配
                            $.post(
                                '/SysConfig.mvc/GetSysConfig',
                                { configName: "EnterpriseName" },
                                function (resp) {
                                    if (resp && resp.Result) {
                                        resultData = resp.Data;
                                        data['EnterpriseName'] = resultData.ConfigValue; //企业名称
                                        processShipDocPrint(type, data);
                                    }
                                }
                             );

                        }
                    }
                 );

             }
         }
    );                                         // end Ajax request
}
//原材料打印入库单
function printStuffinDoc(type, shipDocId) {
    $.post('/StuffIn.mvc/Get',{ id: shipDocId },
         function (result) {
             if (result && result.Result) {
                 var data = result.Data;
                 var newData = {};
                 if (!$.isEmptyObject(data)) {
                     for (k in data) {
                         if (StuffInProperties.hasOwnProperty(k)) {
                             if (typeof (data[k]) == "string" && data[k].indexOf('Date(') > 0) {
                                 newData[StuffInProperties[k]] = dataTimeFormat(data[k]);
                             }
                             else {
                                 newData[StuffInProperties[k]] = data[k];
                             }
                         }
                     }
                     jsonStr = JSON.stringify(newData);

                     if (type == 'print') {//打印
                         WebPrint_S(jsonStr);

                     } else if (type == 'preview') {//预览

                         WebShowPrint_S(jsonStr);

                     }
                     else if (type == 'design') {//设计
                         WebDesign_S(jsonStr);
                     }
                 }
                 else {
                     alert("通讯失败！请联系管理员！");
                 }
             }
         }
    );                     // end Ajax request
}

//打印物资入库单
function printGoodsInDoc(type, goodsInId) {
    $.post(
        '/GoodsIn.mvc/Get',
         { id: goodsInId },
         function (result) {
             if (result && result.Result) {
                 var data = result.Data;
                 var newData = {};
                 if (!$.isEmptyObject(data)) {
                     $.post(
                                '/SysConfig.mvc/GetSysConfig',
                                { configName: "EnterpriseName" },
                                function (resp) {
                                    if (resp && resp.Result) {
                                        resultData = resp.Data;
                                        data['EnterpriseName'] = resultData.ConfigValue; //企业名称

                                        for (k in data) {
                                            if (GoodsInProperties.hasOwnProperty(k)) {
                                                if (typeof (data[k]) == "string" && data[k].indexOf('Date(') > 0) {
                                                    newData[GoodsInProperties[k]] = dataTimeFormat(data[k]);
                                                }
                                                else {
                                                    newData[GoodsInProperties[k]] = data[k];
                                                }
                                            }
                                        }

                                        jsonStr = JSON.stringify(newData);
                                       
                                        if (type == 'print') {
                                            WebPrint_GoodsIn(jsonStr);

                                        } else if (type == 'preview') {

                                            WebShowPrint_GoodsIn(jsonStr);

                                        }
                                        else if (type == 'design') {
                                            WebDesign_GoodsIn(jsonStr);
                                        }
                                    }
                                }
                     );
                     
                 }
                 else {
                     alert("通讯失败！请联系管理员！");
                 }
             }
         }
    );                      // end Ajax request
}

//打印物资领用单
function printGoodsOutDoc(type, goodsOutId) {
    $.post(
        '/GoodsOut.mvc/Get',
         { id: goodsOutId },
         function (result) {
             if (result && result.Result) {
                 var data = result.Data;
                 var newData = {};
                 if (!$.isEmptyObject(data)) {

                     for (k in data) {
                         if (GoodsOutProperties.hasOwnProperty(k)) {
                             if (typeof (data[k]) == "string" && data[k].indexOf('Date(') > 0) {
                                 newData[GoodsOutProperties[k]] = dataTimeFormat(data[k]);
                             }
                             else {
                                 newData[GoodsOutProperties[k]] = data[k];
                             }
                         }
                     }

                     jsonStr = JSON.stringify(newData);
                     
                     if (type == 'print') {
                         WebPrint_GoodsOut(jsonStr);

                     } else if (type == 'preview') {

                         WebShowPrint_GoodsOut(jsonStr);

                     }
                     else if (type == 'design') {
                         WebDesign_GoodsOut(jsonStr);
                     }
                 }
                 else {
                     alert("通讯失败！请联系管理员！");
                 }
             }
         }
    );                      // end Ajax request
}

//打印物资借用单
function printGoodsBorrowDoc(type, goodsBorrowId) {
    $.post(
        '/GoodsBorrow.mvc/Get',
         { id: goodsBorrowId },
         function (result) {
             if (result && result.Result) {
                 var data = result.Data;
                 var newData = {};
                 if (!$.isEmptyObject(data)) {
                     for (k in data) {
                         if (GoodsBorrowProperties.hasOwnProperty(k)) {
                             if (typeof (data[k]) == "string" && data[k].indexOf('Date(') > 0) {
                                 newData[GoodsBorrowProperties[k]] = dataTimeFormat(data[k]);
                             }
                             else {
                                 newData[GoodsBorrowProperties[k]] = data[k];
                             }
                         }
                     }
                     jsonStr = JSON.stringify(newData);

                     if (type == 'print') {
                         WebPrint_GoodsBorrow(jsonStr);

                     } else if (type == 'preview') {

                         WebShowPrint_GoodsBorrow(jsonStr);

                     }
                     else if (type == 'design') {
                         WebDesign_GoodsBorrow(jsonStr);
                     }
                 }
                 else {
                     alert("通讯失败！请联系管理员！");
                 }
             }
         }
    );                     // end Ajax request
}

function shippingDocDesign() {
    var data = '{"发货单号":"201203080005","任务号":"RWD00000025","合同编号":"HT00000033","合同名称":"重选客户的合同","客户编号":"KHM00000001","客户名称":"长沙中联重科股份有限公司","客户配比":null,"施工配比":"SG000132","工程名称":"生产调度测试任务","工程地址":"生产调度测试任务","砼强度":"C20","浇筑方式":"泵送","施工部位":"基础","泵名称":null,"抗渗等级":null,"抗压等级":null,"抗冻等级":null,"骨料粒径":null,"水泥品种":null,"坍落度":null,"混凝土方量":10,"砂浆方量":0,"剩余方量":2,"计划方量":890,"运输方量":0,"车牌号":"03","累计车数":13,"发车时间":"10:02","驾驶员":"admin","质检员":"admin","调度员":null,"上料员":null,"操作员":null,"计划班组":null,"生产日期":"2012-03-08 08:53:13","搅拌站名称":"2#站","供货单位":null,"施工单位":null,"":"搅拌站名称","运距":null,"工地联系人":null,"工地电话":null,"签收方量":10,"理论配比名称":"C30P6"}';
    WebDesign(data);
}
function StuffInRepDesign() {
    var data = '{"入库单号":"201203080005","原料名称":"水泥","筒仓名称":"A5"}';
    WebDesign_S(data);
}
function GoodsInRepDesign() {
    var data = '{"入库编号":"GIN2014040001","物资名称":"签字笔","供应商":"五金店","运输商":"自提","入库数量":"20","入库单价":"1.5","入库时间":"2014-04-23 10:2348","经办人":"罗峰","备注":"备注"}';
    WebDesign_GoodsIn(data);
}
function GoodsOutRepDesign() {
    var data = '{"领用编号":"GO2014040001","物资名称":"签字笔", "经办人":"罗峰","备注":"备注"}';
    WebDesign_GoodsOut(data);
}
function GoodsBorrowRepDesign() {
    var data = '{"借用编号":"GO2014040001","物资名称":"签字笔", "经办人":"罗峰","备注":"备注"}';
    WebDesign_GoodsBorrow(data);
}

var reportTemplate = window.location.protocol + "//" + window.location.host + "/Content/Files/Report/ShippingDocument.xml";
var reportTemplate_S = window.location.protocol + "//" + window.location.host + "/Content/Files/Report/StuffIn.xml";
var reportTemplate_GoodsIn = window.location.protocol + "//" + window.location.host + "/Content/Files/Report/GoodsIn.xml";
var reportTemplate_GoodsOut = window.location.protocol + "//" + window.location.host + "/Content/Files/Report/GoodsOut.xml";
var reportTemplate_GoodsBorrow = window.location.protocol + "//" + window.location.host + "/Content/Files/Report/GoodsBorrow.xml";

//设置发货单模板
function setShipDocTemplate(fileName) {
    if (!isEmpty(fileName)) {
        reportTemplate = window.location.protocol + "//" + window.location.host + "/Content/Files/Report/" + fileName;
    }
}

function processShipDocPrint(type, data) {
    var newData = {};
    if (!$.isEmptyObject(data)) {
        if (isEmpty(data.PumpName)) {
            data.PumpName = data.CastMode;
        }
        if (data.BetonCount == 0 && data.SlurryCount > 0) {
            //只打砂浆，强度位置打印砂浆配比名
            data.ConStrength = data.FormulaName;
        }
        if (data.ShipDocType == "1") {
            //水票
            data.ShipDocType = "水票";
            data.ProvidedTimes = data.ProvidedCube = data.ParCube = data.RealSlump = "";
        } else if (data.ShipDocType == "0") {
            data.ShipDocType = "发货单";
        }

        if (isEmpty(data.Remark)) {
            data.Remark = "";
        }

        //整车转发不显示备注
        if (data.Remark.indexOf('整车转发') > 0) {
            data.Remark = '';
        }

        data.XuCube = data.XuCube + data.ParCube;

        for (k in data) {
            if (ShipDocProperties.hasOwnProperty(k)) {
                if (typeof (data[k]) == "string" && data[k].indexOf('Date(') > 0) {
                    newData[ShipDocProperties[k]] = dataTimeFormat(data[k]);
                }
                else {
                    newData[ShipDocProperties[k]] = data[k];
                }
            }
        }
        

        jsonStr = JSON.stringify(newData);

        //alert(jsonStr);

        if (type == 'print') {
            WebPrint(jsonStr);

        } else if (type == 'preview') {

            WebShowPrint(jsonStr);

        }
        else if (type == 'design') {
            WebDesign(jsonStr);
        }
    }
    else {
        alert("通讯失败！请联系管理员！");
    }
}
//预览打印
function WebShowPrint(data) {

    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate, _cacheTemplate); 
        ActReport.DicData = data;
        ActReport.WebShowPrint();

    }
}
//设计
function WebDesign(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate, _cacheTemplate);
        
        ActReport.DicData = data;
        ActReport.RequestCookie = _token;
        ActReport.ReportDesign();
    }
}
//直接打印
function WebPrint(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebPrint();
    }
}

//进出库单据打印预览
function WebShowPrint_S(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_S, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebShowPrint();
    }
}
//进出库单据设计
function WebDesign_S(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_S, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.RequestCookie = _token;
        ActReport.ReportDesign();
    }
}
//进出库单直接打印
function WebPrint_S(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_S, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebPrint();
    }
}

//物资入库单打印预览
function WebShowPrint_GoodsIn(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsIn, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebShowPrint();
    }
}
//物资入库单打印设计
function WebDesign_GoodsIn(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsIn, _cacheTemplate); 
        ActReport.DicData = data;
        ActReport.RequestCookie = _token;
        ActReport.ReportDesign();
    }
}
//物资入库单直接打印
function WebPrint_GoodsIn(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsIn, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebPrint();
    }
}

//物资领用单打印预览
function WebShowPrint_GoodsOut(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsOut, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebShowPrint();
    }
}
//物资领用单打印设计
function WebDesign_GoodsOut(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsOut, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.RequestCookie = _token;
        ActReport.ReportDesign();
    }
}
//物资领用单直接打印
function WebPrint_GoodsOut(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsOut, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebPrint();
    }
}

//物资借用单打印预览
function WebShowPrint_GoodsBorrow(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsBorrow, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebShowPrint();
    }
}
//物资借用单打印设计
function WebDesign_GoodsBorrow(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsBorrow, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.RequestCookie = _token;
        ActReport.ReportDesign();
    }
}
//物资领用单直接打印
function WebPrint_GoodsBorrow(data) {
    var ActReport = getActReport("ActReport");
    if (ActReport) {
        ActReport.loadReport(reportTemplate_GoodsBorrow, _cacheTemplate);
        ActReport.DicData = data;
        ActReport.WebPrint();
    }
}
//报表参数
var ShipDocProperties = { 
ID:"发货单号",
TaskID:"任务号",
ContractID: "合同编号",
ContractName:"合同名称",
CustomerID:"客户编号",
CustName:"客户名称",
CustMixpropID:"客户配比",
ConsMixpropID:"施工配比",
ProjectName: "工程名称",
Remark:"备注",
ProjectAddr:"工程地址",
ConStrength: "砼强度",
//此处添加相应的任务单中的字段，注意字段匹配
BetonTag: "砼标记",
OtherDemand:"其他要求",
CastMode:"浇筑方式",
ConsPos: "施工部位",
CarpGrade: "级配",
PumpName:"泵名称",
ImpGrade:"抗渗等级",
ImyGrade:"抗压等级",
ImdGrade:"抗冻等级",
CarpRadii:"骨料粒径",
CementBreed:"水泥品种",
RealSlump:"坍落度",
BetonCount:"混凝土方量",
SlurryCount:"砂浆方量",
ParCube: "本车方量",
XuCube: "其他方量2",
SignInCube:"签收方量",
RemainCube:"剩余方量", 
ProvidedCube:"累计方量",
PlanCube:"计划方量", 
TransferCube:"运输方量", 
CarID:"车牌号",
ProvidedTimes:"累计车数", 
DeliveryTime:"发车时间", 
Driver:"驾驶员",
Surveyor: "质检员",
Signer:"调度员",
ForkLift:"上料员",
Operator:"操作员",
PlanClass:"计划班组",
ProduceDate:"生产日期",
ProductLineName:"搅拌站名称", 
SupplyUnit:"供货单位",
ConstructUnit:"施工单位",
FormulaName:"理论配比名称", 
Distance:"运距", 
LinkMan:"工地联系人",
Tel: "工地电话"
, PumpName1: "清洗泵名称1"
, EnterpriseName: "企业名称"
};
var StuffInProperties = {
    ID: "入库单号",
    StuffName: "原料名称",
    SiloName: "筒仓名称",
    InDate: "进厂时间",
    SupplyName: "供货厂商",
    TransportName: "运输公司",
    CarNo: "运输车号",
    Driver: "司机",
    TotalNum: "毛重",
    CarWeight: "皮重",
    InNum: "净重",
    EnterpriseName: "企业名称"
};

var GoodsInProperties = {
    ID: "入库编号",
    GoodsName: "物资名称",
    SupplyName: "供应商",
    TransportName: "运输商",
    InNum: "入库数量",
    InPrice: "入库单价",
    InTime: "入库时间",
    Operator: "经办人",
    Remark: "备注",
    Builder: "创建人",
    BuildTime: "创建时间",
    Modifier: "修改人",
    ModifyTime: "修改时间",
    EnterpriseName: "企业名称"
};

var GoodsOutProperties = {
    ID: "领用编号",
    GoodsName: "物资名称",
    DepartmentName: "领用部门",
    OutMan: "领用人",
    OutNum: "领用数量",
    OutTime: "领用时间",
    OutReason: "领用原因",
    Operator: "经办人",
    Remark: "备注",
    Builder: "创建人",
    BuildTime: "创建时间",
    Modifier: "修改人",
    ModifyTime: "修改时间"
};

var GoodsBorrowProperties = {
    ID: "借用编号",
    GoodsName: "物资名称",
    DepartmentName: "借用部门",
    BorrowMan: "借用人",
    BorrowNum: "借用数量",
    BorrowTime: "借用时间",
    BorrowReason: "借用原因",
    Operator: "经办人",
    Remark: "备注",
    Builder: "创建人",
    BuildTime: "创建时间",
    Modifier: "修改人",
    ModifyTime: "修改时间"
};

if (navigator.appVersion.indexOf("MSIE") >= 0) {
    document.write('<object id="ActReport" classid="clsid:BE05092E-8D70-48F7-8341-1C707C3A3535" width="0" height="0" ></object><script language="javascript" event="OnPrintCompleted" for="Report">OnPrintCompleted();</script>');
}
//else if (navigator.mimeTypes["application/x-itst-activex"]) {
else {
    document.write('<object id="ActReport" clsid="{BE05092E-8D70-48F7-8341-1C707C3A3535}" event_OnPrintCompleted="OnPrintCompleted" type="application/x-itst-activex" width="0" height="0" ></object>');
}