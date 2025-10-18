using BetterGolfASP.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterGolfASP.Models;

namespace DB.Repositories
{
    public class OrderRowRepository:Repository<OrderRow>
    {
        public OrderRowRepository(Context context) : base(context) { }
    }
}
