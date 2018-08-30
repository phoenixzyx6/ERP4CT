using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Web.Helpers;

namespace ZLERP.Web.Controllers
{
    public class ProjectRouteController : BaseController<ProjectRoute, string>
    {
        /// <summary>
        /// 播放工地路线
        /// </summary>
        /// <param name="RouteId"></param>
        /// <returns></returns>
        public ActionResult Play(string RouteId)
        {
            try
            {
                var pr = this.service.GetGenericService<RouteDetail>().Query().Where(p => p.RouteId == RouteId).FirstOrDefault();
                if (pr != null)
                {
                    return OperateResult(true, "操作成功", pr);
                }
                else
                {
                    return OperateResult(false, "没有路线", null);
                }

            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }

        }

        /// <summary>
        /// 标定为主路线
        /// </summary>
        /// <param name="RouteId"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public ActionResult SetPrimary(string RouteId, string ProjectId)
        {
            try
            {
                IList<ProjectRoute> prlist = this.service.GetGenericService<ProjectRoute>().Query().Where(p => p.ProjectId == ProjectId && p.ID != RouteId && p.IsPrimary == true).ToList();
                if (prlist.Count > 0)
                {//将其他的路线取消主路线标志
                    foreach (ProjectRoute pr in prlist)
                    {
                        pr.IsPrimary = false;
                        base.Update(pr);
                    }
                }

                ProjectRoute Primarypr = this.service.GetGenericService<ProjectRoute>().Get(RouteId);
                Primarypr.IsPrimary = true;
                Primarypr.Times += 1;
                base.Update(Primarypr);

                return OperateResult(true, "设置成功", null);
            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }

        }


        public ActionResult SavePath(string ProjectId, double? Distance, string LonLatStr)
        {
            try
            {
                this.service.ProjectRoute.SavePath(ProjectId, Distance, LonLatStr);
                return OperateResult(true, "设置成功", null);
            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }
        }

        public ActionResult GetPathByPjid(string ProjectId)
        {
            try
            {
                var pr = this.service.ProjectRoute.Query().Where(p => p.ProjectId == ProjectId && p.IsPrimary == true).OrderByDescending(p => p.Times).FirstOrDefault();
                if (pr != null)
                {
                    return OperateResult(true, "操作成功", pr.RouteDetails);
                }
                else
                {
                    return OperateResult(false, "没有路线或未设置主路线", null);
                }

            }
            catch (Exception e)
            {
                return OperateResult(false, "error:" + e.Message, null);
            }
        }
    }
}
