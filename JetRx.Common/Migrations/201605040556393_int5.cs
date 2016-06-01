namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.prescription", "prescription_type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.prescription", "prescription_type");
        }
    }
}
