using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HISSAP1.Models
{
  public class Organization
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string ContactPerson { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
    public string Email { get; set; }
  }
}
