using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Web.Script.Serialization;

namespace ZLERP.Model
{
    public class JqGridJson<TEntity>
    {
              
        public int page { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
         
        public int total { 
            get {
                return (int)Math.Ceiling((float)records / (float)pageSize);
               
            }
        }
        /// <summary>
        /// 每页记录数
        /// </summary>       
        [ScriptIgnore]
        public  int pageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>       
        public int records { get; set; }
        public string userdata { get; set; }
        /// <summary>
        /// 行数据
        /// </summary>      
        public IList<TEntity> rows { get; set; }
    }
}
