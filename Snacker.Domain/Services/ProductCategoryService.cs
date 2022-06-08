using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Snacker.Domain.Services
{
    public class ProductCategoryService : BaseService<ProductCategory>, IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository) : base(productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public ICollection<ProductCategory> GetFromRestaurant(long restaurantId)
        {
            return _productCategoryRepository.SelectFromRestaurant(restaurantId);
        }

        public ICollection<object> GetWithProducts(long restaurantId)
        {
            var itens = _productCategoryRepository.SelectWithProducts(restaurantId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                if (item.Products.Any())
                {
                    var dto = new ProductCategoryWithRelationshipDTO(item);
                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
