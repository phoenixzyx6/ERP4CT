<%@ Page Title="剩退料统计报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }

            //获取全局配置中的ChangeTime
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            if (Cfg_ChangeTime != null)
            {

                tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
                tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            }
            else
            {
                tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd 00:00");
                tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd 00:00");
            }
            this.tbLinkMan.DataSource = this.SqlDataSource2;
            this.tbLinkMan.DataTextField = "TrueName";
            this.tbLinkMan.DataValueField = "TrueName";
            this.tbLinkMan.DataBind();

            this.tbSigner.DataSource = this.SqlDataSource3;
            this.tbSigner.DataTextField = "TrueName";
            this.tbSigner.DataValueField = "TrueName";
            this.tbSigner.DataBind();

            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));
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
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=tbBeginDate.ClientID %>').timepicker();
        $('#<%=tbEndDate.ClientID %>').timepicker();
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
    前场工长：<asp:DropDownList ID="tbLinkMan" runat="server" Width="100px"></asp:DropDownList>  
    调度：<asp:DropDownList ID="tbSigner" runat="server" Width="100px"></asp:DropDownList>　

    <!--新加了两个查询条件-->
    源工程名称：<asp:TextBox ID="tbProjectName" Width="100" runat="server"></asp:TextBox>
    源砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>


    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>

    <rsweb:ReportViewer ID="ReportViewer1" Width="100%"  Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt" AsyncRendering="False">
    <LocalReport ReportPath="Reports\Produce\R4107051.rdlc"  DisplayName="剩退料统计报表">  
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources> 
    </LocalReport>
</rsweb:ReportViewer> 
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
        ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
        SelectCommand="sp_rpt_tzrelation_stats"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="false"  >
        <SelectParameters>  
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />   
            <asp:ControlParameter ControlID="tbLinkMan" Name="LinkMan" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tbSigner" Name="Signer" PropertyName="Text" />   
            
            <asp:ControlParameter ControlID="tbProjectName" Name="projectname" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />            　
        </SelectParameters>
    </asp:SqlDataSource> 

    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select '' UserID, '' TrueName union SELECT UserID, TrueName FROM Users WHERE UserType='51' AND IsUsed=1 ORDER BY TrueName"
        SelectCommandType="Text" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select '' UserID, '' TrueName union SELECT UserID, TrueName FROM Users WHERE UserType='02' AND IsUsed=1 ORDER BY TrueName"
        SelectCommandType="Text" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>

</asp:Content>
