using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace ZLERP.Web.Helpers
{
    public static class AutoCompleteExtensions
    {

        #region AutoComplete


        /// <summary>
        /// 自动完成控件
        /// </summary>        
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>       
        /// <returns></returns>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName)
        {
            return AutoCompleteHelper(helper, valueInputName, displayInputName, actionName, controllerName, null, null, null, null, null);
        }
        /// <summary>
        /// 自动完成控件
        /// </summary>        
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>             
        /// <param name="valueQueryColumn">查询的值字段名</param>
        /// <param name="displayQueryColumn">查询的文本字段名</param>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string displayQueryColumn

            )
        {
            return AutoCompleteHelper(helper,
                valueInputName,
                displayInputName,
                actionName,
                controllerName,
                valueQueryColumn,
                displayQueryColumn,
                string.Empty,
                string.Empty,
                null);
        }
        /// <summary>
        /// 自动完成控件
        /// </summary>        
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>             
        /// <param name="valueQueryColumn">查询的值字段名</param>
        /// <param name="displayQueryColumn">查询的文本字段名</param>
        /// <param name="condition">附加的查询条件</param>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string displayQueryColumn,
            string condition
            )
        {
            return AutoCompleteHelper(helper,
                valueInputName,
                displayInputName,
                actionName,
                controllerName,
                valueQueryColumn,
                displayQueryColumn,
                condition,
                string.Empty,
                null);
        }
        /// <summary>
        /// 自动完成控件
        /// </summary>        
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>             
        /// <param name="valueQueryColumn">查询的值字段名</param>
        /// <param name="shortQueryColumn">查询的值简码</param>
        /// <param name="displayQueryColumn">查询的文本字段名</param>
        /// <param name="condition">附加的查询条件</param>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string shortQueryColumn,
            string displayQueryColumn,
            string condition,
            string callBackName
            )
        {
            return AutoCompleteHelper(helper,
                valueInputName,
                displayInputName,
                actionName,
                controllerName,
                valueQueryColumn,
                shortQueryColumn,
                displayQueryColumn,
                condition,
                string.Empty,
                null);
        }

        /// <summary>
        /// 自动完成控件
        /// </summary>        
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>             
        /// <param name="valueQueryColumn">查询的值字段名</param>
        /// <param name="displayQueryColumn">查询的文本字段名</param>
        /// <param name="condition">附加的查询条件</param>
        /// <param name="htmlAttributes">其他属性</param>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string displayQueryColumn,
            string condition,
            object htmlAttributes
            )
        {
            return AutoCompleteHelper(helper,
                valueInputName,
                displayInputName,
                actionName,
                controllerName,
                valueQueryColumn,
                displayQueryColumn,
                condition,
                null,
                htmlAttributes);
        }

        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string shortQueryColumn,
            string displayQueryColumn,
            string condition,
            object htmlAttributes
            )
        {
            return AutoCompleteHelper(helper,
                valueInputName,
                displayInputName,
                actionName,
                controllerName,
                valueQueryColumn,
                shortQueryColumn,
                displayQueryColumn,
                condition,
                null,
                htmlAttributes);
        }

        /// <summary>
        /// 自动完成控件
        /// </summary>        
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>             
        /// <param name="valueQueryColumn">查询的值字段名</param>
        /// <param name="displayQueryColumn">查询的文本字段名</param>
        /// <param name="condition">附加的查询条件</param>
        /// <param name="callBackName">需要执行回调函数的名称</param>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string displayQueryColumn,
            string condition,
            string callBackName
            )
        {
            return AutoCompleteHelper(helper,
                valueInputName,
                displayInputName,
                actionName,
                controllerName,
                valueQueryColumn,
                displayQueryColumn,
                condition,
                callBackName,
                null);
        }        

        /// <summary>
        /// 自动完成控件
        /// </summary>       
        /// <param name="helper"></param>
        /// <param name="valueInputName">值字段生成的控件Name</param>
        /// <param name="displayInputName">显示字段生成的控件Name</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoComplete(this HtmlHelper helper,
            string valueInputName,
            string displayInputName,
            string actionName,
            string controllerName,
            object htmlAttributes)
        {

            return AutoCompleteHelper(helper, valueInputName, displayInputName, actionName, controllerName, null, null, null, null, (object)htmlAttributes);

        }

        /// <summary>
        /// 自动完成控件
        /// </summary>
        /// <typeparam name="TModel">要查询的Model</typeparam>
        /// <typeparam name="TProperty">一般为object</typeparam>
        /// <param name="helper"></param>
        /// <param name="valueProperty">值字段</param>
        /// <param name="displayProperty">显示字段</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param> 
        /// <returns></returns>
        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
             Expression<Func<TModel, TProperty>> valueProperty,
            Expression<Func<TModel, TProperty>> displayProperty,
            string actionName,
            string controllerName)
        {

            return AutoCompleteHelper(helper,
                ExpressionHelper.GetExpressionText(valueProperty),
                ExpressionHelper.GetExpressionText(displayProperty),
                actionName,
                controllerName,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                null);

        }

        /// <summary>
        /// 自动完成控件
        /// </summary>
        /// <typeparam name="TModel">要查询的Model</typeparam>
        /// <typeparam name="TProperty">一般为object</typeparam>
        /// <param name="helper"></param>
        /// <param name="valueProperty">值字段</param>
        /// <param name="displayProperty">显示字段</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
             Expression<Func<TModel, TProperty>> valueProperty,
            Expression<Func<TModel, TProperty>> displayProperty,
            string actionName,
            string controllerName,
            object htmlAttributes)
        {

            return AutoCompleteHelper(helper,
               ExpressionHelper.GetExpressionText(valueProperty),
                ExpressionHelper.GetExpressionText(displayProperty),
                actionName,
                controllerName,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                htmlAttributes);

        }
        /// <summary>
        /// 自动完成控件
        /// </summary>
        /// <typeparam name="TModel">要查询的Model</typeparam>
        /// <typeparam name="TProperty">一般为object</typeparam>
        /// <param name="helper"></param>
        /// <param name="valueProperty">值字段</param>
        /// <param name="displayProperty">显示字段</param>
        /// <param name="actionName">请求的ActionName</param>
        /// <param name="controllerName">请求的ControllerName</param> 　
        /// <param name="valueQueryColumn">查询的值字段名</param>
        /// <param name="displayQueryColumn">查询的文本字段名</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
             Expression<Func<TModel, TProperty>> valueProperty,
            Expression<Func<TModel, TProperty>> displayProperty,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string displayQueryColumn,
            object htmlAttributes)
        {

            return AutoCompleteHelper(helper,
                ExpressionHelper.GetExpressionText(valueProperty),
                ExpressionHelper.GetExpressionText(displayProperty),
                actionName,
                controllerName,
                valueQueryColumn,
                displayQueryColumn,
                string.Empty,
                string.Empty,
                (object)htmlAttributes);

        }

        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
             Expression<Func<TModel, TProperty>> valueProperty,
            Expression<Func<TModel, TProperty>> displayProperty,
            string actionName,
            string controllerName,
            string valueQueryColumn,
            string displayQueryColumn,
            object htmlAttributes,string tiaojian)
        {

            return AutoCompleteHelper(helper,
                ExpressionHelper.GetExpressionText(valueProperty),
                ExpressionHelper.GetExpressionText(displayProperty),
                actionName,
                controllerName,
                valueQueryColumn,
                displayQueryColumn,
                tiaojian ,
                string.Empty,
                (object)htmlAttributes);

        }

        /// <summary>
        /// 自动完成控件
        /// </summary>
        /// <typeparam name="TModel">要查询的Model</typeparam>
        /// <typeparam name="TProperty">一般为object</typeparam>
        /// <param name="helper"></param>
        /// <param name="valueProperty">值字段</param>
        /// <param name="displayProperty">显示字段</param>
        /// <param name="actionName">请求的ActionName</param> 
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoCompleteFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> valueProperty,
            Expression<Func<TModel, TProperty>> displayProperty,
            string actionName,
            object htmlAttributes)
        {

            string curController = helper.ViewContext.RouteData.Values["controller"].ToString();
            return AutoCompleteHelper(helper,
               ExpressionHelper.GetExpressionText(valueProperty),
                ExpressionHelper.GetExpressionText(displayProperty),
                actionName,
                curController,
                string.Empty,
                 string.Empty,
                string.Empty,
                string.Empty,
                (object)htmlAttributes);

        }
        //static MvcHtmlString AutoCompleteHelper<TModel, TProperty>(this HtmlHelper<TModel> helper,
        //    Expression<Func<TModel, TProperty>> valueProperty,
        //    Expression<Func<TModel, TProperty>> displayProperty,
        //    string actionName,
        //    string controllerName,
        //    object htmlAttributes)
        //{

        //    return AutoCompleteHelper(helper,
        //        valueProperty,
        //        displayProperty,
        //        actionName,
        //        controllerName,
        //       null,
        //      null,
        //       htmlAttributes);

        //}

        //static MvcHtmlString AutoCompleteHelper<TModel, TProperty>(this HtmlHelper<TModel> helper,
        //    Expression<Func<TModel, TProperty>> valueProperty,
        //    Expression<Func<TModel, TProperty>> displayProperty,
        //    string actionName,
        //    string controllerName,
        //    string valueField,
        //    string textField,
        //    object htmlAttributes)
        //{
        //    string valueInputName = ExpressionHelper.GetExpressionText(valueProperty);
        //    string displayInputName = ExpressionHelper.GetExpressionText(displayProperty);
        //    return AutoCompleteHelper(helper,
        //        valueInputName,
        //        displayInputName,
        //        actionName,
        //        controllerName,
        //      valueField,
        //      textField,
        //      null,
        //      (object)htmlAttributes);

        //}

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="valueProperty"></param>
        /// <param name="displayProperty"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="textField">查询使用的Text字段名</param>
        /// <param name="valueField">查询使用的Value字段名</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        static MvcHtmlString AutoCompleteHelper(this HtmlHelper helper,
           string valueInputName,
           string displayInputName,
            string actionName,
            string controllerName,
            string valueField,
            string textField,
            string condition,
            string callBackName,
            object htmlAttributesObject)
        {



            if (string.IsNullOrEmpty(textField))
                textField = displayInputName;
            if (string.IsNullOrEmpty(valueField))
            {
                valueField = valueInputName;
            }
            valueInputName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(valueInputName);
            displayInputName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(displayInputName);

            StringBuilder inputItemBuilder = new StringBuilder();
            TagBuilder treeInput = new TagBuilder("input");
            treeInput.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            treeInput.MergeAttribute("name", valueInputName);
            treeInput.MergeAttribute("rel", "zl-autocp");
            treeInput.MergeAttribute("zlerp", "autocomplete");
            if (callBackName != null)
            {
                treeInput.MergeAttribute("zl_callback", callBackName);
            }
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            if (string.IsNullOrEmpty(condition))
            {
                treeInput.MergeAttribute("zl-ac-url", url.Action(actionName, controllerName, new { textField = textField, valueField = valueField }));
            }
            else
            {
                treeInput.MergeAttribute("zl-ac-url", url.Action(actionName, controllerName, new { textField = textField, valueField = valueField, condition = condition }));
            }

            treeInput.MergeAttribute("zl-ac-value", valueInputName);
            treeInput.MergeAttribute("zl-ac-text", displayInputName);
            if (htmlAttributesObject != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
                treeInput.MergeAttributes(htmlAttributes);
            }
            // treeInput.GenerateId(valueInputName);
            inputItemBuilder.Append(treeInput.ToString(TagRenderMode.SelfClosing));
            //inputItemBuilder.Append("<script>");
            //inputItemBuilder.Append("$(document).ready(function(){");


            //inputItemBuilder.Append(
            //    string.Format("$('input[rel=zl-autocp]').autocp({{url: '{0}', valueInputName:'{1}', displayInputName:'{2}'}});",
            //    url.Action(actionName, controllerName, new { textField = textField, valueField = valueField }),
            //    valueInputName,
            //    displayInputName
            //    )
            //);
            //inputItemBuilder.Append("});</script>");

            return MvcHtmlString.Create(inputItemBuilder.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="valueInputName"></param>
        /// <param name="displayInputName"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="valueField"></param>
        /// <param name="shortField"></param>
        /// <param name="textField"></param>
        /// <param name="condition"></param>
        /// <param name="callBackName"></param>
        /// <param name="htmlAttributesObject"></param>
        /// <returns></returns>
        static MvcHtmlString AutoCompleteHelper(this HtmlHelper helper,
           string valueInputName,
           string displayInputName,
            string actionName,
            string controllerName,
            string valueField,
            string shortField,
            string textField,
            string condition,
            string callBackName,
            object htmlAttributesObject)
        {



            if (string.IsNullOrEmpty(textField))
                textField = displayInputName;
            if (string.IsNullOrEmpty(valueField))
            {
                valueField = valueInputName;
            }
            valueInputName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(valueInputName);
            displayInputName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(displayInputName);

            StringBuilder inputItemBuilder = new StringBuilder();
            TagBuilder treeInput = new TagBuilder("input");
            treeInput.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            treeInput.MergeAttribute("name", valueInputName);
            treeInput.MergeAttribute("rel", "zl-autocp");
            treeInput.MergeAttribute("zlerp", "autocomplete");
            if (callBackName != null)
            {
                treeInput.MergeAttribute("zl_callback", callBackName);
            }
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            if (string.IsNullOrEmpty(condition))
            {
                treeInput.MergeAttribute("zl-ac-url", url.Action(actionName, controllerName, new { textField = textField, valueField = valueField, shortname = shortField }));
            }
            else
            {
                treeInput.MergeAttribute("zl-ac-url", url.Action(actionName, controllerName, new { textField = textField, valueField = valueField, shortname = shortField, condition = condition }));
            }

            treeInput.MergeAttribute("zl-ac-value", valueInputName);
            treeInput.MergeAttribute("zl-ac-text", displayInputName);
            if (htmlAttributesObject != null)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
                treeInput.MergeAttributes(htmlAttributes);
            }
            inputItemBuilder.Append(treeInput.ToString(TagRenderMode.SelfClosing));            

            return MvcHtmlString.Create(inputItemBuilder.ToString());
        }
        #endregion
    }
}