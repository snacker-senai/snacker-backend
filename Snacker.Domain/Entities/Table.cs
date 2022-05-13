using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Table : BaseEntity
    {
        [Required]
        public string Number { get; set; }
        [JsonIgnore]
        public IList<Order> Orders { get; set; }
    }
}
