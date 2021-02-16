using HomeBudget.Data.UnitOfWork;
using HomeBudget.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.ConsoleApp
{
    class Program
    {
        static BankContext context = new BankContext();
        static void Main(string[] args)
        {
            //addAccounts();
            //addPayments();
        }

        private static void addPayments()
        {
            Payment payment = new Payment
            {
                AccountID = 3,
                RecipientID = 1,
                Amount = 5000,
                DateTime = new DateTime(2021, 1,1),
                Model = 97,
                Purpose="Rent",
                ReferenceNumber="111",
                Categories = new List<Belonging>
                {
                    new Belonging{CategoryID=1,AccountID=3,RecipientID=1, OwnerID=1}
                }
            };
            context.Add(payment);
            context.SaveChanges();

            payment = new Payment
            {
                AccountID = 3,
                RecipientID = 1,
                Amount = 1000,
                DateTime = new DateTime(2021, 1, 1),
                Model = 97,
                Purpose = "Supermarket",
                ReferenceNumber = "01-2021",
                Categories = new List<Belonging>
                {
                    new Belonging{CategoryID=2,AccountID=3,RecipientID=1, OwnerID=1}
                }
            };
            context.Add(payment);
            context.SaveChanges();

            payment = new Payment
            {
                AccountID = 1,
                RecipientID = 3,
                Amount = 2400,
                DateTime = new DateTime(2021, 1, 3),
                Model = 97,
                Purpose = "Plane ticket",
                ReferenceNumber = "",
                Categories = new List<Belonging>
                {
                    new Belonging{CategoryID=5,AccountID=3,RecipientID=1, OwnerID=1}
                }
            };
            context.Add(payment);
            context.SaveChanges();
        }

        private static void addAccounts()
        {
            Account account = new Account
            {
                AccountType = AccountType.CURRENT,
                Currency = Currency.RSD,
                Amount = 1000,
                Number = "1000-2000-3000",
                UserID = 1,
                CreditCards = new List<CreditCard>
                {
                    new CreditCard{CreditCardNumber="1234-5678", PINCode="1234"},
                    new CreditCard{CreditCardNumber="1234-9999", PINCode="1923"},
                }
            };
            context.Add(account);
            context.SaveChanges();

            account = new Account
            {
                AccountType = AccountType.FOREIGN_EXCHANGE,
                Currency = Currency.EUR,
                Amount = 0,
                Number = "1000-2000-4545",
                UserID = 1
            };
            context.Add(account);
            context.SaveChanges();

            account = new Account
            {
                AccountType = AccountType.CURRENT,
                Currency = Currency.RSD,
                Amount = 0,
                Number = "1233-3131-6753",
                UserID = 2,
                CreditCards = new List<CreditCard>
                {
                    new CreditCard{CreditCardNumber="4230-2131", PINCode="1111"},
                }
            };
            context.Add(account);
            context.SaveChanges();
        }
    }
}
