using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterGolfASP.Infrastructure.DB;
using BetterGolfASP.Domain.Models;

namespace BetterGolfASP.Infrastructure.Repositories
{
    public class OrderRowRepository:Repository<OrderRow>
    {
        public OrderRowRepository(Context context) : base(context) { }
    }
}
