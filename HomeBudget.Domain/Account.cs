using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Domain
{
    public enum Currency
    {
        RSD,EUR,USD,GBP
    }
    public enum AccountType
    {
        CURRENT,FOREIGN_EXCHANGE
    }
    public class Account
    {
        public int AccountID { get; set; }
        public Currency Currency { get; set; }
        public AccountType AccountType { get; set; }
        public string Number { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<CreditCard> CreditCards { get; set; }
        public List<Payment> PaymentsFrom { get; set; }
        public List<Payment> PaymentsTo { get; set; }
    }
}
