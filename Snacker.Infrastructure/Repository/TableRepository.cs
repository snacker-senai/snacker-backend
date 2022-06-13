using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class TableRepository : BaseRepository<Table>, ITableRepository
    {
        public TableRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public ICollection<Table> SelectFromRestaurant(long restaurantId)
        {
            return _mySqlContext.Set<Table>().Where(p => p.RestaurantId == restaurantId).ToList();
        }
    }
}
