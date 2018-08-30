<%@ Page Title="每日生产方量对账表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            ZLERP.Web.Helpers.SqlServerHelper helper = new ZLERP.Web.Helpers.SqlServerHelper();

            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }

        }
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}{1}", this.Title, tbBeginDate.Text);
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}{1}", this.Title, tbBeginDate.Text);
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
     
    选择日期：<asp:TextBox ID="tbBeginDate" CssClass="short" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator runat="server" CssClass="red" ControlToValidate="tbBeginDate" Display="Dynamic" ErrorMessage="*">
            </asp:RequiredFieldValidator> 


            <!--新加了两个查询条件-->
    工程名称：<asp:TextBox ID="tbProjectName" Width="100" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>


    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
     
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410731.rdlc"  DisplayName="每日生产方量对账表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_produce_stats_checking"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                  <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />

                   <asp:ControlParameter ControlID="tbProjectName" Name="projectname" PropertyName="Text" />
               <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />  


           </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
