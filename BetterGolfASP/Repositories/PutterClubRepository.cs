using BetterGolfASP.DB;
using BetterGolfASP.Models.Products;
using DB.Repositories;

namespace BetterGolfASP.Repositories
{
    public class PutterClubRepository : Repository<PutterClub>
    {
        public PutterClubRepository(Context context) : base(context) { }
    }
}
