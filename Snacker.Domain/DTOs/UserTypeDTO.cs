using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class UserTypeDTO : BaseDTO
    {
        public UserTypeDTO(UserType userType)
        {
            Id = userType.Id;
            Name = userType.Name;
        }
        public string Name { get; set; }
    }
}
