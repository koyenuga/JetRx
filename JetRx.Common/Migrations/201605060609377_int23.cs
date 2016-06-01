namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.customer", "phone_number", c => c.String());
            AddColumn("dbo.prescription", "transfer_pharmacy_name", c => c.String());
            AddColumn("dbo.prescription", "transfer_pharmacy_phone_number", c => c.String());
            AddColumn("dbo.prescription", "transfer_prescription_number", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.prescription", "transfer_prescription_number");
            DropColumn("dbo.prescription", "transfer_pharmacy_phone_number");
            DropColumn("dbo.prescription", "transfer_pharmacy_name");
            DropColumn("dbo.customer", "phone_number");
        }
    }
}
