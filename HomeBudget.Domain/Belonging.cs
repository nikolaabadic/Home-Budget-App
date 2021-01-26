using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Domain
{
    public class Belonging
    {
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int PaymentID { get; set; }
        public int AccountID { get; set; }
        public int RecipientID { get; set; }
        public Payment Payment { get; set; }
    }
}
