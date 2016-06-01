namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uiui : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.order", "order_customer_id", "dbo.Customer");
            DropForeignKey("dbo.order", "order_prescription_id", "dbo.Prescription");
            DropIndex("dbo.order", new[] { "order_customer_id" });
            DropIndex("dbo.order", new[] { "order_prescription_id" });
            AddColumn("dbo.order", "customer_prescription_Id", c => c.Int());
            CreateIndex("dbo.order", "customer_prescription_Id");
            AddForeignKey("dbo.order", "customer_prescription_Id", "dbo.CustomerPrescription", "Id");
            DropColumn("dbo.order", "order_customer_id");
            DropColumn("dbo.order", "order_prescription_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.order", "order_prescription_id", c => c.Int());
            AddColumn("dbo.order", "order_customer_id", c => c.Int());
            DropForeignKey("dbo.order", "customer_prescription_Id", "dbo.CustomerPrescription");
            DropIndex("dbo.order", new[] { "customer_prescription_Id" });
            DropColumn("dbo.order", "customer_prescription_Id");
            CreateIndex("dbo.order", "order_prescription_id");
            CreateIndex("dbo.order", "order_customer_id");
            AddForeignKey("dbo.order", "order_prescription_id", "dbo.Prescription", "id");
            AddForeignKey("dbo.order", "order_customer_id", "dbo.Customer", "id");
        }
    }
}
