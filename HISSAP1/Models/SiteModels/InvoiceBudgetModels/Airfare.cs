using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class Airfare
  {
    [ForeignKey("Budget")]
    public int Id { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<Traveler> Travelers { get; set; }
  }

  public class Traveler
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Destination { get; set; }
    [Display(Name = "Air Fare")]
    public float AirFare { get; set; }
    public float Transportation { get; set; }
    [Display(Name = "Purpose of Travel")]
    public string PurposeOfTravel { get; set; }
    public int AirfareInterIslandId { get; set; }
    public virtual Airfare Airfare { get; set; }
  }
}