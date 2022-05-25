using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class ProductCategoryDTO : BaseDTO
    {
        public ProductCategoryDTO(ProductCategory productCategory)
        {
            Id = productCategory.Id;
            Name = productCategory.Name;
        }
        public string Name { get; set; }
    }
}
