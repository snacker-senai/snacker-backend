using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Snacker.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool ChangePassword { get; set; }
        [JsonIgnore]
        public long PersonId { get; set; }
        [Required]
        public long UserTypeId { get; set; }
        [Required]
        public Person Person { get; set; }
        [JsonIgnore]
        public UserType UserType { get; set; }
    }
}
