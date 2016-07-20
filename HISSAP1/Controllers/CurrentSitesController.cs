using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HISSAP1.Controllers
{
  [Authorize]
  public class CurrentSitesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: CurrentSites
    public ActionResult Index()
    {
      var currentSite = db.CurrentSite.Include(c => c.Site).Include(c => c.User);
      return View(currentSite.ToList());
    }

    // GET: CurrentSites/Details/5
    public ActionResult Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      CurrentSite currentSite = db.CurrentSite.Find(id);
      if (currentSite == null)
      {
        return HttpNotFound();
      }
      return View(currentSite);
    }

    // GET: CurrentSites/Create
    public ActionResult Create()
    {
      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName");
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
      return View();
    }

    // POST: CurrentSites/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "UserId,SelectedSite")] CurrentSite currentSite)
    {
      if (ModelState.IsValid)
      {
        db.CurrentSite.Add(currentSite);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName", currentSite.SelectedSite);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
      return View(currentSite);
    }

    // GET: CurrentSites/Edit/5
    public ActionResult Edit(/*string id*/)
    {
      string id = User.Identity.GetUserId();
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      CurrentSite currentSite = db.CurrentSite.Find(id);
      if (currentSite == null)
      {
        return HttpNotFound();
      }
      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName", currentSite.SelectedSite);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
      return View(currentSite);
    }

    // POST: CurrentSites/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "UserId,SelectedSite")] CurrentSite currentSite)
    {
      if (ModelState.IsValid)
      {
        db.Entry(currentSite).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      //This action method is used for the edit page, not for the dropdown. Go to the partials controller for the dropdown
      ViewBag.SelectedContract = new SelectList(db.Contracts, "Id", "ContractName", currentSite.Site.SitesContract.Id);
      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName", currentSite.SelectedSite);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
      return View(currentSite);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Switch([Bind(Include = "UserId,SelectedSite")] CurrentSite currentSite, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        db.Entry(currentSite).State = EntityState.Modified;
        db.SaveChanges();
        return Redirect(returnUrl);
      }
      //This action method is used for the edit page, not for the dropdown. Go to the partials controller for the dropdown
      ViewBag.SelectedContract = new SelectList(db.Contracts, "Id", "ContractName", currentSite.Site.SitesContract.Id);
      ViewBag.SelectedSite = new SelectList(db.Sites, "Id", "SiteName", currentSite.SelectedSite);
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
      return Redirect(returnUrl);
    }

    [HttpGet]
    public ActionResult FillSite(int contract)
    {
      var sites = db.Sites.Where(c => c.SitesContractId == contract)
        .Select(u => new {
          Id = u.Id,
          SiteName = u.SiteName
        });

      //To validate use
      var userStore = new UserStore<ApplicationUser>(db);
      var userManager = new UserManager<ApplicationUser>(userStore);

      var user = userManager.FindById(User.Identity.GetUserId());

      var providerId = user.ProviderId;
      //TODO: Validate!!!

      return Json(sites, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public ActionResult FillContract(int provider)
    {
      var contracts = db.Contracts.Where(c => c.ContractsProviderId == provider)
        .Select(u => new {
          Id = u.Id,
          ContractName = u.ContractName
        });

      return Json(contracts, JsonRequestBehavior.AllowGet);
    }


    // GET: CurrentSites/Delete/5
    public ActionResult Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      CurrentSite currentSite = db.CurrentSite.Find(id);
      if (currentSite == null)
      {
        return HttpNotFound();
      }
      return View(currentSite);
    }

    // POST: CurrentSites/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id)
    {
      CurrentSite currentSite = db.CurrentSite.Find(id);
      db.CurrentSite.Remove(currentSite);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
