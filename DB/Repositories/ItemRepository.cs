using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DB.Repositories
{
    public class ItemRepository : Repository<Item>
    {
        public ItemRepository(Context context) : base (context) { }
    }
}
