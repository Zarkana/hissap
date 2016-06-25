using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HISSAP1.Models
{
  public class Contract
  {
    public int Id { get; set; }

    [Required]
    [Display(Name = "Contract Name")]
    [MaxLength(100)]
    public string ContractName { get; set; }

    [Required]
    [Display(Name = "Organization")]
    public int ContractsOrganizationId { get; set; }

    //Foreign Key
    [ForeignKey("ContractsOrganizationId")]
    public virtual Organization ContractsOrganization { get; set; }

    [Required]
    [Display(Name = "Contract Number")]
    /*TODO: Add validation*/
    public string ContractNumber { get; set; }

    [Required]
    /*TODO: Add validation*/
    public string Status { get; set; }

    //Navigation property
    public virtual ICollection<ContractFile> ContractFiles { get; set; }
  }

  public class ContractFile
  {
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Extension { get; set; }
    public int ContractId { get; set; }
    public virtual Contract Contract { get; set; }
  }
}