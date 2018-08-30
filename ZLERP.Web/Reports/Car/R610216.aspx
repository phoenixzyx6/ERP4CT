<%@ Page Title="罐车驾驶员计件工资表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            //获取全局配置中的ChangeTime
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (entCfg != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
            }
            this.tbPricePlan.DataSource = this.SqlDataSource2;
            this.tbPricePlan.DataTextField = "Name";
            this.tbPricePlan.DataValueField = "CarTimesRewardMainId";
            this.tbPricePlan.DataBind();
        }
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.Refresh();
        LocalReport rpt = this.ReportViewer1.LocalReport;
        rpt.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text.Trim()));
        rpt.SetParameters(new ReportParameter("EndDate", tbEndDate.Text.Trim()));

        RunProReport();
    }
    private void RunProReport()
    {
        //取得数据集
        string connstring = ConfigurationManager.ConnectionStrings["ERPReports"].ConnectionString;
        System.Data.SqlClient.SqlConnection conn1 = new System.Data.SqlClient.SqlConnection(connstring);
        conn1.Open();
        System.Data.SqlClient.SqlCommand command1 = new System.Data.SqlClient.SqlCommand("sp_rpt_CarDriverWages", conn1);
        command1.CommandTimeout = 180;
        System.Data.SqlClient.SqlDataAdapter ada1 = new System.Data.SqlClient.SqlDataAdapter(command1);
        //把Command执行类型改为存储过程方式，默认为Text。 
        command1.CommandType = System.Data.CommandType.StoredProcedure;
        command1.Parameters.Add("@PricePlan", System.Data.SqlDbType.VarChar, 60).Value = tbPricePlan.SelectedValue.ToString();
        command1.Parameters.Add("@BeginDate", System.Data.SqlDbType.VarChar, 60).Value = tbBeginDate.Text;
        command1.Parameters.Add("@EndDate", System.Data.SqlDbType.VarChar, 60).Value = tbEndDate.Text;
        command1.ExecuteNonQuery();
        System.Data.DataSet c_ds = new System.Data.DataSet();
        try
        {
            ada1.Fill(c_ds);
        }
        finally
        {
            conn1.Close();
            command1.Dispose();
            conn1.Dispose();
        }
        //为报表浏览器指定报表文件
        this.ReportViewer1.LocalReport.ReportEmbeddedResource = @"Reports\Car\R610216.rdlc";
        this.ReportViewer1.LocalReport.ReportPath = @"Reports\Car\R610216.rdlc";

        //指定数据集,数据集名称后为表,不是DataSet类型的数据集
        this.ReportViewer1.LocalReport.DataSources.Clear();
        this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", c_ds.Tables[0]));
        //显示报表
        this.ReportViewer1.LocalReport.Refresh();
    }
    public void refresh()
    {
        string CurrentYear = Session["YearAccount"] == null ? "ZLERP" : Session["YearAccount"].ToString();
        this.SqlDataSource1.ConnectionString = this.SqlDataSource1.ConnectionString.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.ProviderName = this.SqlDataSource1.ProviderName.Replace("ZLERP", CurrentYear);
        this.SqlDataSource1.DataBind();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //$('#<%=tbBeginDate.ClientID %>').datetimepicker({ hour: 8 });
            //$('#<%=tbEndDate.ClientID %>').datetimepicker({ hour: 8 });

            var dates = $("#<%=tbBeginDate.ClientID %>, #<%=tbEndDate.ClientID %>").datetimepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbBeginDate.ClientID%>" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                        dates.not(this).datepicker("option", option, date);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>     
    起始时间：<asp:TextBox ID="tbBeginDate" runat="server"></asp:TextBox>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox>
    计件工资方案：<asp:DropDownList ID="tbPricePlan" runat="server" Width="100px"></asp:DropDownList> 
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" AsyncRendering="False" PageCountMode="Actual">
        <LocalReport ReportPath="Reports\Car\R610216.rdlc"  DisplayName="罐车驾驶员计件工资表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_CarDriverWages"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />
            <asp:ControlParameter ControlID="tbPricePlan" Name="PricePlan" PropertyName="Text" />
        </SelectParameters>
       </asp:SqlDataSource> 
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="SELECT CarTimesRewardMainId,Name from CarTimesRewardMain ORDER BY CarTimesRewardMainId "
        SelectCommandType="Text" CancelSelectOnNullParameter="False">
    </asp:SqlDataSource>
</asp:Content>
