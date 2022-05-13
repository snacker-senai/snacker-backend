using System.ComponentModel.DataAnnotations;

namespace Snacker.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
