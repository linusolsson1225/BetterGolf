using BetterGolfASP.DB;
using BetterGolfASP.Models.Products;
using BetterGolfASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterGolfASP.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductService _productService;
        public ProductsController(ILogger<ProductsController> logger, Context context, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Category(string type)
        {
            IEnumerable<GolfClub> clubs;
            switch (type.ToLower())
            {
                case "woods":

                    clubs = await _productService.GetAllByTypeAsync<WoodClub>();
                    break;

                case "putters":
                    clubs = await _productService.GetAllByTypeAsync<PutterClub>();
                    break;
                case "irons":
                    clubs = await _productService.GetAllByTypeAsync<IronClub>();
                    break;
                default:
                    clubs = await _productService.GetAllByTypeAsync<GolfClub>();
                    break;
            }
            ViewBag.Category = type;
            return View("CategoryView", clubs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var product = await _productService.GetDetailsAsync(Id);
            return View(product);
        }
    }
}
