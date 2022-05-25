using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class ProductDTO
    {
        public ProductDTO(Product product)
        {
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Image = product.Image;
            Active = product.Active;
            ProductCategory = new ProductCategoryDTO(product.ProductCategory);
            ProductCategoryId = product.ProductCategoryId;
            Restaurant = new RestaurantDTO(product.Restaurant);
            RestaurantId = product.RestaurantId;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public ProductCategoryDTO ProductCategory { get; set; }
        public long ProductCategoryId { get; set; }
        public RestaurantDTO Restaurant { get; set; }
        public long RestaurantId { get; set; }
        public bool Active { get; set; }
    }
}
