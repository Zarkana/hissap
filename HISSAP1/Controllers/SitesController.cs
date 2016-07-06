using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using HISSAP1.CustomFilters;

namespace HISSAP1.Controllers
{
  [Authorization(Roles = "System Administrator, State Administrator, Provider Administrator")]
  public class SitesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Sites
    public ActionResult Index()
    {
      var sites = db.Sites.Include(s => s.SitesContract);
      return View(sites.ToList());
    }

    // GET: Sites/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Site site = db.Sites.Find(id);
      if (site == null)
      {
        return HttpNotFound();
      }
      return View(site);
    }

    // GET: Sites/Create
    public ActionResult Create()
    {
      ViewBag.SitesContractId = new SelectList(db.Contracts, "Id", "ContractName");
      return View();
    }

    // POST: Sites/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,SiteName,SitesContractId,Status,Address,City,Zip")] Site site)
    {
      if (ModelState.IsValid)
      {
        db.Sites.Add(site);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.SitesContractId = new SelectList(db.Contracts, "Id", "ContractName", site.SitesContractId);
      return View(site);
    }

    // GET: Sites/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Site site = db.Sites.Find(id);
      if (site == null)
      {
        return HttpNotFound();
      }
      ViewBag.SitesContractId = new SelectList(db.Contracts, "Id", "ContractName", site.SitesContractId);
      return View(site);
    }

    // POST: Sites/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,SiteName,SitesContractId,Status,Address,City,Zip")] Site site)
    {
      if (ModelState.IsValid)
      {
        db.Entry(site).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.SitesContractId = new SelectList(db.Contracts, "Id", "ContractName", site.SitesContractId);
      return View(site);
    }

    // GET: Sites/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Site site = db.Sites.Find(id);
      if (site == null)
      {
        return HttpNotFound();
      }
      return View(site);
    }

    // POST: Sites/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Site site = db.Sites.Find(id);
      db.Sites.Remove(site);
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
