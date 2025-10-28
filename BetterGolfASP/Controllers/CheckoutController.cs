using BetterGolfASP.DB;
using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Models;
using BetterGolfASP.Services;
using Microsoft.AspNetCore.Mvc;


namespace BetterGolfASP.Controllers;

public class CheckoutController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly CheckOutService _checkoutService;
    private readonly ShoppingCartService _shoppingCartService;

    public CheckoutController(ILogger<ProductsController> logger, Context context, CheckOutService checkoutService, ShoppingCartService shoppingCartService )
    {
        _logger = logger;
        _checkoutService = checkoutService;
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var cartItems = GetCartFromSession(); // Skapa en metod som hämtar kundvagn
        return View(cartItems);
    }

    private List<CartItem> GetCartFromSession()
    {
        return _shoppingCartService.GetItems();
    }

    [HttpGet]
    public async Task<JsonResult> CheckEmailExists(string email)
    {
        var customer = await _checkoutService.FindByEmailAsync(email);
        return Json(customer != null);
    }
}