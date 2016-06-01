namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessToken",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        Secret = c.String(),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationConfig",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerDevice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerIdentification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentificationId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerPrescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrescriptionId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastname = c.String(),
                        phonenumber = c.String(),
                        accesstoken = c.String(),
                        email = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        deviceidentifier = c.String(),
                        devicename = c.String(),
                        devicetype = c.String(),
                        phonenumber = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Identification",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        identification_type = c.String(),
                        image_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        image_type = c.Int(nullable: false),
                        owner_id = c.Int(nullable: false),
                        url = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Prescription",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        doctor_name = c.String(),
                        doctor_address = c.String(),
                        image_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        prescribed_date = c.DateTime(nullable: false),
                        duration = c.Int(nullable: false),
                        refill = c.Boolean(nullable: false),
                        barcode = c.String(),
                        prescription_product_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PrescriptionProduct", t => t.prescription_product_id)
                .Index(t => t.prescription_product_id);
            
            CreateTable(
                "dbo.PrescriptionProduct",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_details = c.String(),
                        image_url = c.String(),
                        unit = c.String(),
                        cost_per_unit = c.String(),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.prescription", "prescription_product_id", "dbo.PrescriptionProduct");
            DropIndex("dbo.prescription", new[] { "prescription_product_id" });
            DropTable("dbo.PrescriptionProduct");
            DropTable("dbo.prescription");
            DropTable("dbo.image");
            DropTable("dbo.identification");
            DropTable("dbo.device");
            DropTable("dbo.customer");
            DropTable("dbo.CustomerPrescription");
            DropTable("dbo.CustomerIdentification");
            DropTable("dbo.CustomerDevice");
            DropTable("dbo.ApplicationConfig");
            DropTable("dbo.Client");
            DropTable("dbo.AccessToken");
        }
    }
}
