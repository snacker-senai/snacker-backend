using System.Collections.Generic;
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
        public bool Active { get; set; }
        [JsonIgnore]
        public long AddressId { get; set; }
        [Required]
        public long RestaurantCategoryId { get; set; }
        [Required]
        public Address Address { get; set; }
        [JsonIgnore]
        public RestaurantCategory RestaurantCategory { get; set; }
        [JsonIgnore]
        public Theme Theme { get; set; }
        [JsonIgnore]
        public ICollection<Person> Persons { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
        [JsonIgnore]
        public ICollection<ProductCategory> ProductCategories { get; set; }
        [JsonIgnore]
        public ICollection<Table> Tables { get; set; }
    }
}
