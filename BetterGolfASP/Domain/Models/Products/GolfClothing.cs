using BetterGolfASP.Domain.Models;

namespace BetterGolfASP.Domain.Models.Products
{
    public enum ClothingType
    {
        Top,
        Bottom,
        Headwear,
        Shoes
    }

    public class Clothing : Product
    {
        public ClothingType Type { get; private set; }

        protected Clothing() { }

        private Clothing(string name, string description, decimal price, ClothingType type, List<string>? imgUrls = null)
            : base(name, description, price, null, imgUrls ?? new List<string>())
        {
            Type = type;
        }

        public static Clothing Create(string name, string description, decimal price, ClothingType type, IEnumerable<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            return new Clothing(name, description, price, type, imgUrls?.ToList());
        }

        public ProductVariant AddSizeVariant(string size, int stock)
        {
            if (Type == ClothingType.Headwear)
                throw new InvalidOperationException("Headwear does not support size variants.");

            if (string.IsNullOrWhiteSpace(size))
                throw new ArgumentException("Size is required", nameof(size));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative", nameof(stock));

            var variant = ProductVariant.Create("Size", size, stock);
            AddVariant(variant);
            return variant;
        }
    }
}
