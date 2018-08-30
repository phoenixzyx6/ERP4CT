using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    [Serializable]
    public class ConsmixPropModify
    {
        public string ID { get; set; }
        public int SynStatus { get; set; }
        public string TaskID { get; set; }
        public object DirtyData { get; set; }
    }
}
