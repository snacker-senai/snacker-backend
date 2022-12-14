using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface ITableRepository : IBaseRepository<Table>
    {
        ICollection<Table> SelectFromRestaurant(long restaurantId);
        string GetTableNumber(long tableId);
    }
}
