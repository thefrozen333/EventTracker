namespace EventTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id, Name) VALUES (1, 'Music Event')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (2, 'Sport Event')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (3, 'Party')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (4, 'Gathering')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Categories WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
