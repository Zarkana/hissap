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
      //var budgets = db.Budgets.Include(b => b.BudgetsSite);
      var budgets = SortBudget();
      return View(budgets/*.ToList()*/);
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

    // GET: Budgets/Create
    public ActionResult Create(int? id)//To pass on values
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

    // POST: Budgets/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //Used to make copies of current budget
    public ActionResult Create([Bind(Include = "Id,BudgetsSiteId,DateCreated,ContractNumber,BudgetStatus,UnemploymentInsuranceState,UnemploymentInsuranceFederal,TotalContractAmount,TotalExpenses,Salary,PersonnelCost")] Budget model)
    {
      //This create method is NOT taking in the entire budget object, it is acting like a create method and only taking in the fields from the form
      //We must get the entire object
      Budget budget = db.Budgets.Find(model.Id);
      //Modify to accept changes from form
      ModelState.Clear();
      budget.TotalContractAmount = model.TotalContractAmount;
      budget.TotalExpenses = model.TotalExpenses;
      budget.Salary = model.Salary;
      budget.PersonnelCost = model.PersonnelCost;

      //TODO: add all budget model data

      //Overwrite possibly tampered data
      budget.BudgetStatus = "New";//Keep new, state administrators change this later
      //TODO: Make year reference database
      budget.Name = DateTime.Now.Year.ToString() + " " + budget.BudgetStatus + " Budget";
      budget.BudgetsSiteId = GetCurrentSite().Site.Id;
      budget.ContractNumber = GetCurrentSite().Site.SitesContract.ContractNumber;
      budget.DateCreated = DateTime.Now;

      //Find and insert existing Detail Models
      //TODO: I swear I cannot tell if I need this.
      //PayrollTaxesAssessment PayrollTaxesAssessment = db.PayrollTaxesAssessments.Find(budget.PayrollTaxesAssessment.Id);
      //budget.PayrollTaxesAssessment = PayrollTaxesAssessment;

      if (ModelState.IsValid)
      {
        db.Budgets.Add(budget);

        //Detail Models
        db.PayrollTaxesAssessments.Add(budget.PayrollTaxesAssessment);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", budget.BudgetsSiteId);
      return View(budget);
    }

    // POST: Budgets/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    //Used to make initial budget
    public ActionResult CreateInitialBudget()
    {
      Budget tempBudget = new Budget();

      ModelState.Clear();
      tempBudget.BudgetStatus = "New";
      tempBudget.Name = DateTime.Now.Year.ToString() + " " + tempBudget.BudgetStatus + " Budget";
      tempBudget.BudgetsSiteId = GetCurrentSite().Site.Id;
      tempBudget.ContractNumber = GetCurrentSite().Site.SitesContract.ContractNumber;
      tempBudget.DateCreated = DateTime.Now;
      //tempBudget.Temp = "temporary";

      //Create empty Detail Models
      //tempBudget.PayrollTaxesAssessment = CreatePayrollTaxesAssessment();
      tempBudget.PayrollTaxesAssessment = new PayrollTaxesAssessment();
      tempBudget.PayrollTaxesAssessment.SocialSecurity = 112;//For testing purposes only

      if (ModelState.IsValid)
      {
        db.Budgets.Add(tempBudget);

        //TODO: It appears that this is TOTALLY unnecessary!!!
        //Detail Models
        //db.PayrollTaxesAssessments.Add(tempBudget.PayrollTaxesAssessment);
        db.SaveChanges();

        return RedirectToAction("Index");
      }

      ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", tempBudget.BudgetsSiteId);
      //TODO: Is this returning to the right place?
      return View(tempBudget);//Return the create form
    }

    // GET: Budgets/Edit/5
    //public ActionResult Edit(int? id)
    //{
    //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  if (TempData["tempBudget"] != null)//If tempBudget exists
    //  {
    //    Budget tempBudget = (Budget)TempData["tempBudget"];//Assign from CreateInitialBudget
    //    id = tempBudget.Id;
    //  }

    //  if (id == null)//If still null, no id exists
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Budget budget = db.Budgets.Find(id);
    //  if (budget == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", budget.BudgetsSiteId);
    //  return View(budget);
    //}

    // POST: Budgets/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit([Bind(Include = "Id,BudgetsSiteId,DateCreated,ContractNumber,BudgetStatus,UnemploymentInsuranceState,UnemploymentInsuranceFederal,TotalContractAmount,TotalExpenses,Salary,PersonnelCost")] Budget budget)
    //{
    //  //RUNS AFTER Create Temp Budget As well as when clicking edit
    //  ModelState.Clear();

    //  //Reset possibly changed data
    //  budget.BudgetStatus = "New";//Keep new, state administrators change this later
    //  //TODO: Make year reference database
    //  budget.Name = DateTime.Now.Year.ToString() + " " + budget.BudgetStatus + " Budget";
    //  budget.BudgetsSiteId = GetCurrentSite().Site.Id;
    //  budget.ContractNumber = GetCurrentSite().Site.SitesContract.ContractNumber;
    //  budget.DateCreated = DateTime.Now;

    //  //TODO: Edit      
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(budget).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  ViewBag.BudgetsSiteId = new SelectList(db.Sites, "Id", "SiteName", budget.BudgetsSiteId);
    //  return View(budget);
    //}

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
      if (payrollTaxesAssessment != null)
      {
        db.PayrollTaxesAssessments.Remove(payrollTaxesAssessment);
      }

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

    //BUDGET DETAILS METHODS

    //[HttpPost]//Creates an empty PayrollTaxesAssessment
    //public PayrollTaxesAssessment CreatePayrollTaxesAssessment()
    //{
    //  PayrollTaxesAssessment model = new PayrollTaxesAssessment();

    //  ModelState.Clear();
    //  model.SocialSecurity = 0;
    //  model.TemporaryDisabilityInsurance = 0;
    //  model.UnemploymentInsuranceState = 0;
    //  model.WorkersCompensation = 0;
    //  model.SumTotal = 0;

    //  if (ModelState.IsValid)
    //  {
    //    return model;
    //  }

    //  //TODO: Is this returning to the right place?
    //  return model;
    //}

    [HttpGet]//GET: Returns the edit view
    public ActionResult EditPayrollTaxesAssessment(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest);}
      PayrollTaxesAssessment model = db.PayrollTaxesAssessments.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/PayrollTaxesAssessment", model);
    }


    [HttpPost]//Edits an already made PayrollTaxesAssessment
    [ValidateAntiForgeryToken]
    public ActionResult EditPayrollTaxesAssessment([Bind(Include = "Id,SocialSecurity,UnemploymentInsuranceState,UnemploymentInsuranceFederal,WorkersCompensation,TemporaryDisabilityInsurance,SumTotal")] PayrollTaxesAssessment model, string returnUrl)
    {
      if (ModelState.IsValid)
      {        
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return Redirect(returnUrl);
      }

      return Redirect(returnUrl);
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult EditPayrollTaxesAssessment([Bind(Include = "Id,SocialSecurity,UnemploymentInsuranceFederal,UnemploymentInsuranceState,WorkersCompensation,TemporaryDisabilityInsurance,SumTotal")] PayrollTaxesAssessment payrollTaxesAssessment)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    db.Entry(payrollTaxesAssessment).State = EntityState.Modified;
    //    db.SaveChanges();
    //    return RedirectToAction("Index");
    //  }
    //  return View(payrollTaxesAssessment);
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddPayrollItem([Bind(Include = "Id,Name,Amount,Justification,PayrollTaxesAssessmentId")] PayrollItem model)
    {
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.PayrollItems.Add(model);
        db.SaveChanges();

        PayrollTaxesAssessment payrollTaxesAssessment = db.PayrollTaxesAssessments.Find(model.PayrollTaxesAssessmentId);
        int BudgetId = payrollTaxesAssessment.Id;

        //return RedirectToAction("Index");
        return RedirectToAction("EditPayrollTaxesAssessment",  new { id = BudgetId });
      }

      ViewBag.PayrollTaxesAssessmentId = new SelectList(db.PayrollTaxesAssessments, "Id", "Id", model.PayrollTaxesAssessmentId);
      return View(model);
    }

    [HttpPost]
    public JsonResult DeletePayrollItem(string id)
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


  }
}
