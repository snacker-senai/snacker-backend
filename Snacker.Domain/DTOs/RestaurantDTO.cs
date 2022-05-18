namespace Snacker.Domain.DTOs
{
    public class RestaurantDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDTO Address { get; set; }
        public long AddressId { get; set; }
        public RestaurantCategoryDTO RestaurantCategory { get; set; }
        public long RestaurantCategoryId { get; set; }
    }
}
