<%@ Page Title="原材料进货结算单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
        }
        else
        {
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
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
    }

    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        LocalReport rpt = (LocalReport)e.Report;
        if (subDataSource.SelectParameters.Count == 6)
        {
            subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["StuffID"]);
            subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["SupplyID"]);
            subDataSource.SelectParameters.Remove(subDataSource.SelectParameters["TransportID"]);

        }
        subDataSource.SelectParameters.Add("StuffID", rpt.GetParameters()["StuffID"].Values.FirstOrDefault());
        subDataSource.SelectParameters.Add("SupplyID", rpt.GetParameters()["SupplyID"].Values.FirstOrDefault());
        subDataSource.SelectParameters.Add("TransportID", rpt.GetParameters()["TransportID"].Values.FirstOrDefault());
        rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));
    }

</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        var Date_Length = 10;//2013-01-01

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
      结算起始日期：<asp:TextBox ID="tbBeginDate"  runat="server"></asp:TextBox> 
 结算截止日期：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
 供货厂家：<asp:TextBox ID="tbSupplyID" runat="server"></asp:TextBox>
运输厂家：<asp:TextBox ID="tbTransportID" runat="server"></asp:TextBox>
 <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"  OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\Bale\R220803.rdlc"  DisplayName="原材料进货结算单">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_stuffin_stats"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbSupplyID" Name="SupplyID" PropertyName="Text" />   
            <asp:ControlParameter ControlID="tbTransportID" Name="TransportID" PropertyName="Text" />
          </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_StuffIn_Detail"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />   
            <asp:Parameter DefaultValue="1" Name="FootStatus" Type="Int16" />         
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
