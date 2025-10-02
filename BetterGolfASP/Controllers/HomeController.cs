using BetterGolfASP.DB;
using BetterGolfASP.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;

namespace BetterGolfASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UoW _unitOfWork;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _unitOfWork = new UoW(context);
        }

        public async Task<IActionResult> Index()
        {
            var allClubs = await _unitOfWork.GolfClubRepository.GetAllAsync();
            var featured = allClubs.Take(8);
            return View(featured);
        }

        //private async Task<IEnumerable<GolfClub>> GetFeaturedProductsAsync()
        //{
        //    // Här kan du filtrera, t.ex. top 8 produkter eller produkter med IsFeatured=true
        //    var allProducts = await _unitOfWork.GolfClubRepository.GetAllAsync();
        //    var featured = allProducts.Take(8); // exempel: ta 8 produkter
        //    return featured;
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
