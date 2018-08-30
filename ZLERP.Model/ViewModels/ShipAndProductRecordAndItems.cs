using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    public class ShipAndProductRecordAndItems
    {
        public ShippingDocument ShippingDocument { get; set; }
        public ProductRecord ProductRecord { get; set; }
        public ProductRecordItem ProductRecordItem { get; set; }
    }
}
