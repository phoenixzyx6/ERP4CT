using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZLERP.Model;
using ZLERP.Model.Generated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ZLERP.Model.ViewModels
{
    public class PartStockContractViewModel
    {
        public PartStockContract PartStockContract { get; set; }

        public PartStockContractItem PartStockContractItem { get; set; }
    }

}
