using BetterGolfASP.Controllers;
using BetterGolfASP.DB;
using BetterGolfASP.Domain.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BetterGolfASP.Services
{
    public class ShoppingCartService
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private const string CartSessionKey = "ShoppingCart";
        private readonly ILogger<ShoppingCartService> _logger;
        private readonly UoW _unitOfWork;
        private ISession Session => _httpcontextAccessor.HttpContext.Session;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, ILogger<ShoppingCartService> logger, Context context)
        {
            _httpcontextAccessor = httpContextAccessor;
            _logger = logger;
            _unitOfWork = new UoW(context);
        }
        
        public List<CartItem> GetItems()
        {
            var data = Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(data))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(data);
        }

        public void SaveItems(List<CartItem> items)
        {
            Session.SetString(CartSessionKey,JsonConvert.SerializeObject(items));
        }

        public async Task AddItemToCart(int productId,int quantity)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Golf club with ID {productId} not found");
            }
            var items = GetItems();
            var existingItem = items.FirstOrDefault(x=>x.ProductID == product.ProductID);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                
                var newItem = CartItem.Create(product.ProductID, product.Name, product.Price, quantity /*product.ImageUrl*/);
                items.Add(newItem);

            }
            SaveItems(items);
        }
        public void RemoveItem(int productID)
        {
            var items = GetItems();
            var toRemove = items.FirstOrDefault(x => x.ProductID == productID);
            if (toRemove!=null)
            {
                items.Remove(toRemove);
            }
            SaveItems(items);
        }
        public decimal CalculateTotalPrice()
        {
            var items = GetItems(); 
            return items.Sum(x => x.Price * x.Quantity);
        }

    }
}
