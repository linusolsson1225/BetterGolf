using BetterGolfASP.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterGolfASP.Models
{
    public class ProductVariant
    {
        [Key]
        public int VariantID { get; set; }
        
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public string AttributeName { get; private set; } = null!;
        public string AttributeValue { get; private set; } = null!;
        public int Stock { get; private set; }
        //public List<string> ImgUrls { get; private set; } = new();

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
    }
}