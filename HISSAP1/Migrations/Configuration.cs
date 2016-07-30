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
      //SEED ADDRESSES

      Address Address1 = new Address { AddressLine1 = "74-4489", AddressLine2 = "Apartment #30", Zip = "96740", City = "Honolulu", State = "HI" };
      Address Address2 = new Address { AddressLine1 = "Waimea", AddressLine2 = "", Zip = "96720", City = "Waimea", State = "HI" };
      Address Address3 = new Address { AddressLine1 = "678-987", AddressLine2 = "", Zip = "96740", City = "Hana", State = "HI" };
      Address Address4 = new Address { AddressLine1 = "71-89", AddressLine2 = "Suite 200", Zip = "96740", City = "Hana", State = "HI" };
      Address Address5 = new Address { AddressLine1 = "345 Queen Street, Ste.900", AddressLine2 = "Suite 200", Zip = "96813", City = "Honolulu", State = "HI" };
      Address Address6 = new Address { AddressLine1 = "45-845 Po'Okela Street", AddressLine2 = "Suite 200", Zip = "96744", City = "Kaneohe", State = "HI" };
      Address Address7 = new Address { AddressLine1 = "2969 Mapunapuna Place", AddressLine2 = "Suite 200", Zip = "96819", City = "Honolulu", State = "HI" };
      Address Address8 = new Address { AddressLine1 = "601 Kamokila Blvd #360", AddressLine2 = "", Zip = "96707", City = "Kapolei", State = "HI" };

      //SEED PROVIDERS

      Provider Provider1 = new Provider
      {
        Name = "ADAD System",
        Address = Address8,
        Phone = "(808)-692-7506",
        Website = "http://health.hawaii.gov/substance-abuse/",
        Email = "webmail@doh.hawaii.gov",
        ContactPerson = "Person"
      };

      Provider Provider2 = new Provider
      {
        Name = "Boys and Girls Club",
        Address = Address5,
        Phone = "(808)-949-4203",
        Website = "https://www.bgch.com",
        Email = "ronald@bolub.com",
        ContactPerson = "Ronald"
      };

      Provider Provider3 = new Provider
      {
        Name = "Alchoholic Rehabilitation Services of Hawaii, Inc",
        Address = Address6,
        Phone = "(808)-236-2600",
        Website = "http://www.bgch.com/",
        Email = "moreinfo@hinamauka.org",
        ContactPerson = "Jim"
      };

      Provider Provider4 = new Provider
      {
        Name = "Alu Like Inc.",
        Address = Address7,
        Phone = "(808)-535-6700",
        Website = "https://www.alulike.org",
        Email = "info@alulike.org",
        ContactPerson = "Val Cribbe"
      };

      context.Providers.AddOrUpdate(Provider1, Provider2, Provider3, Provider4);

      //SEED ROLES

      context.Roles.AddOrUpdate(
          new IdentityRole { Id = "1", Name = "Provider Observer" },
          new IdentityRole { Id = "2", Name = "Provider Staff" },
          new IdentityRole { Id = "3", Name = "Provider Fiscal" },
          new IdentityRole { Id = "4", Name = "Provider Administrator" },
          new IdentityRole { Id = "5", Name = "State Administrator" },
          new IdentityRole { Id = "6", Name = "System Administrator" }
        );

      //if (!(context.Users.Any(u => u.UserName == "admin")))
      //{

      //SEED USERS

      var userStore = new UserStore<ApplicationUser>(context);
      var userManager = new UserManager<ApplicationUser>(userStore);

      //TODO: Figure out what to do with Security Stamp
      var ProviderObserver = new ApplicationUser
      {
        UserName = "providerobserver",
        Email = "jsc940@hawaii.edu",
        SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
        CanPrepareBudget = "No",
        CanSubmitBudget = "No",
        ProviderId = Provider1.Id
      };

      userManager.Create(ProviderObserver, "Hissap123!");
      userManager.AddToRole(ProviderObserver.Id, "Provider Observer");

      var ProviderStaff = new ApplicationUser
      {
        UserName = "providerstaff",
        Email = "jsc940@hawaii.edu",
        SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
        CanPrepareBudget = "No",
        CanSubmitBudget = "No",
        ProviderId = Provider1.Id
      };

      userManager.Create(ProviderStaff, "Hissap123!");
      userManager.AddToRole(ProviderStaff.Id, "Provider Staff");


      var ProviderFiscal = new ApplicationUser
      {
        UserName = "providerfiscal",
        Email = "jsc940@hawaii.edu",
        SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
        CanPrepareBudget = "Yes",
        CanSubmitBudget = "No",
        ProviderId = Provider1.Id
      };

      userManager.Create(ProviderFiscal, "Hissap123!");
      userManager.AddToRole(ProviderFiscal.Id, "Provider Fiscal");

      var ProviderAdministrator = new ApplicationUser
      {
        UserName = "provideradmin",
        Email = "jsc940@hawaii.edu",
        SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
        CanPrepareBudget = "Yes",
        CanSubmitBudget = "Yes",
        ProviderId = Provider1.Id
      };

      userManager.Create(ProviderAdministrator, "Hissap123!");
      userManager.AddToRole(ProviderAdministrator.Id, "Provider Administrator");

      var StateAdministrator = new ApplicationUser
      {
        UserName = "stateadmin",
        Email = "jsc940@hawaii.edu",
        SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
        CanPrepareBudget = "No",
        CanSubmitBudget = "No",
        ProviderId = Provider1.Id
      };

      userManager.Create(StateAdministrator, "Hissap123!");
      userManager.AddToRole(StateAdministrator.Id, "State Administrator");


      var SystemAdministrator = new ApplicationUser
      {
        UserName = "systemadmin",
        Email = "jsc940@hawaii.edu",
        SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
        CanPrepareBudget = "Yes",
        CanSubmitBudget = "Yes",
        ProviderId = Provider1.Id
      };

      userManager.Create(SystemAdministrator, "Hissap123!");
      userManager.AddToRole(SystemAdministrator.Id, "System Administrator");
      //}

      //SEED CONTRACTS

      Contract Contract1 = new Contract
      {
        ContractName = "12-032 | 2016 - 2017",
        ContractsProviderId = Provider2.Id,
        ContractsProvider = Provider2,
        ContractNumber = "12-032",
        Year = "2016-2017",
        Status = "Active"
      };

      Contract Contract2 = new Contract
      {
        ContractName = "12-033 | 2016 - 2017",
        ContractsProviderId = Provider3.Id,
        ContractsProvider = Provider3,
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
        ContractsProviderId = Provider2.Id,
        ContractsProvider = Provider2,
        ContractNumber = "12-035",
        Year = "2016-2017",
        Status = "Active"
      };

      Contract Contract5 = new Contract
      {
        ContractName = "12-036 | 2016 - 2017",
        ContractsProviderId = Provider3.Id,
        ContractsProvider = Provider3,
        ContractNumber = "12-036",
        Year = "2016-2017",
        Status = "Active"
      };

      context.Contracts.AddOrUpdate(Contract1, Contract2, Contract3, Contract4, Contract5);

      //SEED SITES

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
        new CurrentSite { UserId = ProviderObserver.Id, SelectedSite = Site1.Id, User = ProviderObserver },
        new CurrentSite { UserId = ProviderStaff.Id, SelectedSite = Site1.Id, User = ProviderStaff },
        new CurrentSite { UserId = ProviderFiscal.Id, SelectedSite = Site1.Id, User = ProviderFiscal },
        new CurrentSite { UserId = ProviderAdministrator.Id, SelectedSite = Site1.Id, User = ProviderAdministrator },
        new CurrentSite { UserId = StateAdministrator.Id, SelectedSite = Site1.Id, User = StateAdministrator },
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
