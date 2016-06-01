namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jeje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.insurance", "provider_name", c => c.String());
            AddColumn("dbo.insurance", "provider_phone", c => c.String());
            AlterColumn("dbo.AccessToken", "Secret", c => c.String(maxLength: 100));
            AlterColumn("dbo.ApplicationConfig", "Key", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customer", "firstname", c => c.String(maxLength: 75));
            AlterColumn("dbo.Customer", "lastname", c => c.String(maxLength: 75));
            AlterColumn("dbo.Customer", "email", c => c.String(nullable: true));
            AlterColumn("dbo.Customer", "password", c => c.String(nullable: true));
            AlterColumn("dbo.Customer", "phone_number", c => c.String(nullable: true));
            AlterColumn("dbo.Device", "deviceidentifier", c => c.String(maxLength: 75));
            AlterColumn("dbo.Device", "devicename", c => c.String(maxLength: 75));
            AlterColumn("dbo.Device", "devicetype", c => c.String(maxLength: 20));
            AlterColumn("dbo.Device", "phonenumber", c => c.String(nullable: true, maxLength: 20));
            DropColumn("dbo.insurance", "identification_type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.insurance", "identification_type", c => c.String());
            AlterColumn("dbo.Device", "phonenumber", c => c.String());
            AlterColumn("dbo.Device", "devicetype", c => c.String());
            AlterColumn("dbo.Device", "devicename", c => c.String());
            AlterColumn("dbo.Device", "deviceidentifier", c => c.String());
            AlterColumn("dbo.Customer", "phone_number", c => c.String());
            AlterColumn("dbo.Customer", "password", c => c.String());
            AlterColumn("dbo.Customer", "email", c => c.String());
            AlterColumn("dbo.Customer", "lastname", c => c.String());
            AlterColumn("dbo.Customer", "firstname", c => c.String());
            AlterColumn("dbo.ApplicationConfig", "Key", c => c.String());
            AlterColumn("dbo.AccessToken", "Secret", c => c.String());
            DropColumn("dbo.insurance", "provider_phone");
            DropColumn("dbo.insurance", "provider_name");
        }
    }
}
