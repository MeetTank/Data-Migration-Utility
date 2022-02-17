using Data.Domain;
using DataMigration.Domain;
using DataMigration.DomainClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMigration.DataClass
{
    public class DataMigrationDbContext : DbContext
    {
      
        
        public DbSet<SourceTable> SourceTable { get; set; }
        public DbSet<DestinationTable> DestinationTable { get; set; }

        public DbSet<DataMigrationTable> DataMigrationTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=DataMigrationApp;Trusted_Connection=True;");

            }
        
    }
}
