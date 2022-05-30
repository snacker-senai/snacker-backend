using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Bill : BaseEntity
    {
        [Required]
        public bool Active { get; set; }
        [Required]
        public long TableId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}
