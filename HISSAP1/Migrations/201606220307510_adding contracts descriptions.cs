namespace HISSAP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingcontractsdescriptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractFiles", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractFiles", "Description");
        }
    }
}
