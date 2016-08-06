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
using HISSAP1.ViewModels;
using HISSAP1.Models.SiteModels.ProblemStatementModels;
using System.Web.Security;
using HISSAP1.CustomFilters;
using Microsoft.AspNet.Identity;

namespace HISSAP1.Controllers
{
  public class ProblemStatementsController : MyBaseController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: ProblemStatements
    public ActionResult Index()
    {
      var problemStatements = db.ProblemStatements.Include(p => p.ProblemStatementsSite);
      return View(problemStatements.ToList());
    }

    // GET: ProblemStatements/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProblemStatement problemStatement = db.ProblemStatements.Find(id);
      if (problemStatement == null)
      {
        return HttpNotFound();
      }

      //Begin many to many controller code

      //TODO: Consider a function
      var Results = from d in db.IndxProbStateDataSources
                    select new
                    {
                      d.Id,
                      d.Name,
                      Checked = ((from pd in db.ProblemStatementToIndxProbStateDataSources
                                  where (pd.ProblemStatementId == id) & (pd.IndxProbStateDataSourceId == d.Id)
                                  select pd).Count() > 0)
                    };
      var MyViewModel = new ProblemStatementViewModel();

      MyViewModel.ProblemStatementID = id.Value;
      //MyViewModel.Name = problemStatement.Name;
      MyViewModel.ProblemStatementDescription = problemStatement.ProblemStatementDescription;
      MyViewModel.Consequences = problemStatement.Consequences;
      MyViewModel.Resources = problemStatement.Resources;
      MyViewModel.Gaps = problemStatement.Gaps;

      var MyCheckBoxList = new List<CheckBoxViewModel>();

      foreach (var item in Results)
      {
        MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
      }

      MyViewModel.DataSources = MyCheckBoxList;

      //End many to many controller code

      return View(MyViewModel);
    }

    // GET: ProblemStatements/Create
    public ActionResult Create()
    {
      ViewBag.ProblemStatementsSiteId = new SelectList(db.Sites, "Id", "SiteName");
      //Begin many to many controller code

      //TODO: Consider a function
      var MyViewModel = new ProblemStatementViewModel();   

      //TODO: Consider a function
      var Results = from d in db.IndxProbStateDataSources
                    select new
                    {
                      d.Id,
                      d.Name,
                      Checked = false
                    };

      var MyCheckBoxList = new List<CheckBoxViewModel>();


      foreach (var item in Results)
      {
        MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
      }

      MyViewModel.DataSources = MyCheckBoxList;

      //End many to many controller code

      //ViewBag.ProblemStatementsSiteId = new SelectList(db.Sites, "Id", "SiteName", problemStatement.ProblemStatementsSiteId);
      return View(MyViewModel);
    }

    // POST: ProblemStatements/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ProblemStatementViewModel problemStatement)
    {
      if (ModelState.IsValid)
      {
        var MyProblemStatement = new ProblemStatement();

        //MyProblemStatement.Name = problemStatement.Name;
        MyProblemStatement.ProblemStatementDescription = problemStatement.ProblemStatementDescription;
        MyProblemStatement.Consequences = problemStatement.Consequences;
        MyProblemStatement.Resources = problemStatement.Resources;
        MyProblemStatement.Gaps = problemStatement.Gaps;

        MyProblemStatement.ProblemStatementsSite = db.CurrentSite.Find(User.Identity.GetUserId()).Site;

        foreach (var item in db.ProblemStatementToIndxProbStateDataSources)
        {
          if (item.ProblemStatementId == problemStatement.ProblemStatementID)
          {
            db.Entry(item).State = EntityState.Deleted;
          }
        }

        foreach (var item in problemStatement.DataSources)
        {
          if (item.Checked)
          {
            db.ProblemStatementToIndxProbStateDataSources.Add(new ProblemStatementToIndxProbStateDataSource() { ProblemStatementId = problemStatement.ProblemStatementID, IndxProbStateDataSourceId = item.Id });
          }
        }

        db.ProblemStatements.Add(MyProblemStatement);

        db.SaveChanges();
        return RedirectToAction("Index");
      }

      ViewBag.ProblemStatementsSiteId = new SelectList(db.Sites, "Id", "SiteName", problemStatement.ProblemStatementID);
      return View(problemStatement);
    }

    // GET: ProblemStatements/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProblemStatement problemStatement = db.ProblemStatements.Find(id);
      if (problemStatement == null)
      {
        return HttpNotFound();
      }

      //Begin many to many controller code

      //TODO: Consider a function
      var Results = from d in db.IndxProbStateDataSources
                    select new
                    {
                      d.Id,
                      d.Name,
                      Checked = ((from pd in db.ProblemStatementToIndxProbStateDataSources
                                  where (pd.ProblemStatementId == id) & (pd.IndxProbStateDataSourceId == d.Id)
                                  select pd).Count() > 0)
                    };
      var MyViewModel = new ProblemStatementViewModel();

      MyViewModel.ProblemStatementID = id.Value;
      MyViewModel.ProblemStatementDescription = problemStatement.ProblemStatementDescription;
      MyViewModel.Consequences = problemStatement.Consequences;
      MyViewModel.Resources = problemStatement.Resources;
      MyViewModel.Gaps = problemStatement.Gaps;

      var MyCheckBoxList = new List<CheckBoxViewModel>();

      foreach (var item in Results)
      {
        MyCheckBoxList.Add(new CheckBoxViewModel { Id = item.Id, Name = item.Name, Checked = item.Checked });
      }

      MyViewModel.DataSources = MyCheckBoxList;

      //End many to many controller code

      ViewBag.ProblemStatementsSiteId = new SelectList(db.Sites, "Id", "SiteName", problemStatement.ProblemStatementsSiteId);
      return View(MyViewModel);
    }

    // POST: ProblemStatements/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ProblemStatementViewModel problemStatement)
    {
      if (ModelState.IsValid)
      {
        var MyProblemStatement = db.ProblemStatements.Find(problemStatement.ProblemStatementID);

        //MyProblemStatement.Name = problemStatement.Name;
        MyProblemStatement.ProblemStatementDescription = problemStatement.ProblemStatementDescription;
        MyProblemStatement.Consequences = problemStatement.Consequences;
        MyProblemStatement.Resources = problemStatement.Resources;
        MyProblemStatement.Gaps = problemStatement.Gaps;

        foreach (var item in db.ProblemStatementToIndxProbStateDataSources)
        {
          if (item.ProblemStatementId == problemStatement.ProblemStatementID)
          {
            db.Entry(item).State = EntityState.Deleted;
          }
        }

        foreach (var item in problemStatement.DataSources)
        {
          if (item.Checked)
          {
            db.ProblemStatementToIndxProbStateDataSources.Add(new ProblemStatementToIndxProbStateDataSource() { ProblemStatementId = problemStatement.ProblemStatementID, IndxProbStateDataSourceId = item.Id });
          }
        }

        db.SaveChanges();
        return RedirectToAction("Index");
      }
      ViewBag.ProblemStatementsSiteId = new SelectList(db.Sites, "Id", "SiteName", problemStatement.ProblemStatementID);
      return View(problemStatement);
    }

    // GET: ProblemStatements/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProblemStatement problemStatement = db.ProblemStatements.Find(id);
      if (problemStatement == null)
      {
        return HttpNotFound();
      }
      return View(problemStatement);
    }

    // POST: ProblemStatements/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      ProblemStatement problemStatement = db.ProblemStatements.Find(id);
      db.ProblemStatements.Remove(problemStatement);
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
