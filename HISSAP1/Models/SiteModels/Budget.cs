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

    [Display(Name = "Fringe Benefits")]
    public float FringeBenefitsTotal { get; set; }

    [Display(Name = "Personnel Cost")]
    public float PersonnelCost { get; set; }

    //B. OTHER CURRENT EXPENSES
    [Display(Name = "Audit Service")]
    public float AuditService { get; set; }

    [Display(Name = "Contractual Services - Administrative")]
    public float ContractualAdministrativeServicesTotal { get; set; }

    [Display(Name = "Contractual Services - Subcontracts")]
    public float ContractualSubcontractsServicesTotal { get; set; }

    public float Insurance { get; set; }

    [Display(Name = "Lease/Rental of Equipment")]
    public float LeaseRentalEquipment { get; set; }

    [Display(Name = "Lease/Rental of Motor Vehicle")]
    public float LeaseRentalMotorVehicle { get; set; }

    [Display(Name = "Lease/Rental of Space")]
    public float LeaseRentalSpace { get; set; }

    public float Mileage { get; set; }

    [Display(Name = "Postage, Freight, Delivery")]
    public float PostageFreightDelivery { get; set; }

    [Display(Name = "Publication, Printing")]
    public float PublicationPrinting { get; set; }

    [Display(Name = "Repair, Maintenance")]
    public float RepairMaintenance { get; set; }

    [Display(Name = "Staff Training")]
    public float StaffTraining { get; set; }

    public float Supplies { get; set; }

    public float Telecommunication { get; set; }

    public float Utilities { get; set; }

    [Display(Name = "Program Activities")]
    public float ProgramActivities { get; set; }

    [Display(Name = "Indirect Cost")]
    public float IndirectCost { get; set; }

    [Display(Name = "Other")]
    public float OtherTotal { get; set; }

    [Display(Name = "Other Current Expenses")]
    public float OtherCurrentExpenses { get; set; }

    //C. TRANSPORTATION

    [Display(Name = "Airfare, Inter-Island")]
    public float AirfareInterIslandTotal { get; set; }

    [Display(Name = "Airfare, Out-of-State")]
    public float AirfareOutStateTotal { get; set; }

    public float Transportation { get; set; }

    //D. SUBSISTENCE/PER DIEM

    [Display(Name = "Subsistence/Per Diem")]
    public float SubsistencePerDiemTotal { get; set; }

    //E. EQUIPMENT PURCHASES

    [Display(Name = "Equipment Purchases")]
    public float EquipmentPurchasesTotal { get; set; }

    //F. MOTOR VEHICLE PURCHASES

    [Display(Name = "Motor Vehicle Purchases")]
    public float MotorVehiclePurchasesTotal { get; set; }

    public float Total { get; set; }

    /* BUDGET FILES */

    /* TOTAL CONTRACT AMOUNT */

    /* PREPARED BY */

    public virtual PayrollTaxesAssessment PayrollTaxesAssessment { get; set; }
    public virtual FringeBenefit FringeBenefit { get; set; }
    public virtual ContractualAdministrativeService AdministrativeContractualService { get; set; }
    public virtual ContractualSubcontractsService ContractualSubcontractsService { get; set; }
    public virtual OtherBudgetInvoice OtherBudgetInvoice { get; set; }
    public virtual AirfareInterIsland AirfareInterIsland { get; set; }
    public virtual AirfareOutOfState AirfareOutOfState { get; set; }
    public virtual SubsistencePerDiem SubsistencePerDiem { get; set; }
    public virtual EquipmentPurchase EquipmentPurchase { get; set; }

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