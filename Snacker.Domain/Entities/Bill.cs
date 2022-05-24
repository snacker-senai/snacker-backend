using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snacker.Domain.Entities
{
    public class Bill : BaseEntity
    {
        [Required]
        public bool Active { get; set; }
        [Required]
        public ICollection<BillHasOrder> BillHasOrderCollection { get; set; }
    }
}
