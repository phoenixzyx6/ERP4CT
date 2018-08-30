<%@ Page Title="车辆工地明细表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            lbInfo.Text = "";
            tbMonth.Text = DateTime.Today.ToString("yyyyMM") ;

            this.tbCarID.DataSource = this.SqlDataSource2;
            this.tbCarID.DataTextField = "CarID";
            this.tbCarID.DataValueField = "CarID";
            this.tbCarID.DataBind();

            this.tbPricePlan.DataSource = this.SqlDataSource3;
            this.tbPricePlan.DataTextField = "Name";
            this.tbPricePlan.DataValueField = "CarProjectPriceMainId";
            this.tbPricePlan.DataBind();
        }
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        if (this.tbMonth.Text == "")
        {
            lbInfo.Text = "日期不能为空！";
            return;
        }
        lbInfo.Text = "";
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("Month", this.tbMonth.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarID", this.tbCarID.Text));
        var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
        if (entCfg != null)
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
        }
        string tbBeginDate = "";
        string tbEndDate = "";
        var Cfg_ChangeDay = SysConfigs.Where(p => p.ConfigName == "ChangeDay").FirstOrDefault();
        if (Cfg_ChangeDay != null)
        {
            DateTime changeDay = DateTime.Parse(tbMonth.Text.Substring(0, 4) + "-" + tbMonth.Text.Substring(4, 2) + "-" + Cfg_ChangeDay.ConfigValue + " 00:00:00");
            tbBeginDate = changeDay.AddDays(-1).ToString("yyyy-MM-dd")+" 23:59:59";
            tbEndDate = changeDay.AddMonths(-1).ToString("yyyy-MM-dd")+" 00:00:00";
        }

        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbEndDate));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbBeginDate));
    }

    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();
        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $.datepicker.setDefaults({ dateFormat: 'yymm'
            });
            $('#<%=tbMonth.ClientID %>').datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>     
    报表月份：<asp:TextBox ID="tbMonth" runat="server"></asp:TextBox>
    车号：<asp:DropDownList ID="tbCarID" runat="server" Width="100px"></asp:DropDownList>  
    单价方案：<asp:DropDownList ID="tbPricePlan" runat="server" Width="100px"></asp:DropDownList> 
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
        <asp:Label ID="lbInfo" runat="server" ForeColor="Red" Text="提示"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" AsyncRendering="False">
        <LocalReport ReportPath="Reports\Car\R610215.rdlc"  DisplayName="车辆工地明细表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
    </rsweb:ReportViewer> 

       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_CarProjectDetail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>
            <asp:ControlParameter ControlID="tbMonth" Name="Month" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbCarID" Name="CarID" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbPricePlan" Name="PlanId" PropertyName="Text" />
        </SelectParameters>
       </asp:SqlDataSource> 
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select CarID from car where isused=1 order by carid"
        SelectCommandType="Text" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="SELECT CarProjectPriceMainId,Name from CarProjectPriceMain ORDER BY CarProjectPriceMainId "
        SelectCommandType="Text" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>
</asp:Content>
