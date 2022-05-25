using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class ProductWithoutRelationshipDTO
    {
        public ProductWithoutRelationshipDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Image = product.Image;
            Active = product.Active;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
    }
}
