using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public long TableId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
    }
}
