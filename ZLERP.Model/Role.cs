using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    public class Role : _Role
    {
        [ScriptIgnore]
        public virtual IList<SysFunc> SysFuncs { get; set; }

        [ScriptIgnore]
        public virtual IList<UserRole> UserRoles { get; set; }
    }
}
