<%@ Page Title="生产任务单报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true"  %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<script runat="server" language="C#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!IsPostBack)
        {
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            this.ReportViewer1.LocalReport.Refresh();
        } 
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbBeginDate.Text) && !string.IsNullOrEmpty(tbEndDate.Text) && string.Compare(tbBeginDate.Text, tbEndDate.Text) > 0)
        {
            lblMsg.Text = "日期范围错误";
            this.ReportViewer1.Visible = false;
            return;
        }
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
        SetEnterpriseName();
        
    }
    protected void SetEnterpriseName()
    {
        LocalReport rpt = this.ReportViewer1.LocalReport;
        //设置EnterpriseName参数为全局配置中的EnterpriseName
        var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
        if (entCfg != null)
        {
            rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
        }
    }

    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();

        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //        $('#<%=tbBeginDate.ClientID %>').datetimepicker();
            //        $('#<%=tbEndDate.ClientID %>').datetimepicker();
            var dates = $("#<%=tbBeginDate.ClientID %>, #<%=tbEndDate.ClientID %>").datetimepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbBeginDate.ClientID %>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    开始时间<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    <asp:DropDownList ID="DrpList" runat="server">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem Value="1">已完工</asp:ListItem>
        <asp:ListItem Value="0">未完工</asp:ListItem>
    </asp:DropDownList>

    <!--新加了两个查询条件-->
    工程名称：<asp:TextBox ID="tbProjectName" Width="100" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>


    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
        Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
        WaitMessageFont-Size="14pt" Width="100%" Height="500px" 
        SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Produce\R410802.rdlc" DisplayName="生产任务单报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_ProduceTaskDetail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
           <SelectParameters>

               <asp:ControlParameter  ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
               <asp:ControlParameter   ControlID="tbEndDate" Name="EndDate" PropertyName="Text"  />
               <asp:ControlParameter ControlID="DrpList" Name="IsCompleted" 
                   PropertyName="SelectedValue" />
               
               <asp:ControlParameter ControlID="tbProjectName" Name="projectname" PropertyName="Text" />
               <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />     

           </SelectParameters>
 
    </asp:SqlDataSource>
</asp:Content>
