<%@ Page Title="生产计划执行报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }


            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");


            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));

            this.ReportViewer1.LocalReport.DisplayName = DateTime.Parse(tbBeginDate.Text).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(tbEndDate.Text).ToString("yyyy-MM-dd") + "生产计划执行报表";

            this.ReportViewer1.LocalReport.Refresh();

        }
    }
    
    void btnQuery_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbBeginDate.Text) && !string.IsNullOrEmpty(tbEndDate.Text) && string.Compare(tbBeginDate.Text, tbEndDate.Text) > 0)
        {
            lblMsg.Text = "时间范围错误";
            this.ReportViewer1.Visible = false;
            return;
        }
        else
        {
            lblMsg.Text = "";
            this.ReportViewer1.Visible = true;
        }

        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));

        this.ReportViewer1.LocalReport.DisplayName = DateTime.Parse(tbBeginDate.Text).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(tbEndDate.Text).ToString("yyyy-MM-dd") + "生产计划执行报表";

        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=tbBeginDate.ClientID %>').datepicker();
        $('#<%=tbEndDate.ClientID %>').datepicker();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     
    起始时间：<asp:TextBox ID="tbBeginDate" Width="135" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
        ControlToValidate="tbBeginDate" Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server" Width="135"></asp:TextBox>
    <asp:RequiredFieldValidator
        ID="RequiredFieldValidator2" runat="server" CssClass="red" ControlToValidate="tbEndDate"
        Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>

    <!--新加了两个查询条件-->
    工程名称：<asp:TextBox ID="tbProjectName" Width="100" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>


    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410732.rdlc"  DisplayName="生产计划执行报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
        ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
        SelectCommand="sp_rpt_produce_stats_dailyplan_carryout"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
        <SelectParameters>  
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate"  PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate"   PropertyName="Text" />

            <asp:ControlParameter ControlID="tbProjectName" Name="projectname" PropertyName="Text" />
                <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />  

        </SelectParameters>
    </asp:SqlDataSource> 

</asp:Content>
