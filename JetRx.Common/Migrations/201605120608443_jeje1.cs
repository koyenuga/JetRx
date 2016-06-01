namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jeje1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Device", "phonenumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Device", "phonenumber", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
