using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using HISSAP1.Models.SiteModels;
using System.Globalization;
using HISSAP1.Controllers;
using HISSAP1.Helpers;
using HISSAP1.Models.SiteModels.InvoiceBudgetModels;

namespace HISSAP1.Controllers
{
  public class BudgetsController : MyBaseController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Budgets
    public ActionResult Index()
    {
      var budgets = SortBudget();
      return View(budgets);
    }

    // GET: Modify
    public ActionResult Modify(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Budget budget = db.Budgets.Find(id);
      if (budget == null)
      {
        return HttpNotFound();
      }
      return View(budget);
    }

    // GET: Budgets/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Budget budget = db.Budgets.Find(id);
      if (budget == null)
      {
        return HttpNotFound();
      }
      return View(budget);
    }

    // POST: Budgets/Create
    public ActionResult Create(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Budget budget = db.Budgets.Find(id);
      if (budget == null)
      {
        return HttpNotFound();
      }

      var CopiedBudget = db.Budgets.AsNoTracking()
                                   .FirstOrDefault(e => e.Id == budget.Id);

      ModelState.Clear();
      //Overwrite possibly tampered data
      CopiedBudget.Name = DateTime.Now.Year.ToString() + " " + budget.BudgetStatus + " Budget";
      CopiedBudget.BudgetsSiteId = GetCurrentSite().Site.Id;
      CopiedBudget.DateCreated = DateTime.Now;

      //Reset primary ID's for Details objects
      foreach (var item in CopiedBudget.PayrollTaxesAssessment.PayrollItems){item.Id = Guid.NewGuid();}
      CopiedBudget.PayrollTaxesAssessment.Id = 0;
      foreach (var item in CopiedBudget.FringeBenefit.FringeItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.FringeBenefit.Id = 0;

      if (ModelState.IsValid)
      {
        db.Budgets.Add(CopiedBudget);
        db.SaveChanges();

        int lastId = db.Budgets.Max(item => item.Id);//TODO: Bad if taking from wrong site location
        return RedirectToAction("Modify", new { id = lastId });
      }
      ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", budget.BudgetsSiteId);
      return RedirectToAction("Index");
    }

    // GET: Budgets/Edit
    public ActionResult Edit(int? id)//To pass on values
    {
      Budget budget = db.Budgets.Find(id);
      if (budget == null)
      {
        return HttpNotFound();
      }

      //Hidden passed on values      
      budget.BudgetsSiteId = GetCurrentSite().Site.Id;
      budget.ContractNumber = GetCurrentSite().Site.SitesContract.ContractNumber;

      ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName");
      return View(budget);
    }

    // POST: Budgets/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    //Used to edit the current budget
    public ActionResult Edit([Bind(Include = "Id,BudgetsSiteId,DateCreated,ContractNumber,BudgetStatus,UnemploymentInsuranceState,UnemploymentInsuranceFederal,TotalContractAmount,TotalExpenses,Salary,PersonnelCost,AuditService,Insurance,LeaseRentalEquipment,LeaseRentalMotorVehicle,LeaseRentalSpace,Mileage,PostageFreightDelivery,PublicationPrinting,RepairMaintenance,StaffTraining,Supplies,Telecommunication,Utilities,ProgramActivities,IndirectCost,OtherCurrentExpenses,Transportation,Total")] Budget model)
    {
      Budget budget = db.Budgets.Find(model.Id);
      //Modify to also accept changes from form
      ModelState.Clear();
      budget.TotalContractAmount = model.TotalContractAmount;
      budget.TotalExpenses = model.TotalExpenses;
      budget.Salary = model.Salary;
      budget.PersonnelCost = model.PersonnelCost;
      budget.AuditService = model.AuditService;
      budget.Insurance = model.Insurance;
      budget.LeaseRentalEquipment = model.LeaseRentalEquipment;
      budget.LeaseRentalMotorVehicle = model.LeaseRentalMotorVehicle;
      budget.LeaseRentalSpace = model.LeaseRentalSpace;
      budget.Mileage = model.Mileage;
      budget.PostageFreightDelivery = model.PostageFreightDelivery;
      budget.PublicationPrinting = model.PublicationPrinting;
      budget.RepairMaintenance = model.RepairMaintenance;
      budget.StaffTraining = model.StaffTraining;
      budget.Supplies = model.StaffTraining;
      budget.Telecommunication = model.Telecommunication;
      budget.Utilities = model.Utilities;
      budget.ProgramActivities = model.ProgramActivities;
      budget.IndirectCost = model.IndirectCost;
      budget.OtherCurrentExpenses = model.OtherCurrentExpenses;
      budget.Transportation = model.Transportation;
      budget.Total = model.Total;
      //TODO: Possibly pass this data on better

      //Overwrite possibly tampered data
      budget.BudgetStatus = "New";//Keep new, state administrators change this later            
      budget.Name = DateTime.Now.Year.ToString() + " " + budget.BudgetStatus + " Budget";//TODO: Make year reference database
      budget.BudgetsSiteId = GetCurrentSite().Site.Id;
      budget.ContractNumber = GetCurrentSite().Site.SitesContract.ContractNumber;
      budget.DateCreated = DateTime.Now;

      if (ModelState.IsValid)
      {
        db.Entry(budget).State = EntityState.Modified;

        //Detail Models
        db.Entry(budget.PayrollTaxesAssessment).State = EntityState.Modified;
        db.Entry(budget.FringeBenefit).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = budget.Id });
      }
      ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", budget.BudgetsSiteId);
      return View(budget);
    }


    [HttpPost]
    public ActionResult CreateInitialBudget()//Used to make initial budget
    {
      Budget tempBudget = new Budget();

      ModelState.Clear();
      tempBudget.BudgetStatus = "New";
      tempBudget.Name = DateTime.Now.Year.ToString() + " " + tempBudget.BudgetStatus + " Budget";
      tempBudget.BudgetsSiteId = GetCurrentSite().Site.Id;
      tempBudget.ContractNumber = GetCurrentSite().Site.SitesContract.ContractNumber;
      tempBudget.DateCreated = DateTime.Now;

      //Insert empty Detail Models
      tempBudget.PayrollTaxesAssessment = new PayrollTaxesAssessment();
      tempBudget.FringeBenefit = new FringeBenefit();

      if (ModelState.IsValid)
      {
        db.Budgets.Add(tempBudget);
        db.SaveChanges();

        return RedirectToAction("Index");
      }

      ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", tempBudget.BudgetsSiteId);
      //TODO: Is this returning to the right place?
      return View(tempBudget);
    }

    // GET: Budgets/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Budget budget = db.Budgets.Find(id);
      if (budget == null)
      {
        return HttpNotFound();
      }
      return View(budget);
    }

    // POST: Budgets/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      //Budget models
      PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(id);
      if (payrollTaxesAssessment != null){db.PayrollTaxesAssessments.Remove(payrollTaxesAssessment);}
      FringeBenefit fringeBenefit = db.FringeBenefits.Find(id);
      if (fringeBenefit != null) { db.FringeBenefits.Remove(fringeBenefit); }

      Budget budget = db.Budgets.Find(id);
      db.Budgets.Remove(budget);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    //Helper Method to sort the index method by date added
    public IEnumerable<Budget> SortBudget()
    {
      var budgets = db.Budgets.Include(b => b.BudgetsSite);
      budgets = budgets.OrderByDescending(s => s.DateCreated);

      return budgets;
    }


    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    /*BUDGET DETAILS METHODS*/
    
    /*GET: EditObject(int? id)*/

    [HttpGet]//GET: Returns the edit view
    public ActionResult EditPayrollTaxesAssessment(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      PayrollTaxesAssessment model = db.PayrollTaxesAssessments.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/PayrollTaxesAssessment", model);
    }

    [HttpGet]//GET: Returns the edit view
    public ActionResult EditFringeBenefit(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      FringeBenefit model = db.FringeBenefits.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/FringeBenefit", model);
    }

    /*POST: EditObject(model)*/

    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditPayrollTaxesAssessment([Bind(Include = "Id,SocialSecurity,UnemploymentInsuranceState,UnemploymentInsuranceFederal,WorkersCompensation,TemporaryDisabilityInsurance,SumTotal")] PayrollTaxesAssessment model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.PayrollTaxesAssessmentTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }

    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditFringeBenefit([Bind(Include = "Id,HealthInsurance,Retirement,LifeLongDisabilityInsurance,Budget,SumTotal")] FringeBenefit model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.FringeBenefitsTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }

    /*POST: AddItem(model, int id)*/

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddPayrollItem([Bind(Include = "Id,Name,Amount,Justification,PayrollTaxesAssessmentId")] PayrollItem model, int id)
    {
      ModelState.Clear();
      model.PayrollTaxesAssessmentId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.PayrollItems.Add(model);
        db.SaveChanges();

        PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(model.PayrollTaxesAssessmentId);
        int BudgetId = payrollTaxesAssessment.Id;

        //return RedirectToAction("Index");
        return RedirectToAction("EditPayrollTaxesAssessment", new { id = BudgetId });
      }

      ViewBag.PayrollTaxesAssessmentId = new SelectList(db.PayrollTaxesAssessments, "Id", "Id", model.PayrollTaxesAssessmentId);
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddFringeItem([Bind(Include = "Id,Name,Amount,Justification,FringeId")] FringeItem model, int id)
    {
      ModelState.Clear();
      model.FringeBenefitId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.FringeItems.Add(model);
        db.SaveChanges();

        FringeBenefit fringeBenefit = db.FringeBenefits.Find(model.FringeBenefitId);
        int BudgetId = fringeBenefit.Id;

        //return RedirectToAction("Index");
        return RedirectToAction("EditFringeBenefit", new { id = BudgetId });
      }

      ViewBag.FringeBenefitId = new SelectList(db.FringeBenefits, "Id", "Id", model.FringeBenefitId);
      return View(model);
    }

    /*POST: DeleteItem(string id)*/

    [HttpPost]
    public JsonResult DeletePayrollItem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        PayrollItem payrollItem = db.PayrollItems.Find(guid);
        if (payrollItem == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.PayrollItems.Remove(payrollItem);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }

    [HttpPost]
    public JsonResult DeleteFringeItem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        FringeItem model= db.FringeItems.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.FringeItems.Remove(model);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }
  }
}
