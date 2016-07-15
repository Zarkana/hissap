using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class PayrollTaxesAssessment
  {
    [ForeignKey("Budget")]
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

    //[Display(Name = "Sum")]
    //public float Sum1 { get; set; }

    //[Display(Name = "Sum")]
    //public float Sum2 { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }

    public virtual Budget Budget { get; set; }//Never seems to be stored?

    //Navigation property
    public virtual ICollection<PayrollItem> PayrollItems { get; set; }
  }

  public class PayrollItem
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public string Justification { get; set; }
    public int PayrollTaxesAssessmentId { get; set; }
    public virtual PayrollTaxesAssessment PayrollTaxesAssessment { get; set; }
  }

}