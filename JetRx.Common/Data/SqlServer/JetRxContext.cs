using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using JetRx.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace JetRx.Common.Data.SqlServer
{
    public class JetRxContext : DbContext
    {
        public JetRxContext():base("JetRxDbContext")
        {
           
        }

        public virtual DbSet<device> Devices { get; set;}
        public DbSet<customer> Customers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set;}
        public DbSet<CustomerDevice> CustomerDevices { get; set; }

        public DbSet<CustomerPrescription> CustomerPrescriptions { get; set; }
        public DbSet<prescription> Prescriptions { get; set; }
        public DbSet<image> Images { get; set; }
        public DbSet<identification> Identifications { get; set; }
        public DbSet<CustomerIdentification> CustomerIdentifications { get; set; }
        public DbSet<ApplicationConfig> ConfigSettings { get; set; }

        public DbSet<CustomerInsurance> CustomerInsuranceDetails { get; set; }

        public DbSet<ApplicationLog> Log { get; set; }
        public DbSet<insurance> InsuranceDetails{ get; set; }

        public DbSet<order> Orders { get; set;}

        public DbSet<orderstatus> OrderStatus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<device>().ToTable("Device");
            modelBuilder.Entity<customer>().ToTable("Customer");
            modelBuilder.Entity<prescription>().ToTable("Prescription");
            modelBuilder.Entity<ApplicationConfig>().ToTable("ApplicationConfig");
          




        }
    }
}
