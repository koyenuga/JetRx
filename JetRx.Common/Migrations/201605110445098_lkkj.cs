namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lkkj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerPrescription", "Customer_id", c => c.Int());
            AddColumn("dbo.CustomerPrescription", "Device_id", c => c.Int());
            AddColumn("dbo.CustomerPrescription", "Prescription_id", c => c.Int());
            CreateIndex("dbo.CustomerPrescription", "Customer_id");
            CreateIndex("dbo.CustomerPrescription", "Device_id");
            CreateIndex("dbo.CustomerPrescription", "Prescription_id");
            AddForeignKey("dbo.CustomerPrescription", "Customer_id", "dbo.Customer", "id");
            AddForeignKey("dbo.CustomerPrescription", "Device_id", "dbo.Device", "id");
            AddForeignKey("dbo.CustomerPrescription", "Prescription_id", "dbo.Prescription", "id");
            DropColumn("dbo.CustomerPrescription", "PrescriptionId");
            DropColumn("dbo.CustomerPrescription", "CustomerId");
            DropColumn("dbo.CustomerPrescription", "DeviceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerPrescription", "DeviceId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerPrescription", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerPrescription", "PrescriptionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CustomerPrescription", "Prescription_id", "dbo.Prescription");
            DropForeignKey("dbo.CustomerPrescription", "Device_id", "dbo.Device");
            DropForeignKey("dbo.CustomerPrescription", "Customer_id", "dbo.Customer");
            DropIndex("dbo.CustomerPrescription", new[] { "Prescription_id" });
            DropIndex("dbo.CustomerPrescription", new[] { "Device_id" });
            DropIndex("dbo.CustomerPrescription", new[] { "Customer_id" });
            DropColumn("dbo.CustomerPrescription", "Prescription_id");
            DropColumn("dbo.CustomerPrescription", "Device_id");
            DropColumn("dbo.CustomerPrescription", "Customer_id");
        }
    }
}
