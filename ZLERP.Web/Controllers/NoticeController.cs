using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZLERP.Model;
namespace ZLERP.Web.Controllers
{

    public class NoticeController : BaseController<Notice,int?>
    {
        [ValidateInput(false)]
        public override ActionResult Add(Notice entity)
        {
            return base.Add(entity);
        }
        [ValidateInput(false)]
        public override ActionResult Update(Notice entity)
        {

            return base.Update(entity);
        }
    }
}
