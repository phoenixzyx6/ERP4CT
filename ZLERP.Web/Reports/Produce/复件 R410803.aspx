    <%@ Page Title="材料购销存统计" Language="C#"   MasterPageFile="~/Reports/ReportsBase.Master"
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true" %>
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        { 
            tbDate.Text = DateTime.Today.ToString("yyyyMM");
            this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月");

            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }

            tbStartDate.Text = DateTime.Now.ToString("yyyy-MM")+"-01 00:00:00";
            tbEndDate.Text = DateTime.Now.ToString();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("Month1",Convert.ToInt32(this.tbDate.Text.Substring(4, 2)).ToString()));
        //string tbBeginDate = "";
        //string tbEndDate = "";
        //var Cfg_ChangeDay = SysConfigs.Where(p => p.ConfigName == "ChangeDay").FirstOrDefault();
        //if (Cfg_ChangeDay != null)
        //{
        //    DateTime changeDay = DateTime.Parse(tbDate.Text.Substring(0, 4) + "-" + tbDate.Text.Substring(4, 2) + "-" + Cfg_ChangeDay.ConfigValue + " 00:00:00");
        //    tbBeginDate = changeDay.ToString("yyyy-MM-dd");
        //    tbEndDate = changeDay.AddMonths(-1).ToString("yyyy-MM-dd");
        //}

        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("startdate", tbStartDate.Text));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("enddate", tbEndDate.Text));
        
        RunProReport();
        
    }
    private void RunProReport()
    {
        //取得数据集
        string connstring = ConfigurationManager.ConnectionStrings["ERPReports"].ConnectionString;
        System.Data.SqlClient.SqlConnection conn1 = new System.Data.SqlClient.SqlConnection(connstring);
        conn1.Open();
        System.Data.SqlClient.SqlCommand command1 = new System.Data.SqlClient.SqlCommand("sp_rpt_Stuffinfo_ZF", conn1);
        command1.CommandTimeout = 180;
        System.Data.SqlClient.SqlDataAdapter ada1 = new System.Data.SqlClient.SqlDataAdapter(command1);
        //把Command执行类型改为存储过程方式，默认为Text。 
        command1.CommandType = System.Data.CommandType.StoredProcedure;
        command1.Parameters.Add("@Month", System.Data.SqlDbType.VarChar, 60).Value = "201001";
        command1.Parameters.Add("@StartDate", System.Data.SqlDbType.VarChar, 60).Value = tbStartDate.Text;
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
        this.ReportViewer1.LocalReport.ReportEmbeddedResource = @"Reports\Produce\R410803.rdlc";
        this.ReportViewer1.LocalReport.ReportPath = @"Reports\Produce\R410803.rdlc";

        //指定数据集,数据集名称后为表,不是DataSet类型的数据集
        this.ReportViewer1.LocalReport.DataSources.Clear();
        this.ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", c_ds.Tables[0]));
        //显示报表
        this.ReportViewer1.LocalReport.Refresh();
    }
   </script>
   <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server"> 
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //$.datepicker.setDefaults({dateFormat:'yymm'});
            $('#<%=tbDate.ClientID %>').datepicker();

            var dates = $("#<%=tbStartDate.ClientID %>, #<%=tbEndDate.ClientID %>").datetimepicker({
                defaultDate: "+1w",
                changeMonth: true,
                numberOfMonths: 2,
                onSelect: function (selectedDate) {
                    var option = this.id == "<%=tbStartDate.ClientID %>" ? "minDate" : "maxDate",
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
       <div style="display:none;">报表日期：<asp:TextBox ID="tbDate" Text="201203"  runat="server"></asp:TextBox>  </div>  
       报表时间：<asp:TextBox ID="tbStartDate" Text=""  runat="server"></asp:TextBox>
       到<asp:TextBox ID="tbEndDate" Text=""  runat="server"></asp:TextBox>
     <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" /> 
     <%--<span class="red">按结算日和交班时间统计（包含上月结算日，不包含本月结算日）</span>--%>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="800px" runat="server" Font-Names="Verdana"   AsyncRendering="false"  Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410803.rdlc"  DisplayName="材料购销存统计">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
    </rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_Stuffinfo_ZF"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="tbDate" Name="Month" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbStartDate" Name="StartDate" PropertyName="Text" /> 
                <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>


    </asp:Content>
