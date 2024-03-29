﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using HISSAP1.CustomFilters;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace HISSAP1.Controllers
{
  [Authorization(Roles = "System Administrator, State Administrator, Provider Administrator")]
  public class SitesController : MyBaseController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Sites
    public ActionResult Index()
    {
      //TODO: Make into function
      var UserStore = new UserStore<ApplicationUser>(db);
      var UserManager = new UserManager<ApplicationUser>(UserStore);
      var user = UserManager.FindById(User.Identity.GetUserId());

      var sites = db.Sites.Where(c => c.Id == user.CurrentSite.Site.Id).Include(s => s.SitesContract);

      sites = sites.OrderByDescending(s => s.SiteName);

      //sites = db.Sites.Include(s => s.SitesContract);
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
    public ActionResult Create([Bind(Include = "Id,SiteName,SitesContractId,Status,Address")] Site site)
    {
      if (ModelState.IsValid)
      {
        db.Sites.Add(site);
        db.Address.Add(site.Address);
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
    public ActionResult Edit([Bind(Include = "Id,SiteName,SitesContractId,Status,Address")] Site site)
    {
      if (ModelState.IsValid)
      {
        db.Entry(site).State = EntityState.Modified;
        db.Entry(site.Address).State = EntityState.Modified;
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
      Address address = db.Address.Find(site.Address.AddressId);
      db.Sites.Remove(site);
      db.Address.Remove(address);
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
