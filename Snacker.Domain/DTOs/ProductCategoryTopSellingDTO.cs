using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.DTOs
{
    public class ProductCategoryTopSellingDTO : BaseDTO
    {
        public ProductCategoryTopSellingDTO(ProductCategory productCategory, int quantity)
        {
            Id = productCategory.Id;
            Name = productCategory.Name;
            Quantity = quantity;
        }

        public ProductCategoryTopSellingDTO(ProductCategoryDTO productCategory, int quantity)
        {
            Id = productCategory.Id;
            Name = productCategory.Name;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
