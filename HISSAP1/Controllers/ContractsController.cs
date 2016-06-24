﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HISSAP1.Models;
using System.IO;

namespace HISSAP1.Controllers
{
  public class ContractsController : Controller
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    // GET: Contracts
    public ActionResult Index()
    {
      return View(db.Contracts.ToList());
    }

    // GET: Contracts/Details/5
    //public ActionResult Details(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Contract contract = db.Contracts.Find(id);
    //  if (contract == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(contract);
    //}

    // GET: Contracts/Create
    public ActionResult Create()
    {
      ViewBag.Organizations = new SelectList(db.Organizations, "Id", "Name");
      return View();
    }

    // POST: Contracts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Contract contract)
    {
      if (ModelState.IsValid)
      {
        List<ContractFile> fileDetails = new List<ContractFile>();
        for (int i = 0; i < Request.Files.Count; i++)
        {
          var file = Request.Files[i];

          if (file != null && file.ContentLength > 0)
          {
            var fileName = Path.GetFileName(file.FileName);
            ContractFile fileDetail = new ContractFile()
            {
              FileName = fileName,
              Extension = Path.GetExtension(fileName),
              Id = Guid.NewGuid()
            };
            fileDetails.Add(fileDetail);

            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
            file.SaveAs(path);
          }
        }

        contract.ContractFiles = fileDetails;
        db.Contracts.Add(contract);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      return View(contract);
    }

    // GET: Contracts/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Contract contract = db.Contracts.Include(s => s.ContractFiles).SingleOrDefault(x => x.Id == id);
      if (contract == null)
      {
        return HttpNotFound();
      }
      ViewBag.Organizations = new SelectList(db.Organizations, "Id", "Name");
      return View(contract);
    }

    // POST: Contracts/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Contract contract)
    {
      if (ModelState.IsValid)
      {

        //New Files
        for (int i = 0; i < Request.Files.Count; i++)
        {
          var file = Request.Files[i];

          if (file != null && file.ContentLength > 0)
          {
            var fileName = Path.GetFileName(file.FileName);
            ContractFile fileDetail = new ContractFile()
            {
              FileName = fileName,
              Extension = Path.GetExtension(fileName),
              Id = Guid.NewGuid(),
              ContractId = contract.Id/*Had to name ContractId to differentiate from Id, will likely not impact anything*/
            };
            var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
            file.SaveAs(path);

            db.Entry(fileDetail).State = EntityState.Added;
          }
        }

        db.Entry(contract).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(contract);
    }



    [HttpPost]
    public JsonResult DeleteFile(string id)
    {
      if (String.IsNullOrEmpty(id))
      {
        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return Json(new { Result = "Error" });
      }
      try
      {
        Guid guid = new Guid(id);
        ContractFile fileDetail = db.ContractFiles.Find(guid);
        if (fileDetail == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //Remove from database
        db.ContractFiles.Remove(fileDetail);
        db.SaveChanges();

        //Delete file from the file system
        var path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), fileDetail.Id + fileDetail.Extension);
        if (System.IO.File.Exists(path))
        {
          System.IO.File.Delete(path);
        }
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
    }

    //TODO Evaluate whether or not to have get page
    // GET: Contracts/Delete/5
    //public ActionResult Delete(int? id)
    //{
    //  if (id == null)
    //  {
    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //  }
    //  Contract contract = db.Contracts.Find(id);
    //  if (contract == null)
    //  {
    //    return HttpNotFound();
    //  }
    //  return View(contract);
    //}

    // POST: Contracts/Delete/5


    public FileResult Download(String p, String d)
    {
      return File(Path.Combine(Server.MapPath("~/App_Data/Upload/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
    }

    [HttpPost]
    public JsonResult Delete(int id)
    {
      try
      {
        Contract support = db.Contracts.Find(id);
        if (support == null)
        {
          Response.StatusCode = (int)HttpStatusCode.NotFound;
          return Json(new { Result = "Error" });
        }

        //delete files from the file system

        foreach (var item in support.ContractFiles)
        {
          String path = Path.Combine(Server.MapPath("~/App_Data/Upload/"), item.Id + item.Extension);
          if (System.IO.File.Exists(path))
          {
            System.IO.File.Delete(path);
          }
        }

        db.Contracts.Remove(support);
        db.SaveChanges();
        return Json(new { Result = "OK" });
      }
      catch (Exception ex)
      {
        return Json(new { Result = "ERROR", Message = ex.Message });
      }
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