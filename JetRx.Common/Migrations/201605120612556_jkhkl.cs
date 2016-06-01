namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jkhkl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "email", c => c.String());
            AlterColumn("dbo.Customer", "phone_number", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customer", "phone_number", c => c.String(nullable: false));
            AlterColumn("dbo.Customer", "email", c => c.String(nullable: false));
        }
    }
}
