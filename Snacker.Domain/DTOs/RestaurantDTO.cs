using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class RestaurantDTO : BaseDTO
    {
        public RestaurantDTO()
        {

        }
        public RestaurantDTO(Restaurant restaurant)
        {
            Id = restaurant.Id;
            Name = restaurant.Name;
            Description = restaurant.Description;
            Address = new AddressDTO(restaurant.Address);
            AddressId = restaurant.AddressId;
            RestaurantCategory = new RestaurantCategoryDTO(restaurant.RestaurantCategory);
            RestaurantCategoryId = restaurant.RestaurantCategoryId;
            Active = restaurant.Active;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public AddressDTO Address { get; set; }
        public long AddressId { get; set; }
        public RestaurantCategoryDTO RestaurantCategory { get; set; }
        public long RestaurantCategoryId { get; set; }
    }
}
