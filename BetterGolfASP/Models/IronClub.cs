using Models;

namespace BetterGolfASP.Models
{
    public class IronClub : GolfClub
    {
        public TypeOfIron IronType { get; set; }

        public enum TypeOfIron
        {
            Blade,
            CavityBack,
            MuscleBack,
        }
        protected IronClub() { }
        private IronClub(string name, string description, double price, int stock, TypeOfIron irontype, RightOrLeftHanded handedness) : base(name, description, price, stock, handedness)
        {
            IronType = irontype;
        }
        public static IronClub Create(string name, string description, double price, int stock, TypeOfIron typeOfIron, RightOrLeftHanded handedness)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));


            return new IronClub(name, description, price, stock, typeOfIron, handedness);
        }


    }
}