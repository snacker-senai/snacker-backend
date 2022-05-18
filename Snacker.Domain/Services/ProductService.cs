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
                var dto = new ProductDTO()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Image = item.Image,
                    ProductCategory = new ProductCategoryDTO()
                    {
                        Name = item.ProductCategory.Name
                    },
                    ProductCategoryId = item.ProductCategoryId,
                    Restaurant = new RestaurantDTO()
                    {
                        Name = item.Restaurant.Name,
                        Description = item.Restaurant.Description,
                        Address = new AddressDTO()
                        {
                            CEP = item.Restaurant.Address.CEP,
                            City = item.Restaurant.Address.City,
                            Country = item.Restaurant.Address.Country,
                            District = item.Restaurant.Address.District,
                            Number = item.Restaurant.Address.Number,
                            Street = item.Restaurant.Address.Street
                        },
                        AddressId = item.Restaurant.AddressId,
                        RestaurantCategory = new RestaurantCategoryDTO()
                        {
                            Name = item.Restaurant.RestaurantCategory.Name
                        },
                        RestaurantCategoryId = item.Restaurant.RestaurantCategoryId
                    },
                    RestaurantId = item.RestaurantId

                };
                result.Add(dto);
            }
            return result;
        }

        public override object GetById(long id)
        {
            var item = _productRepository.Select(id);
            return new ProductDTO()
            {
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Image = item.Image,
                ProductCategory = new ProductCategoryDTO()
                {
                    Name = item.ProductCategory.Name
                },
                ProductCategoryId = item.ProductCategoryId,
                Restaurant = new RestaurantDTO()
                {
                    Name = item.Restaurant.Name,
                    Description = item.Restaurant.Description,
                    Address = new AddressDTO()
                    {
                        CEP = item.Restaurant.Address.CEP,
                        City = item.Restaurant.Address.City,
                        Country = item.Restaurant.Address.Country,
                        District = item.Restaurant.Address.District,
                        Number = item.Restaurant.Address.Number,
                        Street = item.Restaurant.Address.Street
                    },
                    AddressId = item.Restaurant.AddressId,
                    RestaurantCategory = new RestaurantCategoryDTO()
                    {
                        Name = item.Restaurant.RestaurantCategory.Name
                    },
                    RestaurantCategoryId = item.Restaurant.RestaurantCategoryId
                },
                RestaurantId = item.RestaurantId
            };
        }
    }
}
