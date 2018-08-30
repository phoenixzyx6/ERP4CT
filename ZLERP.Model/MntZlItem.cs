using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 
    /// </summary>
	public class MntZlItem : _MntZlItem
    {
        /// <summary>
        /// 支领单号
        /// </summary>
        [Required]
        [DisplayName("支领单号")]
        public virtual string MntZlID
        {
            get;
            set;
        }
        /// <summary>
        /// 领用项目编号
        /// </summary>
        [Required]
        [DisplayName("领用项目")]
        public virtual string MaintenanceID
        {
            get;
            set;
        }
        public virtual string ItemName
        {
            get { return Maintenance == null ? string.Empty : Maintenance.ItemName; }
        }
        /// <summary>
        /// 零件大类
        /// </summary>
        [Required]
        [DisplayName("零件大类")]
        public virtual string ClassBID
        {
            get;
            set;
        }
        public virtual string ClassBName
        {
            get { return ClassB == null ? string.Empty : ClassB.ClassBName; }
        }
        /// <summary>
        /// 零件中类
        /// </summary>
        [DisplayName("零件中类")]
        public virtual string ClassMID
        {
            get;
            set;
        }
        public virtual string ClassMName
        {
            get { return ClassM == null ? string.Empty : ClassM.ClassMName; }
        }
        /// <summary>
        /// 零件细类
        /// </summary>
        [DisplayName("零件细类")]
        public virtual string ClassSID
        {
            get;
            set;
        }
        public virtual string ClassSName
        {
            get { return Classs == null ? string.Empty : Classs.ClassSName; }
        }
        /// <summary>
        /// 零件编号
        /// </summary>
        [Required]
        [DisplayName("零件")]
        public virtual string PartID
        {
            get;
            set;
        }
        public virtual string PartName
        {
            get { return PartInfo == null ? string.Empty : PartInfo.PartName; }
        }
	}
}