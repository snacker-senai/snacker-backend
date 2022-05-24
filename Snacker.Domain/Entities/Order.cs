using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public long TableId { get; set; }
        [Required]
        public long OrderStatusId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
        [JsonIgnore]
        public OrderStatus OrderStatus { get; set; }
        [JsonIgnore]
        public ICollection<OrderHasProduct> OrderHasProductCollection { get; set; }

        [JsonIgnore]
        public ICollection<BillHasOrder> BillHasOrderCollection { get; set; }
    }
}
