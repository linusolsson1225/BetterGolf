using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Infrastructure.DB;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BetterGolfASP.Application.Services
{
    public class ShoppingCartService
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UoW _unitOfWork;

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, Context context)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = new UoW(context);
        }

        public List<CartItem> GetItems()
        {
            var data = Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(data))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(data);
        }

        public void SaveItems(List<CartItem> items)
        {
            Session.SetString(CartSessionKey, JsonConvert.SerializeObject(items));
        }

        public async Task AddItemToCart(int productId, int quantity, int? variantId = null)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productId} not found");

            var items = GetItems();

            if (product.HasVariants)
            {
                if (variantId == null)
                    throw new ArgumentException("Variant ID must be provided for products with variants.");

                var variant = product.Variants.FirstOrDefault(v => v.VariantId == variantId);
                if (variant == null)
                    throw new KeyNotFoundException($"Variant with ID {variantId} not found.");

                var existingItem = items.FirstOrDefault(x => x.ProductId == productId && x.VariantId == variantId);
                if (existingItem != null)
                    existingItem.Quantity += quantity;
                else
                    items.Add(CartItem.Create(product.ProductId, product.Name, product.Price, quantity, product.ImgUrls.FirstOrDefault(), variant.VariantId));
            }
            else
            {
                var existingItem = items.FirstOrDefault(x => x.ProductId == productId && x.VariantId == null);
                if (existingItem != null)
                    existingItem.Quantity += quantity;
                else
                    items.Add(CartItem.Create(product.ProductId, product.Name, product.Price, quantity, product.ImgUrls.FirstOrDefault()));
            }

            SaveItems(items);
        }

        public void RemoveItem(int productId, int? variantId = null)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(x => x.ProductId == productId && x.VariantId == variantId);
            if (item != null)
                items.Remove(item);

            SaveItems(items);
        }

        public void UpdateQuantity(int productId, int? variantId, string action)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(x => x.ProductId == productId && x.VariantId == variantId);
            if (item == null) return;

            if (action == "increase") item.Quantity++;
            else if (action == "decrease")
            {
                item.Quantity--;
                if (item.Quantity <= 0) items.Remove(item);
            }

            SaveItems(items);
        }

        public int GetCartCount() => GetItems().Sum(x => x.Quantity);

        public decimal CalculateTotalPrice() => GetItems().Sum(x => x.Price * x.Quantity);

        public void Clear() => Session.Remove(CartSessionKey);
    }
}
