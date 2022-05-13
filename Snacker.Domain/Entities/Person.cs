using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class Person : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Document { get; set; }
        [Required]
        public long RestaurantId { get; set; }
        [JsonIgnore]
        public long AddressId { get; set; }
        [JsonIgnore]
        public Restaurant Restaurant { get; set; }
        [Required]
        public Address Address { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
