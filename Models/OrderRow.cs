using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderRow
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;

        private OrderRow(Item item, int quantity)
        {
            Item = item;
            ItemId = item.Id;
            Quantity = quantity;
            Price = item.Price;
        }

        public static OrderRow Create(Item item, int quantity)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            return new OrderRow(item, quantity);
        }

    }
}
