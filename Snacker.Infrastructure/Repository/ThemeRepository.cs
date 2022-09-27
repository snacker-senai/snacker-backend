using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class ThemeRepository : BaseRepository<Theme>, IThemeRepository
    {
        public ThemeRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public ICollection<Theme> SelectFromRestaurant(long restaurantId)
        {
            return _mySqlContext.Set<Theme>().Where(p => p.RestaurantId == restaurantId).ToList();
        }
    }
}
