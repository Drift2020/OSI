namespace OSI_Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Info_file", "status_Now", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Info_file", "status_Now");
        }
    }
}
