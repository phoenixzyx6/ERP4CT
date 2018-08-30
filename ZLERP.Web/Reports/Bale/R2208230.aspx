<%@ Page Title="材料盘点表" Language="C#"   MasterPageFile="~/Reports/ReportsBase.Master"
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
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("Month1",Convert.ToInt32(this.tbDate.Text.Substring(4, 2)).ToString()));
        string tbBeginDate = "";
        string tbEndDate = "";
        var Cfg_ChangeDay = SysConfigs.Where(p => p.ConfigName == "ChangeDay").FirstOrDefault();
        if (Cfg_ChangeDay != null)
        {
            DateTime changeDay = DateTime.Parse(tbDate.Text.Substring(0, 4) + "-" + tbDate.Text.Substring(4, 2) + "-" + Cfg_ChangeDay.ConfigValue + " 00:00:00");
            tbBeginDate = changeDay.ToString("yyyy-MM-dd");
            tbEndDate = changeDay.AddMonths(-1).ToString("yyyy-MM-dd");
        }

        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("startdate", tbEndDate));
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("enddate", tbBeginDate));
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server"> 
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $.datepicker.setDefaults({dateFormat:'yymm'});
        $('#<%=tbDate.ClientID %>').datepicker();

    }); 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
   报表日期：<asp:TextBox ID="tbDate" Text="201203"  runat="server"></asp:TextBox>  

      

     <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" /> <span class="red">按结算日和交班时间统计（包含上月结算日，不包含本月结算日）</span>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="800px" runat="server" Font-Names="Verdana"   AsyncRendering="false"  Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Bale\R2208230.rdlc"  DisplayName="材料盘点表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_StuffCheck2"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters> 
                <asp:ControlParameter ControlID="tbDate" Name="Month" PropertyName="Text" /> 

           </SelectParameters>
       </asp:SqlDataSource>


</asp:Content>
