namespace HISSAP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcontracts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractName = c.String(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        ContractNumber = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ContractId = c.Int(nullable: false),
                        FileUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
            DropTable("dbo.Movies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Genre = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.ContractFiles", "ContractId", "dbo.Contracts");
            DropIndex("dbo.ContractFiles", new[] { "ContractId" });
            DropTable("dbo.ContractFiles");
            DropTable("dbo.Contracts");
        }
    }
}
