using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
    [Display(Name = "Provider")]
    public int ContractsProviderId { get; set; }

    //Foreign Key to the parent provider
    [ForeignKey("ContractsProviderId")]
    public virtual Provider ContractsProvider { get; set; }

    [Required]
    [Display(Name = "ASO Log Number")]
    /*TODO: Add validation*/
    public string ContractNumber { get; set; }

    [Required]
    [Display(Name = "Year")]
    public int Year { get; set; }

    [Required]
    /*TODO: Add validation*/
    public string Status { get; set; }

    //Navigation property
    public virtual ICollection<ContractFile> ContractFiles { get; set; }

    //Navigation property
    public virtual ICollection<Site> Sites { get; set; }
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