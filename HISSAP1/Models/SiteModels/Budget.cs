using HISSAP1.Models.SiteModels.InvoiceBudgetModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels
{
  public class Budget
  {
    /* BUDGET INFORMATION */
    public int Id { get; set; }

    public string Name { get; set; }

    //Important variable to determine if budget is simply temporary to allow for overwriting or canceling changes to the original
    //public string Temp { get; set; }

    [Required]
    [Display(Name = "Provider")]
    public int BudgetsSiteId { get; set; }
    
    [ForeignKey("BudgetsSiteId")]//Foreign Key to the parent Site
    public virtual Site BudgetsSite { get; set; }

    [Display(Name = "Date Created")]
    public DateTime DateCreated { get; set; }

    [Required]
    [Display(Name = "ASO Log Number")]
    /*TODO: Add validation*/
    public string ContractNumber { get; set; }

    [Display(Name = "Budget Status")]
    public string BudgetStatus { get; set; }

    [Display(Name = "Total Contract Amount")]
    public float TotalContractAmount { get; set; }

    [Display(Name = "Total Expenses")]
    public float TotalExpenses { get; set; }

    /* BUDGET EXPENSES */

    //A. PERSONNEL COST
    [RegularExpression("([0-9]+)")]
    public float Salary { get; set; }

    [Display(Name = "Payroll Taxes, Assessments")]
    public float PayrollTaxesAssessmentTotal { get; set; }
    //[Display(Name = "Fringe Benefits")]
    //[RegularExpression("([0-9]+)")]
    //public float FringeBenefits { get; set; }

    [Display(Name = "Personnel Cost")]
    [RegularExpression("([0-9]+)")]
    public float PersonnelCost { get; set; }

    //B. OTHER CURRENT EXPENSES

    //C. TRANSPORTATION

    //D. SUBSISTENCE/PER DIEM

    //E. EQUIPMENT PURCHASES

    //F. MOTOR VEHICLE PURCHASES

    /* BUDGET FILES */

    /* TOTAL CONTRACT AMOUNT */

    /* PREPARED BY */

    public virtual FringeBenefit FringeBenefit { get; set; }//TODO: keep?
    //Navigation property
   /* public virtual ICollection<FringeBenefit> FringeBenefits { get; set; }*///TODO: I believe need...
  
    public virtual PayrollTaxesAssessment PayrollTaxesAssessment { get; set; }//TODO: keep?
    //Navigation property
    /*public virtual ICollection<PayrollTaxesAssessment> PayrollTaxesAssessments { get; set; }*///TODO: I believe need...

    //Navigation property
    public virtual ICollection<BudgetFile> BudgetFiles { get; set; }
  }

  public class BudgetFile
  {
    public Guid Id { get; set; }
    public string BudgetFileName { get; set; }
    public string Extension { get; set; }
    public int RecepitId { get; set; }
    //TODO: implement
    //public string title { get; set; }
    //public string note { get; set; }
    public virtual Budget Budget { get; set; }
  }
}