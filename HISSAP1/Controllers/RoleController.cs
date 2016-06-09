﻿using System;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.EntityFramework;
using HISSAP1.Models;

namespace A11_RBS.Controllers
{
  [Authorize(Roles = "systemAdministrator")]
  public class RoleController : Controller
  {
    ApplicationDbContext context;

    public RoleController()
    {
      context = new ApplicationDbContext();
    }



    /// <summary>
    /// Get All Roles
    /// </summary>
    /// <returns></returns>
    public ActionResult Index()
    {
      var Roles = context.Roles.ToList();
      return View(Roles);
    }

    /// <summary>
    /// Create  a New role
    /// </summary>
    /// <returns></returns>
    public ActionResult Create()
    {
      var Role = new IdentityRole();
      return View(Role);
    }

    /// <summary>
    /// Create a New Role
    /// </summary>
    /// <param name="Role"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Create(IdentityRole Role)
    {
      context.Roles.Add(Role);
      context.SaveChanges();
      return RedirectToAction("Index");
    }


  }
}