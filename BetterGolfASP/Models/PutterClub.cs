using Models;
using static BetterGolfASP.Models.WoodClub;

namespace BetterGolfASP.Models
{
    public class PutterClub:GolfClub
    {
        public ShaftType TypeOfShaft { get; set; }
        public PutterType TypeOfPutter { get; set; }
        public enum ShaftType
        {
            Standard,
            Armlock,
            Broomstick
        }

        public enum PutterType
        {
            Mallet,
            Blade
        }

        private PutterClub(string name, string description, double price, int stock, double loft, ShaftType typeOfShaft, PutterType typeOfPutter, RightOrLeftHanded handedness) : base(name, description, price, stock, handedness)
        {
            TypeOfShaft = typeOfShaft;
            TypeOfPutter = typeOfPutter;
        }

        public static PutterClub Create(string name, string description, double price, int stock, double loft, ShaftType typeOfShaft, PutterType typeOfPutter, RightOrLeftHanded handedness)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));


            return new PutterClub(name, description, price, stock, loft, typeOfShaft, typeOfPutter, handedness);
        }
    }
}
