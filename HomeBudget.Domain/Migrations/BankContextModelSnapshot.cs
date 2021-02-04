﻿// <auto-generated />
using System;
using HomeBudget.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HomeBudget.Domain.Migrations
{
    [DbContext(typeof(BankContext))]
    partial class BankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("HomeBudget.Domain.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalExpense")
                        .HasColumnType("float");

                    b.Property<double>("TotalIncome")
                        .HasColumnType("float");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AccountID");

                    b.HasIndex("UserID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("HomeBudget.Domain.Belonging", b =>
                {
                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("RecipientID")
                        .HasColumnType("int");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.HasKey("CategoryID", "PaymentID", "AccountID", "RecipientID", "OwnerID");

                    b.HasIndex("PaymentID", "AccountID", "RecipientID");

                    b.ToTable("Belongings");
                });

            modelBuilder.Entity("HomeBudget.Domain.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            Description = "Electricity, Water, Cabel,..",
                            Name = "Bills"
                        },
                        new
                        {
                            CategoryID = 2,
                            Description = "Grocery shopping, Restorants,...",
                            Name = "Food"
                        });
                });

            modelBuilder.Entity("HomeBudget.Domain.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("RecipientID")
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Model")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID", "AccountID", "RecipientID");

                    b.HasIndex("AccountID");

                    b.HasIndex("RecipientID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("HomeBudget.Domain.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PINCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Name = "Mark",
                            PINCode = "1234",
                            Surname = "Ronson",
                            Username = "markronson"
                        },
                        new
                        {
                            UserID = 2,
                            Name = "John",
                            PINCode = "1111",
                            Surname = "Parker",
                            Username = "johnparker"
                        });
                });

            modelBuilder.Entity("HomeBudget.Domain.Account", b =>
                {
                    b.HasOne("HomeBudget.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("HomeBudget.Domain.CreditCard", "CreditCards", b1 =>
                        {
                            b1.Property<int>("AccountID")
                                .HasColumnType("int");

                            b1.Property<int>("CreditCardID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .UseIdentityColumn();

                            b1.Property<string>("CreditCardNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PINCode")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AccountID", "CreditCardID");

                            b1.ToTable("CreditCard");

                            b1.WithOwner()
                                .HasForeignKey("AccountID");
                        });

                    b.Navigation("CreditCards");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HomeBudget.Domain.Belonging", b =>
                {
                    b.HasOne("HomeBudget.Domain.Category", "Category")
                        .WithMany("Payments")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeBudget.Domain.Payment", "Payment")
                        .WithMany("Categories")
                        .HasForeignKey("PaymentID", "AccountID", "RecipientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("HomeBudget.Domain.Payment", b =>
                {
                    b.HasOne("HomeBudget.Domain.Account", "Account")
                        .WithMany("PaymentsTo")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HomeBudget.Domain.Account", "Recipient")
                        .WithMany("PaymentsFrom")
                        .HasForeignKey("RecipientID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("HomeBudget.Domain.Account", b =>
                {
                    b.Navigation("PaymentsFrom");

                    b.Navigation("PaymentsTo");
                });

            modelBuilder.Entity("HomeBudget.Domain.Category", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("HomeBudget.Domain.Payment", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
