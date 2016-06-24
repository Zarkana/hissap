namespace HISSAP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeoldwork : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractFiles", "ContractId", "dbo.Contracts");
            DropIndex("dbo.ContractFiles", new[] { "ContractId" });
            DropTable("dbo.ContractFiles");
            DropTable("dbo.Contracts");
        }
        
        public override void Down()
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
                        Description = c.String(),
                        ContractId = c.Int(nullable: false),
                        FileUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ContractFiles", "ContractId");
            AddForeignKey("dbo.ContractFiles", "ContractId", "dbo.Contracts", "Id", cascadeDelete: true);
        }
    }
}
