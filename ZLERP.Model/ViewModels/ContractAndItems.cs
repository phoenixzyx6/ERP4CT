using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLERP.Model.ViewModels
{
    public class ContractAndItems
    {
        public Contract Contract { get; set; }
        public ContractItem ContractItem { get; set; }
        public IdentitySetting IdentitySetting { get; set; }
        public PriceSetting PriceSetting { get; set; }
        public ContractOtherPrice ContractOtherPrice { get; set; }
        public ContractJSTZ ContractJSTZ { get; set; }
        public ContractDZ ContractDZ { get; set; }
        public ContractPay ContractPay { get; set; }
        public ProduceTask ProduceTask { get; set; }
        public ContractProduct ContractProduct { get; set; }
        public ExtraPump ExtraPump { get; set; }
    }
}
