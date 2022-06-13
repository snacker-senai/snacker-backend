using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface ITableService : IBaseService<Table>
    {
        ICollection<object> GetFromRestaurant(long restaurantId);
    }
}
