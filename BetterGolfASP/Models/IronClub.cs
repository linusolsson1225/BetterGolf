using Models;
using static BetterGolfASP.Models.WoodClub;
using static Models.GolfClub;

namespace BetterGolfASP.Models
{
    public class IronClub:GolfClub
    {
        public int IronClubID { get; set; }
        public TypeOfIron IronType { get; set; }

        public enum TypeOfIron
        {
            Blade,
            CavityBack,
            MuscleBack,
        }

        private IronClub(string name, string description, double price, int stock, double loft, TypeOfIron irontype, RightOrLeftHanded handedness) : base(name, description, price, stock, handedness)
        {
            IronType = irontype;
        }
        public static IronClub Create(string name, string description, double price, int stock, double loft, TypeOfIron typeOfIron, RightOrLeftHanded handedness)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));


            return new IronClub(name, description, price, stock, loft, typeOfIron, handedness);
        }


    }
}
