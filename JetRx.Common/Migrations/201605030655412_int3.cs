namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Device", "appdeviceKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Device", "appdeviceKey");
        }
    }
}
