using BetterGolfASP.DB;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repositories
{
    public class OrderRepository: Repository<Order>
    {
        public OrderRepository(Context context) : base(context) { }
    }
}
