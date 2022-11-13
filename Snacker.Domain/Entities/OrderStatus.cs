using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class OrderStatus : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
        [JsonIgnore]
        public ICollection<OrderHasProduct> OrderHasProductList { get; set; }
    }
}
