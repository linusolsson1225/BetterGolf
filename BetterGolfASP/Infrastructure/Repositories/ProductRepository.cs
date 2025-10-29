using BetterGolfASP.Domain.Models.Products;
using BetterGolfASP.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace BetterGolfASP.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Variants)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            return await _context.Products
                .Include(p => p.Variants)
                .FirstOrDefaultAsync(p => p.ProductID == productId);
        }

        public async Task<List<T>> GetByTypeAsync<T>() where T : Product
        {
            return await _context.Products
                .OfType<T>()
                .Include(p => p.Variants)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }

}
