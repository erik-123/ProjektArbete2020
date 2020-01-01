namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tss : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Profils");
            AlterColumn("dbo.Profils", "UserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Profils", "UserID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Profils");
            AlterColumn("dbo.Profils", "UserID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Profils", "UserID");
        }
    }
}
