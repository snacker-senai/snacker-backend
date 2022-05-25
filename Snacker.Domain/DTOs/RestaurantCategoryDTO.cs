using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class RestaurantCategoryDTO
    {
        public RestaurantCategoryDTO(RestaurantCategory restaurantCategory)
        {
            Name = restaurantCategory.Name;
        }
        public string Name { get; set; }
    }
}
