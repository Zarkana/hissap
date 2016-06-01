namespace HISSAP1.Migrations
{
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
    }

    protected override void Seed(HISSAP1.Models.ApplicationDbContext context)
    {

      context.Organizations.AddOrUpdate(
          new Organization { ID = 0, Name = "Boys and Girls Club", Line1 = "345 Queen Street, Ste.900", Line2 = "", City = "Honolulu", State = "HI", Zip = "96813", Phone = "(808)-949-4203", Website = "http://www.bgch.com/", Email = "ronald@boysandgirlsclub.com" }
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
