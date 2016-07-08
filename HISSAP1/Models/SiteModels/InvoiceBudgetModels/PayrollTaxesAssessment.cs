using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class PayrollTaxesAssessment
  {
    public int Id { get; set; }

    [Display(Name = "Social Security")]
    public float SocialSecurity { get; set; }

    [Display(Name = "Unemployment Insurance (Federal)")]
    public float UnemploymentInsuranceFederal { get; set; }

    [Display(Name = "Unemployment Insurance (State)")]
    public float UnemploymentInsuranceState { get; set; }

    [Display(Name = "Worker's Compensation")]
    public float WorkersCompensation { get; set; }

    [Display(Name = "Temporary Disability Insurance")]
    public float TemporaryDisabilityInsurance { get; set; }

    [Display(Name = "Sum")]
    public float Sum1 { get; set; }

    [Display(Name = "Sum")]
    public float Sum2 { get; set; }

    //MAY NEED TO ADD BACK... or not
    public virtual Budget Budget { get; set; }//TODO: make an abstract class?    
  }
}