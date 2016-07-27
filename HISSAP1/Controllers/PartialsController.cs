using HISSAP1.Models;
using HISSAP1.Models.SiteModels;
using HISSAP1.Models.SiteModels.InvoiceBudgetModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Controllers
{
  [ChildActionOnly]
  public class PartialsController : MyBaseController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    public ActionResult ChangeSitePartial(string controller, string action)
    {
      string id = User.Identity.GetUserId();//Get user id
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }//check if exists

      //Get the current users currentSite
      CurrentSite currentSite = db.CurrentSite.Find(id);

      if (currentSite == null) { return HttpNotFound(); }//check if exists

      //TODO: Change provider name to ProviderName, for consitency with site and contract
      ViewBag.SelectedProvider = new SelectList(db.Providers, "Id", "Name", currentSite.Site.SitesContract.ContractsProviderId);
      ViewBag.SelectedContract = new SelectList(db.Contracts, "Id", "ContractName", currentSite.Site.SitesContractId);
      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName", currentSite.SelectedSite);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);

      ViewBag.ViewsController = controller;
      ViewBag.ViewsAction = action;      

      return PartialView("_ChangeSitePartial", currentSite);
    }

    public ActionResult PayrollItemsPartial()
    {
      return PartialView("Budgets/PayrollItem");
    }

    public ActionResult FringeItemsPartial()
    {
      return PartialView("Budgets/FringeItem");
    }

    public ActionResult AdministrativeItemsPartial()
    {
      return PartialView("Budgets/AdministrativeItem");
    }

    public ActionResult SubcontractsItemsPartial()
    {
      return PartialView("Budgets/SubcontractsItem");
    }

    public ActionResult OtherItemsPartial()
    {
      return PartialView("Budgets/OtherItem");
    }

    public ActionResult TravelerPartial()
    {
      return PartialView("Budgets/Traveler");
    }

    public ActionResult SubsistencePerDiemItemsPartial()
    {
      return PartialView("Budgets/SubsistencePerDiemItem");
    }

    public ActionResult EquipmentItemsPartial()
    {
      return PartialView("Budgets/EquipmentItem");
    }

  }


}