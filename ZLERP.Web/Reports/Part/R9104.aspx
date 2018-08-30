<%@ Page Title="配件需求汇总" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
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
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {

    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>   
 
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Part\R9104.rdlc"  DisplayName="配件需求汇总">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="SELECT dbo.PartPlan.PlanID ,CONVERT(varchar(10),dbo.PartPlan.PlanDate,111) AS PlanDate,dbo.PartPlan.PlanMan ,dbo.PartPlan.PartID ,dbo.PartPlan.DepartmentID ,dbo.PartPlan.PlanNum ,dbo.PartPlan.Auditor ,dbo.PartPlan.AuditStatus ,dbo.PartPlan.AuditTime ,dbo.PartPlan.Remark ,dbo.PartPlan.Builder ,dbo.PartPlan.BuildTime ,dbo.PartPlan.Modifier ,dbo.PartPlan.ModifyTime ,dbo.PartPlan.Version ,dbo.PartPlan.Lifecycle,PartInfo.PartName,dbo.PartInfo.GreatClassID,PartInfo.MiddClassID,PartInfo.MinClassID FROM dbo.PartPlan LEFT JOIN dbo.PartInfo ON dbo.PartPlan.PartID = dbo.PartInfo.PartID ORDER BY PartInfo.PartID,dbo.PartPlan.PlanDate ASC"  
        SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource> 

</asp:Content>
