using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
using Lib.Web.Mvc.JQuery.JqGrid;
using ZLERP.Business;

namespace ZLERP.Web.Controllers
{
    public class SysFuncController : BaseController<SysFunc, string>
    {
        /// <summary>
        /// 查找菜单
        /// </summary>
        /// <param name="nodeid"></param>
        /// <returns></returns>
        public ActionResult FindFuncs(string nodeid)
        {
            var sysfuncs = this.service.SysFunc.FindFuncs(nodeid);
            if (sysfuncs != null && sysfuncs.Count > 0)
            {
                var data = new JqGridData<SysFunc>
                {
                    rows = sysfuncs
                };
                return Json(data);

            }
            else
            {
                return new EmptyResult();
            }
            
        }
        /// <summary>
        /// 查找菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<SysFunc> funcss = null;
        public JsonResult FindTree(string id)
        {
            #region 节点有限级数菜单（暂不用）
            //var sub1 = this.service.SysFunc.All()
            //    .Where(p => !p.IsDisabled && root.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p => p.OrderNum)
            //    .ToList();

            //var sub2 = this.service.SysFunc.All()
            //   .Where(p => !p.IsDisabled && sub1.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p => p.OrderNum)
            //   .ToList();

            //var sub3 = this.service.SysFunc.All()
            //   .Where(p => !p.IsDisabled && sub2.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p=>p.OrderNum)
            //   .ToList();

            //var sub4 = this.service.SysFunc.All()
            //   .Where(p => !p.IsDisabled && sub3.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p=>p.OrderNum)
            //   .ToList();

           //var funcs = root.Union(sub1)
           //     .Union(sub2)
           //     .Union(sub3)
            //     .Union(sub4);
            #endregion

            //无限级数菜单 lzl add 2016-07-22
            IList<SysFunc> root = this.service.SysFunc.All()
                .Where(f => string.IsNullOrEmpty(f.ParentID) && !f.IsDisabled).OrderBy(p => p.OrderNum).ToList();
            funcss = root;

            subFindTree(root);

            var treeDics = from f in funcss
                           select new
                           {
                               id = f.ID,
                               name = f.FuncName,
                               //防止没有FuncDesc时，ztree控件不能展开节点报错
                               //modified by:Sky 2012-11-14
                               title = string.IsNullOrEmpty(f.FuncDesc)? f.FuncName: f.FuncDesc,
                               pId = f.ParentID
                               
                           };

            return Json(treeDics.ToList());
        }
        /// <summary>
        /// 子菜单递归查找
        /// </summary>
        /// <param name="root"></param>
        private void subFindTree(IList<SysFunc> root)
        {
            IList<SysFunc> sub = this.service.SysFunc.All()
                .Where(p => !p.IsDisabled && root.Select(f => f.ID).Contains(p.ParentID)).OrderBy(p => p.OrderNum)
                .ToList();
            funcss = funcss.Union(sub).ToList();

            if (sub.Count != 0)
            {
                subFindTree(sub);
            }
        }


        /// <summary>
        /// 查找菜单执行按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FindTreeExcludeButton(string id)
        {
            var root = this.service.SysFunc.All()
                .Where(f => string.IsNullOrEmpty(f.ParentID) && !f.IsDisabled && !f.IsButton);

            var sub1 = this.service.SysFunc.All()
                .Where(p => !p.IsDisabled && !p.IsButton && root.Select(f => f.ID).Contains(p.ParentID))
                .ToList();

            var sub2 = this.service.SysFunc.All()
               .Where(p => !p.IsDisabled && !p.IsButton && sub1.Select(f => f.ID).Contains(p.ParentID))
               .ToList();

            var sub3 = this.service.SysFunc.All()
               .Where(p => !p.IsDisabled && !p.IsButton && sub2.Select(f => f.ID).Contains(p.ParentID))
               .ToList();

            var sub4 = this.service.SysFunc.All()
               .Where(p => !p.IsDisabled && !p.IsButton && sub3.Select(f => f.ID).Contains(p.ParentID))
               .ToList();

            var funcs = root.Union(sub1)
                 .Union(sub2)
                 .Union(sub3)
                 .Union(sub4);

            var treeDics = from f in funcs
                           select new
                           {
                               id = f.ID,
                               name = f.FuncName,
                               //防止没有FuncDesc时，ztree控件不能展开节点报错
                               //modified by:Sky 2012-11-14
                               title = string.IsNullOrEmpty(f.FuncDesc) ? f.FuncName : f.FuncDesc,
                               pId = f.ParentID

                           };

            return Json(treeDics.ToList());
        }

        /// <summary>
        /// 获取报表菜单树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult FindReportTree(string id)
        {
            var root = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserInfo.ID)
                .Where(p => !p.IsDisabled && p.ParentID == "95" && p.ID != "9501")
                .OrderBy(p => p.OrderNum)
                .ToList();
            var sub = this.service.User.GetUserFuncs(AuthorizationService.CurrentUserInfo.ID)
               .Where(p => !p.IsDisabled && root.Select(f => f.ID).Contains(p.ParentID))
               .OrderBy(p => p.OrderNum)
               .ToList();

            var funcs = root
                 .Union(sub);

            var treeDics = from f in funcs
                           select new
                           {
                               id = f.ID,
                               name = f.FuncName,
                               //防止没有FuncDesc时，ztree控件不能展开节点报错
                               //modified by:Sky 2012-11-14
                               title = string.IsNullOrEmpty(f.FuncDesc) ? f.FuncName : f.FuncDesc,
                               pId = f.ParentID,
                               url=(!string.IsNullOrEmpty(f.URL)&&((f.URL.Contains(".asp"))||(f.URL.Contains(".com"))))?f.URL:"",
                               url1 = f.URL
                           };

            return Json(treeDics.ToList());
        }
    }
}
