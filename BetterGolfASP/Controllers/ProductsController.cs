using BetterGolfASP.DB;
using BetterGolfASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BetterGolfASP.Controllers
{
    public class ProductsController : Controller
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

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _unitOfWork.GolfClubRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> UploadImage(int id)
        {
            var club = await _unitOfWork.GolfClubRepository.GetByIdAsync(id);
            if (club == null) return NotFound();

            return View(club);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Please select an image.";
                return RedirectToAction("UploadImage", new { id });
            }

            var club = await _unitOfWork.GolfClubRepository.GetByIdAsync(id);
            if (club == null) return NotFound();
            
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            club.ImageUrl = $"/images/products/{fileName}";
            _unitOfWork.Update(club);
            await _unitOfWork.SaveChangesAsync();

            TempData["Success"] = "Image uploaded successfully!";
            return RedirectToAction("Details", new { id = club.GolfClubID });
        }

        [HttpGet]
        public async Task<IActionResult> SelectProductForImage()
        {
            var clubs = await _unitOfWork.GolfClubRepository.GetAllAsync();
            return View(clubs);
        }


    }
}
