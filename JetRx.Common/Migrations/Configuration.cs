namespace JetRx.Common.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JetRx.Common.Data.SqlServer.JetRxContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JetRx.Common.Data.SqlServer.JetRxContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Clients.AddOrUpdate(new Entities.Client
            {
                Active = true,
                Secret = "F5433BBF-208A-415D-813E-82514AE43FA6",
                Name = "JetRx Prescription Client App",
                RefreshTokenLifeTime = 1440

            });

            context.OrderStatus.AddOrUpdate(new Entities.orderstatus
            {
                status = "New",
                stepOrder = 0,
                description = "New Order just recieved"

            },
            new Entities.orderstatus
            {
                status = "In Process",
                stepOrder = 1,
                description = "Phamacist is processing the order"

            },
            new Entities.orderstatus
            {
                status = "In Review",
                stepOrder = 3,
                description = "Order is being reviewed by phamacist"

            },
            new Entities.orderstatus
            {
                status = "On Hold",
                stepOrder = 4,
                description = "Order is being reviewed by phamacist"

            },
            new Entities.orderstatus
            {
                status = "Fulfilled",
                stepOrder = 90,
                description = "Order is fulfilled by phamacist"

            },
            new Entities.orderstatus
            {
                status = "Delivered",
                stepOrder = 100,
                description = "Order is fulfilled by phamacist"

            }
            );

        }
    }
}
