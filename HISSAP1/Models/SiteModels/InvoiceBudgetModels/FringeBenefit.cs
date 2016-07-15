using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class FringeBenefit
  {
    public int Id { get; set; }

    [Display(Name = "Health Insurance")]
    public float HealthInsurance { get; set; }

    public float Retirement { get; set; }

    [Display(Name = "Life & Long-term Disability Insurance")]
    public float LifeLongDisabilityInsurance { get; set; }

    //[Display(Name = "Sum")]
    //public float Sum1 { get; set; }

    //[Display(Name = "Sum")]
    //public float Sum2 { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }
  }
}