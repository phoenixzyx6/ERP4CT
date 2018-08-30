<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ddKanban.aspx.cs" Inherits="ZLERP.Web.Reports.Produce.ddKanban" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="refresh" content="10" />
    <title>调度看板</title>
    <link href="<%=ResolveUrl("~/Content/themes/default/jquery-ui-1.8.2.custom.css") %>"
        rel="stylesheet" type="text/css" />
    <style type="text/css">
        body
        {
            font-size: 18px;
            font-weight:800;
            margin: 0;
            padding: 0;
        }
        .taskinfo
        {
            padding: 5px;
            line-height: 1.5em;
            width: 500px;
            height: 420px;
            overflow-y: auto;
            float: left;
            margin: 0 5px 5px 0;
        }
        .taskinfo label
        {
            display: inline-block;
        }
        .taskinfo span
        {
            color:Red;
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
        .provided
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
        .provided b
        {
            color: Green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <asp:Repeater ID="rptShipDocs" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="taskinfo ui-widget-content ui-corner-all">
                    <div class="ui-widget-header ui-corner-all ui-helper-clearfix " style="padding: .3em;">                                            
                            <a class="qship" href="javascript:void 0;" act="run" rel='<%#Eval("DispatchID","/DispatchList.mvc/SetRun?id={0}") %>'>
                            开始</a>
                            <a class="qship" href="javascript:void 0;" act="complete" rel='<%#Eval("DispatchID","/DispatchList.mvc/SetCompleted?id={0}") %>'>
                            完成</a>
                    </div>
                    <label>
                        施工单位:</label><span><%#Eval("ConstructUnit")%></span><br />
                    <label>
                        工程名称:</label><span><%#Eval("ProjectName")%></span><br />
                    <label>
                        施工部位:</label><span><%#Eval("ConsPos") %></span><br />
                    <label>
                        强度等级:</label><span><%#Eval("ConStrength") %></span><br />
                    <label>
                        浇注方式:</label><span><%#Eval("CastMode")%></span><br />    
                    <label>
                        坍落度:</label><span><%#Eval("RealSlump")%></span><br />  
                     <label>
                        配比代号:</label><span><%#Eval("FormulaName")%></span><br /> 
                     <label>
                        运输车号:</label><span><%#Eval("CarID")%></span><br />             
                    <label>
                        混凝土方量:</label><span class="provided"><%#Eval("BetonCount", "{0:.#}")%></span>                        
                        <br />
                     <label>
                        砂浆方量:</label><span><%#Eval("SlurryCount", "{0:.#}")%></span>                        
                        <br /> 
                     <label>
                        生产状态:</label> <span> <%# Eval("IsRunning").ToString().ToLower() == "true" ? "<label style='color:Green'>生产</span>" : "等待"%></span>                        
                        <br />                      
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
            $('a.qship').bind('click', function () {
                var url = $(this).attr('rel');
                var act = $(this).attr('act');
                //window.open(url, 'qship', 'scrollbars=no,resizable=no,width=0,height=0');
                if (window.confirm("你确定要" + (act == "run" ? "开始吗？" : "置为完成吗？"))) {
                    $.post(url, null, function (response) {
                        if (response.Result) {
                            //alert("提示\n操作成功")
                        } else {
                            alert("错误：\n" + response.Message);
                        }
                        window.location.reload();
                    });
                }

            });
        });
    </script>
</body>
</html>
