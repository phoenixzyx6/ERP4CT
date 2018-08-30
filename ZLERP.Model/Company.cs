using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;


namespace ZLERP.Model
{
	public class Company : _Company
    {
        [Required]
        public override string CompName
        {
            get
            {
                return base.CompName;
            }
            set
            {
                base.CompName = value;
            }
        }

        [Required]
        public override string CompAddr
        {
            get
            {
                return base.CompAddr;
            }
            set
            {
                base.CompAddr = value;
            }
        }
        [Required]
        public override string Tel
        {
            get
            {
                return base.Tel;
            }
            set
            {
                base.Tel = value;
            }
        }

        [Required]
        public override string Principal
        {
            get
            {
                return base.Principal;
            }
            set
            {
                base.Principal = value;
            }
        }


	}
}