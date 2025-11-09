using BetterGolfASP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetterGolfASP.Presentation.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingCartController(ShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }
        
        [HttpGet]
        public IActionResult GetCartHtml()
        {
            // Return the current cart items as a partial view
            return PartialView("_CartPartial", _shoppingCartService.GetItems());
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.TotalPrice = _shoppingCartService.CalculateTotalPrice();
            return View(_shoppingCartService.GetItems());
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity, int? variantId)
        {
            await _shoppingCartService.AddItemToCart(productId, quantity, variantId);
            return PartialView("_CartPartial", _shoppingCartService.GetItems());
        }

        [HttpPost]
        public IActionResult RemoveItem(int productId, int? variantId)
        {
            _shoppingCartService.RemoveItem(productId, variantId);
            return PartialView("_CartPartial", _shoppingCartService.GetItems());
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int? variantId, string action)
        {
            _shoppingCartService.UpdateQuantity(productId, variantId, action);
            return PartialView("_CartPartial", _shoppingCartService.GetItems());
        }

        [HttpGet]
        public IActionResult GetCartCount() => Json(_shoppingCartService.GetCartCount());
    }
}