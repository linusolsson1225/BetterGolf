using BetterGolfASP.Domain.Cart;

namespace BetterGolfASP.Presentation.ViewModels;

public class OrderConfirmationViewModel
{
    public string OrderNumber { get; set; }
    public List<CartItem> OrderItems { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}