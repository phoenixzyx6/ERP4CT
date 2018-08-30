using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Security.Principal;
using ZLERP.Business;
using System.Web.Script.Serialization;
using System.Linq.Expressions;
using System.Text;
using ZLERP.Model;
using System.Web.UI;
using System.Globalization;
using ZLERP.Resources;
using System.Web.Mvc.Html;
using System.Configuration;
using System.Web.Routing;
using System.Threading;
namespace ZLERP.Web.Helpers
{
    /// <summary>
    /// ViewHelper的扩展方法
    /// </summary>
    public static class HelperExtensions
    {
        #region HtmlHelper
        private static string m_Version {
            get {
                string ver =  Assembly.GetExecutingAssembly().GetName().Version.ToString();
                int pos = ver.LastIndexOf(".");
                if (pos > 0)
                    ver = ver.Substring(0, pos);
                return ver;
            }
        }
        /// <summary>
        /// ERP版本
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string ERPVersion(this HtmlHelper helper)
        {
              
            return m_Version;
        }
        /*
        /// <summary>
        /// 取得当前用户的按钮列表JSON
        /// </summary>
        /// <param name="funcId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static IHtmlString GetButtons(this HtmlHelper helper, int flag)
        {
            string funcId = helper.ViewContext.HttpContext.Request.QueryString["f"];
            if (!string.IsNullOrEmpty(funcId))
            {
                using (PublicService ps = new PublicService())
                {
                    return helper.Raw(ToJson(ps.User.GetUserButtons(funcId, flag)));
                }
            }
            else
                return new HtmlString("[]");
        }
         */
        #region SelectListData
        /// <summary>
        /// 取得dropdownlist数据
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> SelectListData<TModel, TProperty>(this HtmlHelper helper,
            Expression<Func<TModel, TProperty>> textField,
            Expression<Func<TModel, TProperty>> valueField,
            Expression<Func<TModel, TProperty>> orderBy,
            bool ascending )
            where TModel : Entity
        {
            string dataTextField = ExpressionHelper.GetExpressionText(textField);
            string dataValueField = ExpressionHelper.GetExpressionText(valueField);
            return SelectListDataHelper<TModel>(
                dataTextField,
                 dataValueField,
                 "",
                  ExpressionHelper.GetExpressionText(orderBy),
                  ascending,
                  null
                  );
        }
        /// <summary>
        /// 取得dropdownlist数据
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="nullValue"></param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> SelectListData<TModel, TProperty>(this HtmlHelper helper,
            Expression<Func<TModel, TProperty>> textField,
            Expression<Func<TModel, TProperty>> valueField,          
            Expression<Func<TModel, TProperty>> orderBy,
            bool ascending,
            string nullValue)
            where TModel : Entity
        {
            string dataTextField = ExpressionHelper.GetExpressionText(textField);
            string dataValueField = ExpressionHelper.GetExpressionText(valueField);
            return SelectListDataHelper<TModel>(                
                dataTextField,
                 dataValueField,
                 "",
                  ExpressionHelper.GetExpressionText(orderBy),
                  ascending,
                  nullValue
                  );
        }
        /// <summary>
        /// 取得dropdownlist数据
        /// </summary>
        /// <typeparam name="TModel">要取数据的类型</typeparam>
        /// <typeparam name="TProperty">指定object</typeparam>
        /// <param name="helper"></param>
        /// <param name="textField">显示的字段</param>
        /// <param name="valueField">值字段</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="ascending">是否升序</param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> SelectListData<TModel, TProperty>(this HtmlHelper helper, 
            Expression<Func<TModel, TProperty>> textField, 
            Expression<Func<TModel, TProperty>> valueField, 
            string condition, 
            Expression<Func<TModel, TProperty>> orderBy, 
            bool ascending)
            where TModel : Entity
        {
            string dataTextField = ExpressionHelper.GetExpressionText(textField);
            string dataValueField = ExpressionHelper.GetExpressionText(valueField);
            return SelectListDataHelper<TModel>(                
                dataTextField,
                 dataValueField,
                 condition,
                  ExpressionHelper.GetExpressionText(orderBy),
                  ascending,
                  null
                  );
           
        }
        /// <summary>
        /// 取得Dropdownlist数据
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="helper"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        private static IEnumerable<SelectListItem> SelectListData<TModel>(this HtmlHelper helper,
           string textField,
           string valueField,
            string condition,
            string orderBy,
            bool ascending)
            where TModel : Entity
        {
            return SelectListDataHelper<TModel>(
                textField,
                valueField ,
                 condition,
                  orderBy ,
                  ascending,
                  null);
             
        }

        private static IEnumerable<SelectListItem> SelectListData<TModel>(this HtmlHelper helper,
          string textField,
          string valueField,      
           string orderBy,
           bool ascending)
           where TModel : Entity
        {
            return SelectListDataHelper<TModel>(
                textField,
                valueField,
                 "",
                  orderBy,
                  ascending,
                  null);

        }
        public static IEnumerable<SelectListItem> SelectListData<TModel>(
          string textField,
          string valueField,
           string orderBy,
           bool ascending)
           where TModel : Entity
        {
            return SelectListDataHelper<TModel>(
                textField,
                valueField,
                 "",
                  orderBy,
                  ascending,
                  null);

        }
        public static IEnumerable<SelectListItem> SelectListData<TModel>( 
          string textField,
          string valueField,
            string condition,
           string orderBy,
           bool ascending,
            string nullValue)
           where TModel : Entity
        {
            return SelectListDataHelper<TModel>(
                textField,
                valueField,
                 condition,
                  orderBy,
                  ascending,
                  nullValue);

        }

        #endregion
        /*
        /// <summary>
        /// 取得Dic表下拉菜单数据
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="parentid">父节点ID</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> SelectDicData(this HtmlHelper helper,
            ZLERP.Model.Enums.DicEnum parentid)
        {
            
            return SelectListDataHelper<Dic>(
                ExpressionHelper.GetExpressionText("DicName"),
                 ExpressionHelper.GetExpressionText("DicName"),
                 "ParentID='" + parentid + "'",
                  ExpressionHelper.GetExpressionText("ID"),
                  true,
                  null);


        }
        /// <summary>
        /// 取得Dic表下拉菜单数据，附带空选择项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="parentid">父节点ID</param>
        /// <param name="nullValue">空选择项的值</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> SelectDicData(this HtmlHelper helper,
            ZLERP.Model.Enums.DicEnum parentid, string nullValue)
        {

            return SelectListDataHelper<Dic>(
                ExpressionHelper.GetExpressionText("DicName"),
                 ExpressionHelper.GetExpressionText("DicName"),
                 "ParentID='" + parentid + "'",
                  ExpressionHelper.GetExpressionText("ID"),
                  true,
                  nullValue);


        }

        */
        private static object lock_SelectListData = new object();
        /// <summary>
        /// 取得dropdownlist数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>        
        /// <param name="name"></param>
        /// <param name="textField"></param>
        /// <param name="valueField"></param>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <param name="condition"></param>
        /// <param name="nullValue">空项值，为null则不生成空选择项，非null时，空选择项的值为指定的nullValue</param>
        /// <returns></returns>
        static IEnumerable<SelectListItem> SelectListDataHelper<TEntity>( 
            string textField, 
            string valueField, 
            string condition, 
            string orderBy,
            bool ascending,
            string nullValue) 
            where TEntity: Entity
        {
            if (string.IsNullOrEmpty(textField) || string.IsNullOrEmpty(valueField))
            {
                throw new ApplicationException(string.Format("{0}:textField,valueField", Lang.Error_ParameterRequired));
            }
            //return CacheHelper.GetCacheItem<IEnumerable<SelectListItem>>(
            //    CacheHelper.GetCacheKey<TEntity>(MethodInfo.GetCurrentMethod().Name, textField,valueField, condition, orderBy, ascending.ToString()),

            //    lock_SelectListData,
            //    delegate
            //    {
                    using (PublicService ps = new PublicService())
                    {
                        var data = ps.GetGenericService<TEntity>().All(new List<string> { textField, valueField }, condition, orderBy, ascending);
                        var query = from object item in data
                                        let value = Eval(item, valueField)
                                        select new SelectListItem
                                        {
                                            Value = value,
                                            Text = Eval(item, textField),
                                            Selected = false
                                        };
                        var listItems = query.ToList();
                        //插入空项
                        if (nullValue != null) {
                            listItems.Insert(0, new SelectListItem { Text = "", Value = nullValue });
                        }
                         
                        return listItems;
                    }
              //  });
        }
        /*
        #region 日期时间控件
        private static string defaultFormat = "yy-mm-dd";//默认显示格式
        /// <summary>
        /// 使用特定名称生成日期时间控件
        /// </summary>
        /// <param name="html">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <returns>MvcHtmlString文本</returns>
        public static MvcHtmlString DateTime(this HtmlHelper html, string name)
        {
            return DateTime(html, name, defaultFormat);
        }

        /// <summary>
        /// 使用特定名字生成日期时间控件-带格式化参数
        /// </summary>
        /// <param name="html">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <param name="format">显示格式</param>
        /// <returns>MvcHtmlString文本</returns>
        public static MvcHtmlString DateTime(this HtmlHelper html, string name, string format) {
            return GenerateHtml(name, null, format);
        }

        /// <summary>
        /// 使用特定名字生成日期时间控件-带初始化数据
        /// </summary>
        /// <param name="html">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <param name="date">初始化日期</param>
        /// <returns>MvcHtmlString文本</returns>
        public static MvcHtmlString DateTime(this HtmlHelper html, string name, DateTime date) {
            return DateTime(html, name, date, defaultFormat);
        }

        /// <summary>
        /// 使用特定名字生成日期时间控件-带初始化数据
        /// </summary>
        /// <param name="html">HtmlHelper对象</param>
        /// <param name="name">控件名称</param>
        /// <param name="date">初始化日期</param>
        /// <param name="format">显示格式</param>
        /// <returns>MvcHtmlString文本</returns>
        public static MvcHtmlString DateTime(this HtmlHelper html, string name, DateTime date, string format) {
            return GenerateHtml(name, date, format);
        }

        /// <summary>
        /// 通过lambda表达式生成日期时间控件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html">HtmlHelper对象</param>
        /// <param name="exp">lambda表达式</param>
        /// <returns>MvcHtmlString文本</returns>
        public static MvcHtmlString DateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> exp) {
            return DateTimeFor(html, exp, defaultFormat);
        }

        /// <summary>
        /// 通过lambda表达式生成日期时间控件
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="html">HtmlHelper对象</param>
        /// <param name="exp">lambda表达式</param>
        /// <param name="format">显示格式</param>
        /// <returns></returns>
        public static MvcHtmlString DateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> exp, string format) {
            string name = ExpressionHelper.GetExpressionText(exp);

            DateTime value;

            object data = ModelMetadata.FromLambdaExpression<TModel, TProperty>(exp, html.ViewData).Model;
            if (data != null && System.DateTime.TryParse(data.ToString(), out value))
            {

                return GenerateHtml(name, value, format);

            }
            else {
                return GenerateHtml(name, null, format);
            }
        }

        public static MvcHtmlString GenerateHtml(string name, DateTime? dt, string format)
        {
            StringBuilder inputBuilder = new StringBuilder();
            TagBuilder datetimeInput = new TagBuilder("input");
            datetimeInput.MergeAttribute("type", "text");
            datetimeInput.MergeAttribute("class", "text");
            datetimeInput.MergeAttribute("name", name);
            datetimeInput.MergeAttribute("id",name.Replace(".","_"));

            inputBuilder.Append(datetimeInput.ToString(TagRenderMode.SelfClosing));

            inputBuilder.Append("<script>$(document).ready(function(){");
            inputBuilder.Append("$('#" + name.Replace(".", "_") + "').datetimepicker({ampm: false,dateFormat:'" + format + "'});");
            inputBuilder.Append("});</script>");
            return MvcHtmlString.Create(inputBuilder.ToString());
        }
        #endregion

        */
        #endregion

        #region User
        /// <summary>
        /// 登录的用户名
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string UserName(this IPrincipal user)
        {
            return AuthorizationService.CurrentUserName;
        }
        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string UserID(this IPrincipal user)
        {
            return AuthorizationService.CurrentUserID;
            
        }

        public static string YearAccountID(this IPrincipal user)
        {
            //string value = AuthorizationService.CurrentUserInfo.YearAccountID == null ? "默认" : AuthorizationService.CurrentUserInfo.YearAccountID;
            //return value;

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string identityName = Thread.CurrentPrincipal.Identity.Name;
                if (!string.IsNullOrEmpty(identityName))
                    //return identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[2];
                    return identityName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];//zjy
                else
                    return String.Empty;
            }
            else
                return String.Empty;
        }


        #endregion

        /// <summary>
        /// 字典ComboBox
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static MvcHtmlString DicComboBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            ZLERP.Model.Enums.DicEnum parentId)
        {

            return helper.DicEditorHelper(expression, parentId, null, null, null, "DicComboBox");
        }
        /// <summary>
        /// 字典ComboBox
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId"></param>
        /// <param name="additionalViewData"></param>
        /// <returns></returns>
        public static MvcHtmlString DicComboBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            ZLERP.Model.Enums.DicEnum parentId,
            object additionalViewData)
        {
            return helper.DicEditorHelper(expression, parentId, null, null, additionalViewData, "DicComboBox");
        }

        public static MvcHtmlString DicEidtor(this HtmlHelper helper, string fieldName, string id, ZLERP.Model.Enums.DicEnum parentId) {
            string pId = parentId.ToString();
            return helper.Editor(fieldName, "DicDropDown", new { ParentID = pId, id = id });
        }
       
        /// <summary>
        /// 字典下拉列表，无空选择项
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static MvcHtmlString DicEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper, 
            Expression<Func<TModel, TProperty>> expression,
            ZLERP.Model.Enums.DicEnum parentId)
        {

            return helper.DicEditorHelper(expression, parentId, null, null, null);
        }
        /// <summary>
        /// 字典下拉列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId">父级字典ID</param>
        /// <param name="nullValue">要显示的空项的值，null则不会显示空项</param>
        /// <returns></returns>
        public static MvcHtmlString DicEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            ZLERP.Model.Enums.DicEnum parentId,
            string nullValue)
        {

            return helper.DicEditorHelper(expression, parentId, nullValue, null, null);
        }
        /// <summary>
        /// 字典下拉列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId"></param>
        /// <param name="nullValue"></param>
        /// <param name="nullDisplay"></param>
        /// <returns></returns>
        public static MvcHtmlString DicEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TProperty>> expression,
           ZLERP.Model.Enums.DicEnum parentId,
           string nullValue, string nullDisplay)
        {

            return helper.DicEditorHelper(expression, parentId, nullValue, nullDisplay, null);
        }
        /// <summary>
        /// 字典下拉列表(可设置宽度等html属性)
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId"></param>
        /// <param name="additionalViewData"></param>
        /// <returns></returns>
        public static MvcHtmlString DicEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TProperty>> expression,
           ZLERP.Model.Enums.DicEnum parentId,
           object additionalViewData)
        {
            return helper.DicEditorHelper(expression, parentId, null, null, additionalViewData);
        }

        /// <summary>
        /// 生成一个按钮(div组合而成)
        /// </summary>
        /// <param name="html"></param>
        /// <param name="text">按钮上的文字</param>
        /// <param name="htmlAttr">按钮的HTML属性串</param>
        /// <returns></returns>
        public static MvcHtmlString Button(this HtmlHelper html, string text, string htmlAttr)
        {
            return MvcHtmlString.Create(CreateButton(text, htmlAttr));
        }

        public static MvcHtmlString Button(this HtmlHelper html, string text)
        {
            return html.Button(text, string.Empty);
        }

        static string CreateButton(string text, string htmlAttr)
        {
            return string.Format("<div class=\"menuButton\"><div {1} ><div class=\"left\"></div><div class=\"center\"><span ></span>{0}</div><div class=\"right\"></div></div></div>", text, htmlAttr);
        }


        public static MvcHtmlString DicEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TProperty>> expression,
           ZLERP.Model.Enums.DicEnum parentId,
           string nullValue, string nullDisplay,
           object additionalViewData)
        {
            return helper.DicEditorHelper(expression, parentId, nullValue, nullDisplay, additionalViewData);
        }
        
        /// <summary>
        /// 字典下拉列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="parentId">父级字典ID</param>
        /// <param name="nullValue">要显示的空项的值，null则不会显示空项</param>
        /// <param name="nullDisplay">要显示的空项的文本，null则会显示nullValue</param>
        /// <param name="additionalViewData">附加数据</param>
        /// <param name="templateName">模版名</param>
        /// <returns></returns>
        static MvcHtmlString DicEditorHelper<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            ZLERP.Model.Enums.DicEnum parentId, string nullValue, string nullDisplay, object additionalViewData, string templateName = "DicDropDown")
        {
            string pId = parentId.ToString();
            if(helper.ViewData.ContainsKey(pId)){
                //insert null value
                if (nullValue!=null) {
                    var dic = helper.ViewData[pId] as IList<Dic>;

                    nullDisplay = string.IsNullOrEmpty(nullDisplay) ? nullValue : nullDisplay;
                    if (dic.Where(p => p.TreeCode == nullValue && p.DicName == nullDisplay).Count() == 0)
                    {
                        dic.Insert(0, new Dic { TreeCode = nullValue, DicName = nullDisplay });
                    }
                }
                IEnumerable<KeyValuePair<string, object>> routeValueDictionary = new RouteValueDictionary(new { ParentID = pId }).Concat(new RouteValueDictionary(additionalViewData));
                foreach (KeyValuePair<string, object> pair in routeValueDictionary)
                {
                    helper.ViewDataContainer.ViewData[pair.Key] = pair.Value;
                }
                return helper.EditorFor(expression, templateName);
            }
            else{
                return MvcHtmlString.Empty;
            }
        }

        /// <summary>
        /// 固定宽度的下拉列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <param name="selectList"></param>
        /// <returns></returns>        

        public static MvcHtmlString fixedDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList)
        {

            return helper.DropDownListFor(expression, selectList,"", new { style = "Width:130px" });
        }

        /// <summary>
        /// 序列化为json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToJson<T>(this IList<T> list) {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return   serializer.Serialize(list);
        }
         
        public static string ToJson(object list)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(list);
        }

        /// <summary>
        /// 从指定对象中取指定字段的值
        /// </summary>
        /// <param name="container"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public  static string Eval(object container, string expression)
        {
            object value = container;
            if (!String.IsNullOrEmpty(expression))
            {
                value = DataBinder.Eval(container, expression);
            }
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 请求验证标识，用于actreport验证上传请求等
        /// </summary>
        /// <returns></returns>
        public static string RequestVerificationToken(this HtmlHelper helper)
        {
            return ConfigurationManager.AppSettings["RequestVerificationToken"];
        }
    }
}