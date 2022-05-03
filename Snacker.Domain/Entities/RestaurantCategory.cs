using System.ComponentModel.DataAnnotations;

namespace Snacker.Domain.Entities
{
    public class RestaurantCategory : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
