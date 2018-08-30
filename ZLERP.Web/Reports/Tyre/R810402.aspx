<%@ Page Title="车辆轮胎维修记录表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"   Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbCar.DataSource = SqlDataSource2;
            tbCar.DataValueField = "CarID";
            tbCar.DataTextField = "CarID";
            this.tbCar.DataBind();
            this.ReportViewer1.LocalReport.Refresh();
        }
        
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
        LocalReport rpt = this.ReportViewer1.LocalReport;
        this.ReportViewer1.LocalReport.Refresh();
    }

    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();

        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
        this.SqlDataSource2.ConnectionString = this.SqlDataSource2.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource2.ProviderName = this.SqlDataSource2.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource2.DataBind();
    }
    
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $(document).ready(function () {
            //$('#<%=tbBeginDate.ClientID %>').datetimepicker({ hour: 8 });
            //$('#<%=tbEndDate.ClientID %>').datetimepicker({ hour: 8 });

            var dates = $("#<%=tbBeginDate.ClientID %>, #<%=tbEndDate.ClientID %>").datetimepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbBeginDate.ClientID%>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                    dates.not(this).datepicker("option", option, date);
                }
            });
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>   
    起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
        车号：<asp:DropDownList ID="tbCar" runat="server" Width="150px">
              </asp:DropDownList>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Tyre\R810402.rdlc"  DisplayName="车辆轮胎维修记录表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_TyreRepair"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
       <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="beginDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="endDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbCar" Name="CarID" PropertyName="Text" />
       </SelectParameters>
       </asp:SqlDataSource> 
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select '' CarID union select  CarID from Car where IsUsed = 1"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
</asp:Content>
