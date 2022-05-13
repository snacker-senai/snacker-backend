using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public IList<Product> Products { get; set; }
    }
}
