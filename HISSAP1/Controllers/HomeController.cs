using HISSAP1.CustomFilters;
using HISSAP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Controllers
{
  [Authorize]
  public class HomeController : MyBaseController
  {
    private ApplicationDbContext db = new ApplicationDbContext();

    //TODO: Add content
    public ActionResult Index()
    {
      return View();
    }

    //TODO: remove?
    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }


    //TODO: remove?
    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    [Authorization(Roles = "System Administrator, State Administrator, Provider Administrator")]
    public ActionResult Administration()
    {
      //ViewBag.Message = "Your administration page.";

      return View();
    }
  }
}