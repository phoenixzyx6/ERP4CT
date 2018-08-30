using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.License.Obj
{
    public class LicenseObj
    {
        public string DateEnd { get; set; }
        public string DateStart { get; set; }
        public byte[] Message { get; set; }
        /// <summary>
        /// 1永久 2临时
        /// </summary>
        public int type { get; set; }
        private bool _IsOK = false;
        public bool IsOK { get { return _IsOK; } set { _IsOK = value; } }
        public string Version { get; set; }
    }
}
