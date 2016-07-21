using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class SubsistencePerDiem
  {
    [ForeignKey("Budget")]
    public int Id { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<SubsistencePerDiemItem> SubsistencePerDiemItems { get; set; }

    //Navigation property
    public virtual ICollection<Traveler> Travelers { get; set; }
  }

  public class SubsistencePerDiemItem
  {
    public Guid Id { get; set; }
    [Display(Name = "No. Days")]
    public int NumberOfDays { get; set; }
    [Display(Name = "Per Diem/Subsistence")]
    public float SubsistencePerDiemAmount { get; set; }

    [Required]
    public Guid TravelerId { get; set; }
    //Foreign Key to the parent SubsistencePerDiem
    [ForeignKey("TravelerId")]
    public virtual Traveler Traveler { get; set; }

    public int SubsistencePerDiemId { get; set; }
    public virtual SubsistencePerDiem SubsistencePerDiem { get; set; }
  }
}