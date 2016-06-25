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
      context.Organizations.AddOrUpdate(
        new Organization { Id = 1, Name = "Boys and Girls Club", Line1 = "345 Queen Street, Ste.900", Line2 = "lolol", City = "Honolulu", State = "HI", Zip = "96813", Phone = "(808)-949-4203", Website = "https://www.bgch.com", Email = "ronald@bolub.com", ContactPerson = "Ronald" },
          new Organization { Id = 2, Name = "Alchoholic Rehabilitation Services of Hawaii, Inc", Line1 = "45-845 Po'Okela Street", Line2 = "", City = "Kaneohe", State = "HI", Zip = "96744", Phone = "(808)-236-2600", Website = "http://www.bgch.com/", Email = "moreinfo@hinamauka.org", ContactPerson = "Jim" }
        );

      context.Roles.AddOrUpdate(
          new IdentityRole { Id = "1", Name = "Provider Observer" },
          new IdentityRole { Id = "2", Name = "Provider Agent" },
          new IdentityRole { Id = "3", Name = "Provider Fiscal" },
          new IdentityRole { Id = "4", Name = "Provider Administrator" },
          new IdentityRole { Id = "5", Name = "State Administrator" },
          new IdentityRole { Id = "6", Name = "System Administrator" }
        );

      /*
       First approach
       using the entity framework LINQ to Entity
       */

      //var passwordHash = new PasswordHasher();
      //string password = passwordHash.HashPassword("Hissap123!");

      //var adminUser = new ApplicationUser
      //{
      //  Email = "jsc940@gmail.com",
      //  //EmailConfirmed = false,
      //  PasswordHash = password,
      //  PhoneNumber = "",
      //  SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
      //  //TwoFactorEnabled = false,
      //  //LockoutEndDateUtc = null,
      //  //LockoutEnabled = true,
      //  //AccessFailedCount = 0,
      //  UserName = "admin"
      //};

      //context.Users.AddOrUpdate(u => u.UserName, adminUser);

      /*
       Second approach
       insert a user is using the UserStore
       */

      if (!(context.Users.Any(u => u.UserName == "dj")))
      {
        var userStore = new UserStore<ApplicationUser>(context);
        var userManager = new UserManager<ApplicationUser>(userStore);

        var ProviderObserver = new ApplicationUser {
          UserName = "john",
          Email = "john@gmail.com",
          SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
          PhoneNumber = "0797697898"
        };

        userManager.Create(ProviderObserver, "Hissap123!");
        userManager.AddToRole(ProviderObserver.Id, "Provider Observer");

        var ProviderAdministrator = new ApplicationUser
        {
          UserName = "ron",
          Email = "ronn@gmail.com",
          SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
          PhoneNumber = "",
        };

        userManager.Create(ProviderAdministrator, "Hissap123!");
        userManager.AddToRole(ProviderAdministrator.Id, "Provider Administrator");

        var SystemAdministrator = new ApplicationUser
        {
          UserName = "admin",
          Email = "jsc940@gmail.com",
          SecurityStamp = "7b9fba01-718b-4802-a741-6c1c22bb015b",
          PhoneNumber = "",
        };

        userManager.Create(SystemAdministrator, "Hissap123!");
        userManager.AddToRole(SystemAdministrator.Id, "System Administrator");

      }


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
