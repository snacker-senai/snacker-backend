using System;

namespace Snacker.Domain.DTOs
{
    public class PersonDTO
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
        public RestaurantDTO Restaurant { get; set; }
        public long RestaurantId { get; set; }
        public AddressDTO Address { get; set; }
        public long AddressId { get; set; }
    }
}