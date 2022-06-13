using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snacker.Domain.DTOs
{
    public class PaginatedReturnDTO
    {
        public int Page { get; set; }
        public ICollection<object> Itens { get; set; }
    }
}
