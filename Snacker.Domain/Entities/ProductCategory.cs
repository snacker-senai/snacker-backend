using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
    }
}
