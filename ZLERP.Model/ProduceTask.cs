using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ZLERP.Model.Generated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产任务单
    /// </summary>
    public class ProduceTask : _ProduceTask
    {
        [Required]
        public override string CastMode
        {
            get
            {
                return base.CastMode;
            }
            set
            {
                base.CastMode = value;
            }
        }

        public override string Seller
        {
            get
            {
                return base.Seller;
            }
            set
            {
                base.Seller = value;
            }
        }
        [Required]
        public override string Slump
        {
            get
            {
                return base.Slump;
            }
            set
            {
                base.Slump = value;
            }
        }
        [Required]
        public override string ConsPos
        {
            get
            {
                return base.ConsPos;
            }
            set
            {
                base.ConsPos = value;
            }
        }
        [Required,DisplayName("合同编号")]
        public virtual string ContractID
        {
            get;
            set;
        }
        [Required, DisplayName("合同明细")]
        public virtual int ContractItemsID
        {
            get;
            set;
        }
        
        private string _CustName;
        [DisplayName("客户名称")]
        public virtual string CustName
        {
            get { return (this.Contract == null || this.Contract.Customer == null) ? this._CustName : this.Contract.Customer.CustName; }
            set { this._CustName = value; }
        }


        [Required,DisplayName("合同名称")]
        public virtual string ContractName
        {
            get;
            set;
        }

        [DisplayName("配比状态")]
        public virtual int FormulaStatus
        {
            get;
            set;
        }

        [Required, DisplayName("区间")]
        public virtual string RegionID
        {
            get;
            set;
        }

        [DisplayName("工程明细")]
        public virtual string ProjectID
        {
            get;
            set;
        }
        [Required]
        public override string ProjectName
        {
            get
            {
                return base.ProjectName;
            }
            set
            {
                base.ProjectName = value;
            }
        }

        public override bool IsFormulaSend
        {
            get
            {
                return this.FormulaStatus > 0;
            }
            set
            {
                base.IsFormulaSend = value;
            }
        }
        public override string IdentityValue
        {
            get
            {
                if (this.TaskIdentities != null && this.TaskIdentities.Count > 0)
                {
                    return string.Join(",", this.TaskIdentities.Select(p => p.IdentityName).ToList());
                }
                else
                    return "";
            }

        }

        /// <summary>
        /// 是否出开盘鉴定报告
        /// </summary>
        public virtual bool OpenCheckReport
        {
            get;
            set;
        }

        public virtual string MixpropCode
        {
            get { return this.CustMixprop == null ? "" : this.CustMixprop.MixpropCode; }
        }

        public virtual string BetonFormulaName
        {
            get { return this.BetonFormulaInfo == null ? "" : this.BetonFormulaInfo.FormulaName; }
        }

        public virtual string SlurryFormulaName
        {
            get { return this.SlurryFormulaInfo == null ? "" : this.SlurryFormulaInfo.FormulaName; }
        }

        /**/
        public virtual DateTime LastShipTime
        {
            get
            {
                if (this.ShippingDocuments != null && this.ShippingDocuments.Count > 0)
                {
                    ShippingDocument shippingDocument = this.ShippingDocuments.Where(m => m.IsEffective).OrderByDescending(m => m.BuildTime).FirstOrDefault();
                    return shippingDocument == null ? this.NeedDate : shippingDocument.BuildTime;
                }
                else
                {
                    return this.NeedDate;
                }
            }
            set { this.LastShipTime = value; }
        }

        public virtual bool IsLubricatingSlurry
        {
            get
            {
                if (this.SlurryFormulaInfo != null)
                {
                    return this.SlurryFormulaInfo.FormulaType == "FType_S" ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 累计车数
        /// </summary>
        [DisplayName("累计车数")]
        public virtual int? ProvidedTimes
        {
            get
            {
                if (this.ShippingDocuments != null && this.ShippingDocuments.Count > 0)
                {

                    ShippingDocument shippingDocument = this.ShippingDocuments.Where(m => m.IsEffective && m.ShipDocType == "0").OrderByDescending(m => m.ID).FirstOrDefault();
                    if (shippingDocument != null)
                    { return shippingDocument.ProvidedTimes;
                    }
                    else
                    {
                        return 0;
                    }



                }
                else
                {
                    return 0;
                }

            }
        }

        /// <summary>
        /// 已供方量
        /// </summary>
        [DisplayName("已供方量")]
        public virtual decimal ProvidedCube
        {
            get
            {

                if (this.ShippingDocuments != null && this.ShippingDocuments.Count > 0)
                {

                    ShippingDocument shippingDocument = this.ShippingDocuments.Where(m => m.IsEffective && m.ShipDocType == "0").OrderByDescending(m => m.ID).FirstOrDefault();
                    if (shippingDocument != null)
                    {
                        return shippingDocument.ProvidedCube;
                    }
                    else
                    {
                        return 0;
                    }       
                }
                else
                {
                    return 0;

                }
            }
        }

        /// <summary>
        /// 代生产方量
        /// </summary>
        [DisplayName("代生产方量")]
        public virtual decimal InsteadCube
        {
            get
            {

                if (this.InsteadProducts != null && this.InsteadProducts.Count > 0)
                {
                    decimal a = 0;
                    foreach (InsteadProduct i in InsteadProducts) {
                        a = a + i.ProductNum;
                    }
                    return a;
                }
                else
                {
                    return 0;

                }
            }
        }
        

        /// <summary>
        /// 含水(砂)率编号
        /// </summary>
        [DisplayName("含水(砂)率号")]
        public override string Field1
        {
            get
            {
                return base.Field1;
            }
            set
            {
                base.Field1 = value;
            }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        [DisplayName("异常信息")]
        public virtual string Exception
        {
            get;
            set;
        }

        /// <summary>
        /// 责任部门
        /// </summary>
        [DisplayName("责任部门")]
        public virtual string ResponsibleParty
        {
            get;
            set;
        }


        [DisplayName("要求检测人")]
        public virtual string DemandChecker { get; set; }
        [DisplayName("要求检测时间")]
        public virtual DateTime? DemandCheckTime { get; set; }
        [DisplayName("要求塌落度")]
        public virtual string DemandSlump { get; set; }
        [DisplayName("实测检测人")]
        public virtual string RealChecker { get; set; }
        [DisplayName("实测检测时间")]
        public virtual DateTime? RealCheckTime { get; set; }
        [DisplayName("实测塌落度")]
        public virtual string RealSlump { get; set; }
    }
}