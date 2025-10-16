using BetterGolfASP.DB;
using BetterGolfASP.Models.Products;
using DB.Repositories;

namespace BetterGolfASP.Repositories
{
    public class WoodClubRepository : Repository<WoodClub>
    {
        public WoodClubRepository(Context context) : base(context) { }
    }
}
