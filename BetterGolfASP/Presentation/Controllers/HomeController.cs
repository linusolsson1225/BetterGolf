using BetterGolfASP.Application.Services;
using BetterGolfASP.Infrastructure.DB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BetterGolfASP.Domain.Models;

namespace BetterGolfASP.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UoW _unitOfWork;
        private ProductService _productService;

        public HomeController(ILogger<HomeController> logger, Context context, ProductService productService)
        {
            _logger = logger;
            _unitOfWork = new UoW(context);
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var allClubs = await _productService.GetAllAsync();
            var featured = allClubs.Take(8);
            return View(featured);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
