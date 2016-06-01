using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using JetRx.Enitities;
namespace JetRx.DataSource.SqlServer
{
    public class JetRxContext : DbContext
    {
        public JetRxContext():base("JetRxDbContext")
        {

        }

        public DbSet<Device> Devices { get; set;}
        public DbSet<Customer> Customers { get; set; }
    }
}
