using HISSAP1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Controllers
{
  public class MyBaseController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      ViewBag.Providers = new SelectList(db.Providers, "Id", "Name");
      ViewBag.Contracts = new SelectList(db.Contracts, "Id", "ContractName");
      ViewBag.Sites = new SelectList(db.Sites, "Id", "SiteName");
      /*ViewBag.someThing = "someThing";*/ //Add whatever

      base.OnActionExecuting(filterContext);
    }

    //Returns the currentsite object of the current user
    public CurrentSite GetCurrentSite()
    {
      //TODO: Make so that it can also return the error things...
      string id = User.Identity.GetUserId();//Get user id

      //Get the current users currentSite
      CurrentSite currentSite = db.CurrentSite.Find(id);
      
      return currentSite;
    }


  }
}