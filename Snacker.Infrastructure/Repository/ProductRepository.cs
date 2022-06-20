using Microsoft.EntityFrameworkCore;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Snacker.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
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

        public ICollection<Product> SelectFromRestaurant(long restaurantId)
        {
            return _mySqlContext.Set<Product>().Include(p => p.ProductCategory).Include(p => p.Restaurant).Include(p => p.Restaurant.RestaurantCategory).Include(p => p.Restaurant.Address).Where(p => p.RestaurantId == restaurantId).ToList();
        }

        public ICollection<Product> SelectTopSelling(long restaurantId)
        {
            return _mySqlContext.Set<Product>().Include(p => p.ProductCategory).Include(p => p.Restaurant).Include(p => p.Restaurant.RestaurantCategory).Include(p => p.Restaurant.Address).Include(p => p.OrderHasProductCollection).ThenInclude(p => p.Order).Where(p => p.RestaurantId == restaurantId).ToList();
        }
    }
}
