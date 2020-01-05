namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BloggPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BloggPosts",
                c => new
                    {
                        Post_Id = c.Int(nullable: false, identity: true),
                        Avsändare = c.String(),
                        Mottagare = c.String(),
                        SkickatDatum = c.DateTimeOffset(nullable: false, precision: 7),
                        MeddelandeText = c.String(),
                    })
                .PrimaryKey(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BloggPosts");
        }
    }
}
