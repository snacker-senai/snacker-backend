using Snacker.Domain.Entities;
using Snacker.Infrastructure.Context;

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
    }
}
