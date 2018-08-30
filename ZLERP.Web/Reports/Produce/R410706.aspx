<%@ Page Title="生产任务通知单" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {
            this.DropDownList1.DataSource = this.SqlDataSource2;
            this.DropDownList1.DataTextField = "TaskID";
            this.DropDownList1.DataValueField = "TaskID";
            this.DropDownList1.DataBind();

            LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;
            if (Request["uid"] != null)
            {
                //LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;
                int i = this.DropDownList1.Items.IndexOf(this.DropDownList1.Items.FindByValue(Request["uid"].ToString()));
                this.DropDownList1.SelectedIndex = i;
            }
            //LocalReport rpt = this.ReportViewer1.LocalReport;
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (entCfg != null)
            {
                rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
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
        任务单编号 
        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" AutoPostBack="true" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410706.rdlc"  DisplayName="生产任务通知单">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_TaskDetail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="DropDownList1" Name="TaskID" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select TaskID from ProduceTasks"  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>
</asp:Content>
