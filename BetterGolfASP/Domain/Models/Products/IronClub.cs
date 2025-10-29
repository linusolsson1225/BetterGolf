namespace BetterGolfASP.Domain.Models.Products
{
    public class IronClub : GolfClub
    {
        public TypeOfIron IronType { get; private set; }

        public enum TypeOfIron
        {
            Blade,
            CavityBack,
            MuscleBack
        }

       
        protected IronClub() { }

        private IronClub(string name,string description,decimal price,int stock,TypeOfIron ironType,RightOrLeftHanded handedness,List<string>? imgUrls = null):
            base(name, description, price, stock, imgUrls ?? new List<string>(), handedness)
        {
            IronType = ironType;
        }

        public static IronClub Create(string name,string description,decimal price,int stock,TypeOfIron ironType,RightOrLeftHanded handedness,IEnumerable<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));

            return new IronClub(name, description, price, stock, ironType, handedness, imgUrls?.ToList());
        }
    }

}