using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    ///  生产调度
    /// </summary>
	public class DispatchList : _DispatchList
    {

        [Required]
        public override string ProductLineID
        {
            get
            {
                return base.ProductLineID;
            }
            set
            {
                base.ProductLineID = value;
            }
        }

        [Required]
        public override decimal? PCRate
        {
            get
            {
                return base.PCRate;
            }
            set
            {
                base.PCRate = value;
            }
        }

        /// <summary>
        /// 工程名称
        /// </summary>
        public virtual string ProjectName
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.ProjectName; 
            }
        }

        /// <summary>
        /// 砼强度
        /// </summary>
        public virtual string ConStrength 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.ConStrength; 
            }
        }

        /// <summary>
        /// 浇筑方式
        /// </summary>
        public virtual string CastMode 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.CastMode; 
            }
        }


        /// <summary>
        /// 施工部位
        /// </summary>
        public virtual string ConsPos 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.ConsPos; 
            }
        }

        /// <summary>
        /// 车号
        /// </summary>
        public virtual string CarID 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.CarID; 
            }
        } 
 
        /// <summary>
        /// 累计车数
        /// </summary>
        public virtual int? ProvidedTimes 
        {
            get{
                return ShippingDocument == null ? 0:ShippingDocument.ProvidedTimes; 
            }
        } 
 
        /// <summary>
        /// 已供方量
        /// </summary>
        public virtual decimal ProvidedCube 
        {
            get{
                return ShippingDocument == null ? 0:ShippingDocument.ProvidedCube; 
            }
        }

        /// <summary>
        /// 计划方量
        /// </summary>
        public virtual decimal PlanCube
        {
            get
            {
                return ShippingDocument == null ? 0 : ShippingDocument.PlanCube;
            }
        } 

        /// <summary>
        /// 出票方量
        /// </summary>
        public virtual decimal ParCube 
        {
            get{
                return ShippingDocument == null ? 0:ShippingDocument.ParCube; 
            }
        } 
 
        /// <summary>
        /// 司机姓名
        /// </summary>
        public virtual string Driver 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.Driver; 
            }
        } 
        /// <summary>
        /// 发货员
        /// </summary>
        public virtual string Signer 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.Signer; 
            }
        } 
        
        /// <summary>
        /// 泵名称
        /// </summary>
        public virtual string PumpName 
        {
            get{
                return ShippingDocument == null ? string.Empty:ShippingDocument.PumpName; 
            }
        }
        

        /// <summary>
        /// 发货单备注
        /// </summary>
        public override string Remark 
        {
            get{
                return ShippingDocument == null ? string.Empty : ShippingDocument.Remark; 
            }
        }

        /// <summary>
        /// 打印次数
        /// </summary>
        public virtual int? PrintCount
        {
            get
            {
                return ShippingDocument == null ? 0 : ShippingDocument.PrintCount;
            }
        }

        /// <summary>
        /// 其他方量
        /// </summary>
        public virtual decimal OtherCube
        {
            get
            {
                return ShippingDocument == null ? 0 : ShippingDocument.OtherCube;
            }
        }

        /// <summary>
        /// 虚方量
        /// </summary>
        public virtual decimal? XuCube
        {
            get
            {
                return ShippingDocument == null ? 0 : ShippingDocument.XuCube;
            }
        }

        /// <summary>
        /// 剩余方量
        /// </summary>
        public virtual decimal? RemainCube
        {
            get
            {
                return ShippingDocument == null ? 0 : ShippingDocument.RemainCube;
            }
        }

        /// <summary>
        /// 质检员
        /// </summary>
        public virtual string Surveyor
        {
            get
            {
                return ShippingDocument == null ? string.Empty : ShippingDocument.Surveyor;
            }
        }

        /// <summary>
        /// 合同编号
        /// </summary>
        public virtual string ContractID
        {
            get
            {
                return ShippingDocument == null ? string.Empty : ShippingDocument.ContractID;
            }
        }

        /// <summary>
        /// 生产线
        /// </summary>
        public virtual string ProductLineName
        {
            get
            {
                return ShippingDocument == null ? string.Empty : ShippingDocument.ProductLineName;
            }
        }

        /// <summary>
        /// 发货单号
        /// </summary>
        public virtual string ShipDocID
        {
            get;
            set;
        }
        /// <summary>
        /// 任务单号
        /// </summary>
        public virtual string TaskID
        {
            get;
            set;
        }
	}
}