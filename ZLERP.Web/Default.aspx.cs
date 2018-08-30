using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Configuration;
using System.Web.Routing;
using System.Web.Security;
namespace ZLERP.Web
{
    

    
        public partial class _Default : Page
        {
            public void Page_Load(object sender, System.EventArgs e)
            {
               
              
                if (Request.Browser.IsMobileDevice)
                {
                    Response.RedirectToRoute("Mobile_default", new { controller = "Home", action = "Index" });
                }
                else
                {
                     Response.RedirectToRoute("Default", new { controller = "Home" , action= "Index" });
                }
             
            }
        }
    

}