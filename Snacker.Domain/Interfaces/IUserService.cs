using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        User ValidateUser(string email, string password);
        object Login(string email, string password);
        ICollection<object> GetFromRestaurant(long restaurantId);
        object GetFromRestaurantById(long restaurantId, long id);
        string GetTokenValue(string token, string claimType);
    }
}
