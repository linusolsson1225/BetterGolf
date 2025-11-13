using BetterGolfASP.Domain.Cart;
using BetterGolfASP.Domain.Models.Products;
using BetterGolfASP.Infrastructure.DB;
using Newtonsoft.Json;

namespace BetterGolfASP.Application.Services
{
    public class ShoppingCartService(IHttpContextAccessor httpContextAccessor, Context context)
    {
        private const string CartSessionKey = "ShoppingCart";
        private readonly UoW _unitOfWork = new(context);

        private ISession Session => httpContextAccessor.HttpContext.Session;

        // Retrieves the current cart items from the session.
        // Returns an empty list if no cart data is found.
        public List<CartItem> GetItems()
        {
            var data = Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(data))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(data);
        }
        
        // Saves the provided list of cart items to the session as a JSON string.
        private void SaveItems(List<CartItem> items)
        {
            Session.SetString(CartSessionKey, JsonConvert.SerializeObject(items));
        }
        
        // Adds a product (and variant) to the cart.
        // Validates the product, updates the quantity if it already exists, and saves the cart to the session.
        public async Task AddItemToCart(int productId, int quantity, int? variantId = null)
        {
            var product = await GetAndValidateProductAsync(productId, variantId);
            var items = GetItems();

            AddOrUpdateCartItem(items, product, quantity, variantId);

            SaveItems(items);
        }
        
        // Fetches a product by ID and validates its existence.
        // Also validates the specified variant if the product has variants, throwing exceptions if not found.
        private async Task<Product> GetAndValidateProductAsync(int productId, int? variantId)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId)
                          ?? throw new KeyNotFoundException($"Product with ID {productId} not found");

            if (product.HasVariants && variantId == null)
                throw new ArgumentException("Variant ID must be provided for products with variants.");

            if (product.HasVariants && product.Variants.All(v => v.VariantId != variantId))
                throw new KeyNotFoundException($"Variant with ID {variantId} not found.");

            return product;
        }
        
        // Adds a new cart item or updates the quantity of an existing item in the cart.
        // Handles both products with variants and those without.
        private static void AddOrUpdateCartItem(List<CartItem> items, Product product, int quantity, int? variantId)
        {
            CartItem? existingItem;

            if (product.HasVariants)
            {
                existingItem = items.FirstOrDefault(x => x.ProductId == product.ProductId && x.VariantId == variantId);
                if (existingItem != null)
                    existingItem.Quantity += quantity;
                else
                {
                    var variant = product.Variants.First(v => v.VariantId == variantId);
                    items.Add(CartItem.Create(product.ProductId, product.Name, product.Price, quantity,
                        product.ImgUrls.FirstOrDefault(), variant.VariantId, variant.AttributeName, variant.AttributeValue));
                }
            }
            else
            {
                existingItem = items.FirstOrDefault(x => x.ProductId == product.ProductId && x.VariantId == null);
                if (existingItem != null)
                    existingItem.Quantity += quantity;
                else
                    items.Add(CartItem.Create(product.ProductId, product.Name, product.Price, quantity,
                        product.ImgUrls.FirstOrDefault()));
            }
        }
        
        // Removes a product (or specific variant) from the cart if it exists and updates the session.
        public void RemoveItem(int productId, int? variantId = null)
        {
            var items = GetItems();
            var item = items.FirstOrDefault(x => x.ProductId == productId && x.VariantId == variantId);
            if (item != null)
                items.Remove(item);

            SaveItems(items);
        }
        
        // Updates the quantity of a specific cart item based on the given action ("increase" or "decrease").
        // Removes the item from the cart if the quantity drops to zero or below, and saves the updated cart.
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
