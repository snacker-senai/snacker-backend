using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Address : BaseEntity
    {
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Country { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }
    }
}
