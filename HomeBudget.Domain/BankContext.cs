using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Domain
{
    public class BankContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Server = (localdb)\mssqllocaldb; Database = Banking; ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().OwnsMany(a => a.CreditCards);
            modelBuilder.Entity<Payment>(p =>
            {
                p.HasKey(p => new { p.PaymentID, p.AccountID, p.RecipientID });
                p.HasOne(p => p.Account).WithMany(a => a.PaymentsTo).OnDelete(DeleteBehavior.Restrict);
                p.HasOne(p => p.Recipient).WithMany(a => a.PaymentsFrom).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Belonging>(b =>
            {
                b.HasKey(b => new { b.CategoryID, b.PaymentID,b.AccountID,b.RecipientID });
                b.HasOne(b => b.Payment).WithMany(p => p.Categories).OnDelete(DeleteBehavior.Restrict);
            });
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Name = "Mark", Surname = "Ronson", Username = "markronson",PINCode= "1234" },
                new User { UserID = 2, Name = "John", Surname = "Parker", Username = "johnparker", PINCode = "1111" }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, Name = "Bills", Description = "Electricity, Water, Cabel,.." },
                new Category { CategoryID = 2, Name = "Food", Description="Grocery shopping, Restorants,..."}
            );
        }
    }
}
