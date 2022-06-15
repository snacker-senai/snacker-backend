using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snacker.Domain.DTOs
{
    public class CreateRestaurantDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDTO Address { get; set; }
        public long RestaurantCategoryId { get; set; }
        public bool Active { get; set; }
        public PersonDTO Person { get; set; }
        public UserDTO User { get; set; }
    }
}
