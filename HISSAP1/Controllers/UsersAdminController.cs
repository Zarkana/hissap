using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using HISSAP1.Models;
using System.Web.Security;
using HISSAP1.CustomFilters;

namespace HISSAP1.Controllers
{
  [Authorization(Roles = "System Administrator, State Administrator, Provider Administrator")]
  public class UsersAdminController : MyBaseController
  {
    public UsersAdminController()
    {
      context = new ApplicationDbContext();
      UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
      RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
    }

    public UsersAdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      UserManager = userManager;
      RoleManager = roleManager;
    }

    public UserManager<ApplicationUser> UserManager { get; private set; }
    public RoleManager<IdentityRole> RoleManager { get; private set; }
    public ApplicationDbContext context { get; private set; }

    //
    // GET: /Users/
    [HttpGet]
    public async Task<ActionResult> Index()
    {
      //TODO: Make into function
      var UserStore = new UserStore<ApplicationUser>(context);
      var UserManager = new UserManager<ApplicationUser>(UserStore);
      var user = UserManager.FindById(User.Identity.GetUserId());

      var users = await UserManager.Users.Where(c => c.ProviderId == user.ProviderId).ToListAsync();

      return View(users);
    }

    //
    // GET: /Users/Details/5
    [HttpGet]
    public async Task<ActionResult> Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var user = await UserManager.FindByIdAsync(id);
      return View(user);
    }

    //
    // GET: /Users/Create
    [HttpGet]
    public async Task<ActionResult> Create()
    {
      //Get the list of Roles
      //ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");
      ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
      //Alternate method
      //ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
      ViewBag.Providers = new SelectList(context.Providers, "Id", "Name");
      return View();
    }

    //
    // POST: /Users/Create
    [HttpPost]
    public async Task<ActionResult> Create(RegisterViewModel model, string RoleId)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser();
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.ProviderId = model.SelectedProvider;//Added
        user.CanPrepareBudget = model.CanPrepareBudget;//Added
        user.CanSubmitBudget = model.CanSubmitBudget;//Added

        var adminresult = await UserManager.CreateAsync(user, model.Password);

        //Add User Admin to Role Admin
        if (adminresult.Succeeded)
        {
          //if (!String.IsNullOrEmpty(RoleId))
          //{

            //Find Role Admin
            //var role = await RoleManager.FindByIdAsync(RoleId);
            //var result = await UserManager.AddToRoleAsync(user.Id, role.Name);
            await UserManager.AddToRoleAsync(user.Id, model.Name);
            //if (!result.Succeeded)
            //{
            //  ModelState.AddModelError("", result.Errors.First().ToString());
            //  ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Id", "Name");
            //  return View();
            //}
          //}
        }
        else
        {
          ModelState.AddModelError("", adminresult.Errors.First().ToString());
          ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");
          return View();

        }
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");
        return View();
      }
    }

    //
    // GET: /Users/Edit/1
    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }     
      ViewBag.Providers = new SelectList(context.Providers, "Id", "Name");

      //Document   
      if (Request.IsAuthenticated)
      {
        //var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        var userRoles = UserManager.GetRoles(User.Identity.GetUserId());
        var role = userRoles[0];
        ViewBag.MyRole = role;
      }

      var user = await UserManager.FindByIdAsync(id);

      string selected = "0";
      foreach ( var item in user.Roles)
      {
        //If the userid of the item in userRoles matches the current users id
        if(item.UserId == user.Id)
        {
          //Put the items roleid into selected
          selected = item.RoleId;
        }
      }

      //Could be a bit wonky
      //Select -> selected 
      //This uses viewbag
      /*ViewBag.RoleId = new SelectList(context.Roles, "Id", "Name", selected);*/
      //This uses view data
      ViewBag.RoleId = new SelectList(context.Roles, "Id", "Name", selected);

      if (user == null)
      {
        return HttpNotFound();
      }
      return View(user);
    }

    //
    // POST: /Users/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit([Bind(Include = "UserName,Id,Email,ProviderId,CanSubmitBudget,CanPrepareBudget")] ApplicationUser formuser, string id, string RoleId)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      //ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");
      var user = await UserManager.FindByIdAsync(id);
      user.UserName = formuser.UserName;
      user.Email = formuser.Email;
      user.ProviderId = formuser.ProviderId;//Added
      user.CanSubmitBudget = formuser.CanSubmitBudget;//Added
      user.CanPrepareBudget = formuser.CanPrepareBudget;//Added
      if (ModelState.IsValid)
      {
        //Update the user details
        await UserManager.UpdateAsync(user);

        //If user has existing Role then remove the user from the role
        // This also accounts for the case when the Admin selected Empty from the drop-down and
        // this means that all roles for the user must be removed
        var rolesForUser = await UserManager.GetRolesAsync(id);
        if (rolesForUser.Count() > 0)
        {
          foreach (var item in rolesForUser)
          {
            var result = await UserManager.RemoveFromRoleAsync(id, item);
          }
        }

        if (!String.IsNullOrEmpty(RoleId))
        {
          //Find Role
          var role = await RoleManager.FindByIdAsync(RoleId);
          //Add user to new role
          var result = await UserManager.AddToRoleAsync(id, role.Name);
          if (!result.Succeeded)
          {
            ModelState.AddModelError("", result.Errors.First().ToString());
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");
            return View();
          }
        }
        return RedirectToAction("Index");
      }
      else
      {
        ViewBag.RoleId = new SelectList(RoleManager.Roles, "Id", "Name");
        return View();
      }
    }

    // GET: /Users/Delete/5
    [HttpGet]
    public async Task<ActionResult> Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      var user = await UserManager.FindByIdAsync(id);
      if (user == null)
      {
        return HttpNotFound();
      }
      return View(user);
    }


    // POST: /Users/Delete/5
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
        //Made in identity 1.0
        //var user = await context.Users.FindAsync(id);
        var user = await UserManager.FindByIdAsync(id);
        var logins = user.Logins;
        //Made in identity 1.0
        //foreach (var login in logins)
        foreach (var login in logins.ToList())
        {
          //Made in identity 1.0
          //context.UserLogins.Remove(login);
          await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
        }
        //Made in identity 1.0
        //var rolesForUser = await IdentityManager.Roles.GetRolesForUserAsync(id, CancellationToken.None);
        var rolesForUser = await UserManager.GetRolesAsync(id);
        if (rolesForUser.Count() > 0)
        {

          //Made in identity 1.0        
          //foreach (var item in rolesForUser)
          foreach (var item in rolesForUser.ToList())
          {
            //Made in identity 1.0
            //var result = await IdentityManager.Roles.RemoveUserFromRoleAsync(user.Id, item.Id, CancellationToken.None);
            var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
          }
        }
        await UserManager.DeleteAsync(user);
        //Made in identity 1.0
        //await context.SaveChangesAsync();
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

  }
}