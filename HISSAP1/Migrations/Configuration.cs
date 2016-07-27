namespace HISSAP1.Migrations
{
  using Microsoft.AspNet.Identity;
  using Microsoft.AspNet.Identity.EntityFramework;
  using Models;
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<HISSAP1.Models.ApplicationDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
      AutomaticMigrationDataLossAllowed = true;
    }

    protected override void Seed(HISSAP1.Models.ApplicationDbContext context)
    {
      Address Address1 = new Address { AddressLine1 = "74-4489", AddressLine2 = "Apartment #30", Zip = "96740", City = "Honolulu", State = "HI" };
      Address Address2 = new Address { AddressLine1 = "Waimea", AddressLine2 = "", Zip = "96720", City = "Waimea", State = "HI" };
      Address Address3 = new Address { AddressLine1 = "678-987", AddressLine2 = "", Zip = "96740", City = "Hana", State = "HI" };
      Address Address4 = new Address { AddressLine1 = "71-89", AddressLine2 = "Suite 200", Zip = "96740", City = "Hana", State = "HI" };
      Address Address5 = new Address { AddressLine1 = "345 Queen Street, Ste.900", AddressLine2 = "Suite 200", Zip = "96813", City = "Honolulu", State = "HI" };
      Address Address6 = new Address { AddressLine1 = "45-845 Po'Okela Street", AddressLine2 = "Suite 200", Zip = "96744", City = "Kaneohe", State = "HI" };
      Address Address7 = new Address { AddressLine1 = "2969 Mapunapuna Place", AddressLine2 = "Suite 200", Zip = "96819", City = "Honolulu", State = "HI" };


      Provider Provider1 = new Provider
      {
        Name = "Boys and Girls Club",
        Address = Address5,
        //Line1 = "345 Queen Street, Ste.900",
        //Line2 = "",
        //City = "Honolulu",
        //State = "HI",
        //Zip = "96813",
        Phone = "(808)-949-4203",
        Website = "https://www.bgch.com",
        Email = "ronald@bolub.com",
        ContactPerson = "Ronald"
      };

      Provider Provider2 = new Provider
      {
        Name = "Alchoholic Rehabilitation Services of Hawaii, Inc",
        Address = Address6,
        //Line1 = "45-845 Po'Okela Street",
        //Line2 = "",
        //City = "Kaneohe",
        //State = "HI",
        //Zip = "96744",
        Phone = "(808)-236-2600",
        Website = "http://www.bgch.com/",
        Email = "moreinfo@hinamauka.org",
        ContactPerson = "Jim"
      };

      Provider Provider3 = new Provider
      {
        Name = "Alu Like Inc.",
        Address = Address7,
        //Line1 = "2969 Mapunapuna Place",
        //Line2 = "Suite 200",
        //City = "Honolulu",
        //State = "HI",
        //Zip = "96819",
        Phone = "(808)-535-6700",
        Website = "https://www.alulike.org",
        Email = "info@alulike.org",
        ContactPerson = "Val Cribbe"
      };

      context.Providers.AddOrUpdate(Provider1, Provider2, Provider3);

      context.Roles.AddOrUpdate(
          new IdentityRole { Id = "1", Name = "Provider Observer" },
          new IdentityRole { Id = "2", Name = "Provider Agent" },
          new IdentityRole { Id = "3", Name = "Provider Fiscal" },
          new IdentityRole { Id = "4", Name = "Provider Administrator" },
          new IdentityRole { Id = "5", Name = "State Administrator" },
          new IdentityRole { Id = "6", Name = "System Administrator" }
        );

      //if (!(context.Users.Any(u => u.UserName == "admin")))
      //{
        var userStore = new UserStore<ApplicationUser>(context);
        var userManager = new UserManager<ApplicationUser>(userStore);

        var ProviderObserver = new ApplicationUser
        {
          UserName = "john",
          Email = "john@gmail.com",
          SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
          CanPrepareBudget = "No",
          CanSubmitBudget = "No",
          ProviderId = 1
        };

        userManager.Create(ProviderObserver, "Hissap123!");
        userManager.AddToRole(ProviderObserver.Id, "Provider Observer");

        var ProviderAdministrator = new ApplicationUser
        {
          UserName = "ron",
          Email = "ronn@gmail.com",
          SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
          CanPrepareBudget = "No",
          CanSubmitBudget = "Yes",
          ProviderId = 1
        };

        userManager.Create(ProviderAdministrator, "Hissap123!");
        userManager.AddToRole(ProviderAdministrator.Id, "Provider Administrator");

        var SystemAdministrator = new ApplicationUser
        {
          UserName = "admin",
          Email = "jsc940@gmail.com",
          SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
          CanPrepareBudget = "Yes",
          CanSubmitBudget = "Yes",
          ProviderId = 2
        };

        userManager.Create(SystemAdministrator, "Hissap123!");
        userManager.AddToRole(SystemAdministrator.Id, "System Administrator");
      //}
      Contract Contract1 = new Contract
      {
        ContractName = "12-032 | 2016 - 2017",
        ContractsProviderId = Provider1.Id,
        ContractsProvider = Provider1,
        ContractNumber = "12-032",
        Year = "2016-2017",
        Status = "Active"
      };

      Contract Contract2 = new Contract
      {
        ContractName = "12-033 | 2016 - 2017",
        ContractsProviderId = Provider2.Id,
        ContractsProvider = Provider2,
        ContractNumber = "12-033",
        Year = "2016-2017",
        Status = "Active"
      };

      Contract Contract3 = new Contract
      {
        ContractName = "12-034 | 2016 - 2017",
        ContractsProviderId = Provider3.Id,
        ContractsProvider = Provider3,
        ContractNumber = "12-034",
        Year = "2016-2017",
        Status = "Active"
      };

      Contract Contract4 = new Contract
      {
        ContractName = "12-035 | 2016 - 2017",
        ContractsProviderId = Provider1.Id,
        ContractsProvider = Provider1,
        ContractNumber = "12-035",
        Year = "2016-2017",
        Status = "Active"
      };

      Contract Contract5 = new Contract
      {
        ContractName = "12-036 | 2016 - 2017",
        ContractsProviderId = Provider2.Id,
        ContractsProvider = Provider2,
        ContractNumber = "12-036",
        Year = "2016-2017",
        Status = "Active"
      };

      context.Contracts.AddOrUpdate(Contract1, Contract2, Contract3, Contract4, Contract5);

      Site Site1 = new Site
      {
        Id = 1,
        SiteName = "Manoa",
        SitesContractId = Contract1.Id,
        SitesContract = Contract1,
        Address = Address1,
        Status = "Active"
      };

      Site Site2 = new Site
      {
        Id = 2,
        SiteName = "HCC",
        SitesContractId = Contract1.Id,
        SitesContract = Contract1,
        Status = "Active",
        Address = Address2
      };

      Site Site3 = new Site
      {
        Id = 3,
        SiteName = "Waimea",
        SitesContractId = Contract2.Id,
        SitesContract = Contract2,
        Status = "Active",
        Address = Address3
      };

      Site Site4 = new Site
      {
        Id = 4,
        SiteName = "Hana",
        SitesContractId = Contract2.Id,
        SitesContract = Contract3,
        Status = "Active",
        Address = Address4
      };

      Site Site5 = new Site
      {
        Id = 5,
        SiteName = "Atherton",
        SitesContractId = Contract2.Id,
        SitesContract = Contract2,
        Status = "Active",
        Address = Address5
      };

      Site Site6 = new Site
      {
        Id = 6,
        SiteName = "YMCA",
        SitesContractId = Contract3.Id,
        SitesContract = Contract3,
        Status = "Active",
        Address = Address6
      };

      Site Site7 = new Site
      {
        Id = 7,
        SiteName = "Waikiki",
        SitesContractId = Contract3.Id,
        SitesContract = Contract3,
        Status = "Active",
        Address = Address7
      };

      context.Sites.AddOrUpdate(Site1, Site2, Site3, Site4, Site5, Site6, Site7);

      context.CurrentSite.AddOrUpdate(
        new CurrentSite { UserId = ProviderAdministrator.Id, SelectedSite = Site1.Id, User = ProviderAdministrator },
        new CurrentSite { UserId = ProviderObserver.Id, SelectedSite = Site1.Id, User = ProviderObserver },
        new CurrentSite { UserId = SystemAdministrator.Id, SelectedSite = Site1.Id, User = SystemAdministrator }
        );

      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data. E.g.
      //
      //    context.People.AddOrUpdate(
      //      p => p.FullName,
      //      new Person { FullName = "Andrew Peters" },
      //      new Person { FullName = "Brice Lambson" },
      //      new Person { FullName = "Rowan Miller" }
      //    );
      //
    }
  }
}
