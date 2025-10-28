namespace BetterGolfASP.Domain.Cart
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity {  get; set; }
        public string? ImageUrl { get; set; }
        
        public CartItem()
        {
                
        }
        private CartItem(int productID, string name, decimal price, int quantity, string? imageUrl)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageUrl = imageUrl;
            
        }
        
        public static CartItem Create(int productID, string name, decimal price, int quantity, string? imageUrl=null)
        {
            if (productID <= 0)
                throw new ArgumentException("ProductID must be greater than zero.", nameof(productID));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));
            if (quantity <= 0)
                throw new ArgumentException("Quantity can not be 0.", nameof(quantity));

            return new CartItem(productID, name, price, quantity, imageUrl);
        }


    }
}
