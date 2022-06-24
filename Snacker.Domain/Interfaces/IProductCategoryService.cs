using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IProductCategoryService : IBaseService<ProductCategory>
    {
        ICollection<ProductCategoryWithRelationshipDTO> GetWithProducts(long restaurantId);
        ICollection<ProductCategory> GetFromRestaurant(long restaurantId);
    }
}
