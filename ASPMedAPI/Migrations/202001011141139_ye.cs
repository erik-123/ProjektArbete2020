namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ye : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profils",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Förnamn = c.String(),
                        Efternamn = c.String(),
                        Födelsedatum = c.DateTime(nullable: false),
                        ProfileURL = c.String(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profils");
        }
    }
}
