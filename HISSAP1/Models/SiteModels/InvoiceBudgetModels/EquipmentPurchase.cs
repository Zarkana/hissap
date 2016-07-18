using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class EquipmentPurchase
  {
    [ForeignKey("Budget")]
    public int Id { get; set; }

    public virtual Budget Budget { get; set; }

    //Navigation property
    public virtual ICollection<EquipmentItem> EquipmentItems { get; set; }
  }

  public class EquipmentItem
  {
    public Guid Id { get; set; }
    [Display(Name = "Equipment Description")]
    public string EquipmentDescription { get; set; }
    [Display(Name = "Number of Items")]
    public int NumberItems { get; set; }
    [Display(Name = "Cost Per Item")]
    public float CostPerItem { get; set; }
    public string Justification { get; set; }
    public int EquipmentPurchaseId { get; set; }
    public virtual EquipmentPurchase EquipmentPurchase { get; set; }
  }
}