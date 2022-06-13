using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class RestaurantCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; }
        [JsonIgnore]
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
