using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HISSAP1.Models
{
  public class CurrentSite
  {
    [Key, ForeignKey("User")]//Acts as both the key to the table, and foreign key to users
    public string UserId { get; set; }

    //TODO: Set by default
    //[Required]
    //[Display(Name = "Provider")]
    //public int SelectedProvider { get; set; }

    //[Required]
    //[Display(Name = "Contract")]
    //public int SelectedContract { get; set; }

    [Required]
    [Display(Name = "Site")]
    public int SelectedSite { get; set; }

    //[ForeignKey("SelectedProvider")]
    //public virtual Provider Provider { get; set; }

    //[ForeignKey("SelectedContract")]
    //public virtual Contract Contract { get; set; }

    [ForeignKey("SelectedSite")]
    public virtual Site Site { get; set; }

    public virtual ApplicationUser User { get; set; }
  }

  public class MyContext : DbContext
  {

    //TODO: Evaluate, does not use base.OnModelCreating... may not need
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      
      modelBuilder.Entity<ApplicationUser>()
          .HasOptional(u => u.CurrentSite)
          .WithRequired(s => s.User);
    }


    //TODO: Should this be here? Or in ApplicationDbContext...
    public virtual DbSet<ApplicationUser> User { get; set; }
    public virtual DbSet<CurrentSite> CurrentSite { get; set; }
  }

}