using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    public class UserToRole
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public UserRole UserRole { get; set; }
    }
}
