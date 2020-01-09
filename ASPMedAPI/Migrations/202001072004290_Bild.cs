namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bild : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profils", "ProfileURL", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profils", "ProfileURL", c => c.String());
        }
    }
}
