using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DB.Repositories
{
    public class CustomerRepository: Repository<Customer>
    {
        public CustomerRepository(Context context) : base(context) { }
    }
}
