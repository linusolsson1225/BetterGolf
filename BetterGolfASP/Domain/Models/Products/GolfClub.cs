namespace BetterGolfASP.Domain.Models.Products
{
    public abstract class GolfClub : Product
    {
        public RightOrLeftHanded Handedness { get; set; }

        public enum RightOrLeftHanded
        {
            Right,
            Left
        }

        protected GolfClub() { }

        protected GolfClub(string name, string description,decimal price,int? stock,List<string>? imgUrls,RightOrLeftHanded handedness):
            base(name, description, price, stock, imgUrls ?? new List<string>())
        {
            Handedness = handedness;
        }

    }

}