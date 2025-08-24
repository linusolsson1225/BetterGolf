namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public string Status { get; set; }
        public List<OrderRow> OrderRows { get; set; } = new();

        private Order(Customer customer, List<OrderRow>? items = null)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            CustomerId = customer.Id;
            OrderDate = DateTime.UtcNow;
            Status = "Pending";
            OrderRows = items ?? new List<OrderRow>();
        }

        public static Order Create(Customer customer, List<OrderRow>? items = null)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Order must contain at least one item.", nameof(items));

            return new Order(customer, items);
        }
    }
}
