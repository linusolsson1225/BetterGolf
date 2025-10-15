using BetterGolfASP.DB;
using BetterGolfASP.Models;
using DB.Repositories;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BetterGolfASP.Repositories
{
    public class GolfClubsRepository : Repository<GolfClub>
    {
        private readonly Context _context;
        public GolfClubsRepository(Context context) : base(context) 
        {
            _context = context;
        }
        public async Task<GolfClub> GetByIdAsync(int golfClubId)
        {
            var golfClub = await _context.GolfClubs
                .FirstOrDefaultAsync(o => o.GolfClubID == golfClubId);

            if (golfClub == null)
                throw new KeyNotFoundException($"Golf club wiht product id of {golfClubId} not found.");
             
            return golfClub;
        }
        public async Task<IEnumerable<T>>GetAllByTypeAsync<T>()
        {
            var golfClubs = await _context.GolfClubs.
                OfType<T>().
                ToListAsync();
            if (golfClubs == null)
                throw new KeyNotFoundException("No golf clubs found.");
            return golfClubs;
        }
        public async Task<List<GolfClub>> GetAllAsync()
        {
            var golfClubs = await _context.GolfClubs.ToListAsync();

            if (golfClubs == null)
                throw new KeyNotFoundException("No golf clubs.");
            return golfClubs;
        }
    }
}

