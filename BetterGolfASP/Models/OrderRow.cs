using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderRow
    {
        public int OrderRowID { get; set; }
        public int GolfclubID { get; set; }
        public GolfClub? Golfclub { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total => Price * Quantity;

        protected OrderRow() { }

        private OrderRow(GolfClub Golfclub, int quantity)
        {
            this.Golfclub = Golfclub;
            GolfclubID = Golfclub.GolfClubID;
            Quantity = quantity;
            Price = Golfclub.Price;
        }

        public static OrderRow Create(GolfClub item, int quantity)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Item cannot be null.");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            return new OrderRow(item, quantity);
        }

    }
}
