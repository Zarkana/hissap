namespace HISSAP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Zip = c.String(nullable: false),
                        Provider_Id = c.Int(),
                        Site_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Providers", t => t.Provider_Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Provider_Id)
                .Index(t => t.Site_Id);
            
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
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractName = c.String(nullable: false, maxLength: 100),
                        ContractsProviderId = c.Int(nullable: false),
                        ContractNumber = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Providers", t => t.ContractsProviderId, cascadeDelete: true)
                .Index(t => t.ContractsProviderId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        ContactPerson = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Website = c.String(),
                        Address_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiteName = c.String(nullable: false, maxLength: 100),
                        SitesContractId = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                        Address_AddressId = c.Int(),
                        SiteContact_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_AddressId)
                .ForeignKey("dbo.SiteContacts", t => t.SiteContact_Id)
                .ForeignKey("dbo.Contracts", t => t.SitesContractId, cascadeDelete: true)
                .Index(t => t.SitesContractId)
                .Index(t => t.Address_AddressId)
                .Index(t => t.SiteContact_Id);
            
            CreateTable(
                "dbo.SiteContacts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WorkPhone = c.String(),
                        Email = c.String(),
                        Site_Id = c.Int(),
                        Site_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id1)
                .Index(t => t.Site_Id)
                .Index(t => t.Site_Id1);
            
            CreateTable(
                "dbo.CurrentSites",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SelectedSite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Sites", t => t.SelectedSite, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.SelectedSite);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ProviderId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CurrentSites", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CurrentSites", "SelectedSite", "dbo.Sites");
            DropForeignKey("dbo.Sites", "SitesContractId", "dbo.Contracts");
            DropForeignKey("dbo.SiteContacts", "Site_Id1", "dbo.Sites");
            DropForeignKey("dbo.Sites", "SiteContact_Id", "dbo.SiteContacts");
            DropForeignKey("dbo.SiteContacts", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Addresses", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Sites", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Contracts", "ContractsProviderId", "dbo.Providers");
            DropForeignKey("dbo.Addresses", "Provider_Id", "dbo.Providers");
            DropForeignKey("dbo.Providers", "Address_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ContractFiles", "ContractId", "dbo.Contracts");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CurrentSites", new[] { "SelectedSite" });
            DropIndex("dbo.CurrentSites", new[] { "UserId" });
            DropIndex("dbo.SiteContacts", new[] { "Site_Id1" });
            DropIndex("dbo.SiteContacts", new[] { "Site_Id" });
            DropIndex("dbo.Sites", new[] { "SiteContact_Id" });
            DropIndex("dbo.Sites", new[] { "Address_AddressId" });
            DropIndex("dbo.Sites", new[] { "SitesContractId" });
            DropIndex("dbo.Providers", new[] { "Address_AddressId" });
            DropIndex("dbo.Contracts", new[] { "ContractsProviderId" });
            DropIndex("dbo.ContractFiles", new[] { "ContractId" });
            DropIndex("dbo.Addresses", new[] { "Site_Id" });
            DropIndex("dbo.Addresses", new[] { "Provider_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CurrentSites");
            DropTable("dbo.SiteContacts");
            DropTable("dbo.Sites");
            DropTable("dbo.Providers");
            DropTable("dbo.Contracts");
            DropTable("dbo.ContractFiles");
            DropTable("dbo.Addresses");
        }
    }
}
