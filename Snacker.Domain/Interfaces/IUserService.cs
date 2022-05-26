using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        ICollection<object> GetFromRestaurant(long restaurantId);
        object GetFromRestaurantById(long restaurantId, long id);
    }
}
