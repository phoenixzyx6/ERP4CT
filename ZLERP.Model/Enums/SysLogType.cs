using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 系统日志类别
    /// </summary>
    public enum SysLogType
    {
        LoginSuccess,
        LoginFailed,
        LoginPasswordError,
        LoginNotAllowedIP,
        Delete,
        Audit,
        UnAudit,
        ChangeCarOrder,
        UpdateStuffInventory,
        ChangeCar

    }
}
