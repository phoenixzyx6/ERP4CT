<%@ Page Title="材料消耗日报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
            
        }
        else
        {
            tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.DropDownList1.DataSource = this.SqlDataSource2;
            this.DropDownList1.DataTextField = "ProductLineName";
            this.DropDownList1.DataValueField = "ProductLineID";
            this.DropDownList1.DataBind();
            this.ReportViewer1.LocalReport.Refresh();
        }
        this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;
        rpt.SetParameters(new ReportParameter("ProductLineName", this.DropDownList1.SelectedItem.Text.ToString()));
        rpt.SetParameters(new ReportParameter("SearchDate", this.tbDate.Text));
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;
        rpt.SetParameters(new ReportParameter("ProductLineName", this.DropDownList1.SelectedItem.Text.ToString()));
        rpt.SetParameters(new ReportParameter("SearchDate", this.tbDate.Text));
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=tbDate.ClientID %>').datepicker();

    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     生产线：<asp:DropDownList ID="DropDownList1" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    日期：<asp:TextBox ID="tbDate" runat="server"></asp:TextBox> 
    <asp:Button ID="Button1" runat="server" Text="查询" onclick="Button1_Click" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Bale\R220804.rdlc"  DisplayName="材料消耗日报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_dd_stuff"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                <asp:ControlParameter ControlID="tbDate" Name="Date" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="DropDownList1" Name="ProductLineID" PropertyName="Text" />
           </SelectParameters>
       </asp:SqlDataSource> 
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="select ProductLineID,ProductLineName from ProductLine where IsUsed=1"  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>

</asp:Content>
