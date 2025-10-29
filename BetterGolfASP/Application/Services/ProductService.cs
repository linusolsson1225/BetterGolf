using BetterGolfASP.Domain.Models.Products;
using BetterGolfASP.Infrastructure.DB;

namespace BetterGolfASP.Application.Services
{
    public class ProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly UoW _unitOfWork;

        public ProductService(ILogger<ProductService> logger, Context context)
        {
            _logger = logger;
            _unitOfWork = new UoW(context);
        }

        public async Task<IEnumerable<T>> GetAllByTypeAsync<T>() where T : GolfClub
        {
            return await _unitOfWork.ProductRepository.GetByTypeAsync<T>();
        }
        public async Task<Product> GetDetailsAsync(int productId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new KeyNotFoundException($"No golf club with Id:{productId} found");
            }

            return product;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }
    }
}
