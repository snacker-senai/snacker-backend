using Snacker.Domain.Entities;

namespace Snacker.Domain.DTOs
{
    public class UserDTO : BaseDTO
    {
        public UserDTO()
        {

        }
        public UserDTO(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Password = user.Password;
            Person = new PersonDTO(user.Person);
            PersonId = user.PersonId;
            UserType = new UserTypeDTO(user.UserType);
            UserTypeId = user.UserTypeId;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserTypeDTO UserType { get; set; }
        public long UserTypeId { get; set; }
        public PersonDTO Person { get; set; }
        public long PersonId { get; set; }
    }
}
