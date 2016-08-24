namespace EventTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiestoEvent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Category_Id", newName: "CategoryId");
            RenameColumn(table: "dbo.Events", name: "Host_Id", newName: "HostId");
            RenameIndex(table: "dbo.Events", name: "IX_Host_Id", newName: "IX_HostId");
            RenameIndex(table: "dbo.Events", name: "IX_Category_Id", newName: "IX_CategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_CategoryId", newName: "IX_Category_Id");
            RenameIndex(table: "dbo.Events", name: "IX_HostId", newName: "IX_Host_Id");
            RenameColumn(table: "dbo.Events", name: "HostId", newName: "Host_Id");
            RenameColumn(table: "dbo.Events", name: "CategoryId", newName: "Category_Id");
        }
    }
}
