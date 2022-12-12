using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.DTOs
{
    public class ProductTopSellingDTO : BaseDTO
    {
        public ProductTopSellingDTO(Product product, int quantity)
        {
            Id = product.Id;
            Name = product.Name;
            Quantity = quantity;
            Category = new ProductCategoryDTO(product.ProductCategory);
        }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ProductCategoryDTO Category { get; set; }
    }
}
