using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class OrderHasProduct
    {
        [Required]
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        [Required]
        public long ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
