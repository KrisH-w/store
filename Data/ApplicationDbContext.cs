using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using store.Models;
namespace store.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> IvoiceItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>()
                .ToTable("AspNetUsers")
                .HasDiscriminator<string>("UserType")
                .HasValue<Employee>("Employee")
                .HasValue<Customer>("Customer");
        }
        public DbSet<store.Models.Customer> Customer { get; set; }
    }
}
