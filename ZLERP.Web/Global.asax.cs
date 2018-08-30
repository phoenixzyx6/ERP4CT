using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text;
using log4net; 
using ZLERP.NHibernateRepository;
using ZLERP.Web.Controllers.Attributes;
using System.ComponentModel.DataAnnotations;
using ZLERP.Model;
using MvcContrib;
using System.Configuration;
using MvcContrib.IncludeHandling.Configuration;
using MvcContrib.IncludeHandling;

namespace ZLERP.Web
{

    public class MvcApplication : System.Web.HttpApplication
    {
       
        ILog logger = LogManager.GetLogger(typeof(MvcApplication));
        public MvcApplication() {

            EndRequest += new EventHandler(MvcApplication_EndRequest);
            
        }

        void MvcApplication_EndRequest(object sender, EventArgs e)
        {   //on session per request.
            if (UnitOfWork.IsStarted)
                UnitOfWork.Current.Dispose();
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
            filters.Add(new UrlAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Reports/{*pathInfo}");
            routes.IgnoreRoute("help/{*pathInfo}");
            //Reports route
            routes.MapRoute(
                "Reports",
                "Report.mvc/{path}/{report}",
                new {controller="Report", action = "Index"}
                );

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}.mvc/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //    , new string[] { "ZLERP.Web.Controllers" }
            //);
            routes.MapRoute(
                "Default", // Route name
                "{controller}.mvc/{action}/{id}", // URL with parameters
                new { controller = "Account", action = "LogOn", id = UrlParameter.Optional } // Parameter defaults
                , new string[] { "ZLERP.Web.Controllers" }
            );

        }
 

        protected void Application_Start()
        {
            Application["OnLineCount"] = 0;//在应用程序第一次启动时初始化在线人数为0

            AreaRegistration.RegisterAllAreas(); 
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            //log4net配置信息
            log4net.Config.XmlConfigurator.Configure();

            var httpContextProvider = new HttpContextProvider(HttpContext.Current);
            var controllers = new[] { typeof(ZLERP.Web.Controllers.HomeController) };
            var includeHandlingSettings = (IIncludeHandlingSettings)ConfigurationManager.GetSection("includeHandling");
            DependencyResolver.SetResolver(new QnDDepResolver(httpContextProvider, includeHandlingSettings, controllers));

            //注册自定义的模型验证
            //暂时没起到作用，不需要 by:Sky 2012/03/14
           // DataAnnotationsModelValidatorProvider.RegisterAdapterFactory(typeof(RequiredAttribute), (m, c, a) => new MyRequiredAttributeAdapter(m, c, (RequiredAttribute)a));
 
            //初始化uowfactory
            IUnitOfWorkFactory factory = new UnitOfWorkFactory();
            factory.Configuration();
             
        }
        /// <summary>
        /// 离开应用程序启动这件事会发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnLineCount"] = (int)Application["OnLineCount"] - 1;
            Application.UnLock();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                StringBuilder sb = new StringBuilder();


                Exception ex = exception.GetBaseException();
                sb.Append(ex.Message);
                sb.Append("\r\nURL: " + Request.Url);
                sb.Append("\r\nUserHostAddress: " + Request.UserHostAddress);
                sb.Append("\r\nFORM: " + Request.Form.ToString());
                sb.Append("\r\nQUERYSTRING: " + Request.Params.ToString());

                sb.Append("\r\nSOURCE: " + exception.Source);
                sb.Append("\r\nExeptionData: " + exception.Data);
                sb.Append("\r\n引发当前异常的原因: " + exception.TargetSite);
                sb.Append("\r\n堆栈跟踪: " + exception.StackTrace);

                sb.Append("\r\nBaseException.SOURCE: " + ex.Source);
                sb.Append("\r\nBaseException.Data: " + ex.Data);
                sb.Append("\r\nBaseException.TargetSite: " + ex.TargetSite);
                sb.Append("\r\nBaseException.StackTrace: " + ex.StackTrace);
                logger.Error(sb.ToString());
                Response.Write(string.Format("<script>if(typeof(showError)!='undefined'){{showError(\"服务器错误\",\"{0}\");}} else{{alert(\"{0}\");}}</script>", Server.HtmlEncode(ex.Message).Replace("\r\n","<br/>")));
                //string errmsg = sb.ToString();
                //string[] parameters = {errmsg, Request.ApplicationPath};
                //Response.Write(string.Format("<script>alert(\"{0}\");window.location.href=\"{1}Login.mvc\";</script>", parameters));

                Response.End();
                Server.ClearError();
            }
        } 
    }

    public class QnDDepResolver : IDependencyResolver
    {
        private readonly IDictionary<Type, Func<object>> types;

        public QnDDepResolver(IHttpContextProvider httpContextProvider, IIncludeHandlingSettings settings, Type[] controllers)
        {
            types = new Dictionary<Type, Func<object>>
			{
				{ typeof (IHttpContextProvider),() => httpContextProvider },
				{ typeof (IKeyGenerator), () => new KeyGenerator() },
				{ typeof (IIncludeHandlingSettings), () => settings }
			};
            types.Add(typeof(IIncludeReader), () => new FileSystemIncludeReader(GetImplementationOf<IHttpContextProvider>()));

            types.Add(typeof(IIncludeStorage), () => new StaticIncludeStorage(GetImplementationOf<IKeyGenerator>()));

            types.Add(typeof(IIncludeCombiner), () => new IncludeCombiner(settings, GetImplementationOf<IIncludeReader>(), GetImplementationOf<IIncludeStorage>(), GetImplementationOf<IHttpContextProvider>()));

            types.Add(typeof(IncludeController), () => new IncludeController(settings, GetImplementationOf<IIncludeCombiner>()));
            foreach (var controller in controllers)
            {
                var controllerType = controller;
                types.Add(controllerType, () => Activator.CreateInstance(controllerType));
            }
        }

        private Interface GetImplementationOf<Interface>()
        {
            return (Interface)GetService(typeof(Interface));
        }

        public object GetService(Type serviceType)
        {
            if (types.ContainsKey(serviceType))
            {
                return types[serviceType]();
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            yield break;
        }
    }
}