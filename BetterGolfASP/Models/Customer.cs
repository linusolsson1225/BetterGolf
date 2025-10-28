using BetterGolfASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BetterGolfASP.Models
{
    public class Customer
    {
        public int CustomerId {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List <Order> Orders { get; set; } = new();

        protected Customer() { }

        private Customer(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Orders = new List<Order>();
        }
        public static Customer Create(string firstname, string lastname, string email)
        {
            if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("Field is required");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email address.", nameof(email));

            return new Customer(firstname, lastname, email);
        }
    }
    
}
