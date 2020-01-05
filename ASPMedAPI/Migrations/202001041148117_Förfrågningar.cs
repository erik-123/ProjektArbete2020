namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Förfrågningar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VänFörfrågan",
                c => new
                    {
                        Vän_ID = c.Int(nullable: false, identity: true),
                        Person1 = c.String(),
                        Person2 = c.String(),
                    })
                .PrimaryKey(t => t.Vän_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VänFörfrågan");
        }
    }
}
