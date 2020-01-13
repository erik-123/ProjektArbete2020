namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aktiv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profils", "aktiv", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profils", "aktiv");
        }
    }
}
