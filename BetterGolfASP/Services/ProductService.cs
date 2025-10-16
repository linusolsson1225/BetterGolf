using BetterGolfASP.Controllers;
using BetterGolfASP.DB;
using BetterGolfASP.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace BetterGolfASP.Services
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
            return await _unitOfWork.GolfClubRepository.GetAllByTypeAsync<T>();
        }
        public async Task<GolfClub> GetDetailsAsync(int productId)
        {
            var product = await _unitOfWork.GolfClubRepository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new KeyNotFoundException($"No golf club with Id:{productId} found");
            }

            return (product);
        }
    }
}
