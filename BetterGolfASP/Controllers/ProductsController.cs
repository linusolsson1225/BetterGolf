using BetterGolfASP.DB;
using BetterGolfASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterGolfASP.Controllers
{
    public class ProductsController: Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly UoW _unitOfWork;

        public ProductsController(ILogger<ProductsController> logger, Context context)
        {
            _logger = logger;
            _unitOfWork = new UoW(context);
        }
        public async Task<IEnumerable<T>> GetAllByTypeAsync<T>() where T : GolfClub
        {
            return await _unitOfWork.GolfClubRepository.GetAllByTypeAsync<T>();
        }
        public async Task<IActionResult> Category(string type)
        {
            IEnumerable<GolfClub> clubs;
            switch (type.ToLower())
            {
                case "woods":

                    clubs = await _unitOfWork.GolfClubRepository.GetAllByTypeAsync<WoodClub>();
                    break;

                case "putters":
                    clubs = await _unitOfWork.GolfClubRepository.GetAllByTypeAsync<PutterClub>();
                    break;
                case "irons":
                    clubs = await _unitOfWork.GolfClubRepository.GetAllByTypeAsync<IronClub>();
                    break;
                default:
                    clubs = await _unitOfWork.GolfClubRepository.GetAllByTypeAsync<GolfClub>();
                    break;
            }
            ViewBag.Category = type;
            return View("CategoryView", clubs);




        }
    }
}
