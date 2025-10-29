using BetterGolfASP.Domain.Models;

namespace BetterGolfASP.Domain.Models.Products
{
    public class WoodClub : GolfClub
    {
        public TypeOfWood WoodType { get; private set; }

        public enum TypeOfWood
        {
            Hybrid,
            Driver,
            Spoon
        }

        protected WoodClub() { }

        private WoodClub(
            string name,string description,decimal price,TypeOfWood typeOfWood,RightOrLeftHanded handedness,List<string>? imgUrls = null) :
            base(name, description, price, null, imgUrls ?? new List<string>(), handedness)
        {
            WoodType = typeOfWood;
        }

        public static WoodClub Create(string name,string description,decimal price,TypeOfWood typeOfWood,RightOrLeftHanded handedness,IEnumerable<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            return new WoodClub(name, description, price, typeOfWood, handedness, imgUrls?.ToList());
        }
        public ProductVariant AddLoftVariant(double loft, int stock)
        {
            if (loft <= 0)
                throw new ArgumentException("Loft must be greater than 0",nameof(loft));
            if (stock<0)
                throw new ArgumentException("Stock can not be negative",nameof(stock));
            var variant = ProductVariant.Create("Loft",$"{loft}",stock);
            AddVariant(variant);
            return variant;
        }
        public IEnumerable<string>GetAvaliableLofts()
        {
            return Variants.Select(v=>v.AttributeValue);
        }
    }

}
