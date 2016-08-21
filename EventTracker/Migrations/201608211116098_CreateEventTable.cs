namespace EventTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEventTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(),
                        Category_Id = c.Byte(),
                        Host_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Host_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Host_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Host_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Events", new[] { "Host_Id" });
            DropIndex("dbo.Events", new[] { "Category_Id" });
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
        }
    }
}
