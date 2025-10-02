using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BetterGolfASP.Models.WoodClub;

namespace BetterGolfASP.Models
{
    public abstract class GolfClub
    {
        public int GolfClubID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public RightOrLeftHanded Handedness { get; set; }
        public enum RightOrLeftHanded
        {
            Right,
            Left
        }
        protected GolfClub() { }
        public GolfClub(string name, string description, double price, int stock, RightOrLeftHanded handedness)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Handedness = handedness;
        }


    }
}