using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public override ICollection<User> Select()
        {
            return _mySqlContext.Set<User>().Include(p => p.UserType).Include(p => p.Person).Include(p => p.Person.Address).Include(p => p.Person.Restaurant).Include(p => p.Person.Restaurant.Address).Include(p => p.Person.Restaurant.RestaurantCategory).ToList();
        }

        public override User Select(long id)
        {
            return _mySqlContext.Set<User>().Include(p => p.UserType).Include(p => p.Person).Include(p => p.Person.Address).Include(p => p.Person.Restaurant).Include(p => p.Person.Restaurant.Address).Include(p => p.Person.Restaurant.RestaurantCategory).FirstOrDefault(p => p.Id == id);
        }

        public ICollection<User> SelectFromRestaurant(long restaurantId)
        {
            return _mySqlContext.Set<User>().Include(p => p.UserType).Include(p => p.Person).Include(p => p.Person.Address).Include(p => p.Person.Restaurant).Include(p => p.Person.Restaurant.Address).Include(p => p.Person.Restaurant.RestaurantCategory).Where(p => p.Person.RestaurantId == restaurantId).ToList();
        }

        public User ValidateUser(string email, string password)
        {
            return _mySqlContext.Set<User>().Include(p => p.UserType).Include(p => p.Person).Include(p => p.Person.Address).Include(p => p.Person.Restaurant).Include(p => p.Person.Restaurant.Address).Include(p => p.Person.Restaurant.RestaurantCategory).Where(p => p.Email.ToLower() == email.ToLower() && p.Password == password).FirstOrDefault();
        }
    }
}
