using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IThemeService : IBaseService<Theme>
    {
        ICollection<object> GetFromRestaurant(long restaurantId);
    }
}
