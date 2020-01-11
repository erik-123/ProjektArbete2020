namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FörsökerFixAprofil : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profils", "ProfileURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profils", "ProfileURL", c => c.String(nullable: false));
        }
    }
}
