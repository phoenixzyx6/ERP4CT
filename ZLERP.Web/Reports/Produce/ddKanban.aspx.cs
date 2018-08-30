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
    public partial class ddKanban : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["shipDocs"] = null;
                string plid = Request["plid"];
                BindPLShipDoc(plid);
            }
        }

        void BindPLShipDoc(string plid)
        {
            SqlServerHelper helper = new SqlServerHelper();
            SqlParameter[] paras = new SqlParameter[] { new SqlParameter("@PLID", plid) };
            DataSet ds = helper.ExecuteDataset("sp_dd_kanban", System.Data.CommandType.StoredProcedure, paras);
            rptShipDocs.DataSource = ds;
            rptShipDocs.DataBind();

        }        
    }
}