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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
