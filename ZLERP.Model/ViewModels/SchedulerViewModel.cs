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
    public class SchedulerViewModel
    {
        public DispatchList DispatchList { get; set; }

        public ShippingDocument ShippingDocument { get; set; }

        public TZRalation TZRalation { get; set; }

        public CustomerPlan CustomerPlan { get; set; }

        public InsteadProduct InsteadProduct { get; set; }

        public ProduceTask ProduceTask { get; set; }
    }

}
