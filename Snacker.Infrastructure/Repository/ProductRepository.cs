using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public override ICollection<Product> Select()
        {
            return _mySqlContext.Set<Product>().Include(p => p.ProductCategory).Include(p => p.Restaurant).Include(p => p.Restaurant.RestaurantCategory).Include(p => p.Restaurant.Address).ToList();
        }

        public override Product Select(long id)
        {
            return _mySqlContext.Set<Product>().Include(p => p.ProductCategory).Include(p => p.Restaurant).Include(p => p.Restaurant.RestaurantCategory).Include(p => p.Restaurant.Address).FirstOrDefault(p => p.Id == id);
        }
    }
}
