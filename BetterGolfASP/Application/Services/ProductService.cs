using BetterGolfASP.Domain.Models.Products;
using BetterGolfASP.Infrastructure.DB;

namespace BetterGolfASP.Application.Services
{
    public class ProductService(ILogger<ProductService> logger, Context context)
    {
        private readonly ILogger<ProductService> _logger = logger;
        private readonly UoW _unitOfWork = new(context);

        // Retrieves all products of a specific type from the repository.
        // Generic method constrained to types derived from GolfClub.
        public async Task<IEnumerable<T>> GetAllByTypeAsync<T>() where T : GolfClub
        {
            return await _unitOfWork.ProductRepository.GetByTypeAsync<T>();
        }
        
        // Retrieves a product by its ID from the repository.
        public async Task<Product> GetDetailsAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new KeyNotFoundException($"No golf club with Id:{productId} found");
            }

            return product;
        }
        
        //Retrieves all products from the repository.
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }
    }
}
