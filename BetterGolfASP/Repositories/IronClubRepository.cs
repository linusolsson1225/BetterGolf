using BetterGolfASP.DB;
using BetterGolfASP.Models;
using DB.Repositories;
using Models;

namespace BetterGolfASP.Repositories
{
    public class IronClubRepository: Repository<IronClub>
    {
        public IronClubRepository(Context context) : base(context) { }
    }
}
