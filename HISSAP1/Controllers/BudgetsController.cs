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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
      foreach (var item in CopiedBudget.PayrollTaxesAssessment.PayrollItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.PayrollTaxesAssessment.Id = 0;
      foreach (var item in CopiedBudget.FringeBenefit.FringeItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.FringeBenefit.Id = 0;
      foreach (var item in CopiedBudget.ContractualAdministrativeService.AdministrativeItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.ContractualAdministrativeService.Id = 0;
      foreach (var item in CopiedBudget.ContractualSubcontractsService.SubcontractsItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.ContractualSubcontractsService.Id = 0;
      foreach (var item in CopiedBudget.OtherBudgetInvoice.OtherItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.OtherBudgetInvoice.Id = 0;
      foreach (var item in CopiedBudget.Airfare.Travelers) { item.Id = Guid.NewGuid(); }
      CopiedBudget.Airfare.Id = 0;
      //foreach (var item in CopiedBudget.AirfareInterIsland.Travelers) { item.Id = Guid.NewGuid(); }
      //CopiedBudget.AirfareInterIsland.Id = 0;
      //foreach (var item in CopiedBudget.AirfareOutOfState.Travelers) { item.Id = Guid.NewGuid(); }
      //CopiedBudget.AirfareOutOfState.Id = 0;
      foreach (var item in CopiedBudget.SubsistencePerDiem.SubsistencePerDiemItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.SubsistencePerDiem.Id = 0;
      foreach (var item in CopiedBudget.EquipmentPurchase.EquipmentItems) { item.Id = Guid.NewGuid(); }
      CopiedBudget.EquipmentPurchase.Id = 0;

      if (ModelState.IsValid)
      {
        db.Budgets.Add(CopiedBudget);
        db.SaveChanges();

        var UserStore = new UserStore<ApplicationUser>(db);
        var UserManager = new UserManager<ApplicationUser>(UserStore);

        var user = UserManager.FindById(User.Identity.GetUserId());

        var budgets = db.Budgets.Where(c => c.BudgetsSite.Id == user.CurrentSite.SelectedSite);//Get all budgets from the currentsite

        int lastId = budgets.Max(item => item.Id);

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
        db.Entry(budget.ContractualAdministrativeService).State = EntityState.Modified;
        db.Entry(budget.ContractualSubcontractsService).State = EntityState.Modified;
        db.Entry(budget.OtherBudgetInvoice).State = EntityState.Modified;
        db.Entry(budget.Airfare).State = EntityState.Modified;
        //db.Entry(budget.AirfareInterIsland).State = EntityState.Modified;
        //db.Entry(budget.AirfareOutOfState).State = EntityState.Modified;
        db.Entry(budget.SubsistencePerDiem).State = EntityState.Modified;
        db.Entry(budget.EquipmentPurchase).State = EntityState.Modified;

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
      tempBudget.ContractualAdministrativeService = new ContractualAdministrativeService();
      tempBudget.ContractualSubcontractsService = new ContractualSubcontractsService();
      tempBudget.OtherBudgetInvoice = new OtherBudgetInvoice();
      tempBudget.Airfare = new Airfare();
      //tempBudget.AirfareInterIsland = new AirfareInterIsland();
      //tempBudget.AirfareOutOfState = new AirfareOutOfState();
      tempBudget.SubsistencePerDiem = new SubsistencePerDiem();
      tempBudget.EquipmentPurchase = new EquipmentPurchase();

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
      if (payrollTaxesAssessment != null) { db.PayrollTaxesAssessments.Remove(payrollTaxesAssessment); }
      FringeBenefit fringeBenefit = db.FringeBenefits.Find(id);
      if (fringeBenefit != null) { db.FringeBenefits.Remove(fringeBenefit); }
      ContractualAdministrativeService contractualAdministrativeService = db.ContractualAdministrativeServices.Find(id);
      if (contractualAdministrativeService != null) { db.ContractualAdministrativeServices.Remove(contractualAdministrativeService); }
      ContractualSubcontractsService contractualSubcontractsService = db.ContractualSubcontractsServices.Find(id);
      if (contractualSubcontractsService != null) { db.ContractualSubcontractsServices.Remove(contractualSubcontractsService); }
      OtherBudgetInvoice otherBudgetInvoice = db.OtherBudgetInvoices.Find(id);
      if (otherBudgetInvoice != null) { db.OtherBudgetInvoices.Remove(otherBudgetInvoice); }
      Airfare airfare = db.Airfares.Find(id);
      if (airfare != null) { db.Airfares.Remove(airfare); }
      //AirfareInterIsland airfareInterIsland = db.AirfaresInterIsland.Find(id);
      //if (airfareInterIsland != null) { db.AirfaresInterIsland.Remove(airfareInterIsland); }
      //AirfareOutOfState airfareOutOfState = db.AirfaresOutOfState.Find(id);
      //if (airfareOutOfState != null) { db.AirfaresOutOfState.Remove(airfareOutOfState); }
      SubsistencePerDiem subsistencePerDiem = db.SubsistencePerDiems.Find(id);
      if (subsistencePerDiem != null) { db.SubsistencePerDiems.Remove(subsistencePerDiem); }
      EquipmentPurchase equipmentPurchase = db.EquipmentPurchases.Find(id);
      if (equipmentPurchase != null) { db.EquipmentPurchases.Remove(equipmentPurchase); }

      Budget budget = db.Budgets.Find(id);
      db.Budgets.Remove(budget);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    //Helper Method to sort the index method by date added
    public IEnumerable<Budget> SortBudget()
    {
      var UserStore = new UserStore<ApplicationUser>(db);
      var UserManager = new UserManager<ApplicationUser>(UserStore);

      var user = UserManager.FindById(User.Identity.GetUserId());

      var budgets = db.Budgets.Where(c => c.BudgetsSite.Id == user.CurrentSite.SelectedSite);

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
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditContractualAdministrativeService(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      ContractualAdministrativeService model = db.ContractualAdministrativeServices.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/ContractualAdministrativeService", model);
    }
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditContractualSubcontractsService(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      ContractualSubcontractsService model = db.ContractualSubcontractsServices.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/ContractualSubcontractsService", model);
    }
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditOtherBudgetInvoice(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      OtherBudgetInvoice model = db.OtherBudgetInvoices.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/Other", model);
    }
    //[HttpGet]//GET: Returns the edit view
    //public ActionResult EditAirfare(int? id)
    //{
    //  if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
    //  Airfare model = db.Airfares.Find(id);

    //  if (model == null) { return HttpNotFound(); }
    //  return View("Details/Airfare", model);
    //}
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditAirfareInterIsland(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      Airfare model = db.Airfares.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/AirfareInterIsland", model);
    }
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditAirfareOutOfState(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      Airfare model = db.Airfares.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/AirfareOutOfState", model);
    }
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditSubsistencePerDiem(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      SubsistencePerDiem model = db.SubsistencePerDiems.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/SubsistencePerDiem", model);
    }
    [HttpGet]//GET: Returns the edit view
    public ActionResult EditEquipmentPurchase(int? id)
    {
      if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
      EquipmentPurchase model = db.EquipmentPurchases.Find(id);

      if (model == null) { return HttpNotFound(); }
      return View("Details/EquipmentPurchase", model);
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
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditContractualAdministrativeService([Bind(Include = "Id,SumTotal,Budget")] ContractualAdministrativeService model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.ContractualAdministrativeServicesTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditContractualSubcontractsService([Bind(Include = "Id,SumTotal,Budget")] ContractualSubcontractsService model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.ContractualSubcontractsServicesTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditOtherBudgetInvoice([Bind(Include = "Id,SumTotal,Budget")] OtherBudgetInvoice model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.OtherTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditAirfareInterIsland([Bind(Include = "Id,SumTotal,Budget")] Airfare model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.AirfareTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditAirfareOutOfState([Bind(Include = "Id,SumTotal,Budget")] Airfare model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.AirfareTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditAirfare([Bind(Include = "Id,SumTotal,Budget")] Airfare model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.AirfareTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditSubsistencePerDiem([Bind(Include = "Id,SumTotal,Budget")] SubsistencePerDiem model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.SubsistencePerDiemTotal = model.SumTotal;//Assign the new value to the total

        db.Entry(budget).State = EntityState.Modified;
        db.Entry(model).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Modify", new { id = model.Id });
      }
      return Redirect(returnUrl);
    }
    [HttpPost]//POST: Edits an already made model
    [ValidateAntiForgeryToken]
    public ActionResult EditEquipmentPurchase([Bind(Include = "Id,SumTotal,Budget")] EquipmentPurchase model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        Budget budget = db.Budgets.Find(model.Id);//Retrieve the budget
        budget.EquipmentPurchasesTotal = model.SumTotal;//Assign the new value to the total

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

        return RedirectToAction("EditFringeBenefit", new { id = BudgetId });
      }

      ViewBag.FringeBenefitId = new SelectList(db.FringeBenefits, "Id", "Id", model.FringeBenefitId);
      return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddAdministrativeItem([Bind(Include = "Id,BusinessIndividualName,ServicesProvided,SubContractNumber,Comments,Amount,ContractualAdministrativeServiceId")] AdministrativeItem model, int id)
    {
      ModelState.Clear();
      model.ContractualAdministrativeServiceId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.AdministrativeItems.Add(model);
        db.SaveChanges();

        ContractualAdministrativeService contractualAdministrativeService = db.ContractualAdministrativeServices.Find(model.ContractualAdministrativeServiceId);
        int BudgetId = contractualAdministrativeService.Id;

        return RedirectToAction("EditContractualAdministrativeService", new { id = BudgetId });
      }

      ViewBag.ContractualAdministrativeServicesId = new SelectList(db.ContractualAdministrativeServices, "Id", "Id", model.ContractualAdministrativeServiceId);
      return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddSubcontractsItem([Bind(Include = "Id,BusinessIndividualName,ServicesProvided,SubContractNumber,Comments,Amount,ContractualSubcontractsServiceId")] SubcontractsItem model, int id)
    {
      ModelState.Clear();
      model.ContractualSubcontractsServiceId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.SubcontractsItems.Add(model);
        db.SaveChanges();

        ContractualSubcontractsService contractualSubcontractsService = db.ContractualSubcontractsServices.Find(model.ContractualSubcontractsServiceId);
        int BudgetId = contractualSubcontractsService.Id;

        return RedirectToAction("EditContractualSubcontractsService", new { id = BudgetId });
      }

      ViewBag.ContractualSubcontractsServiceId = new SelectList(db.ContractualSubcontractsServices, "Id", "Id", model.ContractualSubcontractsServiceId);
      return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddOtherItem([Bind(Include = "Id,Name,Amount,Justification")] OtherItem model, int id)
    {
      ModelState.Clear();
      model.OtherBudgetInvoiceId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.OtherItems.Add(model);
        db.SaveChanges();

        OtherBudgetInvoice otherBudgetInvoice = db.OtherBudgetInvoices.Find(model.OtherBudgetInvoiceId);
        int BudgetId = otherBudgetInvoice.Id;

        return RedirectToAction("EditOtherBudgetInvoice", new { id = BudgetId });
      }

      ViewBag.OtherBudgetInvoiceId = new SelectList(db.OtherBudgetInvoices, "Id", "Id", model.OtherBudgetInvoiceId);
      return View(model);
    }
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult AddAirfareInterIsland([Bind(Include = "Id,Name,Destination,AirFare,Transportation,PurposeOfTravel")] Traveler model, int id)
    //{
    //  ModelState.Clear();
    //  model.AirfareTravelId = id;
    //  if (ModelState.IsValid)
    //  {
    //    model.Id = Guid.NewGuid();
    //    db.Travelers.Add(model);
    //    db.SaveChanges();

    //    AirfareInterIsland airfareInterIsland = db.AirfaresInterIsland.Find(model.AirfareTravelId);
    //    int BudgetId = airfareInterIsland.Id;

    //    return RedirectToAction("EditAirfareInterIsland", new { id = BudgetId });
    //  }

    //  ViewBag.AirfareInterIslandId = new SelectList(db.AirfaresInterIsland, "Id", "Id", model.AirfareTravelId);
    //  return View(model);
    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult AddAirfareOutOfState([Bind(Include = "Id,Name,Destination,AirFare,Transportation,PurposeOfTravel")] Traveler model, int id)
    //{
    //  ModelState.Clear();
    //  model.AirfareTravelId = id;
    //  if (ModelState.IsValid)
    //  {
    //    model.Id = Guid.NewGuid();
    //    db.Travelers.Add(model);
    //    db.SaveChanges();

    //    AirfareOutOfState airfareOutOfState = db.AirfaresOutOfState.Find(model.AirfareTravelId);
    //    int BudgetId = airfareOutOfState.Id;

    //    return RedirectToAction("EditAirfareOutOfState", new { id = BudgetId });
    //  }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddTraveler([Bind(Include = "Id,Name,Destination,AirFare,Transportation,PurposeOfTravel")] Traveler model, int id)
    {
      ModelState.Clear();
      model.AirfareTravelId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.Travelers.Add(model);
        db.SaveChanges();

        Airfare airfare = db.Airfares.Find(model.AirfareTravelId);
        int BudgetId = airfare.Id;

        return RedirectToAction("EditAirfareInterIsland", new { id = BudgetId });
      }

      ViewBag.AirfareId = new SelectList(db.Airfares, "Id", "Id", model.AirfareTravelId);
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddEquipmentItem([Bind(Include = "Id,EquipmentDescription,NumberItems,CostPerItem,Justification")] EquipmentItem model, int id)
    {
      ModelState.Clear();
      model.EquipmentPurchaseId = id;
      if (ModelState.IsValid)
      {
        model.Id = Guid.NewGuid();
        db.EquipmentItems.Add(model);
        db.SaveChanges();

        EquipmentPurchase equipmentPurchase = db.EquipmentPurchases.Find(model.EquipmentPurchaseId);
        int BudgetId = equipmentPurchase.Id;

        return RedirectToAction("EditEquipmentPurchase", new { id = BudgetId });
      }

      ViewBag.EquipmentPurchaseId = new SelectList(db.EquipmentPurchases, "Id", "Id", model.EquipmentPurchaseId);
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
        FringeItem model = db.FringeItems.Find(guid);
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
    [HttpPost]
    public JsonResult DeleteAdministrativeItem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        AdministrativeItem model = db.AdministrativeItems.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.AdministrativeItems.Remove(model);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }
    [HttpPost]
    public JsonResult DeleteSubcontractsItem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        SubcontractsItem model = db.SubcontractsItems.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.SubcontractsItems.Remove(model);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }
    [HttpPost]
    public JsonResult DeleteOtherIem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        OtherItem model = db.OtherItems.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.OtherItems.Remove(model);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }
    [HttpPost]
    public JsonResult DeleteTraveler(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        Traveler model = db.Travelers.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.Travelers.Remove(model);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }
    [HttpPost]
    public JsonResult DeleteSubsistencePerDiemItem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        SubsistencePerDiemItem model = db.SubsistencePerDiemItems.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.SubsistencePerDiemItems.Remove(model);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }
    [HttpPost]
    public JsonResult DeleteEquipmentItem(string id)//TODO: Combine into one function
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        EquipmentItem model = db.EquipmentItems.Find(guid);
        if (model == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.EquipmentItems.Remove(model);
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
