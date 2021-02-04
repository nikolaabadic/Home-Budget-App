using HomeBudget.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.WebApp.Models
{
    public class BelongingModel
    {
        public int Num { get; set; }
        public int CategoryID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }
        //public List<Belonging> List { get; set; }
    }
}
