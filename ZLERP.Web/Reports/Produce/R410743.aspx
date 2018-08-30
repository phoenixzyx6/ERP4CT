<%@ Page Title="值班计划报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true"  %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {            
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }

            ////获取全局配置中的ChangeTime
            //var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            //if (Cfg_ChangeTime != null)
            //{

            //    tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            //    tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            //}
            //else
            //{
            //    tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd 00:00");
            //    tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd 00:00");
            //}

            var Cfg_ChangeDay = SysConfigs.Where(p => p.ConfigName == "ChangeDay").FirstOrDefault();
            if (Cfg_ChangeDay != null)
            {
                DateTime changeDay = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-") + Cfg_ChangeDay.ConfigValue + " 00:00:00").AddDays(1);
                if (DateTime.Now > changeDay)
                {
                    tbBeginDate.Text = changeDay.ToString("yyyy-MM-dd");
                    tbEndDate.Text = changeDay.AddMonths(1).ToString("yyyy-MM-dd");
                }
                else
                {
                    tbBeginDate.Text = changeDay.AddMonths(-1).ToString("yyyy-MM-dd");
                    tbEndDate.Text = changeDay.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-01");
                tbEndDate.Text = DateTime.Today.AddMonths(1).ToString("yyyy-MM-01");
            }
            
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));

            this.ReportViewer1.LocalReport.DisplayName = DateTime.Parse(tbBeginDate.Text).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(tbEndDate.Text).ToString("yyyy-MM-dd") + "值班计划报表";
            
            this.ReportViewer1.LocalReport.Refresh();
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e) 
    {
        if (!string.IsNullOrEmpty(tbBeginDate.Text) && !string.IsNullOrEmpty(tbEndDate.Text) && string.Compare(tbBeginDate.Text, tbEndDate.Text) > 0)
        {
            lblMsg.Text = "时间范围错误";
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

        this.ReportViewer1.LocalReport.DisplayName = DateTime.Parse(tbBeginDate.Text).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(tbEndDate.Text).ToString("yyyy-MM-dd") + "值班计划报表";
        
        this.ReportViewer1.LocalReport.Refresh();
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=tbBeginDate.ClientID %>').datepicker();
        $('#<%=tbEndDate.ClientID %>').datepicker();
    }); 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>     
    起始时间：<asp:TextBox ID="tbBeginDate" Width="135" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="red"
        ControlToValidate="tbBeginDate" Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    截止时间：<asp:TextBox ID="tbEndDate" runat="server" Width="135"></asp:TextBox>
    <asp:RequiredFieldValidator
        ID="RequiredFieldValidator2" runat="server" CssClass="red" ControlToValidate="tbEndDate"
        Display="Dynamic" ErrorMessage="*">
    </asp:RequiredFieldValidator>
    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Produce\R410743.rdlc"  DisplayName="值班计划报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
    ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
    SelectCommand="sp_rpt_DutyPlan"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
    <SelectParameters>
        <asp:ControlParameter ControlID="tbBeginDate" Name="BeginDate"  PropertyName="Text" />
        <asp:ControlParameter ControlID="tbEndDate" Name="EndDate"   PropertyName="Text" />
    </SelectParameters>
</asp:SqlDataSource> 
</asp:Content>
