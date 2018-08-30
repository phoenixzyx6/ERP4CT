<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kanban.aspx.cs" Inherits="ZLERP.Web.Reports.Produce.Kanban" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="refresh" content="30" />
    <title>调度看板</title>
    <link href="<%=ResolveUrl("~/Content/themes/default/jquery-ui-1.8.2.custom.css") %>"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-size: 12px;
            margin: 0;
            padding: 0;
        }
        .taskinfo
        {
            padding: 5px;
            line-height: 1.5em;
            width: 220px;
            height: 320px;
            overflow-y: auto;
            float: left;
            margin: 0 5px 5px 0;
        }
        .taskinfo label
        {
            color: #ccc;
            display: inline-block;
        }
        ul.tasks
        {
            margin: 0;
            padding: 0;
        }
        .taskinfo li
        {
            display: inline-block;
            margin-bottom: 5px;
            height: 250px;
            padding: 0;
        }
        .taskinfo a
        {
        }
        a.alert
        {
            color: Red;
        }
        ul.inner
        {
            margin: 0;
            padding: 0;
            text-align: center;
        }
        .inner li
        {
            border: 1px solid #ccc;
            background-color: #eee;
            width: 30px;
            height: 30px;
            line-height: 30px;
            text-align: center;
        }
        .inner a, .inner a:link
        {
            text-decoration: none;
        }
        .alert
        {
            color: red;
        }
        .provided b
        {
            color: Green;
        }
        table.inner
        {
            border-left: 1px solid #ccc;
            border-top: 1px solid #ccc;
            width: 100%;
        }
        .inner td, .inner th
        {
            border-width: 0 1px 1px 0;
            border-style: solid;
            border-color: #ccc;
            text-align: center;
        }
        #aReadyOpen
        {
            color: #FC0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="font-size: 16px; text-align: center; font-weight: bold; color: #2E6E9E;
            padding: 5px;">
            今日计划生产任务:
            <asp:Label ID="lblTotalTaskCount" runat="server" Text=""></asp:Label>
            ,正生产:<span id="openCount"></span> ,未开盘:<asp:Label ID="lbUnOpenCount" runat="server"
                Text=""></asp:Label><%--<span id="unOpenCount"></span>--%>
            ,已完成:<asp:Label ID="lbCompletedCount" runat="server" Text="" Style="color: green"></asp:Label>
            ,当日已供方量:
            <asp:Label ID="lbTodayParCube" runat="server" Text="" Style="color: #FF0000"></asp:Label>
            ,当日计划方量:
            <asp:Label ID="lbTodayPlanCube" runat="server" Text="" Style="color: #FF0000"></asp:Label>
            ,可调度车数:
            <asp:Label ID="lbDDCar" runat="server" Text="" Style="color: #FF0000"></asp:Label>
            ,出货车数:
            <asp:Label ID="lbRunningCar" runat="server" Text="" Style="color: #FF0000"></asp:Label>
            <a href="#" id="aReadyOpen" style="display: none;"></a>
        </div>
        <asp:Repeater ID="rptTasks" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="taskinfo ui-widget-content ui-corner-all">
                    <div class="ui-widget-header ui-corner-all ui-helper-clearfix " style="padding: .3em;">
                        <a class="qship" href="javascript:void 0;" rel='<%#Eval("TaskId","/DispatchList.mvc/AddDispatch?taskId={0}") %>'>
                            <%#Eval("ProjectName") %>
                            (<span><%#Eval("TaskID") %></span>)[运行车数：<%#Eval("CarNotBackCount")%>]</a>
                    </div>
                    <label style="color: #000000">
                        单位:</label><span><%#Eval("ConstructUnit")%></span><br />
                    <label style="color: #000000">
                        地址:</label><span><%#Eval("ProjectAddr")%></span><br />
                    <label style="color: #000000">
                        部位:</label><span><%#Eval("ConsPos") %></span><br />
                    <label style="color: #000000">
                        强度:</label><span><%#Eval("ConStrength") %></span><br />
                    <label style="color: #000000">
                        浇注方式:</label><span class="pumpType"><%#Eval("CastMode")%>&nbsp;&nbsp;</span>
                    <label style="color: #000000">
                        工长:</label><span><%#Eval("LinkMan") %></span><br />
                    <label style="color: #000000">
                        联系电话:</label><span><%#Eval("Tel") %></span><br />
                    <label style="color: #000000">
                        数量:</label><span class="provided"><%#Eval("PlanCube","{0:.#}")%></span><span style="color: #FF6600"><%#Eval("PerCubes","{0:#}")%>方/小时</span>
                    <br />
                    <label style="color: #000000">
                        最后发车:</label><span class="lastTime"></span>
                    <br />
                    <label style="color: #000000">
                        运行车辆:</label><span style="color: #000080; font-weight: bolder;"><%#Eval("CarNotBack")%></span><br />
                    <label style="color: #000000">
                        计划时间:</label><span class="ndt"><%#Eval("NeedDate")%></span><asp:Repeater ID="Repeater1"
                            DataSource='<%# BindTaskShippingDocs(Eval("TaskId").ToString()) %>' runat="server">
                            <HeaderTemplate>
                                <table class="inner" cellspacing="0">
                                    <tr>
                                        <th>
                                            车次
                                        </th>
                                        <th>
                                            车号
                                        </th>
                                        <th>
                                            砼类型
                                        </th>
                                        <th>
                                            方量
                                        </th>
                                        <th>
                                            累计
                                        </th>
                                        <th>
                                            出厂
                                        </th>
                                        <th>
                                            回厂
                                        </th>
                                        <!--  <th>
                                            时量
                                        </th>-->
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%#Eval("ProvidedTimes","{0:.#}")%>
                                    </td>
                                    <td>
                                        <%#Eval("CarID") %>
                                    </td>
                                    <td>
                                        <%# ((string)Eval("ShipDocType")) == "1"? "水":( ((decimal)Eval("SlurryCount")) > 0 ? "砂浆" : "砼")%>
                                    </td>
                                    <td>
                                        <%#Eval("ParCube","{0:.#}") %>
                                    </td>
                                    <td>
                                        <%#Eval("ProvidedCube","{0:.#}")%>
                                    </td>
                                    <td>
                                        <%#Eval("BuildTime","{0:H:mm}")%>
                                    </td>
                                    <td>
                                        <%#Eval("ArriveTime", "{0:H:mm}")%>
                                    </td>
                                    <!--  <td>
                                        <%#Eval("Surveyor")%>
                                    </td>-->
                                    <input class="bt" type="hidden" value='<%#Eval("BuildTime")%>' /><input class="pumpName"
                                        type="hidden" value='<%#Eval("PumpName")%>' />
                                    <input class="pd" type="hidden" value='<%#Eval("ProvidedCube","{0:.#}")%>' />
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table></FooterTemplate>
                        </asp:Repeater>
                    <input class="needDate" type="hidden" value='<%#Eval("NeedDate") %>' />
                </div>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js")%>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/Scripts/jquery-ui.min.js")%>" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            function isOverTime(dateStr, pumpType) {
                var ts = 20;
                if (pumpType == "塔吊") ts = 50;
                // else if (pumpType == "自流") ts = 50;
                var lastDate = new Date(Date.parse(dateStr.replace(/-/g, "/")));
                var nowDate = new Date();
                return ((nowDate - lastDate) / (1000 * 60)) > ts;
            }
            //两小时内开盘
            function isReadyOpen(needDate) {
                var needDate = new Date(Date.parse(needDate.replace(/-/g, "/")));
                var nowDate = new Date();
                var ts = needDate - nowDate;
                if (ts > 0 && ts <= 2 * 60 * 60 * 1000) {
                    return true;
                }
                return false;
            }
            function mm(m) {//计算小时数，保留一位小数，但不四舍五入
                return m.replace(/^((\d+?)(\.?)(\d+?))$/, function (a, b, c, d, e) { if (d == ".") { return c + "." + e.substr(0, 1) } else { return b } })
            }
            function timeSince(dateStr) {
                var lastDate = new Date(Date.parse(dateStr.replace(/-/g, "/")));
                var nowDate = new Date();
                var min = ((nowDate - lastDate) / (1000 * 60));
                if (min < 0) {
                    return dateStr;
                }
                if (min > 60)

                    return "<b><font color=red>" + mm((min / 60).toString()) + "</font></b>小时前(" + dateStr + ")";
                else
                    return "<b><font color=red>" + Math.round(min) + "</font></b>分钟前";
            }
            $('.inner').each(function () {
                //最后发车时间
                var me = $(this);
                var lastTime = $('input.bt', me).last().val();
                var pumpType = me.parents('.taskinfo').children('.pumpType').text();
                if (lastTime) {
                    me.parents('.taskinfo').children('.lastTime').html(timeSince(lastTime));
                    if (isOverTime(lastTime, pumpType)) {
                        me.parents('.taskinfo').children('.ui-widget-header').children('.qship').addClass('alert');
                    }
                }
                //已供
                var pd = $('input.pd', me).last().val();
                if (pd) {
                    var provided = me.parents('.taskinfo').children('.provided');
                    provided.html("<b>" + pd + "</b>/" + provided.html());
                }
                var pumpName = $('input.pumpName', me).val();
                if (pumpName) {
                    me.parents('.taskinfo').children('.pumpType').html(pumpName);
                }

                //未开盘
                if ($('tr', me).length < 2) {
                    me.parents('.taskinfo').hide();
                }
            });

            $('.taskinfo').each(function () {
                var me = $(this);
                var needDate = $('input.needDate', me).last().val();
                if (isReadyOpen(needDate)) {
                    me.addClass('readyOpen');
                    //$('table', me).before('计划时间：' + needDate);
                    //$('计划时间：' + needDate).insertBefore($('table', me));
                }
            });

            $('#openCount').html($('.taskinfo:visible').length);
            //$('#unOpenCount').html($('.taskinfo:hidden').length);
            $('#unOpenCount').html($('#lblTotalTaskCount').html() - $('#openCount').html());

            var readyOpen = $('.readyOpen:hidden');
            if (readyOpen.length > 0) {
                var link = $('#aReadyOpen');
                link.fadeIn();
                link.html('即将开盘:' + readyOpen.length);
                link.bind('click', function () {
                    readyOpen.appendTo('#divReadyOpen');
                    readyOpen.fadeIn();
                    $('#divReadyOpen').dialog({ width: 700, height: 500, modal: true });
                });
            }

            $('a.qship').bind('click', function () {
                var url = $(this).attr('rel');
                // $(this).attr('href',"javascript:void 0");
                window.open(url, 'qship', 'scrollbars=yes,resizable=yes,width=600,height=300');

            });
        });
    </script>
    <div id="divReadyOpen" title="即将开盘的任务" style="display: none;">
    </div>
</body>
</html>
