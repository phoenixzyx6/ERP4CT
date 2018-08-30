<%--#
{
"ReportType":"实验室",
"Stencil":"PHBDesignReport",
"TplName":"混凝土配合比设计试验报告",
"FileName":"PHBDesignReport.aspx",
"PreviewFile":"PHBDesignReport_Preview.aspx",
"Parms":{"taskid":"ID","formulaid":"BetonFormula"}
}
#--%>
<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>混凝土配合比设计试验报告</title>
    <style type="text/css">
        @media print
        {
            /*打印时input边框不可见*/
            input, select, textarea
            {
                border: none;
            }
            #print
            {
                display: none;
            }
            #fixedBottom,#ExamTypeName
            {
                display: none;
            }
        }
        body
        {
            padding: 0px;
            margin: 0 0 30px 0;
            border: none;
            text-align: center;
        }
        table
        {
            font-size: 14px;
            line-height: 30px;
        }
        td
        {
            line-height: 30px;
            height: 30px;
        }
        .table
        {
            border: #000 1px solid;
            border-collapse: collapse;
        }
        .tableWithNoTop
        {
            border-top: none;
            border-collapse: collapse;
        }
        .tableWithNoTopAndBottom
        {
            border: #000 1px solid;
            border-bottom: none;
            border-top: none;
            border-collapse: collapse;
        }
        .tdWithNoTopAndBottom
        {
            border: #000 1px solid;
            border-bottom: none;
            border-top: none;
        }
        .table tr td
        {
            border: #000 1px solid;
        }
        .reportTitle
        {
            font-size: 24px;
            font-family: "微软雅黑";
            word-spacing: normal;
            letter-spacing: 8px;
            padding-bottom: 20px;
            padding-top: 10px;
        }
        .cpname
        {
            font-size: 18px;
        }
        #calendar
        {
            padding: 5px;
            text-align: left;
            border: 1px solid #7FCAE2;
            background: #FFF;
            margin-bottom: 0.8em;
        }
        #calendar td
        {
            padding: 2px;
            font-size: 12px;
            line-height: 12px;
            font-weight: bold;
        }
        #calendar_week td
        {
            height: 2em;
            line-height: 2em;
            border-bottom: 1px solid #E8E8E8;
        }
        #hourminute td
        {
            padding: 4px 2px;
            border-top: 1px solid #E8E8E8;
        }
        #append_parent a
        {
            color: #2366A8;
            font-size: 12px;
            text-decoration: none;
        }
        .calendar_expire, .calendar_expire a:link, .calendar_expire a:visited
        {
            color: #666;
            font-weight: normal;
        }
        .calendar_default, .calendar_default a:link, .calendar_default a:visited
        {
            color: #94BA3F;
        }
        .calendar_checked, .calendar_checked a:link, .calendar_checked a:visited
        {
            color: #f60;
            font-weight: bold;
        }
        td.calendar_checked, span.calendar_checked
        {
            background: #E8E8E8;
        }
        .calendar_today, .calendar_today a:link, .calendar_today a:visited
        {
            color: #000;
            font-weight: bold;
        }
        #calendar_header td
        {
            width: 30px;
            height: 20px;
            border-bottom: 1px solid #E8E8E8;
            font-weight: normal;
        }
        #calendar_year
        {
            display: none;
            line-height: 130%;
            background: #FFF;
            position: absolute;
            z-index: 10;
        }
        #calendar_year .col
        {
            float: left;
            background: #FFF;
            margin-left: 1px;
            border: 1px solid #E8E8E8;
            padding: 4px;
        }
        #calendar_month
        {
            display: none;
            background: #FFF;
            line-height: 130%;
            border: 1px solid #DDD;
            padding: 4px;
            position: absolute;
            z-index: 11;
        }
        .tablehover
        {
            border: #F90 solid 1px;
            background-color: #FC9;
        }
        .tablehover tr td
        {
            border: #F90 solid 1px;
        }
        .contentInfo
        {
            width: 80px;
            height: 25px;
            text-align: right;
            border: 1px #F90 solid;
            line-height: 25px;
            font-size: 12px;
            background-color: #FC3;
        }
        #fixedBottom
        {
            position: fixed;
            bottom: 0;
            background: #E1E1E1;
            text-align: center;
            width: 100%;
            height: 30px;
            border: #000 1px solid;
            border-left: none;
            border-right: none;
            line-height: 30px;
            z-index: 999; /*opacity:.70;
	        filter:alpha(opacity=70);除去透明度设置*/
            _bottom: auto;
            _width: 100%;
            _position: absolute;
            _top: expression(eval(document.documentElement.scollTop+document.documentElement.clientHeight-this.offsetHeight-(parseInt(this.currentStyle.marginTop, 10)||0)-(parseInt(this.currentStyle.marginBottom, 10)||0)));
        }
        .button
        {
            margin: 2px 5px;
            float: right;
            text-align: center;
            border: #06F solid 1px;
            word-spacing: normal;
            font-size: 15px;
            letter-spacing: 2px;
            border-radius: 4px;
            width: auto;
            padding: 0 8px;
            height: 24px;
            line-height: 24px;
            background-color: #06F;
            color: #FFF;
            cursor: pointer;
        }
        .hover
        {
            transition: border linear .2s,box-shadow linear .5s;
            -moz-transition: border linear .2s,-moz-box-shadow linear .5s;
            -webkit-transition: border linear .2s,-webkit-box-shadow linear .5s;
            outline: none;
            border-color: rgb(17,107,179,.3);
            box-shadow: 0 0 8px rgba(17,107,179,.8);
            -moz-shadow: 0 0 8px rgba(17,107,179,.3);
            -webkit-shadow: 0 0 8px rgba(17,107,179,.3);
        }
    </style>
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/json2.js")%>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.each($("form"), function (i, n) { $(this)[0].reset() });
            $("input,select,textarea").not($("input[attr=spec]")).each(function (i) {
                $(this).parent("td").css({ "padding-left": "2px", "padding-right": "2px" });
                $(this).css({ "padding-left": "2px", "font-size": "14px", "text-align": "center" });
                $(this).width(parseInt($(this).parent("td").width() * 0.8));
                $("#ExamTypeName").css({ "width": "130px", "text-align": "left" });

            });
            $("table[aliases]").hover(
                function () {
                    $(this).addClass("tablehover");
                    var pos = $(this).position();
                    $("#ContentInfo").html($(this).attr("aliases")).addClass("contentInfo").show().offset({ top: pos.top, left: pos.left - $("#ContentInfo").width() });
                }, function () {
                    $(this).removeClass("tablehover");
                    var pos = $(this).position();
                    $("#ContentInfo").hide().offset({ top: pos.top, left: pos.left - $("#ContentInfo").width() });
                }
	        );

            $.post("/StuffExam.mvc/GetExamTypeList", {}, function (data) {
                if (!$.isEmptyObject(data.Data)) {
                    $("#ExamTypeName").empty();
                    $("#ExamTypeName").append("<option value='-1'> </option>");
                    for (var i = 0; i < data.Data.length; i++) {
                        var d = data.Data[i];
                        $("#ExamTypeName").append("<option value='" + d + "'>" + d + "</option>");
                    }
                }
            });

            $("table #StuffInfo select").each(function (i) {
                var id = $(this).attr("id").toString();
                var typeSuffix = id.substring(0, id.indexOf("_"));
                $.post(
			        "/StuffExam.mvc/SearchByType",
			        {
			            typeSuffix: typeSuffix
			        },
			        function (data) {
			            if (!$.isEmptyObject(data.Data)) {
			                $("#" + id).empty();
			                $("#" + id).append("<option value='-1'> </option>");
			                t = $("#" + id);
			                $("#StuffReportCache").data(id, data.Data); //缓存报告数据
			                for (var i = 0; i < data.Data.length; i++) {
			                    var d = data.Data[i];
			                    t.append("<option value='" + d.ID + "'>" + d.ID + "</option>");
			                }
			            }
			        }
		        );
            });
            $("table #StuffInfo select").bind({
                change: function () {
                    var id = $(this).attr("id").toString();
                    var typeSuffix = id.substring(0, id.indexOf("_"));
                    var selectedVal = $(this).val();
                    //取对应报告的所有缓存数据
                    var cacheData = $("#StuffReportCache").data(id);
                    //匹配所选编号的缓存数据并将表单赋值
                    var _FactoryFiled = $("form#StuffForm #" + typeSuffix + "_Factory");
                    var _BreedFiled = $("form#StuffForm #" + typeSuffix + "_Breed");
                    var _TechnicalParamFiled = $("form#StuffForm #" + typeSuffix + "_TechnicalParam");
                    if (parseInt(selectedVal) == -1) {
                        _FactoryFiled.val("");
                        _BreedFiled.val("");
                        _TechnicalParamFiled.val("");
                        return false;
                    } else {
                        if (!$.isEmptyObject(cacheData)) {
                            $.each(cacheData, function (i, n) {
                                if (selectedVal == n.ID.toString()) {//找到匹配的数据
                                    _FactoryFiled.val(n.SupplyID);
                                    _BreedFiled.val(n.StuffName);
                                    _TechnicalParamFiled.val(n.TechnicalParam);
                                    return false; //退出循环
                                }
                            });
                        }
                    }

                }
            });

            $("#ExamTypeName").bind("change", function () {
                var typename = $(this).val();
                $("table #StuffInfo select").val(-1);
                $("table #StuffInfo select").trigger("change");
                $.post("/StuffExam.mvc/GetStuffExamList", { typename: typename }, function (data) {
                    if (!$.isEmptyObject(data.Data)) {
                        for (var i = 0; i < data.Data.length; i++) {
                            var d = data.Data[i];
                            var stufftype = d.StuffTypeID.length == 4 ? d.StuffTypeID.substring(0, 3) : d.StuffTypeID;
                            $("#" + stufftype + "_ReportID").val(d.ID);
                            $("#" + stufftype + "_ReportID").trigger("change");

                            $("#" + stufftype + "_ReportID").val(d.ID);
                            $("#" + stufftype + "_ReportID").trigger("change");
                        }
                    }
                });
            });

            //方式一：页面传值（具体的任务单信息和理论配比信息）

            //方式二：页面传参（只有任务单ID与理论配比名称）
            var recTaskID = '<% =Request.QueryString["taskid"]%>';
            var recFormulaID = '<% =Request.QueryString["formulaid"]%>';
            var recReportType = '<% =Request.QueryString["reporttype"]%>';
            var recStencil = '<% =Request.QueryString["stencil"]%>';
            var _CustNameFiled = $("form#TaskForm #CustName");
            var _ConstrengthFiled = $("form#TaskForm #Constrength");
            var _CementTypeFiled = $("form#TaskForm #CementType");
            var _ImpGradeFiled = $("form#TaskForm #ImpGrade");
            var _SlumpFiled = $("form#TaskForm #Slump");
            var _CastModeFiled = $("form#TaskForm #CastMode");
            /*加载任务单信息*/
            $.post(
		        "/ProduceTask.mvc/get",
		        {
		            id: recTaskID
		        },
		        function (data) {
		            if (data.Result && data.Data) {
		                _CustNameFiled.val(data.Data.CustName);
		                _ConstrengthFiled.val(data.Data.ConStrength);
		                _CementTypeFiled.val(data.Data.CementType);
		                _ImpGradeFiled.val(data.Data.ImpGrade);
		                _SlumpFiled.val(data.Data.Slump);
		                _CastModeFiled.val(data.Data.CastMode);
		            }
		        }
	        );

            /*加载理论配比信息*/
            $.post(
		        "/Formula.mvc/FindFormulaInfo",
		        { FormulaID: recFormulaID },
		        function (res) {
		            if (res.Data) {
		                var model = res.Result;
		                var ce_amount = 0; //水泥
		                var wa = 0; //水
		                var fa_amount = 0; //砂总量
		                var ca_amount = 0; //石头总量
		                var wc_amount = 0; //胶凝材料总量
		                var pos = 2;
		                var src = 0;
		                /*用标准值:适用于试验报表数据与实际生产数据存在差异的搅拌站,需要客户填写*/
		                //填充理论配合比
		                if (model == false) {		                    
		                    $.each(res.Data, function (i, n) {
		                        if (n.StuffTypeID.toString().indexOf("FA") != -1) fa_amount += n.StandardAmount;
		                        if (n.StuffTypeID.toString().indexOf("CA") != -1) ca_amount += n.StandardAmount;
		                        if (n.StuffTypeID.toString().indexOf("CE") != -1) ce_amount = n.StandardAmount;
		                        if (n.StuffTypeID.toString().indexOf("WA") != -1) wa = n.StandardAmount;
		                        if (n.StuffTypeID.toString().indexOf("AIR") != -1) wc_amount += n.StandardAmount;
		                        var match_c = $("#" + n.StuffTypeID + "_Amount");
		                        if (match_c) match_c.val(n.StandardAmount);
		                    });

		                    //计算质量配合比（以水泥用量为标准，各材料用量除以水泥用量）
		                    $.each(res.Data, function (i, n) {
		                        var match_Qc = $("#" + n.StuffTypeID + "_QAmount");
		                        if (match_Qc) {
		                            src = n.StandardAmount / ce_amount;
		                            match_Qc.val(Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos))
		                        }

		                    });
		                }
		                /*用理论值：适用于试验报表数据与实际生产数据相同的搅拌站
		                //填充理论配合比用理论值*/
		                else {
		                    $.each(res.Data, function (i, n) {
		                        if (n.StuffTypeID.toString().indexOf("FA") != -1) fa_amount += n.StuffAmount;
		                        if (n.StuffTypeID.toString().indexOf("CA") != -1) ca_amount += n.StuffAmount;
		                        if (n.StuffTypeID.toString().indexOf("CE") != -1) ce_amount = n.StuffAmount;
		                        if (n.StuffTypeID.toString().indexOf("WA") != -1) wa = n.StuffAmount;
		                        if (n.StuffTypeID.toString().indexOf("AIR") != -1) wc_amount += n.StuffAmount;
		                        var match_c = $("#" + n.StuffTypeID + "_Amount");
		                        if (match_c) match_c.val(n.StuffAmount);
		                    });

		                    //计算质量配合比（以水泥用量为标准，各材料用量除以水泥用量）
		                    $.each(res.Data, function (i, n) {
		                        var match_Qc = $("#" + n.StuffTypeID + "_QAmount");
		                        if (match_Qc) {
		                            src = n.StuffAmount / ce_amount;
		                            match_Qc.val(Math.round(src * Math.pow(10, pos)) / Math.pow(10, pos))
		                        }
		                    });
		                }

		                //计算水胶比、砂率
		                //水胶比 = 水/胶凝材料（如粉煤灰、矿粉、硅粉等）
		                $("#WC_Rate").val(wc_amount == 0 ? 0 : Math.round((wa / wc_amount) * Math.pow(10, pos)) / Math.pow(10, pos))
		                //砂率 = 砂/（砂+石）
		                $("#SC_Rate").val(fa_amount + ca_amount == 0 ? 0 : Math.round((fa_amount / (fa_amount + ca_amount)) * Math.pow(10, pos)) / Math.pow(10, pos) * 100)
		            }
		        }
	        );
            //关闭
            $("#close").unbind("click");
            $("#close").bind("click", function (e) {
                window.close();
            })
            //打印
            $("#print").unbind("click");
            $("#print").bind("click", function (e) {
                window.print();
            })
            //打印并保存
            $("#save").unbind("click");
            $("#save").bind("click", function (e) {
                //前置动作，判断前后两次的内容有没有变化
                var reportdataCache = $("#ReportDataCache").data("reportdata");
                if (JSON.stringify($("form").serializeArray()) == reportdataCache) {
                    showTips("请不要重复提交保存", 0, 3);
                    return false;
                }
                //                var savename = window.prompt("请输入要保存的名称，如不填则按默认名称保存", "");
                //                if (savename == null || savename == "") {
                //                    alert(11);
                //                }
                $.post(
			        "/ReportDatum.mvc/SaveReport",
			        {
			            ReportDataContent: JSON.stringify($("form").serializeArray()),
			            ObjectID: recTaskID,
			            Stencil: recStencil, //使用具体的文件或名称（暂无定论，先测试用）
			            ReportType: recReportType
			        },
			        function (resp) {
			            if (resp.Result) {
			                showTips("保存成功", 0, 3);
			                $("#ReportDataCache").data("reportdata", JSON.stringify($("form").serializeArray()));
			                $("#print").trigger("click");
			            }
			        }
		        );
            });

            $(".button").hover(
  		        function () {
  		            $(this).addClass("hover");
  		        },
  		        function () {
  		            $(this).removeClass("hover");
  		        }
	        );

            function showTips(tips, height, time) {
                var windowWidth = document.documentElement.clientWidth;
                var tipsDiv = '<div class="tipsClass" style="width:200px;padding:3px 5px;">' + tips + '</div>';
                //var _tip = $("#reportcontainer").append(tipsDiv);
                var _tip = $(tipsDiv).appendTo($("#reportcontainer"));
                _tip.css({
                    "top": height + "px",
                    "left": ($(window).width() - $("div.tipsClass").width()) / 2 + "px",
                    "position": "fixed",
                    "height": "20px",
                    "line-height": "20px",
                    "background": "#06F",
                    "font-size": "12px",
                    "border": "1px #ff0 solid",
                    "margin": "0 auto",
                    "text-align": "center",
                    "color": "#fff",
                    "opacity": "1"
                }).fadeIn("slow");
                setTimeout(
                    function () {
                        $("div.tipsClass").slideUp("fast", function () {
                            $(this).remove();
                        });
                    }, (time * 1000)
                );
            }
        });
    </script>
</head>
<body>
    <div id="append_parent">
    </div>
    <div id="reportcontainer" style="width: 670px; text-align: center; margin: 0 auto;">
        <table width="670" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" class="cpname">
                    郴州泰湘新型材料有限公司实验室
                </td>
            </tr>
            <tr>
                <td height="35" align="center" class="reportTitle">
                    混凝土配合比设计试验报告
                </td>
            </tr>
            <tr>
                <td>
                    <form id="RIDForm" style="border: none;">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="225" valign="top">
                                委托编号：
                                <input name="DelegateID" type="text" id="DelegateID" size="15" attr="spec" />
                            </td>
                            <td width="203" valign="top">
                                检验编号：
                                <input name="CheckID" type="text" id="CheckID" size="15" attr="spec" />
                            </td>
                            <td width="202" valign="top">
                                报告编号：
                                <input name="ReportID" type="text" id="ReportID" size="15" attr="spec" />
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td>
                    <form id="TaskForm" style="border: none;">
                    <table id="TaskInfo" aliases="任务单信息" width="100%" border="0" align="center" cellpadding="0"
                        cellspacing="0" class="table">
                        <tr>
                            <td width="120" align="center">
                                委托单位
                            </td>
                            <td width="333" colspan="4" align="left">
                                <input name="CustName" type="text" id="CustName" size="40" value="" />
                            </td>
                            <td width="79" align="center">
                                检验性质
                            </td>
                            <td width="137" align="left">
                                <input name="JYXZ" type="text" id="JYXZ" value="自检" size="12" />
                            </td>
                        </tr>
                        <tr>
                            <td width="120" align="center">
                                设计强度等级
                            </td>
                            <td width="108" align="left">
                                <input name="Constrength" type="text" id="Constrength" size="12" value="" />
                            </td>
                            <td colspan="2" align="center">
                                混凝土种类
                            </td>
                            <td width="87" align="left">
                                <input name="CementType" type="text" id="CementType" value="" size="10" />
                            </td>
                            <td width="79" align="center">
                                收样日期
                            </td>
                            <td width="137" align="left">
                                <input name="ShouYangDate" type="text" id="ShouYangDate" size="15" onclick="showcalendar(event,this);" />
                            </td>
                        </tr>
                        <tr>
                            <td width="120" align="center">
                                设计抗渗等级
                            </td>
                            <td width="108" align="left">
                                <input name="ImpGrade" type="text" id="ImpGrade" size="12" />
                            </td>
                            <td width="31" rowspan="3" align="center">
                                稠<br />
                                度
                            </td>
                            <td width="103" align="center">
                                坍落度(mm)
                            </td>
                            <td width="87" align="left">
                                <input name="Slump" type="text" id="Slump" size="10" />
                            </td>
                            <td width="79" align="center">
                                检验日期
                            </td>
                            <td width="137" align="left">
                                <input name="JianYanDate" type="text" id="JianYanDate" size="15" onclick="showcalendar(event,this);" />
                            </td>
                        </tr>
                        <tr>
                            <td width="120" align="center">
                                浇筑方法
                            </td>
                            <td width="108" align="left">
                                <input id="CastMode" name="CastMode" type="text" size="12" />
                            </td>
                            <td width="103" align="center">
                                扩展度(mm)
                            </td>
                            <td width="87" align="left">
                                <input id="Expand" name="Expand" type="text" size="10" />
                            </td>
                            <td width="79" align="center">
                                报告日期
                            </td>
                            <td width="137" align="left">
                                <input name="ReportDate" type="text" id="ReportDate" size="15" onclick="showcalendar(event,this);" />
                            </td>
                        </tr>
                        <tr>
                            <td width="120" align="center">
                                养护方法
                            </td>
                            <td width="108" align="left">
                                <input name="Conserve" type="text" id="Conserve" value="标准养护" size="12" />
                            </td>
                            <td width="103" align="center">
                                维勃稠度(s)
                            </td>
                            <td width="87" align="left">
                                --
                            </td>
                            <td width="79" align="center">
                                检验依据
                            </td>
                            <td width="137" align="left">
                                <input name="According" type="text" id="According" size="15" value="JGJ55-2011" />
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td class="tdWithNoTopAndBottom" align="center">
                    使用原材料<select id="ExamTypeName" name="ExamTypeName">
                        <option value="-1"></option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <form id="StuffForm" action="" style="border: none;">
                    <table id="StuffInfo" aliases="原材料信息" width="100%" border="0" align="center" cellpadding="0"
                        cellspacing="0" class="table">
                        <tr>
                            <td width="26" rowspan="3" align="center">
                                水<br />
                                泥
                            </td>
                            <td width="46" align="center">
                                厂家
                            </td>
                            <td colspan="3" id="dd">
                                <input name="CE_Factory" type="text" id="CE_Factory" size="20" value="" />
                            </td>
                            <td width="26" rowspan="3" align="center">
                                砂
                            </td>
                            <td width="47" align="center">
                                产地
                            </td>
                            <td colspan="3">
                                <input name="FA_Factory" type="text" id="FA_Factory" size="20" value="" />
                            </td>
                            <td width="26" rowspan="6" align="center">
                                石
                            </td>
                            <td width="38" align="center">
                                产地
                            </td>
                            <td width="118">
                                <input name="CA_Factory" type="text" id="CA_Factory" size="10" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                品种
                            </td>
                            <td width="80">
                                <input name="CE_Breed" type="text" id="CE_Breed" size="6" value="" />
                            </td>
                            <td width="40" align="center">
                                强度<br />
                                等级
                            </td>
                            <td width="53">
                                <input name="CE_TechnicalParam" type="text" id="CE_TechnicalParam" size="6" value="" />
                            </td>
                            <td align="center">
                                品种
                            </td>
                            <td width="71">
                                <input name="FA_Breed" type="text" id="FA_Breed" size="6" value="" />
                            </td>
                            <td width="45" align="center">
                                模数
                            </td>
                            <td width="52">
                                <input name="FA_TechnicalParam" type="text" id="FA_TechnicalParam" size="6" value="" />
                            </td>
                            <td align="center">
                                品种
                            </td>
                            <td>
                                <input name="CA_Breed" type="text" id="CA_Breed" size="12" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                检验报告编号
                            </td>
                            <td colspan="2">
                                <select id="CE_ReportID" name="CE_ReportID">
                                    <option value="-1"></option>
                                </select>
                            </td>
                            <td colspan="2" align="center">
                                检验报告编号
                            </td>
                            <td colspan="2">
                                <select id="FA_ReportID" name="FA_ReportID">
                                    <option value="-1"></option>
                                </select>
                            </td>
                            <td rowspan="2" align="center">
                                粒径<br />
                                (mm)
                            </td>
                            <td>
                                <input name="CA_TechnicalParam" type="text" id="CA_TechnicalParam" size="6" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="3" align="center">
                                掺<br />
                                合<br />
                                料
                            </td>
                            <td height="44" align="center">
                                产地
                            </td>
                            <td colspan="3">
                                <input name="AIR_Factory" type="text" id="AIR_Factory" size="20" value="" />
                            </td>
                            <td rowspan="3" align="center">
                                外<br />
                                加<br />
                                剂
                            </td>
                            <td align="center">
                                厂家
                            </td>
                            <td colspan="3">
                                <input name="ADM_Factory" type="text" id="ADM_Factory" size="20" value="" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                品种
                            </td>
                            <td>
                                <input name="AIR_Breed" type="text" id="AIR_Breed" size="6" value="" />
                            </td>
                            <td align="center">
                                级别
                            </td>
                            <td>
                                <input name="AIR_TechnicalParam" type="text" id="AIR_TechnicalParam" size="6" value="" />
                            </td>
                            <td align="center">
                                品种
                            </td>
                            <td>
                                <input name="ADM_Breed" type="text" id="ADM_Breed" size="6" value="" />
                            </td>
                            <td align="center">
                                名称
                            </td>
                            <td>
                                JB-231
                            </td>
                            <td rowspan="2" align="center">
                                检验报告编号
                            </td>
                            <td rowspan="2">
                                <select id="CA_ReportID" name="CA_ReportID">
                                    <option value="-1"></option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                检验报告编号
                            </td>
                            <td colspan="2">
                                <select id="AIR_ReportID" name="AIR_ReportID">
                                    <option value="-1"></option>
                                </select>
                            </td>
                            <td colspan="2" align="center">
                                检验报告编号
                            </td>
                            <td colspan="2">
                                <select id="ADM_ReportID" name="ADM_ReportID">
                                    <option value="-1"></option>
                                </select>
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td class="tdWithNoTopAndBottom" align="center">
                    试验室确定配合比
                </td>
            </tr>
            <tr>
                <td>
                    <form id="Formula" style="border: none;">
                    <table id="FormulaInfo" aliases="理论配比信息" width="100%" border="0" align="center" cellpadding="0"
                        cellspacing="0" class="table">
                        <tr>
                            <td width="130" rowspan="3" align="center">
                                每m3 混凝土各材料用量(kg)
                            </td>
                            <td width="79" rowspan="2" align="center">
                                水
                            </td>
                            <td width="76" rowspan="2" align="center">
                                水泥
                            </td>
                            <td width="79" rowspan="2" align="center">
                                砂
                            </td>
                            <td colspan="2" align="center">
                                石
                            </td>
                            <td width="80" rowspan="2" align="center">
                                粉煤灰
                            </td>
                            <td width="80" rowspan="2" align="center">
                                外加剂
                            </td>
                        </tr>
                        <tr>
                            <td width="72" align="center">
                                31.5
                            </td>
                            <td width="72" align="center">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="79" align="center">
                                <input name="WA_Amount" type="text" id="WA_Amount" size="7" value="" />
                            </td>
                            <td width="76" align="center">
                                <input name="CE_Amount" type="text" id="CE_Amount" size="7" value="" />
                            </td>
                            <td width="79" align="center">
                                <input name="FA_Amount" type="text" id="FA_Amount" size="7" value="" />
                            </td>
                            <td width="72" align="center">
                                <input name="CA_Amount" type="text" id="CA_Amount" size="7" value="" />
                            </td>
                            <td width="72" align="center">
                                <input name="CA1_Amount" type="text" id="CA1_Amount" size="7" value="" />
                            </td>
                            <td width="80" align="center">
                                <input name="AIR1_Amount" type="text" id="AIR1_Amount" size="8" value="" />
                            </td>
                            <td width="80" align="center">
                                <input name="ADM1_Amount" type="text" id="ADM1_Amount" size="8" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td width="130" align="center">
                                质量配合比
                            </td>
                            <td width="79" align="center">
                                <input name="WA_QAmount" type="text" id="WA_QAmount" size="7" value="" />
                            </td>
                            <td width="76" align="center">
                                <input name="CE_QAmount" type="text" id="CE_QAmount" size="7" value="" />
                            </td>
                            <td width="79" align="center">
                                <input name="FA_QAmount" type="text" id="FA_QAmount" size="7" value="" />
                            </td>
                            <td width="72" align="center">
                                <input name="CA_QAmount" type="text" id="CA_QAmount" size="7" value="" />
                            </td>
                            <td width="72" align="center">
                                <input name="CA1_QAmount" type="text" id="CA1_QAmount" size="7" value="" />
                            </td>
                            <td width="80" align="center">
                                <input name="AIR1_QAmount" type="text" id="AIR1_QAmount" size="8" value="" />
                            </td>
                            <td width="80" align="center">
                                <input name="ADM1_QAmount" type="text" id="ADM1_QAmount" size="8" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td width="130" align="center">
                                水胶比
                            </td>
                            <td width="79" align="center">
                                <input name="WC_Rate" type="text" id="WC_Rate" size="7" value="" />
                            </td>
                            <td align="center">
                                砂率(%)
                            </td>
                            <td width="79" align="center">
                                <input name="SC_Rate" type="text" id="SC_Rate" size="7" value="" />
                            </td>
                            <td colspan="2" align="center">
                                抗渗最大压力(MPa)
                            </td>
                            <td colspan="2" align="center">
                                <input name="MaxImpGrade" type="text" id="MaxImpGrade" size="15" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td width="130" align="center">
                                抗压强度(MPa)
                            </td>
                            <td colspan="2" align="right">
                                <input name="ImyGradeDay1" type="text" id="ImyGradeDay1" size="5" value="7" attr="spec"
                                    style="width: 90px; padding-left: 2px; text-align: center;" />
                                (d)
                            </td>
                            <td colspan="2" align="center">
                                <input name="ImyGradeDay1Val" type="text" id="ImyGradeDay1Val" size="10" value="" />
                            </td>
                            <td align="right">
                                <input name="ImyGradeDay2" type="text" id="ImyGradeDay2" size="4" value="28" attr="spec"
                                    style="width: 30px; padding-left: 2px; text-align: center;" />
                                (d)
                            </td>
                            <td colspan="2" align="center">
                                <input name="ImyGradeDay2Val" type="text" id="ImyGradeDay2Val" size="15" value="" />
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="ResultInfo" width="100%" border="0" align="center" cellpadding="0" cellspacing="0"
                        class="table tableWithNoTop">
                        <tr>
                            <td width="30" align="center">
                                备<br />
                                注
                            </td>
                            <td width="257" align="left" valign="middle">
                                <textarea id="Remark" name="Remark" cols="36" rows="7">1、本配合比中的材料用量均为砂、石在干燥状态下的用量。</textarea>
                            </td>
                            <td width="37" align="center">
                                检<br />
                                验<br />
                                单<br />
                                位
                            </td>
                            <td width="307" align="center">
                                (检验报告专用章)
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="225" align="center" valign="middle">
                                批准：
                            </td>
                            <td width="203" align="center" valign="middle">
                                校核：
                            </td>
                            <td width="202" align="center" valign="middle">
                                审批：
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script src="<%=ResolveUrl("~/Scripts/calendar.js") %>" type="text/javascript"></script>
    <div id="ContentInfo" style="position: absolute;">
    </div>
    <div id="StuffReportCache">
    </div>
    <div id="ReportDataCache"></div>
    <div id="fixedBottom">
        <div style="height: auto; padding-right: 41%;">
            <div id="close" class="button">
                关闭本页</div>
            <div id="save" class="button">
                打印并保存</div>
            <div id="print" class="button">
                打印</div>
        </div>
    </div>
</body>
</html>
