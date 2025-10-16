using BetterGolfASP.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterGolfASP.Models
{
    public class ProductVariant
    {
        [Key]
        public int VariantID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public string AttributeName { get; private set; } = null!;
        public string AttributeValue { get; private set; } = null!;
        public int Stock { get; private set; }
        public List<string> ImgUrls { get; private set; } = new();

        protected ProductVariant() { } 

        private ProductVariant(string attributeName, string attributeValue, int stock, List<string>? imgUrls = null)
        {
            AttributeName = attributeName;
            AttributeValue = attributeValue;
            Stock = stock;
            ImgUrls = imgUrls ?? new List<string>();
        }

        
        public static ProductVariant Create(
            string attributeName,
            string attributeValue,
            int stock,
            List<string>? imgUrls = null)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
                throw new ArgumentException("Attribute name is required.", nameof(attributeName));

            if (string.IsNullOrWhiteSpace(attributeValue))
                throw new ArgumentException("Attribute value is required.", nameof(attributeValue));

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.", nameof(stock));

            return new ProductVariant(attributeName, attributeValue, stock, imgUrls);
        }
    }
}