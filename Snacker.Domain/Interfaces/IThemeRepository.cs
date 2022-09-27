using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IThemeRepository : IBaseRepository<Theme>
    {
        ICollection<Theme> SelectFromRestaurant(long restaurantId);
    }
}
