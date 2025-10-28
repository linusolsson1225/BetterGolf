using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterGolfASP.DB;
using Microsoft.EntityFrameworkCore;
using BetterGolfASP.Models;

namespace DB.Repositories
{
    public class CustomerRepository: Repository<Customer>
    {
        private readonly Context _context;
        public CustomerRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
