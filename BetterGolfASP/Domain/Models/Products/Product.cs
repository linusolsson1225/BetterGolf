using System.ComponentModel.DataAnnotations;

namespace BetterGolfASP.Domain.Models.Products
{
    public abstract class Product
    {
        [Key]
        public int ProductId { get; private set; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        public decimal Price { get; private set; }

        public int? Stock { get; private set; }

        public List<string> ImgUrls { get; init; } = new();

        public List<ProductVariant> Variants { get; private set; } = new();

        protected Product() { }

        protected Product(string name, string description, decimal price, int? stock, List<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock.HasValue && stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImgUrls = imgUrls ?? new List<string>();
        }

        public void AddVariant(ProductVariant variant)
        {
            if(variant == null) 
                throw new ArgumentNullException(nameof(variant));
            Variants.Add(variant);
        }
        public bool HasVariants=>Variants.Count > 0;

        public int GetTotalStock()
        {
            if (HasVariants)
            {
                return Variants.Sum(v => v.Stock);
            }

            return Stock ?? 0;
        }
        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (Stock < quantity)
                throw new InvalidOperationException("Not enough stock available.");

            Stock -= quantity;
        }

        

    }
}
