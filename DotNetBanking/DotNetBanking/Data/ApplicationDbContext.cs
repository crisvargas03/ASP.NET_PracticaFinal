using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
     

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Account> accounts { get; set; }

        public DbSet<User> users { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityUser>()
        //        .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=netbacking.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
