<%@ Page Title="材料消耗表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
        }
        else
        {            
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.DropDownList1.DataSource = this.SqlDataSource2;
            this.DropDownList1.DataTextField = "ProductLineName";
            this.DropDownList1.DataValueField = "ProductLineID";
            this.DropDownList1.DataBind();
            this.ReportViewer1.LocalReport.Refresh();
        }
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;

        //获取全局配置中的ChangeTime
        var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
        if (Cfg_ChangeTime != null)
        {
            if (this.tbBeginDate.Text.Length <= 10)
            {
                this.tbBeginDate.Text += " " + Cfg_ChangeTime.ConfigValue;
            }

            if (this.tbEndDate.Text.Length <= 10)
            {
                this.tbEndDate.Text += " " + Cfg_ChangeTime.ConfigValue;
            }
        }

        rpt.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text.Trim()));
        rpt.SetParameters(new ReportParameter("EndDate", tbEndDate.Text.Trim()));
    }


</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var Date_Length = 10; //2013-01-01

            if ($('#<%=tbBeginDate.ClientID %>').val().length <= Date_Length) {
                $('#<%=tbBeginDate.ClientID %>').datepicker();
            }
            else {
                $('#<%=tbBeginDate.ClientID %>').datetimepicker();
            }

            if ($('#<%=tbEndDate.ClientID %>').val().length <= Date_Length) {
                $('#<%=tbEndDate.ClientID %>').datepicker();
            }
            else {
                $('#<%=tbEndDate.ClientID %>').datetimepicker();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    起始日期：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止日期：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
       生产线：<asp:DropDownList ID="DropDownList1" runat="server" Width="150px" >
    </asp:DropDownList>
    工程名称:  <asp:TextBox ID="tbProjectName" runat="server"></asp:TextBox>
    砼强度:<asp:TextBox ID="tbConStrength" Width="80" runat="server"></asp:TextBox>
    浇筑方式:<asp:TextBox ID="tbCastMethod" Width="80" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Bale\R220801.rdlc" DisplayName="材料消耗表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_StuffandBale"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="DateFrom" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="DateTo" PropertyName="Text" />
              <asp:ControlParameter ControlID="tbProjectName" Name="ProjectName" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbConStrength" Name="ConStrength" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbCastMethod" Name="CastMethod" PropertyName="Text" />
              <asp:ControlParameter ControlID="DropDownList1" Name="ProductLineID" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
      <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select ''  ProductLineID, '' ProductLineName  union  select ProductLineID,ProductLineName from ProductLine  where IsUsed=1"  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>
</asp:Content>