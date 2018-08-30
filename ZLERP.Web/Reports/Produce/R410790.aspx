<%@ Page Title="拌台与调度方量差异查询" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
            tbBeginDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbProductLineID.DataSource = this.SqlDataSource2;
            this.tbProductLineID.DataTextField = "ProductLineName";
            this.tbProductLineID.DataValueField = "ProductLineID";
            this.tbProductLineID.DataBind();
        }
    }
    
    void btnQuery_Click(object sender, EventArgs e)
    {
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
        //设置传递给报表的参数
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("ProductLineName", tbProductLineID.SelectedItem.Text));
        hfOnlyDiff.Value =  cbOnlyDiff.Checked ? "1" : "0";
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $.datepicker.setDefaults({ dateFormat: 'yy-mm-dd' });
        var Date_Length = 10;
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
     <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    选择日期：从<asp:TextBox ID="tbBeginDate" CssClass="short" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red" ControlToValidate="tbBeginDate" Display="Dynamic" ErrorMessage="*">
            </asp:RequiredFieldValidator> 到
            <asp:TextBox ID="tbEndDate" CssClass="short" runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="red" ControlToValidate="tbEndDate" Display="Dynamic" ErrorMessage="*">
            </asp:RequiredFieldValidator>
    生产线：<asp:DropDownList ID="tbProductLineID" runat="server" Width="150px">
    </asp:DropDownList>
    <asp:CheckBox ID="cbOnlyDiff"  runat="server" Checked="false"></asp:CheckBox ><asp:Label Text="只查差异项" runat="server"></asp:Label>
    <asp:HiddenField ID = "hfOnlyDiff" runat="server" />
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410790.rdlc"  DisplayName="拌台与调度方量差异查询">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server"   
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="ProduceCudeAndSendCudeDiff"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>  
                  <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
                  <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />
                  <asp:ControlParameter ControlID="tbProductLineID" Name="ProductLineId" PropertyName="Text" />
                  <asp:ControlParameter ControlID="hfOnlyDiff" Name="OnlyDiff" PropertyName="Value" />
           </SelectParameters>
       </asp:SqlDataSource> 
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select ''  ProductLineID, '' ProductLineName  union  select ProductLineID,ProductLineName from ProductLine  where IsUsed=1"
        SelectCommandType="Text" CancelSelectOnNullParameter="False"></asp:SqlDataSource>
</asp:Content>
