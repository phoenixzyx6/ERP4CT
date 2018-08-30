<%@ Page Title="生产计划表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        { 
            tbBeginDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            
        }
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}{1}", this.Title, tbBeginDate.Text);
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", Convert.ToDateTime(tbBeginDate.Text).ToString("yyyy年MM月dd日")));
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}{1}", this.Title, tbBeginDate.Text); 
        SetEnterpriseName();
    }

    void SetEnterpriseName() {
        ZLERP.Web.Helpers.SqlServerHelper helper = new ZLERP.Web.Helpers.SqlServerHelper();
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
        $.datepicker.setDefaults({ dateFormat: 'yy-mm-dd' });
        $('#<%=tbBeginDate.ClientID %>').datepicker();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
     
    选择计划日期：<asp:TextBox ID="tbBeginDate" CssClass="short" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator runat="server" CssClass="red" ControlToValidate="tbBeginDate" Display="Dynamic" ErrorMessage="*">
            </asp:RequiredFieldValidator> 
    <asp:CheckBox ID="cbAudit"  runat="server" Text="已审核" Checked="true"></asp:CheckBox > 
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
     
    <rsweb:ReportViewer ID="ReportViewer1" CssClass="reportViewer" Width="100%" 
            Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SplitterBackColor="White" 
            SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Produce\R310604.rdlc"  DisplayName="生产计划表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_CustomerPlan"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                  <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
                  <asp:ControlParameter ControlID="cbAudit" Name="auditstatus" PropertyName="Checked" />
           </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
