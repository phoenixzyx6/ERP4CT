using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.Enums
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum AuditStatus
    {
        NotAudit = 0,
        Pass = 1,
        Reject = -1
    }
}
