<%@ Page Title="销售合同结算" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master"
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true" %>

<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["uid"] != null)
            {
                LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;
                this.tbID.Text = Request["uid"].ToString();
                if (Request["jstime"] != null && Request["jstime"].ToString().Trim()!="")
                {
                    this.beginTime.Text = Request["jstime"].ToString();
                }
                else
                {
                    this.beginTime.Text = "1991-01-01";
                }
                //this.beginTime.Enabled = false;
                typeid.Text = "1";
                this.ReportViewer1.LocalReport.Refresh();
            }
        }

    }



    ZLERP.Business.PublicService s = null;
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbID.Text.Length > 0)
        {

            LocalReport rpt = this.ReportViewer1.LocalReport;
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (entCfg != null)
            {
                rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
            }
            if (s == null)
            {
                s = new ZLERP.Business.PublicService();
            }
            ZLERP.Model.Contract contract = s.Contract.Get(this.tbID.Text);
            rpt.SetParameters(new ReportParameter("ContractName", contract.ContractName));
            rpt.SetParameters(new ReportParameter("CustName", contract.CustName));
            rpt.SetParameters(new ReportParameter("begindate", this.beginTime.Text));
            rpt.SetParameters(new ReportParameter("enddate", this.endTime.Text));
            if (typeid.Text == "1")
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\Sales\R311401.rdlc";
            }
            else
            {
                this.ReportViewer1.LocalReport.ReportPath = @"Reports\Sales\R311401T.rdlc";
            }
            this.ReportViewer1.LocalReport.Refresh();

        }

    }
    protected void dr1_SelectedIndexChanged(object sender, EventArgs e)
    {
        typeid.Text = this.dr1.SelectedValue;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (this.endTime.Text != "" && this.beginTime.Text != "")
        {
            if (s == null)
            {
                s = new ZLERP.Business.PublicService();
            }
            ZLERP.Model.Contract contract = s.Contract.Get(this.tbID.Text);
            DateTime ET = Convert.ToDateTime(this.endTime.Text);
            DateTime BT = Convert.ToDateTime(this.beginTime.Text);
            if (ET > BT)
            {
                s.GetGenericService<ZLERP.Model.ContractJS>().Add(new ZLERP.Model.ContractJS() { ContractID = contract.ID, BSMoney = Convert.ToInt32(this.TextBox_bs.Text), JSDate = ET,StartJSDate=BT, JSMoney = Convert.ToDecimal(this.TextBox_js.Text), Remark = this.TextBox_remark.Text,Type=dr2.SelectedValue });
                if (dr2.SelectedValue=="结算")
                {
                    contract.FootDate = ET;
                    contract.TotalJSMoney = contract.TotalJSMoney + Convert.ToDecimal(this.TextBox_js.Text);
                    s.GetGenericService<ZLERP.Model.Contract>().Update(contract);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('结算成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('结束时间不得小于开始时间！');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('结算失败！');", true);
        }
    } 
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=beginTime.ClientID %>').datepicker();
            $('#<%=endTime.ClientID %>').datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    合同号
    <asp:TextBox ID="tbID" runat="server" ReadOnly="True"></asp:TextBox>
    结算起始日期<asp:TextBox ID="beginTime" runat="server"></asp:TextBox>
    结算办理日期<asp:TextBox ID="endTime" runat="server"></asp:TextBox>
    <asp:DropDownList ID='dr1' runat="server" OnSelectedIndexChanged="dr1_SelectedIndexChanged">
        <asp:ListItem Text="有运费" Value="1"></asp:ListItem>
        <asp:ListItem Text="无运费" Value="2"></asp:ListItem>
    </asp:DropDownList>
    <div style="display: none">
        <asp:TextBox runat="server" ID="typeid"></asp:TextBox>
    </div>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查询" Width="57px" />
    <br />
    结算金额<asp:TextBox ID="TextBox_js" runat="server"></asp:TextBox>
    推迟结算(月)<asp:TextBox ID="TextBox_bs" runat="server"></asp:TextBox>
    备注<asp:TextBox ID="TextBox_remark" runat="server"></asp:TextBox>
    <asp:DropDownList ID='dr2' runat="server">
        <asp:ListItem Text="结算" Value="结算"></asp:ListItem>
        <asp:ListItem Text="产值" Value="产值"></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="Button2" runat="server" Text="确认结算" Width="73px" OnClick="Button2_Click" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server"
        Font-Names="Verdana" Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\Sales\R311401.rdlc" DisplayName="销售合同结算">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ERPReports %>"
        ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="sp_rpt_HNTJS"
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbID" Name="ID" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="endTime" Name="EndDate" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="beginTime" Name="BeginDate" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="dr1" Name="type" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
