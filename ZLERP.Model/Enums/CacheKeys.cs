using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 缓存key值静态类
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// 所有系统全局配置
        /// </summary>
        public const string AllSysConfigs = "GetAllSysConfigs";
        /// <summary>
        /// 用户权限缓存格式化串
        /// <example>string.Format(UserFuncsFormatString,userId)</example>
        /// </summary>
        public const string UserFuncsFormatString = "GetUserFuncs_{0}";

        /// <summary>
        /// 角色权限缓存格式化串
        /// <example>string.Format(UserFuncsFormatString,userId)</example>
        /// </summary>
        public const string RoleFuncsFormatString = "GetRoleFuncs_{0}";

        /// <summary>
        /// 所有字典缓存
        /// </summary>
        public const string AllDics = "AllDics";
        /// <summary>
        /// 所有系统模板缓存
        /// </summary>
        public const string AllFuncs = "AllFuncs";
    }
}
