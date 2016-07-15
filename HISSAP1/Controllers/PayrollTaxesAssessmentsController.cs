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
//TODO: Delete, just to view generated code
namespace HISSAP1.Controllers
{
    //public class PayrollTaxesAssessmentsController : Controller
    //{
    //    private ApplicationDbContext db = new ApplicationDbContext();

    //    // GET: PayrollTaxesAssessments
    //    public ActionResult Index()
    //    {
    //        return View(db.PayrollTaxesAssessments.ToList());
    //    }

    //    // GET: PayrollTaxesAssessments/Details/5
    //    public ActionResult Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(id);
    //        if (payrollTaxesAssessment == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(payrollTaxesAssessment);
    //    }

    //    // GET: PayrollTaxesAssessments/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: PayrollTaxesAssessments/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,SocialSecurity,UnemploymentInsuranceFederal,UnemploymentInsuranceState,WorkersCompensation,TemporaryDisabilityInsurance,SumTotal")] PayrollTaxesAssessment payrollTaxesAssessment)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.PayrollTaxesAssessments.Add(payrollTaxesAssessment);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        return View(payrollTaxesAssessment);
    //    }

    //    // GET: PayrollTaxesAssessments/Edit/5
    //    public ActionResult Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(id);
    //        if (payrollTaxesAssessment == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(payrollTaxesAssessment);
    //    }

    //    // POST: PayrollTaxesAssessments/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,SocialSecurity,UnemploymentInsuranceFederal,UnemploymentInsuranceState,WorkersCompensation,TemporaryDisabilityInsurance,SumTotal")] PayrollTaxesAssessment payrollTaxesAssessment)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(payrollTaxesAssessment).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        return View(payrollTaxesAssessment);
    //    }

    //    // GET: PayrollTaxesAssessments/Delete/5
    //    public ActionResult Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(id);
    //        if (payrollTaxesAssessment == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(payrollTaxesAssessment);
    //    }

    //    // POST: PayrollTaxesAssessments/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(id);
    //        db.PayrollTaxesAssessments.Remove(payrollTaxesAssessment);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    //}
}
