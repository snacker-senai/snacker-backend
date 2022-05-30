using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Infrastructure.Repository
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(MySqlContext mySqlContext) : base(mySqlContext)
        {
        }

        public ICollection<ProductCategory> SelectWithProducts(long restaurantId)
        {
            return _mySqlContext.Set<ProductCategory>().Include(p => p.Products).Where(p => p.RestaurantId == restaurantId).ToList();
        }
    }
}
