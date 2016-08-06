using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using HISSAP1.Models.SiteModels.OutcomeModels;

namespace HISSAP1.Controllers
{
  public class IndxOutcomeTypesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: IndxOutcomeTypes
    public ActionResult Index()
    {
      return View(db.IndxOutcomeTypes.ToList());
    }

    // GET: IndxOutcomeTypes/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxOutcomeType indxOutcomeType = db.IndxOutcomeTypes.Find(id);
      if (indxOutcomeType == null)
      {
        return HttpNotFound();
      }
      return View(indxOutcomeType);
    }

    // GET: IndxOutcomeTypes/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: IndxOutcomeTypes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "IndxOutcomeTypeID,Name")] IndxOutcomeType indxOutcomeType)
    {
      if (ModelState.IsValid)
      {
        db.IndxOutcomeTypes.Add(indxOutcomeType);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(indxOutcomeType);
    }

    // GET: IndxOutcomeTypes/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxOutcomeType indxOutcomeType = db.IndxOutcomeTypes.Find(id);
      if (indxOutcomeType == null)
      {
        return HttpNotFound();
      }
      return View(indxOutcomeType);
    }

    // POST: IndxOutcomeTypes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "IndxOutcomeTypeID,Name")] IndxOutcomeType indxOutcomeType)
    {
      if (ModelState.IsValid)
      {
        db.Entry(indxOutcomeType).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(indxOutcomeType);
    }

    // GET: IndxOutcomeTypes/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxOutcomeType indxOutcomeType = db.IndxOutcomeTypes.Find(id);
      if (indxOutcomeType == null)
      {
        return HttpNotFound();
      }
      return View(indxOutcomeType);
    }

    // POST: IndxOutcomeTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      IndxOutcomeType indxOutcomeType = db.IndxOutcomeTypes.Find(id);
      db.IndxOutcomeTypes.Remove(indxOutcomeType);
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
