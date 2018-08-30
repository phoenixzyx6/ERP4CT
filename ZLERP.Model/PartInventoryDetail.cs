using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace ZLERP.Model
{
    /// <summary>
    /// 盘点明细
    /// </summary>
	public class PartInventoryDetail : _PartInventoryDetail
    {
        [ScriptIgnore]
        public virtual PartInfo PartInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 配件名称
        /// </summary>
        [DisplayName("配件名称")]
        public virtual string PartName
        {
            get
            {
                return PartInfo == null ? string.Empty : PartInfo.PartName;
            }
        }
        /// <summary>
        /// 配件名称
        /// </summary>
        [DisplayName("盘盈（损）值")]
        public virtual decimal DiffenceAmount
        {
            get
            {
                return ActualValue - FaceValue;
            }
        }

        public virtual string InventoryID
        {
            get;
            set;
            
        }
	}
}