using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class RestaurantCategoryDTO : BaseDTO
    {
        public RestaurantCategoryDTO()
        {

        }
        public RestaurantCategoryDTO(RestaurantCategory restaurantCategory)
        {
            Id = restaurantCategory.Id;
            Name = restaurantCategory.Name;
        }
        public string Name { get; set; }
    }
}
