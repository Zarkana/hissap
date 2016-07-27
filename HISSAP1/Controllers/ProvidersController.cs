using HISSAP1.Controllers;
using HISSAP1.CustomFilters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HISSAP1.Models
{
  [Authorization(Roles = "System Administrator, State Administrator, Provider Administrator")]
  public class ProvidersController : MyBaseController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Providers
    public ActionResult Index()
    {
      //TODO: Make into function
      var UserStore = new UserStore<ApplicationUser>(db);
      var UserManager = new UserManager<ApplicationUser>(UserStore);
      var user = UserManager.FindById(User.Identity.GetUserId());

      var providers = db.Providers.Where(c => c.Id == user.CurrentSite.Site.SitesContract.ContractsProvider.Id);

      providers = providers.OrderByDescending(s => s.Name);

      return View(providers.ToList());
    }

    // GET: Providers/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Provider organization = db.Providers.Find(id);
      if (organization == null)
      {
        return HttpNotFound();
      }
      return View(organization);
    }

    // GET: Providers/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Providers/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,Address,ContactPerson,Phone,Website,Email")] Provider provider)
    {
      if (ModelState.IsValid)
      {        
        db.Providers.Add(provider);
        db.Address.Add(provider.Address);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(provider);
    }

    // GET: Providers/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Provider organization = db.Providers.Find(id);
      if (organization == null)
      {
        return HttpNotFound();
      }
      return View(organization);
    }

    // POST: Providers/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,Address,ContactPerson,Phone,Website,Email")] Provider provider)
    {
      if (ModelState.IsValid)
      {
        db.Entry(provider).State = EntityState.Modified;
        db.Entry(provider.Address).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(provider);
    }

    // GET: Providers/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Provider provider = db.Providers.Find(id);
      if (provider == null)
      {
        return HttpNotFound();
      }
      return View(provider);
    }

    // POST: Providers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Provider provider = db.Providers.Find(id);
      Address address = db.Address.Find(provider.Address.AddressId);
      db.Providers.Remove(provider);
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
