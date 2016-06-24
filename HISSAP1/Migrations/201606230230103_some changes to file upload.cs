namespace HISSAP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somechangestofileupload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractName = c.String(nullable: false, maxLength: 100),
                        OrganizationId = c.Int(nullable: false),
                        ContractNumber = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContractFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        ContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContractFiles", "ContractId", "dbo.Contracts");
            DropIndex("dbo.ContractFiles", new[] { "ContractId" });
            DropTable("dbo.ContractFiles");
            DropTable("dbo.Contracts");
        }
    }
}
