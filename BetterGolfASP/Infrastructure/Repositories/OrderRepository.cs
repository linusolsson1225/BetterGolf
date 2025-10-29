using BetterGolfASP.Domain.Models;
using BetterGolfASP.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGolfASP.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        private readonly Context _context;

        public OrderRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
                throw new KeyNotFoundException($"Order with ID {orderId} not found.");

            return order;
        }
        
        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();

            if (orders == null)
                throw new KeyNotFoundException("No orders found");
            return orders;  
        }
    }
}
