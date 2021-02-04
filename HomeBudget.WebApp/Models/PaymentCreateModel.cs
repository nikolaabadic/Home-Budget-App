using HomeBudget.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.WebApp.Models
{
    public class PaymentCreateModel
    {
        public int AccountID { get; set; }
        public Payment Payment { get; set; }
        public string AccountNumber { get; set; }
        public string RecipientAccountNumber { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
