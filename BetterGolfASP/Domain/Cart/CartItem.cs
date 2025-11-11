namespace BetterGolfASP.Domain.Cart
{
    public class CartItem
    {
        public int ProductId { get;  set; }
        public int? VariantId { get; set; }
        public string? VariantName { get; set; }
        public string? VariantAttributeValue { get; set; }
        public string Name { get;  set; }
        public decimal Price { get;  set; }
        public int Quantity {  get; set; }
        public string? ImageUrl { get;  set; }
        
        public CartItem()
        {
                
        }

        private CartItem(int productId, string name, decimal price, int quantity, string? imageUrl,
            int? variantId = null,string? variantName=null,string? variantAttributeValue=null)
        {
            ProductId = productId;
            VariantId = variantId;
            VariantName = variantName;
            VariantAttributeValue = variantAttributeValue;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageUrl = imageUrl;
            
        }
        
        public static CartItem Create(int productId, string name, decimal price, int quantity, string? imageUrl = null, int? variantId = null, string? variantName=null,string? variantAttributeValue=null)
        {
            if (productId <= 0)
                throw new ArgumentException("ProductID must be greater than zero.", nameof(productId));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));
            if (quantity <= 0)
                throw new ArgumentException("Quantity cannot be 0.", nameof(quantity));
            
            return new CartItem(productId, name, price, quantity, imageUrl, variantId,variantName,variantAttributeValue);
        }
    }
}
