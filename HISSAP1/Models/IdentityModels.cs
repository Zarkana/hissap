using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace HISSAP1.Models
{
  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Add custom user claims here
      return userIdentity;
    }
    public int ProviderId { get; set; }//Added to allow for Providers...

    [Required]
    [Display(Name = "Can Submit Budget")]
    public string CanSubmitBudget { get; set; }

    [Required]
    [Display(Name = "Can Prepare Budget")]
    public string CanPrepareBudget { get; set; }

    public virtual CurrentSite CurrentSite { get; set; }//Added to allow for user settings                                                   
  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }

    public DbSet<Provider> Providers { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractFile> ContractFiles { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<SiteContact> SiteContacts { get; set; }
    public DbSet<Address> Address { get; set; }

    public DbSet<CurrentSite> CurrentSite { get; set; }


    //PROBLEM STATEMENT

    public DbSet<SiteModels.ProblemStatement> ProblemStatements { get; set; }
    public DbSet<SiteModels.ProblemStatementModels.IndxProbStateDataSource> IndxProbStateDataSources { get; set; }
    public DbSet<HISSAP1.Models.SiteModels.ProblemStatementModels.ProblemStatementToIndxProbStateDataSource> ProblemStatementToIndxProbStateDataSources { get; set; }

    //OUTCOMES

    public DbSet<SiteModels.Outcome> Outcomes { get; set; }
    public DbSet<SiteModels.OutcomeModels.IndxOutcomeType> IndxOutcomeTypes { get; set; }
    public DbSet<HISSAP1.Models.SiteModels.OutcomeModels.OutcomeToIndxOutcomeType> OutcomeToIndxOutcomeTypes { get; set; }
    public DbSet<SiteModels.OutcomeModels.IndxMeasurementTool> IndxMeasurementTools { get; set; }
    public DbSet<HISSAP1.Models.SiteModels.OutcomeModels.OutcomeToIndxMeasurementTool> OutcomeToIndxMeasurementTools { get; set; }
    public DbSet<SiteModels.OutcomeModels.IndxMeasurementType> IndxMeasurementTypes { get; set; }
    public DbSet<HISSAP1.Models.SiteModels.OutcomeModels.OutcomeToIndxMeasurementType> OutcomeToIndxMeasurementTypes { get; set; }

    //BUDGET

    public DbSet<SiteModels.Budget> Budgets { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.PayrollTaxesAssessment> PayrollTaxesAssessments { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.PayrollItem> PayrollItems { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.FringeBenefit> FringeBenefits { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.FringeItem> FringeItems { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.ContractualAdministrativeService> ContractualAdministrativeServices { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.AdministrativeItem> AdministrativeItems { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.ContractualSubcontractsService> ContractualSubcontractsServices { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.SubcontractsItem> SubcontractsItems { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.OtherBudgetInvoice> OtherBudgetInvoices { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.OtherItem> OtherItems { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.Airfare> Airfares { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.Traveler> Travelers { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.SubsistencePerDiem> SubsistencePerDiems { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.SubsistencePerDiemItem> SubsistencePerDiemItems { get; set; }

    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.EquipmentPurchase> EquipmentPurchases { get; set; }
    public System.Data.Entity.DbSet<HISSAP1.Models.SiteModels.InvoiceBudgetModels.EquipmentItem> EquipmentItems { get; set; }
  }
}