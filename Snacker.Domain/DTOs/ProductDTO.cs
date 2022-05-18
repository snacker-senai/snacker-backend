namespace Snacker.Domain.DTOs
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public ProductCategoryDTO ProductCategory { get; set; }
        public long ProductCategoryId { get; set; }
        public RestaurantDTO Restaurant { get; set; }
        public long RestaurantId { get; set; }
    }
}
