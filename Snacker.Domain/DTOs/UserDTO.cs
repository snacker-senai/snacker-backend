namespace Snacker.Domain.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserTypeDTO UserType { get; set; }
        public long UserTypeId { get; set; }
        public PersonDTO Person { get; set; }
        public long PersonId { get; set; }
    }
}
