namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerDevice", "created_at", c => c.DateTime());
            AddColumn("dbo.CustomerDevice", "updated_at", c => c.DateTime());
            AlterColumn("dbo.AccessToken", "IssuedUtc", c => c.DateTime());
            AlterColumn("dbo.AccessToken", "ExpiresUtc", c => c.DateTime());
            AlterColumn("dbo.CustomerIdentification", "created_at", c => c.DateTime());
            AlterColumn("dbo.CustomerIdentification", "updated_at", c => c.DateTime());
            AlterColumn("dbo.CustomerPrescription", "created_at", c => c.DateTime());
            AlterColumn("dbo.CustomerPrescription", "updated_at", c => c.DateTime());
            AlterColumn("dbo.prescription", "prescribed_date", c => c.DateTime());
            AlterColumn("dbo.PrescriptionProduct", "created_at", c => c.DateTime());
            AlterColumn("dbo.PrescriptionProduct", "updated_at", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PrescriptionProduct", "updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PrescriptionProduct", "created_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.prescription", "prescribed_date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CustomerPrescription", "updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CustomerPrescription", "created_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CustomerIdentification", "updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CustomerIdentification", "created_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AccessToken", "ExpiresUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AccessToken", "IssuedUtc", c => c.DateTime(nullable: false));
            DropColumn("dbo.CustomerDevice", "updated_at");
            DropColumn("dbo.CustomerDevice", "created_at");
        }
    }
}
