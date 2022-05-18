using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>
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

        public override ICollection<User> Select()
        {
            return _mySqlContext.Set<User>().Include(p => p.UserType).Include(p => p.Person).Include(p => p.Person.Address).Include(p => p.Person.Restaurant).Include(p => p.Person.Restaurant.Address).Include(p => p.Person.Restaurant.RestaurantCategory).ToList();
        }

        public override User Select(long id)
        {
            return _mySqlContext.Set<User>().Include(p => p.UserType).Include(p => p.Person).Include(p => p.Person.Address).Include(p => p.Person.Restaurant).Include(p => p.Person.Restaurant.Address).Include(p => p.Person.Restaurant.RestaurantCategory).FirstOrDefault(p => p.Id == id);
        }
    }
}
