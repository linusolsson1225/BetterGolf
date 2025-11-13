
using BetterGolfASP.Infrastructure.DB;
using BetterGolfASP.Domain.Models.Products;

namespace BetterGolfASP.Application.Services
{
    public class AdminService(ILogger<AdminService> logger, Context context, IWebHostEnvironment environment)
    {
        private readonly UoW _unitOfWork = new UoW(context);
        
        //Fetching a specific product from database
        public async Task<Product> GetProductAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            return product ?? throw new KeyNotFoundException($"Could not find product with {productId}");
        }
        
        //Remove Selected imageUrl from a specific product and updates product image URL in database
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
        // Uploads an image file for a specific product.
        // Saves the file to the server, updates the product’s image URLs in the database,
        // and returns the URL of the uploaded image
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

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            
            var imageUrl = $"/images/products/{fileName}";
            product.ImgUrls.Add(imageUrl);

            
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return imageUrl;
        }
        
        //Getting all products to a list
        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return products;
        }
    }
}
