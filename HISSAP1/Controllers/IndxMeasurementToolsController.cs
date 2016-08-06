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
  public class IndxMeasurementToolsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: IndxMeasurementTools
    public ActionResult Index()
    {
      return View(db.IndxMeasurementTools.ToList());
    }

    // GET: IndxMeasurementTools/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxMeasurementTool indxMeasurementTool = db.IndxMeasurementTools.Find(id);
      if (indxMeasurementTool == null)
      {
        return HttpNotFound();
      }
      return View(indxMeasurementTool);
    }

    // GET: IndxMeasurementTools/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: IndxMeasurementTools/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "IndxMeasurementToolID,Name")] IndxMeasurementTool indxMeasurementTool)
    {
      if (ModelState.IsValid)
      {
        db.IndxMeasurementTools.Add(indxMeasurementTool);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(indxMeasurementTool);
    }

    // GET: IndxMeasurementTools/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxMeasurementTool indxMeasurementTool = db.IndxMeasurementTools.Find(id);
      if (indxMeasurementTool == null)
      {
        return HttpNotFound();
      }
      return View(indxMeasurementTool);
    }

    // POST: IndxMeasurementTools/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "IndxMeasurementToolID,Name")] IndxMeasurementTool indxMeasurementTool)
    {
      if (ModelState.IsValid)
      {
        db.Entry(indxMeasurementTool).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(indxMeasurementTool);
    }

    // GET: IndxMeasurementTools/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      IndxMeasurementTool indxMeasurementTool = db.IndxMeasurementTools.Find(id);
      if (indxMeasurementTool == null)
      {
        return HttpNotFound();
      }
      return View(indxMeasurementTool);
    }

    // POST: IndxMeasurementTools/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      IndxMeasurementTool indxMeasurementTool = db.IndxMeasurementTools.Find(id);
      db.IndxMeasurementTools.Remove(indxMeasurementTool);
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
