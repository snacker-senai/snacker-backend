using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using System.Collections.Generic;
using System;

namespace Snacker.Domain.Interfaces
{
    public interface IProductService : IBaseService<Product>
    {
        ICollection<object> GetFromRestaurant(long restaurantId);
        ICollection<object> GetFromRestaurantWhereActive(long restaurantId);
        ICollection<ProductTopSellingDTO> GetTopSelling(long restaurantId,DateTime initialDate, DateTime finalDate);
    }
}
