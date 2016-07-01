using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Models
{
  public class CurrentSite
  {
    [Key, ForeignKey("User")]
    public string UserId { get; set; }
    //Named CurrentSite to possibly not conflict with Id of the User

    //TODO: Set by default
    [Required]
    [Display(Name = "Provider")]
    public int ContractsProviderId { get; set; }

    public int SelectedContract { get; set; }
    public int SelectedSite { get; set; }

    //Storing lists
    [ForeignKey("ContractsProviderId")]
    public virtual Provider ContractsProvider { get; set; }
    //public IEnumerable<SelectListItem> ContractList { get; set; }
    //public IEnumerable<SelectListItem> SiteList { get; set; }

    public virtual ApplicationUser User { get; set; }
  }

  public class MyContext : DbContext
  {

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<ApplicationUser>()
                      .HasOptional(u => u.CurrentSite)
                      .WithRequired(s => s.User);
    }

    public virtual DbSet<ApplicationUser> User { get; set; }
    public virtual DbSet<CurrentSite> CurrentSite { get; set; }
  }

}