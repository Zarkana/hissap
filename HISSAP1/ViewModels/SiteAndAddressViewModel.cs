using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HISSAP1.Models
{
  public class SiteAndAddressViewModel
  {
    public Site Site { get; set; }
    public Address Address { get; set; }
  }

  public class SitesAndAddressesViewModel
  {
    public IEnumerable<Site> Sites { get; set; }
    public IEnumerable<Address> Addresses { get; set; }
  }

}