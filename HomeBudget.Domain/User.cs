using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Domain
{
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string PINCode { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
