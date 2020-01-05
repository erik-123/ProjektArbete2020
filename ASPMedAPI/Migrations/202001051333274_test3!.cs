namespace ASPMedAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Date = c.DateTime(nullable: false),
                        Receiver_Id = c.String(maxLength: 128),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Receiver_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPosts", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserPosts", "Receiver_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserPosts", new[] { "Sender_Id" });
            DropIndex("dbo.UserPosts", new[] { "Receiver_Id" });
            DropTable("dbo.UserPosts");
        }
    }
}
