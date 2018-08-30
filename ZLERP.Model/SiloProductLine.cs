using System;
using System.Collections.Generic;
using System.Text; 
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
namespace ZLERP.Model
{
    /// <summary>
    /// 筒仓机组关系
    /// </summary>
    public class SiloProductLine : _SiloProductLine
    {

        public virtual Silo Silo
        {
            get;
            set;
        }

        public virtual ProductLine ProductLine
        {
            get;
            set;
        }

        public virtual string SiloName
        {
            get
            {
                if (Silo != null)
                {
                    return Silo.SiloName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public virtual string ProductLineName
        {
            get
            {
                if (ProductLine != null)
                {
                    return ProductLine.ProductLineName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public virtual string StuffName
        {
            get
            {
                if (Silo == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Silo.StuffName;
                }
            }
        }

        [Range(1,24)]
        public override int OrderNum
        {
            get
            {
                return base.OrderNum;
            }
            set
            {
                base.OrderNum = value;
            }
        }

    }
}