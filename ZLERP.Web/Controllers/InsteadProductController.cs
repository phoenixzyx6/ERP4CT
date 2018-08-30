using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using ZLERP.Model;
using ZLERP.Model.ViewModels;
using ZLERP.Resources;
using ZLERP.Business;
using ZLERP.Model.Enums;

namespace ZLERP.Web.Controllers
{
    public class InsteadProductController : BaseController<InsteadProduct, int>
    {

        public override ActionResult Add(InsteadProduct InsteadProduct)
        {
            InsteadProduct.ProductTotalPrice = InsteadProduct.ProductSinglePrice * InsteadProduct.ProductNum;
            return base.Add(InsteadProduct);
        }
    }
}
