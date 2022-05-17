using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class RestaurantRepository : BaseRepository<Restaurant>
    {
        public RestaurantRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public override void Insert(Restaurant obj)
        {
            var address = _mySqlContext.Set<Address>().Add(obj.Address);
            _mySqlContext.SaveChanges();
            obj.AddressId = address.Entity.Id;
            _mySqlContext.Set<Restaurant>().Add(obj);
            _mySqlContext.SaveChanges();
        }

        public override ICollection<Restaurant> Select()
        {
            return _mySqlContext.Set<Restaurant>().Include(p => p.RestaurantCategory).Include(p => p.Address).ToList();
        }

        public override Restaurant Select(int id)
        {
            return _mySqlContext.Set<Restaurant>().Include(p => p.RestaurantCategory).Include(p => p.Address).FirstOrDefault(p => p.Id == id);
        }
    }
}
