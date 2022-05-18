using Snacker.Domain.DTOs;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;
using System.Collections.Generic;

namespace Snacker.Domain.Services
{
    public class RestaurantService : BaseService<Restaurant>
    {
        private readonly IBaseRepository<Restaurant> _restaurantRepository;
        public RestaurantService(IBaseRepository<Restaurant> restaurantRepository) : base(restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public override Restaurant Add<TValidator>(Restaurant obj)
        {
            Validate(obj, Activator.CreateInstance<RestaurantValidator>());
            _restaurantRepository.Insert(obj);
            return obj;
        }

        public override ICollection<object> Get()
        {
            var itens = _restaurantRepository.Select();
            var result = new List<object>();
            foreach (var item in itens)
            {
                var dto = new RestaurantDTO()
                {

                    Name = item.Name,
                    Description = item.Description,
                    Address = new AddressDTO()
                    {
                        Street = item.Address.Street,
                        City = item.Address.City,
                        Country = item.Address.Country,
                        Number = item.Address.Number,
                        CEP = item.Address.CEP,
                        District = item.Address.District
                    },
                    AddressId = item.AddressId,
                    RestaurantCategory = new RestaurantCategoryDTO()
                    {
                        Name = item.RestaurantCategory.Name,
                    },
                    RestaurantCategoryId = item.RestaurantCategoryId,
                };
                result.Add(dto);
            }
            return result;
        }

        public override object GetById(long id)
        {
            var item = _restaurantRepository.Select(id);
            return new RestaurantDTO()
            {

                Name = item.Name,
                Description = item.Description,
                Address = new AddressDTO()
                {
                    Street = item.Address.Street,
                    City = item.Address.City,
                    Country = item.Address.Country,
                    Number = item.Address.Number,
                    CEP = item.Address.CEP,
                    District = item.Address.District
                },
                AddressId = item.AddressId,
                RestaurantCategory = new RestaurantCategoryDTO()
                {
                    Name = item.RestaurantCategory.Name,
                },
                RestaurantCategoryId = item.RestaurantCategoryId,
            };
        }
    }
}
