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

namespace HISSAP1.Controllers
{
  public class OutcomesController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Outcomes
    public ActionResult Index()
    {
      return View(db.Outcomes.ToList());
    }

    // GET: Outcomes/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Outcome outcome = db.Outcomes.Find(id);
      if (outcome == null)
      {
        return HttpNotFound();
      }

      //Begin many to many controller code

      var OutcomeTypeList = db.IndxOutcomeTypes.Select(o => new SelectListItem { Value = "" + o.IndxOutcomeTypeID, Text = o.Name }).ToList();

      //TODO: Consider a function
      //var Results = from d in db.IndxOutcomeTypes
      //              select new
      //              {
      //                d.IndxOutcomeTypeID,
      //                d.Name
      //              };
      var MyViewModel = new Outcome();

      MyViewModel.Id = id.Value;
      //MyViewModel.Name = problemStatement.Name;
      MyViewModel.OutcomeDescription = outcome.OutcomeDescription;
      MyViewModel.BaselinePercentage = outcome.BaselinePercentage;
      MyViewModel.DesiredPercentage = outcome.DesiredPercentage;

      //var MyCheckBoxList = new List<CheckBoxViewModel>();

      //foreach (var item in Results)
      //{
      //  MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
      //}

      ViewBag.OutcomeTypes = OutcomeTypeList;
      MyViewModel.OutcomeTypes = OutcomeTypeList;


      //End many to many controller code

      return View(outcome);
    }

    // GET: Outcomes/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: Outcomes/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,DesiredOutcome,OutcomeDescription,BaselinePercentage,DesiredPercentage")] Outcome outcome)
    {
      if (ModelState.IsValid)
      {
        db.Outcomes.Add(outcome);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(outcome);
    }

    // GET: Outcomes/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Outcome outcome = db.Outcomes.Find(id);
      if (outcome == null)
      {
        return HttpNotFound();
      }


      //Begin many to many controller code

      var MyViewModel = new Outcome();      

      MyViewModel.Id = id.Value;
      MyViewModel.OutcomeDescription = outcome.OutcomeDescription;
      MyViewModel.BaselinePercentage = outcome.BaselinePercentage;
      MyViewModel.DesiredPercentage = outcome.DesiredPercentage;

      //var MyCheckBoxList = new List<CheckBoxViewModel>();

      //foreach (var item in Results)
      //{
      //  MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
      //}
      var OutcomeTypeList = db.IndxOutcomeTypes.Select(o => new SelectListItem { Value = "" + o.IndxOutcomeTypeID, Text = o.Name }).ToList();

      ViewBag.OutcomeTypes = OutcomeTypeList;
      MyViewModel.OutcomeTypes = OutcomeTypeList;

      //End many to many controller code

      //TODO: add sites to outcomes
      //ViewBag.ProblemStatementsSiteId = new SelectList(db.Sites, "Id", "SiteName", problemStatement.ProblemStatementsSiteId);
      return View(outcome);
    }

    // POST: Outcomes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,DesiredOutcome,OutcomeDescription,BaselinePercentage,DesiredPercentage")] Outcome outcome)
    {
      if (ModelState.IsValid)
      {
        db.Entry(outcome).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(outcome);
    }

    // GET: Outcomes/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Outcome outcome = db.Outcomes.Find(id);
      if (outcome == null)
      {
        return HttpNotFound();
      }
      return View(outcome);
    }

    // POST: Outcomes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Outcome outcome = db.Outcomes.Find(id);
      db.Outcomes.Remove(outcome);
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
