namespace OSI_Net.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Info_file",
                c => new
                    {
                        Info_fileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Path_PC = c.String(),
                        Path_Net = c.String(),
                    })
                .PrimaryKey(t => t.Info_fileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Info_file");
        }
    }
}
