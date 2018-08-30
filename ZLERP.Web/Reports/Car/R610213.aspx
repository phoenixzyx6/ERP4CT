<%@ Page Title="车辆信息表" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>

<script runat="server" language="c#">
    
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (!Page.IsPostBack)
        {
            //设置ReportViewer高度
            if (Request.Cookies["rFrameHeight"] != null)
            {
                string strHeight = Server.HtmlEncode(Request.Cookies["rFrameHeight"].Value); ;
                this.ReportViewer1.Height = int.Parse(strHeight);
            }
            else
            {
                this.ReportViewer1.Height = 500;
            }

            this.tbCarNo.DataSource = this.SqlDataSource2;
            this.tbCarNo.DataTextField = "CarID";
            this.tbCarNo.DataValueField = "CarID";
            this.tbCarNo.DataBind();
        }
    }

    void btnQuery_Click(object sender, EventArgs e)
    {
        if (this.tbCarNo.Text == "")
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarNo", " "));
        }
        else
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("CarNo", this.tbCarNo.Text));
        }
        var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
        if (entCfg != null)
        {
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
        }
        
    }
    void btnPrint_Click(object sender, EventArgs e)
    {
        
        //var pageSettings = this.ReportViewer1.GetPageSettings();
        //pageSettings.PaperSize = new System.Drawing.Printing.PaperSize()
        //{
        //    Height = 100,
        //    Width = 50
        //};
        //pageSettings.Landscape = true;
        //this.ReportViewer1.SetPageSettings(pageSettings);

        //if (this.printDoc == null)
        //{
        //    printDoc = new ZLERP.Web.Reports.EMFStreamPrintDocument(this.ReportViewer1.LocalReport, "Test");
        //    if (this.printDoc.ErrorMessage != string.Empty)
        //    {
        //        this.printDoc = null;
        //        return;
        //    }
        //}
        //this.printDoc.Print();
        //this.printDoc = null;
        
        ZLERP.Web.Reports.PrintHelp ph = new ZLERP.Web.Reports.PrintHelp();
        ph.Run(this.ReportViewer1.LocalReport, "Microsoft XPS Document Writer", true);
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

         });
    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server"> 

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>     
    车牌号：<asp:DropDownList ID="tbCarNo" runat="server" Width="100px"></asp:DropDownList>
    <asp:Button ID="btnQuery" OnClick="btnQuery_Click" runat="server" Text="查询" />
    <asp:Button ID="btnPrint" OnClick="btnPrint_Click" runat="server" Text="打印" />

    <div id="reportdiv">
      <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="100%" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" AsyncRendering="False" PageCountMode="Actual">

        <LocalReport ReportPath="Reports\Car\R610213.rdlc"  DisplayName="车辆信息表">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>

      </rsweb:ReportViewer> 
      </div>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_CarInfo"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False"  >
           <SelectParameters>
            <asp:ControlParameter ControlID="tbCarNo" Name="CarNo" PropertyName="Text" />
        </SelectParameters>
        
       </asp:SqlDataSource> 
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ZLERP %>"
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" SelectCommand="select '' CarID union  select CarID from car where isused=1"
           SelectCommandType="Text" CancelSelectOnNullParameter="False">
        </asp:SqlDataSource>
</asp:Content>
