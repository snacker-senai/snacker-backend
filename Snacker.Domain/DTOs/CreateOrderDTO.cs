using System.Collections.Generic;

namespace Snacker.Domain.DTOs
{
    public class CreateOrderDTO
    {
        public ICollection<ProductAndQuantityDTO> ProductsWithQuantity { get; set; }
        public long? TableId { get; set; }
    }
}
