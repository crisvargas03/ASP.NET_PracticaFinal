using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Core;


namespace Banking.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Transactions> transactions { get; set; }
        public DbSet<Account> accounts { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=tienda.db");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
