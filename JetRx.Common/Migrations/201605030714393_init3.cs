namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerInsurance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InsuranceId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        created_at = c.DateTime(),
                        updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.insurance",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        identification_type = c.String(),
                        image_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.insurance");
            DropTable("dbo.CustomerInsurance");
        }
    }
}
