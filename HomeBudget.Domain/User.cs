using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomeBudget.Domain
{
    public class User
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "You must enter all fields!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must enter all fields!")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "You must enter all fields!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "You must enter all fields!")]
        public string PINCode { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}
