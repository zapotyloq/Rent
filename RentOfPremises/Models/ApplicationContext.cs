using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RentOfPremises.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Premises> Premises { get; set; }
        public DbSet<RentOfPremises> RentOfPremises { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().HasMany(p => p.Premises).WithOne(p => p.Building).HasForeignKey(p => p.BuildingNumber);
            modelBuilder.Entity<Premises>().HasMany(p => p.RentOfPremises).WithOne(p => p.Premises);
            modelBuilder.Entity<Organization>().HasMany(p => p.RentOfPremises).WithOne(p => p.Organization);
            modelBuilder.Entity<RentOfPremises>().HasMany(p => p.Invoices).WithOne(p => p.RentOfPremises);
            base.OnModelCreating(modelBuilder);
        }
    }
}
