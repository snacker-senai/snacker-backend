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

        public string GetTableNumber(long tableId)
        {
            return _mySqlContext.Set<Table>().Where(p => p.Id == tableId).First().Number;
        }

        public ICollection<Table> SelectFromRestaurant(long restaurantId)
        {
            return _mySqlContext.Set<Table>().Where(p => p.RestaurantId == restaurantId).ToList();
        }
    }
}
