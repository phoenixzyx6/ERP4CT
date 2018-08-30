<%@ Page Title="材料消耗估算" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
AutoEventWireup="true"  %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Page.IsPostBack)
        //{
        //    this.ReportViewer1.LocalReport.Refresh();
        //}
        //else
        //    tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        //this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
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
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=tbBeginDate.ClientID %>').datetimepicker({ hour: 8 });
        $('#<%=tbEndDate.ClientID %>').datetimepicker({ hour: 8 }); 
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
     
    日期从<asp:TextBox ID="tbBeginDate"  runat="server"></asp:TextBox> 至 
    <asp:TextBox ID="tbEndDate" runat="server"  ></asp:TextBox>     
    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Bale\R2231.rdlc"  DisplayName="材料消耗估算">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_CalStuffAmout"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          　<SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate"  PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate"   PropertyName="Text" />
          </SelectParameters>
       </asp:SqlDataSource> 

</asp:Content>
