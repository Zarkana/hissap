using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Controllers
{
  [ChildActionOnly]
  public class PartialsController : MyBaseController
  {
    public ActionResult ChangeSitePartial()
    {
      return PartialView("_ChangeSitePartial");
    }
  }
}