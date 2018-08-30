<%@ Page Title="商砼销售提成考核明细表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            tbMonth.Text = DateTime.Today.ToString("yyyyMM");
            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("Month", Convert.ToInt32(this.tbMonth.Text).ToString()));

        var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
        if (entCfg != null)
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
        }
        //设置ReportViewer高度
        if (Request.Cookies["rFrameHeight"] != null)
        {
            string strHeight = Server.HtmlEncode(Request.Cookies["rFrameHeight"].Value);
            this.ReportViewer1.Height = int.Parse(strHeight);
        }
        else
        {
            this.ReportViewer1.Height = 500;
        }
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $.datepicker.setDefaults({dateFormat:'yymm'});
            $('#<%=tbMonth.ClientID %>').datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">           
        </asp:ScriptManager>
        报表时间：<asp:TextBox ID="tbMonth" runat="server"></asp:TextBox>
        销售员：<asp:TextBox ID="tbSaleMan" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" AsyncRendering="False">
        <LocalReport ReportPath="Reports\Sales\R310418.rdlc"  DisplayName="商砼销售提成考核明细表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_SaleDeductPer"  SelectCommandType="StoredProcedure" 
            CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                  <asp:ControlParameter ControlID="tbMonth" Name="Month" PropertyName="Text" /> 
                  
                  <asp:ControlParameter ControlID="tbSaleMan" Name="Salesman" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
