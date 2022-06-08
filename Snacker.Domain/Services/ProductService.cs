using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }

        public override ICollection<object> Get()
        {
            var itens = _productRepository.Select();
            var result = new List<object>();
            foreach (var item in itens)
            {
                var dto = new ProductDTO(item);
                result.Add(dto);
            }
            return result;
        }

        public override object GetById(long id)
        {
            var item = _productRepository.Select(id);
            return new ProductDTO(item);
        }

        public ICollection<object> GetFromRestaurant(long restaurantId)
        {
            var itens = _productRepository.SelectFromRestaurant(restaurantId);
            var result = new List<object>();
            foreach (var item in itens)
            {
                var dto = new ProductDTO(item);
                result.Add(dto);
            }
            return result;
        }
    }
}
