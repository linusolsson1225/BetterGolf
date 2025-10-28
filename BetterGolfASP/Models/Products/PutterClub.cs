using BetterGolfASP.Models;
using static BetterGolfASP.Models.Products.WoodClub;

namespace BetterGolfASP.Models.Products
{
    public class PutterClub : GolfClub
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

        protected PutterClub() { }

        private PutterClub(string name, string description, decimal price, int stock,ShaftType typeOfShaft, PutterType typeOfPutter,RightOrLeftHanded handedness,List<string>? imgUrls = null)
            : base(name, description, price, stock, imgUrls ?? new List<string>(), handedness)
        {
            TypeOfShaft = typeOfShaft;
            TypeOfPutter = typeOfPutter;
        }

        public static PutterClub Create(string name,string description,decimal price,int stock,ShaftType typeOfShaft,PutterType typeOfPutter,RightOrLeftHanded handedness,IEnumerable<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));

            return new PutterClub(
                name,
                description,
                price,
                stock,
                typeOfShaft,
                typeOfPutter,
                handedness,
                imgUrls?.ToList());
        }

    }

}