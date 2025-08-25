using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI;

namespace BetterGolf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SeedDatabase seed = new SeedDatabase();
            seed.Seed();

        }
    }
}
