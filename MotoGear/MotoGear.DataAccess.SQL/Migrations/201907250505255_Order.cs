namespace MotoGear.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "LastName", c => c.String());
            DropColumn("dbo.Orders", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Surname", c => c.String());
            DropColumn("dbo.Orders", "LastName");
        }
    }
}
