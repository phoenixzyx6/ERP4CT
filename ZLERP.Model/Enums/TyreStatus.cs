using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 轮胎状态
    /// </summary>
    public class TyreStatus
    {
        public static string UsAble = "TyreStatus1"  ; //待用
        public static string Using  = "TyreStatus2"  ; //使用中
        public static string Repair = "TyreStatus3"  ; //维修
        public static string Scrap =  "TyreStatus4"  ; //报废
    }
}
