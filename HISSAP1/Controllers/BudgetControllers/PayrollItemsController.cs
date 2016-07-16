using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using HISSAP1.Models.SiteModels.InvoiceBudgetModels;

namespace HISSAP1.Controllers.BudgetControllers
{
    public class PayrollItemsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //// GET: PayrollItems
        //public ActionResult Index()
        //{
        //    var payrollItems = db.PayrollItems.Include(p => p.PayrollTaxesAssessment);
        //    return View(payrollItems.ToList());
        //}

        //// GET: PayrollItems/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PayrollItem payrollItem = db.PayrollItems.Find(id);
        //    if (payrollItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(payrollItem);
        //}

        //// GET: PayrollItems/Create
        //public ActionResult Create()
        //{
        //    ViewBag.PayrollTaxesAssessmentId = new SelectList(db.PayrollTaxesAssessments, "Id", "Id");
        //    return View();
        //}

        //// POST: PayrollItems/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Amount,Justification,PayrollTaxesAssessmentId")] PayrollItem payrollItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        payrollItem.Id = Guid.NewGuid();
        //        db.PayrollItems.Add(payrollItem);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.PayrollTaxesAssessmentId = new SelectList(db.PayrollTaxesAssessments, "Id", "Id", payrollItem.PayrollTaxesAssessmentId);
        //    return View(payrollItem);
        //}

        //// GET: PayrollItems/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PayrollItem payrollItem = db.PayrollItems.Find(id);
        //    if (payrollItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.PayrollTaxesAssessmentId = new SelectList(db.PayrollTaxesAssessments, "Id", "Id", payrollItem.PayrollTaxesAssessmentId);
        //    return View(payrollItem);
        //}

        //// POST: PayrollItems/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Amount,Justification,PayrollTaxesAssessmentId")] PayrollItem payrollItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(payrollItem).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.PayrollTaxesAssessmentId = new SelectList(db.PayrollTaxesAssessments, "Id", "Id", payrollItem.PayrollTaxesAssessmentId);
        //    return View(payrollItem);
        //}

        //// GET: PayrollItems/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PayrollItem payrollItem = db.PayrollItems.Find(id);
        //    if (payrollItem == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(payrollItem);
        //}

        //// POST: PayrollItems/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    PayrollItem payrollItem = db.PayrollItems.Find(id);
        //    db.PayrollItems.Remove(payrollItem);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
