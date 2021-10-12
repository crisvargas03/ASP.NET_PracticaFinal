using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking.Core;
using Microsoft.AspNetCore.Identity;
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
        /*Esto son nuestro DBSET O NUESTRA TABLA UTILZANDO CODEFIRST CON NUESTRA CLASES*/
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<User> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*Aqui estoy especificando la tabla que quiero ignora de identity asi como tambien la columna de IdentityUser que no necesito*/
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityRole>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();
            modelBuilder.Entity<IdentityUser>()

                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.ConcurrencyStamp)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.LockoutEnd)
                .Ignore(c => c.AccessFailedCount)
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.Email)
                .Ignore(c => c.NormalizedEmail)
                .Ignore(c => c.PhoneNumber);


            //modelBuilder.Entity<IdentityUser>().ToTable("Users");//to change the name of table.

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source=netbacking.db");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
