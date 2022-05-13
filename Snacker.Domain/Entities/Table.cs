using System.ComponentModel.DataAnnotations;

namespace Snacker.Domain.Entities
{
    public class Table : BaseEntity
    {
        [Required]
        public string Number { get; set; }
    }
}
