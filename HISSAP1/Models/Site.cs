using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models
{
  public class Site
  {
    public int Id { get; set; }

    [Required]
    [Display(Name = "Site Name")]
    [MaxLength(100)]
    public string SiteName { get; set; }

    [Required]
    [Display(Name = "Contract")]
    public int SitesContractId { get; set; }

    //Foreign Key to the parent contract
    [ForeignKey("SitesContractId")]
    public virtual Contract SitesContract { get; set; }

    [Required]
    /*TODO: Add validation*/
    public string Status { get; set; }

    public virtual Address Address { get; set; }//TODO: keep?
    //Navigation property
    public virtual ICollection<Address> Addresses { get; set; }

    public virtual SiteContact SiteContact { get; set; }//TODO: keep?
    //Navigation property
    public virtual ICollection<SiteContact> SiteContacts { get; set; }

  }

  public class SiteContact
  {
    //TODO: validation
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string WorkPhone { get; set; }
    public string Email { get; set; }
    public virtual Site Site { get; set; }
  }
}