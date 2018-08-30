using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Resources;
using ZLERP.Model;
using log4net;

namespace ZLERP.Web.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class HandleAjaxErrorAttribute : FilterAttribute, IExceptionFilter
    {
         
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }
        public HandleAjaxErrorAttribute()
            : base()
        {
             
        }
         public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            ILog log = LogManager.GetLogger(this.GetType().FullName);
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.IsChildAction || !filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }
              
            Exception exception = filterContext.Exception;

            if (exception.InnerException != null)
                exception = exception.InnerException;
            log.Error(exception.Message, exception);

            //filterContext.Result = new JsonResult
            //{
            //    Data = new ResultInfo { Result = false, 
            //        Message = string.IsNullOrEmpty(this.ErrorMessage) 
            //                    ? Lang.Msg_Operate_Failed+":" + exception.Message
            //                    : this.ErrorMessage
            //    }
            //};
            filterContext.Result = new EmptyResult();
            filterContext.RequestContext.HttpContext.Response.Write(exception.Message);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;
             
        }
         
    }
}