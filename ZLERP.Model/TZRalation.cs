using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  运输单退转料关系表
    /// </summary>
	public class TZRalation : _TZRalation
    {
        [ScriptIgnore]
        public virtual ShippingDocument SourceShippingDocument
        {
            get;
            set;
        }
        [ScriptIgnore]
        public virtual ShippingDocument TargetShippingDocument
        {
            get;
            set;
        }

        /// <summary>
        /// 驾驶员
        /// </summary>
        public virtual User DriverUser
        {
            get;
            set;
        }

        [DisplayName("源工程名称")]
        public virtual string SourceProjectName
        {
            get {
                return SourceShippingDocument == null ? string.Empty : SourceShippingDocument.ProjectName;
            }
        }

        [DisplayName("目标工程名称")]
        public virtual string TargetProjectName
        {
            get
            {
                return TargetShippingDocument == null ? string.Empty : TargetShippingDocument.ProjectName;
            }
        }

        [DisplayName("源砼强度")]
        public virtual string SourceConStrength
        {
            get
            {
                return SourceShippingDocument == null ? string.Empty : SourceShippingDocument.ConStrength;
            }
        }

        [DisplayName("目标砼强度")]
        public virtual string TargetConStrength
        {
            get
            {
                return TargetShippingDocument == null ? string.Empty : TargetShippingDocument.ConStrength;
            }
        }


        [DisplayName("源运输车号")]
        public virtual string SourceCarID
        {
            get
            {
                return SourceShippingDocument == null ? string.Empty : SourceShippingDocument.CarID;
            }
        }

        [DisplayName("源剩退方量")]
        public virtual decimal SourceCube
        {
            get;
            set;
        }

        [Required]
        [DisplayName("目标运输车号")]
        public override string CarID
        {
            get
            {
                return base.CarID;
            }
            set
            {
                base.CarID = value;
            }
        }

        [DisplayName("目标剩退方量")]
        public override decimal Cube
        {
            get
            {
                return base.Cube;
            }
            set
            {
                base.Cube = value;
            }
        }

        [Required]
        public override string ReturnType
        {
            get
            {
                return base.ReturnType;
            }
            set
            {
                base.ReturnType = value;
            }
        }

        [Required]
        public override string ActionType
        {
            get
            {
                return base.ActionType;
            }
            set
            {
                base.ActionType = value;
            }
        }

        /// <summary>
        /// 手动自动模式
        /// </summary>
        public virtual string AH
        {
            get;
            set;
        }

        /// <summary>
        /// 过磅（自动模式）毛重(Kg)
        /// </summary>
        public virtual int TotalWeight
        {
            get;
            set;
        }

        /// <summary>
        /// 过磅（自动模式）皮重(Kg)
        /// </summary>
        public virtual int CarWeight
        {
            get;
            set;
        }


        /// <summary>
        /// 过磅（自动模式）净重(Kg)
        /// </summary>
        public virtual int Weight
        {
            get;
            set;
        }

        /// <summary>
        /// 过磅（自动模式）换算率(Kg/m³)
        /// </summary>
        public virtual int Exchange
        {
            get;
            set;
        }

        /// <summary>
        /// 0未锁定 1锁定
        /// </summary>
        public virtual string IsLock
        {
            get;
            set;
        }
	}
}