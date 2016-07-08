using HISSAP1.Models.SiteModels.InvoiceBudgetModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels
{
  public class Budget
  {
    /* BUDGET INFORMATION */
    public int Id { get; set; }
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
    [RegularExpression("([0-9]+)")]
    //[Display(Name = "Payroll Taxes, Assessments")]
    //public float PayrollTaxesAssessment { get; set; }
    public virtual PayrollTaxesAssessment PayrollTaxesAssessment { get; set; }//TODO: keep?
    [Display(Name = "Fringe Benefits")]
    [RegularExpression("([0-9]+)")]
    public float FringeBenefits { get; set; }
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

    //Navigation property
    public virtual ICollection<PayrollTaxesAssessment> PayrollTaxesAssessments { get; set; }//TODO: I believe need...

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