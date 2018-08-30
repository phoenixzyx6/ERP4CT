<%@ Page Title="油耗里程曲线图" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            //获取全局配置中的ChangeTime
            tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd") ;
            
            this.tbCar.DataSource = this.SqlDataSource2;
            this.tbCar.DataTextField = "CarNo";
            this.tbCar.DataValueField = "CarID";
            this.tbCar.DataBind();
        }
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
       
        if (tbCar.SelectedItem == null || string.IsNullOrEmpty(tbDate.Text))
        {
            return;
        }
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;
        rpt.SetParameters(new ReportParameter("Day", tbDate.Text.Trim()));
        rpt.SetParameters(new ReportParameter("CarID", tbCar.SelectedItem.Value.Trim()));
        rpt.SetParameters(new ReportParameter("isDouble", tbPrecision.SelectedItem.Value.Trim()));
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

            var dates = $("#<%=tbDate.ClientID %>").datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbDate.ClientID%>" ? "minDate" : "maxDate",
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
    日期：<asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
    车辆：<asp:DropDownList ID="tbCar" runat="server" Width="150px"> </asp:DropDownList>
    精度：<asp:DropDownList ID="tbPrecision" runat="server" Width="150px">
            <asp:ListItem Value="1">单倍精度</asp:ListItem>
            <asp:ListItem Value="2">双倍精度</asp:ListItem>
        </asp:DropDownList>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\GPS\R420704.rdlc"  DisplayName="油耗里程曲线图">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select  CarID,(CarID + ' -- ' + CarNo) as CarNo from Car where  IsUsed=1 and TerminalID is not null"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_GPSDayCarOilMile"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                <asp:ControlParameter ControlID="tbDate" Name="Day" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbCar" Name="CarID" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbPrecision" Name="isDouble" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
