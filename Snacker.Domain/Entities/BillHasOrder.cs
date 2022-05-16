using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class BillHasOrder : BaseEntity
    {
        [Required]
        public long BillId { get; set; }
        [JsonIgnore]
        public Bill Bill { get; set; }
        [Required]
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
