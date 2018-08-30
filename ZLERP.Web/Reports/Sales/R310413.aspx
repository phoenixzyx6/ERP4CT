<%@ Page Title="砼产品销售收款统计表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        { 
        }
        //设置ReportViewer高度
        if (Request.Cookies["rFrameHeight"] != null)
        {
            string strHeight = Server.HtmlEncode(Request.Cookies["rFrameHeight"].Value); ;
            this.ReportViewer1.Height = int.Parse(strHeight);
        }
        else
        {
            this.ReportViewer1.Height = 500;
        }
        this.ReportViewer1.LocalReport.DisplayName = string.Format("{0}({1}-{2})", this.Title, tbBeginDate.Text,tbEndDate.Text);
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        if (tbBeginDate.Text.Trim()=="")
        {
            lblMsg.Text = "开始时间不能为空！";
            return;
        }
        if (tbEndDate.Text.Trim() == "")
        {
            lblMsg.Text = "结束时间不能为空！";
            return;
        }
        if (Convert.ToDateTime(this.tbBeginDate.Text) >= Convert.ToDateTime(this.tbEndDate.Text))
        {
            lblMsg.Text = "开始时间不能大于结束时间！";
            return;
        }
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=tbBeginDate.ClientID %>').datetimepicker();
            $('#<%=tbEndDate.ClientID %>').datetimepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">           
        </asp:ScriptManager>
    起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    
    &nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" AsyncRendering="False">
        <LocalReport ReportPath="Reports\Sales\R310413.rdlc"  DisplayName="生产销售累计表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ERPReports %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_sales_pay"  SelectCommandType="StoredProcedure" 
            CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                  <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />  
             　
           </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
