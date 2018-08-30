<%@ Page Title="单车生产明细表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
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

            if (Request["uid"] != null)
            {
                this.tbShipDocID.Text = Request["uid"].ToString();
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("ShipDocID", this.tbShipDocID.Text));
                this.ReportViewer1.LocalReport.Refresh();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbShipDocID.Text.Length > 0)
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("ShipDocID", this.tbShipDocID.Text));
            this.ReportViewer1.LocalReport.Refresh();
        }
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
        运输单号<asp:TextBox ID="tbShipDocID" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" 
            Width="57px" />
        <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410791.rdlc"  DisplayName="单车生产明细表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_ProducerecordDetail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="tbShipDocID" Name="ShipDocID" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="SELECT ShipDocID,ProduceDate FROM dbo.ShippingDocument WHERE IsEffective=1 ORDER BY  ProduceDate DESC "  SelectCommandType="Text" CancelSelectOnNullParameter="False"  >
       </asp:SqlDataSource>
</asp:Content>
