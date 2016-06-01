namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class int8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RawRequest = c.String(),
                        RecievedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationLog");
        }
    }
}
