using HISSAP1.Models;
using HISSAP1.Models.SiteModels.InvoiceBudgetModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

    protected override ViewResult View(IView view, object model)
    {
      ////TODO: may cause conflicts
      //var UserStore = new UserStore<ApplicationUser>(db);
      //var UserManager = new UserManager<ApplicationUser>(UserStore);

      //var user = UserManager.FindById(User.Identity.GetUserId());

      //ViewBag.Tier1 = false;
      //ViewBag.Tier2 = false;
      //ViewBag.Tier3 = false;
      //ViewBag.Tier4 = false;
      //ViewBag.Tier5 = false;
      //ViewBag.Tier6 = false;
      //ViewBag.CanPrepareBudget = false;
      //ViewBag.CanSubmitBudget = false;

      //if (Request.IsAuthenticated)
      //{
      //  //Get credentials
      //  List<IdentityRole> roles = db.Roles.ToList();

      //  roles.ForEach(delegate (IdentityRole role)
      //  {
      //    if (User.IsInRole(role.Name))
      //    {
      //      if (role.Name == "Provider Observer")
      //      {
      //        ViewBag.Tier1 = true;
      //      }
      //      if (role.Name == "Provider Fiscal")
      //      {
      //        ViewBag.Tier1 = true;
      //        ViewBag.Tier2 = true;
      //      }
      //      if (role.Name == "Provider Agent")
      //      {
      //        ViewBag.Tier1 = true;
      //        ViewBag.Tier2 = true;
      //        ViewBag.Tier3 = true;
      //      }
      //      if (role.Name == "Provider Administrator")
      //      {
      //        ViewBag.Tier1 = true;
      //        ViewBag.Tier2 = true;
      //        ViewBag.Tier3 = true;
      //        ViewBag.Tier4 = true;
      //      }
      //      if (role.Name == "State Administrator")
      //      {
      //        ViewBag.Tier1 = true;
      //        ViewBag.Tier2 = true;
      //        ViewBag.Tier3 = true;
      //        ViewBag.Tier4 = true;
      //        ViewBag.Tier5 = true;
      //      }
      //      if (role.Name == "System Administrator")
      //      {
      //        ViewBag.Tier1 = true;
      //        ViewBag.Tier2 = true;
      //        ViewBag.Tier3 = true;
      //        ViewBag.Tier4 = true;
      //        ViewBag.Tier5 = true;
      //        ViewBag.Tier6 = true;
      //      }
      //    }
      //  });
      //  if (user.CanPrepareBudget == "Yes")
      //  {
      //    ViewBag.CanPrepareBudget = true;
      //  }
      //  if (user.CanSubmitBudget == "Yes")
      //  {
      //    ViewBag.CanSubmitBudget = true;
      //  }
      //}//If authenticated

      return base.View(view, model);
    }


    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      //TODO: may cause conflicts
      var UserStore = new UserStore<ApplicationUser>(db);
      var UserManager = new UserManager<ApplicationUser>(UserStore);

      var user = UserManager.FindById(User.Identity.GetUserId());


      ViewBag.Providers = new SelectList(db.Providers, "Id", "Name");
      ViewBag.Contracts = new SelectList(db.Contracts, "Id", "ContractName");
      ViewBag.Sites = new SelectList(db.Sites, "Id", "SiteName");

      //TODO: Look over this code, could be done better
      if (Request.IsAuthenticated)
      {
        var siteId = user.CurrentSite.Site.Id;//Get the current siteId
        var budgets = db.Budgets.Where(c => c.BudgetsSite.Id == siteId);//Find the budgets with the current siteId;
        if (budgets.Count() > 0)
        {
          int lastId = budgets.Max(item => item.Id);//Find the newest budget's Id
          var travelers = db.Travelers.Where(c => c.AirfareTravelId == lastId);//Get travelers from the newest budget
                                                                               /*SelectList TravelersLists = new SelectList(travelers, "Id", "Name");*///Only insert travelers from the newest budget
          IEnumerable<SelectListItem> TravelersList = travelers.AsEnumerable().Select(x => new SelectListItem
          {
            Value = x.Id.ToString(),
            Text = string.Format("{0} - {1}", x.Name, x.Destination)
          });

          ViewBag.Travelers = TravelersList;
        }
      }

      ViewBag.Tier1 = false;
      ViewBag.Tier2 = false;
      ViewBag.Tier3 = false;
      ViewBag.Tier4 = false;
      ViewBag.Tier5 = false;
      ViewBag.Tier6 = false;
      ViewBag.CanPrepareBudget = false;
      ViewBag.CanSubmitBudget = false;

      if (Request.IsAuthenticated)
      {
        //Get credentials
        List<IdentityRole> roles = db.Roles.ToList();

        roles.ForEach(delegate (IdentityRole role)
        {
          if (User.IsInRole(role.Name))
          {
            if (role.Name == "Provider Observer")
            {
              ViewBag.Tier1 = true;
            }
            if (role.Name == "Provider Fiscal")
            {
              ViewBag.Tier1 = true;
              ViewBag.Tier2 = true;
            }
            if (role.Name == "Provider Agent")
            {
              ViewBag.Tier1 = true;
              ViewBag.Tier2 = true;
              ViewBag.Tier3 = true;
            }
            if (role.Name == "Provider Administrator")
            {
              ViewBag.Tier1 = true;
              ViewBag.Tier2 = true;
              ViewBag.Tier3 = true;
              ViewBag.Tier4 = true;
            }
            if (role.Name == "State Administrator")
            {
              ViewBag.Tier1 = true;
              ViewBag.Tier2 = true;
              ViewBag.Tier3 = true;
              ViewBag.Tier4 = true;
              ViewBag.Tier5 = true;
            }
            if (role.Name == "System Administrator")
            {
              ViewBag.Tier1 = true;
              ViewBag.Tier2 = true;
              ViewBag.Tier3 = true;
              ViewBag.Tier4 = true;
              ViewBag.Tier5 = true;
              ViewBag.Tier6 = true;
            }
          }
        });
        if (user.CanPrepareBudget == "Yes")
        {
          ViewBag.CanPrepareBudget = true;
        }
        if (user.CanSubmitBudget == "Yes")
        {
          ViewBag.CanSubmitBudget = true;
        }
      }//If authenticated



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



    //public Boolean ValidRole(List<string> roles)
    //{
    //  Boolean IsInARole = false;
    //  Boolean ValidRole = false;
    //  roles.ForEach(delegate (String role)
    //  {
    //    if (User.IsInRole(role))
    //    {
    //      IsInARole = true;
    //    }
    //  });

    //  if(Request.IsAuthenticated && IsInARole)
    //  {
    //    ValidRole = true;
    //  }
    //  return ValidRole;
    //}

  }
}