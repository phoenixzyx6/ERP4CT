using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Linq;
namespace ZLERP.Model
{
    /// <summary>
    ///  系统功能
    /// </summary>
	public class SysFunc : _SysFunc, ITreeGridable
    {
        #region ITreeGridable 成员
        
        public virtual int level
        {
            get
            {
                return this.ID.Length / 2 - 1;
            }
           
        }

        public virtual string parent
        {
            get
            {
                return this.ParentID;
            }
        }
      
        public virtual bool leaf
        {
            get
            {
                return this.IsButton;
            }
        }

        public virtual bool expanded
        {
            get
            {
                return false;
            }
        }

        public virtual bool loaded
        {
            get
            {
                return false;
            }
        }

        [Required]
        [Display(Name="功能编号")]
        public override string ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }
        [Required]
        public override string FuncName
        {
            get
            {
                return base.FuncName;
            }
            set
            {
                base.FuncName = value;
            }
        }
        /// <summary>
        /// Url列表，多url的func对应的多个url, 无url返回空和List`1string 
        /// </summary>
        [ScriptIgnore]
        public virtual IList<string> Urls
        {
            get
            {
                if (!string.IsNullOrEmpty(URL))
                {
                    return URL.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p=>p.Trim())
                        .ToList();
                }
                else
                    return new List<string>();
            }
        }

        /// <summary>
        /// 小写Url列表 
        /// </summary>
        [ScriptIgnore]
        public virtual IList<string> LowerUrls
        {
            get
            {
                return Urls.Select(p => p.ToLower()).ToList();
            }
        }
               
        #endregion
    }
}