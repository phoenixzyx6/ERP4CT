<%@ Page Title="材料入库暗扣报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
    AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
       
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
        
        //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
        //this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));
        LocalReport rpt = this.ReportViewer1.LocalReport;
        rpt.SetParameters(new ReportParameter("BeginDate", this.tbBeginDate.Text));
        rpt.SetParameters(new ReportParameter("EndDate", this.tbEndDate.Text));
        this.ReportViewer1.LocalReport.Refresh();
    }

    protected void ReportViewer1_Drillthrough(object sender, DrillthroughEventArgs e)
    {
        LocalReport rpt = (LocalReport)e.Report;
        //if (subDataSource1.SelectParameters.Count == 5)
        //{
        //    subDataSource1.SelectParameters.Remove(subDataSource1.SelectParameters["StuffID"]);
        //    subDataSource1.SelectParameters.Remove(subDataSource1.SelectParameters["SupplyID"]);
        //    subDataSource1.SelectParameters.Remove(subDataSource1.SelectParameters["TransportID"]);

        //}
        //subDataSource1.SelectParameters.Add("StuffID", rpt.GetParameters()["StuffID"].Values.FirstOrDefault());
        //subDataSource1.SelectParameters.Add("SupplyID", rpt.GetParameters()["SupplyID"].Values.FirstOrDefault());
        //subDataSource1.SelectParameters.Add("TransportID", rpt.GetParameters()["TransportID"].Values.FirstOrDefault());
        //rpt.DataSources.Add(new ReportDataSource("DataSet1", "subDataSource1"));
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    起始日期：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止日期：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt"　OnDrillthrough="ReportViewer1_Drillthrough">
        <LocalReport ReportPath="Reports\Bale\R220808.rdlc" DisplayName="材料入库暗扣报表">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_stuffin_darkweight"
        SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
   
</asp:Content>
