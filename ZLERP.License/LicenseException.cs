using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.License.Client
{
    public class LicenseException : ApplicationException
    {
        public LicenseException() { }
        public LicenseException(string message)
            : base(message)
        {

        }

        public LicenseException(string message, Exception e) : base(message, e) { }


    }
}
