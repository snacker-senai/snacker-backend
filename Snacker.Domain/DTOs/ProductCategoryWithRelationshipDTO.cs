using System.Collections.Generic;

namespace Snacker.Domain.DTOs
{
    public class ProductCategoryWithRelationshipDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductWithoutRelationshipDTO> Products { get; set; }
    }
}
