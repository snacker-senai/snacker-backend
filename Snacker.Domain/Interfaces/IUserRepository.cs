using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User ValidateUser(string email, string password);
        ICollection<User> SelectFromRestaurant(long restaurantId);
        User SelectFromRestaurantById(long restaurantId, long id);
    }
}
