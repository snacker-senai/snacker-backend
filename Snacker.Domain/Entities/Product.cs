using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public long ProductCategoryId { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public ProductCategory ProductCategory { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
        [JsonIgnore]
        public ICollection<OrderHasProduct> OrderHasProductCollection { get; set; }
    }
}
