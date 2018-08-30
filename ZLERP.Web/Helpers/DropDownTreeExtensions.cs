using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;
namespace ZLERP.Web.Helpers
{
    public static class DropDownTreeExtensions
    {

        #region 下拉树形
        /// <summary>
        /// 下拉树形
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownTreeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName)
        {
            string curController = helper.ViewContext.RouteData.Values["controller"].ToString();
            return DropDownTreeForHelper(helper, expression, actionName, curController, "name","id", null);
           
        }

        /// <summary>
        /// 下拉树形
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownTreeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName, string routeValues)
        {
            //string curController = helper.ViewContext.RouteData.Values["controller"].ToString();
            return DropDownTreeForHelper(helper, expression, actionName, routeValues, "name", "id", null);

        }
        /// <summary>
        /// 下拉树形
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <param name="defaultValue">未选择任何节点时的默认值</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownTreeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName, object routeValues,string defaultValue)
        {
            string curController = helper.ViewContext.RouteData.Values["controller"].ToString();
            return DropDownTreeForHelper(helper, expression, actionName, curController, "name", "id", routeValues, defaultValue);

        }
        /// <summary>
        /// 下拉树形
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString DropDownTreeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName,string controllerName, object routeValues)
        {
            return DropDownTreeForHelper(helper, expression, actionName, controllerName, "name", "id", routeValues);
            
        }
        /// <summary>
        /// 下拉树形
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="defaultValue">未选择任何节点时的默认值</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownTreeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName, object routeValues,string defaultValue)
        {
            return DropDownTreeForHelper(helper, expression, actionName, controllerName, "name", "id", routeValues, defaultValue);

        }
        static MvcHtmlString DropDownTreeFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName, object routeValues, string showField = "name", string valueField = "id")
        {
            //var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(routeValues);
            return DropDownTreeForHelper(helper, expression, actionName, controllerName, showField, valueField, routeValues);
        }
        /// <summary>
        /// 下拉树形
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="actionName">请求数据的Action Name</param>
        /// <param name="controllerName">请求数据的Controller Name</param>
        /// <returns></returns>
        static MvcHtmlString DropDownTreeForHelper<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName, string showField, string valueField, object routeValues, string defaultValue="")
        {

            string name = ExpressionHelper.GetExpressionText(expression);
            string fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
         //   StringBuilder inputItemBuilder = new StringBuilder();
            TagBuilder treeInput = new TagBuilder("input");
            treeInput.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            treeInput.MergeAttribute("name", fullName);
            treeInput.MergeAttribute("zlerp", "ddtree");
           // treeInput.MergeAttributes(htmlAttributes);

            //inputItemBuilder.Append(treeInput.ToString(TagRenderMode.SelfClosing));

            //inputItemBuilder.Append("<script>$(document).ready(function(){");
            //inputItemBuilder.Append("$('input[rel=ddtree]').dropdownTree({");
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            treeInput.MergeAttribute("zl-dt-url", url.Action(actionName, controllerName, routeValues));
            treeInput.MergeAttribute("zl-dt-value", valueField);
            treeInput.MergeAttribute("zl-dt-text", showField);
            treeInput.MergeAttribute("zl-dt-default-val", defaultValue);

            //inputItemBuilder.Append(string.Format(" url: '{0}',", url.Action(actionName, controllerName)));


            //inputItemBuilder.Append(string.Format("showField:'{0}',", showField));
            //inputItemBuilder.Append(string.Format("valueField:'{0}'", valueField));
            //inputItemBuilder.Append(" }); });</script>");
            return MvcHtmlString.Create(treeInput.ToString(TagRenderMode.SelfClosing));
        }


        #endregion

    }
}