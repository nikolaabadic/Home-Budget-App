using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBudget.Domain
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Belonging> Payments { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
