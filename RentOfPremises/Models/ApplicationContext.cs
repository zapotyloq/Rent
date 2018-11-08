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
        public DbSet<Premise> Premises { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>().HasMany(p => p.Premises).WithOne(p => p.Building).HasForeignKey(p => p.BuildingNumber);
            modelBuilder.Entity<Premise>().HasMany(p => p.Rents).WithOne(p => p.Premise);
            modelBuilder.Entity<Organization>().HasMany(p => p.Rents).WithOne(p => p.Organization);
            modelBuilder.Entity<Rent>().HasMany(p => p.Invoices).WithOne(p => p.Rent);
            base.OnModelCreating(modelBuilder);
        }
    }
}
