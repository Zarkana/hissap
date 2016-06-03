namespace HISSAP1.Migrations
{
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
      context.Organizations.AddOrUpdate(
        new Organization { ID = 1, Name = "Boys and Girls Club", Line1 = "345 Queen Street, Ste.900", Line2 = "lolol", City = "Honolulu", State = "HI", Zip = "96813", Phone = "(808)-949-4203", Website = "https://www.bgch.com", Email = "ronald@bolub.com", ContactPerson = "Ronald" },
          new Organization { ID = 2, Name = "Alchoholic Rehabilitation Services of Hawaii, Inc", Line1 = "45-845 Po'Okela Street", Line2 = "", City = "Kaneohe", State = "HI", Zip = "96744", Phone = "(808)-236-2600", Website = "http://www.bgch.com/", Email = "moreinfo@hinamauka.org", ContactPerson = "Jim" }
        );

      context.Roles.AddOrUpdate(
          new IdentityRole { Id = "1", Name = "providerObserver" },
          new IdentityRole { Id = "2", Name = "providerAgent" },
          new IdentityRole { Id = "3", Name = "providerFiscal" },
          new IdentityRole { Id = "4", Name = "providerAdministrator" },
          new IdentityRole { Id = "5", Name = "stateAdministrator" },
          new IdentityRole { Id = "6", Name = "systemAdministrator" }
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
