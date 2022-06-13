using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Table : BaseEntity
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
        [JsonIgnore]
        public ICollection<Bill> Bills { get; set; }
    }
}
