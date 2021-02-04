using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Domain
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string PINCode { get; set; }
    }
}
