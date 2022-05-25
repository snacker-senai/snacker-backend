using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class UserTypeDTO
    {
        public UserTypeDTO(UserType userType)
        {
            Name = userType.Name;
        }
        public string Name { get; set; }
    }
}
