using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        ICollection<Product> SelectFromRestaurant(long restaurantId);
    }
}
