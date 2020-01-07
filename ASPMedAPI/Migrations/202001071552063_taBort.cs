namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taBort : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Images");
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        post_id = c.Int(nullable: false, identity: true),
                        post_txt = c.String(),
                        post_date = c.DateTime(nullable: false),
                        post_like = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.post_id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Image_ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Image_ID);
            
        }
    }
}
