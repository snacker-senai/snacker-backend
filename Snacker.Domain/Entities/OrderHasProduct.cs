using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class OrderHasProduct : BaseEntity
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public long OrderId { get; set; }
        public string Details { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
        [Required]
        public long ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        [Required]
        public long OrderStatusId { get; set; }
        [JsonIgnore]
        public OrderStatus OrderStatus { get; set; }
    }
}
