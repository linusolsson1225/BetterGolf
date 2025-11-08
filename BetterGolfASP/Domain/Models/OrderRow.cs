using BetterGolfASP.Domain.Models.Products;


namespace BetterGolfASP.Domain.Models
{
    public class OrderRow
    {
        public int OrderRowId { get; private set; }
        public int ProductId { get; private set; }
        public int? VariantId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        
        public ProductVariant? Variant { get; private set; }
        public Product Product { get; private set; }
        public decimal Total => Price * Quantity;

        protected OrderRow() { } 

        private OrderRow(Product product, int quantity, ProductVariant? variant = null)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ProductId = product.ProductId;

            Variant = variant;
            VariantId = variant?.VariantId;

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            Quantity = quantity;
            Price =  product.Price;
        }

        
        public static OrderRow Create(Product product, int quantity, ProductVariant? variant = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            return new OrderRow(product, quantity, variant);
        }
    }
}