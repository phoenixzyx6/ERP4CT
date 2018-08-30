<%@ Page Title="粉煤灰检测原始报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
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

        //System.IO.StreamReader mainstream = new System.IO.StreamReader(Server.MapPath(@"~\Reports\Lab\R720202.rdlc"));
        //ReportViewer1.LocalReport.LoadReportDefinition(mainstream);
        //mainstream.Close();
        //if (ReportViewer1.ShowReportBody == false)
        //{
        //    ReportViewer1.ShowReportBody = true;
        //}

        //List<string> _reportNameList = new List<string>();
        //_reportNameList.Add("R720202Sub");//这个名字为在插入子报表时候，需要输入的报表名称。这个名称可以有具体的文件也可以没有。不要用类似Subreport2名称去加载子报表，否则会出错  
        //_reportNameList.Add("R720202Sub2");
        //foreach (string reportName in _reportNameList)
        //{
        //    System.IO.StreamReader subStream = new System.IO.StreamReader(Server.MapPath(reportName + ".rdlc"));
        //    ReportViewer1.LocalReport.LoadSubreportDefinition(reportName, subStream);
        //    subStream.Close();
        //}
        ////设置主报表数据源和所有报表（主，子）报表需要的参数等逻辑 
        //items = new List<ReportDataSource>();
        //items.Add(new ReportDataSource("DataSet1", "subDataSource"));
        //items.Add(new ReportDataSource("DataSet2", "subDataSource2"));

        ////ReportViewer1.LocalReport.DataSources.Add(items);  
        ////设置子报表进行事件订阅              
        //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
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
        <LocalReport ReportPath="Reports\Lab\R720202.rdlc"  DisplayName="粉煤灰试验报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
       </rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>" ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Lab_AirOrigin"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="tbExamID" Name="AirOriginId" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="subDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>" ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Lab_BurntInfo" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbExamID" Name="AirOriginId" PropertyName="Text" /> 
        </SelectParameters>
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="subDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>" ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Lab_ActiveInfo" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbExamID" Name="AirOriginId" PropertyName="Text" /> 
        </SelectParameters>
       </asp:SqlDataSource>
</asp:Content>
