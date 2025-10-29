using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BetterGolfASP.Domain.Cart;

namespace BetterGolfASP.Presentation.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}