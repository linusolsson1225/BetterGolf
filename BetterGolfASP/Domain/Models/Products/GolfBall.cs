using BetterGolfASP.Domain.Models;

namespace BetterGolfASP.Domain.Models.Products
{
    public class GolfBall:Product
    {
        private GolfBall(string name, string description, decimal price, List<string>? imgUrls) :
           base(name, description, price, null, imgUrls ?? new List<string>())
        {
            
        }
        public static GolfBall Create(string name, string description, decimal price, IEnumerable<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            

            return new GolfBall(name, description, price, imgUrls?.ToList());
        }
        public ProductVariant AddPackageSizeVariant(int packageSize,int stock)
        {
            if (packageSize <= 0)
                throw new ArgumentException("Can not have package size of 0",nameof(packageSize));
            if (stock < 0)
                throw new ArgumentException("Stock can not be negative",nameof(stock));
            var variant = ProductVariant.Create("PackageSize", $"{packageSize}", stock);
            AddVariant(variant);
            return variant;
        }

    }
}
