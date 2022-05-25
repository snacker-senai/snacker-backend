using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.DTOs
{
    public class ProductCategoryWithRelationshipDTO
    {
        public ProductCategoryWithRelationshipDTO(ProductCategory productCategory)
        {
            Id = productCategory.Id;
            Name = productCategory.Name;
            Products = new List<ProductWithoutRelationshipDTO>();
            foreach (var product in productCategory.Products)
            {
                Products.Add(new ProductWithoutRelationshipDTO(product));
            }
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductWithoutRelationshipDTO> Products { get; set; }
    }
}
