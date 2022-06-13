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

        }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
       
    }
}
