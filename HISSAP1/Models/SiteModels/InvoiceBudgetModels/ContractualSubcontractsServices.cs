using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class ContractualSubcontractsService
  {
    [ForeignKey("Budget")]
    public int Id { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<SubcontractsItem> SubcontractsItems { get; set; }
  }

  public class SubcontractsItem
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
    public int ContractualSubcontractsServiceId { get; set; }
    public virtual ContractualSubcontractsService ContractualSubcontractsService { get; set; }
  }
}