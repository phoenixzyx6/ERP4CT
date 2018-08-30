<%@ Page Title="水票统计报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
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
            this.tbCarID.DataSource = this.SqlDataSource2;
            this.tbCarID.DataTextField = "CarID";
            this.tbCarID.DataValueField = "CarID";
            this.tbCarID.DataBind();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(tbStartDateTime.Text) && !string.IsNullOrEmpty(tbEndDateTime.Text) && string.Compare(tbStartDateTime.Text, tbEndDateTime.Text) > 0)
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
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("StartDateTime", tbStartDateTime.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDateTime", tbEndDateTime.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=tbStartDateTime.ClientID %>').datetimepicker();
            $('#<%=tbEndDateTime.ClientID %>').datetimepicker();
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    日期从<asp:TextBox ID="tbStartDateTime" Width="135" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
        ControlToValidate="tbStartDateTime" Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    至
    <asp:TextBox ID="tbEndDateTime" runat="server" Width="135"></asp:TextBox><asp:RequiredFieldValidator
        ID="RequiredFieldValidator2" runat="server" CssClass="red" ControlToValidate="tbEndDateTime"
        Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    车号：<asp:DropDownList ID="tbCarID" runat="server" Width="150px">
    </asp:DropDownList>

    <!--新加了两个查询条件-->
    工程名称：<asp:TextBox ID="tbProjectName" Width="100" runat="server"></asp:TextBox>
    砼强度：<asp:TextBox ID="tbConStrength" Width="100" runat="server"></asp:TextBox>


    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410730.rdlc" DisplayName="水票统计报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_waterdoc"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbStartDateTime" Name="StartDateTime" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDateTime" Name="EndDateTime" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbCarID" Name="CarID" PropertyName="Text" />

            <asp:ControlParameter ControlID="tbProjectName" Name="projectname1" PropertyName="Text" />
               <asp:ControlParameter ControlID="tbConStrength" Name="conStrength" PropertyName="Text" />  


        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select ''  CarID union select CarID from Car where IsUsed=1"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
</asp:Content>
