using BetterGolfASP.DB;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repositories
{
    public class OrderRowRepository:Repository<OrderRow>
    {
        public OrderRowRepository(Context context) : base(context) { }
    }
}
