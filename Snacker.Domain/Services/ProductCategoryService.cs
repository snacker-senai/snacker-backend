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

        public ICollection<object> GetWithProducts()
        {
            var itens = _productCategoryRepository.SelectWithProducts();
            var result = new List<object>();
            foreach (var item in itens)
            {
                if (item.Products.Any())
                {
                    var dto = new ProductCategoryWithRelationshipDTO()
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
                    foreach (var product in item.Products)
                    {
                        dto.Products = new List<ProductWithoutRelationshipDTO>
                        {
                            new ProductWithoutRelationshipDTO
                            {
                                Id = product.Id,
                                Name = product.Name,
                                Description = product.Description,
                                Image = product.Image,
                                Price = product.Price,
                                Active = product.Active,
                            }
                        };
                    }
                    result.Add(dto);
                }
            }
            return result;
        }
    }
}
