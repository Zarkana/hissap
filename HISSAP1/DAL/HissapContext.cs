//using HISSAP1.Models;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration.Conventions;

//namespace HISSAP1.DAL
//{
//  public class HissapContext : DbContext
//  {

//    public HissapContext() : base("HissapContext")
//    {
//    }

//    public DbSet<Organization> Organization { get; set; }
//    public DbSet<Organization> Movie { get; set; }

//    protected override void OnModelCreating(DbModelBuilder modelBuilder)
//    {
//      //Removes the pluralization of table names
//      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
//    }
//  }
//}