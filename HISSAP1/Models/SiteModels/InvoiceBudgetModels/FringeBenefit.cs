using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class FringeBenefit
  {
    [ForeignKey("Budget")]
    public int Id { get; set; }

    [Display(Name = "Health Insurance")]
    public float HealthInsurance { get; set; }

    public float Retirement { get; set; }

    [Display(Name = "Life & Long-term Disability Insurance")]
    public float LifeLongDisabilityInsurance { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<FringeItem> FringeItems { get; set; }
  }

  public class FringeItem
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public string Justification { get; set; }
    public int FringeBenefitId { get; set; }
    public virtual FringeBenefit FringeBenefit { get; set; }
  }

}