namespace Snacker.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string CEP { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string Country { get; set; }
    }
}
