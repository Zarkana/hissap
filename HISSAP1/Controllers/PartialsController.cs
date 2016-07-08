﻿using HISSAP1.Models;
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


    public ActionResult ChangeSitePartial()
    {

      string id = User.Identity.GetUserId();//Get user id
      if (id == null){return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}//check if exists

      //Get the current users currentSite
      CurrentSite currentSite = db.CurrentSite.Find(id);

      if (currentSite == null){return HttpNotFound();}//check if exists

      //TODO: Change provider name to ProviderName, for consitency with site and contract
      ViewBag.SelectedProvider = new SelectList(db.Providers, "Id", "Name", currentSite.Site.SitesContract.ContractsProviderId);
      ViewBag.SelectedContract = new SelectList(db.Contracts, "Id", "ContractName", currentSite.Site.SitesContractId);
      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName", currentSite.SelectedSite);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);

      return PartialView("_ChangeSitePartial", currentSite);
    }
  }

}