using Models;

namespace BetterGolfASP.Models
{
    public class WoodClub : GolfClub
    {
        public double Loft { get; set; }
        public TypeOfWood WoodType { get; set; }
        
        public enum TypeOfWood
        {
            Hybrid,
            Driver,
            Spoon
        }
        protected WoodClub() { }
        private WoodClub(string name, string description, double price, int stock, double loft, TypeOfWood typeOfWood, RightOrLeftHanded handedness):base(name, description, price, stock, handedness)
        {
            Loft = loft;
            WoodType = typeOfWood;
        }
        public static WoodClub Create(string name, string description, double price, int stock, double loft, TypeOfWood typeOfWood, RightOrLeftHanded handedness)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));
            

            return new WoodClub(name, description, price, stock, loft,typeOfWood,handedness);
        }
    }
}
