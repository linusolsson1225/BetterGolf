using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using static BetterGolfASP.Models.Products.IronClub;

namespace BetterGolfASP.Domain.Cart
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity {  get; set; }
        public string ImageUrl { get; set; }
        private CartItem(int productID, string name, double price, int quantity, string imageUrl)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageUrl = imageUrl;
            
        }
        public CartItem()
        {
                
        }
        public static CartItem Create(int productID, string name, double price, int quantity, string imageUrl)
        {
            if (productID <= 0)
                throw new ArgumentException("ProductID must be greater than zero.", nameof(productID));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (price <= 0)
                throw new ArgumentException("Price must be greater than zero.", nameof(price));
            if (quantity <= 0)
                throw new ArgumentException("Quantity can not be 0.", nameof(quantity));
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new ArgumentException("Image URL is required.", nameof(imageUrl));

            return new CartItem(productID, name, price, quantity, imageUrl);
        }


    }
}
