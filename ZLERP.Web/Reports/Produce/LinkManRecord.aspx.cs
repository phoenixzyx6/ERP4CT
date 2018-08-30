using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZLERP.Business;
using ZLERP.Model;

namespace ZLERP.Web.Reports.Produce
{
    public partial class LinkManRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (AuthorizationService.CurrentUserInfo.UserType != "51")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "valid", "logonvalid()", true);
                }
                else
                {
                    BindRepeater();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (AuthorizationService.CurrentUserInfo.UserType != "51")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "valid", "logonvalid()", true);
            }
            else
            {
                ZLERP.Model.LinkManRecord linkManRecord = new ZLERP.Model.LinkManRecord();
                linkManRecord.Content = this.Content.Text.Trim();
                PublicService ps = new PublicService();
                ps.GetGenericService<ZLERP.Model.LinkManRecord>().Add(linkManRecord);
                BindRepeater();
                ClientScript.RegisterStartupScript(this.GetType(), "success", "savesuccess()", true);
            }
        }

        protected void BindRepeater()
        {
            PublicService ps = new PublicService();
            IList<ZLERP.Model.LinkManRecord> list = ps.GetGenericService<ZLERP.Model.LinkManRecord>().Find(string.Format("Builder = '{0}'", AuthorizationService.CurrentUserInfo.ID), 1, 5, "BuildTime", "desc");
            Repeater1.DataSource = list;
            Repeater1.DataBind();
        }
    }
}