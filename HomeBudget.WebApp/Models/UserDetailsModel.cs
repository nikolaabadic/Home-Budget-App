using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.WebApp.Models
{
    public class UserDetailsModel
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
