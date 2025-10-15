using BetterGolfASP.DB;
using BetterGolfASP.Models;
using BetterGolfASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetterGolfASP.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(ShoppingCartService shoppingCartService,ILogger<ShoppingCartController> logger,Context context)
        {
                _shoppingCartService = shoppingCartService;
            _logger = logger;
                
        }
        
        public IActionResult Index()
        {
            var items = _shoppingCartService.GetItems();
            var total = _shoppingCartService.CalculateTotalPrice();
            ViewBag.TotalPrice = total;
            return View(items);
        }

        [HttpPost]
        public async Task <IActionResult> AddToCart(int productId, int quantity)
        {

            await _shoppingCartService.AddItemToCart(productId, quantity);
            var cartCount = _shoppingCartService.GetItems().Sum(x => x.Quantity);
            return Json(new { count = cartCount });
        }
        
        [HttpPost]
        public IActionResult RemoveItem(int productId)
        {
            _shoppingCartService.RemoveItem(productId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId,string action)
        {
            var items = _shoppingCartService.GetItems();
            var item = items.FirstOrDefault(i => i.ProductID == productId);
            if (item != null)
            {
                if (action =="increase")
                {
                    item.Quantity++;
                }
                else if (action == "decrease")
                {
                    item.Quantity--;
                    if (item.Quantity == 0)
                    {
                        items.Remove(item);
                    }
                }
                    
            }
            _shoppingCartService.SaveItems(items);
            return PartialView("_CartPartial", items);
        }
        [HttpGet]
        public IActionResult GetCartCount()
        {
            var count = _shoppingCartService.GetItems().Count();
            return Json(count);
        }
        [HttpGet]
        public IActionResult GetCartHtml()
        {
            var items = _shoppingCartService.GetItems();
            return PartialView("_CartPartial",items);
        }
    }
}
