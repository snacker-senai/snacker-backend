using System.ComponentModel.DataAnnotations;

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
    }
}
