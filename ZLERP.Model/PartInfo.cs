using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 配件信息
    /// </summary>
	public class PartInfo : _PartInfo
    {
        [ScriptIgnore]
        public virtual ClassB ClassB
        {
            get;
            set;
        }
        /// <summary>
        /// 大类名称
        /// </summary>
        public virtual string ClassBName 
        {
            get { return ClassB == null ? string.Empty : ClassB.ClassBName; }
        }

        [ScriptIgnore]
        public virtual ClassM ClassM
        {
            get;
            set;
        }
        /// <summary>
        /// 中类名称
        /// </summary>
        public virtual string ClassMName
        {
            get { return ClassM == null ? string.Empty : ClassM.ClassMName; }
        }

        [ScriptIgnore]
        public virtual Classs Classs
        {
            get;
            set;
        }
        /// <summary>
        /// 细类名称
        /// </summary>
        public virtual string ClassSName
        {
            get { return Classs == null ? string.Empty : Classs.ClassSName; }
        }
        [ScriptIgnore]
        public virtual Dic BreadNameDic
        {
            get;
            set;
        }
        
	}
}