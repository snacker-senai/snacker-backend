using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using System.Collections.Generic;

namespace Snacker.Domain.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        ICollection<object> GetFromRestaurant(long restaurantId);
        ICollection<ProductTopSellingDTO> GetTopSelling(long restaurantId);
    }
}
