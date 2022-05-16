using System.Collections.Generic;
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
        [Required]
        public ICollection<OrderHasProduct> OrderHasProductCollection { get; set; }

        [Required]
        public ICollection<BillHasOrder> BillHasOrderCollection { get; set; }
    }
}
