using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models
{
  public class Address
  {
    public int AddressId { get; set; }

    [Required]
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string Zip { get; set; }

    //MAY NEED TO ADD BACK... or not
    /*public virtual Location Location { get; set; }*///TODO: make an abstract class?
  }
}