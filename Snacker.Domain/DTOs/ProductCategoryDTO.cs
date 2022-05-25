using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class ProductCategoryDTO
    {
        public ProductCategoryDTO(ProductCategory productCategory)
        {
            Name = productCategory.Name;
        }
        public string Name { get; set; }
    }
}
