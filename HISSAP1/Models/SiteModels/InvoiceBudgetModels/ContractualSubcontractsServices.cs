using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class ContractualSubcontractsService
  {
    public int Id { get; set; }

    [Display(Name = "Business Individual Name")]
    public string BusinessIndividualName { get; set; }

    [Display(Name = "Services Provided")]
    public string ServicesProvided { get; set; }

    [Display(Name = "Sub-Contract Number")]
    public string SubContractNumber { get; set; }

    public string Comments { get; set; }

    public float Amount { get; set; }
    //Navigation property
    public virtual ICollection<SubcontractualItem> SubcontractsItems { get; set; }
  }

  public class SubcontractualItem
  {
    public Guid Id { get; set; }
    [Display(Name = "Business Individual Name")]
    public string BusinessIndividualName { get; set; }
    [Display(Name = "Services Provided")]
    public string ServicesProvided { get; set; }
    [Display(Name = "Sub-Contract Number")]
    public string SubContractNumber { get; set; }
    public string Comments { get; set; }
    public float Amount { get; set; }
    public int ContractualAdministrativeServiceId { get; set; }
    public virtual ContractualSubcontractsService ContractualSubcontractsService { get; set; }
  }
}