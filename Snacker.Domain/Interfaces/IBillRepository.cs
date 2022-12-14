using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        ICollection<Bill> SelectActiveFromTable(long tableId);
        Bill SelectByIdWithoutRelationships(long billId);
    }
}
