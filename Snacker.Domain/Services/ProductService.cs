using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class ProductService : BaseService<Product>
    {
        private readonly IBaseRepository<Product> _productRepository;
        public ProductService(IBaseRepository<Product> productRepository) : base(productRepository)
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
    }
}
