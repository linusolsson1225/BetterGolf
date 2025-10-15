using BetterGolfASP.DB;
using Microsoft.AspNetCore.Mvc;
using BetterGolfASP.Models;

namespace BetterGolfASP.Services
{
    public class AdminService
    {

        private readonly ILogger<AdminService> _logger;
        private readonly UoW _unitOfWork;
        private readonly IWebHostEnvironment _environment;

        public AdminService(ILogger<AdminService> logger, Context context, IWebHostEnvironment environment)
        {
            _logger = logger;
            _unitOfWork = new UoW(context);
            _environment = environment;
        }
      
        public async Task<GolfClub> GetProductAsync(int productId)
        {
            var club = await _unitOfWork.GolfClubRepository.GetByIdAsync(productId);
            if (club == null)
                throw new KeyNotFoundException($"Could not find club with {productId}");

            return club;
        }

       
        public async Task<string> UploadProductImageAsync(int productId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new KeyNotFoundException($"Please Select an image.");
            }

            var club = await _unitOfWork.GolfClubRepository.GetByIdAsync(productId);
            if (club == null)
                throw new KeyNotFoundException($"Could not find {productId}");

            var uploadsDir = Path.Combine(_environment.WebRootPath, "images", "products");
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

            return club.ImageUrl;
        }

       
        public async Task<List<GolfClub>> GetAllProductsAsync()
        {
            var clubs = await _unitOfWork.GolfClubRepository.GetAllAsync();
            return (clubs);
        }
    }
}
