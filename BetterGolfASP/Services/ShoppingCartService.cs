using BetterGolfASP.Controllers;
using BetterGolfASP.DB;
using BetterGolfASP.Models;
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
            var club = await _unitOfWork.GolfClubRepository.GetByIdAsync(productId);
            if (club == null)
            {
                throw new KeyNotFoundException($"Golf club with ID {productId} not found");
            }
            var items = GetItems();
            var existingItem = items.FirstOrDefault(x=>x.ProductID == club.GolfClubID);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                
                var newItem = CartItem.Create(club.GolfClubID, club.Name, club.Price, quantity, club.ImageUrl);
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
        public double CalculateTotalPrice()
        {
            var items = GetItems().ToList();
            return items.Sum(x => x.Price * x.Quantity);
        }
    }
}
