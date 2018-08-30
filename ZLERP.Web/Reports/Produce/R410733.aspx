<%@ Page Title="发货单小票排查报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    AutoEventWireup="true" %>

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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(tbBeginTicketNO.Text) || string.IsNullOrEmpty(tbEndTicketNO.Text))
        {
            lblMsg.Text = "票号范围不能为空";
            this.ReportViewer1.Visible = false;
            return;
        }
        else if (tbBeginTicketNO.Text.Length != tbEndTicketNO.Text.Length)
        {
            lblMsg.Text = "票号位数不一致";
            this.ReportViewer1.Visible = false;
            return;
        }
        else
        {
            lblMsg.Text = "";
            this.ReportViewer1.Visible = true;
        }
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginTicketNO", tbBeginTicketNO.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndTicketNO", tbEndTicketNO.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    小票号从<asp:TextBox ID="tbBeginTicketNO" Width="135" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
        ControlToValidate="tbBeginTicketNO" Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    至
    <asp:TextBox ID="tbEndTicketNO" runat="server" Width="135"></asp:TextBox><asp:RequiredFieldValidator
        ID="RequiredFieldValidator2" runat="server" CssClass="red" ControlToValidate="tbEndTicketNO"
        Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410733.rdlc" DisplayName="发货单小票排查报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_ship_ticket"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginTicketNO" Name="BeginTicketNO" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndTicketNO" Name="EndTicketNO" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
