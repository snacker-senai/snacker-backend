using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class AddressDTO : BaseDTO
    {
        public AddressDTO(Address address)
        {
            Id = address.Id;
            CEP = address.CEP;
            City = address.City;
            Country = address.Country;
            District = address.District;
            Number = address.Number;
            Street = address.Street;
        }
        public string CEP { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
    }
}
