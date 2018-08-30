using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model
{
    [Serializable]
    public class ResultInfo
    {

        public bool Result { get; set; }
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}