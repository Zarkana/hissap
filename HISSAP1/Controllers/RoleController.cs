using System;
using System.Linq;
using System.Web.Mvc;

using Microsoft.AspNet.Identity.EntityFramework;
using HISSAP1.Models;
using HISSAP1.CustomFilters;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net;

namespace A11_RBS.Controllers
{
  //[AuthLog(Roles = "systemAdministrator")]
  public class RoleController : Controller
  {
    ApplicationDbContext context;
    //May need to remove
    public UserManager<ApplicationUser> UserManager { get; private set; }
    public RoleManager<IdentityRole> RoleManager { get; private set; }

    public RoleController()
    {
      context = new ApplicationDbContext();
      UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
      RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
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

    //
    // GET: /Roles/Edit/Admin
    public async Task<ActionResult> Edit(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var role = await RoleManager.FindByIdAsync(id);
      if (role == null)
      {
        return HttpNotFound();
      }
      return View(role);
    }

    //
    // POST: /Roles/Edit/5
    [HttpPost]

    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] IdentityRole role)
    {
      if (ModelState.IsValid)
      {
        var result = await RoleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
          ModelState.AddModelError("", result.Errors.First().ToString());
          return View();
        }
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    //
    // GET: /Roles/Delete/5
    public async Task<ActionResult> Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var role = await RoleManager.FindByIdAsync(id);
      if (role == null)
      {
        return HttpNotFound();
      }
      return View(role);
    }

    //
    // POST: /Roles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
      if (ModelState.IsValid)
      {
        if (id == null)
        {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        var role = await RoleManager.FindByIdAsync(id);
        var result = await RoleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
          ModelState.AddModelError("", result.Errors.First().ToString());
          return View();
        }
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

  }
}