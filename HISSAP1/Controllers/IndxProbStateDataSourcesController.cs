using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using HISSAP1.Models.SiteModels.ProblemStatementModels;
using HISSAP1.CustomFilters;

namespace HISSAP1.Controllers
{
  //[Authorization(Roles = "System Administrator")]
  //public class IndxProbStateDataSourcesController : Controller
  //{
  //  private ApplicationDbContext db = new ApplicationDbContext();

  //  // GET: IndxProbStateDataSources
  //  public ActionResult Index()
  //  {
  //    return View(db.IndxProbStateDataSources.ToList());
  //  }

  //  // GET: IndxProbStateDataSources/Details/5
  //  public ActionResult Details(int? id)
  //  {
  //    if (id == null)
  //    {
  //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  //    }
  //    IndxProbStateDataSource indxProbStateDataSource = db.IndxProbStateDataSources.Find(id);
  //    if (indxProbStateDataSource == null)
  //    {
  //      return HttpNotFound();
  //    }
  //    return View(indxProbStateDataSource);
  //  }

  //  // GET: IndxProbStateDataSources/Create
  //  public ActionResult Create()
  //  {
  //    return View();
  //  }

  //  // POST: IndxProbStateDataSources/Create
  //  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  //  // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  //  [HttpPost]
  //  [ValidateAntiForgeryToken]
  //  public ActionResult Create([Bind(Include = "Id,Name")] IndxProbStateDataSource indxProbStateDataSource)
  //  {
  //    if (ModelState.IsValid)
  //    {
  //      db.IndxProbStateDataSources.Add(indxProbStateDataSource);
  //      db.SaveChanges();
  //      return RedirectToAction("Index");
  //    }

  //    return View(indxProbStateDataSource);
  //  }

  //  // GET: IndxProbStateDataSources/Edit/5
  //  public ActionResult Edit(int? id)
  //  {
  //    if (id == null)
  //    {
  //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  //    }
  //    IndxProbStateDataSource indxProbStateDataSource = db.IndxProbStateDataSources.Find(id);
  //    if (indxProbStateDataSource == null)
  //    {
  //      return HttpNotFound();
  //    }
  //    return View(indxProbStateDataSource);
  //  }

  //  // POST: IndxProbStateDataSources/Edit/5
  //  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
  //  // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
  //  [HttpPost]
  //  [ValidateAntiForgeryToken]
  //  public ActionResult Edit([Bind(Include = "Id,Name")] IndxProbStateDataSource indxProbStateDataSource)
  //  {
  //    if (ModelState.IsValid)
  //    {
  //      db.Entry(indxProbStateDataSource).State = EntityState.Modified;
  //      db.SaveChanges();
  //      return RedirectToAction("Index");
  //    }
  //    return View(indxProbStateDataSource);
  //  }

  //  // GET: IndxProbStateDataSources/Delete/5
  //  public ActionResult Delete(int? id)
  //  {
  //    if (id == null)
  //    {
  //      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  //    }
  //    IndxProbStateDataSource indxProbStateDataSource = db.IndxProbStateDataSources.Find(id);
  //    if (indxProbStateDataSource == null)
  //    {
  //      return HttpNotFound();
  //    }
  //    return View(indxProbStateDataSource);
  //  }

  //  // POST: IndxProbStateDataSources/Delete/5
  //  [HttpPost, ActionName("Delete")]
  //  [ValidateAntiForgeryToken]
  //  public ActionResult DeleteConfirmed(int id)
  //  {
  //    IndxProbStateDataSource indxProbStateDataSource = db.IndxProbStateDataSources.Find(id);
  //    db.IndxProbStateDataSources.Remove(indxProbStateDataSource);
  //    db.SaveChanges();
  //    return RedirectToAction("Index");
  //  }

  //  protected override void Dispose(bool disposing)
  //  {
  //    if (disposing)
  //    {
  //      db.Dispose();
  //    }
  //    base.Dispose(disposing);
  //  }
  //}
}
