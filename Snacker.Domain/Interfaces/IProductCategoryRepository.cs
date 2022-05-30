using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategory>
    {
        ICollection<ProductCategory> SelectWithProducts(long restaurantId);
    }
}
