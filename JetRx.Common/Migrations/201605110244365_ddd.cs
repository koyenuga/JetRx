namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.order", "Customer_id", c => c.Int());
            AddColumn("dbo.order", "Identification_id", c => c.Int());
            AddColumn("dbo.order", "Insurance_id", c => c.Int());
            CreateIndex("dbo.order", "Customer_id");
            CreateIndex("dbo.order", "Identification_id");
            CreateIndex("dbo.order", "Insurance_id");
            AddForeignKey("dbo.order", "Customer_id", "dbo.Customer", "id");
            AddForeignKey("dbo.order", "Identification_id", "dbo.identification", "id");
            AddForeignKey("dbo.order", "Insurance_id", "dbo.insurance", "id");
            DropColumn("dbo.order", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.order", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.order", "Insurance_id", "dbo.insurance");
            DropForeignKey("dbo.order", "Identification_id", "dbo.identification");
            DropForeignKey("dbo.order", "Customer_id", "dbo.Customer");
            DropIndex("dbo.order", new[] { "Insurance_id" });
            DropIndex("dbo.order", new[] { "Identification_id" });
            DropIndex("dbo.order", new[] { "Customer_id" });
            DropColumn("dbo.order", "Insurance_id");
            DropColumn("dbo.order", "Identification_id");
            DropColumn("dbo.order", "Customer_id");
        }
    }
}
