using System.ComponentModel.DataAnnotations;

namespace Snacker.Domain.Entities
{
    public class UserType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
