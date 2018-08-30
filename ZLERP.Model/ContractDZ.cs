﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using ZLERP.Model.Generated;

namespace ZLERP.Model
{
    /// <summary>
    /// 合同垫资
    /// </summary>
    public class ContractDZ : _ContractDZ
    {
        /// <summary>
        /// 合同名称
        /// </summary>
        [DisplayName("合同名称")]
        public virtual string ContractName
        {
            get { return Contract == null ? "" : Contract.ContractName; }
        }
        /// <summary>
        /// 合同编码
        /// </summary>
        [DisplayName("合同编码")]
        [Required]
        public virtual string ContractID
        {
            get;
            set;
        }
        
        [ScriptIgnore]
        public virtual Contract Contract
        {
            get;
            set;
        }
    }
}
