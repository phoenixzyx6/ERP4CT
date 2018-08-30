using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 配件进货退回
    /// </summary>
	public class PartReturn : _PartReturn
    {
        [Required]
        public override System.DateTime? ReturnDate
        {
            get
            {
                return base.ReturnDate;
            }
            set
            {
                base.ReturnDate = value;
            }
        }

        [Required]
        public override string Recipientor
        {
            get
            {
                return base.Recipientor;
            }
            set
            {
                base.Recipientor = value;
            }
        }
	}
}