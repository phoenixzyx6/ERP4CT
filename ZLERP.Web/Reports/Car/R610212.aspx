<%@ Page Title="搅拌车皮重变化报表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" 
    Inherits="ZLERP.Web.Reports.ReportBase" AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //this.ReportViewer1.LocalReport.DisplayName = this.Title + DateTime.Today.ToString("yyyy年MM月dd日");
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var Cfg_EntName = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (Cfg_EntName != null)
            {
                this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", Cfg_EntName.ConfigValue));
            }

            //获取全局配置中的ChangeTime
            var Cfg_ChangeTime = SysConfigs.Where(p => p.ConfigName == "ChangeTime").FirstOrDefault();
            if (Cfg_ChangeTime != null)
            {
                tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
                tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd") + " " + Cfg_ChangeTime.ConfigValue;
            }
            else
            {
                tbBeginDate.Text = DateTime.Today.ToString("yyyy-MM-dd 00:00");
                tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd 00:00");
            }

            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("BeginDate", tbBeginDate.Text));
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EndDate", tbEndDate.Text));

            this.ReportViewer1.LocalReport.DisplayName = DateTime.Parse(tbBeginDate.Text).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(tbEndDate.Text).ToString("yyyy-MM-dd") + "搅拌车皮重变化报表";

            this.ReportViewer1.LocalReport.Refresh();
        }
    }
    void btnQuery_Click(object sender, EventArgs e)
    {
        //this.ReportViewer1.LocalReport.Refresh();
        //LocalReport rpt = this.ReportViewer1.LocalReport;
        //rpt.SetParameters(new ReportParameter("StartDate", tbBeginDate.Text.Trim()));
        //rpt.SetParameters(new ReportParameter("EndDate", tbEndDate.Text.Trim()));

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

        this.ReportViewer1.LocalReport.DisplayName = DateTime.Parse(tbBeginDate.Text).ToString("yyyy-MM-dd") + "至" + DateTime.Parse(tbEndDate.Text).ToString("yyyy-MM-dd") + "手动生产记录报表";

        this.ReportViewer1.LocalReport.Refresh();
    }
</script>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 <asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#<%=tbBeginDate.ClientID %>').datetimepicker();
        $('#<%=tbEndDate.ClientID %>').datetimepicker();

        $('div.dropdown').hover(
            function () {
                $(this).show();
            },
            function () {
                setTimeout(function () {
                    $('div.dropdown').slideUp('fast');
                }, 500);
            }
        );

        var dd = $('div.dropdown');

        $('#<%=tbCarIds.ClientID %>').bind('focus', function () {            
            var ctl = $(this);
            var left = ctl.offset().left;
            var top = ctl.offset().top + ctl.outerHeight();

            dd.attr('style', 'left:' + left + 'px; top:' + top + 'px;width:' + ctl.outerWidth() + 'px;');
            dd.slideDown();
            $('input', dd).bind('click', function () {
            var checkedCars = [];
                var cb = $(this);
                var v = ctl.val();
                var checkedVal = cb.val();
                var checked = cb.attr('checked');
                $('div.dropdown li input:checked').each(function () {
                    checkedCars.push($(this).val());
                });
                if (checkedCars.length > 0) {
                    checkedCars.sort();
                    ctl.val(checkedCars.join());
                }
                else
                    ctl.val(''); 
            });
        });

    });
</script>
<style type="text/css">
    .dropdown
    {
        background-color:#eee;
        position:absolute;
        border:1px solid #ccc;
        z-index:1000;
        }
        .dropdown ul{margin:0;padding:0;}
        .dropdown li
        {
            list-style:none;
            }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
      起始日期：<asp:TextBox ID="tbBeginDate"  runat="server"></asp:TextBox> 
 截止日期：<asp:TextBox ID="tbEndDate" runat="server"></asp:TextBox> 
 车辆：<asp:TextBox ID="tbCarIds" runat="server" AutoCompleteType="None" ></asp:TextBox> 
 <div class="dropdown" style="display:none;">
    <ul>
    <li><label><input type="checkbox"  value="01"/>01</label></li>
     <li><label><input type="checkbox"  value="02"/>02</label></li>
      <li><label><input type="checkbox"  value="03"/>03</label></li>
       <li><label><input type="checkbox"  value="04"/>04</label></li>
    </ul>
 </div>
 <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" />
 <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
 <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana"
            WaitMessageFont-Size="12pt">
        <LocalReport ReportPath="Reports\Car\R610212.rdlc"  DisplayName="搅拌车皮重变化报表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
 </rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_CarEmptyWeight"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
          <SelectParameters>
            <asp:ControlParameter ControlID="tbBeginDate" Name="StartDate" PropertyName="Text" /> 
            <asp:ControlParameter ControlID="tbEndDate" Name="EndDate" PropertyName="Text" />  
             <asp:ControlParameter ControlID="tbCarIds" Name="CarIds" PropertyName="Text" />  
          </SelectParameters>
       </asp:SqlDataSource>
</asp:Content>
