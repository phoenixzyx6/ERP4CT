using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  字典表
    /// </summary>
	public class Dic : _Dic , ITreeGridable
    {
        [Required]
        [Display(Name="字典编号")]
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
        public override string DicName
        {
            get
            {
                return base.DicName;
            }
            set
            {
                base.DicName = value;
            }
        } 

        #region ITreeGridable 成员

        public virtual int level
        {
            get { return this.Deep; }
        }

        public virtual string parent
        {
            get {return this.ParentID; }
        }
      
        public virtual bool leaf
        {
            get { return this.IsLeaf; }
        }

        public virtual bool expanded
        {
            get { return false; }
        }

        public virtual bool loaded
        {
            get {return false; }
        }
        
        #endregion
    }
}