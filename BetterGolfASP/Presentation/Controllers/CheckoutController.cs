using BetterGolfASP.Application.Services;
using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Infrastructure.DB;
using BetterGolfASP.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BetterGolfASP.Presentation.Controllers;

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
        var cartItems = GetCartFromSession();
        var model = new CheckoutViewModel
        {
            CartItems = cartItems
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var confirmationVm = await _checkoutService.ProcessOrderAsync(model);
        _shoppingCartService.Clear();

        return View("OrderConfirmation", confirmationVm);
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