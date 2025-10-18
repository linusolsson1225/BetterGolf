using System.ComponentModel.DataAnnotations;

namespace BetterGolfASP.Models.Products
{
    public abstract class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int? Stock { get; set; }

        public List<string> ImgUrls { get; set; } = new();

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
            if (!HasVariants)
            {
                return Variants.Sum(v=>v.Stock);
            }
            return Stock ?? 0;
        }

    }
}
