using BetterGolfASP.DB;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private readonly Context _context;

        public OrderRepository(Context context) : base(context)
        {
            _context = context;
        }

        public Order GetById(int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderID == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order not found.");

            return order;
        }
    }
}
