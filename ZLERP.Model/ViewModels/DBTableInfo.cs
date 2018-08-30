using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    /// <summary>
    /// 表占用空间信息
    /// </summary>
    public class DBTableInfo
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public string Reserved { get; set; }
        public string Data { get; set; }
        public string IndexSize { get; set; }
        public string UnUsed { get; set; }

        public string Desc { get; set; }
    }
}
