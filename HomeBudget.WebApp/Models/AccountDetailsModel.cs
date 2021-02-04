using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.WebApp.Models
{
    public class AccountDetailsModel
    {
        public int OwnerAccountID { get; set; }
        public Currency Currency { get; set; }
        public AccountType AccountType { get; set; }
        public string Number { get; set; }
        public double Amount { get; set; }       
        public List<Account> Accounts { get; set; }
        public List<CreditCard> CreditCards { get; set; }
        public List<Payment> Payments { get; set; }
        public string Username { get; set; }
    }
}
