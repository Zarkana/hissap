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
  public class IndxMeasurementTypesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: IndxMeasurementTypes
    public ActionResult Index()
    {
      return View(db.IndxMeasurementTypes.ToList());
    }

    // GET: IndxMeasurementTypes/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxMeasurementType indxMeasurementType = db.IndxMeasurementTypes.Find(id);
      if (indxMeasurementType == null)
      {
        return HttpNotFound();
      }
      return View(indxMeasurementType);
    }

    // GET: IndxMeasurementTypes/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: IndxMeasurementTypes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "IndxMeasurementTypeID,Name")] IndxMeasurementType indxMeasurementType)
    {
      if (ModelState.IsValid)
      {
        db.IndxMeasurementTypes.Add(indxMeasurementType);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(indxMeasurementType);
    }

    // GET: IndxMeasurementTypes/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxMeasurementType indxMeasurementType = db.IndxMeasurementTypes.Find(id);
      if (indxMeasurementType == null)
      {
        return HttpNotFound();
      }
      return View(indxMeasurementType);
    }

    // POST: IndxMeasurementTypes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "IndxMeasurementTypeID,Name")] IndxMeasurementType indxMeasurementType)
    {
      if (ModelState.IsValid)
      {
        db.Entry(indxMeasurementType).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(indxMeasurementType);
    }

    // GET: IndxMeasurementTypes/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxMeasurementType indxMeasurementType = db.IndxMeasurementTypes.Find(id);
      if (indxMeasurementType == null)
      {
        return HttpNotFound();
      }
      return View(indxMeasurementType);
    }

    // POST: IndxMeasurementTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      IndxMeasurementType indxMeasurementType = db.IndxMeasurementTypes.Find(id);
      db.IndxMeasurementTypes.Remove(indxMeasurementType);
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
