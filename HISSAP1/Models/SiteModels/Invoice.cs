using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models.SiteModels
{
  public class Invoice
  {
    public int Id { get; set; }


    //Navigation property
    public virtual ICollection<Receipt> Receipts { get; set; }
  }

  public class Receipt
  {
    public Guid Id { get; set; }
    public string ReceiptName { get; set; }
    public string Extension { get; set; }
    public int RecepitId { get; set; }
    //TODO: implement
    //public string title { get; set; }
    //public string note { get; set; }
    public virtual Budget Budget { get; set; }
  }
}