namespace MotoGear.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.CartItems", new[] { "ShoppingCart_Id" });
            CreateTable(
                "dbo.ShoppingCartItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ShoppingCartId = c.String(maxLength: 128),
                        ProductId = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCartId)
                .Index(t => t.ShoppingCartId);
            
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CartId = c.String(),
                        ProductId = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                        ShoppingCart_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ShoppingCartItems", "ShoppingCartId", "dbo.ShoppingCarts");
            DropIndex("dbo.ShoppingCartItems", new[] { "ShoppingCartId" });
            DropTable("dbo.ShoppingCartItems");
            CreateIndex("dbo.CartItems", "ShoppingCart_Id");
            AddForeignKey("dbo.CartItems", "ShoppingCart_Id", "dbo.ShoppingCarts", "Id");
        }
    }
}
