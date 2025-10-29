using BetterGolfASP.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGolfASP.Domain.Models
{
    public class OrderRow
    {
        public int OrderRowId { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Price * Quantity;

        protected OrderRow() { } 

        private OrderRow(Product product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ProductID = product.ProductID;
            Quantity = quantity;
            Price = product.Price;
        }

        public static OrderRow Create(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            return new OrderRow(product, quantity);
        }
    }
}