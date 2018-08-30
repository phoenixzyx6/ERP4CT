<%@ Page Title="混凝土开盘鉴定记录" Language="C#" MasterPageFile="~/Reports/ReportsBase.Master" Inherits="ZLERP.Web.Reports.ReportBase"
AutoEventWireup="true"  %>
<script runat="server" language="c#">
    protected void Page_Load(object sender, EventArgs e)
    {
        refresh();
        if (Page.IsPostBack)
        {
            this.ReportViewer1.LocalReport.Refresh();
        }
        else
        {
            if (Request["uid"] != null)
            {
                LocalReport rpt = (LocalReport)this.ReportViewer1.LocalReport;
                this.tbID.Text = Request["uid"].ToString();
                rpt.SetParameters(new ReportParameter("OP_ID", this.tbID.Text));
                //设置EnterpriseName参数为全局配置中的EnterpriseName
                var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
                if (entCfg != null)
                {
                    rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
                }
                this.ReportViewer1.LocalReport.Refresh();
            }
        }
       
    }



    ZLERP.Business.PublicService s = null;
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.tbID.Text.Length > 0)
        {
            this.ReportViewer1.LocalReport.Refresh();
            LocalReport rpt = this.ReportViewer1.LocalReport;
            rpt.SetParameters(new ReportParameter("OP_ID", this.tbID.Text));
            //设置EnterpriseName参数为全局配置中的EnterpriseName
            var entCfg = SysConfigs.Where(p => p.ConfigName == "EnterpriseName").FirstOrDefault();
            if (entCfg != null)
            {
                rpt.SetParameters(new ReportParameter("EnterpriseName", entCfg.ConfigValue));
            }


            //获得生产线
            if (s == null)
                s = new ZLERP.Business.PublicService();
            ZLERP.Model.ShippingDocument shipdoc = s.ShippingDocument.Get(this.tbID.Text);
            if (shipdoc != null)
            {
                string productlineID = shipdoc.ProductLineID;
                //获得生产线筒仓
                ZLERP.Model.ProductLine p = s.GetGenericService<ZLERP.Model.ProductLine>().Get(productlineID);
                IList<ZLERP.Model.SiloProductLine> siloList = p.SiloProductLines.Where(pp => pp.Rate > 0).ToList();
                int num = siloList.Count;//也是用料数
                if (num > 0)
                {
                    if (13 - num > 0)
                    {
                        System.Data.DataTable t = ((System.Data.DataView)SqlDataSource1.Select(new DataSourceSelectArguments())).Table;
                        if (t.Rows.Count == 0)
                            return;
                        //缺少
                        int q = 13 - num;
                        //盘数
                        //int time = t.Rows.Count / num;

                        System.Data.DataRow r;
                        //缺少列条数
                        //生成每盘数
                        object[] objs = t.Rows[0].ItemArray;
                        for (int j = 0; j < q; j++)
                        {
                            //循环数据
                            r = t.NewRow();
                            for (int k = 0; k < objs.Length; k++)
                            {
                                r[k] = objs[k];
                            }
                            r["pottimes"] = j + 1;
                            r["siloname"] = DBNull.Value; ;
                            r["stuffname"] = string.Format("<NULL{0}>", j);
                            r["FinalStuffType"] = "CC" + j.ToString();
                            r["ActualAmount"] = DBNull.Value;
                            r["TheoreticalAmount"] = DBNull.Value;
                            r["Amount"] = DBNull.Value;
                            r["StuffAmount"] = DBNull.Value;
                            r["PerAmount"] = DBNull.Value;
                            r["SupplyName"] = DBNull.Value;
                            r["Sizex"] = DBNull.Value;
                            t.Rows.Add(r);
                        }


                        ReportDataSource reportDataSource = new ReportDataSource();
                        reportDataSource.Value = t;
                        reportDataSource.Name = "DataSet1";
                        ReportViewer1.LocalReport.DataSources[0] = reportDataSource;
                        this.ReportViewer1.LocalReport.Refresh();
                    }
                }
            }
        }
        
        
        

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
        发货单号
        <asp:TextBox ID="tbID" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="查询" 
            Width="57px" />
    <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="500px" runat="server" Font-Names="Verdana" 
            Font-Size="9pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="12pt" SizeToReportContent="True">
        <LocalReport ReportPath="Reports\QCC\R710843.rdlc"  DisplayName="混凝土开盘鉴定记录">  
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources> 
        </LocalReport>
</rsweb:ReportViewer> 
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:ZLERP %>" 
           ProviderName="<%$ ConnectionStrings:ZLERP.ProviderName %>" 
           SelectCommand="sp_rpt_ProducerecordDetail"  SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="True"  >
           <SelectParameters> 
                 <asp:ControlParameter ControlID="tbID" Name="ShipDocID" PropertyName="Text" /> 
           </SelectParameters>
       </asp:SqlDataSource>       
</asp:Content>
