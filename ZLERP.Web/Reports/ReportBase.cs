using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZLERP.Web.Helpers;
using ZLERP.Business;
using ZLERP.Model;
using System.Data.SqlClient;
using System.Data;

namespace ZLERP.Web.Reports
{
    public class ReportBase : System.Web.UI.Page
    {
        public IList<SysConfig> SysConfigs = GetSysConfig();
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);
        //    SysConfigs = GetSysConfig();
        //}
        static IList<SysConfig> GetSysConfig()
        {
            PublicService ps = new PublicService();
            return ps.SysConfig.GetAllSysConfigs();

        }

        
             
    }
}