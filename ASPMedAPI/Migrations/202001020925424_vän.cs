namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vän : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vän",
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
            DropTable("dbo.Vän");
        }
    }
}
