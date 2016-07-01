using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;

namespace HISSAP1.Controllers
{
  public class CurrentSitesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: CurrentSites
    //public ActionResult Index()
    //{
    //    var currentSite = db.CurrentSite.Include(c => c.User);
    //    return View(currentSite.ToList());
    //}

    // GET: CurrentSites/Details/5
    //public ActionResult Details(string id)
    //{
    //    if (id == null)
    //    {
    //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //    }
    //    CurrentSite currentSite = db.CurrentSite.Find(id);
    //    if (currentSite == null)
    //    {
    //        return HttpNotFound();
    //    }
    //    return View(currentSite);
    //}

    // GET: CurrentSites/Create
    //public ActionResult Create()
    //{
    //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
    //    return View();
    //}

    // POST: CurrentSites/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "UserId,SelectedProvider,SelectedContract,SelectedSite")] CurrentSite currentSite)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.CurrentSite.Add(currentSite);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
    //    return View(currentSite);
    //}


    // GET: CurrentSites/Edit/5
    public ActionResult Edit(string id)
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
      ViewBag.Providers = new SelectList(db.Providers, "Id", "Name");
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
      return View(currentSite);
    }

    //TODO: evaluate if needed?
    public IList<Provider> GetProviders()
    {
      return (from c in db.Providers
              select c).ToList();

    }

    // POST: CurrentSites/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "UserId,SelectedProvider,SelectedContract,SelectedSite")] CurrentSite currentSite)
    {
      if (ModelState.IsValid)
      {
        db.Entry(currentSite).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.UserId = new SelectList(db.Users, "Id", "Email", currentSite.UserId);
      return View(currentSite);
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
