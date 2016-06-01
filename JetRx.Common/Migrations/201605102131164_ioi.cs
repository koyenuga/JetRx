namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ioi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.order",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        status_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.orderstatus", t => t.status_id)
                .Index(t => t.status_id);
            
            CreateTable(
                "dbo.orderstatus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        status = c.String(),
                        description = c.String(),
                        stepOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Prescription", "order_id", c => c.Int());
            CreateIndex("dbo.Prescription", "order_id");
            AddForeignKey("dbo.Prescription", "order_id", "dbo.order", "id");
            DropColumn("dbo.Customer", "phonenumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "phonenumber", c => c.String());
            DropForeignKey("dbo.order", "status_id", "dbo.orderstatus");
            DropForeignKey("dbo.Prescription", "order_id", "dbo.order");
            DropIndex("dbo.Prescription", new[] { "order_id" });
            DropIndex("dbo.order", new[] { "status_id" });
            DropColumn("dbo.Prescription", "order_id");
            DropTable("dbo.orderstatus");
            DropTable("dbo.order");
        }
    }
}
