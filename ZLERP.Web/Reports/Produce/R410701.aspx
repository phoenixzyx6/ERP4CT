<%@ Page Title="调度日报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {

            tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbProductLineID.DataSource = this.SqlDataSource2;
            this.tbProductLineID.DataTextField = "ProductLineName";
            this.tbProductLineID.DataValueField = "ProductLineID";
            this.tbProductLineID.DataBind();
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
    }

    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        LocalReport rpt = (LocalReport)e.Report;
        if (subDataSource.SelectParameters.Count == 3)
        {
            subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["TaskID"]);

        }
        subDataSource.SelectParameters.Add("TaskID", rpt.GetParameters()["TaskID"].Values.FirstOrDefault());
        
        //subDataSource.SelectParameters.Add("ProjectName",  "卡子湾18#楼");
        rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=tbDate.ClientID %>').datepicker();           

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    报表日期：<asp:TextBox ID="tbDate" runat="server"></asp:TextBox>
    生产线：<asp:DropDownList ID="tbProductLineID" runat="server" Width="150px"></asp:DropDownList>
    
    <!--新加了两个查询条件-->
    工程名称：<asp:TextBox ID="tbProjectName" Width="120" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>
    
    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt" 
    OnDrillthrough="ReportViewer1_Drillthrough" AsyncRendering="False">
        <LocalReport ReportPath="Reports\Produce\R410701.rdlc" DisplayName="调度日报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_dd_daily"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbDate" Name="Date" PropertyName="Text"/>
            <asp:ControlParameter ControlID="tbProductLineID" Name="ProductLineID" PropertyName="Text" />

            <asp:ControlParameter ControlID="tbProjectName" Name="projectname" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />


        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select ''  ProductLineID, '' ProductLineName  union  select ProductLineID,ProductLineName from ProductLine  where IsUsed=1"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
    <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_ShipDoc"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbDate" Name="Date" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbProductLineID" Name="ProductLineID" PropertyName="Text" />

        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
