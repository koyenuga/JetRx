namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "created_at", c => c.DateTime());
            AddColumn("dbo.Customer", "updated_at", c => c.DateTime());
            AddColumn("dbo.identification", "created_at", c => c.DateTime());
            AddColumn("dbo.identification", "updated_at", c => c.DateTime());
            AddColumn("dbo.identification", "customer_id", c => c.Int());
            AddColumn("dbo.insurance", "created_at", c => c.DateTime());
            AddColumn("dbo.insurance", "updated_at", c => c.DateTime());
            AddColumn("dbo.insurance", "customer_id", c => c.Int());
            CreateIndex("dbo.identification", "customer_id");
            CreateIndex("dbo.insurance", "customer_id");
            AddForeignKey("dbo.identification", "customer_id", "dbo.Customer", "id");
            AddForeignKey("dbo.insurance", "customer_id", "dbo.Customer", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.insurance", "customer_id", "dbo.Customer");
            DropForeignKey("dbo.identification", "customer_id", "dbo.Customer");
            DropIndex("dbo.insurance", new[] { "customer_id" });
            DropIndex("dbo.identification", new[] { "customer_id" });
            DropColumn("dbo.insurance", "customer_id");
            DropColumn("dbo.insurance", "updated_at");
            DropColumn("dbo.insurance", "created_at");
            DropColumn("dbo.identification", "customer_id");
            DropColumn("dbo.identification", "updated_at");
            DropColumn("dbo.identification", "created_at");
            DropColumn("dbo.Customer", "updated_at");
            DropColumn("dbo.Customer", "created_at");
        }
    }
}
