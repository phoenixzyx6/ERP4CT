
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Script.Serialization; 

namespace ZLERP.Model.Generated
{
    public abstract class _InsteadProduct : EntityBase<int?>
    {
        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(Version);

            return sb.ToString().GetHashCode();
        }

        #endregion
        
        #region Properties

        
        [DisplayName("代生产时间")]
        public virtual DateTime ProductTime
        {
            get;
            set;
        }
        [Required]
        [DisplayName("代生产站名")]
        public virtual String ProductName
        {
            get;
            set;
        }
        [Required]
        [DisplayName("代生产方量")]
        [Range(0.000001, float.MaxValue, ErrorMessage = "代生产方量必须是大于0的数")]
        public virtual decimal ProductNum
        {
            get;
            set;
        }
        [Required]
        [DisplayName("代生产单价")]
        [Range(0.000001, float.MaxValue, ErrorMessage = "代生产单价必须是大于0的数")]
        public virtual decimal ProductSinglePrice
        {
            get;
            set;
        }

        [DisplayName("代生产总价")]
        public virtual decimal ProductTotalPrice
        {
            get;
            set;
        }

        #endregion
    }
}
