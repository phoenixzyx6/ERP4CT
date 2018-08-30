using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLERP.Business;
using ZLERP.NHibernateRepository;

namespace ZLERP.Web.help
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["page"] != null)
                {
                    string a = Request.QueryString["page"].ToString();
                    SysFuncService sysFunc = new SysFuncService(UnitOfWork.Start());
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>changeURL('" + sysFunc.Get(a).FuncName + "')</script>");
                }
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (this.TreeView1.SelectedNode.ChildNodes.Count == 0) 
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script>changeURL('" + this.TreeView1.SelectedNode.Text + "')</script>");
            }
        }
    }
}