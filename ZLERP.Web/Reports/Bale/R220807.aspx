<%@ Page Title="筒仓统计报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
        }
        else
        {
            this.ReportViewer1.LocalReport.Refresh();
                        
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e) {
        if (!string.IsNullOrEmpty(tbBeginDate.Text) && !string.IsNullOrEmpty(tbEndDate.Text) && string.Compare(tbBeginDate.Text, tbEndDate.Text) > 0)
        {
            lblMsg.Text = "日期范围错误";
            this.ReportViewer1.Visible = false;
            return;
        }
        else
        {
            lblMsg.Text = "";
            this.ReportViewer1.Visible = true;
        }

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
        
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }

    protected void tbProjectName_TextChanged(object sender, EventArgs e)
    {

    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {

        var Date_Length = 10; //2013-01-01

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
     
    日期从<asp:TextBox ID="tbBeginDate" Width="135"  runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red" ControlToValidate="tbBeginDate" Display="Dynamic" ErrorMessage="*">
            </asp:RequiredFieldValidator> 至 
    <asp:TextBox ID="tbEndDate" runat="server" Width="135" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red" ControlToValidate="tbEndDate" Display="Dynamic" ErrorMessage="*">
            </asp:RequiredFieldValidator> 
        
    &nbsp;<asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Bale\R220807.rdlc"  DisplayName="筒仓入库统计报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_stuffInReport_silo"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate"  PropertyName="Text" />
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDate"   PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>     
         
</asp:Content>
