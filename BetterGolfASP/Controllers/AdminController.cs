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
        public async Task<IActionResult>UploadImage(int productId)
        {
            var product = await _adminService.GetProductAsync(productId);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> ManageImages(int productId)
        {
            var product = await _adminService.GetProductAsync(productId);

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            try
            {
                var imageUrl = await _adminService.UploadProductImageAsync(id, file);
                TempData["Success"] = "Image uploaded successfully!";
              
                return RedirectToAction("UploadImage", new { productId = id });
                
            }
            catch (ArgumentException ex)
            {
                TempData["Error"] = ex.Message;
                
                return RedirectToAction("UploadImage", new { productId = id });
            }
            catch (KeyNotFoundException)
            {
                
                return NotFound();
            }
            catch (Exception)
            {
                TempData["Error"] = "Something went wrong while uploading the image.";
                return RedirectToAction("UploadImage", new { productId = id });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(int id, string imageUrl)
        {
            try
            {
                await _adminService.RemoveImageAsync(id, imageUrl);

                return RedirectToAction("SelectProductForImage", new { productId = id });
            }
            catch (KeyNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("SelectProductForImage", new { productId = id });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Ett oväntat fel uppstod vid borttagning av bilden.";
                return RedirectToAction("SelectProductForImage", new { productId = id });
            }
        }
        [HttpGet]
        public async Task<IActionResult> SelectProductForImage()
        {
            var clubs = await _adminService.GetAllProductsAsync();
            return View(clubs);
        }
    }
}
