using HISSAP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Controllers
{
  public class MyBaseController : Controller
  {
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      ApplicationDbContext db = new ApplicationDbContext();
      ViewBag.Providers = new SelectList(db.Providers, "Id", "Name");
      ViewBag.Contracts = new SelectList(db.Contracts, "Id", "ContractName");
      /*ViewBag.someThing = "someThing";*/ //Add whatever
      base.OnActionExecuting(filterContext);
    }
  }
}