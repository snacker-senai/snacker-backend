using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Restaurant : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public long AddressId { get; set; }
        [Required]
        public long RestaurantCategoryId { get; set; }
        [JsonIgnore]
        public Address Address { get; set; }
        [JsonIgnore]
        public RestaurantCategory RestaurantCategory { get; set; }
    }
}
