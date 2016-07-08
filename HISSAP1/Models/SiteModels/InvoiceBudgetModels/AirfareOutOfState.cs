using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class AirfareOutOfState
  {
    public int Id { get; set; }

    public string Traveler { get; set; }

    public string Destination { get; set; }

    [Display(Name = "Air Fare")]
    public float AirFare { get; set; }

    public float Transportation { get; set; }

    [Display(Name = "Purpose of Travel")]
    public string PurposeOfTravel { get; set; }
  }
}