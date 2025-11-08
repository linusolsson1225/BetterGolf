using BetterGolfASP.Domain.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterGolfASP.Domain.Models
{
    public class ProductVariant
    {
        [Key]
        public int VariantId { get; set; }
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string AttributeName { get; private set; } = null!;
        public string AttributeValue { get; private set; } = null!;
        public int Stock { get; private set; }
        

        protected ProductVariant() { } 

        private ProductVariant(string attributeName, string attributeValue, int stock)
        {
            AttributeName = attributeName;
            AttributeValue = attributeValue;
            Stock = stock;
            
        }

        
        public static ProductVariant Create(
            string attributeName,
            string attributeValue,
            int stock)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException("Attribute name is required.", nameof(attributeName));

            if (string.IsNullOrWhiteSpace(attributeValue))
                throw new ArgumentException("Attribute value is required.", nameof(attributeValue));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));

            return new ProductVariant(attributeName, attributeValue, stock);
        }
        public void ReduceVariantStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (Stock < quantity)
                throw new InvalidOperationException("Not enough stock available.");

            Stock -= quantity;
        }

    }
}