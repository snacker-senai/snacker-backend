using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IProductCategoryService : IBaseService<ProductCategory>
    {
        ICollection<object> GetWithProducts();
    }
}
