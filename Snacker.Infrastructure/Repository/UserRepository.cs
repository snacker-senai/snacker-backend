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

        public override void Insert(User obj)
        {
            var address = _mySqlContext.Set<Address>().Add(obj.Person.Address);
            _mySqlContext.SaveChanges();
            obj.Person.AddressId = address.Entity.Id;
            var person = _mySqlContext.Set<Person>().Add(obj.Person);
            _mySqlContext.SaveChanges();
            obj.PersonId = person.Entity.Id;
            _mySqlContext.Set<User>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        public override void Update(User obj)
        {
            _mySqlContext.Entry(obj.Person.Address).State = EntityState.Modified;
            _mySqlContext.SaveChanges();
            _mySqlContext.Entry(obj.Person).State = EntityState.Modified;
            _mySqlContext.SaveChanges();
            _mySqlContext.Entry(obj).State = EntityState.Modified;
            _mySqlContext.SaveChanges();
        }

        public override void Delete(long id)
        {
            var user = Select(id);
            _mySqlContext.Set<User>().Remove(user);
            _mySqlContext.SaveChanges();
            _mySqlContext.Set<Person>().Remove(user.Person);
            _mySqlContext.SaveChanges();
            _mySqlContext.Set<Address>().Remove(user.Person.Address);
            _mySqlContext.SaveChanges();
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
