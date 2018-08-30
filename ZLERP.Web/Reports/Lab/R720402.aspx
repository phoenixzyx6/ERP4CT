<%@ Page Title="矿粉试验报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    List<ReportDataSource> items;
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {

            if (Request["reportid"] != null)
            {
                LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;

                this.tbExamID.Text = Request["reportid"].ToString();
                SetEnterpriseName();
            }
        }
        //定义子报表处理方法
        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbExamID.Text.Length > 0)
        {

            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("AirOriginId", tbExamID.Text.Trim()));
            
            this.ReportViewer1.LocalReport.Refresh();
            SetEnterpriseName();
        }

        
    }
    //void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
    //{
    //    foreach (var ReportDataSource in items)
    //    {
    //        ReportViewer1.LocalReport.DataSources.Add(ReportDataSource);
    //    }
    //}
    
    void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
    {
        //e.DataSources.Clear();
        //string fname = e.Parameters["AirOriginId"].Values[0];

        //foreach (var ReportDataSource in subDataSource)
        //{
        //    e.DataSources.Add(ReportDataSource);
        //}
        
        e.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource"));
        e.DataSources.Add(new ReportDataSource("DataSet2", "subDataSource2"));
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
        检测编号<asp:TextBox ID="tbExamID" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" 
            Width="57px" />
        <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" 
            runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Lab\R720402.rdlc"  DisplayName="矿粉试验报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
       </rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>" ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Lab_Air2Origin"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="tbExamID" Name="AirOriginId" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>" ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Lab_Air2Density" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbExamID" Name="AirOriginId" PropertyName="Text" /> 
        </SelectParameters>
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="subDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>" ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Lab_Air2ActiveInfo" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbExamID" Name="AirOriginId" PropertyName="Text" /> 
        </SelectParameters>
       </asp:SqlDataSource>
</asp:Content>
