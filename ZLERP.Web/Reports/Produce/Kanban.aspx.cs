using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLERP.Web.Helpers;
using System.Data;
using System.Data.SqlClient;
using ZLERP.Model;
using ZLERP.Business;

namespace ZLERP.Web.Reports.Produce
{
    public partial class Kanban : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                Session["shipDocs"] = null; 
                BindTodayProduceTasks();
            }
        }

        void BindTodayProduceTasks()
        {
            SqlServerHelper helper = new SqlServerHelper();
            DataSet ds = helper.ExecuteDataset("sp_kanban_producetasks", System.Data.CommandType.StoredProcedure, null);
            
            rptTasks.DataSource = ds;
            rptTasks.DataBind();
            if (ds != null && ds.Tables.Count > 0)
            {
                lblTotalTaskCount.Text = string.Format("{0}", ds.Tables[0].Rows.Count);
                decimal ParCubes = 0;
                decimal PlanCubes = 0;
                int RunningCar = 0;
                int DDCar = 0;
                int dPlanTaskCount = 0;
                int dUnOpenCount = 0;
                int dCompletedCount = 0;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ParCubes += Convert.ToDecimal(dr["ParCubes"]);
                }
                if (dt.Rows.Count > 0)
                {
                    PlanCubes = Convert.ToDecimal(dt.Rows[0]["PlanCubes"]);
                    RunningCar = Convert.ToInt32(dt.Rows[0]["RunningCar"]);
                    DDCar = Convert.ToInt32(dt.Rows[0]["DDCar"]); 
                    dPlanTaskCount = Convert.ToInt32(dt.Rows[0]["PlanTaskCount"]);
                    dUnOpenCount = Convert.ToInt32(dt.Rows[0]["UnOpenCount"]);
                    dCompletedCount = Convert.ToInt32(dt.Rows[0]["CompletedCount"]);
                }
                if (dPlanTaskCount > 0) {
                    //显示计划任务数
                    this.lblTotalTaskCount.Text = dPlanTaskCount.ToString();
                }
                this.lbUnOpenCount.Text = dUnOpenCount.ToString();
                this.lbCompletedCount.Text = dCompletedCount.ToString();
                this.lbTodayParCube.Text = ParCubes.ToString();
                this.lbTodayPlanCube.Text = PlanCubes.ToString();
                this.lbRunningCar.Text = RunningCar.ToString();
                this.lbDDCar.Text = DDCar.ToString();

                //今日计划方量采用系统配置中手动设置的方量
                //modified by:Sky
                PublicService ps = new PublicService();
                var planCube = ps.SysConfig.GetAllSysConfigs().Where(p => p.ConfigName == "TodayPlanAmount").FirstOrDefault();
                decimal dSetPlanCube;
                //系统设置中方量设置不为0的数字时显示系统设置中的方量
                //给有的站不使用工地计划备用
                if (planCube != null &&  Decimal.TryParse(planCube.ConfigValue, out dSetPlanCube) && dSetPlanCube > 0)
                {
                    this.lbTodayPlanCube.Text = dSetPlanCube.ToString();
                }
            }
        }

        public IList<ShippingDocument> BindTaskShippingDocs(string taskId) {
            IList<ShippingDocument> docs = GetShippingDocs();
            return docs.Where(p => p.TaskID == taskId).OrderBy(p=>p.BuildTime).ToList();
        }
        IList<ShippingDocument> GetShippingDocs()
        {
            if (Session["shipDocs"] == null)
            {
                SqlServerHelper helper = new SqlServerHelper();
                IList<ShippingDocument> docs = new List<ShippingDocument>();
                using (SqlDataReader sdr = helper.ExecuteReader("sp_kanban_shippingdocs", System.Data.CommandType.StoredProcedure, null))
                {
                    while (sdr.Read())
                    {
                        ShippingDocument d = new ShippingDocument();
                        d.TaskID = sdr.GetString(0);
                        d.CarID = sdr.IsDBNull(1) ? "" : sdr.GetString(1);
                        d.ParCube = sdr.IsDBNull(2) ? 0 : sdr.GetDecimal(2);
                        d.ProvidedCube = sdr.IsDBNull(3) ? 0 : sdr.GetDecimal(3);
                        d.ProvidedTimes = sdr.IsDBNull(4) ? 0 : sdr.GetInt32(4);
                        d.BuildTime = sdr.GetDateTime(5);
                        d.SlurryCount = sdr.IsDBNull(6) ? 0 : sdr.GetDecimal(6);
                        d.PumpName = sdr.IsDBNull(7) ? "" : sdr.GetString(7);
                        d.ShipDocType = sdr.IsDBNull(8) ? "" : sdr.GetString(8);
                        d.ArriveTime = sdr.IsDBNull(9) ? (DateTime?)null : sdr.GetDateTime(9);
                        //暂时用质检员代替时量

                        docs.Add(d);
                    }
                }
                Session["shipDocs"] = docs;
                
                return docs;
            }
            else
            {
                return (IList<ShippingDocument>)Session["shipDocs"];
            }

        }
    }
}