using System;
using System.Collections.Generic;
using System.Text;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model
{
    public class Purchase : _Purchase
    {
        public virtual string PurchaseContract_Name
        { get; set; }

        public virtual string PurchaseContract_SupplyTel
        { get; set; }

        public virtual string PurchaseContract_Supply
        { get; set; }

        public virtual string PurchaseContract_SupplyTel1
        { get; set; }

        public virtual string PurchaseContract_Supply1
        { get; set; }

        public virtual DateTime PurchaseContract_Date
        { get; set; }

        public virtual DateTime PurchaseContract_StartDate
        { get; set; }

        public virtual DateTime PurchaseContract_EndDate
        { get; set; }
    }
}
