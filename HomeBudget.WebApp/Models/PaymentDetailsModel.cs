using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.WebApp.Models
{
    public class PaymentDetailsModel
    {
        public int OwnerAccountID { get; set; }
        public Payment Payment { get; set; }
        public List<Category> Categories { get; set; }
        public int Num { get; set; }
    }
}
