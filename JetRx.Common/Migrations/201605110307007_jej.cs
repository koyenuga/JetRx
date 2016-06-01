namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jej : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.identification", "customer_id", "dbo.Customer");
            DropForeignKey("dbo.insurance", "customer_id", "dbo.Customer");
            DropForeignKey("dbo.order", "Identification_id", "dbo.identification");
            DropForeignKey("dbo.order", "Insurance_id", "dbo.insurance");
            DropForeignKey("dbo.Prescription", "order_id", "dbo.order");
            DropIndex("dbo.identification", new[] { "customer_id" });
            DropIndex("dbo.insurance", new[] { "customer_id" });
            DropIndex("dbo.order", new[] { "Identification_id" });
            DropIndex("dbo.order", new[] { "Insurance_id" });
            DropIndex("dbo.Prescription", new[] { "order_id" });
            RenameColumn(table: "dbo.order", name: "Customer_id", newName: "order_customer_id");
            RenameIndex(table: "dbo.order", name: "IX_Customer_id", newName: "IX_order_customer_id");
            AddColumn("dbo.Customer", "Identification_id", c => c.Int());
            AddColumn("dbo.Customer", "Insurance_id", c => c.Int());
            AddColumn("dbo.order", "created_at", c => c.DateTime());
            AddColumn("dbo.order", "updated_at", c => c.DateTime());
            AddColumn("dbo.order", "order_prescription_id", c => c.Int());
            CreateIndex("dbo.Customer", "Identification_id");
            CreateIndex("dbo.Customer", "Insurance_id");
            CreateIndex("dbo.order", "order_prescription_id");
            AddForeignKey("dbo.Customer", "Identification_id", "dbo.identification", "id");
            AddForeignKey("dbo.Customer", "Insurance_id", "dbo.insurance", "id");
            AddForeignKey("dbo.order", "order_prescription_id", "dbo.Prescription", "id");
            DropColumn("dbo.identification", "customer_id");
            DropColumn("dbo.insurance", "customer_id");
            DropColumn("dbo.order", "Identification_id");
            DropColumn("dbo.order", "Insurance_id");
            DropColumn("dbo.Prescription", "order_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prescription", "order_id", c => c.Int());
            AddColumn("dbo.order", "Insurance_id", c => c.Int());
            AddColumn("dbo.order", "Identification_id", c => c.Int());
            AddColumn("dbo.insurance", "customer_id", c => c.Int());
            AddColumn("dbo.identification", "customer_id", c => c.Int());
            DropForeignKey("dbo.order", "order_prescription_id", "dbo.Prescription");
            DropForeignKey("dbo.Customer", "Insurance_id", "dbo.insurance");
            DropForeignKey("dbo.Customer", "Identification_id", "dbo.identification");
            DropIndex("dbo.order", new[] { "order_prescription_id" });
            DropIndex("dbo.Customer", new[] { "Insurance_id" });
            DropIndex("dbo.Customer", new[] { "Identification_id" });
            DropColumn("dbo.order", "order_prescription_id");
            DropColumn("dbo.order", "updated_at");
            DropColumn("dbo.order", "created_at");
            DropColumn("dbo.Customer", "Insurance_id");
            DropColumn("dbo.Customer", "Identification_id");
            RenameIndex(table: "dbo.order", name: "IX_order_customer_id", newName: "IX_Customer_id");
            RenameColumn(table: "dbo.order", name: "order_customer_id", newName: "Customer_id");
            CreateIndex("dbo.Prescription", "order_id");
            CreateIndex("dbo.order", "Insurance_id");
            CreateIndex("dbo.order", "Identification_id");
            CreateIndex("dbo.insurance", "customer_id");
            CreateIndex("dbo.identification", "customer_id");
            AddForeignKey("dbo.Prescription", "order_id", "dbo.order", "id");
            AddForeignKey("dbo.order", "Insurance_id", "dbo.insurance", "id");
            AddForeignKey("dbo.order", "Identification_id", "dbo.identification", "id");
            AddForeignKey("dbo.insurance", "customer_id", "dbo.Customer", "id");
            AddForeignKey("dbo.identification", "customer_id", "dbo.Customer", "id");
        }
    }
}
