namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.prescription", "doctor_phonenumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.prescription", "doctor_phonenumber");
        }
    }
}
