using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class SubsistencePerDiem
  {
    public int Id { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<SubsistencePerDiemItem> SubsistencePerDiemItems { get; set; }
  }

  public class SubsistencePerDiemItem
  {
    public Guid Id { get; set; }
    [Display(Name = "No. Days")]
    public int NumberOfDays { get; set; }
    [Display(Name = "Per Diem/Subsistence")]
    public string SubsistencePerDiem { get; set; }
    public int TravelerId { get; set; }
    public virtual Traveler Traveler{ get; set; }
  }
}