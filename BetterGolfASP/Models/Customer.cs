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

        public string Country { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public List <Order> Orders { get; set; } = new();

        protected Customer() { }

        private Customer(string firstname, string lastname, string email, string country, string city,string zipCode, string phoneNumber)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Orders = new List<Order>();
            Country = country;
            City = city;
            ZipCode = zipCode;
            PhoneNumber = phoneNumber;
        }
        public static Customer Create(
            string firstname,
            string lastname,
            string email,
            string country,
            string city,
            string zipCode,
            string phoneNumber)
        {
            
            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentException("First name is required.", nameof(firstname));
            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("Last name is required.", nameof(lastname));
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required.", nameof(country));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required.", nameof(city));
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new ArgumentException("Zip code is required.", nameof(zipCode));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                    throw new ArgumentException("Invalid email address.", nameof(email));
            }
            catch
            {
                throw new ArgumentException("Invalid email address.", nameof(email));
            }

            
            

            return new Customer(firstname,lastname, email, country, city, zipCode, phoneNumber);
        }
    }
    
}
