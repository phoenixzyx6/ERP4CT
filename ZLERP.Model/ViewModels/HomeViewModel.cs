using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    public class HomeViewModel
    {
        /// <summary>
        /// 当前登录用户根功能列表
        /// </summary>
        public IList<MenuInfo> RootFuncs { get; set; }
        /// <summary>
        /// 所有系统配置项
        /// </summary>
        public IList<SysConfig> SysConfigs { get; set; }

        public User User;

        /// <summary>
        /// 当前登录用户子功能列表[JSON]
        /// </summary>
        public string SubFuncs { get; set; }

        public class MenuInfo
        {
            public string ID { get; set; }
            /// <summary>
            /// 父节点
            /// </summary>
            public string PID { get; set; }
            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 描述
            /// </summary>
            public string Desc { get; set; }
            /// <summary>
            ///  是否叶子
            /// </summary>
            public bool IsL { get; set; }
            /// <summary>
            /// 是否按钮
            /// </summary>
            public bool IsB { get; set; }
            /// <summary>
            /// 路径
            /// </summary>
            public string Url { get; set; }

        }
    }


}
