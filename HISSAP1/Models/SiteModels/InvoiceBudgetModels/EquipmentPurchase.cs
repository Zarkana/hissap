using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels.InvoiceBudgetModels
{
  public class EquipmentPurchase
  {
    public int Id { get; set; }

    [Display(Name = "Equipment Description")]
    public string EquipmentDescription { get; set; }

    [Display(Name = "No. of Items")]
    public int NumberOfItems { get; set; }

    [Display(Name = "Cost Per Item")]
    public float CostPerItem { get; set; }

    public string Justification { get; set; }
  }
}