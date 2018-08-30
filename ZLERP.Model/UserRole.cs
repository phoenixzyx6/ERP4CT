using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    public class UserRole:_UserRole
    {
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
