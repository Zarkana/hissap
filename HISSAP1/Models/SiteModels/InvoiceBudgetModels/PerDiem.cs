using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class PerDiem
  {
    public int Id { get; set; }

    public string Traveler { get; set; }

    [Display(Name = "No. Days")]
    public int NumberOfDays { get; set; }

    [Display(Name = "Per Diem/Subsistence")]
    public string PerDiemSubsistence { get; set; }
  }
}