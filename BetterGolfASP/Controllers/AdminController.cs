using BetterGolfASP.DB;
using BetterGolfASP.Services;
using Microsoft.AspNetCore.Mvc;

namespace BetterGolfASP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly AdminService _adminService;
        public AdminController(ILogger<AdminController> logger, Context context, AdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }


        [HttpGet]
        public async Task<IActionResult> UploadImage(int productId)
        {
            var club = await _adminService.GetProductAsync(productId);
            return View(club);
        }

        [HttpPost]
        //FIXA NÄR BILDERNA ÄR FIXADE

        //public async Task<IActionResult> UploadImage(int id, IFormFile file)
        //{
        //    try
        //    {
        //        var imageUrl = await _adminService.UploadProductImageAsync(id, file);
        //        TempData["Success"] = "Image uploaded successfully!";
        //        return RedirectToAction("Details", "Products", new { id }); ;
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        TempData["Error"] = ex.Message;
        //        return RedirectToAction("UploadImage", new { id });
        //    }
        //    catch (KeyNotFoundException)
        //    {
        //        return NotFound();
        //    }
        //    catch (Exception)
        //    {
        //        TempData["Error"] = "Something went wrong while uploading the image.";
        //        return RedirectToAction("UploadImage", new { id });
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> SelectProductForImage()
        {
            var clubs = await _adminService.GetAllProductsAsync();
            return View(clubs);
        }
    }
}
