using Models;

namespace BetterGolfASP.Models
{
    public class Order
    {
        public enum OrderStatus
        {
            Pending,
            Packing,
            Sent
        }

        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }
        public Customer? Customer { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderRow> OrderRows { get; set; } = new();

        protected Order() { }

        private Order(Customer customer, List<OrderRow> items)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            CustomerID = customer.CustomerID;

            if (items == null || items.Count == 0)
                throw new ArgumentException("Order must contain at least one item.", nameof(items));

            OrderRows = items;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }
        public static Order Create(Customer customer, List<OrderRow> items)
        {
            return new Order(customer, items);
        }

        public void MarkPacking() => Status = OrderStatus.Packing;
        public void MarkSent() => Status = OrderStatus.Sent;
    }
}
