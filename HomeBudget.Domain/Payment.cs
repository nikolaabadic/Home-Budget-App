using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeBudget.Domain
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public DateTime DateTime { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
        public int RecipientID { get; set; }
        public Account Recipient { get; set; }
        public string Purpose { get; set; }
        public int Model { get; set; }
        public string ReferenceNumber { get; set; }
        public double Amount { get; set; }
        public List<Belonging> Categories { get; set; }
    }
}
