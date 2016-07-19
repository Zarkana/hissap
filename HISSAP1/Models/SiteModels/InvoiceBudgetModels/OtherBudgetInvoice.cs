using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class OtherBudgetInvoice
  {
    [ForeignKey("Budget")]
    public int Id { get; set; }

    [Display(Name = "Sum Total")]
    public float SumTotal { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<OtherItem> OtherItems { get; set; }
  }

  public class OtherItem
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public string Justification { get; set; }
    public int OtherBudgetInvoiceId { get; set; }
    public virtual OtherBudgetInvoice OtherBudgetInvoice { get; set; }
  }
}