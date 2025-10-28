using BetterGolfASP.DB;
using Microsoft.AspNetCore.Mvc;
using BetterGolfASP.Models.Products;

namespace BetterGolfASP.Services
{
    public class AdminService(ILogger<AdminService> logger, Context context, IWebHostEnvironment environment)
    {
        private readonly UoW _unitOfWork = new UoW(context);

        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            return product ?? throw new KeyNotFoundException($"Could not find product with {productId}");
        }

        public async Task<Product> RemoveImageAsync(int productId, string imageUrl)
        {
            var product = await GetProductAsync(productId);
            bool wasRemoved = product.ImgUrls.Remove(imageUrl);

            if (wasRemoved)
            {
                
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Image URL '{imageUrl}' not found for product {productId}.");
            }

            return product;
        }

        public async Task<string> UploadProductImageAsync(int productId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                
                throw new ArgumentException("Please select an image.", nameof(file));
            }
            
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null)
                throw new KeyNotFoundException($"Could not find product with ID: {productId}");

            var uploadsDir = Path.Combine(environment.WebRootPath, "images", "products");
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            
            var imageUrl = $"/images/products/{fileName}";
            product.ImgUrls.Add(imageUrl);

            
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return imageUrl;
        }


        public async Task<List<Product>> GetAllProductsAsync()
        {
            var clubs = await _unitOfWork.ProductRepository.GetAllAsync();
            return (clubs);
        }
    }
}
