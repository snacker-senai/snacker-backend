using Snacker.Domain.Entities;
using System;

namespace Snacker.Domain.DTOs
{
    public class PersonDTO : BaseDTO
    {
        public PersonDTO()
        {

        }
        public PersonDTO(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            BirthDate = person.BirthDate;
            Phone = person.Phone;
            Document = person.Document;
            Restaurant = new RestaurantDTO(person.Restaurant);
            RestaurantId = person.RestaurantId;
        }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
        public RestaurantDTO Restaurant { get; set; }
        public long RestaurantId { get; set; }
    }
}